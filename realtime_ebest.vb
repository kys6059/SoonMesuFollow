﻿Option Explicit On

Imports System.Reflection
Imports System.Windows.Forms.DataVisualization.Charting
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
    Dim 현재까지주문금액 As Long
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
    Dim XAQuery_EBEST_순매수현황조회 As XAQuery = New XAQuery
    Dim XAQuery_EBEST_외국인선물_순매수현황조회 As XAQuery = New XAQuery

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

    Public 콜중간청산가능개수 As Integer = 0
    Public 풋중간청산가능개수 As Integer = 0

    Public 최종투자금액 As Long = 0
    Public 콜현재까지매수금액 As Long = 0
    Public 풋현재까지매수금액 As Long = 0

    Public totalBuyingCount As Integer '--------------------------------------------------------------- 더이상 사용하지 않음
    Public BuyList As List(Of buytemplete) '--------------------------------------------------------------- 더이상 사용하지 않음

    Public 행사가시작 As Single = -1
    Public 행사가끝 As Single = -1


    Private Sub Add_여러가지Handler()
        AddHandler XASession1.Login, AddressOf XASession1_Login   'VB에서 이벤트를 등록하는 방식임 --- 매우 중요
        AddHandler XAQuery_계좌조회.ReceiveData, AddressOf XAQuery_계좌조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_선물옵션_잔고평가_이동평균조회.ReceiveData, AddressOf XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_매수매도.ReceiveData, AddressOf XAQuery_매수매도_ReceiveData '-----------------------------------------------------------------매수매도 이벤트 등록
        AddHandler XAQuery_구매가능수량조회.ReceiveData, AddressOf XAQuery_구매가능수량조회_ReceiveData '구매가능 수량 조회
        AddHandler XAQuery_현재날짜조회.ReceiveData, AddressOf XAQuery_현재날짜조회_ReceiveData
        AddHandler XAQuery_전체종목조회.ReceiveData, AddressOf XAQuery_전체종목조회_ReceiveData
        AddHandler XAQuery_EBEST_분봉데이터호출.ReceiveData, AddressOf XAQuery_EBEST_분봉데이터호출_ReceiveData
        AddHandler XAQuery_EBEST_순매수현황조회.ReceiveData, AddressOf XAQuery_EBEST_순매수현황조회_ReceiveData
        AddHandler XAQuery_EBEST_외국인선물_순매수현황조회.ReceiveData, AddressOf XAQuery_EBEST_외국인선물_순매수현황조회_ReceiveData
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
            isRealFlag = True
            계좌조회() '계좌조회 호출

            Ebest_realTime_Start()

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

        If 최종투자금액 = 0 Then
            Dim 켈리지수비율 As Single = Val(Form2.txt_F2_켈리지수비율.Text)
            최종투자금액 = 주문가능금액 * 켈리지수비율
        End If

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

        'Console.WriteLine("XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData 이벤트 진입")

        평가종합.매매손익합계 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tdtsunik", 0))
        평가종합.cts_expcode = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "cts_expcode", 0)
        평가종합.cts_medocd = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "cts_medocd", 0)
        평가종합.평가금액 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tappamt", 0))
        평가종합.평가손익 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tsunik", 0))

        평가종합.현재까지주문금액 = 평가종합.평가금액 - 평가종합.평가손익

        Dim count As Integer = XAQuery_선물옵션_잔고평가_이동평균조회.GetBlockCount("t0441OutBlock1")        ' Occurs 의 갯수를 구한다.
        If count = 0 Then
            If 콜현재환매개수 > 0 Or 풋현재환매개수 > 0 Then
                Add_Log("일반", "잔고청산 완료")
            End If
            콜최대구매개수 = 0
            콜현재환매개수 = 0
            풋최대구매개수 = 0
            풋현재환매개수 = 0
            콜중간청산가능개수 = 0
            풋중간청산가능개수 = 0
            콜현재까지매수금액 = 0
            풋현재까지매수금액 = 0
        Else
            Dim 콜잔고있음 As Boolean = False
            Dim 풋잔고있음 As Boolean = False

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

                If callput = "2" And it.A02_구분 = "매수" Then

                    Dim 현재종목번호 As String = Right(it.A01_종복번호, 3)
                    Dim 신호의종목번호 As String = 현재살아있는신호의종목리턴하는함수(0)
                    If 현재종목번호 = 신호의종목번호 Then   '2개이상의 종목이 생기는 경우 신호가 발생할 때의 종목번호에 대해서만 최대구매개수 등 변경 진행함
                        If 콜최대구매개수 < it.A03_잔고수량 Then
                            Add_Log("신규매수", String.Format("콜최대구매개수 증가 {0} to {1} ", 콜최대구매개수, it.A03_잔고수량))
                            콜최대구매개수 = it.A03_잔고수량

                        ElseIf 콜최대구매개수 > it.A03_잔고수량 Then '------------------------------------------------------환매갯수 변경
                            Dim temp As Integer = 콜최대구매개수 - it.A03_잔고수량
                            If temp > 콜현재환매개수 Then
                                콜현재환매개수 = temp
                                Add_Log("청산", "청산으로 인한 콜현재환매개수 변경 to  " & 콜현재환매개수.ToString())
                            End If

                        End If
                    End If
                    콜잔고있음 = True
                ElseIf callput = "3" And it.A02_구분 = "매수" Then

                    Dim 현재종목번호 As String = Right(it.A01_종복번호, 3)
                    Dim 신호의종목번호 As String = 현재살아있는신호의종목리턴하는함수(1)
                    If 현재종목번호 = 신호의종목번호 Then   '2개이상의 종목이 생기는 경우 신호가 발생할 때의 종목번호에 대해서만 최대구매개수 등 변경 진행함
                        If 풋최대구매개수 < it.A03_잔고수량 Then
                            Add_Log("신규매수", String.Format("풋최대구매개수 증가 {0} to {1}", 풋최대구매개수, it.A03_잔고수량))
                            풋최대구매개수 = it.A03_잔고수량

                        ElseIf 풋최대구매개수 > it.A03_잔고수량 Then '------------------------------------------------------환매갯수 변경
                            Dim temp As Integer = 풋최대구매개수 - it.A03_잔고수량
                            If temp > 풋현재환매개수 Then
                                풋현재환매개수 = temp
                                Add_Log("청산", "풋현재환매개수 변경 to  " & 풋현재환매개수.ToString())
                            End If
                        End If
                    End If
                    풋잔고있음 = True
                End If
            Next

            If 콜잔고있음 = False Then
                콜최대구매개수 = 0
                콜현재환매개수 = 0
                콜중간청산가능개수 = 0
                콜현재까지매수금액 = 0
            End If
            If 풋잔고있음 = False Then
                풋최대구매개수 = 0
                풋현재환매개수 = 0
                풋중간청산가능개수 = 0
                풋현재까지매수금액 = 0
            End If
        End If



        Console.WriteLine(String.Format("잔고조회 카운트:{0}", count))
        Form2.Display계좌정보() '계좌정보를 다 가져 오면 화면에 한번 refresh해준다

    End Sub


    Public Sub 한종목매도(ByVal code As String, ByVal price As Single, ByVal count As Integer, ByVal str As String, ByVal 지정가시장가타입 As String)

        If XAQuery_매수매도 Is Nothing Then XAQuery_매수매도 = New XAQuery
        XAQuery_매수매도.ResFileName = "C:\eBEST\xingAPI\Res\CFOAT00100.res"

        Dim adjustPrice As Single = Math.Max(price - 0.3, 0.1)

        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoIsuNo", 0, code) '종목번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "BnsTpCode", 0, "1")      '매매구분 매도-1, 매수 -2
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdPrc", 0, adjustPrice)             '주문가격 double 타입
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdprcPtnCode", 0, 지정가시장가타입)   '호가유형 지정가 00, 시장가 03
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "OrdQty", 0, count) ' 주문수량 long타입

        Dim nSuccess As Integer = XAQuery_매수매도.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " 한종목매도 오류: " & nSuccess.ToString())

        Add_Log(str, "매도 진입 Code : " & code & ", 가격 = " & price.ToString() & ", 수량 = " & count.ToString())

    End Sub

    Public Sub 한종목매수(ByVal code As String, ByVal price As Single, ByVal count As Integer, ByVal str As String, ByVal 지정가시장가타입 As String)

        If count > 0 Then

            If XAQuery_매수매도 Is Nothing Then XAQuery_매수매도 = New XAQuery
            XAQuery_매수매도.ResFileName = "C:\eBEST\xingAPI\Res\CFOAT00100.res"

            Dim adjustPrice As Single = price + 0.1

            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoIsuNo", 0, code) '종목번호
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "BnsTpCode", 0, "2")      '매매구분 매도-1, 매수 -2
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdprcPtnCode", 0, 지정가시장가타입)   '호가유형 지정가 00, 시장가 03
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdPrc", 0, adjustPrice)             '주문가격 double 타입
            XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "OrdQty", 0, count) ' 주문수량 long타입

            Dim nSuccess As Integer = XAQuery_매수매도.Request(False)
            If nSuccess < 0 Then Add_Log("일반", " 한종목매수 오류: " & nSuccess.ToString())

            Add_Log(str, "매수 진입 Code : " & code & ", 가격 = " & price.ToString() & ", 수량 = " & count.ToString())
        Else
            Add_Log(str, code & " 0개 매수가 호출됨" & " Code : " & code & ", 가격 = " & price.ToString() & ", 수량 = " & count.ToString())
        End If

    End Sub

    Private Sub XAQuery_매수매도_ReceiveData(ByVal szTrCode As String)

        Dim OrdNo As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock2", "OrdNo", 0)
        Dim 종목코드 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "FnoIsuNo", 0)
        Dim 심플종목코드 As String = Mid(종목코드, 6, 3) '290 같이 마지막 3자리
        Dim 주문가격 As Single = Math.Round(Val(XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "FnoOrdPrc", 0)), 2)
        Dim 주문수량 As Integer = Val(XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "OrdQty", 0))
        Dim 매수매도구분 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "BnsTpCode", 0)

        '최대구매개수 계산   --- 팔 때 반대로 매수를 더 많이 하는 걸 방지하기 위해 추가함 20220623  --------------- 모의투자에서 매수, 매도를 연속으로 했더니 매수만 두번 들어오는 문제 혹인 20221012 환매갯수 계산을 잔고에서 처리하도록 변경함
        'Dim callput As String = Mid(종목코드, 1, 1)
        'Dim 환매개수string As String = ""

        'Dim intordno As Integer = Val(OrdNo)

        'If intordno > 0 Then '주문번호가 0보다 클 때만 현재환매갯수의 값을 바꾼다 '가끔 주문이 거부되어 0으로 들어오는 현상 확인됨 20220826
        'If 매수매도구분 = "2" Then         '매수라면
        '
        'If List잔고 IsNot Nothing Then
        '
        'Dim 매도종목매치 As Boolean = False
        '
        'For i As Integer = 0 To List잔고.Count - 1
        '
        'Dim it As 잔고Type = List잔고(i)
        'If it.A02_구분 = "매도" And it.A01_종복번호 = 종목코드 Then
        '매도종목매치 = True
        'Exit For
        'End If
        '
        'Next

        'If 매도종목매치 = True Then
        'If callput = "2" Then '콜이라면
        '환매개수string = String.Format("-매수,콜환매 {0} to {1} ", 콜현재환매개수, 콜현재환매개수 + 주문수량)
        '콜현재환매개수 = 콜현재환매개수 + 주문수량
        'Else
        '환매개수string = String.Format("-매수,풋환매 {0} to {1} ", 풋현재환매개수, 풋현재환매개수 + 주문수량)
        '풋현재환매개수 = 풋현재환매개수 + 주문수량
        'End If
        'End If
        '
        '
        'End If
        'End If
        'End If

        Dim str As String = "주문No=" & OrdNo.ToString() & ",코드=" & 종목코드 & ",가격=" & 주문가격.ToString() & ",수량=" & 주문수량.ToString() & ",구분=" & 매수매도구분 ' & 환매개수string

        If CInt(OrdNo) = 0 Then
            Add_Log("에러", str)
        End If


    End Sub

    Public Sub 구매가능수량조회(ByVal callput As Integer)  '매도에서 필요한 기능으로 현재 사용하지 않음

        '모의투자에서는 QryTp 일반/금액/비율이 동작하지 않음
        '실계좌에서는 금액으로 동작하는 거 확인하 (20220809)

        Dim 투자비율반영금액 As Long = 1.0

        If optionList.Count > 0 And selectedJongmokIndex(callput) >= 0 Then
            Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))
            Dim code As String = it.Code(callput)

            If XAQuery_구매가능수량조회 Is Nothing Then XAQuery_구매가능수량조회 = New XAQuery
            XAQuery_구매가능수량조회.ResFileName = "C:\eBEST\xingAPI\Res\CFOAQ10100.res"
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "RecCnt", 0, "1")   '레코드카운트
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "QryTp", 0, "1")                 '1-일반, 2-금액 3 - 비율
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "OrdAmt", 0, 투자비율반영금액)                '주문금액
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "RatVal", 0, 1)                '비율값
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoIsuNo", 0, code) '종목번호
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "BnsTpCode", 0, "1")      '매매구분 매도-1, 매수 -2
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoOrdPrc", 0, it.price(callput, 3))   '매수금액
            XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoOrdprcPtnCode", 0, "03")   '호가유형 지정가 00, 시장가 03

            Dim nSuccess As Integer = XAQuery_구매가능수량조회.Request(False)
            If nSuccess < 0 Then Add_Log("일반", " XAQuery_구매가능수량조회 오류: " & nSuccess.ToString())

            'Console.WriteLine("일반", "XAQuery_구매가능수량조회 매수 진입")
        End If


    End Sub

    'XAQuery_구매가능수량조회
    Private Sub XAQuery_구매가능수량조회_ReceiveData(ByVal szTrCode As String)  ' 매도에서 필요한 기능으로 현재 사용하지 않음

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

        '매수를 위해서 여유를 남긴다
        신규주문가능수량 = Math.Min(Math.Round(신규주문가능수량 * 0.9, 0), 신규주문가능수량 - 1)

        If currentIndex_순매수 >= 0 Then
            If EBESTisConntected = True Then  '매수마감시간안에서만 보여줌
                If 종목구분 = "2" Then
                    If 콜구매가능개수 <> 신규주문가능수량 Then
                        콜구매가능개수 = 신규주문가능수량
                        Form2.lbl_F2_콜구매가능개수.Text = String.Format("{0}  ({1})", 신규주문가능수량, 선물옵션현재가)
                    End If
                ElseIf 종목구분 = "3" Then
                    If 풋구매가능개수 <> 신규주문가능수량 Then
                        풋구매가능개수 = 신규주문가능수량
                        Form2.lbl_F2_풋구매가능개수.Text = String.Format("{0}  ({1})", 신규주문가능수량, 선물옵션현재가)
                    End If
                End If
            End If
        End If

        Dim str As String = "구매가능수량조회 선물옵션현재가 =" & 선물옵션현재가.ToString()
        str += ", 종목코드=" & 종목코드
        str += ", 주문가능수량=" & 주문가능수량.ToString()
        str += ", 신규주문가능수량=" & 신규주문가능수량.ToString()
        str += ", 청산주문가능수량=" & 청산주문가능수량.ToString()
        str += ", 사용예정증거금액=" & Format(사용예정증거금액, "###,###,###,#00")
        str += ", 사용예정현금증거금액=" & Format(사용예정현금증거금액, "###,###,###,#00")
        str += ", 주문가능금액=" & Format(주문가능금액, "###,###,###,#00")

        Console.WriteLine(str)

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
            Form2.Timer_Change()
        End If

        Add_Log("일반", "EBEST 오늘날짜는 " & 오늘날짜.ToString() & ", sMonth = " & sMonth)
    End Sub

    Public Sub XAQuery_전체종목조회함수()
        If XAQuery_전체종목조회 Is Nothing Then XAQuery_전체종목조회 = New XAQuery
        XAQuery_전체종목조회.ResFileName = "c:\ebest\xingApi\res\t2301.res"

        Dim 월물 As String = Form2.txt_월물.Text
        Dim 구분 As String = Form2.txt_week_정규.Text 'G" 정규, M:미니, W:위클리

        XAQuery_전체종목조회.SetFieldData("t2301InBlock", "yyyymm", 0, 월물)
        XAQuery_전체종목조회.SetFieldData("t2301InBlock", "gubun", 0, 구분)

        Dim nSuccess As Integer = XAQuery_전체종목조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", " XAQuery_전체종목조회 오류: " & nSuccess.ToString())

    End Sub

    Private Sub XAQuery_전체종목조회_ReceiveData(ByVal szTrCode As String)

        Dim callCount As Long = XAQuery_전체종목조회.GetBlockCount("t2301OutBlock1")
        Dim putCount As Long = XAQuery_전체종목조회.GetBlockCount("t2301OutBlock2")

        Dim highLimit As Single = Val(Form2.txt_UpperLimit.Text)
        Dim lowLimit As Single = Val(Form2.txt_LowerLimit.Text)
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

                If Form2.chk_ChangeTargetIndex.Checked = True Then   '바꿀 수 있을 대는 바꾼다
                    If (it.price(0, 3) > lowLimit And it.price(0, 3) < highLimit) Or (it.price(1, 3) > lowLimit And it.price(1, 3) < highLimit) Then  '콜풋 둘 중 하나가 범위안에 들어오면
                        optionList.Add(it)
                        retCount += 1
                    End If
                Else
                    If 행사가시작 < 0 Then '아직 신호에서 확정되지 않았으면 바꾼다
                        If (it.price(0, 3) > lowLimit And it.price(0, 3) < highLimit) Or (it.price(1, 3) > lowLimit And it.price(1, 3) < highLimit) Then  '콜풋 둘 중 하나가 범위안에 들어오면
                            optionList.Add(it)
                            retCount += 1
                        End If
                    Else '신호가 뜨고 확정이 되었으면 행사가시작과 끝 범위안에 있는 것만 추가한다
                        If Val(it.HangSaGa) >= 행사가시작 And Val(it.HangSaGa) <= 행사가끝 Then
                            optionList.Add(it)
                            retCount += 1
                        End If
                    End If
                End If


            Next

            TotalCount = retCount

            SetSelectedIndex_For_순매수()  '순매수용으로 변경함


            '만약 현재 cyclecount에 시가가 0으로 되어 있는게 있으면 전체 종목 수신할때 채워넣는다 --- 이건 1초에 2건까지 가능. 나머지는 3초에 한건만 가능 . 
            If optionList.Count > 0 Then

                일분옵션데이터채워넣기()

            End If

            Console.WriteLine("옵션 종목 Count =  " & optionList.Count.ToString())

        End If

    End Sub

    Private Sub 일분옵션데이터채워넣기()

        If currentIndex_1MIn <= 0 Or 일분옵션데이터(0).HangSaGa = Nothing Then Return
        For callput As Integer = 0 To 1

            If 일분옵션데이터(callput).price(currentIndex_1MIn, 0) <= 0 Then  '만약 마지막 시가가 비어있으면 시고저종 4개를 다 채운다

                'Add_Log("일반", "채워넣기 진입 - Callput : " + callput.ToString() + " 인덱스 = " + currentIndex_1MIn.ToString())

                '여기다가 행사가로부터 인덱스 뽑는 로직 추가함
                Dim index1 As Integer
                index1 = 행사가로부터인덱스찾기(콜선택된행사가(callput))
                Dim it As ListTemplate = optionList(index1)

                If it.HangSaGa = 일분옵션데이터(callput).HangSaGa Then


                    일분옵션데이터(callput).price(currentIndex_1MIn, 0) = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3)
                    일분옵션데이터(callput).price(currentIndex_1MIn, 1) = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3)
                    일분옵션데이터(callput).price(currentIndex_1MIn, 2) = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3)
                    일분옵션데이터(callput).price(currentIndex_1MIn, 3) = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3)

                    Dim 이전시간 As Integer = Val(일분옵션데이터(callput).ctime(currentIndex_1MIn - 1)) + 1  '시간을 안 넣으니 시간이 0으로 들어가서 신호 확인 시 하한치가 없으면 늦은시간에도 발생하는 문제점 발생하여 추가
                    일분옵션데이터(callput).ctime(currentIndex_1MIn) = 이전시간.ToString()



                    'Add_Log("일반", "채워넣기 성공- Callput : " + callput.ToString() + " 인덱스 = " + currentIndex_1MIn.ToString())

                Else
                    'Add_Log("--오류--", "채워넣기 실패 - 행사가 오류 Callput : " + callput.ToString() + " 인덱스 = " + currentIndex_1MIn.ToString())
                End If

            Else ' 만약 0보다 큰 값이라면 마지막 종가만 업데이트 한다

                Dim index1 As Integer
                index1 = 행사가로부터인덱스찾기(콜선택된행사가(callput))
                Dim it As ListTemplate = optionList(index1)

                If it.HangSaGa = 일분옵션데이터(callput).HangSaGa Then

                    Dim 배율 As Single = 일분옵션데이터(callput).price(currentIndex_1MIn, 3) / it.price(callput, 3)

                    If 배율 > 0.9 And 배율 < 1.1 Then
                        일분옵션데이터(callput).price(currentIndex_1MIn, 3) = it.price(callput, 3)
                    End If

                    'Add_Log("일반", "채워넣기 성공- Callput : " + callput.ToString() + " 인덱스 = " + currentIndex_1MIn.ToString())

                Else
                    'Add_Log("--오류--", "채워넣기 실패 - 행사가 오류 Callput : " + callput.ToString() + " 인덱스 = " + currentIndex_1MIn.ToString())
                End If

            End If

        Next


    End Sub


    Public Sub XAQuery_EBEST_분봉데이터호출함수(ByVal capplut As Integer)
        't8415 
        If XAQuery_EBEST_분봉데이터호출 Is Nothing Then XAQuery_EBEST_분봉데이터호출 = New XAQuery
        XAQuery_EBEST_분봉데이터호출.ResFileName = "c:\ebest\xingApi\res\t8415.res"

        If optionList.Count > 0 Then
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
        End If

    End Sub


    Private Sub XAQuery_EBEST_분봉데이터호출_ReceiveData(ByVal szTrCode As String)

        Dim callput As Integer
        Dim callputstriong As String = Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "shcode", 0), 1)
        If callputstriong = "2" Then
            callput = 0
        Else
            callput = 1
        End If

        Dim 종목코드 As String = XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "shcode", 0)
        Dim 행사가 As String = Right(종목코드, 3)
        Dim 현재인덱스 As Integer = 행사가로부터인덱스찾기(행사가)

        Dim targetDatelong As Long = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "date", 0))

        If targetDatelong <> TargetDate Then
            TargetDate = targetDatelong
        End If

        Data(callput).어제시고저종(0) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jisiga", 0))
        Data(callput).어제시고저종(1) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jihigh", 0))
        Data(callput).어제시고저종(2) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jilow", 0))
        Data(callput).어제시고저종(3) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock", "jiclose", 0))

        Dim Count As Long = XAQuery_EBEST_분봉데이터호출.GetBlockCount("t8415OutBlock1")



        If optionList Is Nothing Then Return
        If optionList.Count <= 0 Then Return

        If Form2.chk_자동저장모드.Checked = True Then  '이건 3시30분 지나서 저장하기 위해서 받는 부분


            ' 여기다가 전체 저장 or 일부저장 나눠서 넣기
            DB일간데이터리스트(현재인덱스, callput).Code = 종목코드
            DB일간데이터리스트(현재인덱스, callput).HangSaGa = 행사가

            Dim startPoint As Integer = 0

            For i As Integer = 0 To Count - 1

                If Val(Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "time", i), 4)) > 900 Then
                    DB일간데이터리스트(현재인덱스, callput).ctime(startPoint) = Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "time", i), 4)
                    DB일간데이터리스트(현재인덱스, callput).price(startPoint, 0) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "open", i))
                    DB일간데이터리스트(현재인덱스, callput).price(startPoint, 1) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "high", i))
                    DB일간데이터리스트(현재인덱스, callput).price(startPoint, 2) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "low", i))
                    DB일간데이터리스트(현재인덱스, callput).price(startPoint, 3) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "close", i))
                    DB일간데이터리스트(현재인덱스, callput).거래량(startPoint) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "jdiff_vol", i))

                    startPoint += 1
                End If


            Next



            모든인덱스수신됨Counter += 1
            Add_Log(현재인덱스.ToString(), 행사가 + " " + callput.ToString() + " 수신횟수 = " + 모든인덱스수신됨Counter.ToString())

        Else

            Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))

            일분옵션데이터(callput).Code = it.Code(callput)
            일분옵션데이터(callput).HangSaGa = it.HangSaGa

            Dim 거래량AtFirst As Long = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "jdiff_vol", 0))

            Dim startPoint As Integer = 0

            For i As Integer = 0 To Count - 1
                If Val(Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "time", i), 4)) > 900 Then
                    일분옵션데이터(callput).ctime(startPoint) = Left(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "time", i), 4)
                    일분옵션데이터(callput).price(startPoint, 0) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "open", i))
                    일분옵션데이터(callput).price(startPoint, 1) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "high", i))
                    일분옵션데이터(callput).price(startPoint, 2) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "low", i))
                    일분옵션데이터(callput).price(startPoint, 3) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "close", i))
                    일분옵션데이터(callput).거래량(startPoint) = Val(XAQuery_EBEST_분봉데이터호출.GetFieldData("t8415OutBlock1", "jdiff_vol", i))
                    startPoint += 1
                End If
            Next

            'EBEST는 장 시작전에도 1개가 들어와서 이렇게 1개만 들어올 때 장 전인지를 거래량으로 판단한다
            If Count <= 1 Then
                If 거래량AtFirst > 0 Then
                    timeIndex_1Min = startPoint   'Time의 Count
                Else
                    timeIndex_1Min = 0
                End If
            Else
                timeIndex_1Min = startPoint   'Time의 Count
            End If
            currentIndex_1MIn = timeIndex_1Min - 1

            '장전에 무수히 +가 되면 안되니 장 시작 후 풋코드를 받으면 ReceiveCount를 증가시킨다
            If currentIndex_1MIn >= 0 And callput = 1 Then
                ReceiveCount += 1
            End If



        End If




    End Sub
    Public Function 매도실행호출_1개(ByVal callput As Integer) As Boolean

        Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))

        Dim code As String = it.Code(callput)
        Dim price As Single = it.price(callput, 3)
        Dim count As Integer = 1

        한종목매도(code, price, count, "테스트", "03") '호가유형 지정가 00, 시장가 03
        Return True
    End Function

    '   Public Function 매도실행호출(ByVal callput As Integer) As Boolean

    '   Dim tempIndex As Integer = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다
    '  Dim price As Single = Data(callput).price(tempIndex, 3)
    ' Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))
    '
    ' Dim code As String = it.Code(callput)
    'Dim 구매가능대비비율 As Single = Val(Form1.txt_구매가능대비비율.Text)
    'Dim 최소구매가능개수 As Integer = Math.Min(콜구매가능개수, 풋구매가능개수)
    '
    'If 최소구매가능개수 > 0 And 구매가능대비비율 > 0 Then
    ' Dim singleCount As Single = 최소구매가능개수 * 구매가능대비비율
    'Dim count As Integer = Math.Truncate(singleCount)
    'If count > 0 Then 한종목매도(code, price, count)
    'Return True
    'Else
    'Return False
    'End If
    '
    'End Function

    Public Sub XAQuery_EBEST_분봉데이터호출함수_1분(ByVal capplut As Integer)
        't8415 
        If XAQuery_EBEST_분봉데이터호출 Is Nothing Then XAQuery_EBEST_분봉데이터호출 = New XAQuery
        XAQuery_EBEST_분봉데이터호출.ResFileName = "c:\ebest\xingApi\res\t8415.res"

        Dim 이번에선택된인덱스 As Integer = 0

        If Form2.chk_자동저장모드.Checked = True Then

            If 호출할인덱스번호 >= TotalCount Then
                호출할인덱스번호 = 0
                이번에선택된인덱스 = 0
            Else
                이번에선택된인덱스 = 호출할인덱스번호
                If capplut = 1 Then 호출할인덱스번호 += 1
            End If



        Else
            이번에선택된인덱스 = selectedJongmokIndex(capplut)
        End If

        If optionList Is Nothing Then Return

        If optionList.Count > 0 And 이번에선택된인덱스 >= 0 Then
            Dim it As ListTemplate = optionList(이번에선택된인덱스)
            Dim code As String = it.Code(capplut)

            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "shcode", 0, code) '코드 8자리
            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "ncnt", 0, "1")
            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "qrycnt", 0, "500") '비압축모델인 경우 최대 500건 - 8.3시간
            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "nday", 0, "1")
            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "sdate", 0, TargetDate) '종료일자
            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "edate", 0, TargetDate) '종료일자
            XAQuery_EBEST_분봉데이터호출.SetFieldData("t8415InBlock", "comp_yn", 0, "N") '비압축모델 N

            Dim nSuccess As Integer = XAQuery_EBEST_분봉데이터호출.Request(False)
            If nSuccess < 0 Then Add_Log("일반", " XAQuery_EBEST_분봉데이터호출 오류: " & nSuccess.ToString())
        End If

    End Sub

    Public Sub XAQuery_EBEST_외국인선물_순매수현황조회함수()
        If XAQuery_EBEST_외국인선물_순매수현황조회 Is Nothing Then XAQuery_EBEST_외국인선물_순매수현황조회 = New XAQuery
        XAQuery_EBEST_외국인선물_순매수현황조회.ResFileName = "c:\ebest\xingApi\res\t1664.res"

        XAQuery_EBEST_외국인선물_순매수현황조회.SetFieldData("t1664InBlock", "mgubun", 0, "3")  '선물 3
        XAQuery_EBEST_외국인선물_순매수현황조회.SetFieldData("t1664InBlock", "vagubun", 0, "2")   '1:수량, 2:금액
        XAQuery_EBEST_외국인선물_순매수현황조회.SetFieldData("t1664InBlock", "bdgubun", 0, "1")   '0:당일, 1:전일
        XAQuery_EBEST_외국인선물_순매수현황조회.SetFieldData("t1664InBlock", "cnt", 0, "999")               '갯수 : 30초마다 하나씩 들어와서 하루에 최대 810건 정도 들어 있음 999건으로 조회하면 다 들어옴


        Dim nSuccess As Integer = XAQuery_EBEST_외국인선물_순매수현황조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", "XAQuery_EBEST_외국인선물_순매수현황조회() 함수호출 시 오류: " & nSuccess.ToString())
    End Sub


    Private Sub XAQuery_EBEST_외국인선물_순매수현황조회_ReceiveData(ByVal szTrCode As String)

        Dim insertCount As Integer = 0
        Dim 외국인선물카운트 = XAQuery_EBEST_외국인선물_순매수현황조회.GetBlockCount("t1664OutBlock1")



        For i As Integer = 0 To 외국인선물카운트 - 1

            Dim 외 As Long
            Dim 시간 As Long

            시간 = Val(XAQuery_EBEST_외국인선물_순매수현황조회.GetFieldData("t1664OutBlock1", "dt", i))  '시간

            외 = Val(XAQuery_EBEST_외국인선물_순매수현황조회.GetFieldData("t1664OutBlock1", "tjj17", i))   '외국인

            Dim index As Integer = 시간으로_순매수인덱스찾기(시간)

            If index >= 0 Then

                순매수리스트(index).외국인_선물_순매수 = 외

                insertCount += 1

            End If

        Next




        Console.WriteLine("선물 순매리스리스트 수신 : " & insertCount.ToString() & "건")


    End Sub

    Private Function 시간으로_순매수인덱스찾기(ByVal 시간 As Long) As Integer

        Dim ret As Integer = -1

        If 순매수리스트 Is Nothing Then
            Add_Log("에러", "순매수리스트 Is Nothing")
            Return -1
        End If

        For i As Integer = 0 To 순매수리스트.Length - 1

            If 순매수리스트(i).sTime = 시간 Then
                ret = i
                Return ret
            End If

        Next


        Return -1
    End Function



    Public Sub XAQuery_EBEST_순매수현황조회함수()
        If XAQuery_EBEST_순매수현황조회 Is Nothing Then XAQuery_EBEST_순매수현황조회 = New XAQuery
        XAQuery_EBEST_순매수현황조회.ResFileName = "c:\ebest\xingApi\res\t1621.res"

        XAQuery_EBEST_순매수현황조회.SetFieldData("t1621InBlock", "upcode", 0, "001") '업종코드 : 코스피 001
        XAQuery_EBEST_순매수현황조회.SetFieldData("t1621InBlock", "nmin", 0, "1")   '분
        XAQuery_EBEST_순매수현황조회.SetFieldData("t1621InBlock", "cnt", 0, "999")               '갯수 : 30초마다 하나씩 들어와서 하루에 최대 810건 정도 들어 있음 999건으로 조회하면 다 들어옴
        XAQuery_EBEST_순매수현황조회.SetFieldData("t1621InBlock", "bgubun", 0, "0")               '0:당일, 1:전일

        Dim nSuccess As Integer = XAQuery_EBEST_순매수현황조회.Request(False)
        If nSuccess < 0 Then Add_Log("일반", "XAQuery_EBEST_순매수현황조회() 함수호출 시 오류: " & nSuccess.ToString())
    End Sub



    Private Sub XAQuery_EBEST_순매수현황조회_ReceiveData(ByVal szTrCode As String)

        순매수리스트카운트 = XAQuery_EBEST_순매수현황조회.GetBlockCount("t1621OutBlock1")

        'ReDim 순매수리스트(순매수리스트카운트 - 1)
        Dim 기관순매수적용비율 As Single = Val(Form2.txt_F2_기관순매수적용비율.Text)

        Dim 첫번째시간 As Integer = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "time", 0))
        Dim 정배열Flag As Boolean = False

        If 첫번째시간 <= 90000 Then
            정배열Flag = True
        Else
            정배열Flag = False
        End If

        If 정배열Flag = False Then
            For i As Integer = 0 To 순매수리스트카운트 - 1

                Dim 외, 연, 기 As Long

                순매수리스트(순매수리스트카운트 - 1 - i).sDate = XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "date", i)
                순매수리스트(순매수리스트카운트 - 1 - i).sTime = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "time", i))
                순매수리스트(순매수리스트카운트 - 1 - i).개인순매수 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "indmsamt", i))   '개인순매수 대금
                순매수리스트(순매수리스트카운트 - 1 - i).코스피지수 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "upclose", i)) '코스피지수
                순매수리스트(순매수리스트카운트 - 1 - i).기관순매수 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "sysmsamt", i))   '기관계 
                기 = 순매수리스트(순매수리스트카운트 - 1 - i).기관순매수
                연 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "monmsamt", i))   '연기금
                외 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "formsamt", i))   '외국인

                순매수리스트(순매수리스트카운트 - 1 - i).외국인순매수 = 외
                순매수리스트(순매수리스트카운트 - 1 - i).연기금순매수 = 연

                순매수리스트(순매수리스트카운트 - 1 - i).외국인_기관_순매수 = 외 + Math.Round(기 * 기관순매수적용비율)
                순매수리스트(순매수리스트카운트 - 1 - i).외국인_연기금_순매수 = 외 + Math.Round(연 * 기관순매수적용비율)
            Next
        Else
            For i As Integer = 0 To 순매수리스트카운트 - 1

                Dim 외, 연, 기 As Long

                순매수리스트(i).sDate = XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "date", i)
                순매수리스트(i).sTime = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "time", i))
                순매수리스트(i).개인순매수 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "indmsamt", i))   '개인순매수 대금
                순매수리스트(i).코스피지수 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "upclose", i)) '코스피지수
                순매수리스트(i).기관순매수 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "sysmsamt", i))   '기관계 
                기 = 순매수리스트(i).기관순매수
                연 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "monmsamt", i))   '연기금
                외 = Val(XAQuery_EBEST_순매수현황조회.GetFieldData("t1621OutBlock1", "formsamt", i))   '외국인

                순매수리스트(i).외국인순매수 = 외
                순매수리스트(i).연기금순매수 = 연

                순매수리스트(i).외국인_기관_순매수 = 외 + Math.Round(기 * 기관순매수적용비율)
                순매수리스트(i).외국인_연기금_순매수 = 외 + Math.Round(연 * 기관순매수적용비율)
            Next
        End If

        timeIndex_순매수 = 순매수리스트카운트
        currentIndex_순매수 = timeIndex_순매수 - 1
        Console.WriteLine("순매리스리스트 수신 : " & 순매수리스트카운트.ToString() & "건")


    End Sub

    Public Sub 전체잔고정리하기() '현재 청산 가능한 수량을 가진 수량을 모두 판다

        If List잔고 IsNot Nothing Then

            For i As Integer = 0 To List잔고.Count - 1
                Dim it As 잔고Type = List잔고(i)
                If it.A02_구분 = "매도" Then  '무엇인가 매도된 상태라면
                    Dim 종목번호 As String = it.A01_종복번호
                    Dim count As Integer
                    count = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                    If count > 0 Then 한종목매수(종목번호, it.A10_현재가, count, "매도전체를청산", "03")  '호가유형 지정가 00, 시장가 03
                End If
                If it.A02_구분 = "매수" Then  '무엇인가 매수된 상태라면
                    Dim 종목번호 As String = it.A01_종복번호
                    Dim count As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                    If count > 0 Then 한종목매도(종목번호, it.A10_현재가, count, "매수전체를청산", "03") '호가유형 지정가 00, 시장가 03
                End If
            Next
        End If
    End Sub

End Module
