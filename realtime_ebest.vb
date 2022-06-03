Imports XA_DATASETLib
Imports XA_SESSIONLib

Module realtime_ebest

    Dim XASession1 As XASession = New XASession
    Dim ev As _IXASessionEvents
    Dim XAQuery_매도증거금조회 As XAQuery ' = New XAQuery

    Public Const g_strServerAddress As String = "hts.etrade.co.kr"
    Public Const g_iPortNum As Integer = 20001
    Public isConntected As Boolean

    Public Function ConnectToEbest(ByVal id As String, ByVal pwd As String, ByVal cert As String, ByVal nvserverType As Integer, ByVal strServerAddress As String) As Boolean
        '예제에 따라 먼저 끊고 시작함
        XASession1.DisconnectServer()

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

        AddHandler XASession1.Login, AddressOf XASession1_Login   'VB에서 이벤트를 등록하는 방식임 --- 매우 중요

        Return True

    End Function

    Public Sub 매도증거금조회()
        Dim nSuccess As Integer
        Dim yyyy, tempMonth As String

        If XAQuery_매도증거금조회 Is Nothing Then
            XAQuery_매도증거금조회 = New XAQuery
            XAQuery_매도증거금조회.ResFileName = "C:\eBEST\xingAPI\Res\CFOBQ10800.res"    '매도증거금 조회

        End If

        yyyy = "20" + Left(sMonth, 2)

        tempMonth = Mid(Data(0).Code(0), 5, 1)

        'Inblok채우기
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "RecCnt", 0, 1)                  '레코드갯수 1
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "PrdgrpClssCode", 0, "01")      '상품군코드 - 고정
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "ClssGrpCode", 0, "501")                '기초자산코드 - KOSPI200 - 501

        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "BaseYear", 0, yyyy)         '기준연도
        Call XAQuery_매도증거금조회.SetFieldData("CFOBQ10800InBlock1", "FstmmTpCode", 0, tempMonth)         '최근월물구분


        nSuccess = XAQuery_매도증거금조회.Request(False)

        AddHandler XAQuery_매도증거금조회.ReceiveData, AddressOf XAQuery_매도증거금조회_ReceiveData  '-----------------------------------------------------------------이벤트 등록

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
        Else
            Add_Log("일반", "증거금 조회 실패")
        End If

    End Sub

    Private Sub XASession1_Login(ByVal szCode As String, ByVal szMsg As String)

        Add_Log("일반", "Login Event 수신 완료 - 매도증거금 조회")
        매도증거금조회()

    End Sub

End Module
