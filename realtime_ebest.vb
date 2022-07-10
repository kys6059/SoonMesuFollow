Option Explicit On

Imports XA_DATASETLib
Imports XA_SESSIONLib

Structure buytemplete '---------------------------------- 더 이상 사용하지 않음

    Dim B01_주문일 As String
    Dim B02_주문번호 As Long
    Dim B03_원주문번호 As Long
    Dim B04_주문시각 As String
    Dim B05_종목코드 As String
    Dim B06_종목명 As String
    Dim B07_매매구분 As String
    Dim B08_정정취소구분명 As String
    Dim B09_호가유형 As String
    Dim B10_주문가 As Double
    Dim B11_주문수량 As Long
    Dim B12_주문구분명 As String
    Dim B13_체결구분명 As String
    Dim B14_체결가 As Double
    Dim B15_체결수량 As Long
    Dim B16_약정시각 As String
    Dim B17_약정번호 As Long
    Dim B18_체결번호 As Long
    Dim B19_매매손익 As String
    Dim B20_미체결수량 As Long

End Structure

Structure 잔고Type
    Dim A01_종복번호 As String
    Dim A02_구분 As String
    Dim A03_잔고수량 As Long
    Dim A04_청산가능수량 As Long
    Dim A05_평균단가 As Single
    Dim A06_총매입금액 As Long
    Dim A07_매매구분 As String
    Dim A08_매매손익 As Long
    Dim A09_처리순번 As Long
    Dim A10_현재가 As Single
    Dim A11_평가금액 As Long
    Dim A12_평가손익 As Long
    Dim A13_수익율 As Single
End Structure

Structure 평가종합Type
    Dim 매매손익합계 As Long
    Dim cts_expcode As String
    Dim cts_medocd As String
    Dim 평가금액 As Long
    Dim 평가손익 As Long
End Structure

Module realtime_ebest

    Dim XASession1 As XASession = New XASession
    Dim XAQuery_계좌조회 As XAQuery = New XAQuery
    Dim XAQuery_선물옵션_잔고평가_이동평균조회 As XAQuery = New XAQuery
    Dim XAQuery_매수매도 As XAQuery = New XAQuery
    Dim XAQuery_구매가능수량조회 As XAQuery = New XAQuery
    Dim XAQuery_현재날짜조회 As XAQuery = New XAQuery
    Dim XAQuery_전체종목조회 As XAQuery = New XAQuery
    Dim XAQuery_EBEST_분봉데이터호출 As XAQuery = New XAQuery

    Public Const g_strServerAddress As String = "hts.etrade.co.kr"
    Public 거래비밀번호 As String = "3487"
    Public Const g_iPortNum As Integer = 20001
    Public EBESTisConntected As Boolean = False
    Public strAccountNum As String '계좌번호 - 로그인 결과에서 받아온다

    Public 주문가능금액, 인출가능금액 As Long
    Public 평가종합 As 평가종합Type = New 평가종합Type
    Public List잔고 As List(Of 잔고Type)
    Public 매도증거금조회Flag As Boolean = False

    Public 콜구매가능개수 As Integer = 0
    Public 풋구매가능개수 As Integer = 0
    Public 콜최대구매개수 As Integer = 0
    Public 풋최대구매개수 As Integer = 0
    Public 콜현재환매개수 As Integer = 0
    Public 풋현재환매개수 As Integer = 0

    Public totalBuyingCount As Integer '--------------------------------------------------------------- 더이상 사용하지 않음
    Public BuyList As List(Of buytemplete) '--------------------------------------------------------------- 더이상 사용하지 않음

    Private Sub Add_여러가지Handler()
        AddHandler XASession1.Login, AddressOf XASession1_Login   'VB에서 이벤트를 등록하는 방식임 --- 매우 중요
        AddHandler XAQuery_계좌조회.ReceiveData, AddressOf XAQuery_계좌조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_선물옵션_잔고평가_이동평균조회.ReceiveData, AddressOf XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_매수매도.ReceiveData, AddressOf XAQuery_매수매도_ReceiveData '-----------------------------------------------------------------매수매도 이벤트 등록
        AddHandler XAQuery_구매가능수량조회.ReceiveData, AddressOf XAQuery_구매가능수량조회_ReceiveData '구매가능 수량 조회
        AddHandler XAQuery_현재날짜조회.ReceiveData, AddressOf XAQuery_현재날짜조회_ReceiveData
        AddHandler XAQuery_전체종목조회.ReceiveData, AddressOf XAQuery_전체종목조회_ReceiveData
        AddHandler XAQuery_EBEST_분봉데이터호출.ReceiveData, AddressOf XAQuery_EBEST_분봉데이터호출_ReceiveData
    End Sub


    Public Function ConnectToEbest(ByVal id As String, ByVal pwd As String, ByVal cert As String, ByVal nvserverType As Integer, ByVal strServerAddress As String) As Boolean


        If EBESTisConntected = False Then

            XASession1.DisconnectServer() '예제에 따라 먼저 끊고 시작함

            ' 서버연결
            Dim bSuccess As Boolean = XASession1.ConnectServer(strServerAddress, g_iPortNum)

            If bSuccess = False Then
                Dim errNum As Integer = XASession1.GetLastError()
                Add_Log("일반", XASession1.GetErrorMessage(errNum))
                Return False
            End If

            Dim conn As Boolean = XASession1.Login(id, pwd, cert, nvserverType, True)
            If conn = False Then
                Dim errNum As Integer = XASession1.GetLastError()
                Add_Log("일반", XASession1.GetErrorMessage(errNum))
                Return False
            End If

            Add_여러가지Handler()

        End If

        Return True

    End Function

    Private Sub XASession1_Login(ByVal szCode As String, ByVal szMsg As String)

        Dim iAccountNum As Integer
        Dim tempAccount As String

        If szCode = "0000" Then
            iAccountNum = XASession1.GetAccountListCount()
            If iAccountNum > 1 Then
                For i = 0 To iAccountNum - 1
                    tempAccount = XASession1.GetAccountList(i)
                    If Mid(tempAccount, 10, 2) = "02" Then
                        strAccountNum = tempAccount
                    End If
                Next
            Else
                strAccountNum = XASession1.GetAccountList(0)
            End If

            Add_Log("일반", "Login Event 수신 완료")
            EBESTisConntected = True
            계좌조회() '계좌조회 호출

            Form1.Ebest_realTime_Start()

        End If

    End Sub

    Public Sub 계좌조회()

        If XAQuery_계좌조회 Is Nothing Then XAQuery_계좌조회 = New XAQuery
        XAQuery_계좌조회.ResFileName = "c:\ebest\xingApi\res\CFOBQ10500.res"

        XAQuery_계좌조회.SetFieldData("CFOBQ10500InBlock1", "RecCnt", 0, 1) '종목번호
        XAQuery_계좌조회.SetFieldData("CFOBQ10500InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_계좌조회.SetFieldData("CFOBQ10500InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"

        Dim nSuccess As Integer = XAQuery_계좌조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", "계좌조회() 함수호출 시 오류: " & nSuccess.ToString())

    End Sub

    Private Sub XAQuery_계좌조회_ReceiveData(ByVal szTrCode As String)

        주문가능금액 = Val(XAQuery_계좌조회.GetFieldData("CFOBQ10500OutBlock2", "OrdAbleAmt", 0)) '주문가능금액
        인출가능금액 = Val(XAQuery_계좌조회.GetFieldData("CFOBQ10500OutBlock2", "WthdwAbleAmt", 0))

        If TargetDate > 0 Then

        End If

    End Sub

    Public Sub 선물옵션_잔고평가_이동평균조회()

        If XAQuery_선물옵션_잔고평가_이동평균조회 Is Nothing Then XAQuery_선물옵션_잔고평가_이동평균조회 = New XAQuery

        XAQuery_선물옵션_잔고평가_이동평균조회.ResFileName = "c:\ebest\xingApi\res\t0441.res"
        XAQuery_선물옵션_잔고평가_이동평균조회.SetFieldData("t0441InBlock", "accno", 0, strAccountNum)      '계좌번호
        XAQuery_선물옵션_잔고평가_이동평균조회.SetFieldData("t0441InBlock", "passwd", 0, 거래비밀번호)                '비밀먼호
        XAQuery_선물옵션_잔고평가_이동평균조회.SetFieldData("t0441InBlock", "cts_expcode", 0, " ")                'cts_expcode
        XAQuery_선물옵션_잔고평가_이동평균조회.SetFieldData("t0441InBlock", "cts_medocd", 0, " ")                'cts_medocd



        Dim nSuccess As Integer = XAQuery_선물옵션_잔고평가_이동평균조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " 선물옵션_잔고평가_이동평균조회 오류: " & nSuccess.ToString())

    End Sub

    Private Sub XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData(ByVal szTrCode As String)

        If List잔고 Is Nothing Then
            List잔고 = New List(Of 잔고Type)
        Else
            List잔고.Clear()
        End If

        Console.WriteLine("XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData 이벤트 진입")

        평가종합.매매손익합계 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tdtsunik", 0))
        평가종합.cts_expcode = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "cts_expcode", 0)
        평가종합.cts_medocd = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "cts_medocd", 0)
        평가종합.평가금액 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tappamt", 0))
        평가종합.평가손익 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tsunik", 0))

        Dim count As Integer = XAQuery_선물옵션_잔고평가_이동평균조회.GetBlockCount("t0441OutBlock1")        ' Occurs 의 갯수를 구한다.

        For i As Integer = 0 To count - 1
            Dim it As 잔고Type = New 잔고Type
            it.A01_종복번호 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "expcode", i) '종목번호
            it.A02_구분 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "medosu", i) '구분
            it.A03_잔고수량 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "jqty", i))
            it.A04_청산가능수량 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "cqty", i))
            it.A05_평균단가 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "pamt", i))
            it.A06_총매입금액 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "mamt", i))
            it.A07_매매구분 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "medocd", i)
            it.A08_매매손익 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "dtsunik", i))
            it.A09_처리순번 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "sysprocseq", i))
            it.A10_현재가 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "price", i))
            it.A11_평가금액 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "appamt", i))
            it.A12_평가손익 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "dtsunik1", i))
            it.A13_수익율 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "sunikrt", i))
            List잔고.Add(it)

            '최대구매개수 계산   --- 팔 때 반대로 매수를 더 많이 하는 걸 방지하기 위해 추가함 20220623
            Dim callput As String = Mid(it.A01_종복번호, 1, 1)

            If callput = "2" Then
                If 콜최대구매개수 < it.A03_잔고수량 Then
                    콜최대구매개수 = it.A03_잔고수량
                    Add_Log("일반", "콜최대구매개수 변경 to  " & 콜최대구매개수.ToString())
                End If
            Else
                If 풋최대구매개수 < it.A03_잔고수량 Then
                    풋최대구매개수 = it.A03_잔고수량
                    Add_Log("일반", "풋최대구매개수 변경 to  " & 풋최대구매개수.ToString())
                End If
            End If


        Next

        Form1.Display계좌정보() '계좌정보를 다 가져 오면 화면에 한번 refresh해준다

    End Sub


    Public Sub 한종목매도(ByVal code As String, ByVal count As Integer)

        If XAQuery_매수매도 Is Nothing Then XAQuery_매수매도 = New XAQuery
        XAQuery_매수매도.ResFileName = "C:\eBEST\xingAPI\Res\CFOAT00100.res"

        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoIsuNo", 0, code) '종목번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "BnsTpCode", 0, "1")      '매매구분 매도-1, 매수 -2
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdprcPtnCode", 0, "03")   '호가유형 지정가 00, 시장가 03
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "OrdQty", 0, count) ' 주문수량 long타입

        Dim nSuccess As Integer = XAQuery_매수매도.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " 한종목매도 오류: " & nSuccess.ToString())

        Add_Log("일반", "한종목 매도 진입")

    End Sub

    Public Sub 한종목매수(ByVal code As String, ByVal count As Integer)

        If count > 0 Then

            If XAQuery_매수매도 Is Nothing Then XAQuery_매수매도 = New XAQuery
            XAQuery_매수매도.ResFileName = "C:\eBEST\xingAPI\Res\CFOAT00100.res"

            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoIsuNo", 0, code) '종목번호
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "BnsTpCode", 0, "2")      '매매구분 매도-1, 매수 -2
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdprcPtnCode", 0, "03")   '호가유형 지정가 00, 시장가 03
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "OrdQty", 0, count) ' 주문수량 long타입

            Dim nSuccess As Integer = XAQuery_매수매도.Request(False)
            If nSuccess < 0 Then Add_Log("일반", " 한종목매수 오류: " & nSuccess.ToString())

            Add_Log("일반", "한종목 매수 진입")
        Else
            Add_Log("일반", code & " 0개 매수가 호출됨")
        End If


    End Sub

    Private Sub XAQuery_매수매도_ReceiveData(ByVal szTrCode As String)

        Dim OrdNo As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock2", "OrdNo", 0)

        Dim 종목코드 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "FnoIsuNo", 0)
        Dim 심플종목코드 As String = Mid(종목코드, 6, 3) '290 같이 마지막 3자리
        Dim 주문가격 As Single = Math.Round(Val(XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "FnoOrdPrc", 0)), 2)
        Dim 주문수량 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "OrdQty", 0)
        Dim 매수매도구분 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "BnsTpCode", 0)

        Dim str As String = "주문No = " & OrdNo.ToString() & ",코드 = " & 종목코드 & ",가격 = " & 주문가격.ToString() & ",수량 = " & 주문수량.ToString() & ",구분 = " & 매수매도구분

        Add_Log("일반", str)

    End Sub

    Public Sub 구매가능수량조회(ByVal callput As Integer)

        'If 주문가능금액 = 0 Then 주문가능금액 = 100000000  - 이렇게 해도 주문가능수량은 0이었음. 매도 가능계좌가 아니라서 그럴 수도 있음

        Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))
        Dim code As String = it.Code(callput)

        If XAQuery_구매가능수량조회 Is Nothing Then XAQuery_구매가능수량조회 = New XAQuery
        XAQuery_구매가능수량조회.ResFileName = "C:\eBEST\xingAPI\Res\CFOAQ10100.res"
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "RecCnt", 0, 1)   '레코드카운트
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "QryTp", 0, "1")                '조회구분
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "OrdAmt", 0, 주문가능금액)                '주문금액
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "RatVal", 0, 1.0)                '비율값
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoIsuNo", 0, code) '종목번호
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "BnsTpCode", 0, "1")      '매매구분 매도-1, 매수 -2
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoOrdprcPtnCode", 0, "03")   '호가유형 지정가 00, 시장가 03

        Dim nSuccess As Integer = XAQuery_구매가능수량조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " XAQuery_구매가능수량조회 오류: " & nSuccess.ToString())

        'Console.WriteLine("일반", "XAQuery_구매가능수량조회 매수 진입")

    End Sub

    'XAQuery_구매가능수량조회
    Private Sub XAQuery_구매가능수량조회_ReceiveData(ByVal szTrCode As String)

        'Add_Log("일반", "XAQuery_구매가능수량조회 Received 이벤트 진입")

        Dim 종목코드 As String = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock1", "FnoIsuNo", 0)
        Dim 종목구분 As String = Left(종목코드, 1) '"2" - 콜, "3" - 풋

        Dim 선물옵션현재가 As Single = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "FnoNowPrc", 0))
        Dim 주문가능수량 As Long = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "OrdAbleQty", 0))
        Dim 신규주문가능수량 As Long = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "NewOrdAbleQty", 0))
        Dim 청산주문가능수량 As Long = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "LqdtOrdAbleQty", 0))
        Dim 사용예정증거금액 As Long = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "UsePreargMgn", 0))
        Dim 사용예정현금증거금액 As Long = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "UsePreargMnyMgn", 0))
        Dim 주문가능금액 As Long = Val(XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "OrdAbleAmt", 0))

        If 종목구분 = "2" Then
            If 콜구매가능개수 <> 신규주문가능수량 Then Add_Log("일반", "콜 구매가능개수 변경 " & 신규주문가능수량.ToString())
            콜구매가능개수 = 신규주문가능수량
        ElseIf 종목구분 = "3" Then
            If 풋구매가능개수 <> 신규주문가능수량 Then Add_Log("일반", "풋 구매가능개수 변경 " & 신규주문가능수량.ToString())
            풋구매가능개수 = 신규주문가능수량
        Else
            Add_Log("에러", "XAQuery_구매가능수량조회_ReceiveData에서 종목명이 비었거나 이상함")
        End If

        Dim str As String = "구매가능수량조회 선물옵션현재가 =" & 선물옵션현재가.ToString()
        str += ", 종목코드=" & 종목코드
        str += ", 주문가능수량=" & 주문가능수량.ToString()
        str += ", 신규주문가능수량=" & 신규주문가능수량.ToString()
        str += ", 청산주문가능수량=" & 청산주문가능수량.ToString()
        str += ", 사용예정증거금액=" & Format(사용예정증거금액, "###,###,###,#00")
        str += ", 사용예정현금증거금액=" & Format(사용예정현금증거금액, "###,###,###,#00")
        str += ", 주문가능금액=" & Format(주문가능금액, "###,###,###,#00")

        'Console.WriteLine(str)

    End Sub


    Public Sub XAQuery_현재날짜조회함수()

        If XAQuery_현재날짜조회 Is Nothing Then XAQuery_현재날짜조회 = New XAQuery
        XAQuery_현재날짜조회.ResFileName = "c:\ebest\xingApi\res\t0167.res"

        XAQuery_현재날짜조회.SetFieldData("t0167InBlock", "id", 0, "f92887") 'id

        Dim nSuccess As Integer = XAQuery_현재날짜조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " XAQuery_현재날짜조회 오류: " & nSuccess.ToString())

    End Sub


    Private Sub XAQuery_현재날짜조회_ReceiveData(ByVal szTrCode As String)

        Dim 오늘날짜 As Integer = Val(XAQuery_현재날짜조회.GetFieldData("t0167OutBlock", "dt", 0))

        If 오늘날짜 > 0 Then
            TargetDate = 오늘날짜
            sMonth = getsMonth(TargetDate).ToString() 'DB에서 읽은 날짜로부터 월물을 찾아낸다
            Form1.Timer_Change()
        End If

        Add_Log("일반", "EBEST 오늘날짜는 " & 오늘날짜.ToString() & ", sMonth = " & sMonth)
    End Sub

    Public Sub XAQuery_전체종목조회함수()
        If XAQuery_전체종목조회 Is Nothing Then XAQuery_전체종목조회 = New XAQuery
        XAQuery_전체종목조회.ResFileName = "c:\ebest\xingApi\res\t2301.res"

        Dim 구분 As String = "G" 'G" 정규, M:미니, W:위클리

        XAQuery_전체종목조회.SetFieldData("t2301InBlock", "yyyymm", 0, "20" & sMonth)
        XAQuery_전체종목조회.SetFieldData("t2301InBlock", "gubun", 0, 구분)

        Dim nSuccess As Integer = XAQuery_전체종목조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " XAQuery_전체종목조회 오류: " & nSuccess.ToString())

    End Sub

    Private Sub XAQuery_전체종목조회_ReceiveData(ByVal szTrCode As String)

        Dim callCount As Long = XAQuery_전체종목조회.GetBlockCount("t2301OutBlock1")
        Dim putCount As Long = XAQuery_전체종목조회.GetBlockCount("t2301OutBlock2")

        Dim highLimit As Single = Val(Form1.txt_UpperLimit.Text)
        Dim lowLimit As Single = Val(Form1.txt_LowerLimit.Text)
        Dim retCount As Integer = 0

        optionList.Clear()

        If callCount = putCount And callCount > 0 Then
            For i As Integer = 0 To callCount - 1

                Dim it As ListTemplate = New ListTemplate
                it.Initialize()

                it.HangSaGa = XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "actprice", i) '행사가

                it.Code(0) = XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "optcode", i) '콜옵션코드
                it.price(0, 0) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "open", i))
                it.price(0, 1) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "high", i))
                it.price(0, 2) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "low", i))
                it.price(0, 3) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "price", i))
                it.거래량(0) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "volume", i))
                it.시간가치(0) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock1", "timevl", i))

                it.Code(1) = XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "optcode", i) '콜옵션코드
                it.price(1, 0) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "open", i))
                it.price(1, 1) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "high", i))
                it.price(1, 2) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "low", i))
                it.price(1, 3) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "price", i))
                it.거래량(1) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "volume", i))
                it.시간가치(1) = Val(XAQuery_전체종목조회.GetFieldData("t2301OutBlock2", "timevl", i))

                If (it.price(0, 3) > lowLimit And it.price(0, 3) < highLimit) Or (it.price(1, 3) > lowLimit And it.price(1, 3) < highLimit) Then  '콜풋 둘 중 하나가 범위안에 들어오면
                    optionList.Add(it)
                    retCount += 1
                End If

            Next

            TotalCount = retCount

            SetSelectedIndex()
            XAQuery_EBEST_분봉데이터호출함수(0)                 '콜 그래프 Data 호출

            Console.WriteLine("옵션 종목 Count =  " & optionList.Count.ToString())

        End If

    End Sub

    Public Sub XAQuery_EBEST_분봉데이터호출함수(ByVal capplut As Integer)
        't8415 
        If XAQuery_EBEST_분봉데이터호출 Is Nothing Then XAQuery_EBEST_분봉데이터호출 = New XAQuery
        XAQuery_EBEST_분봉데이터호출.ResFileName = "c:\ebest\xingApi\res\t8415.res"

        Dim it As ListTemplate = optionList(selectedJongmokIndex(capplut))
        Dim code As String = it.Code(capplut)

        XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "shcode", 0, code) '코드 8자리
        XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "ncnt", 0, "5")
        XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "qrycnt", 0, "100")
        XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "nday", 0, "1")
        XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "edate", 0, TargetDate)
        XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "comp_yn", 0, "N")

        Dim nSuccess As Integer = XAQuery_EBEST_분봉데이터호출.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " XAQuery_EBEST_분봉데이터호출 오류: " & nSuccess.ToString())
    End Sub


    Private Sub XAQuery_EBEST_분봉데이터호출_ReceiveData(ByVal szTrCode As String)

        Dim callput As Integer

        Dim callputstriong As String = Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "shcode", 0), 1) '

        If callputstriong = "2" Then
            callput = 0
        Else
            callput = 1
        End If

        Data(callput).어제시고저종(0) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jisiga", 0))
        Data(callput).어제시고저종(1) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jihigh", 0))
        Data(callput).어제시고저종(2) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jilow", 0))
        Data(callput).어제시고저종(3) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jiclose", 0))

        Dim Count As Long = XAQuery_EBEST_분봉데이터호출.GetBlockCount("t8415OutBlock1")

        timeIndex = Count   'Time의 Count
        currentIndex = timeIndex - 1

        For i As Integer = 0 To Count - 1
            Data(callput).ctime(i) = Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "time", i), 4)
            Data(callput).price(i, 0) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "open", i))
            Data(callput).price(i, 1) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "high", i))
            Data(callput).price(i, 2) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "low", i))
            Data(callput).price(i, 3) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "close", i))
            Data(callput).거래량(i) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "jdiff_vol", i))
        Next

    End Sub


    Public Function 매도실행호출(ByVal callput As Integer) As Boolean

        Dim tempIndex As Integer = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다

        Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))

        Dim code As String = it.Code(callput)
        Dim 구매가능대비비율 As Single = Val(Form1.txt_구매가능대비비율.Text)
        Dim 최소구매가능개수 As Integer = Math.Min(콜구매가능개수, 풋구매가능개수)

        If 최소구매가능개수 > 2 And 구매가능대비비율 > 0 Then
            Dim singleCount As Single = 최소구매가능개수 * 구매가능대비비율
            Dim count As Integer = Math.Truncate(singleCount)
            한종목매도(code, count)
            Return True
        Else
            Return False
        End If

    End Function

End Module
