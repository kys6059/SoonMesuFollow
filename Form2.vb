Option Explicit On
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form2

    Private Sub btn_f2_폼닫기_Click(sender As Object, e As EventArgs) Handles btn_f2_폼닫기.Click
        Me.Hide()
    End Sub

    Private Sub btn_F2_SelectDB_Click(sender As Object, e As EventArgs) Handles btn_F2_SelectDB.Click

        If List잔고 IsNot Nothing Then
            If List잔고.Count > 0 Then
                Add_Log("일반", "양매도 잔고가 있을 때는 매수DB 오픈 금지")
                Return
            End If
        End If

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
        SoonMesuCalcAlrotithmAll() '--------------------------- 신호 만들기, 해제 검사하기 등
        If chk_F2_화면끄기.Checked = False Then
            DrawGrid()             '표
            Draw_Shinho_Grid()
            DrawGraph()
            txt_F2_최종방향.Text = 이전순매수방향
        End If

    End Sub

    Private Sub SetScrolData_F2() '타임 스크롤의 최대최소값을 지정한다
        HSc_F2_시간조절.Maximum = timeIndex_순매수 - 1
        HSc_F2_시간조절.Minimum = 0
        HSc_F2_시간조절.Refresh()
        Dim str As String = String.Format("{0}건 중 {1}번째({2})", timeIndex_순매수, currentIndex_순매수, 순매수리스트(currentIndex_순매수).sTime)
        Lbl_F2_현재시간Index.Text = str
    End Sub

    Private Sub DrawGrid()


        grid_3.Columns.Clear()
        grid_3.Rows.Clear()
        grid_3.ColumnCount = 6
        grid_3.RowCount = PIP_Point_Lists.Length


        If PIP_Point_Lists.Length > 0 Then

            grid_3.RowHeadersVisible = False
            grid_3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            For i As Integer = 0 To grid_3.ColumnCount - 1
                grid_3.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                grid_3.Columns(i).Width = 70
            Next

            grid_3.Columns(0).HeaderText = "번호"
            grid_3.Columns(1).HeaderText = "포인트수"
            grid_3.Columns(2).HeaderText = "평균거리"
            grid_3.Columns(3).HeaderText = "기울기"
            grid_3.Columns(4).HeaderText = "신호"
            grid_3.Columns(5).HeaderText = "포인트 리스트"
            grid_3.Columns(0).Width = 50
            grid_3.Columns(5).Width = 250

            '데이터 입력하기

            For i = 0 To PIP_Point_Lists.Length - 1
                grid_3.Rows(i).Cells(0).Value = i
                grid_3.Rows(i).Cells(1).Value = PIP_Point_Lists(i).PointCount
                grid_3.Rows(i).Cells(2).Value = Math.Round(PIP_Point_Lists(i).표준편차, 2)
                grid_3.Rows(i).Cells(3).Value = Math.Round(PIP_Point_Lists(i).마지막선기울기, 2)
                grid_3.Rows(i).Cells(4).Value = PIP_Point_Lists(i).마지막신호

                Dim str As String = ""
                For j = 0 To PIP_Point_Lists(i).PointCount - 1
                    str = str & PIP_Point_Lists(i).PoinIndexList(j) & ", "

                Next
                grid_3.Rows(i).Cells(5).Value = str
                grid_3.Rows(i).Cells(5).Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                For k = 0 To grid_3.ColumnCount - 1
                    If PIP적합포인트인덱스 = i Then
                        grid_3.Rows(i).Cells(k).Style.BackColor = Color.Yellow
                        grid_3.Rows(i).Cells(k).Style.ForeColor = Color.Red
                    End If
                Next

            Next

        End If

        grid_3.Refresh()

    End Sub

    Private Sub Draw_Shinho_Grid()


        grid_shinho.Columns.Clear()
        grid_shinho.Rows.Clear()
        grid_shinho.ColumnCount = 24
        grid_shinho.RowCount = SoonMesuShinhoList.Count

        grid_shinho.RowHeadersVisible = False
        grid_shinho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For i As Integer = 0 To grid_shinho.ColumnCount - 1
            grid_shinho.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid_shinho.Columns(i).Width = 72
        Next
        grid_shinho.Columns(0).HeaderText = "신호차수"
        grid_shinho.Columns(1).HeaderText = "발생Idx"
        grid_shinho.Columns(2).HeaderText = "발생시간"
        grid_shinho.Columns(3).HeaderText = "신호ID"
        grid_shinho.Columns(4).HeaderText = "발생순매수"
        grid_shinho.Columns(5).HeaderText = "해제순매수"
        grid_shinho.Columns(6).HeaderText = "발생지수"
        grid_shinho.Columns(7).HeaderText = "해제지수"
        grid_shinho.Columns(8).HeaderText = "콜풋"
        grid_shinho.Columns(9).HeaderText = "행사가"
        grid_shinho.Columns(10).HeaderText = "발생가격"
        grid_shinho.Columns(11).HeaderText = "주문번호"
        grid_shinho.Columns(12).HeaderText = "종목코드"
        grid_shinho.Columns(13).HeaderText = "체결상태"
        grid_shinho.Columns(14).HeaderText = "현재가격"
        grid_shinho.Columns(15).HeaderText = "현재상태"
        grid_shinho.Columns(16).HeaderText = "이익율"
        grid_shinho.Columns(17).HeaderText = "중간매도"
        grid_shinho.Columns(18).HeaderText = "매도시간"
        grid_shinho.Columns(19).HeaderText = "매도Idx"
        grid_shinho.Columns(20).HeaderText = "매도사유"
        grid_shinho.Columns(21).HeaderText = "환산이익"
        grid_shinho.Columns(22).HeaderText = "지수차이"
        grid_shinho.Columns(23).HeaderText = "기타"

        'grid_shinho.Columns(0).Width = 50


        '데이터 입력하기

        For i As Integer = 0 To SoonMesuShinhoList.Count - 1
            Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
            grid_shinho.Rows(i).Cells(0).Value = s.A00_신호차수
            grid_shinho.Rows(i).Cells(1).Value = s.A01_발생Index
            grid_shinho.Rows(i).Cells(2).Value = s.A02_발생시간
            grid_shinho.Rows(i).Cells(3).Value = s.A03_신호ID
            grid_shinho.Rows(i).Cells(4).Value = s.A04_신호발생순매수
            grid_shinho.Rows(i).Cells(5).Value = s.A05_신호해제순매수
            grid_shinho.Rows(i).Cells(6).Value = s.A06_신호발생종합주가지수
            grid_shinho.Rows(i).Cells(7).Value = s.A07_신호해제종합주가지수
            grid_shinho.Rows(i).Cells(8).Value = s.A08_콜풋
            grid_shinho.Rows(i).Cells(9).Value = s.A09_행사가
            grid_shinho.Rows(i).Cells(10).Value = s.A10_신호발생가격
            grid_shinho.Rows(i).Cells(11).Value = s.A11_주문번호
            grid_shinho.Rows(i).Cells(12).Value = s.A12_종목코드
            grid_shinho.Rows(i).Cells(13).Value = s.A13_체결상태
            grid_shinho.Rows(i).Cells(14).Value = s.A14_현재가격
            grid_shinho.Rows(i).Cells(15).Value = s.A15_현재상태
            grid_shinho.Rows(i).Cells(16).Value = s.A16_이익률
            grid_shinho.Rows(i).Cells(17).Value = s.A17_중간매도Flag
            grid_shinho.Rows(i).Cells(18).Value = s.A18_매도시간
            grid_shinho.Rows(i).Cells(19).Value = s.A19_매도Index
            grid_shinho.Rows(i).Cells(20).Value = s.A20_매도사유
            grid_shinho.Rows(i).Cells(21).Value = s.A21_환산이익율
            grid_shinho.Rows(i).Cells(22).Value = s.A55_메모
        Next

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

            'str = "For_" + i.ToString()
            'F2_Chart_순매수.Series.Add(str)
            'F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
            'F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            'F2_Chart_순매수.Series(str).Color = Color.Blue
            'F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary

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

            'Dim For_Series As String = "For_" + chartNumber.ToString()
            Dim For_Kig_Series As String = "For_Kig_" + chartNumber.ToString()
            Dim oneMinute_Series As String = "oneMinute_" + chartNumber.ToString()
            Dim PIP_Series As String = "PIP_" + chartNumber.ToString()
            Dim retIndex As Integer = 0

            For i As Integer = 0 To currentIndex_순매수

                Dim target순매수 As Long = Get순매수(i)

                F2_Chart_순매수.Series(oneMinute_Series).Points.AddXY(i, 순매수리스트(i).코스피지수) '오른쪽 이중축에 적용 - 
                F2_Chart_순매수.Series(oneMinute_Series).Points(i).AxisLabel = Format("{0}", 순매수리스트(i).sTime)

                If i >= 4 Then
                    'F2_Chart_순매수.Series(For_Series).Points.AddXY(i, 순매수리스트(i).외국인순매수) '외국인 순매수는 2분이 지나야 정상적인 데이터를 가지기 때문에 3번인덱스 이상에서만 표시한다
                    retIndex = F2_Chart_순매수.Series(For_Kig_Series).Points.AddXY(i, target순매수) '외국인+연기금 순매수를 입력한다

                    Dim str As String = String.Format("시간:{0}{1}외국인:{2}{3}외+연:{4}{5}코스피:{6}", 순매수리스트(i).sTime, vbCrLf, 순매수리스트(i).외국인순매수, vbCrLf, target순매수, vbCrLf, 순매수리스트(i).코스피지수)
                    'F2_Chart_순매수.Series(For_Series).Points(retIndex).ToolTip = str
                    F2_Chart_순매수.Series(For_Kig_Series).Points(retIndex).ToolTip = str
                    F2_Chart_순매수.Series(oneMinute_Series).Points(i).ToolTip = str
                End If

            Next

            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Maximum = KOSPI_MAX + 1
            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Minimum = KOSPI_MIN - 1

            'PIP 시리즈를 표시한다
            If currentIndex_순매수 >= 4 Then
                For i As Integer = 0 To PIP_Point_Lists.Length - 1
                    Dim targetPointCount As Integer = Val(txt_TargetPointCount.Text)

                    If PIP_Point_Lists(i).PointCount = targetPointCount Then '원하는 pointCount와 같은 점수만을 화면에 표시한다
                        If PIP_Point_Lists(i).PoinIndexList IsNot Nothing Then

                            For j As Integer = 0 To PIP_Point_Lists(i).PoinIndexList.Count - 1
                                Dim point As Integer = PIP_Point_Lists(i).PoinIndexList(j)
                                Dim target순매수 As Long = Get순매수(point)
                                F2_Chart_순매수.Series(PIP_Series).Points.AddXY(point, target순매수)
                            Next

                        End If

                    End If
                Next
            End If

            '신호를 그린다
            If SoonMesuShinhoList IsNot Nothing Then
                For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                    Dim Str As String = "Shinho_" + i.ToString()
                    F2_Chart_순매수.Series.Add(Str)
                    F2_Chart_순매수.Series(Str).ChartArea = "ChartArea_0"
                    F2_Chart_순매수.Series(Str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                    F2_Chart_순매수.Series(Str).Color = Color.DarkRed
                    F2_Chart_순매수.Series(Str).YAxisType = AxisType.Primary

                    F2_Chart_순매수.Series(Str).BorderWidth = 2

                    Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)

                    If s.A08_콜풋 = 0 Then
                        F2_Chart_순매수.Series(Str).Color = Color.Blue
                    Else
                        F2_Chart_순매수.Series(Str).Color = Color.Red
                    End If

                    If currentIndex_순매수 >= s.A01_발생Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A01_발생Index, s.A04_신호발생순매수)  '시작점

                    If s.A15_현재상태 = 1 Then '끝점
                        F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Solid
                        F2_Chart_순매수.Series(Str).Points.AddXY(currentIndex_순매수, Get순매수(currentIndex_순매수))
                    Else
                        F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Dot
                        If currentIndex_순매수 >= s.A19_매도Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A19_매도Index, s.A05_신호해제순매수)
                    End If
                Next
            End If

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
                g.DrawRectangle(pen, New Rectangle((currentMouseLocation.X - 100), F2_Chart_순매수.Bottom - 200, 97, 20))
                g.DrawString(Index_time, font, brush, New PointF(currentMouseLocation.X - 94, F2_Chart_순매수.Bottom - 198))
            End If
        End If


    End Sub

    Private Sub btn_점의수줄이기_Click(sender As Object, e As EventArgs) Handles btn_점의수줄이기.Click

        If Val(txt_TargetPointCount.Text) >= 3 Then
            Dim n As Integer = Val(txt_TargetPointCount.Text) - 1
            txt_TargetPointCount.Text = n.ToString()
            DrawGraph()
        End If
    End Sub

    Private Sub btn_점의수늘리기_Click(sender As Object, e As EventArgs) Handles btn_점의수늘리기.Click
        If Val(txt_TargetPointCount.Text) <= 9 Then
            Dim n As Integer = Val(txt_TargetPointCount.Text) + 1
            txt_TargetPointCount.Text = n.ToString()
            DrawGraph()
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmb_F2_순매수기준.Items.Clear()
        cmb_F2_순매수기준.Items.Add("0.FOR_SYS")
        cmb_F2_순매수기준.Items.Add("1.FOR_KIG")
        cmb_F2_순매수기준.Items.Add("2.FOR_Only")
        cmb_F2_순매수기준.SelectedIndex = 0

        Dim strToday As String = Format(Today, "yyMMdd")
        txt_F2_실험조건.Text = "B" + strToday
    End Sub

    Private Sub btn_당일반복_Click(sender As Object, e As EventArgs) Handles btn_당일반복.Click
        chk_F2_매수실행.Checked = False
        당일반복중_flag = True
        chk_F2_화면끄기.Checked = True
        For i As Integer = 0 To 순매수리스트카운트 - 1
            currentIndex_순매수 = i

            F2_Clac_DisplayAllGrid()
        Next
        chk_F2_화면끄기.Checked = False
        F2_Clac_DisplayAllGrid()

        Add_Log("일반", "Form2_당일 자동반복 완료")

        당일반복중_flag = False

    End Sub

    Private Sub btn_F2_동일조건반복_Click(sender As Object, e As EventArgs) Handles btn_F2_동일조건반복.Click
        chk_F2_매수실행.Checked = False
        당일반복중_flag = True

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        자동반복계산로직(0)
        Add_Log("Form2 자동 반복 계산로직 완료", "")
        당일반복중_flag = False
    End Sub

    Private Sub 자동반복계산로직(ByVal cnt As Integer)

        isRealFlag = False
        당일반복중_flag = True
        For i As Integer = 0 To 순매수데이터날짜수 - 1

            HSc_F2_날짜조절.Value = i     ' 이 안에서도 Clac_DisplayAllGrid  호출하지만 그건 그날짜 data의 첫번째만 호출하는 것임
            HSc_F2_날짜조절.Refresh()

            chk_F2_화면끄기.Checked = True

            '당일 내부에서 변경
            For j As Integer = 0 To 순매수리스트카운트 - 1

                currentIndex_순매수 = j
                If currentIndex_순매수 = 순매수리스트카운트 - 1 Then chk_F2_화면끄기.Checked = False
                F2_Clac_DisplayAllGrid()
            Next

            '매일매일 신호리스트를 시뮬레이션전체신호리스트에 복사한다
            For j = 0 To SoonMesuShinhoList.Count - 1
                SoonMesuSimulationTotalShinhoList.Add(SoonMesuShinhoList(j))
            Next

            Threading.Thread.Sleep(50)

        Next

        '여기서 DB에 입력하면 완료됨. 만약 입력하면 반드시 clear할 것
        If SoonMesuSimulationTotalShinhoList.Count > 0 Then

            'Add_Log(cnt.ToString() + "차 자동계산완료", " : " + " Total 신호건수 = " + SimulationTotalShinhoList.Count.ToString())
            InsertSoonMeSuShinhoResult()

            SoonMesuSimulationTotalShinhoList.Clear()

        End If

        당일반복중_flag = False

    End Sub

    Private Sub btn_F2_전체조건반복_Click(sender As Object, e As EventArgs) Handles btn_F2_전체조건반복.Click
        chk_F2_매수실행.Checked = False
        Form1.chk_양매도실행.Checked = False
        Form1.chk_중간청산.Checked = False

        Dim 선행포인트수마진() As String = {"1.00", "0.85"} 'a
        Dim 순매수판정기준() As Integer = {0} 'b
        Dim 최대포인트수() As String = {"10", "08"} 'c
        Dim 상승하락기울기기준() As String = {"3.0", "4.0"} 'd
        Dim 손절차() As String = {"08", "09"} 'e
        Dim 익절차() As String = {"15", "16", "17"} 'f

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To 선행포인트수마진.Length - 1
            For c As Integer = 0 To 최대포인트수.Length - 1
                For d As Integer = 0 To 상승하락기울기기준.Length - 1
                    For ee As Integer = 0 To 손절차.Length - 1
                        For f As Integer = 0 To 익절차.Length - 1

                            txt_선행_포인트_마진.Text = 선행포인트수마진(a)
                            txt_F2_최대포인트수.Text = 최대포인트수(c)
                            txt_F2_상승하락기울기기준.Text = 상승하락기울기기준(d)
                            txt_F2_손절매차.Text = 손절차(ee)
                            txt_F2_익절차.Text = 익절차(f)

                            txt_선행_포인트_마진.Refresh()
                            txt_F2_최대포인트수.Refresh()
                            txt_F2_상승하락기울기기준.Refresh()
                            txt_F2_손절매차.Refresh()
                            txt_F2_익절차.Refresh()

                            Dim cntstr As String
                            If cnt < 10 Then
                                cntstr = "00" & cnt.ToString()
                            ElseIf cnt >= 10 And cnt < 100 Then
                                cntstr = "0" & cnt.ToString()
                            Else
                                cntstr = cnt.ToString()
                            End If

                            SoonMesuSimulation_조건 = String.Format("CNT_{0}_A_{1}_C_{2}_D_{3}_E_{4}_F_{5}", cntstr, 선행포인트수마진(a), 최대포인트수(c), 상승하락기울기기준(d), 손절차(ee), 익절차(f))

                            'SoonMesuSimulation_조건 = String.Format("CNT_{0}_E_{1}_F_{2}", cntstr, 손절차(ee), 익절차(f))
                            Console.WriteLine(SoonMesuSimulation_조건)
                            Add_Log("F2_시뮬레이션 진행 : ", SoonMesuSimulation_조건)
                            자동반복계산로직(cnt)
                            cnt += 1
                        Next
                    Next


                Next
            Next
        Next

        SoonMesuSimulation_조건 = ""
    End Sub
End Class