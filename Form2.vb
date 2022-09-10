Option Explicit On
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form2

    Private Sub btn_f2_폼닫기_Click(sender As Object, e As EventArgs) Handles btn_f2_폼닫기.Click
        Me.Hide()
    End Sub

    Private Sub btn_F2_SelectDB_Click(sender As Object, e As EventArgs) Handles btn_F2_SelectDB.Click

        Add_Log("일반", "전체 Data 취합 Click")

        Dim dateCount As Integer = GetRawData_1min(txt_F2_DB_Date_Limit.Text, txt_F2_TableName.Text) '이걸하면 딕셔너리에 데이터를 넣고 날짜수를 리턴해줌
        Dim 순매수dateCount As Integer = GetRawData_순매수(txt_F2_DB_Date_Limit.Text, "soonMeSuTable") '이걸하면 딕셔너리에 데이터를 넣고 날짜수를 리턴해줌 - 순매수리스트

        If dateCount <> 순매수dateCount Then
            Add_Log("에러", "1분 데이터 카운트와 순매수 DateCount가 다름" & dateCount.ToString() & " : " & 순매수dateCount.ToString())
            Return
        End If

        Add_Log("일반", "DB 전체 일 수는 " + dateCount.ToString() + " 일")

        순매수데이터날짜수 = dateCount

        If 순매수데이터날짜수 > 0 Then

            HSc_F2_날짜조절.Maximum = 순매수데이터날짜수 - 1
            HSc_F2_날짜조절.LargeChange = 1
            HSc_F2_날짜조절.Refresh()

            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다
            F2_TargetDateIndex = 0 'DB_날짜 인덱스임 (전역변수)

            F2_날짜변경처리함수()

        Else
            MsgBox("DB에 데이터가 없습니다")
        End If

    End Sub

    Private Sub F2_Clac_DisplayAllGrid()

        SetScrolData_F2()
        CalcColorData()        '최대최소 계산
        CalcPIPData()          '대표선 계산
        DrawGraph()

    End Sub

    Private Sub SetScrolData_F2() '타임 스크롤의 최대최소값을 지정한다
        HSc_F2_시간조절.Maximum = timeIndex_순매수 - 1
        HSc_F2_시간조절.Minimum = 0
        HSc_F2_시간조절.Refresh()
        Dim str As String = String.Format("{0}건 중 {1}번째", timeIndex_순매수, currentIndex_순매수)
        Lbl_F2_현재시간Index.Text = str
    End Sub

    Private Sub DrawGraph()
        F2_Chart_순매수.Visible = False
        InitGraph()
        For i As Integer = 0 To 0
            DrawWinFormGraph(i)
        Next

        F2_Chart_순매수.Visible = True
    End Sub

    Private Sub CalcColorData()        '최대최소,제2저가 계산

        Dim min = Single.MaxValue
        Dim max = Single.MinValue

        For i As Integer = 0 To 순매수리스트카운트 - 1
            min = Math.Min(min, 순매수리스트(i).코스피지수)
            max = Math.Max(max, 순매수리스트(i).코스피지수)
        Next

        KOSPI_MAX = max
        KOSPI_MIN = min
        KOSPI_CUR = 순매수리스트(순매수리스트카운트 - 1).코스피지수

    End Sub

    Private Sub InitGraph()

        Dim str, ChartAreaStr As String

        F2_Chart_순매수.Series.Clear()
        F2_Chart_순매수.ChartAreas.Clear()
        F2_Chart_순매수.Legends.Clear()
        F2_Chart_순매수.Annotations.Clear()

        For i As Integer = 0 To 0 '0번은 메인으로쓰고 나머지 3개 정도를 왼쪽에 그려서 쓸 계획임 일단 하나만 구현함

            ChartAreaStr = "ChartArea_" + i.ToString()
            F2_Chart_순매수.ChartAreas.Add(ChartAreaStr)

            str = "oneMinute_" + i.ToString()
            F2_Chart_순매수.Series.Add(str)
            F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
            F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            F2_Chart_순매수.Series(str).Color = Color.Black
            F2_Chart_순매수.Series(str).YAxisType = AxisType.Secondary

            str = "For_" + i.ToString()
            F2_Chart_순매수.Series.Add(str)
            F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
            F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            F2_Chart_순매수.Series(str).Color = Color.Blue
            F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary

            str = "For_Kig_" + i.ToString()
            F2_Chart_순매수.Series.Add(str)
            F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
            F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            F2_Chart_순매수.Series(str).Color = Color.Magenta
            F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary

            str = "PIP_" + i.ToString()
            F2_Chart_순매수.Series.Add(str)
            F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
            F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            F2_Chart_순매수.Series(str).Color = Color.DarkRed
            F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary
            F2_Chart_순매수.Series(str).BorderDashStyle = ChartDashStyle.DashDotDot
            F2_Chart_순매수.Series(str).BorderWidth = 3

            ''Lebel 설정 - 이건 소수점 2째자리까지만 표기하도록 하는 기능인거 같음 - 필요 없을 듯
            'txt_ebest_id.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"

            '축 선 속성 설정
            F2_Chart_순매수.ChartAreas(i).AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            F2_Chart_순매수.ChartAreas(i).AxisX.MajorGrid.LineColor = Color.Gray
            F2_Chart_순매수.ChartAreas(i).AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            F2_Chart_순매수.ChartAreas(i).AxisY.MajorGrid.LineColor = Color.Gray

            F2_Chart_순매수.ChartAreas(i).AxisY2.LabelStyle.Format = "{0:0.00}"
        Next

        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY.IsStartedFromZero = False
    End Sub
    Private Sub DrawWinFormGraph(ByVal chartNumber As Integer)

        If currentIndex_순매수 >= 0 Then  '기본축은 순매수축으로 한다

            For i As Integer = 0 To F2_Chart_순매수.Series.Count - 1
                F2_Chart_순매수.Series(i).Points.Clear()
            Next

            Dim For_Series As String = "For_" + chartNumber.ToString()
            Dim For_Kig_Series As String = "For_Kig_" + chartNumber.ToString()
            Dim oneMinute_Series As String = "oneMinute_" + chartNumber.ToString()
            Dim PIP_Series As String = "PIP_" + chartNumber.ToString()
            Dim retIndex = 0

            For i As Integer = 0 To currentIndex_순매수

                F2_Chart_순매수.Series(For_Series).Points.AddXY(i, 순매수리스트(i).외국인순매수) '외국인 순매수를 입력한다
                F2_Chart_순매수.Series(For_Series).Points(retIndex).AxisLabel = Format("{0}", 순매수리스트(retIndex).sTime)

                F2_Chart_순매수.Series(For_Kig_Series).Points.AddXY(i, 순매수리스트(i).외국인_연기금_순매수) '외국인+연기금 순매수를 입력한다
                F2_Chart_순매수.Series(oneMinute_Series).Points.AddXY(i, 순매수리스트(i).코스피지수) '오른쪽 이중축에 적용

                Dim str As String = String.Format("시간:{0}{1}외국인:{2}{3}외+연:{4}{5}코스피:{6}", 순매수리스트(i).sTime, vbCrLf, 순매수리스트(i).외국인순매수, vbCrLf, 순매수리스트(i).외국인_연기금_순매수, vbCrLf, 순매수리스트(i).코스피지수)
                F2_Chart_순매수.Series(For_Series).Points(i).ToolTip = str
                F2_Chart_순매수.Series(For_Kig_Series).Points(i).ToolTip = str
                F2_Chart_순매수.Series(oneMinute_Series).Points(i).ToolTip = str

            Next

            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Maximum = KOSPI_MAX + 1
            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Minimum = KOSPI_MIN - 1

            For i As Integer = 0 To PIP_Point_Lists.Length - 1

                If PIP_Point_Lists(i).PointCount >= 2 Then   '2보다 작다는 말은 비어있다는 말이다
                    For j As Integer = 0 To PIP_Point_Lists(i).PoinIndexList.Count - 1
                        Dim point As Integer = PIP_Point_Lists(i).PoinIndexList(j)
                        F2_Chart_순매수.Series(PIP_Series).Points.AddXY(point, 순매수리스트(point).외국인_연기금_순매수)
                    Next

                End If
            Next

        End If

    End Sub

    Private Sub HSc_F2_시간조절_ValueChanged(sender As Object, e As EventArgs) Handles HSc_F2_시간조절.ValueChanged
        If currentIndex_순매수 >= 0 Then
            Dim value = HSc_F2_시간조절.Value
            If value <> currentIndex_순매수 Then
                currentIndex_순매수 = value
                currentIndex_1MIn = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
                F2_Clac_DisplayAllGrid()
            End If
        End If
    End Sub

    Private Sub HSc_F2_날짜조절_ValueChanged(sender As Object, e As EventArgs) Handles HSc_F2_날짜조절.ValueChanged

        Dim newValue As Integer = HSc_F2_날짜조절.Value

        If F2_TargetDateIndex <> newValue Then

            InitDataStructure_1Min()
            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다

            F2_TargetDateIndex = newValue 'DB_날짜 인덱스임 (전역변수)
            F2_날짜변경처리함수()
        End If

    End Sub

    Private Sub F2_날짜변경처리함수()

        InitDataStructure_1Min()

        TargetDate = DBDateList_1Min(F2_TargetDateIndex)
        sMonth = getsMonth(TargetDate).ToString() 'DB에서 읽은 날짜로부터 월물을 찾아낸다

        Dim TotalCount1 As Integer = GetDataFromDBHandler_1Min(TargetDate)
        순매수리스트카운트 = Get순매수데이터(TargetDate) '전역변수 순매수리스트에 하루치 Data를 입력한다

        If TotalCount1 > 0 And 순매수리스트카운트 > 0 Then
            'MakeOptinList()  이건 리스트를 만드는 기능인데 이건 매수할 때 필요가 없다. 

            '나중에 여기에 조건들 집어넣기 

            F2_Clac_DisplayAllGrid()

        End If

        Dim str As String = String.Format("{0}일 중 {1}번째({2})", 순매수데이터날짜수, F2_TargetDateIndex + 1, TargetDate)
        Lbl_F2_현재날짜Index.Text = str
        Lbl_F2_현재날짜Index.Refresh()

    End Sub

    'MouseMove 시 선과 숫자를 볼수있게 하는 기능 구현
    Private currentMouseLocation As Point = Point.Empty
    Private plotArea As RectangleF = RectangleF.Empty


    Private Sub F2_Chart_순매수_MouseMove(sender As Object, e As MouseEventArgs) Handles F2_Chart_순매수.MouseMove
        Me.currentMouseLocation = e.Location
        F2_Chart_순매수.Invalidate()
    End Sub

    Private Sub F2_Chart_순매수_PostPaint(sender As Object, e As ChartPaintEventArgs) Handles F2_Chart_순매수.PostPaint

        plotArea = F2_Chart_순매수.ChartAreas(0).Position.ToRectangleF

        Dim currentChart As Chart = sender
        Dim g As Graphics = e.ChartGraphics.Graphics

        Dim pen As Pen = New Pen(Color.Red)
        Dim font As Font = New Font("Arial", 11.0F)
        Dim brush As Brush = New SolidBrush(Color.Green)

        g.DrawLine(pen, plotArea.X, currentMouseLocation.Y, F2_Chart_순매수.Right, currentMouseLocation.Y)
        g.DrawLine(pen, currentMouseLocation.X, plotArea.Y, currentMouseLocation.X, F2_Chart_순매수.Bottom)

        'Y2축 Label
        Dim y2LabelHeight As Integer = 20

        Dim startY = currentMouseLocation.Y - y2LabelHeight - 2
        If startY < plotArea.Y Then
            startY = plotArea.Y
        End If

        Dim endY = currentMouseLocation.Y + y2LabelHeight - 2
        If endY > (F2_Chart_순매수.Bottom) Then
            startY = F2_Chart_순매수.Bottom - y2LabelHeight
        End If

        '외국인순매수
        Dim yValuesingle As Single = F2_Chart_순매수.ChartAreas(0).AxisY.PixelPositionToValue(currentMouseLocation.Y)
        If Not Single.IsNaN(yValuesingle) Then
            Dim yValue As Long = Math.Round(yValuesingle)
            g.DrawRectangle(pen, New Rectangle(F2_Chart_순매수.Left + 25, startY, 75, y2LabelHeight))
            g.DrawString(yValue.ToString() & "억", font, brush, New PointF(F2_Chart_순매수.Left + 28, startY))
        End If

        '종합주가지수
        Dim y2Value As Single = Math.Round(F2_Chart_순매수.ChartAreas(0).AxisY2.PixelPositionToValue(currentMouseLocation.Y), 2)
        If Not Single.IsNaN(y2Value) Then
            g.DrawRectangle(pen, New Rectangle(F2_Chart_순매수.Right - 100, startY, 75, y2LabelHeight))
            g.DrawString(y2Value.ToString(), font, brush, New PointF(F2_Chart_순매수.Right - 97, startY))
        End If

        'X축 시간
        Dim xValue As Single = F2_Chart_순매수.ChartAreas(0).AxisX.PixelPositionToValue(currentMouseLocation.X)
        If Not Single.IsNaN(xValue) And xValue >= 0 Then
            If 순매수리스트 IsNot Nothing Then
                Dim xValueInt As Integer = Math.Round(xValue)
                Dim xTime As String = 순매수리스트(xValueInt).sTime
                Dim Index_time As String = String.Format("{0}:{1}", xValueInt, xTime)
                g.DrawRectangle(pen, New Rectangle((currentMouseLocation.X - 120), F2_Chart_순매수.Bottom - 200, 117, 20))
                g.DrawString(Index_time, font, brush, New PointF(currentMouseLocation.X - 75, F2_Chart_순매수.Bottom - 198))
            End If
        End If


    End Sub
End Class