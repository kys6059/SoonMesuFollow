Option Explicit On

Module Module_For1Min
    Structure 순매수탬플릿
        Dim sDate As String
        Dim sTime As String
        Dim 외국인순매수 As Long
        Dim 개인순매수 As Long
        Dim 기관순매수 As Long
        Dim 연기금순매수 As Long
        Dim 외국인_연기금_순매수 As Long
        Dim 코스피지수 As Single
    End Structure

    Structure 일분데이터템플릿

        Dim HangSaGa As String
        Dim Code As String
        Dim ctime() As String  '시간 100개 콜풋 구분 없음
        Dim price(,) As Single '시간index, 시고저종
        Dim 거래량() As Long ' 100개

        Public Sub Initialize()
            ReDim ctime(400) '
            ReDim price(400, 3)  '콜풋, 시간, 시고저종
            ReDim 거래량(400) '1분단위는 약 396개임
        End Sub
    End Structure

    Structure PIP탬플릿
        Dim PointCount As Integer
        Dim 표준편차 As Double
        Dim PoinIndexList As List(Of Integer)

    End Structure

    '이하 외국인순매수 데이터 확보용 자료구조 추가 20220821
    Public 일분옵션데이터() As 일분데이터템플릿
    Public 순매수리스트() As 순매수탬플릿
    Public 순매수리스트카운트 As Integer '순매수리스트 카운트
    Public PIP_Point_Lists() As PIP탬플릿
    Public KOSPI_MIN, KOSPI_MAX, KOSPI_CUR As Single

    Public currentIndex_1MIn As Integer = -1
    Public timeIndex_1Min As Integer

    Public currentIndex_순매수 As Integer = -1
    Public timeIndex_순매수 As Integer = -1

    Public F2_TargetDateIndex As Integer = 0 '-------------------------------------------- 이건 순매수테이블과 공용으로 활용한다
    Public 순매수데이터날짜수 As Integer = 0

    Public PIP적합포인트수 As Integer = 2


    Public Sub InitDataStructure_1Min()

        '이하 외국인순매수 데이터 확보용 자료구조 추가 20220821
        ReDim 순매수리스트(999) '외국인 순매수금액이 커지면 코스피 지수 상승하는 상황을 고려하는 리스트 - 하루치임
        ReDim 일분옵션데이터(1)
        For i As Integer = 0 To 1
            일분옵션데이터(i).Initialize()
        Next

        ReDim PIP_Point_Lists(8) 'Point가 2개부터 최대 10개까지 8개만 계산한다 - 2개는 직선1개만 있다는 계산임

        KOSPI_MIN = 0
        KOSPI_MAX = 0
        KOSPI_CUR = 0

        TargetDate = 0
        currentIndex_1MIn = -1
        timeIndex_1Min = -1

        currentIndex_순매수 = -1
        timeIndex_순매수 = -1

    End Sub

    Public Function FindIndexFormTime_1Min(ByVal strTime As String) As Integer

        Dim si, bun As Integer
        Dim Interval_1 As Integer = 1

        If Len(strTime) = 4 Then
            si = Val(Mid(strTime, 1, 2))
            bun = Val(Mid(strTime, 3, 2))
        ElseIf Len(strTime) = 3 Then
            si = Val(Mid(strTime, 1, 1))
            bun = Val(Mid(strTime, 2, 2))
        Else
            MsgBox("시간이 안맞음. 현재 시간이 " & strTime & "으로 나옴")
            Return -1
        End If
        Return (((si - 9) * (60 / Interval_1)) + (bun / Interval_1)) - 1
    End Function

    Public Function 순매수리스트의인덱스찾기(iTime As Integer) As Integer

        '930, 901 이렇게 들어온다
        Dim ret As Integer = -1
        If iTime = 901 Then
            ret = 0
        Else
            For i As Integer = 0 To 순매수리스트카운트 - 1

                Dim 초 As Integer = Val(순매수리스트(i).sTime) Mod 100
                If 초 = 0 Then   '30초짜리는 버린다

                    Dim 시분 As Integer = Val(순매수리스트(i).sTime) / 100
                    If 시분 = iTime Then
                        ret = i
                        Exit For
                    End If

                End If
            Next
        End If

        Return ret

    End Function

    Public Function 순매수시간으로1MIN인덱스찾기(ByVal 순매수시간 As Integer) As Integer
        Dim ret As Integer = -1

        Dim 시분 As Integer = 순매수시간 / 100

        For i As Integer = 0 To timeIndex_1Min - 1
            If 시분 = Val(일분옵션데이터(0).ctime(i)) Then
                ret = i
                Exit For
            End If
        Next

        Return ret
    End Function

    Public Sub CalcPIPData()          '대표선 계산

        Dim minPoint = 2
        Dim 최대포인트수대비비율 As Single = Val(Form2.txt_최대포인트수대비비율.Text)
        Dim maxPoint As Integer = Math.Min(Math.Ceiling(currentIndex_순매수 / 최대포인트수대비비율), 10)

        If maxPoint >= 2 Then ReDim PIP_Point_Lists(maxPoint - minPoint)

        F2_Clear_Log()

        For i As Integer = 0 To maxPoint - minPoint
            If currentIndex_순매수 >= 4 Then

                Dim pointCount = i + minPoint
                Dim pipIndexList As List(Of Integer) = PIP_PD(currentIndex_순매수, pointCount)

                PIP_Point_Lists(i).PointCount = pointCount
                PIP_Point_Lists(i).PoinIndexList = pipIndexList
                PIP_Point_Lists(i).표준편차 = Calc_PIP거리계산(pipIndexList, currentIndex_순매수, pointCount)

                Dim str As String = String.Format("pipIndexList({0}), 평균거리는 = {1},   ", pointCount, Math.Round(PIP_Point_Lists(i).표준편차, 2))
                For j As Integer = 0 To pipIndexList.Count - 1
                    str += pipIndexList(j).ToString() & ", "
                Next
                Console.WriteLine(str)
                F2_Add_Log(str)
            End If
        Next

        '평균거리가 줄어들다가 늘어나는 점이 있으면 그 점을 화면에 표시한다. 단 평균거리는 0보다 크고 1보다 작아야 한다

        PIP적합포인트수 = maxPoint
        Dim 선행_포인트_마진 As Single = Val(Form2.txt_선행_포인트_마진.Text)
        For i As Integer = 1 To maxPoint - minPoint
            If PIP_Point_Lists(i - 1).표준편차 < currentIndex_순매수 / 10 And PIP_Point_Lists(i - 1).표준편차 > 0 Then
                If PIP_Point_Lists(i).표준편차 > PIP_Point_Lists(i - 1).표준편차 * 선행_포인트_마진 Then
                    PIP적합포인트수 = PIP_Point_Lists(i - 1).PointCount
                    Exit For
                End If
            End If
        Next
        Form2.txt_TargetPointCount.Text = PIP적합포인트수.ToString()

    End Sub

    Private Function Calc_PIP거리계산(ByVal pipIndexList As List(Of Integer), ByVal LastIndex As Integer, ByVal PointCount As Integer) As Double

        LastIndex = Math.Min(LastIndex, 760) '759번째 인덱스가 1520분이다 항상 이때까지만 계산한다

        Dim RightPipPoint = 1
        Dim leftPipIndex = pipIndexList(RightPipPoint - 1)
        Dim RightPipIndex = pipIndexList(RightPipPoint)

        Dim maxDistance As Double = Double.MinValue
        Dim maxDistanceIndex As Integer = 0
        Dim totalDistance As Double = 0.0
        Dim cnt As Integer = 0

        For i As Integer = 4 To LastIndex  '순매수리스트에 처음3개는 0으로 들어오기 때문에 4번째부터 계산한다

            If RightPipIndex = i And i < LastIndex Then

                RightPipPoint += 1
                leftPipIndex = RightPipIndex
                RightPipIndex = pipIndexList(RightPipPoint)

            Else '거리측정

                Dim distance As Double = PerpendichalrDistance(leftPipIndex, 순매수리스트(leftPipIndex).외국인_연기금_순매수, RightPipIndex, 순매수리스트(RightPipIndex).외국인_연기금_순매수, i, 순매수리스트(i).외국인_연기금_순매수)
                If distance > maxDistance Then
                    maxDistance = distance
                    maxDistanceIndex = i
                End If

                cnt += 1
                totalDistance += Math.Abs(distance)

            End If
        Next

        Dim 표준거리 = totalDistance / cnt

        Return 표준거리

    End Function

    Public Function PIP_PD(ByVal LastIndex As Integer, ByVal n As Integer) As List(Of Integer)

        'rawData는 외국인순매수+연기금순매수값으로 한다
        '9시 2분부터 값이 정상적으로 들어오기 때문에 0,1,2번 값은 버리고 3번인덱스부터 계산한다

        LastIndex = Math.Min(LastIndex, 760) '759번째 인덱스가 1520분이다 항상 이때까지만 계산한다

        Dim pipData(n) As Double

        Dim pipIndexList As List(Of Integer) = New List(Of Integer)

        pipIndexList.Add(3) 'PIP 1
        pipIndexList.Add(LastIndex) 'PIP 2

        For pipCount As Integer = 2 To n - 1

            Dim RightPipPoint = 1
            Dim leftPipIndex = pipIndexList(RightPipPoint - 1)
            Dim RightPipIndex = pipIndexList(RightPipPoint)

            Dim maxDistance As Double = Double.MinValue
            Dim maxDistanceIndex As Integer = 0

            For i As Integer = 4 To LastIndex  '순매수리스트에 처음3개는 0으로 들어오기 때문에 4번째부터 계산한다

                If RightPipIndex = i And i < (LastIndex) Then

                    RightPipPoint += 1
                    leftPipIndex = RightPipIndex
                    RightPipIndex = pipIndexList(RightPipPoint)

                Else '거리측정

                    Dim distance As Double = PerpendichalrDistance(leftPipIndex, 순매수리스트(leftPipIndex).외국인_연기금_순매수, RightPipIndex, 순매수리스트(RightPipIndex).외국인_연기금_순매수, i, 순매수리스트(i).외국인_연기금_순매수)
                    If distance > maxDistance Then
                        maxDistance = distance
                        maxDistanceIndex = i
                    End If
                End If
            Next

            For i As Integer = 0 To pipIndexList.Count - 1

                If pipIndexList(i) > maxDistanceIndex Then
                    pipIndexList.Insert(i, maxDistanceIndex)
                    Exit For
                End If

            Next
        Next

        Return pipIndexList

    End Function

    Private Function EuclideanDistance(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal x3 As Double, ByVal y3 As Double) As Double

        Return Math.Sqrt(Math.Pow(x2 - x3, 2) + Math.Pow(y2 - y3, 2)) + Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2))

    End Function

    Private Function PerpendichalrDistance(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal x3 As Double, ByVal y3 As Double) As Double

        Dim s As Double = (y2 - y1) / (x2 - x1)

        Return Math.Abs(s * x3 - y3 + y1 - s * x1) / Math.Sqrt(s * s + 1)


    End Function

    Public Sub F2_Clear_Log()
        Form2.txt_F2_Log.Clear()
    End Sub

    Public Sub F2_Add_Log(ByVal str As String)

        Form2.txt_F2_Log.Text = Form2.txt_F2_Log.Text + vbCrLf + str

    End Sub



End Module
