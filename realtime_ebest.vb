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
    Dim XAQuery_매도증거금조회 As XAQuery = New XAQuery
    Dim XAQuery_계좌조회 As XAQuery = New XAQuery
    Dim XAQuery_체결정보조회 As XAQuery = New XAQuery
    Dim XAQuery_선물옵션_잔고평가_이동평균조회 As XAQuery = New XAQuery
    Dim XAQuery_매수매도 As XAQuery = New XAQuery
    Dim XAQuery_구매가능수량조회 As XAQuery = New XAQuery

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

    Public totalBuyingCount As Integer '--------------------------------------------------------------- 더이상 사용하지 않음
    Public BuyList As List(Of buytemplete) '--------------------------------------------------------------- 더이상 사용하지 않음

    Private Sub Add_여러가지Handler()
        AddHandler XASession1.Login, AddressOf XASession1_Login   'VB에서 이벤트를 등록하는 방식임 --- 매우 중요
        AddHandler XAQuery_계좌조회.ReceiveData, AddressOf XAQuery_계좌조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_선물옵션_잔고평가_이동평균조회.ReceiveData, AddressOf XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_매도증거금조회.ReceiveData, AddressOf XAQuery_매도증거금조회_ReceiveData  '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_체결정보조회.ReceiveData, AddressOf XAQuery_체결정보조회_ReceiveData '-----------------------------------------------------------------이벤트 등록
        AddHandler XAQuery_매수매도.ReceiveData, AddressOf XAQuery_매수매도_ReceiveData '-----------------------------------------------------------------매수매도 이벤트 등록
        AddHandler XAQuery_구매가능수량조회.ReceiveData, AddressOf XAQuery_구매가능수량조회_ReceiveData '구매가능 수량 조회
    End Sub


    Public Function ConnectToEbest(ByVal id As String, ByVal pwd As String, ByVal cert As String, ByVal nvserverType As Integer, ByVal strServerAddress As String) As Boolean


        If EBESTisConntected = False Then

            XASession1.DisconnectServer() '예제에 따라 먼저 끊고 시작함

            ' 서버연결
            Dim bSuccess As Boolean = XASession1.ConnectServer(strServerAddress, g_iPortNum)

            If bSuccess = False Then
                Dim errNum As Integer = XASession1.GetLastError()
                MessageBox.Show(XASession1.GetErrorMessage(errNum))
                Return False
            End If

            Dim conn As Boolean = XASession1.Login(id, pwd, cert, nvserverType, True)
            If conn = False Then
                Dim errNum As Integer = XASession1.GetLastError()
                MessageBox.Show(XASession1.GetErrorMessage(errNum))
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

        End If

    End Sub

    Public Sub 계좌조회()

        If XAQuery_계좌조회 Is Nothing Then XAQuery_계좌조회 = New XAQuery
        XAQuery_계좌조회.ResFileName = "c:\ebest\xingApi\res\CFOBQ10500.res"

        XAQuery_계좌조회.SetFieldData("CFOBQ10500InBlock1", "RecCnt", 0, 1) '종목번호
        XAQuery_계좌조회.SetFieldData("CFOBQ10500InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_계좌조회.SetFieldData("CFOBQ10500InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"

        Dim nSuccess As Integer = XAQuery_계좌조회.Request(False)
        If nSuccess < 0 Then MessageBox.Show("계좌조회() 함수호출 시 오류: " & nSuccess.ToString())


        'Add_Log("일반", "계좌 조회시작")
    End Sub

    Private Sub XAQuery_계좌조회_ReceiveData(ByVal szTrCode As String)

        주문가능금액 = XAQuery_계좌조회.GetFieldData("CFOBQ10500OutBlock2", "OrdAbleAmt", 0) '주문가능금액
        인출가능금액 = XAQuery_계좌조회.GetFieldData("CFOBQ10500OutBlock2", "WthdwAbleAmt", 0)

        If TargetDate > 0 Then
            선물옵션_잔고평가_이동평균조회()
            'Add_Log("일반", "계좌조회Receive")
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
        If nSuccess < 0 Then MessageBox.Show(" 선물옵션_잔고평가_이동평균조회 오류: " & nSuccess.ToString())

        'Add_Log("일반", "선물옵션_잔고평가 진입")

    End Sub

    Private Sub XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData(ByVal szTrCode As String)

        If List잔고 Is Nothing Then
            List잔고 = New List(Of 잔고Type)
        Else
            List잔고.Clear()
        End If

        'Add_Log("일반", "XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData 이벤트 진입")
        Console.WriteLine("XAQuery_선물옵션_잔고평가_이동평균조회_ReceiveData 이벤트 진입")

        평가종합.매매손익합계 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tdtsunik", 0)
        평가종합.cts_expcode = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "cts_expcode", 0)
        평가종합.cts_medocd = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "cts_medocd", 0)
        평가종합.평가금액 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tappamt", 0)
        평가종합.평가손익 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock", "tsunik", 0)

        Dim count As Integer = XAQuery_선물옵션_잔고평가_이동평균조회.GetBlockCount("t0441OutBlock1")        ' Occurs 의 갯수를 구한다.

        For i As Integer = 0 To count - 1
            Dim it As 잔고Type = New 잔고Type
            it.A01_종복번호 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "expcode", i) '종목번호
            it.A02_구분 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "medosu", i) '구분
            it.A03_잔고수량 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "jqty", i)
            it.A04_청산가능수량 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "cqty", i)
            it.A05_평균단가 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "pamt", i)
            it.A06_총매입금액 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "mamt", i)
            it.A07_매매구분 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "medocd", i)
            it.A08_매매손익 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "dtsunik", i)
            it.A09_처리순번 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "sysprocseq", i)
            it.A10_현재가 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "price", i)
            it.A11_평가금액 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "appamt", i)
            it.A12_평가손익 = Val(XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "dtsunik1", i))
            it.A13_수익율 = XAQuery_선물옵션_잔고평가_이동평균조회.GetFieldData("t0441OutBlock1", "sunikrt", i)
            List잔고.Add(it)
        Next

        If 매도증거금조회Flag = False And Form1.chk_모의투자연결.Checked = False Then 매도증거금조회()

        Form1.Display계좌정보() '계좌정보를 다 가져 오면 화면에 한번 refresh해준다

    End Sub

    Public Sub 매도증거금조회()
        Dim nSuccess As Integer
        Dim yyyy, tempMonth As String

        If XAQuery_매도증거금조회 Is Nothing Then XAQuery_매도증거금조회 = New XAQuery

        XAQuery_매도증거금조회.ResFileName = "C:\eBEST\xingAPI\Res\CFOBQ10800.res"    '매도증거금 조회
        yyyy = "20" + Left(sMonth, 2)

        tempMonth = Mid(Data(0).Code(0), 5, 1)

        'Inblok채우기
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "RecCnt", 0, 1)                  '레코드갯수 1
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "PrdgrpClssCode", 0, "01")      '상품군코드 - 고정
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "ClssGrpCode", 0, "501")                '기초자산코드 - KOSPI200 - 501

        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "BaseYear", 0, yyyy)         '기준연도
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "FstmmTpCode", 0, tempMonth)         '최근월물구분

        nSuccess = XAQuery_매도증거금조회.Request(False)

        If nSuccess < 0 Then
            MessageBox.Show(" 체결정보조회 전송오류: " & nSuccess.ToString())
        Else
            Add_Log("일반", "매도증거금 조회 시작 - 성공 nSuccess = " & nSuccess.ToString())
        End If

    End Sub


    Private Sub XAQuery_매도증거금조회_ReceiveData(ByVal szTrCode As String)

        Add_Log("일반", "매도증거금 조회 이벤트 진입")

        Dim count As Integer = XAQuery_매도증거금조회.GetBlockCount("CFOBQ10800OutBlock2")        ' Occurs 의 갯수를 구한다.

        Dim tempCode As String
        Dim tempPrice, 옵션가격증거금, Total증거금 As Long

        For i As Integer = 0 To count - 1
            tempCode = XAQuery_매도증거금조회.GetFieldData("CFOBQ10800OutBlock2", "FnoIsuNo", i) '콜 종목코드 가져오기
            tempPrice = XAQuery_매도증거금조회.GetFieldData("CFOBQ10800OutBlock2", "OrdMgn1", i) '콜 증거금 가져오기 - 순위험증거금
            옵션가격증거금 = XAQuery_매도증거금조회.GetFieldData("CFOBQ10800OutBlock2", "BasePrc2", i) * 250000 '옵션가격증거금은 기준가 * 250000으로 계산함
            Total증거금 = tempPrice + 옵션가격증거금

            For j = 0 To TotalCount - 1
                If Data(j).Code(0) = tempCode Then
                    Data(j).증거금(0) = Total증거금
                    Exit For
                End If
            Next

            tempCode = XAQuery_매도증거금조회.GetFieldData("CFOBQ10800OutBlock2", "FnoIsuNo0", i) '풋 종목코드 가져오기
            tempPrice = XAQuery_매도증거금조회.GetFieldData("CFOBQ10800OutBlock2", "OrdMgn2", i) '풋 증거금 가져오기 - 순위험증거금
            옵션가격증거금 = XAQuery_매도증거금조회.GetFieldData("CFOBQ10800OutBlock2", "BasePrc2", i) * 250000 '옵션가격증거금은 기준가 * 250000으로 계산함
            Total증거금 = tempPrice + 옵션가격증거금

            For j = 0 To TotalCount - 1
                If Data(j).Code(1) = tempCode Then
                    Data(j).증거금(1) = Total증거금
                    Exit For
                End If
            Next

        Next

        If count > 0 Then
            Add_Log("일반", "증거금 조회 완료")
            매도증거금조회Flag = True
        Else
            Add_Log("일반", "증거금 조회실패 - 카운트가 0(모의투자에서는 제공하지 않음)")
            매도증거금조회Flag = False
        End If

    End Sub

    '이건 예전 방식임 - 더이상 사용하지 않음 20220604
    '선물옵션_잔고평가_이동평균조회 이게 새로 나온 거 같고 이게 좋아보여서 다시 구현함 
    Public Sub 체결정보조회()

        If XAQuery_체결정보조회 Is Nothing Then XAQuery_체결정보조회 = New XAQuery
        XAQuery_체결정보조회.ResFileName = "c:\ebest\xingApi\res\CFOAQ00600.res"  '체결정보조회

        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "RecCnt", 0, 1)                  '레코드갯수 1
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "AcntNo", 0, strAccountNum)      '계좌번호
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "InptPwd", 0, 거래비밀번호)                '비밀먼호

        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "QrySrtDt", 0, TargetDate.ToString())         '시작일
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "QryEndDt", 0, TargetDate.ToString())         '종료일
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "FnoClssCode", 0, "00")          '선물옵션 분류코드 00 전체 11 선물 22 옵션
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "PrdgrpCode", 0, "00")           '상품군코드 00 전체 01 주가지수 02 개별주식 03 가공채권 04 통화 05 상품 06 금리
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "PrdtExecTpCode", 0, "0")        '체결 구분  0 전체 1 체결 2 미체결
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "StnlnSeqTp", 0, "4")            '정렬순서 3:역순 4: 정순
        XAQuery_체결정보조회.SetFieldData("CFOAQ00600InBlock1", "CommdaCode", 0, "99")           '통신매체코드 99로 고정



        Dim nSuccess As Integer = XAQuery_체결정보조회.Request(False)
        If nSuccess < 0 Then MessageBox.Show(" 체결정보조회 오류: " & nSuccess.ToString())

    End Sub
    Private Sub XAQuery_체결정보조회_ReceiveData(ByVal szTrCode As String)

        If BuyList Is Nothing Then BuyList = New List(Of buytemplete)

        Dim orderCount As Long = XAQuery_체결정보조회.GetBlockCount("CFOAQ00600OutBlock3")        ' Occurs 의 갯수를 구한다.

        If orderCount > 0 Then
            If BuyList.Count > 0 Then

                Dim 첫번째체결정보주문시가 As String = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdTime", 0)
                If BuyList(0).B04_주문시각 = 첫번째체결정보주문시가 Then  '10개가 넘으면 나누어서 들어오는데, 첫번째 들어오는 row의 주문시각이 기존 리스트의 첫번째와 같은 시간이면 기존 리스트를 삭제하고 밑에서 다시 입력한다
                    BuyList.Clear()
                End If

            End If
        End If

        For i As Integer = 0 To orderCount - 1

            Dim it As buytemplete = New buytemplete
            it.B01_주문일 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdDt", i)
            it.B02_주문번호 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdNo", i)
            it.B03_원주문번호 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrgOrdNo", i)
            it.B04_주문시각 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdTime", i)
            it.B05_종목코드 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "FnoIsuNo", i)

            it.B06_종목명 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "IsuNm", i)
            it.B07_매매구분 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "BnsTpNm", i)
            it.B08_정정취소구분명 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "MrcTpNm", i)
            it.B09_호가유형 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "FnoOrdprcPtnNm", i)
            it.B10_주문가 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdPrc", i)

            it.B11_주문수량 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdQty", i)
            it.B12_주문구분명 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "OrdTpNm", i)
            it.B13_체결구분명 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "ExecTpNm", i)
            it.B14_체결가 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "ExecPrc", i)
            it.B15_체결수량 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "ExecQty", i)

            it.B16_약정시각 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "CtrctTime", i)
            it.B17_약정번호 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "CtrctNo", i)
            it.B18_체결번호 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "ExecNo", i)
            it.B19_매매손익 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "BnsplAmt", i)
            it.B20_미체결수량 = XAQuery_체결정보조회.GetFieldData("CFOAQ00600OutBlock3", "UnercQty", i)

            BuyList.Add(it)

        Next

        If XAQuery_체결정보조회.IsNext = 1 Then   ' 체결건수가 10건이 넘어서 다음에 또 있다고 나오면 true로request하면 다음걸 또 받아온다

            Dim nSuccess As Integer = XAQuery_체결정보조회.Request(True)
            If nSuccess < 0 Then MessageBox.Show(" 체결정보조회 전송오류: " & nSuccess.ToString())

        End If

    End Sub


    Public Sub 한종목매도(ByVal code As String, ByVal price As Single, ByVal count As Integer)

        If XAQuery_매수매도 Is Nothing Then XAQuery_매수매도 = New XAQuery
        XAQuery_매수매도.ResFileName = "C:\eBEST\xingAPI\Res\CFOAT00100.res"

        Dim adjustPrice As Single = price - 0.1

        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoIsuNo", 0, code) '종목번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "BnsTpCode", 0, "1")      '매매구분 매도-1, 매수 -2
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdprcPtnCode", 0, "00")   '호가유형 지정가 00, 시장가 03
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdPrc", 0, adjustPrice)             '주문가격 double 타입
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "OrdQty", 0, count) ' 주문수량 long타입

        Dim nSuccess As Integer = XAQuery_매수매도.Request(False)
        If nSuccess < 0 Then MessageBox.Show(" 한종목매도 오류: " & nSuccess.ToString())

        Add_Log("일반", "한종목 매도 진입")

    End Sub

    Public Sub 한종목매수(ByVal code As String, ByVal price As Single, ByVal count As Integer)

        If XAQuery_매수매도 Is Nothing Then XAQuery_매수매도 = New XAQuery
        XAQuery_매수매도.ResFileName = "C:\eBEST\xingAPI\Res\CFOAT00100.res"

        Dim adjustPrice As Single = price + 0.1

        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "AcntNo", 0, strAccountNum)   '계좌번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "Pwd", 0, 거래비밀번호)                '비밀먼호"
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoIsuNo", 0, code) '종목번호
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "BnsTpCode", 0, "2")      '매매구분 매도-1, 매수 -2
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdprcPtnCode", 0, "00")   '호가유형 지정가 00, 시장가 03
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "FnoOrdPrc", 0, adjustPrice)             '주문가격 double 타입
        XAQuery_매수매도.SetFieldData("CFOAT00100InBlock1", "OrdQty", 0, count) ' 주문수량 long타입

        Dim nSuccess As Integer = XAQuery_매수매도.Request(False)
        If nSuccess < 0 Then MessageBox.Show(" 한종목매수 오류: " & nSuccess.ToString())

        Add_Log("일반", "한종목 매수 진입")

    End Sub

    Private Sub XAQuery_매수매도_ReceiveData(ByVal szTrCode As String)

        Dim OrdNo As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock2", "OrdNo", 0)

        Dim 종목코드 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "FnoIsuNo", 0)
        Dim 심플종목코드 As String = Mid(종목코드, 6, 3) '290 같이 마지막 3자리
        Dim 주문가격 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "FnoOrdPrc", 0)
        Dim 주문수량 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "OrdQty", 0)
        Dim 매수매도구분 As String = XAQuery_매수매도.GetFieldData("CFOAT00100OutBlock1", "BnsTpCode", 0)

        Dim str As String = "주문접수 : OrdNo = " & OrdNo.ToString() & ", 종목코드 = " & 종목코드 & ", 심플종목코드 = " & 심플종목코드 & ", 주문가격 = " & 주문가격.ToString() & ", 주문수량 = " & 주문수량.ToString() & ", 매수매도구분 = " & 매수매도구분

        Add_Log("일반", str)

    End Sub

    Public Sub 구매가능수량조회(ByVal code As String, ByVal price As Single)

        Dim adjustPrice As Single = price - 0.1

        'If 주문가능금액 = 0 Then 주문가능금액 = 100000000  - 이렇게 해도 주문가능수량은 0이었음. 매도 가능계좌가 아니라서 그럴 수도 있음

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
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoOrdPrc", 0, adjustPrice)             '주문가격 double 타입
        XAQuery_구매가능수량조회.SetFieldData("CFOAQ10100InBlock1", "FnoOrdprcPtnCode", 0, "00")   '호가유형 지정가 00, 시장가 03

        Dim nSuccess As Integer = XAQuery_구매가능수량조회.Request(False)
        If nSuccess < 0 Then MessageBox.Show(" XAQuery_구매가능수량조회 오류: " & nSuccess.ToString())

        Add_Log("일반", "XAQuery_구매가능수량조회 매수 진입")

    End Sub

    'XAQuery_구매가능수량조회
    Private Sub XAQuery_구매가능수량조회_ReceiveData(ByVal szTrCode As String)

        Add_Log("일반", "XAQuery_구매가능수량조회 Received 이벤트 진입")

        Dim 종목코드 As String = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock1", "FnoIsuNo", 0)
        Dim 종목구분 As String = Left(종목코드, 1) '"2" - 콜, "3" - 풋

        Dim 선물옵션현재가 As Single = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "FnoNowPrc", 0)
        Dim 주문가능수량 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "OrdAbleQty", 0)
        Dim 신규주문가능수량 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "NewOrdAbleQty", 0)
        Dim 청산주문가능수량 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "LqdtOrdAbleQty", 0)
        Dim 사용예정증거금액 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "UsePreargMgn", 0)
        Dim 사용예정현금증거금액 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "UsePreargMnyMgn", 0)
        Dim 주문가능금액 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "OrdAbleAmt", 0)
        Dim 현금주문가능금액 As Long = XAQuery_구매가능수량조회.GetFieldData("CFOAQ10100OutBlock2", "MnyOrdAbleAmt", 0)

        If 종목구분 = "2" Then
            콜구매가능개수 = 신규주문가능수량
        ElseIf 종목구분 = "3" Then
            풋구매가능개수 = 신규주문가능수량
        Else
            Add_Log("에러", "XAQuery_구매가능수량조회_ReceiveData에서 종목명이 비었거나 이상함")
        End If

        Dim str As String = "구매가능수량조회 선물옵션현재가 =" & 선물옵션현재가.ToString()
        str += ", 종목코드=" & 종목코드
        str += ", 주문가능수량=" & 주문가능수량.ToString()
        str += ", 신규주문가능수량=" & 신규주문가능수량.ToString()
        str += ", 청산주문가능수량=" & 청산주문가능수량.ToString()
        str += ", 사용예정증거금액=" & 사용예정증거금액.ToString()
        str += ", 사용예정현금증거금액=" & 사용예정현금증거금액.ToString()
        str += ", 주문가능금액=" & Format(주문가능금액, "###,###,###,#00")
        str += ", 현금주문가능금액=" & Format(현금주문가능금액, "###,###,###,#00")

        Add_Log("일반", str)

    End Sub

End Module
