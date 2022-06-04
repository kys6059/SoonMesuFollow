Option Explicit On

Structure DataSet

    Dim HangSaGa As String '콜풋
    Dim Code() As String '콜풋
    Dim ctime() As String  '시간 100개 콜풋 구분 없음

    Dim price(,,) As Single '콜풋, 시간, 시고저종

    Dim Big(,) As Single '콜풋 시고저종
    Dim Small(,) As Single '콜풋 시고저종
    Dim 거래량(,) As Long '콜풋 100개
    Dim secondMin() As Single '콜풋

    Dim 증거금() As Long

    Public Sub Initialize()

        ReDim Code(1)
        ReDim 증거금(1)
        ReDim ctime(100) '
        ReDim price(1, 100, 3)  '콜풋, 시간, 시고저종
        ReDim Big(1, 3) '시고저종을 기록해야해서 4개
        ReDim Small(1, 3) '시고저종을 기록해야해서 4개
        ReDim 거래량(1, 100) '시간대별 거래량을 기록해야 해서 100개가 필요함
        ReDim secondMin(1) '시고저종 관계없이 저가에서만 제2저가를 구함

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


'해야할 일 정리 ------------------- 20220211

'그래프 그리기 ------------------------------
'설정 - 입력 항목들 만들기  ------------------------------- 완료
'실시간 타이머 넣기 --------------------------------------- 완료 (화면 그리는데 시간이 많이 걸려서 좀 느림)
'DB에 저장, 불러오기
'신호 넣기
'특정 날짜 Data 가져오기 기능 추가 ------------------------ 완료 (월물이 바뀌는 날은 안됨, CYBOS에서 data가 지난달거는 조회 안됨)
'일정 시간 후 자동 저장 기능 ---- Timer에 구현함

'해야할 일 정리 ------------------- 20220306

'그래프 그리기 ------------------------------ Y축 넣기가 이상함, 합계 넣기, Annotation 넣기
'설정 - 입력 항목들 만들기  ------------------------------- 완료
'실시간 타이머 넣기 --------------------------------------- 완료 (화면 그리는데 시간이 많이 걸려서 좀 느림)
'DB에 저장, 불러오기 -------------------------------------- 개별 가져오기는 완료, 단 한꺼번에 가져와서 밀고 당기는 거는 너무 느려서 개선 필요
'신호 넣기
'특정 날짜 Data 가져오기 기능 추가 ------------------------ 완료 (월물이 바뀌는 날은 안됨, CYBOS에서 data가 지난달거는 조회 안됨)
'일정 시간 후 자동 저장 기능 ---- Timer에 구현 필요


Module Module_common

    '전역변수 선언
    Public Data() As DataSet  '-------------------- 전체가 들어있는 자료형
    Public SumDataSet As SumDatSetType
    Public TargetDate As Integer
    Public sMonth As String
    Public Interval As Integer
    Public TotalJongMokCount As Integer
    Public CurrentTime As Integer
    Public isRealFlag As Boolean
    Public TotalCount As Integer
    Public UpperLimit, LowerLimt As Single
    Public timeIndex As Integer '시간이 내려감에따라 증가하는 인덱스  - 항상 초기화 필요(DB에서 가져올 때, 대신에서 가져올때)
    Public currentIndex As Integer '시뮬레이션할 때 현재커서 위치를 나타냄
    Public selectedJongmokIndex(1) As Integer
    Public JongmokTargetPrice As Single  '기준이 되는 targetprice - default 2.0
    Public timerCount As Integer
    Public timerMaxInterval As Integer


    Public Sub InitDataStructure()

        ReDim Data(100) '최대 100개지만 실제로는 28개까지만 지원한다 - 콜풋 각각
        For i As Integer = 0 To 100
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
        timerMaxInterval = 16 '16초보다 크면 실행함
        currentIndex = -1
        timeIndex = -1

        JongmokTargetPrice = Val(Form1.txt_JongmokTargetPrice.Text)
        selectedJongmokIndex(0) = -1
        selectedJongmokIndex(1) = -1

        If ShinhoList Is Nothing Then
            ShinhoList = New List(Of ShinhoType)
        Else
            ShinhoList.Clear()
        End If



    End Sub


    Public Function CalcTargetJonhmokIndex(ByVal callput As Integer) As Integer

        Dim temptarget, i, tempIndex As Integer
        Dim mingap, gap As Single

        mingap = 1000.0
        tempIndex = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다


        '장이 끝나는 시간 기준이 아니라 장 시작시간 기준으로 변경함
        For i = 0 To TotalCount

            If Data(i).price(callput, 0, 0) > 0 And Data(i).price(callput, 0, 0) < 3.0 Then
                gap = Math.Abs(Data(i).price(callput, 0, 3) - JongmokTargetPrice)
                If gap < mingap Then
                    mingap = gap
                    temptarget = i
                End If
            End If

        Next

        Return temptarget

    End Function

    Public Sub CalcSumPrice()

        Dim selectedCallIndex As Integer = selectedJongmokIndex(0)
        Dim selectedputIndex As Integer = selectedJongmokIndex(1)

        SumDataSet.Initialze()
        For j As Integer = 0 To currentIndex

            '합계 계산
            If Val(Data(selectedCallIndex).ctime(j)) = Val(Data(selectedPutIndex).ctime(j)) Then
                If Data(selectedCallIndex).price(0, j, 0) > 0 And Data(selectedPutIndex).price(1, j, 0) > 0 Then
                    SumDataSet.siSum(j) = Data(selectedCallIndex).price(0, j, 0) + Data(selectedputIndex).price(1, j, 0)
                    SumDataSet.jongSum(j) = Data(selectedCallIndex).price(0, j, 3) + Data(selectedputIndex).price(1, j, 3)
                End If
            End If

        Next

        For j = 0 To currentIndex - 1

            If SumDataSet.siMax < SumDataSet.siSum(j) Then SumDataSet.siMax = SumDataSet.siSum(j)
            If SumDataSet.siMin > SumDataSet.siSum(j) Then SumDataSet.siMin = SumDataSet.siSum(j)

            If SumDataSet.jongMax < SumDataSet.jongSum(j) Then SumDataSet.jongMax = SumDataSet.jongSum(j)
            If SumDataSet.jongmin > SumDataSet.jongSum(j) Then SumDataSet.jongmin = SumDataSet.jongSum(j)

        Next

    End Sub


    '최고, 최저, 제2저가 계산
    Public Sub CalcColorData()
        Dim i, j, k, callput As Integer
        Dim min, max, secondmin As Single

        For callput = 0 To 1 '콜 풋 루프

            For i = 0 To TotalCount - 1 '종목루프

                secondmin = 1000

                For j = 0 To 3 ' 시고저종 루프

                    min = 1000
                    max = 0

                    For k = 0 To currentIndex - 1 '시간 루프

                        '최고값 계산
                        If Data(i).price(callput, k, j) > max And Data(i).price(callput, k, j) > 0 Then
                            max = Data(i).price(callput, k, j)
                        End If

                        '최저값 계산
                        If Data(i).price(callput, k, j) < min And Data(i).price(callput, k, j) > 0 Then
                            min = Data(i).price(callput, k, j)
                        End If

                        '제2저가 계산
                        If j = 2 Then
                            If Data(i).price(callput, k, j) < secondmin And Data(i).price(callput, k, j) > min Then
                                secondmin = Data(i).price(callput, k, j)
                            End If
                        End If

                    Next

                    '최고,최저값 입력
                    Data(i).Big(callput, j) = max
                    Data(i).Small(callput, j) = min
                    Data(i).secondMin(callput) = secondmin

                Next

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
    Public Function ItsColor(ByVal jongMok As Integer, ByVal iFlag As Integer, ByVal iIndex As Integer, ByVal sigojuejong As Integer) As Integer

        Dim color As Integer

        color = 4 '아래 조건에 아무데도 안걸리면 흰색

        If (Data(jongMok).price(iFlag, iIndex, sigojuejong) > 0) Then

            '제2저가 - 하늘색
            If (Data(jongMok).price(iFlag, iIndex, sigojuejong) > (Data(jongMok).secondMin(iFlag) + 0.005)) Then
                color = 3
            End If

            '최고가
            If (Math.Abs(Data(jongMok).price(iFlag, iIndex, sigojuejong) - Data(jongMok).Big(iFlag, sigojuejong)) < 0.005) Then
                color = 0
            End If

            '최저가
            If (Math.Abs(Data(jongMok).price(iFlag, iIndex, sigojuejong) - Data(jongMok).Small(iFlag, sigojuejong)) < 0.005) Then
                color = 1
            End If

        End If


        Return color

    End Function

    '마지막에 currentIndex에서는 0 값이 들어와서 마지막 조건일 때는 그 앞에걸로 하는 걸로 바꾼다
    Public Function GetMaxIndex() As Integer

        Dim ret As Integer

        If currentIndex > 78 Then
            ret = 78
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

End Module
