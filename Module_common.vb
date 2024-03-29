﻿Option Explicit On

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
    Public 콜선택된행사가(1) As String
    Public selectedJongmokIndex(1) As Integer

    Public JongmokTargetPrice As Single  '기준이 되는 targetprice - default 2.0
    Public timerCount As Integer
    Public timerMaxInterval As Integer
    Public ReceiveCount As Integer '잔고를 수신할 때마다 1씩 증가 시킨다. 이걸 이용해서 3 이상일 때 알고리즘을 판정한다.

    Public optionList As List(Of ListTemplate)  '제일 왼쪽 grid에 표시될 option List 


    Public 당일반복중_flag As Boolean = False '당일 반복 중에 매수를 수십회 하는 문제를 해결하기 위해 당일 반복 전후에 Flag를 설정하여 청산에서 확인한다

    '로그인 Data가 Received 되면 불려지는 함수
    Public Sub Ebest_realTime_Start()

        InitDataStructure()
        XAQuery_현재날짜조회함수() '프로그램 상 오늘 날짜를 가져온다

    End Sub

    Public Sub InitDataStructure()

        '이하 외국인순매수 데이터 확보용 자료구조 추가 20220821
        ReDim 순매수리스트(999) '외국인 순매수금액이 커지면 코스피 지수 상승하는 상황을 고려하는 리스트
        ReDim 일분옵션데이터(1)

        ReDim Data(1) '2개만 만들고 콜은 0번, 풋은 항상 1번이다
        For i As Integer = 0 To 1
            Data(i).Initialize()
            일분옵션데이터(i).Initialize()
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
        timerMaxInterval = 6 '이크면 0으로 바꿈
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
        Form2.lbl_F2_매매신호.Text = "0"
        Form2.lbl_F2_매매신호.BackColor = Color.White

    End Sub

    Public Function 인덱스로부터행사가찾기(ByVal index As Integer) As String

        Dim it As ListTemplate = optionList(index)
        Dim 행사가 As String = it.HangSaGa
        Return 행사가

    End Function

    '4방향 모두 검사하는 방식으로 변경함 - 2023.04.20 동작 중 이 함수에 안들어와서 좀 이상함
    Public Function CalcTargetJonhmokIndex(ByVal callput As Integer, ByVal 방향 As Single) As Integer


        Dim temptarget As Integer = -1
        Dim TargetPrice As Single = Val(Form1.txt_JongmokTargetPrice.Text)
        Dim mingap As Single = 1000.0

        For i As Integer = 0 To optionList.Count - 1

            Dim it As ListTemplate = optionList(i)

            If 방향 = 0 Then
                If it.price(callput, 3) >= 0.2 And it.price(callput, 3) < TargetPrice Then
                    Dim gap = Math.Abs(it.price(callput, 3) - TargetPrice)
                    If gap < mingap Then
                        mingap = gap
                        temptarget = i
                    End If
                End If

            Else
                If it.price(callput, 3) >= 0.2 And it.price(callput, 3) >= TargetPrice Then
                    Dim gap = Math.Abs(it.price(callput, 3) - TargetPrice)
                    If gap < mingap Then
                        mingap = gap
                        temptarget = i
                    End If
                End If
            End If

        Next

        Return temptarget

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

            Dim str As String = "SetSelectedIndex 진입 - selectedJongmokIndex(0) = " & selectedJongmokIndex(0).ToString() & " Form1.chk_ChangeTargetIndex.Checked = " & Form1.chk_ChangeTargetIndex.Checked.ToString()

            Console.WriteLine(str)

            Dim Targetprice As Single = Val(Form1.txt_JongmokTargetPrice.Text)
            Dim targetCallIndex As Single = 1100.0
            Dim targetPutIndex As Single = 1100.0
            Dim minGap As Single = 1100.0
            Dim 최종목표인덱스(1) As Integer
            Dim i, j As Integer
            Dim str10 As String = ""


            For i = 0 To 1 '방향

                Dim 목표인덱스 As Integer = CalcTargetJonhmokIndex(0, i)
                Dim 목표가격 As Single = GetCurrentPrice(0, 목표인덱스, 3)

                For j = 0 To 1 '반대편의 방향

                    Dim 반대목표인덱스 As Integer = CalcTargetJonhmokIndex(1, j)
                    Dim 반대목표가격 As Single = GetCurrentPrice(1, 반대목표인덱스, 3)

                    Dim gap As Single = Math.Abs(목표가격 - 반대목표가격)
                    Dim dist1 As Single = Math.Abs(목표가격 - Targetprice)
                    Dim dist2 As Single = Math.Abs(반대목표가격 - Targetprice)
                    Dim dist_avg As Single = (dist1 + dist2) / 2
                    Dim gap_dist As Single = Math.Round(gap + dist_avg, 2)
                    str10 = String.Format("cp= {1}, pp= {3},cIn={0}, pIn={2}, gap= {4}, dist={5},gap+dist= {6}", 목표인덱스, 목표가격, 반대목표인덱스, 반대목표가격, Math.Round(gap, 2), Math.Round(dist_avg, 2), gap_dist)

                    Console.WriteLine(str10)

                    '                     str10 = String.Format("cprice = {1}, pprice = {3},cIndex={0},  pIndex={2},  gap = {4}", 목표인덱스, 목표가격, 반대목표인덱스, 반대목표가격, Math.Round(gap, 2))
                    'If ReceiveCount > 0 Then Add_Log("탐색", str10)

                    If minGap > gap_dist Then
                        If ReceiveCount > 0 Then Add_Log("SET", str10)
                        minGap = gap_dist
                        targetCallIndex = 목표인덱스
                        targetPutIndex = 반대목표인덱스
                    End If

                Next

            Next

            '여기다가 행사가 추출하는 로직 추가함
            콜선택된행사가(0) = 인덱스로부터행사가찾기(targetCallIndex)
            콜선택된행사가(1) = 인덱스로부터행사가찾기(targetPutIndex)

        End If

        '여기다가 행사가로부터 인덱스 뽑는 로직 추가함
        Dim index1 As Integer
        index1 = 행사가로부터인덱스찾기(콜선택된행사가(0))
        If index1 >= 0 Then selectedJongmokIndex(0) = index1

        Dim index2 As Integer
        index2 = 행사가로부터인덱스찾기(콜선택된행사가(1))
        If index2 >= 0 Then selectedJongmokIndex(1) = index2

        If ShinhoList.Count > 0 Then

            Dim shinho As ShinhoType = ShinhoList(0)
            Dim index As Integer

            index = 행사가로부터인덱스찾기(shinho.A12_콜행사가)
            selectedJongmokIndex(0) = index

            index = 행사가로부터인덱스찾기(shinho.A22_풋행사가)
            selectedJongmokIndex(1) = index

        End If

    End Sub

    Public Function 행사가로부터인덱스찾기(ByVal hangsaga As String) As Integer

        For i As Integer = 0 To optionList.Count - 1
            Dim it As ListTemplate = optionList(i)
            If it.HangSaGa = hangsaga Or Left(it.HangSaGa, 3) = hangsaga Then
                Return i
            End If
        Next

        Return -1

    End Function

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
            Form2.txt_Log.Text = tDate & " " & Data(0).ctime(currentIndex) & " " & str1 & " : " & str2 & vbCrLf & Form2.txt_Log.Text
        Else
            Form2.txt_Log.Text = tDate & " " & str1 & " : " & str2 & vbCrLf & Form2.txt_Log.Text
        End If
        Form2.txt_Log.Refresh()

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

    'DB로부터 읽은 Data로부터 OptionList를 만들어낸다
    '이 때 Data 안에는 2개 밖에 없을거기 때문에 Option List도 2개가 된다 0번 - 콜, 1번 풋
    Public Sub MakeOptinList()

        optionList.Clear()

        For i As Integer = 0 To TotalCount - 1

            If currentIndex > 0 Then

                Dim it As ListTemplate = New ListTemplate
                it.Initialize()

                Dim max As Single = Single.MinValue
                Dim min As Single = Single.MaxValue

                For j As Integer = 0 To currentIndex - 1
                    If Data(i).price(j, 1) > max Then max = Data(i).price(j, 1)
                    If Data(i).price(j, 2) < min Then min = Data(i).price(j, 2)
                Next

                it.HangSaGa = Data(i).HangSaGa '행사가
                it.price(i, 0) = Data(i).price(0, 0)
                it.price(i, 1) = max
                it.price(i, 2) = min
                it.price(i, 3) = Data(i).price(currentIndex, 3)

                optionList.Add(it)

            End If

            selectedJongmokIndex(i) = i
        Next

    End Sub

End Module
