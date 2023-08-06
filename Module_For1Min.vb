Option Explicit On
Imports System.Drawing.Text
Imports System.Net.Security

Module Module_For1Min



    Structure 순매수탬플릿
        Dim sDate As String
        Dim sTime As String
        Dim 외국인순매수 As Long
        Dim 개인순매수 As Long
        Dim 기관순매수 As Long
        Dim 연기금순매수 As Long
        Dim 외국인_연기금_순매수 As Long
        Dim 외국인_기관_순매수 As Long
        Dim 코스피지수 As Single
    End Structure


    Structure 일분데이터템플릿

        Dim HangSaGa As String
        Dim Code As String
        Dim ctime() As String  '시간 100개 콜풋 구분 없음
        Dim price(,) As Single '시간index, 시고저종
        Dim 거래량() As Long ' 100개
        Dim 이동평균선() As Single '콜풋

        Public Sub Initialize()
            ReDim ctime(480) '
            ReDim price(480, 3)  '콜풋, 시간, 시고저종
            ReDim 거래량(480) '1분단위는 약 396개임
            ReDim 이동평균선(480)
        End Sub
    End Structure

    Structure PIP탬플릿
        Dim PointCount As Integer
        Dim 표준편차 As Double
        Dim 마지막신호_점수 As Integer '매우상승 2, 상승 1, 0, 하락 -1, 매우하락 -2
        Dim 마지막선기울기 As Double
        Dim 마지막선거리합 As Double
        Dim PoinIndexList As List(Of Integer)
        Dim 마지막점과그앞점간INDEXCOUNT As Integer
        Dim dataSource As Integer '0-외국인+기관, 1-외국인, 2-기관

    End Structure

    '이하 외국인순매수 데이터 확보용 자료구조 추가 20220821
    Public 일분옵션데이터() As 일분데이터템플릿
    Public 순매수리스트() As 순매수탬플릿
    Public 순매수리스트카운트 As Integer '순매수리스트 카운트
    Public PIP_Point_Lists() As PIP탬플릿


    Public currentIndex_1MIn As Integer = -1
    Public timeIndex_1Min As Integer

    Public currentIndex_순매수 As Integer = -1
    Public timeIndex_순매수 As Integer = -1

    Public F2_TargetDateIndex As Integer = 0 '-------------------------------------------- 이건 순매수테이블과 공용으로 활용한다
    Public 순매수데이터날짜수 As Integer = 0

    Public PIP적합포인트인덱스 As Integer = 0



    Public 신호최소유지시간index As Integer = 6 '신호가 뜬 후 최소 얼마간 유지할 건지를 판단하는 변수로 만약 4라면 2분 초과 필요하다

    Public 시작전_개별기울기 As Integer = 15  '시작전 외국인/기관 각각의 기울기 기준 - 2개가 같은 방향일때를 확인하기 위함

    Public Sub InitDataStructure_1Min()

        '이하 외국인순매수 데이터 확보용 자료구조 추가 20220821
        ReDim 순매수리스트(999) '외국인 순매수금액이 커지면 코스피 지수 상승하는 상황을 고려하는 리스트 - 하루치임
        ReDim 일분옵션데이터(1)
        For i As Integer = 0 To 1
            일분옵션데이터(i).Initialize()
        Next

        'ReDim PIP_Point_Lists(8) 'Point가 2개부터 최대 10개까지 8개만 계산한다 - 2개는 직선1개만 있다는 계산임
        ReDim PIP_Point_Lists(2) '0 - 외국인 + 기관, 1 - 외국인, 2 - 기관

        TargetDate = 0
        currentIndex_1MIn = -1
        timeIndex_1Min = -1

        currentIndex_순매수 = -1
        timeIndex_순매수 = -1
        이전순매수방향 = "중립"

        If SoonMesuShinhoList Is Nothing Then
            SoonMesuShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuShinhoList.Clear()
        End If

        If optionList Is Nothing Then
            optionList = New List(Of ListTemplate)
        Else
            optionList.Clear()
        End If

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
        Dim 최대포인트수 As Single = Val(Form2.txt_F2_최대포인트수.Text)
        Dim pointCount As Integer = Math.Min(Math.Ceiling(currentIndex_순매수 / 6), 최대포인트수)

        If pointCount < 2 Then pointCount = 2

        If currentIndex_순매수 <= 4 Then
            이전순매수방향 = "중립"
            If SoonMesuShinhoList Is Nothing Then
                SoonMesuShinhoList = New List(Of 순매수신호_탬플릿)
            Else
                SoonMesuShinhoList.Clear()
            End If
        End If

        For i As Integer = 0 To 2 '0 - 외국인+기관, 1 - 외국인, 2 - 기관
            If currentIndex_순매수 >= 4 Then

                Dim pipIndexList As List(Of Integer) = PIP_PD(currentIndex_순매수, pointCount, i)

                PIP_Point_Lists(i).dataSource = i
                PIP_Point_Lists(i).PointCount = pointCount
                PIP_Point_Lists(i).PoinIndexList = pipIndexList
                PIP_Point_Lists(i).표준편차 = Calc_PIP거리계산(pipIndexList, currentIndex_순매수, pointCount, i)
                PIP_Point_Lists(i).마지막선기울기 = Calc_PIP마지막선기울기계산(pipIndexList, currentIndex_순매수, pointCount, i)
                PIP_Point_Lists(i).마지막신호_점수 = 마지막신호판단(PIP_Point_Lists(i).마지막선기울기)
                PIP_Point_Lists(i).마지막점과그앞점간INDEXCOUNT = pipIndexList(pointCount - 1) - pipIndexList(pointCount - 2)     '마지막점과 그 앞의 점까지의 index - 이게 만약 2라면 신호가 안뜨도록 만들도록 사용할 계획임 20230726
                'PIP_Point_Lists(i).마지막선거리합 = Calc_PIP마지막선거리합계산(pipIndexList, currentIndex_순매수, pointCount,i)

                If Form2.chk_F2_화면끄기.Checked = False Then
                    Dim str As String = String.Format("pipIndexList({0}), 평균거리는={1},기울기={2},신호점수={3}  ", pointCount, Math.Round(PIP_Point_Lists(i).표준편차, 2), Math.Round(PIP_Point_Lists(i).마지막선기울기, 2), PIP_Point_Lists(i).마지막신호_점수)
                    For j As Integer = 0 To pipIndexList.Count - 1
                        str += pipIndexList(j).ToString() & ", "
                    Next
                    'Console.WriteLine(str)
                End If
            End If
        Next

        CalcTotalShinhoLevel()  '외국인과 기관의 합계 신호를 계산하여  전역변수에 넣는다  ---------------- 신규합계_마지막신호

    End Sub

    Public Function Calc_직선기울기계산(ByVal DataSourceNumber As Integer) As Single

        Dim ret = 0.0

        If currentIndex_순매수 >= 4 Then
            Dim pointCount As Integer = 2
            Dim pipIndexList As List(Of Integer) = PIP_PD(currentIndex_순매수, pointCount, DataSourceNumber)
            ret = Calc_PIP마지막선기울기계산(pipIndexList, currentIndex_순매수, pointCount, DataSourceNumber)
        End If

        Return ret

    End Function

    '외국인과 기관의 합계 신호를 계산하여  전역변수에 넣는다  ---------------- 신규합계_마지막신호
    Private Sub CalcTotalShinhoLevel()

        Dim ret As Integer = 0
        Dim 신호발생점수기준 As Integer = Val(Form2.txt_F2_신호발생점수기준.Text)
        For i As Integer = 1 To 2
            ret = ret + PIP_Point_Lists(i).마지막신호_점수
        Next

        If ret >= 신호발생점수기준 Then
            신규합계_마지막신호 = "상승"
        ElseIf ret <= (신호발생점수기준 * -1) Then
            신규합계_마지막신호 = "하락"
        Else
            신규합계_마지막신호 = "중립"
        End If

    End Sub

    '아직 사용하지 않음
    Private Function Calc_PIP마지막선거리합계산(ByVal pipIndexList As List(Of Integer), ByVal LastIndex As Integer, ByVal PointCount As Integer, ByVal dataSource As Integer) As Double

        If PointCount < 3 Then Return 0

        Dim leftPipIndex = pipIndexList(PointCount - 3)
        Dim RightPipIndex = pipIndexList(PointCount - 2)
        Dim LastPoint = pipIndexList(PointCount - 1)

        Dim distance As Double = PerpendichalrDistance(leftPipIndex, Get순매수(leftPipIndex, dataSource), RightPipIndex, Get순매수(RightPipIndex, dataSource), LastPoint, Get순매수(LastPoint, dataSource)) '이전 선을 기준으로 현재 마지막점의 거리를 계산한다
        Return distance

    End Function

    Public Function Get순매수(ByVal index As Integer, ByVal dataSource As Integer) As Long
        Dim ret As Long = 0
        If dataSource = 0 Then
            ret = 순매수리스트(index).외국인_기관_순매수
        ElseIf dataSource = 1 Then
            ret = 순매수리스트(index).외국인순매수
        ElseIf dataSource = 2 Then
            ret = 순매수리스트(index).기관순매수
        End If
        Return ret
    End Function

    Private Function 마지막신호판단(ByVal 기울기 As Double) As Integer

        Dim ret As Integer = 0
        Dim 기준_1차 As Single = Val(Form2.txt_F2_1차상승판정기울기기준.Text)
        Dim 기준_2차 As Single = Val(Form2.txt_F2_2차상승판정기준기울기.Text)

        Dim 절대치_기울기 As Single = Math.Abs(기울기)
        If 절대치_기울기 > 기준_2차 Then

            If 기울기 > 0 Then
                ret = 2
            Else
                ret = -2
            End If

        ElseIf 절대치_기울기 > 기준_1차 Then
            If 기울기 > 0 Then
                ret = 1
            Else
                ret = -1
            End If
        End If

        Return ret
    End Function

    Private Function Calc_PIP마지막선기울기계산(ByVal pipIndexList As List(Of Integer), ByVal LastIndex As Integer, ByVal PointCount As Integer, ByVal dataSource As Integer) As Double

        Dim x1 As Double = pipIndexList(PointCount - 2)
        Dim x2 As Double = pipIndexList(PointCount - 1)
        Dim y1 As Double = Get순매수(x1, dataSource)
        Dim y2 As Double = Get순매수(x2, dataSource)

        Dim 기울기 As Double = (y2 - y1) / (x2 - x1)

        Return 기울기

    End Function

    Private Function Calc_PIP거리계산(ByVal pipIndexList As List(Of Integer), ByVal LastIndex As Integer, ByVal PointCount As Integer, ByVal dataSource As Integer) As Double

        LastIndex = Math.Min(LastIndex, 760) '759번째 인덱스가 1520분이다 항상 이때까지만 계산한다

        Dim RightPipPoint = 1
        Dim leftPipIndex = pipIndexList(RightPipPoint - 1)
        Dim RightPipIndex = pipIndexList(RightPipPoint)

        Dim maxDistance As Double = Double.MinValue
        Dim maxDistanceIndex As Integer = 0
        Dim totalDistance As Double = 0.0
        Dim cnt As Integer = 0

        Dim startIndex As Integer = 4
        For i As Integer = 0 To 4    '처음에는 0에 0900이 들어오고 이때 순매수 빈값이 들어왔는데 어느날에선가부터 2분부터 들어오는 걸로 바뀌었거나 내가 필터링 해서 0902부터 인덱스 0에 쌓이고 있는 현상 확인
            If Get순매수(i, dataSource) <> 0 Then startIndex = i
            Exit For
        Next
        Dim PIP_CALC_MAX_INDEX As Integer = Val(Form2.txt_F2_PIP_CALC_MAX_INDEX.Text)
        If PIP_CALC_MAX_INDEX > 0 Then startIndex = Math.Max(LastIndex - PIP_CALC_MAX_INDEX, 4)

        For i As Integer = startIndex To LastIndex  '순매수리스트에 처음3개는 0으로 들어오기 때문에 4번째부터 계산한다

            If RightPipIndex = i And i < LastIndex Then

                RightPipPoint += 1
                leftPipIndex = RightPipIndex
                RightPipIndex = pipIndexList(RightPipPoint)

            Else '거리측정

                Dim distance As Double = PerpendichalrDistance(leftPipIndex, Get순매수(leftPipIndex, dataSource), RightPipIndex, Get순매수(RightPipIndex, dataSource), i, Get순매수(i, dataSource))

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

    Public Function PIP_PD(ByVal LastIndex As Integer, ByVal n As Integer, ByVal dataSource As Integer) As List(Of Integer) '여기서 dataSource은 0 - 외국인+기관, 1 - 외국인, 2 - 기관

        LastIndex = Math.Min(LastIndex, 760) '759번째 인덱스가 1520분이다 항상 이때까지만 계산한다

        Dim pipData(n) As Double
        Dim pipIndexList As List(Of Integer) = New List(Of Integer)


        Dim startIndex As Integer = 4
        For i As Integer = 0 To 4
            If Get순매수(i, dataSource) <> 0 Then startIndex = i
            Exit For
        Next
        Dim PIP_CALC_MAX_INDEX As Integer = Val(Form2.txt_F2_PIP_CALC_MAX_INDEX.Text)
        If PIP_CALC_MAX_INDEX > 0 Then startIndex = Math.Max(LastIndex - PIP_CALC_MAX_INDEX, 4)

        pipIndexList.Add(startIndex)
        pipIndexList.Add(LastIndex) 'PIP 2

        For pipCount As Integer = 2 To n - 1

            Dim RightPipPoint = 1
            Dim leftPipIndex = pipIndexList(RightPipPoint - 1)
            Dim RightPipIndex = pipIndexList(RightPipPoint)

            Dim maxDistance As Double = Double.MinValue
            Dim maxDistanceIndex As Integer = 0

            For i As Integer = startIndex To LastIndex  '순매수리스트에 처음3개는 0으로 들어오기 때문에 4번째부터 계산한다

                If RightPipIndex = i And i < (LastIndex) Then

                    RightPipPoint += 1
                    leftPipIndex = RightPipIndex
                    RightPipIndex = pipIndexList(RightPipPoint)

                Else '거리측정

                    Dim distance As Double = PerpendichalrDistance(leftPipIndex, Get순매수(leftPipIndex, dataSource), RightPipIndex, Get순매수(RightPipIndex, dataSource), i, Get순매수(i, dataSource))

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

    Public Sub SetSelectedIndex_For_순매수()  '순매수로직을 위해 기존 양매도로직을 수정함

        If currentIndex_순매수 > 0 Then
            If Val(순매수리스트(currentIndex_순매수).sTime) > 151000 Then  '14시 45분에는 자동 변경을 끈다  --- F 알고리즘 때문에 15시 10분으로 바꾼다 
                Form2.chk_ChangeTargetIndex.Checked = False
            End If
        End If

        If selectedJongmokIndex(0) < 0 Or Form2.chk_ChangeTargetIndex.Checked = True Then '아직 한번도 선택하지 않았거나 Checked가 True일 때만 자동으로 변경함



            Dim str As String = "SetSelectedIndex 진입 - selectedJongmokIndex(0) = " & selectedJongmokIndex(0).ToString() & " Form2.chk_ChangeTargetIndex.Checked = " & Form2.chk_ChangeTargetIndex.Checked.ToString()

            Console.WriteLine(str)

            Dim Targetprice As Single = Val(Form2.txt_F2_매수_기준가.Text)
            Dim targetCallIndex As Single = 1100.0
            Dim targetPutIndex As Single = 1100.0

            Dim 최종목표인덱스(1) As Integer
            Dim i, j As Integer
            Dim str10 As String = ""

            For i = 0 To 1 '방향

                'If is동일신호가현재살아있나("ALL", i) Then Return   '해당 방향의 어떤 신호라도 살아있으면 인덱스를 바꾸지 않고 리턴한다  -- 밑에서 순매수리스트에 있는 행사가에서 인덱스 가져오는 기능이 있음 - 이건 콜풋 종목의 크기가 변하기 때문에 인덱스를 반드시 바꾸어야 함

                Dim targetIndex As Integer
                Dim minGap As Single = 1100.0
                For j = 0 To optionList.Count - 1
                    Dim it As ListTemplate = optionList(j)
                    Dim gap As Single = Math.Abs(Targetprice - it.price(i, 3))
                    If gap < minGap And it.price(i, 3) >= 0.2 Then '0.2보다 높은 종목만 선택함
                        targetIndex = j
                        minGap = gap
                    End If
                Next

                콜선택된행사가(i) = 인덱스로부터행사가찾기(targetIndex)
            Next

        End If

        '여기다가 행사가로부터 인덱스 뽑는 로직 추가함
        Dim index1 As Integer
        index1 = 행사가로부터인덱스찾기(콜선택된행사가(0))
        If index1 >= 0 Then selectedJongmokIndex(0) = index1

        Dim index2 As Integer
        index2 = 행사가로부터인덱스찾기(콜선택된행사가(1))
        If index2 >= 0 Then selectedJongmokIndex(1) = index2

        If SoonMesuShinhoList IsNot Nothing Then
            For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A15_현재상태 = 1 Then selectedJongmokIndex(s.A08_콜풋) = 행사가로부터인덱스찾기(s.A09_행사가)
            Next
        End If

    End Sub

    'DB로부터 읽은 Data로부터 OptionList를 만들어낸다
    '이 때 Data 안에는 2개 밖에 없을거기 때문에 Option List도 2개가 된다 0번 - 콜, 1번 풋
    Public Sub MakeOptinList_For_1Minute()

        optionList.Clear()

        For i As Integer = 0 To TotalCount - 1

            If currentIndex_1MIn > 0 Then

                Dim it As ListTemplate = New ListTemplate
                it.Initialize()

                Dim max As Single = Single.MinValue
                Dim min As Single = Single.MaxValue

                For j As Integer = 0 To currentIndex_1MIn - 1
                    If 일분옵션데이터(i).price(j, 1) > max Then max = 일분옵션데이터(i).price(j, 1)
                    If 일분옵션데이터(i).price(j, 2) < min Then min = 일분옵션데이터(i).price(j, 2)
                Next

                it.HangSaGa = 일분옵션데이터(i).HangSaGa '행사가
                it.price(i, 0) = 일분옵션데이터(i).price(0, 0)
                it.price(i, 1) = max
                it.price(i, 2) = min
                it.price(i, 3) = 일분옵션데이터(i).price(currentIndex, 3)

                optionList.Add(it)

            End If

            selectedJongmokIndex(i) = i
        Next

    End Sub

    Public Function 이동평균선값계산(ByVal 이동평균선기준일자 As Integer, ByVal callput As Integer, ByVal index As Integer) As Single

        Dim sumValue As Single = 0
        Dim cnt As Integer = 0

        For i As Integer = 0 To 이동평균선기준일자 - 1  '자기를 포함한 이동평균선기준일자까지 더한다

            If 일분옵션데이터(callput).price(index - i, 3) > 0 Then
                sumValue = sumValue + 일분옵션데이터(callput).price(index - i, 3)
                cnt += 1
            End If

        Next

        Dim 이동평균값 As Single = sumValue / cnt
        Return 이동평균값

    End Function

End Module
