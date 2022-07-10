Option Explicit On

Structure DataSet  '2개만 만든다. 0번은 콜, 1번은 풋으로 한다

    Dim HangSaGa As String
    Dim Code As String
    Dim ctime() As String  '시간 100개 콜풋 구분 없음
    Dim price(,) As Single '시간index, 시고저종
    Dim Big() As Single '시고저종
    Dim Small() As Single ' 시고저종
    Dim 거래량() As Long ' 100개
    Dim secondMin As Single
    Dim 어제시고저종() As Single


    Public Sub Initialize()
        ReDim ctime(100) '
        ReDim price(100, 3)  '콜풋, 시간, 시고저종
        ReDim Big(3) '시고저종을 기록해야해서 4개
        ReDim Small(3) '시고저종을 기록해야해서 4개
        ReDim 거래량(100) '시간대별 거래량을 기록해야 해서 100개가 필요함
        ReDim 어제시고저종(3) '시간대별 거래량을 기록해야 해서 100개가 필요함
    End Sub

End Structure

Structure SumDatSetType
    Dim siSum() As Single
    Dim jongSum() As Single
    Dim siMax As Single
    Dim siMin As Single
    Dim jongMax As Single
    Dim jongmin As Single

    Public Sub Initialze()
        ReDim siSum(100)
        ReDim jongSum(100)
        siMax = Single.MinValue
        jongMax = Single.MinValue
        siMin = Single.MaxValue
        jongmin = Single.MaxValue
    End Sub
End Structure


Structure ListTemplate
    Dim HangSaGa As String '콜풋
    Dim Code() As String '콜풋
    Dim price(,) As Single '콜풋, 시고저종
    Dim 거래량() As Long '콜풋
    Dim 시간가치() As Single

    Public Sub Initialize()
        ReDim Code(1)
        ReDim price(1, 3)  '콜풋, 시고저종
        ReDim 거래량(1) '콜풋
        ReDim 시간가치(1) '콜풋
    End Sub
End Structure


Module Module_common

    '전역변수 선언
    Public Data() As DataSet  '-------------------- 전체가 들어있는 자료형
    Public SumDataSet As SumDatSetType
    Public TargetDate As Long
    Public sMonth As String
    Public Interval As Integer
    Public TotalJongMokCount As Integer
    Public CurrentTime As Integer
    Public isRealFlag As Boolean
    Public TotalCount As Integer   '------------------------ Data 구조체를 항상 0만 쓸거기 때문에 이건 항상 1로 둔다
    Public UpperLimit, LowerLimt As Single
    Public timeIndex As Integer '시간이 내려감에따라 증가하는 인덱스  - 항상 초기화 필요(DB에서 가져올 때, 대신에서 가져올때)
    Public currentIndex As Integer '시뮬레이션할 때 현재커서 위치를 나타냄
    Public selectedJongmokIndex(1) As Integer
    Public JongmokTargetPrice As Single  '기준이 되는 targetprice - default 2.0
    Public timerCount As Integer
    Public timerMaxInterval As Integer

    Public optionList As List(Of ListTemplate)  '제일 왼쪽 grid에 표시될 option List 



    Public Sub InitDataStructure()

        ReDim Data(1) '2개만 만들고 콜은 0번, 풋은 항상 1번이다
        For i As Integer = 0 To 1
            Data(i).Initialize()
        Next

        SumDataSet.Initialze() '콜풋 합계 data를 위한 dataset

        TargetDate = 0
        sMonth = "0"
        Interval = Val(Form1.txt_Interval.Text)
        TotalJongMokCount = 0
        UpperLimit = Val(Form1.txt_UpperLimit.Text)
        LowerLimt = Val(Form1.txt_LowerLimit.Text)
        timeIndex = 0
        timerCount = 0 '16초마다 돌아가는 타이머 주기
        timerMaxInterval = 8 '7초보다 크면 0으로 바꿈
        currentIndex = -1
        timeIndex = -1

        JongmokTargetPrice = Val(Form1.txt_JongmokTargetPrice.Text)
        selectedJongmokIndex(0) = -1
        selectedJongmokIndex(1) = -1

        If optionList Is Nothing Then
            optionList = New List(Of ListTemplate)
        Else
            optionList.Clear()
        End If

        If ShinhoList Is Nothing Then
            ShinhoList = New List(Of ShinhoType)
        Else
            ShinhoList.Clear()
        End If



    End Sub

    '리턴값은 최소갭일 때의 상대방 인덱스, 그 때 최소갭은 ByRef 방식으로 리턴한다. 
    Public Function CalcTargetJonhmokIndex(ByVal callput As Integer, ByVal TargetPrice As Single, ByRef mingap As Single) As Integer

        Dim tempGap As Single
        Dim retIndex As Integer

        For i As Integer = 0 To optionList.Count - 1

            Dim it As ListTemplate = optionList(i)
            tempGap = Math.Abs(it.price(callput, 3) - TargetPrice)
            If mingap > tempGap And it.price(callput, 3) > 0 Then
                mingap = tempGap
                retIndex = i
            End If

        Next

        Return retIndex

    End Function

    Public Function GetCurrentPrice(ByVal callput As Integer, ByVal index As Integer, ByVal sigojuejong As Integer) As Single

        Dim retValue As Single = 0
        If index < TotalCount Then
            Dim it As ListTemplate = optionList(index)
            retValue = it.price(callput, sigojuejong)
        End If
        Return retValue

    End Function


    Public Sub SetSelectedIndex()
        If selectedJongmokIndex(0) < 0 Or Form1.chk_ChangeTargetIndex.Checked = True Then '아직 한번도 선택하지 않았거나 Checked가 True일 때만 자동으로 변경함

            Dim Targetprice As Single = Val(Form1.txt_JongmokTargetPrice.Text)
            Dim calltempMinGap As Single = 1100.0
            Dim puttempMinGap As Single = 1100.0

            Dim callTempSelectedIndex = CalcTargetJonhmokIndex(0, Targetprice, calltempMinGap)
            Dim putTempSelectedIndex = CalcTargetJonhmokIndex(1, Targetprice, puttempMinGap)

            Dim callprice = GetCurrentPrice(0, callTempSelectedIndex, 3)
            Dim putprice = GetCurrentPrice(1, putTempSelectedIndex, 3)

            If callprice > 0 And putprice > 0 Then
                Dim calltempMinGap2 As Single = 1100.0
                Dim puttempMinGap2 As Single = 1100.0
                Dim cIndex = CalcTargetJonhmokIndex(0, putprice, calltempMinGap2)
                Dim pIndex = CalcTargetJonhmokIndex(1, callprice, puttempMinGap2)

                If calltempMinGap2 < puttempMinGap2 Then
                    selectedJongmokIndex(0) = cIndex
                    selectedJongmokIndex(1) = putTempSelectedIndex
                Else
                    selectedJongmokIndex(0) = callTempSelectedIndex
                    selectedJongmokIndex(1) = pIndex
                End If
            Else
                selectedJongmokIndex(0) = callTempSelectedIndex
                selectedJongmokIndex(1) = putTempSelectedIndex
            End If

        End If
    End Sub

    Public Sub CalcSumPrice()

        SumDataSet.Initialze()

        For j As Integer = 0 To currentIndex

            '합계 계산
            If Data(0).price(j, 0) > 0 And Data(1).price(j, 0) > 0 Then
                SumDataSet.siSum(j) = Data(0).price(j, 0) + Data(1).price(j, 0)
                SumDataSet.jongSum(j) = Data(0).price(j, 3) + Data(1).price(j, 3)
            End If

        Next

        For j = 0 To currentIndex
            If SumDataSet.siMax < SumDataSet.siSum(j) Then SumDataSet.siMax = SumDataSet.siSum(j)
            If SumDataSet.siMin > SumDataSet.siSum(j) Then SumDataSet.siMin = SumDataSet.siSum(j)

            If SumDataSet.jongMax < SumDataSet.jongSum(j) Then SumDataSet.jongMax = SumDataSet.jongSum(j)
            If SumDataSet.jongmin > SumDataSet.jongSum(j) Then SumDataSet.jongmin = SumDataSet.jongSum(j)
        Next

    End Sub


    '최고, 최저, 제2저가 계산
    Public Sub CalcColorData()

        For callput As Integer = 0 To 1 '콜 풋 루프

            Dim secondmin As Single = 1000
            For j As Integer = 0 To 3 '시고저종 루프

                Dim min As Single = 1000
                Dim max As Single = 0

                For k As Integer = 0 To currentIndex - 1 '시간 루프
                    If Data(callput).price(k, j) > max And Data(callput).price(k, j) > 0 Then max = Data(callput).price(k, j) '최고값
                    If Data(callput).price(k, j) < min And Data(callput).price(k, j) > 0 Then min = Data(callput).price(k, j) '최저값
                    If j = 2 Then  '저가만 계산
                        If Data(callput).price(k, j) < secondmin And Data(callput).price(k, j) > min Then secondmin = Data(callput).price(k, j)
                    End If
                Next

                Data(callput).Big(j) = max
                Data(callput).Small(j) = min
                If j = 2 Then Data(callput).secondMin = secondmin

            Next

        Next
    End Sub

    '칼러를 반환하는 함수
    '칼라 0번은 빨강색
    '칼라 1번은 파란색 Chk_Display_Blue
    '칼라 2번은 까만색
    '칼라 3번은 하늘색 - 제2저가
    '칼라 4번은 그냥 흰색
    '칼라 5번은 오렌지색
    '칼라 6번은 분홍색
    '칼라 7번은 녹색 Chk_Display_Green
    Public Function ItsColor(ByVal iFlag As Integer, ByVal iIndex As Integer, ByVal sigojuejong As Integer) As Integer
        Dim color As Integer

        color = 4 '아래 조건에 아무데도 안걸리면 흰색

        If (Data(iFlag).price(iIndex, sigojuejong) > 0) Then

            '제2저가 - 하늘색
            If (Data(iFlag).price(iIndex, sigojuejong) > (Data(iFlag).secondMin + 0.005)) Then color = 3
            '최고가
            If (Math.Abs(Data(iFlag).price(iIndex, sigojuejong) - Data(iFlag).Big(sigojuejong)) < 0.005) Then color = 0
            '최저가
            If (Math.Abs(Data(iFlag).price(iIndex, sigojuejong) - Data(iFlag).Small(sigojuejong)) < 0.005) Then color = 1

        End If

        Return color

    End Function

    '마지막에 currentIndex에서는 0 값이 들어와서 마지막 조건일 때는 그 앞에걸로 하는 걸로 바꾼다
    Public Function GetMaxIndex() As Integer

        Dim ret As Integer

        If currentIndex > 79 Then
            ret = 79
        Else
            ret = currentIndex
        End If

        Return ret

    End Function


    Public Function getsMonth(ByVal idate As Long) As Long
        Dim i As Integer
        Dim today_date, next_month As Date
        Dim dayOfDate, monthOfDate, YearOfdate, nextmonth_year, next_month_month As Integer
        Dim 목요일count As Integer
        Dim tempdate As Date
        Dim getMonth As Long

        today_date = makeDate(idate)  '날짜를 만들고
        next_month = DateAdd("M", 1, today_date)

        dayOfDate = idate Mod 100
        monthOfDate = Month(today_date)
        YearOfdate = Year(today_date)

        nextmonth_year = Year(next_month)
        next_month_month = Month(next_month)

        목요일count = 0

        For i = 1 To dayOfDate

            tempdate = DateSerial(YearOfdate, monthOfDate, i)
            If Weekday(tempdate) = 5 Then ' 5 목요일
                목요일count = 목요일count + 1
            End If


        Next

        If 목요일count < 2 Then  '무조건 이번달과 같다

            getMonth = (YearOfdate Mod 100) * 100 + monthOfDate

        ElseIf 목요일count = 2 Then

            If Weekday(today_date) = 5 Then '오늘이 목요일이면
                '이번달이다
                getMonth = (YearOfdate Mod 100) * 100 + monthOfDate
            Else
                '다음달이다
                getMonth = (nextmonth_year Mod 100) * 100 + next_month_month
            End If

        Else '무조건 다음달이다
            getMonth = (nextmonth_year Mod 100) * 100 + next_month_month
        End If

        Return getMonth

    End Function


    Public Function getRemainDate(ByVal 월물 As String, ByVal idate As Long) As Integer

        Dim yearof월물, monthof월물, i, 목요일count As Integer
        Dim tempdate, 만기일, tempToday As Date

        yearof월물 = Int(Val(월물) / 100) + 2000
        monthof월물 = Int(Val(월물) Mod 100)
        목요일count = 0

        For i = 1 To 14
            tempdate = DateSerial(yearof월물, monthof월물, i)

            If Weekday(tempdate) = 5 Then
                목요일count = 목요일count + 1
            End If

            If 목요일count >= 2 Then '이날이 만기일임
                만기일 = tempdate
                Exit For
            End If

        Next

        tempToday = makeDate(idate)

        Return DateDiff("d", tempToday, 만기일)

    End Function

    Public Function makeDate(ByVal idate As Long) As Date

        Dim year, mon, dt As Integer
        Dim tempdate As Date

        '먼저 현재 행의 날짜 만들기
        If idate < 20000101 Then
            year = (idate / 10000) + 2000
        Else
            year = (idate / 10000)
        End If
        mon = (idate / 100) Mod 100
        dt = idate Mod 100
        tempdate = DateSerial(year, mon, dt)

        makeDate = tempdate

    End Function

    Public Sub Add_Log(ByVal str1 As String, ByVal str2 As String)

        Dim tDate As String
        tDate = Format(Now(), "MM-dd hh:mm:ss")

        If currentIndex > 0 Then
            Form1.txt_Log.Text = tDate & Data(0).ctime(currentIndex) & " " & str1 & " : " & str2 & vbCrLf & Form1.txt_Log.Text
        Else
            Form1.txt_Log.Text = tDate & " " & str1 & " : " & str2 & vbCrLf & Form1.txt_Log.Text
        End If
        Form1.txt_Log.Refresh()

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

End Module
