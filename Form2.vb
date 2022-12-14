Option Explicit On
Imports System.Windows.Forms.DataVisualization.Charting
Imports Google.Api

Public Class Form2

    Private Sub btn_F2_SelectDB_Click(sender As Object, e As EventArgs) Handles btn_F2_SelectDB.Click

        If List잔고 IsNot Nothing Then
            If List잔고.Count > 0 Then
                Add_Log("일반", "양매도 잔고가 있을 때는 매수DB 오픈 금지")
                Return
            End If
        End If
        isRealFlag = False
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

    Private Sub 화면그리기()
        SetScrolData_F2()

        RedrawAll() 'grid1에 옵션리스트를 출력하고 선택된 것을 표시한다
        DrawGrid_PIP_계산그리드()
        Draw_Shinho_Grid()
        DrawGraph()
        txt_F2_최종방향.Text = 이전순매수방향

        lbl_F2_콜중간청산갯수.Text = 콜중간청산개수.ToString()
        lbl_F2_풋중간청산갯수.Text = 풋중간청산개수.ToString()

        lbl_F2_외국인순매수.Text = Format(순매수리스트(currentIndex_순매수).외국인순매수, "###,##0")
        lbl_F2_기관순매수.Text = Format(순매수리스트(currentIndex_순매수).기관순매수, "###,##0")
        lbl_F2_순매수합계.Text = Format(순매수리스트(currentIndex_순매수).외국인_기관_순매수, "###,##0")
    End Sub

    Private Sub F2_Clac_DisplayAllGrid()

        If currentIndex_순매수 >= 0 Then
            lbl_ReceiveCounter.Text = "수신횟수 = " & ReceiveCount.ToString()

            'CalcColorData()        '최대최소 계산
            CalcPIPData()          '대표선 계산
            SoonMesuCalcAlrotithmAll() '--------------------------- 신호 만들기, 해제 검사하기 등

            If chk_F2_화면끄기.Checked = False Then
                화면그리기()
                Update()
            End If

            If chk_F2_AutoSave.Checked = True And isRealFlag = True Then '자동 저장 기능 
                If currentIndex_순매수 >= 780 Then

                    chk_F2_AutoSave.Checked = False

                    Dim random As New Random
                    Timer_AutoSave111.Interval = random.Next(1000, 50000)
                    Timer_AutoSave111.Enabled = True

                End If
            End If
        End If

    End Sub

    Private Sub SetScrolData_F2() '타임 스크롤의 최대최소값을 지정한다
        HSc_F2_시간조절.Maximum = timeIndex_순매수 - 1
        HSc_F2_시간조절.Minimum = 0
        HSc_F2_시간조절.Refresh()
        Dim str As String = String.Format("{0}건 중 {1}번째({2})", timeIndex_순매수, currentIndex_순매수, 순매수리스트(currentIndex_순매수).sTime)
        Lbl_F2_현재시간Index.Text = str
    End Sub

    Private Sub DrawGrid_PIP_계산그리드()

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
        If SoonMesuShinhoList IsNot Nothing Then
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
                grid_shinho.Rows(i).Cells(21).Value = Format(s.A21_환산이익율, "##0.0%")
                grid_shinho.Rows(i).Cells(22).Value = s.A55_메모

                If s.A21_환산이익율 > 0 Then
                    grid_shinho.Rows(i).Cells(21).Style.BackColor = Color.Yellow
                    grid_shinho.Rows(i).Cells(21).Style.ForeColor = Color.Red
                ElseIf s.A21_환산이익율 <= 0 Then
                    grid_shinho.Rows(i).Cells(21).Style.BackColor = Color.GreenYellow
                    grid_shinho.Rows(i).Cells(21).Style.ForeColor = Color.Black
                End If

                For j As Integer = 0 To grid_shinho.ColumnCount - 1 '매수시간에 아닌 때 발생한 신호는 회색처리한다
                    If Val(s.A02_발생시간) > Val(txt_F2_매수마감시간.Text) Then grid_shinho.Rows(i).Cells(j).Style.ForeColor = Color.Gray
                Next
            Next
        End If
    End Sub

    Private Sub DrawGraph()

        F2_InitGraph()
        F2_DrawWinFormGraph(0)

        Init_Option_1min_Graph()
        Draw_Option_1min_Graph()

    End Sub

    Private Sub CalcColorData()

    End Sub

    Private Sub F2_InitGraph()

        F2_Chart_순매수.Visible = False
        Dim str, ChartAreaStr As String

        F2_Chart_순매수.Series.Clear()
        F2_Chart_순매수.ChartAreas.Clear()
        F2_Chart_순매수.Legends.Clear()
        F2_Chart_순매수.Annotations.Clear()

        ChartAreaStr = "ChartArea_0"
        F2_Chart_순매수.ChartAreas.Add(ChartAreaStr)

        str = "oneMinute_0"
        F2_Chart_순매수.Series.Add(str)
        F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
        F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
        F2_Chart_순매수.Series(str).Color = Color.Black
        F2_Chart_순매수.Series(str).YAxisType = AxisType.Secondary

        For i As Integer = 0 To 2 '0 - 외+기

            If (i = 0 And chk_F2_DATA_0.Checked = True) Or (i = 1 And chk_F2_DATA_1.Checked = True) Or (i = 2 And chk_F2_DATA_2.Checked = True) Then
                str = "For_Kig_" + i.ToString()
                F2_Chart_순매수.Series.Add(str)
                F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
                F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary
                If i = 0 Then
                    F2_Chart_순매수.Series(str).Color = Color.Magenta
                ElseIf i = 1 Then
                    F2_Chart_순매수.Series(str).Color = Color.YellowGreen
                Else
                    F2_Chart_순매수.Series(str).Color = Color.Violet
                End If

                str = "PIP_" + i.ToString()
                F2_Chart_순매수.Series.Add(str)
                F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
                F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary
                F2_Chart_순매수.Series(str).BorderDashStyle = ChartDashStyle.DashDotDot
                F2_Chart_순매수.Series(str).BorderWidth = 2

                If i = 0 Then
                    F2_Chart_순매수.Series(str).Color = Color.DarkRed
                ElseIf i = 1 Then
                    F2_Chart_순매수.Series(str).Color = Color.Red
                Else
                    F2_Chart_순매수.Series(str).Color = Color.OrangeRed
                End If


                ''Lebel 설정 - 이건 소수점 2째자리까지만 표기하도록 하는 기능인거 같음 - 필요 없을 듯
                'txt_ebest_id.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"
            End If

        Next

        '축 선 속성 설정
        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisX.MajorGrid.LineColor = Color.Gray
        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY.MajorGrid.LineColor = Color.Gray
        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.LabelStyle.Format = "{0:0.00}"
        F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY.IsStartedFromZero = False

    End Sub
    Private Sub F2_DrawWinFormGraph(ByVal chartNumber As Integer)

        If currentIndex_순매수 >= 0 Then  '기본축은 순매수축으로 한다

            For i As Integer = 0 To F2_Chart_순매수.Series.Count - 1
                F2_Chart_순매수.Series(i).Points.Clear()
            Next
            Dim oneMinute_Series As String = "oneMinute_0"
            Dim retIndex As Integer = 0

            Dim min As Single = Single.MaxValue
            Dim max As Single = Single.MinValue

            For i As Integer = 0 To currentIndex_순매수
                '코스피 지수 입력
                F2_Chart_순매수.Series(oneMinute_Series).Points.AddXY(i, 순매수리스트(i).코스피지수) '오른쪽 이중축에 적용 - 
                F2_Chart_순매수.Series(oneMinute_Series).Points(i).AxisLabel = Format("{0}", 순매수리스트(i).sTime)
                min = Math.Min(min, 순매수리스트(i).코스피지수)
                max = Math.Max(max, 순매수리스트(i).코스피지수)
            Next

            For j As Integer = 0 To 2

                Dim For_Kig_Series As String = "For_Kig_" + j.ToString()

                For i As Integer = 4 To currentIndex_순매수                     '각 매수 주체별 순매수 값 그리기 

                    If (j = 0 And chk_F2_DATA_0.Checked = True) Or (j = 1 And chk_F2_DATA_1.Checked = True) Or (j = 2 And chk_F2_DATA_2.Checked = True) Then
                        Dim target순매수 As Long = Get순매수(i, j)
                        retIndex = F2_Chart_순매수.Series(For_Kig_Series).Points.AddXY(i, target순매수) ' 순매수를 입력한다
                        Dim str As String = String.Format("시간:{0}{1}구분:{2}{3}순매수:{4}{5}코스피:{6}", 순매수리스트(i).sTime, vbCrLf, j, vbCrLf, target순매수, vbCrLf, 순매수리스트(i).코스피지수)
                        F2_Chart_순매수.Series(For_Kig_Series).Points(retIndex).ToolTip = str
                    End If

                Next
            Next

            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Maximum = max + 1
            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Minimum = min - 1

            'PIP 시리즈를 표시한다
            If currentIndex_순매수 >= 4 Then

                For i As Integer = 0 To 2
                    Dim PIP_Series As String = "PIP_" + i.ToString()
                    If (i = 0 And chk_F2_DATA_0.Checked = True) Or (i = 1 And chk_F2_DATA_1.Checked = True) Or (i = 2 And chk_F2_DATA_2.Checked = True) Then
                        If PIP_Point_Lists(i).PoinIndexList IsNot Nothing Then
                            For j As Integer = 0 To PIP_Point_Lists(i).PoinIndexList.Count - 1
                                Dim point As Integer = PIP_Point_Lists(i).PoinIndexList(j)
                                Dim target순매수 As Long = Get순매수(point, i)
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

                    F2_Chart_순매수.Series(Str).BorderWidth = 3

                    Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)

                    If s.A08_콜풋 = 0 Then
                        F2_Chart_순매수.Series(Str).Color = Color.Blue
                    Else
                        F2_Chart_순매수.Series(Str).Color = Color.Red
                    End If

                    If currentIndex_순매수 >= s.A01_발생Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A01_발생Index, s.A04_신호발생순매수)  '시작점

                    If s.A15_현재상태 = 1 Then '끝점
                        F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Solid
                        F2_Chart_순매수.Series(Str).Points.AddXY(currentIndex_순매수, Get순매수(currentIndex_순매수, 0))
                    Else
                        F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Dot
                        If currentIndex_순매수 >= s.A19_매도Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A19_매도Index, s.A05_신호해제순매수)
                    End If

                    '주가지수 그리기
                    Str = "kospi_" + i.ToString()

                    F2_Chart_순매수.Series.Add(Str)
                    F2_Chart_순매수.Series(Str).ChartArea = "ChartArea_0"
                    F2_Chart_순매수.Series(Str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                    F2_Chart_순매수.Series(Str).Color = Color.Green
                    F2_Chart_순매수.Series(Str).YAxisType = AxisType.Secondary
                    F2_Chart_순매수.Series(Str).BorderWidth = 1

                    If s.A08_콜풋 = 0 Then
                        F2_Chart_순매수.Series(Str).Color = Color.Green
                    Else
                        F2_Chart_순매수.Series(Str).Color = Color.Orange
                    End If

                    If currentIndex_순매수 >= s.A01_발생Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A01_발생Index, s.A06_신호발생종합주가지수)  '시작점


                    If s.A15_현재상태 = 1 Then '끝점
                        F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Solid
                        F2_Chart_순매수.Series(Str).Points.AddXY(currentIndex_순매수, 순매수리스트(currentIndex_순매수).코스피지수)
                    Else
                        F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Dot
                        If currentIndex_순매수 >= s.A19_매도Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A19_매도Index, s.A07_신호해제종합주가지수)
                    End If

                Next
            End If

        End If
        F2_Chart_순매수.Visible = True
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
            InitDataStructure()
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
            MakeOptinList_For_1Minute()

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
            'g.DrawRectangle(pen, New Rectangle(currentMouseLocation.X - 100, startY, 75, y2LabelHeight))
            g.DrawString(yValue.ToString() & "억", font, brush, New PointF(currentMouseLocation.X - 60, startY))
        End If

        '종합주가지수
        Dim y2Value As Single = Math.Round(F2_Chart_순매수.ChartAreas(0).AxisY2.PixelPositionToValue(currentMouseLocation.Y), 2)
        If Not Single.IsNaN(y2Value) Then
            'g.DrawRectangle(pen, New Rectangle(F2_Chart_순매수.Right - 100, startY, 75, y2LabelHeight))
            g.DrawString(y2Value.ToString(), font, brush, New PointF(currentMouseLocation.X, startY + 30))
        End If

        'X축 시간
        Dim xValue As Single = F2_Chart_순매수.ChartAreas(0).AxisX.PixelPositionToValue(currentMouseLocation.X)
        If Not Single.IsNaN(xValue) And xValue >= 0 And xValue < timeIndex_순매수 Then
            If 순매수리스트 IsNot Nothing Then
                Dim xValueInt As Integer = Math.Min(Math.Truncate(xValue), currentIndex_순매수)
                Dim xTime As String = 순매수리스트(xValueInt).sTime
                Dim Index_time As String = String.Format("{0}:{1}", xValueInt, xTime)
                'g.DrawRectangle(pen, New Rectangle((currentMouseLocation.X - 100), F2_Chart_순매수.Bottom - 200, 97, 20))
                'g.DrawString(Index_time, font, brush, New PointF(currentMouseLocation.X - 94, F2_Chart_순매수.Bottom - 198))
                g.DrawString(Index_time, font, brush, New PointF(currentMouseLocation.X, startY))


            End If
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

        ReceiveCount = 0


        Dim dt As Date = Now.AddDays(-30)  '여기 원래 -30을 넣어야 함
        Dim strdt As String = Format(dt, "yyMM01")
        txt_F2_DB_Date_Limit.Text = "WHERE cdate >= " + strdt


        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)
        sMonth = 월물
        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)

        월물_위클리옵션판단(남은날짜) 'txt_월물과 txt_weekly_정규 텍스트박스에 값을 입력한다
        손절매수준설정(남은날짜)
        currentIndex = -1

    End Sub

    Private Sub btn_당일반복_Click(sender As Object, e As EventArgs) Handles btn_당일반복.Click
        chk_실거래실행.Checked = False
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
        chk_실거래실행.Checked = False
        당일반복중_flag = True

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        자동반복계산로직(0, True)
        Add_Log("Form2 자동 반복 계산로직 완료", "")
        당일반복중_flag = False
    End Sub

    Private Sub 자동반복계산로직(ByVal cnt As Integer, ByVal 일일조건설정flag As Boolean)

        isRealFlag = False
        당일반복중_flag = True
        For i As Integer = 0 To 순매수데이터날짜수 - 1

            chk_F2_화면끄기.Checked = True
            HSc_F2_날짜조절.Value = i     ' 이 안에서도 Clac_DisplayAllGrid  호출하지만 그건 그날짜 data의 첫번째만 호출하는 것임
            HSc_F2_날짜조절.Refresh()

            Dim lDate As Long = Val(TargetDate)
            Dim 월물 As Long = getsMonth(lDate)
            sMonth = 월물

            Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)
            남은날짜 = 남은날짜 Mod 7

            'If 남은날짜 < 3 Then
            If 일일조건설정flag = True Then
                일일조건설정(TargetDate)    '전체조건일 때는 스킵해야 함
            End If

            '당일 내부에서 변경
            For j As Integer = 0 To 순매수리스트카운트 - 1

                currentIndex_순매수 = j
                If currentIndex_순매수 = 순매수리스트카운트 - 1 Then
                    chk_F2_화면끄기.Checked = False
                End If
                F2_Clac_DisplayAllGrid()

            Next

            '매일매일 신호리스트를 시뮬레이션전체신호리스트에 복사한다
            For j = 0 To SoonMesuShinhoList.Count - 1
                SoonMesuSimulationTotalShinhoList.Add(SoonMesuShinhoList(j))
            Next

            'End If
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

    Private Sub Init_Option_1min_Graph()

        Chart1.Visible = False
        Dim str, ChartAreaStr As String

        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()
        Chart1.Legends.Clear()
        Chart1.Annotations.Clear()

        For i As Integer = 0 To 1

            ChartAreaStr = "ChartArea_" + i.ToString()
            Chart1.ChartAreas.Add(ChartAreaStr)

            str = "CandleStick_" + i.ToString()
            Chart1.Series.Add(str)
            Chart1.Series(str).ChartArea = ChartAreaStr
            Chart1.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Candlestick
            Chart1.Series(str).CustomProperties = “PriceDownColor=Blue, PriceUpColor=Red”

            ''Lebel 설정
            Chart1.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"
            '축 선 속성 설정
            Chart1.ChartAreas(i).AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart1.ChartAreas(i).AxisX.MajorGrid.LineColor = Color.Gray
            Chart1.ChartAreas(i).AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart1.ChartAreas(i).AxisY.MajorGrid.LineColor = Color.Gray
        Next
    End Sub

    Private Sub Draw_Option_1min_Graph()
        Dim i, callput, retindex As Integer '

        If currentIndex_1MIn >= 0 Then

            For i = 0 To Chart1.Series.Count - 1
                Chart1.Series(i).Points.Clear()
            Next

            Dim maxValue As Single = Single.MinValue
            Dim minValue As Single = Single.MaxValue

            For callput = 0 To 1

                Dim CandlestrickSeries As String = "CandleStick_" + callput.ToString()

                For i = 0 To currentIndex_1MIn
                    ' main Series 입력
                    If 일분옵션데이터(callput).price(i, 1) > 0 Then
                        retindex = Chart1.Series(CandlestrickSeries).Points.AddXY(i, 일분옵션데이터(callput).price(i, 1)) '고가를 처음 넣는다
                        Chart1.Series(CandlestrickSeries).Points(retindex).YValues(1) = 일분옵션데이터(callput).price(i, 2) '저가
                        Chart1.Series(CandlestrickSeries).Points(retindex).YValues(2) = 일분옵션데이터(callput).price(i, 0) '시가
                        Chart1.Series(CandlestrickSeries).Points(retindex).YValues(3) = 일분옵션데이터(callput).price(i, 3) '종가

                        'X축 시간
                        Chart1.Series(CandlestrickSeries).Points(i).AxisLabel = Format("{0}", 일분옵션데이터(callput).ctime(i))

                        If 일분옵션데이터(callput).price(i, 0) < 일분옵션데이터(callput).price(i, 3) Then '시가보다 종가가 크면 
                            Chart1.Series(CandlestrickSeries).Points(retindex).Color = Color.Red
                            Chart1.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Red
                        ElseIf 일분옵션데이터(callput).price(i, 0) > 일분옵션데이터(callput).price(i, 3) Then
                            Chart1.Series(CandlestrickSeries).Points(retindex).Color = Color.Blue
                            Chart1.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Blue
                        End If

                        Dim str As String = "시간:" & 일분옵션데이터(0).ctime(i) & vbCrLf & "시가:" & 일분옵션데이터(callput).price(i, 0) & vbCrLf & "종가:" & 일분옵션데이터(callput).price(i, 3)
                        Chart1.Series(CandlestrickSeries).Points(retindex).ToolTip = str

                        If maxValue < 일분옵션데이터(callput).price(i, 1) Then maxValue = 일분옵션데이터(callput).price(i, 1) '계산해놓은 big, small로 보니 마지막 CurrentIndex의 값이 반영이 안되어 여기서 일일이 계산해서 처리하도록 변경 20220607
                        If minValue > 일분옵션데이터(callput).price(i, 2) Then minValue = 일분옵션데이터(callput).price(i, 2)
                    End If
                Next
            Next

            '콜 풋 차트의 크기를 똑같이 하기 위해서 최대,최소값을 맞춘다
            maxValue = maxValue + 0.1
            minValue = minValue - 0.1
            For i = 0 To 1
                Chart1.ChartAreas(i).AxisY.Minimum = minValue
                Chart1.ChartAreas(i).AxisY.Maximum = maxValue
                Chart1.ChartAreas(i).AxisY.Interval = 0.2
            Next
        End If

        '신호를 그린다
        If SoonMesuShinhoList IsNot Nothing Then
            For i = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                Dim Str As String = "Shinho_" + i.ToString()
                Chart1.Series.Add(Str)

                If s.A08_콜풋 = 0 Then
                    Chart1.Series(Str).ChartArea = "ChartArea_0"
                Else
                    Chart1.Series(Str).ChartArea = "ChartArea_1"
                End If

                Chart1.Series(Str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart1.Series(Str).Color = Color.Black
                Chart1.Series(Str).BorderWidth = 3

                '시작점,끝점 찾기
                Dim 신호시작점 As Integer = 순매수시간으로1MIN인덱스찾기(Val(s.A02_발생시간))
                Dim 신호끝점 As Integer

                If currentIndex_1MIn >= 신호시작점 Then Chart1.Series(Str).Points.AddXY(신호시작점, s.A10_신호발생가격)  '시작점

                If s.A15_현재상태 = 1 Then '끝점
                    신호끝점 = currentIndex_1MIn
                    Chart1.Series(Str).BorderDashStyle = ChartDashStyle.Solid
                    If currentIndex_1MIn >= 신호끝점 Then Chart1.Series(Str).Points.AddXY(신호끝점, 일분옵션데이터(s.A08_콜풋).price(currentIndex_1MIn, 3))
                Else
                    신호끝점 = 순매수시간으로1MIN인덱스찾기(Val(s.A18_매도시간))
                    Chart1.Series(Str).BorderDashStyle = ChartDashStyle.Dot
                    If currentIndex_1MIn >= 신호끝점 Then Chart1.Series(Str).Points.AddXY(신호끝점, s.A22_신호해제가격)
                End If
            Next
        End If

        Chart1.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub btn_이베스트로그인_Click(sender As Object, e As EventArgs) Handles btn_이베스트로그인.Click
        InitDataStructure()
        InitDataStructure_1Min()
        이베스트로그인함수()
        ReceiveCount = 0
    End Sub

    Private Sub 이베스트로그인함수()

        Dim nServerType As Integer
        Dim strServerAddress, password As String

        If txt_ebest_pwd.Text = "" Then
            MessageBox.Show("이베스트 비밀번호가 비어있습니다")
            Return
        End If

        If chk_모의투자연결.Checked = False Then
            nServerType = 0     ' 실서버
            strServerAddress = "hts.etrade.co.kr"
            거래비밀번호 = "3487"
            password = txt_ebest_pwd.Text
        Else
            nServerType = 1     ' 모의투자서버
            strServerAddress = "demo.ebestsec.co.kr"
            password = "kys60590"
            거래비밀번호 = "0000"
        End If

        Dim id As String = txt_ebest_id1.Text
        Dim cert As String = txt_ebest인증비밀번호.Text

        Dim bSuccess As Boolean = ConnectToEbest(id, password, cert, nServerType, strServerAddress)
        If bSuccess = False Then
            Add_Log("일반", "로그인 실패")
        Else
            Add_Log("일반", "이베스트 로그인 성공 at " + strServerAddress)

        End If
    End Sub

    Private Sub btn_TimerStart_Click(sender As Object, e As EventArgs) Handles btn_TimerStart.Click
        If btn_TimerStart.Text = "START" Then
            Timer1.Interval = 1000
            Timer1.Enabled = True
            btn_TimerStart.Text = "STOP"
            timerCount = 0
        Else
            Timer1.Enabled = False
            btn_TimerStart.Text = "START"
            label_timerCounter.Text = "---"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        label_timerCounter.Text = timerCount.ToString()

        Select Case timerCount
            Case 0
                If EBESTisConntected = True Then
                    XAQuery_전체종목조회함수()
                End If
            Case 1
                If EBESTisConntected = True Then
                    XAQuery_EBEST_순매수현황조회함수() ' +  Received면 콜 분봉 조회
                End If

            Case 2
                If EBESTisConntected = True Then 계좌조회()
                If EBESTisConntected = True Then 선물옵션_잔고평가_이동평균조회()

            Case 3
                If EBESTisConntected = True Then 구매가능수량조회(0)

            Case 4
                If EBESTisConntected = True Then XAQuery_EBEST_분봉데이터호출함수_1분(1)

            Case 5
                If EBESTisConntected = True Then 구매가능수량조회(1)
                If EBESTisConntected = True Then 선물옵션_잔고평가_이동평균조회()


            Case 8
                If EBESTisConntected = True Then F2_Clac_DisplayAllGrid()
                매매신호처리함수()
            Case Else

        End Select

        timerCount = timerCount + 1
        If timerCount >= timerMaxInterval Then timerCount = 0
    End Sub

    Private Sub 손절매수준설정(ByVal 남은날짜 As Integer)

        남은날짜 = 남은날짜 Mod 7
        Dim isWeekly As Boolean = False
        Dim 마감시간이후기울기 As String = txt_F2_마감시간이후기울기.Text
        Dim 중간청산목표이익 As String = txt_F2_중간청산비율.Text
        Dim 기준가격 As String = txt_JongmokTargetPrice.Text
        If txt_week_정규.Text = "W" Then isWeekly = True

        Select Case 남은날짜
            Case 0

                중간청산목표이익 = "0.35"

            Case 1

                중간청산목표이익 = "0.35"

            Case 2

                중간청산목표이익 = "0.3"

            Case 3

                중간청산목표이익 = "0.3"
                chk_실거래실행.Checked = False
            Case 6

                중간청산목표이익 = "0.3"
                chk_실거래실행.Checked = False
        End Select

        txt_F2_중간청산비율.Text = 중간청산목표이익

    End Sub

    Private Sub cmb_selectedJongmokIndex_0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_0.SelectedIndexChanged
        Dim selectedIndex = cmb_selectedJongmokIndex_0.SelectedIndex

        If selectedIndex > 0 And selectedJongmokIndex(0) <> selectedIndex - 1 Then

            selectedJongmokIndex(0) = selectedIndex - 1
            '여기다가 행사가 추출하는 로직 추가함
            콜선택된행사가(0) = 인덱스로부터행사가찾기(selectedJongmokIndex(0))
            chk_ChangeTargetIndex.Checked = False 'Clac_DisplayAllGrid에서 또 자동으로 selected를 계산하는 걸 방지하기 위해 false로 바꾼다
            F2_Clac_DisplayAllGrid()
            Add_Log("일반", "cmb_selectedJongmokIndex_0_SelectedIndexChanged  호출됨")
        End If
    End Sub

    Private Sub cmb_selectedJongmokIndex_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_1.SelectedIndexChanged
        Dim selectedIndex = cmb_selectedJongmokIndex_1.SelectedIndex

        If selectedIndex > 0 And selectedJongmokIndex(1) <> selectedIndex - 1 Then

            selectedJongmokIndex(1) = selectedIndex - 1
            '여기다가 행사가 추출하는 로직 추가함

            콜선택된행사가(1) = 인덱스로부터행사가찾기(selectedJongmokIndex(1))
            chk_ChangeTargetIndex.Checked = False
            F2_Clac_DisplayAllGrid()
            Add_Log("일반", "cmb_selectedJongmokIndex_1_SelectedIndexChanged  호출됨")
        End If
    End Sub

    Private Sub RedrawAll()

        If optionList.Count > 0 Then
            InitFirstGrid()
            DrawGrid1Data()

            If currentIndex_1MIn >= 0 Then

                'grd_selected 조절하기
                'combo에 전체 종목을 Add한다 인덱스, 행사가, 현재가격
                cmb_selectedJongmokIndex_0.Items.Clear()
                cmb_selectedJongmokIndex_1.Items.Clear()

                cmb_selectedJongmokIndex_0.Items.Add(" ") '0번이 선택되는게 초기화인지 명시적으로 0번을 선택했는지를 확인하기 위해서 제일 앞에 널값을 하나 넣는다
                cmb_selectedJongmokIndex_1.Items.Add(" ")

                For i As Integer = 0 To optionList.Count - 1

                    Dim it As ListTemplate = optionList(i)
                    Dim str As String
                    str = i.ToString() & ". 행사가 : " & it.HangSaGa & " (" & it.price(0, 3) & ")"
                    cmb_selectedJongmokIndex_0.Items.Add(str)
                    str = i.ToString() & ". 행사가 : " & it.HangSaGa & " (" & it.price(1, 3) & ")"
                    cmb_selectedJongmokIndex_1.Items.Add(str)

                Next

                cmb_selectedJongmokIndex_1.SelectedIndex = selectedJongmokIndex(1) + 1
                cmb_selectedJongmokIndex_0.SelectedIndex = selectedJongmokIndex(0) + 1

            End If
            txt_TargetDate.Text = TargetDate
        End If
    End Sub

    Private Sub InitFirstGrid()

        grid1.Columns.Clear()
        grid1.Rows.Clear()

        '전체 크기 지정
        grid1.ColumnCount = 7
        grid1.RowCount = TotalCount

        For i As Integer = 0 To 6
            grid1.Columns(i).Width = 68
            grid1.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        grid1.Columns(0).HeaderText = "시간가치"
        grid1.Columns(1).HeaderText = "시가"
        grid1.Columns(2).HeaderText = "종가"
        grid1.Columns(3).HeaderText = "행사가"
        grid1.Columns(4).HeaderText = "시간가치"
        grid1.Columns(5).HeaderText = "시가"
        grid1.Columns(6).HeaderText = "종가"

        grid1.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
        grid1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For i As Integer = 0 To TotalCount - 1
            grid1.Rows(i).HeaderCell.Value = i.ToString()
        Next

        grid1.RowHeadersWidth = 50

    End Sub

    Private Sub DrawGrid1Data()


        For i As Integer = 0 To TotalCount - 1
            Dim it As ListTemplate = optionList(i)
            grid1.Rows(i).Cells(0).Value = Format(it.시간가치(0), "##0.00")
            grid1.Rows(i).Cells(1).Value = Format(it.price(0, 0), "##0.00")
            grid1.Rows(i).Cells(2).Value = Format(it.price(0, 3), "##0.00")
            grid1.Rows(i).Cells(3).Value = it.HangSaGa
            grid1.Rows(i).Cells(4).Value = Format(it.시간가치(1), "##0.00")
            grid1.Rows(i).Cells(5).Value = Format(it.price(1, 0), "##0.00")
            grid1.Rows(i).Cells(6).Value = Format(it.price(1, 3), "##0.00")
        Next

        If selectedJongmokIndex(0) >= 0 And selectedJongmokIndex(1) >= 0 Then
            grid1.Rows(selectedJongmokIndex(0)).Cells(2).Style.BackColor = Color.LightGreen
            grid1.Rows(selectedJongmokIndex(1)).Cells(6).Style.BackColor = Color.LightGreen
            grid1.Rows(selectedJongmokIndex(0)).Cells(2).Style.ForeColor = Color.Black
            grid1.Rows(selectedJongmokIndex(1)).Cells(6).Style.ForeColor = Color.Black
        End If
    End Sub

    Public Sub Display계좌정보()

        lbl_주문가능금액.Text = Format(주문가능금액, "###,###,###,###,##0")
        lbl_인출가능금액.Text = Format(인출가능금액, "###,###,###,###,##0")
        lbl_매매손익합계.Text = Format(평가종합.매매손익합계, "###,###,###,###,##0")
        lbl_평가금액.Text = Format(평가종합.평가금액, "###,###,###,###,##0")
        lbl_평가손익.Text = Format(평가종합.평가손익, "###,###,###,###,##0")
        lbl_계좌번호.Text = strAccountNum

        Init_grd_잔고조회()
        Draw_grd_잔고조회()

    End Sub

    Private Sub Init_grd_잔고조회()

        grd_잔고조회.Columns.Clear()
        grd_잔고조회.Rows.Clear()

        grd_잔고조회.ColumnCount = 13         '전체 크기 지정

        If List잔고 Is Nothing Then
            grd_잔고조회.RowCount = 1
        Else
            grd_잔고조회.RowCount = List잔고.Count
        End If

        grd_잔고조회.RowHeadersWidth = 35

        grd_잔고조회.Columns(0).HeaderText = "종목번호"
        grd_잔고조회.Columns(1).HeaderText = "구분"
        grd_잔고조회.Columns(2).HeaderText = "잔고수량"
        grd_잔고조회.Columns(3).HeaderText = "청산가능수량"
        grd_잔고조회.Columns(4).HeaderText = "평균단가"
        grd_잔고조회.Columns(5).HeaderText = "총매입금액"
        grd_잔고조회.Columns(6).HeaderText = "매매구분"
        grd_잔고조회.Columns(7).HeaderText = "매매손익"
        grd_잔고조회.Columns(8).HeaderText = "처리순번"
        grd_잔고조회.Columns(9).HeaderText = "현재가"
        grd_잔고조회.Columns(10).HeaderText = "평가금액"
        grd_잔고조회.Columns(11).HeaderText = "평가손익"
        grd_잔고조회.Columns(12).HeaderText = "수익율(%)"

        Dim defaultWidth As Integer = 72
        grd_잔고조회.Columns(0).Width = defaultWidth + 15
        grd_잔고조회.Columns(1).Width = defaultWidth
        grd_잔고조회.Columns(2).Width = defaultWidth
        grd_잔고조회.Columns(3).Width = defaultWidth + 15
        grd_잔고조회.Columns(4).Width = defaultWidth
        grd_잔고조회.Columns(5).Width = defaultWidth + 15
        grd_잔고조회.Columns(6).Width = defaultWidth
        grd_잔고조회.Columns(7).Width = defaultWidth
        grd_잔고조회.Columns(8).Width = defaultWidth
        grd_잔고조회.Columns(9).Width = defaultWidth
        grd_잔고조회.Columns(10).Width = defaultWidth + 15
        grd_잔고조회.Columns(11).Width = defaultWidth + 15
        grd_잔고조회.Columns(12).Width = defaultWidth + 15

        grd_잔고조회.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For i = 0 To grd_잔고조회.ColumnCount - 1   '헤더 가운데 정렬
            grd_잔고조회.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grd_잔고조회.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Sub Draw_grd_잔고조회()

        If List잔고 Is Nothing Then Return
        Dim 총매입금액 As Long = 0
        Dim 평가손익 As Long = 0

        For i As Integer = 0 To List잔고.Count - 1
            grd_잔고조회.Rows(i).Cells(0).Value = List잔고(i).A01_종복번호
            grd_잔고조회.Rows(i).Cells(1).Value = List잔고(i).A02_구분
            grd_잔고조회.Rows(i).Cells(2).Value = Format(List잔고(i).A03_잔고수량, "###,###,###,##0")
            grd_잔고조회.Rows(i).Cells(3).Value = List잔고(i).A04_청산가능수량
            grd_잔고조회.Rows(i).Cells(4).Value = List잔고(i).A05_평균단가
            grd_잔고조회.Rows(i).Cells(5).Value = Format(List잔고(i).A06_총매입금액, "###,###,###,##0")
            grd_잔고조회.Rows(i).Cells(6).Value = List잔고(i).A07_매매구분
            grd_잔고조회.Rows(i).Cells(7).Value = List잔고(i).A08_매매손익
            grd_잔고조회.Rows(i).Cells(8).Value = List잔고(i).A09_처리순번
            grd_잔고조회.Rows(i).Cells(9).Value = List잔고(i).A10_현재가
            grd_잔고조회.Rows(i).Cells(10).Value = Format(List잔고(i).A11_평가금액, "###,###,###,##0")
            grd_잔고조회.Rows(i).Cells(11).Value = Format(List잔고(i).A12_평가손익, "###,###,###,##0")
            grd_잔고조회.Rows(i).Cells(12).Value = Format(List잔고(i).A13_수익율, "##0.0")

            If List잔고(i).A13_수익율 > 0 Then
                grd_잔고조회.Rows(i).Cells(12).Style.BackColor = Color.Yellow
                grd_잔고조회.Rows(i).Cells(12).Style.ForeColor = Color.Red
            Else
                grd_잔고조회.Rows(i).Cells(12).Style.BackColor = Color.LightGreen
                grd_잔고조회.Rows(i).Cells(12).Style.ForeColor = Color.Black
            End If

            '총매입금액 += List잔고(i).A06_총매입금액
            '평가손익 += List잔고(i).A12_평가손익
        Next

        'Dim rawCount As Integer = List잔고.Count
        'Dim 수익율 As Single = 평가손익 / 총매입금액
        'If rawCount > 0 Then
        'grd_잔고조회.Rows(rawCount).Cells(5).Value = Format(총매입금액, "###,###,###,##0")
        'grd_잔고조회.Rows(rawCount).Cells(11).Value = Format(평가손익, "###,###,###,##0")
        'grd_잔고조회.Rows(rawCount).Cells(12).Value = Format(수익율, "##0.0%")

        'If 수익율 > 0 Then
        'grd_잔고조회.Rows(rawCount).Cells(12).Style.BackColor = Color.Yellow
        'grd_잔고조회.Rows(rawCount).Cells(12).Style.ForeColor = Color.Red
        'Else
        'grd_잔고조회.Rows(rawCount).Cells(12).Style.BackColor = Color.LightGreen
        'grd_잔고조회.Rows(rawCount).Cells(12).Style.ForeColor = Color.Black
        'End If
        'End If
    End Sub

    Public Function 진짜할건지확인(str As String) As Boolean

        Dim dr As DialogResult
        dr = MessageBox.Show("진짜 " & str & "할건가?", str & "여부", MessageBoxButtons.YesNo)

        If dr = DialogResult.No Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btn_call_매도_Click(sender As Object, e As EventArgs) Handles btn_call_매도.Click

        If 진짜할건지확인("매매") = False Then Return
        If 매도실행호출_1개(0) = False Then Add_Log("일반", "매도 시 최소구매가능개수 부족. 방향 = 콜")

    End Sub

    Private Sub btn_put_매도_Click(sender As Object, e As EventArgs) Handles btn_put_매도.Click

        If 진짜할건지확인("매매") = False Then Return
        If 매도실행호출_1개(1) = False Then Add_Log("일반", "매도 시 최소구매가능개수 부족. 방향 = 풋")

    End Sub

    Private Sub btn_매도를청산_Click(sender As Object, e As EventArgs) Handles btn_매도를청산.Click
        If 진짜할건지확인("매매") = False Then Return
        If List잔고 IsNot Nothing Then

            Dim 매매1회최대수량 As Integer = Val(txt_F2_1회최대매매수량.Text)
            For i As Integer = 0 To List잔고.Count - 1

                Dim it As 잔고Type = List잔고(i)
                If it.A02_구분 = "매도" Then  '무엇인가 매도된 상태라면
                    Dim 종목번호 As String = it.A01_종복번호

                    Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                    Dim count As Integer = 0
                    If callput = "2" Then
                        count = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                        'count = Math.Min(count, 콜최대구매개수 - 콜현재환매개수)
                        count = Math.Min(count, 매매1회최대수량)
                    Else
                        count = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                        'count = Math.Min(count, 풋최대구매개수 - 풋현재환매개수)
                        count = Math.Min(count, 매매1회최대수량)
                    End If
                    If count > 0 Then 한종목매수(종목번호, it.A10_현재가, count)
                End If

            Next
        End If
    End Sub

    Private Sub btn_매수를청산_Click(sender As Object, e As EventArgs) Handles btn_매수를청산.Click
        If 진짜할건지확인("매매") = False Then Return
        If List잔고 IsNot Nothing Then

            Dim 매매1회최대수량 As Integer = Val(txt_F2_1회최대매매수량.Text)
            For i As Integer = 0 To List잔고.Count - 1

                Dim it As 잔고Type = List잔고(i)
                If it.A02_구분 = "매수" Then  '무엇인가 매수된 상태라면
                    Dim 종목번호 As String = it.A01_종복번호
                    Dim count As Integer = Math.Min(it.A03_잔고수량, 매매1회최대수량)
                    한종목매도(종목번호, it.A10_현재가, count)
                End If

            Next
        End If
    End Sub

    Private Sub btn_call_구매가능수_Click(sender As Object, e As EventArgs) Handles btn_call_구매가능수.Click
        If currentIndex_1MIn >= 0 Then 구매가능수량조회(0)
    End Sub

    Private Sub btn_put_구매가능수_Click(sender As Object, e As EventArgs) Handles btn_put_구매가능수.Click
        If currentIndex_1MIn >= 0 Then 구매가능수량조회(1)
    End Sub

    Private Sub btn_F2_전체조건반복_Click(sender As Object, e As EventArgs) Handles btn_F2_전체조건반복.Click
        chk_실거래실행.Checked = False
        Form1.chk_양매도실행.Checked = False
        Form1.chk_중간청산.Checked = False
        당일반복중_flag = True

        Dim 선행포인트수마진() As String = {"0.85"} 'a 일자별 자동 조절 취소
        Dim 순매수판정기준() As Integer = {0} 'b
        Dim 최대포인트수() As String = {"06"} 'c
        Dim 상승하락기울기기준() As String = {"4.0"} 'd  일자별 자동 조절
        Dim PIP_CALC_MAX_INDEX() As String = {"190"} 'ee
        Dim 손절차() As String = {"09"} 'f
        Dim 익절차() As String = {"12"} 'g
        Dim 매수마감시간후기울기() As String = {"4.5"} 'h 일자별 자동 조절
        Dim 최초매매시작시간() As String = {"90800"} 'i
        Dim temp_시작전허용기울기() As String = {"35"} 'j
        Dim timeoutTime() As String = {"151500"} 'k
        Dim 매수시작시간() As String = {"102000"}
        Dim 매수마감시간() As String = {"113000"}
        Dim 신호최소유지시간() As Integer = {6}
        Dim 중간청산이익목표() As String = {"0.35"}
        Dim 시작전매도해제기울기_TEMP() As Double = {"25"}
        Dim 옵션기준손절매() As String = {"-0.20", "-0.22", "-0.24", "-0.26", "-0.28", "-0.30", "-0.35", "-0.40", "-0.45", "-0.50"}
        Dim 기관순매수적용비율() As String = {"1.0"}


        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To 선행포인트수마진.Length - 1
            For b As Integer = 0 To 0  '순매수판정기준.Length - 1
                For c As Integer = 0 To 최대포인트수.Length - 1
                    For d As Integer = 0 To 상승하락기울기기준.Length - 1
                        For ee As Integer = 0 To PIP_CALC_MAX_INDEX.Length - 1
                            For f As Integer = 0 To 손절차.Length - 1
                                For g As Integer = 0 To 익절차.Length - 1
                                    For h As Integer = 0 To 매수마감시간후기울기.Length - 1
                                        For i As Integer = 0 To 최초매매시작시간.Length - 1
                                            For j As Integer = 0 To temp_시작전허용기울기.Length - 1
                                                For k As Integer = 0 To timeoutTime.Length - 1
                                                    For L As Integer = 0 To 매수시작시간.Length - 1
                                                        For M As Integer = 0 To 매수마감시간.Length - 1
                                                            For n As Integer = 0 To 신호최소유지시간.Length - 1
                                                                For q As Integer = 0 To 중간청산이익목표.Length - 1
                                                                    For r As Integer = 0 To 시작전매도해제기울기_TEMP.Length - 1
                                                                        For s As Integer = 0 To 옵션기준손절매.Length - 1
                                                                            For t As Integer = 0 To 기관순매수적용비율.Length - 1

                                                                                txt_선행_포인트_마진.Text = 선행포인트수마진(a)
                                                                                cmb_F2_순매수기준.SelectedIndex = 순매수판정기준(b)
                                                                                txt_F2_최대포인트수.Text = 최대포인트수(c)
                                                                                txt_F2_상승하락기울기기준.Text = 상승하락기울기기준(d)
                                                                                txt_F2_PIP_CALC_MAX_INDEX.Text = PIP_CALC_MAX_INDEX(ee)
                                                                                txt_F2_손절매차.Text = 손절차(f)
                                                                                txt_F2_익절차.Text = 익절차(g)
                                                                                txt_F2_마감시간이후기울기.Text = 매수마감시간후기울기(h)
                                                                                txt_F2_최초매매시작시간.Text = 최초매매시작시간(i)
                                                                                txt_F2_1차매매_기준_기울기.Text = temp_시작전허용기울기(j)
                                                                                txt_F2_TimeoutTime.Text = timeoutTime(k)
                                                                                txt_F2_매수시작시간.Text = 매수시작시간(L)
                                                                                txt_F2_매수마감시간.Text = 매수마감시간(M)
                                                                                신호최소유지시간index = 신호최소유지시간(n)
                                                                                txt_F2_중간청산비율.Text = 중간청산이익목표(q)
                                                                                txt_F2_1차매매_해제_기울기.Text = 시작전매도해제기울기_TEMP(r)
                                                                                txt_F2_옵션가기준손절매.Text = 옵션기준손절매(s)
                                                                                txt_F2_기관순매수적용비율.Text = 기관순매수적용비율(t)

                                                                                txt_선행_포인트_마진.Refresh()
                                                                                txt_F2_최대포인트수.Refresh()
                                                                                txt_F2_상승하락기울기기준.Refresh()
                                                                                txt_F2_PIP_CALC_MAX_INDEX.Refresh()
                                                                                txt_F2_손절매차.Refresh()
                                                                                txt_F2_익절차.Refresh()
                                                                                txt_F2_마감시간이후기울기.Refresh()
                                                                                cmb_F2_순매수기준.Refresh()
                                                                                txt_F2_최초매매시작시간.Refresh()
                                                                                txt_F2_TimeoutTime.Refresh()
                                                                                txt_F2_매수시작시간.Refresh()
                                                                                txt_F2_매수마감시간.Refresh()
                                                                                txt_F2_중간청산비율.Refresh()
                                                                                txt_F2_옵션가기준손절매.Refresh()
                                                                                txt_F2_기관순매수적용비율.Refresh()
                                                                                txt_F2_1차매매_기준_기울기.Refresh()
                                                                                txt_F2_1차매매_해제_기울기.Refresh()

                                                                                Dim cntstr As String
                                                                                If cnt < 10 Then
                                                                                    cntstr = "00" & cnt.ToString()
                                                                                ElseIf cnt >= 10 And cnt < 100 Then
                                                                                    cntstr = "0" & cnt.ToString()
                                                                                Else
                                                                                    cntstr = cnt.ToString()
                                                                                End If

                                                                                SoonMesuSimulation_조건 = String.Format("CNT_{0}_A_{1}_B_{2}_C_{3}_D_{4}_E_{5}_F_{6}_G_{7}_H_{8}_I_{9}_J_{10}_K_{11}_L_{12}_M_{13}_N_{14}_Q_{15}_R_{16}_S_{17}_T_{18}", cntstr, 선행포인트수마진(a), 순매수판정기준(b), 최대포인트수(c), 상승하락기울기기준(d), PIP_CALC_MAX_INDEX(ee), 손절차(f), 익절차(g), 매수마감시간후기울기(h), 최초매매시작시간(i), temp_시작전허용기울기(j), timeoutTime(k), 매수시작시간(L), 매수마감시간(M), 신호최소유지시간(n), 중간청산이익목표(q), 시작전매도해제기울기_TEMP(r), 옵션기준손절매(s), 기관순매수적용비율(t))

                                                                                'SoonMesuSimulation_조건 = String.Format("CNT_{0}_E_{1}_F_{2}", cntstr, 손절차(ee), 익절차(f))
                                                                                Console.WriteLine(SoonMesuSimulation_조건)
                                                                                Add_Log("조건 : ", SoonMesuSimulation_조건)
                                                                                자동반복계산로직(cnt, True) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                                                                                cnt += 1
                                                                            Next
                                                                        Next
                                                                    Next
                                                                Next
                                                            Next
                                                        Next
                                                    Next
                                                Next
                                            Next
                                        Next
                                    Next
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next
        당일반복중_flag = False
        SoonMesuSimulation_조건 = ""
    End Sub

    Public Sub Timer_Change()
        If btn_TimerStart.Text = "START" Then
            Timer1.Interval = 1000
            Timer1.Enabled = True
            btn_TimerStart.Text = "STOP"
            timerCount = 0
        End If
    End Sub

    Private Sub btn_InsertDB_Click(sender As Object, e As EventArgs) Handles btn_InsertDB.Click
        AutoSave()
    End Sub

    Private Sub btn_전체정리_Click(sender As Object, e As EventArgs) Handles btn_전체정리.Click

        If 진짜할건지확인("매매") = False Then Return
        전체잔고정리하기()
    End Sub

    Private Sub Timer_AutoSave111_Tick(sender As Object, e As EventArgs) Handles Timer_AutoSave111.Tick
        AutoSave()
        Timer_AutoSave111.Enabled = False
    End Sub

    Private Sub 일일조건설정(ByVal strToday As String)

        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)
        sMonth = 월물
        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)

        월물_위클리옵션판단(남은날짜) 'txt_월물과 txt_weekly_정규 텍스트박스에 값을 입력한다
        손절매수준설정(남은날짜)
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub F2_Chart_순매수_PaddingChanged(sender As Object, e As EventArgs) Handles F2_Chart_순매수.PaddingChanged

    End Sub
End Class