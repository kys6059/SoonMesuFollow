Option Explicit On

Imports CPSYSDIBLib
Imports CPUTILLib
Imports DSCBO1Lib

Structure MarketEyeObjType

    Dim code As String
    Dim iTime As Long
    Dim jong As Single
    Dim si As Single
    Dim go As Single
    Dim jue As Single
    Dim volume As Long
    Dim 장구분 As SByte
    Dim 전일거래량 As Long
    Dim 전일종가 As Single
    Dim 시간외단일예상체결가 As Single
    Dim 매도호가 As Single
    Dim 매수호가 As Single

End Structure

Module realtime_daishin

    Dim callputobj = New OptionCallput
    Dim oCur = New OptionCurOnly
    Dim optcodeobj As New CpOptionCode
    Dim chartobj As New FutOptChart
    Dim MarketEyeObj As New MarketEye
    Dim MarketEyeRetList As List(Of MarketEyeObjType) = New List(Of MarketEyeObjType)


    Public Sub InitObject() '날짜는 바꿀 수 있지만 월을 바꾸면 이미 지나간 월이되어 가져올 수 있는 Data 건수가 0이된다. 따라서 월물이 바뀔 때는 예전 날짜 data를 가져올 수 없다

        sMonth = optcodeobj.GetData(3, 0)
        Form1.Text = sMonth

    End Sub

    Public Function FindTargetDate() As Boolean
        Dim futureCode As String
        Dim ret As Boolean
        Dim tempTargetDateFromCybos, tempTargetDateFromForm As String

        Dim futurelist As New CpFutureCode  '선물
        Dim fMst As New FutureMst

        futureCode = futurelist.GetData(0, 0) '선물 코드
        fMst.SetInputValue(0, futureCode)
        ret = True


        Try
            fMst.BlockRequest()
        Catch ex As Exception
            ''MsgBox("사이보스에 접속되지 않은 상태입니다")
            ret = False
        End Try

        If ret = True Then
            CurrentTime = fMst.GetHeaderValue(82) '체결시간  ----------------------------------------------------------------- 장 시작전에 이게 어떻게 되는지 봐야 함 이게 0859 이렇게 들어오면 그 시간에 맞춰서 작업하는게 좋겠음
            tempTargetDateFromCybos = fMst.GetHeaderValue(31) '입회일자
            tempTargetDateFromForm = Form1.txt_TargetDate.Text

            If tempTargetDateFromForm <> "" Then 'Form의 값이 null이 아니면 Form의 값을 우선한다

                TargetDate = tempTargetDateFromForm

            Else 'form의 값이 null이라면 그대로 cybos거를 넣는다

                TargetDate = tempTargetDateFromCybos

            End If


        End If

        Return ret

    End Function

    Public Function GetTotalJongmokCount() As Integer

        Dim validCount, Count As Integer
        Dim HangSaGa, callCode, putCode As String
        Dim callPrice, putPrice As Single

        If oCur Is Nothing Then
            oCur = New OptionCurOnly
        End If

        callputobj.SetInputValue(0, sMonth)
        callputobj.BlockRequest()
        Count = callputobj.GetHeaderValue(0) '전체 종목 수 : t

        validCount = 0

        For i As Integer = 0 To Count - 1
            HangSaGa = callputobj.GetDataValue(2, i)
            callPrice = callputobj.GetDataValue(3, i)
            putPrice = callputobj.GetDataValue(11, i)
            callCode = callputobj.GetDataValue(0, i)
            putCode = callputobj.GetDataValue(10, i)

            If (callPrice > LowerLimt And callPrice < UpperLimit) Or (putPrice > LowerLimt And putPrice < UpperLimit) Then     '콜이나 풋 둘 중 하나가 0.2 ~6.0 사이에 있는 것만 등록한다 -> 전부가져오는걸로 변경했더니 가져오는 시간이 너무 오래 걸려서 다시 변경 (44종목 가져오는데 16초걸림)
                Data(validCount).HangSaGa = Mid(HangSaGa, 1, 3)
                Data(validCount).Code(0) = callCode
                Data(validCount).Code(1) = putCode
                validCount = validCount + 1
            End If

        Next
        Return validCount
    End Function

    Public Sub SetTimeDataForData(ByRef tempData() As DataSet)
        Dim si, bun As Integer
        Dim strTemp As String
        Dim i, totalTimeCount, num As Integer

        totalTimeCount = Int(420 / Interval)

        For i = 0 To TotalCount - 1
            For num = 1 To totalTimeCount

                If Interval = 1 Then
                    si = Int(num / 60) + 9
                    bun = Int(num Mod 60)
                ElseIf Interval = 5 Then
                    si = Int(num / 12) + 9
                    bun = Int((num Mod 12) * 5)
                End If

                si = si * 100 + bun
                strTemp = Str(si)
                tempData(i).ctime(num - 1) = strTemp '그리드 왼쪽 기준 시간의 값을 입력한다. 이건 나중에 SearchLine에서 쓰인다
            Next
        Next
    End Sub

    Public Sub GetAllData()

        Dim i, j, callput As Integer
        Dim Items(0 To 6) As Integer
        Dim tempdate1 As String
        Dim TimeLocalCount, iIndex As Integer
        Dim strCurrentTime As String

        For i = 0 To 6
            Items(i) = i
        Next i
        Items(6) = 8

        'self.objFutureChart.SetInputValue(5, [0, 1, 2, 3, 4, 5, 8])  # 요청항목 - 날짜, 시간,시가,고가,저가,종가,거래량

        'Add_Log("일반", TargetDate.ToString() + "일 종목 Count = " & TotalCount.ToString() + " 최소 " + Data(0).Code(0) + " 최대 " + Data(TotalCount - 1).Code(1))

        tempdate1 = TargetDate
        For callput = 0 To 1

            For i = 0 To TotalCount - 1
                chartobj.SetInputValue(0, Data(i).Code(callput))
                chartobj.SetInputValue(1, Asc("1"))
                chartobj.SetInputValue(2, TargetDate)
                chartobj.SetInputValue(3, TargetDate)
                chartobj.SetInputValue(4, 750)
                chartobj.SetInputValue(5, Items)
                chartobj.SetInputValue(6, Asc("m"))
                chartobj.SetInputValue(7, Interval)
                chartobj.BlockRequest()  '------------------------------------ 여기서 요청을 하고 이 뒤에서 응답받은 Data를 Data구조체에 집어넣는다

                TimeLocalCount = chartobj.GetHeaderValue(3) '이 종목에 해당하는 오늘 날짜 데이터가 몇개인지 가져온다 이게 풋이랑 다를 수 있으니 각각 받아야 한다
                If timeIndex < TimeLocalCount Then
                    timeIndex = TimeLocalCount
                    currentIndex = timeIndex - 1 '현재 인덱스는 전체 갯수에서 -1 한 값이 된다
                End If

                For j = 0 To TimeLocalCount - 1
                    strCurrentTime = chartobj.GetDataValue(1, j)
                    iIndex = FindIndexFormTime(strCurrentTime) '해당 시간이 몇번째 인덱스인지 찾아온다
                    Data(i).price(callput, iIndex, 0) = chartobj.GetDataValue(2, j)
                    Data(i).price(callput, iIndex, 1) = chartobj.GetDataValue(3, j)
                    Data(i).price(callput, iIndex, 2) = chartobj.GetDataValue(4, j)
                    Data(i).price(callput, iIndex, 3) = chartobj.GetDataValue(5, j)
                    Data(i).거래량(callput, iIndex) = chartobj.GetDataValue(6, j)
                Next

            Next
        Next

    End Sub

    Public Function FindIndexFormTime(ByVal strTime As String) As Integer

        Dim si, bun As Integer

        If Len(strTime) = 4 Then
            si = Val(Mid(strTime, 1, 2))
            bun = Val(Mid(strTime, 3, 2))
        ElseIf Len(strTime) = 3 Then
            si = Val(Mid(strTime, 1, 1))
            bun = Val(Mid(strTime, 2, 2))
        Else
            MsgBox("시간이 안맞음. 현재 시간이 " & strTime & "으로 나옴")
            FindIndexFormTime = -1
        End If
        FindIndexFormTime = (((si - 9) * (60 / Interval)) + (bun / Interval)) - 1
    End Function

    Public Sub GetMarketEyeData()

        Dim reqList As List(Of String) = New List(Of String)
        Dim rqField = New Integer() {
            0,              '종목코드(string)
            1,              '시간( ulong) - hhmm
            4,              '현재가(long or float)
            5,              '시가(long or float)
            6,              '고가(long or float)
            7,              '저가(long or float)
            8,                '매도호가
            9,                 '매수호가
            10,              '거래량( ulong)
            12,              '장구분(char or empty)   0 - 장전, 1 - 동시호가  2 - 장중
            22,              '전일거래량(ulong)
            23,              '전일종가(long or float)
            54               '시간외 단일 예상체결가
        }

        reqList.Clear()

        For i As Integer = 0 To TotalCount - 1

            reqList.Add(Data(i).Code(0))
            reqList.Add(Data(i).Code(1))

        Next

        If reqList.Count > 0 Then
            MarketEyeObj.SetInputValue(0, rqField)
            MarketEyeObj.SetInputValue(1, reqList.ToArray())
            MarketEyeObj.BlockRequest()

            Dim cnt As Integer = MarketEyeObj.GetHeaderValue(2)

            If cnt > 0 Then
                ParseData(MarketEyeObj)
            End If
        End If

    End Sub

    Private Sub ParseData(ByVal obj As MarketEye)

        Dim cnt As Integer = MarketEyeObj.GetHeaderValue(2)

        If MarketEyeRetList Is Nothing Then
            MarketEyeRetList = New List(Of MarketEyeObjType)
        Else
            MarketEyeRetList.Clear()
        End If

        For j As Integer = 0 To cnt - 1

            Dim m As MarketEyeObjType = New MarketEyeObjType
            m.code = MarketEyeObj.GetDataValue(0, j)
            m.iTime = MarketEyeObj.GetDataValue(1, j)
            m.jong = MarketEyeObj.GetDataValue(2, j)
            m.si = MarketEyeObj.GetDataValue(3, j)
            m.go = MarketEyeObj.GetDataValue(4, j)
            m.jue = MarketEyeObj.GetDataValue(5, j)
            m.매도호가 = MarketEyeObj.GetDataValue(6, j)
            m.매수호가 = MarketEyeObj.GetDataValue(7, j)
            m.volume = MarketEyeObj.GetDataValue(8, j)
            m.장구분 = MarketEyeObj.GetDataValue(9, j)
            m.전일거래량 = MarketEyeObj.GetDataValue(10, j)
            m.전일종가 = MarketEyeObj.GetDataValue(11, j)
            m.시간외단일예상체결가 = MarketEyeObj.GetDataValue(12, j)


            MarketEyeRetList.Add(m)
        Next

    End Sub

End Module

