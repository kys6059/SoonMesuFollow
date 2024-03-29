﻿Option Explicit On
Imports System.Windows.Forms.DataVisualization.Charting
Imports Google.Api
Imports Newtonsoft.Json

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

            F2_날짜변경처리함수() '여기서 날짜별로 인덱스집합을 처리한다

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

        lbl_F2_콜중간청산갯수.Text = 콜중간청산가능개수.ToString()
        lbl_F2_풋중간청산갯수.Text = 풋중간청산가능개수.ToString()
    End Sub

    Private Sub F2_Clac_DisplayAllGrid()

        If currentIndex_순매수 >= 0 Then
            lbl_ReceiveCounter.Text = "수신횟수 = " & ReceiveCount.ToString()

            'CalcColorData()        '최대최소 계산
            CalcPIPData()          '대표선 계산
            Calc이동평균Data() '일분옵션데이터의 값을 루프를 돌면서 이동평균을 계산해서 다시 입력한다

            Calc코스피지수이동평균Data()  '코스피의 이평선을 계산한다 -65 *  2 기준

            CalcMACD이동평균Data() 'MACD관련 이동평균선을 루프를 돌면서 계산해서 입력한다  
            CalcMACD계산치Data()   'MACD값, 신호선 등을 그린다

            CalcRSIData() 'RSI 값을 계산한다

            CalcAlgorithmAll() '--------------------------- 신호 발생 / 해제 확인

            If chk_F2_화면끄기.Checked = False Then
                화면그리기()
                Update()
            End If

            If chk_F2_AutoSave.Checked = True And isRealFlag = True Then '자동 저장 기능 
                If currentIndex_순매수 >= 780 Then

                    chk_F2_AutoSave.Checked = False

                    Timer_AutoSave111.Interval = 10000
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
        grid_3.ColumnCount = 5
        grid_3.RowCount = 5



        grid_3.RowHeadersVisible = False

        grid_3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For i As Integer = 0 To grid_3.ColumnCount - 1
            grid_3.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid_3.Columns(i).Width = 70
        Next

        grid_3.Columns(0).HeaderText = "주체"
        grid_3.Columns(1).HeaderText = "5분(N1)"
        grid_3.Columns(2).HeaderText = "12.5분(매도)"
        grid_3.Columns(3).HeaderText = "20분(O 기준)"
        grid_3.Columns(4).HeaderText = "40분(상관)"
        grid_3.Columns(0).Width = 120
        grid_3.Columns(3).Width = 100
        grid_3.Columns(4).Width = 100



        For i = 0 To 3

            Dim 주체 As String
            Dim 금액 As Long
            If i = 0 Then
                주체 = " (합계)"
                금액 = 순매수리스트(currentIndex_순매수).외국인_기관_순매수
            ElseIf i = 1 Then
                주체 = " (외국인)"
                금액 = 순매수리스트(currentIndex_순매수).외국인순매수
            ElseIf i = 2 Then
                주체 = " (기관)"
                금액 = 순매수리스트(currentIndex_순매수).기관순매수
            Else
                주체 = " (외국인선물)"
                금액 = 순매수리스트(currentIndex_순매수).외국인_선물_순매수
            End If


            grid_3.Rows(i).Cells(0).Value = i.ToString() & 주체
            grid_3.Rows(i).Cells(2).Value = Format(금액, "###,##0")


            If 금액 > 0 Then grid_3.Rows(i).Cells(2).Style.ForeColor = Color.Red
            If 금액 < 0 Then grid_3.Rows(i).Cells(2).Style.ForeColor = Color.Blue


            'Dim 시작전기울기 = Calc_직선기울기계산(0)
            Dim 매수기준기울기 As Single = Math.Round(틱당기울기계산(i, O_tick_count_기준), 1)

            grid_3.Rows(i).Cells(3).Value = 매수기준기울기

            If i = 1 And 매수기준기울기 > O_외국인현물발생기준기울기 Then
                grid_3.Rows(i).Cells(3).Style.ForeColor = Color.Red
            ElseIf i = 1 And 매수기준기울기 < (O_외국인현물발생기준기울기 * -1) Then
                grid_3.Rows(i).Cells(3).Style.ForeColor = Color.Blue
            End If

            If i = 3 And 매수기준기울기 > O_선물발생기준기울기 Then
                grid_3.Rows(i).Cells(3).Style.ForeColor = Color.Red
            ElseIf i = 3 And 매수기준기울기 < (O_선물발생기준기울기 * -1) Then
                grid_3.Rows(i).Cells(3).Style.ForeColor = Color.Blue
            End If


            Dim 매도기준기울기 As Single = Math.Round(틱당기울기계산(i, O_해제tick_count_기준), 1)
            grid_3.Rows(i).Cells(4).Value = 매도기준기울기

            If i = 1 And 매도기준기울기 > O_외국인현물해제기준기울기 Then
                grid_3.Rows(i).Cells(4).Style.ForeColor = Color.Red
            ElseIf i = 1 And 매도기준기울기 < (O_외국인현물해제기준기울기 * -1) Then
                grid_3.Rows(i).Cells(4).Style.ForeColor = Color.Blue
            End If

            If i = 3 And 매도기준기울기 > O_선물해제기준기울기 Then
                grid_3.Rows(i).Cells(4).Style.ForeColor = Color.Red
            ElseIf i = 3 And 매도기준기울기 < (O_선물해제기준기울기 * -1) Then
                grid_3.Rows(i).Cells(4).Style.ForeColor = Color.Blue
            End If
        Next


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
            grid_shinho.Columns(11).HeaderText = "-"
            grid_shinho.Columns(12).HeaderText = "-"
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

            grid_shinho.Columns(11).Width = 20
            grid_shinho.Columns(12).Width = 20

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
                grid_shinho.Rows(i).Cells(11).Value = s.A11_손절기준가격
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

                'For j As Integer = 0 To grid_shinho.ColumnCount - 1 '매수시간에 아닌 때 발생한 신호는 회색처리한다
                'If Val(s.A02_발생시간) > Val(txt_F2_매수마감시간.Text) Then grid_shinho.Rows(i).Cells(j).Style.ForeColor = Color.Gray
                'Next
            Next
        End If
    End Sub

    Private Sub DrawGraph()

        F2_InitGraph()
        F2_DrawWinFormGraph(0)

        Init_Option_1min_Graph()
        Draw_Option_1min_Graph()


        Init_MACD_Graph()
            Draw_MACD_Graph()





    End Sub

    Private Sub CalcColorData()

    End Sub

    Private Sub F2_InitGraph()

        If EBESTisConntected = False Then F2_Chart_순매수.Visible = False
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
        F2_Chart_순매수.Series(str).Color = Color.Gray
        F2_Chart_순매수.Series(str).BorderWidth = 3
        F2_Chart_순매수.Series(str).YAxisType = AxisType.Secondary

        '이평선 그리기
        str = "MV65"
        F2_Chart_순매수.Series.Add(str)
        F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
        F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
        F2_Chart_순매수.Series(str).Color = Color.Red
        F2_Chart_순매수.Series(str).BorderWidth = 2
        F2_Chart_순매수.Series(str).BorderDashStyle = ChartDashStyle.Dot
        F2_Chart_순매수.Series(str).YAxisType = AxisType.Secondary


        For i As Integer = 0 To 3 '0 - 0: 외국인+기관, 1: 외국인, 2: 기관, 3: 외국인선물

            If (i = 0 And chk_F2_DATA_0.Checked = True) Or (i = 1 And chk_F2_DATA_1.Checked = True) Or (i = 2 And chk_F2_DATA_2.Checked = True) Or i = 3 Then
                str = "For_Kig_" + i.ToString()
                F2_Chart_순매수.Series.Add(str)
                F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
                F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary
                If i = 0 Then
                    F2_Chart_순매수.Series(str).Color = Color.Gray
                ElseIf i = 1 Then
                    F2_Chart_순매수.Series(str).Color = Color.Blue
                ElseIf i = 2 Then
                    F2_Chart_순매수.Series(str).Color = Color.Green
                ElseIf i = 3 Then
                    F2_Chart_순매수.Series(str).Color = Color.Red
                End If

                'str = "PIP_" + i.ToString()
                'F2_Chart_순매수.Series.Add(str)
                'F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
                'F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                'F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary
                'F2_Chart_순매수.Series(str).BorderDashStyle = ChartDashStyle.DashDotDot
                'F2_Chart_순매수.Series(str).BorderWidth = 2

                str = "slope_" + i.ToString()
                F2_Chart_순매수.Series.Add(str)
                F2_Chart_순매수.Series(str).ChartArea = ChartAreaStr
                F2_Chart_순매수.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                F2_Chart_순매수.Series(str).YAxisType = AxisType.Primary
                F2_Chart_순매수.Series(str).BorderDashStyle = ChartDashStyle.DashDotDot
                F2_Chart_순매수.Series(str).BorderWidth = 2

                If i = 0 Then
                    F2_Chart_순매수.Series(str).Color = Color.DarkRed
                ElseIf i = 1 Or i = 2 Then
                    F2_Chart_순매수.Series(str).Color = Color.MediumVioletRed
                ElseIf i = 3 Then
                    F2_Chart_순매수.Series(str).Color = Color.DarkBlue
                End If

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
                retIndex = F2_Chart_순매수.Series(oneMinute_Series).Points.AddXY(i, 순매수리스트(i).코스피지수) '오른쪽 이중축에 적용 - 
                F2_Chart_순매수.Series(oneMinute_Series).Points(i).AxisLabel = Format("{0}", 순매수리스트(i).sTime)
                min = Math.Min(min, 순매수리스트(i).코스피지수)
                max = Math.Max(max, 순매수리스트(i).코스피지수)

                '이평선 추가
                If 순매수리스트(retIndex).코스피지수_이동평균선 > 0 Then
                    F2_Chart_순매수.Series("MV65").Points.AddXY(retIndex, 순매수리스트(retIndex).코스피지수_이동평균선) '오른쪽 이중축에 적용 - 
                End If
            Next

            For j As Integer = 0 To 3
                Dim For_Kig_Series As String = "For_Kig_" + j.ToString()
                For i As Integer = 0 To currentIndex_순매수                     '각 매수 주체별 순매수 값 그리기 

                    If (j = 0 And chk_F2_DATA_0.Checked = True) Or (j = 1 And chk_F2_DATA_1.Checked = True) Or (j = 2 And chk_F2_DATA_2.Checked = True) Or j = 3 Then
                        Dim target순매수 As Long = Get순매수(i, j)

                        If j = 3 And target순매수 = 0 And i = currentIndex_순매수 Then Continue For '선물 순매수값을 아직 못 받아와서 마지막이 0 일때는 마지막값만 그리기 제외한다

                        retIndex = F2_Chart_순매수.Series(For_Kig_Series).Points.AddXY(i, target순매수) ' 순매수를 입력한다
                        Dim str As String = String.Format("시간:{0}{1}구분:{2}{3}순매수:{4}{5}코스피:{6}", 순매수리스트(i).sTime, vbCrLf, j, vbCrLf, target순매수, vbCrLf, 순매수리스트(i).코스피지수)
                        F2_Chart_순매수.Series(For_Kig_Series).Points(retIndex).ToolTip = str
                    End If

                Next
            Next

            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Maximum = max + 1
            F2_Chart_순매수.ChartAreas("ChartArea_0").AxisY2.Minimum = min - 1


            If currentIndex_순매수 >= 0 Then

                'For i As Integer = 0 To 2
                'Dim PIP_Series As String = "PIP_" + i.ToString()
                'If (i = 0 And chk_F2_DATA_0.Checked = True) Or (i = 1 And chk_F2_DATA_1.Checked = True) Or (i = 2 And chk_F2_DATA_2.Checked = True) or i = 3 Then
                ''PIP 시리즈를 표시한다
                'If PIP_Point_Lists(i).PoinIndexList IsNot Nothing Then
                'For j As Integer = 0 To PIP_Point_Lists(i).PoinIndexList.Count - 1
                'Dim point As Integer = PIP_Point_Lists(i).PoinIndexList(j)
                'Dim target순매수 As Long = Get순매수(point, i)
                'F2_Chart_순매수.Series(PIP_Series).Points.AddXY(point, target순매수)
                'Next

                'End If

                'End If

                'Next

                For i As Integer = 0 To 3

                    Dim 기울기시리즈 As String = "slope_" + i.ToString()
                    If (i = 0 And chk_F2_DATA_0.Checked = True) Or (i = 1 And chk_F2_DATA_1.Checked = True) Or (i = 2 And chk_F2_DATA_2.Checked = True) Or i = 3 Then
                        If currentIndex_순매수 - E2_tick_count_기준 >= 0 Then

                            If i = 1 Then

                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수 - E2_tick_count_기준, 순매수리스트(currentIndex_순매수 - E2_tick_count_기준).외국인순매수)
                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수, 순매수리스트(currentIndex_순매수).외국인순매수)

                            ElseIf i = 0 Then

                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수 - E2_tick_count_기준, 순매수리스트(currentIndex_순매수 - E2_tick_count_기준).외국인_기관_순매수)
                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수, 순매수리스트(currentIndex_순매수).외국인_기관_순매수)

                            ElseIf i = 2 Then

                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수 - E2_tick_count_기준, 순매수리스트(currentIndex_순매수 - E2_tick_count_기준).기관순매수)
                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수, 순매수리스트(currentIndex_순매수).기관순매수)
                            ElseIf i = 3 Then


                                If i = 3 And 순매수리스트(currentIndex_순매수).외국인_선물_순매수 = 0 Then Continue For '선물 순매수값을 아직 못 받아와서 마지막이 0 일때는 마지막값만 그리기 제외한다

                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수 - E2_tick_count_기준, 순매수리스트(currentIndex_순매수 - E2_tick_count_기준).외국인_선물_순매수)
                                F2_Chart_순매수.Series(기울기시리즈).Points.AddXY(currentIndex_순매수, 순매수리스트(currentIndex_순매수).외국인_선물_순매수)

                            End If

                        End If
                    End If
                Next
            End If

            If SoonMesuShinhoList IsNot Nothing Then
                For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                    Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                    Dim Str As String

                    For j As Integer = 0 To 3  '순매수타입 0 = 외국인+ 기관, 1 : 외국인, 2: 기관
                        If (j = 0 And chk_F2_DATA_0.Checked = True) Or (j = 1 And chk_F2_DATA_1.Checked = True) Or (j = 2 And chk_F2_DATA_2.Checked = True) Or j = 3 Then
                            '신호를 그리든데 각각의 순매수라인에 그린다
                            Str = "Shinho_" + i.ToString() + j.ToString()
                            F2_Chart_순매수.Series.Add(Str)
                            F2_Chart_순매수.Series(Str).ChartArea = "ChartArea_0"
                            F2_Chart_순매수.Series(Str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                            F2_Chart_순매수.Series(Str).Color = Color.DarkRed
                            F2_Chart_순매수.Series(Str).YAxisType = AxisType.Primary
                            F2_Chart_순매수.Series(Str).BorderWidth = 3

                            If s.A08_콜풋 = 0 Then
                                F2_Chart_순매수.Series(Str).Color = Color.Orange
                            Else
                                F2_Chart_순매수.Series(Str).Color = Color.SpringGreen
                            End If

                            'If currentIndex_순매수 >= s.A01_발생Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A01_발생Index, s.A04_신호발생순매수)  '시작점 - 오리지날
                            If currentIndex_순매수 >= s.A01_발생Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A01_발생Index, Get순매수(s.A01_발생Index, j))  '시작점

                            If s.A15_현재상태 = 1 Then '끝점
                                F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Solid
                                F2_Chart_순매수.Series(Str).Points.AddXY(currentIndex_순매수, Get순매수(currentIndex_순매수, j))
                            Else
                                F2_Chart_순매수.Series(Str).BorderDashStyle = ChartDashStyle.Dot
                                If currentIndex_순매수 >= s.A19_매도Index Then F2_Chart_순매수.Series(Str).Points.AddXY(s.A19_매도Index, Get순매수(s.A19_매도Index, j))
                            End If
                        End If

                    Next

                    '주가지수에 신호 그리기

                    Str = "kospi_" + i.ToString()
                    F2_Chart_순매수.Series.Add(Str)
                    F2_Chart_순매수.Series(Str).ChartArea = "ChartArea_0"
                    F2_Chart_순매수.Series(Str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                    F2_Chart_순매수.Series(Str).Color = Color.Red
                    F2_Chart_순매수.Series(Str).YAxisType = AxisType.Secondary
                    F2_Chart_순매수.Series(Str).BorderWidth = 3

                    If s.A08_콜풋 = 0 Then
                        F2_Chart_순매수.Series(Str).Color = Color.Red
                    Else
                        F2_Chart_순매수.Series(Str).Color = Color.Blue
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

                If isRealFlag = False And TotalCount > 1 Then   'DB에서 가져온 오늘의 index가 2개 이상일 때만 수행한다

                    Dim 콜종목 As Integer = 적합한종목찾기(0)
                    Dim 풋종목 As Integer = 적합한종목찾기(1)

                    If selectedJongmokIndex(0) <> 콜종목 And 콜종목 >= 0 Then
                        selectedJongmokIndex(0) = 콜종목
                        DB에서일분옵션데이터채워넣기(콜종목, timeIndex_1Min, 0)
                    End If
                    If selectedJongmokIndex(1) <> 풋종목 And 콜종목 >= 0 Then
                        selectedJongmokIndex(1) = 풋종목
                        DB에서일분옵션데이터채워넣기(풋종목, timeIndex_1Min, 1)
                    End If

                End If

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

        Dim indexCount As Integer = GetDataFromDBHandler_1Min(TargetDate)
        순매수리스트카운트 = Get순매수데이터(TargetDate) '전역변수 순매수리스트에 하루치 Data를 입력한다

        If indexCount > 0 And 순매수리스트카운트 > 0 Then


            MakeOptinList_For_1Minute(indexCount)
            TotalCount = indexCount

            selectedJongmokIndex(0) = 적합한종목찾기(0)
            selectedJongmokIndex(1) = 적합한종목찾기(1)
            DB에서일분옵션데이터채워넣기(selectedJongmokIndex(0), timeIndex_1Min, 0)
            DB에서일분옵션데이터채워넣기(selectedJongmokIndex(1), timeIndex_1Min, 1)

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

        '폼의 켑션
        Text = txt_programversion.Text & "__알고리즘_매수"


        Dim dt As Date = Now.AddDays(-30)  '여기 원래 -30을 넣어야 함
        Dim strdt As String = Format(dt, "yyMM01")

        'strdt = "230903"
        strdt = "240217"  '외국인선물이 저장되기 시작한 날


        txt_F2_DB_Date_Limit.Text = "WHERE cdate >= " + strdt

        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)
        sMonth = 월물

        월목라디오선택()  '월목중 하나 선택

        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)
        월목에따른텍스트입력하기(남은날짜) 'txt_월물과 txt_weekly_정규 텍스트박스에 값을 입력한다

        손절매수준설정(남은날짜)
        currentIndex = -1

        Dim ct As Integer = Val(DateTime.Now.ToString("HHmm"))

        If ct >= 830 And ct <= 840 Then  '자동시작 - 8시30분~8시40분 사이에 켜지면 자동으로 시작한다
            Add_Log("", "자동시작")
            InitDataStructure()
            InitDataStructure_1Min()
            이베스트로그인함수()
            ReceiveCount = 0
        End If

    End Sub

    Private Sub 월목라디오선택()

        Dim today As Date = Now()

        If Weekday(today) >= 3 And Weekday(today) <= 5 Then  '화수목이면 목요일, 나머지는 월요일로 선택함
            rdo_목요일.Checked = True
        Else
            rdo_월요일.Checked = True
        End If

    End Sub

    Private Sub 월목에따른텍스트입력하기(ByVal 남은날짜 As Integer)

        Dim txt월물 As String = sMonth
        Dim txtweekly As String = "G"

        Dim today As Date = Now()
        Dim strThisMonth As String = Format(today, "yyMM")
        Dim sCase As String = ""


        '202402 ---------- G
        If rdo_목요일.Checked = True Then

            If 남은날짜 < 7 Then  '옵션월물을 적용한다

                txt월물 = "20" & sMonth
                txtweekly = "G"
                sCase = "7일미만"

            ElseIf 남은날짜 >= 7 And 남은날짜 < 14 Then  '1주차

                txt월물 = "W1THU"
                txtweekly = "W"
                sCase = "14일미만"

            ElseIf 남은날짜 >= 14 And 남은날짜 < 28 Then

                Dim 목요일count As Integer = 0

                For i As Integer = 1 To today.Day - 1
                    Dim tempdate As Date = New Date(today.Year, today.Month, i)
                    If Weekday(tempdate) = 5 Then
                        목요일count = 목요일count + 1
                    End If
                Next

                txt월물 = "W" & (목요일count + 1).ToString() & "THU"
                txtweekly = "W"

                sCase = "14~28일미만"

            Else '28일 초과는 무조건 3주차 

                txt월물 = "W3THU"
                txtweekly = "W"
                sCase = "28일이상"

            End If
        ElseIf rdo_월요일.Checked = True Then

            txt월물 = 몇월몇번째월요일지찾기()
            txtweekly = "W"
        End If

        txt_월물.Text = txt월물
        txt_week_정규.Text = txtweekly
        Dim str As String = String.Format("월물 = {0}, Week/정규 = {1}, 남은날짜 = {2}({3}), CASE = {4}", txt월물, txtweekly, 남은날짜, 남은날짜 Mod 7, sCase)
        'Add_Log("설정", str)

    End Sub

    Private Function 몇월몇번째월요일지찾기() As String

        Dim txt월물 As String = ""

        Dim today As Date = Now()

        Dim targetDate As Date

        For i As Integer = 0 To 6   ' 월요일을 찾아낸다
            targetDate = today.AddDays(i)
            If Weekday(targetDate) = 2 Then
                Exit For
            End If
        Next

        '몇번째주인지 찾는다
        Dim iYear As Integer = targetDate.Year
        Dim iMonth As Integer = targetDate.Month
        Dim iDate As Integer = targetDate.Day

        Dim 월요일카운트 As Integer = 0
        For i As Integer = 1 To iDate

            Dim tempdate As Date = New Date(iYear, iMonth, i)
            If Weekday(tempdate) = 2 Then
                월요일카운트 = 월요일카운트 + 1
            End If
        Next

        txt월물 = "W" & 월요일카운트.ToString() & "MON"



        Return txt월물

    End Function

    Private Sub btn_당일반복_Click(sender As Object, e As EventArgs) Handles btn_당일반복.Click

        Dim timestatus As Boolean

        If Timer1.Enabled = True Then
            timestatus = True
            Timer1.Stop()
        End If

        chk_실거래실행.Checked = False
        당일반복중_flag = True
        chk_F2_화면끄기.Checked = True
        For i As Integer = 0 To 순매수리스트카운트 - 1

            currentIndex_순매수 = i

            If isRealFlag = False And TotalCount > 1 Then   'DB에서 가져온 오늘의 index가 2개 이상일 때만 수행한다

                Dim 콜종목 As Integer = 적합한종목찾기(0)
                Dim 풋종목 As Integer = 적합한종목찾기(1)

                If selectedJongmokIndex(0) <> 콜종목 And 콜종목 >= 0 Then
                    selectedJongmokIndex(0) = 콜종목
                    DB에서일분옵션데이터채워넣기(콜종목, timeIndex_1Min, 0)
                End If
                If selectedJongmokIndex(1) <> 풋종목 And 콜종목 >= 0 Then
                    selectedJongmokIndex(1) = 풋종목
                    DB에서일분옵션데이터채워넣기(풋종목, timeIndex_1Min, 1)
                End If

            End If

            F2_Clac_DisplayAllGrid()
            'Add_Log("일반", "currentIndex_순매수 = " + currentIndex_순매수.ToString() + ", 현재콜인덱스 = " + selectedJongmokIndex(0).ToString() + "  현재 풋 인덱스 = " + selectedJongmokIndex(1).ToString())
        Next
        chk_F2_화면끄기.Checked = False
        F2_Clac_DisplayAllGrid()

        Add_Log("일반", "Form2_당일 자동반복 완료")

        당일반복중_flag = False
        If timestatus = True Then
            Timer1.Start()
        End If

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

            If 일일조건설정flag = True Then
                일일조건설정(TargetDate)    '전체조건일 때는 스킵해야 함
            End If

            'If 남은날짜 = 0 Or 남은날짜 = 3 Then Continue For   '1,2,6 일만 한다
            'If 남은날짜 = 6 Or 남은날짜 = 2 Or 남은날짜 = 1 Then Continue For   '0,3 일만 한다




            '당일 내부에서 변경
            For j As Integer = 110 To 순매수리스트카운트 - 1  '--------------------------------------------------------------------------------- 950 부터 테스트를 위해 점프함  000000

                currentIndex_순매수 = j
                If currentIndex_순매수 = 순매수리스트카운트 - 1 Then
                    chk_F2_화면끄기.Checked = False
                End If

                If isRealFlag = False And TotalCount > 1 Then   'DB에서 가져온 오늘의 index가 2개 이상일 때만 수행한다

                    Dim 콜종목 As Integer = 적합한종목찾기(0)
                    Dim 풋종목 As Integer = 적합한종목찾기(1)

                    If selectedJongmokIndex(0) <> 콜종목 And 콜종목 >= 0 Then
                        selectedJongmokIndex(0) = 콜종목
                        DB에서일분옵션데이터채워넣기(콜종목, timeIndex_1Min, 0)
                    End If
                    If selectedJongmokIndex(1) <> 풋종목 And 콜종목 >= 0 Then
                        selectedJongmokIndex(1) = 풋종목
                        DB에서일분옵션데이터채워넣기(풋종목, timeIndex_1Min, 1)
                    End If

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


            InsertSoonMeSuShinhoResult("statistics")
            SoonMesuSimulationTotalShinhoList.Clear()

        End If

        당일반복중_flag = False

    End Sub


    Private Sub Init_MACD_Graph() 'MACD 신호선그래프임. 이평선은 옵션그래프에 있음

        'Chart2.Visible = False
        Dim str, ChartAreaStr As String

        Chart2.Series.Clear()
        Chart2.ChartAreas.Clear()
        Chart2.Legends.Clear()
        Chart2.Annotations.Clear()

        For i As Integer = 0 To 1 '콜풋

            ChartAreaStr = "MACD_CHART_" + i.ToString()
            Chart2.ChartAreas.Add(ChartAreaStr)

            str = "basic_series_" + i.ToString()  '중간에 0으로 채우는 시리즈
            Chart2.Series.Add(str)
            Chart2.Series(str).ChartArea = ChartAreaStr
            Chart2.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart2.Series(str).BorderWidth = 1
            Chart2.Series(str).Color = Color.Green




            'MACD 기준선, 신호선 추가
            For j As Integer = 0 To 1
                str = "MACD_CA_" + i.ToString() + "_" + j.ToString()
                Chart2.Series.Add(str)
                Chart2.Series(str).ChartArea = ChartAreaStr
                Chart2.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart2.Series(str).BorderWidth = 1

                If j = 0 Then
                    Chart2.Series(str).Color = Color.Red
                    Chart2.Series(str).BorderWidth = 2
                ElseIf j = 1 Then
                    Chart2.Series(str).Color = Color.Black
                End If
            Next

            'Lebel 설정
            Chart2.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"

            '축 선 속성 설정
            Chart2.ChartAreas(i).AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart2.ChartAreas(i).AxisX.MajorGrid.LineColor = Color.Gray
            Chart2.ChartAreas(i).AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart2.ChartAreas(i).AxisY.MajorGrid.LineColor = Color.Gray
            'Chart2.ChartAreas(i).AxisY.Interval = 0.2

        Next

    End Sub

    Private Sub Draw_MACD_Graph()

        Dim i, callput, retindex As Integer '

        If currentIndex_1MIn >= 0 Then

            For i = 0 To Chart2.Series.Count - 1
                Chart2.Series(i).Points.Clear()
            Next

            For callput = 0 To 1

                Dim BasicSeries As String = "basic_series_" + callput.ToString()

                Dim maxValue As Single = Single.MinValue
                Dim minValue As Single = Single.MaxValue

                Dim MACD_CA_기본(1) As String

                For j = 0 To 1
                    MACD_CA_기본(j) = "MACD_CA_" + callput.ToString() + "_" + j.ToString()
                Next

                For i = 0 To currentIndex_1MIn


                    ' main Series 입력
                    If 일분옵션데이터(callput).price(i, 1) > 0 Then
                        retindex = Chart2.Series(BasicSeries).Points.AddXY(i, 0) ' 위의 그래프와 X축을 통일하기 위해 0값을 모든 X값에 먼저 넣는다

                        'X축 시간
                        Chart2.Series(BasicSeries).Points(retindex).AxisLabel = Format("{0}", 일분옵션데이터(callput).ctime(i))

                        'CA 0,1번 그리기
                        Chart2.Series(MACD_CA_기본(0)).Points.AddXY(retindex, 일분옵션데이터(callput).CA_기본(0, i))
                        Chart2.Series(MACD_CA_기본(1)).Points.AddXY(retindex, 일분옵션데이터(callput).CA_기본(1, i))

                        If maxValue < 일분옵션데이터(callput).CA_기본(0, i) + 0.01 Then maxValue = 일분옵션데이터(callput).CA_기본(0, i) + 0.01
                        If minValue > 일분옵션데이터(callput).CA_기본(0, i) - 0.01 Then minValue = 일분옵션데이터(callput).CA_기본(0, i) - 0.01
                        If maxValue < 일분옵션데이터(callput).CA_기본(1, i) + 0.01 Then maxValue = 일분옵션데이터(callput).CA_기본(1, i) + 0.01
                        If minValue > 일분옵션데이터(callput).CA_기본(1, i) - 0.01 Then minValue = 일분옵션데이터(callput).CA_기본(1, i) - 0.01

                        Dim str As String = String.Format("시간:{0}{1}시가:{2}{3}종가:{4}{5}이평:{6}{7}MACD:{8}{9}신호선:{10}", 일분옵션데이터(0).ctime(i), vbCrLf, 일분옵션데이터(callput).price(i, 0), vbCrLf, 일분옵션데이터(callput).price(i, 3), vbCrLf, 일분옵션데이터(callput).이동평균선(i), vbCrLf, Math.Round(일분옵션데이터(callput).CA_기본(0, i), 3), vbCrLf, Math.Round(일분옵션데이터(callput).CA_기본(1, i), 3))
                        Chart2.Series(BasicSeries).Points(retindex).ToolTip = str
                        Chart2.Series(MACD_CA_기본(0)).Points(retindex).ToolTip = str
                        Chart2.Series(MACD_CA_기본(1)).Points(retindex).ToolTip = str

                    End If
                Next
                Chart2.ChartAreas(callput).AxisY.Minimum = minValue
                Chart2.ChartAreas(callput).AxisY.Maximum = maxValue

            Next
        End If

        Chart2.Visible = True

    End Sub


    Private Sub Init_Option_1min_Graph()

        If EBESTisConntected = False Then Chart1.Visible = False
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

            str = "ipLine_" + i.ToString()  '이동평균선라인
            Chart1.Series.Add(str)
            Chart1.Series(str).ChartArea = ChartAreaStr
            Chart1.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(str).Color = Color.Black
            Chart1.Series(str).BorderWidth = 1

            'MACD 이평선 정의
            For j As Integer = 0 To MA_Interval.Length - 1
                str = "MACD_MA_" + i.ToString() + "_" + j.ToString()
                Chart1.Series.Add(str)
                Chart1.Series(str).ChartArea = ChartAreaStr
                Chart1.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart1.Series(str).BorderWidth = 1

                If j = 0 Then
                    Chart1.Series(str).Color = Color.Red
                ElseIf j = 1 Then
                    Chart1.Series(str).Color = Color.DarkOrange
                ElseIf j = 2 Then
                    Chart1.Series(str).Color = Color.YellowGreen
                ElseIf j = 3 Then
                    Chart1.Series(str).Color = Color.Green
                ElseIf j = 4 Then
                    Chart1.Series(str).Color = Color.Blue
                End If
            Next


            ''Lebel 설정
            Chart1.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"
            '축 선 속성 설정
            Chart1.ChartAreas(i).AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart1.ChartAreas(i).AxisX.MajorGrid.LineColor = Color.Gray
            Chart1.ChartAreas(i).AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart1.ChartAreas(i).AxisY.MajorGrid.LineColor = Color.Gray
            Chart1.ChartAreas(i).AxisY.Interval = 0.2

            'F 알고리즘을 위한 설정
            str = "PIP_" + i.ToString()
            Chart1.Series.Add(str)
            Chart1.Series(str).ChartArea = ChartAreaStr
            Chart1.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(str).YAxisType = AxisType.Primary
            Chart1.Series(str).BorderDashStyle = ChartDashStyle.DashDotDot
            Chart1.Series(str).BorderWidth = 2
            Chart1.Series(str).Color = Color.MediumVioletRed
        Next

    End Sub

    Private Sub Draw_Option_1min_Graph()
        Dim i, callput, retindex As Integer '

        If currentIndex_1MIn >= 0 Then

            For i = 0 To Chart1.Series.Count - 1
                Chart1.Series(i).Points.Clear()
            Next

            For callput = 0 To 1

                Dim CandlestrickSeries As String = "CandleStick_" + callput.ToString()
                Dim 이동평균시리즈 As String = "ipLine_" + callput.ToString()
                Dim maxValue As Single = Single.MinValue
                Dim minValue As Single = Single.MaxValue

                Dim MACD_MA(MA_Interval.Length - 1) As String
                For j = 0 To MA_Interval.Length - 1
                    MACD_MA(j) = "MACD_MA_" + callput.ToString() + "_" + j.ToString()
                Next

                For i = 0 To currentIndex_1MIn
                    ' main Series 입력
                    If 일분옵션데이터(callput).price(i, 1) > 0 Then


                        'X축 시간
                        If 일분옵션데이터(callput).ctime(i) IsNot Nothing Then

                            If 일분옵션데이터(callput).ctime(i) > 900 Then
                                retindex = Chart1.Series(CandlestrickSeries).Points.AddXY(i, 일분옵션데이터(callput).price(i, 1)) '고가를 처음 넣는다
                                Chart1.Series(CandlestrickSeries).Points(retindex).YValues(1) = 일분옵션데이터(callput).price(i, 2) '저가
                                Chart1.Series(CandlestrickSeries).Points(retindex).YValues(2) = 일분옵션데이터(callput).price(i, 0) '시가
                                Chart1.Series(CandlestrickSeries).Points(retindex).YValues(3) = 일분옵션데이터(callput).price(i, 3) '종가
                                Chart1.Series(CandlestrickSeries).Points(retindex).AxisLabel = Format("{0}", 일분옵션데이터(callput).ctime(i))

                                If 일분옵션데이터(callput).price(i, 0) < 일분옵션데이터(callput).price(i, 3) Then '시가보다 종가가 크면 
                                    Chart1.Series(CandlestrickSeries).Points(retindex).Color = Color.Red
                                    Chart1.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Red
                                ElseIf 일분옵션데이터(callput).price(i, 0) > 일분옵션데이터(callput).price(i, 3) Then
                                    Chart1.Series(CandlestrickSeries).Points(retindex).Color = Color.Blue
                                    Chart1.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Blue
                                End If

                                '이동평균선 그리기
                                If 일분옵션데이터(callput).이동평균선(i) > 0 Then Chart1.Series(이동평균시리즈).Points.AddXY(retindex, 일분옵션데이터(callput).이동평균선(i))

                                Dim str As String = String.Format("시간:{0}{1}시가:{2}{3}종가:{4}{5}이평:{6}", 일분옵션데이터(0).ctime(i), vbCrLf, 일분옵션데이터(callput).price(i, 0), vbCrLf, 일분옵션데이터(callput).price(i, 3), vbCrLf, 일분옵션데이터(callput).이동평균선(i))
                                Chart1.Series(CandlestrickSeries).Points(retindex).ToolTip = str

                                If maxValue < 일분옵션데이터(callput).price(i, 1) + 0.1 Then maxValue = 일분옵션데이터(callput).price(i, 1) + 0.1 '계산해놓은 big, small로 보니 마지막 CurrentIndex의 값이 반영이 안되어 여기서 일일이 계산해서 처리하도록 변경 20220607
                                If minValue > 일분옵션데이터(callput).price(i, 2) - 0.1 Then minValue = 일분옵션데이터(callput).price(i, 2) - 0.1

                                'MACD이평 그리기
                                For j As Integer = 0 To 1 'MA_Interval.Length - 1

                                    If 일분옵션데이터(callput).MA(j, i) > 0 Then Chart1.Series(MACD_MA(j)).Points.AddXY(retindex, 일분옵션데이터(callput).MA(j, i))

                                Next
                            End If

                        End If




                    End If
                Next
                Chart1.ChartAreas(callput).AxisY.Minimum = minValue
                Chart1.ChartAreas(callput).AxisY.Maximum = maxValue

                'F알고리즘용 PIP 그리기
                If currentIndex_1MIn >= 0 Then
                    Dim PIP_Series As String = "PIP_" + callput.ToString()

                    If F_PoinIndexList(callput) IsNot Nothing Then
                        If F_PoinIndexList(callput).Count > 0 Then

                            Dim pipIndexList_temp As List(Of Integer) = New List(Of Integer)
                            pipIndexList_temp = F_PoinIndexList(callput)

                            For j As Integer = 0 To pipIndexList_temp.Count - 1
                                Dim point As Integer = pipIndexList_temp(j)   ' 이건 X에 해당
                                Dim 옵션가격 As Single = 일분옵션데이터(callput).price(point, 3)     '이건 Y값에 해당
                                Chart1.Series(PIP_Series).Points.AddXY(point, 옵션가격)
                            Next

                        End If
                    End If
                End If
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
                Chart1.Series(Str).Color = Color.DarkGreen
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

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

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
            Timer1.Interval = 1501
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

        Timer1.Interval = 1001

        label_timerCounter.Text = timerCount.ToString()
        If EBESTisConntected = True Then
        End If

        XAQuery_전체종목조회함수()   '이건 1초에 2건까지 가능. 나머지는 3초에 한건만 가능 . 만약 현재 cyclecount에 시가가 0으로 되어 있는게 있으면 전체 종목 수신할때 채워넣는다

        Select Case timerCount

            Case 0
                XAQuery_EBEST_순매수현황조회함수()
                XAQuery_EBEST_분봉데이터호출함수_1분(0)
                매매신호처리함수()
            Case 1
                계좌조회()
                선물옵션_잔고평가_이동평균조회()
            Case 2
                XAQuery_EBEST_외국인선물_순매수현황조회함수()

            Case 3
                XAQuery_EBEST_순매수현황조회함수()
                XAQuery_EBEST_분봉데이터호출함수_1분(1)
                매매신호처리함수()
            Case 4
                계좌조회()
                선물옵션_잔고평가_이동평균조회()

            Case 5
                XAQuery_EBEST_외국인선물_순매수현황조회함수()

        End Select

        F2_Clac_DisplayAllGrid()    '계산도 1초마다 수행



        timerCount = timerCount + 1
        If timerCount >= timerMaxInterval Then timerCount = 0
    End Sub

    Private Sub 손절매수준설정(ByVal 남은날짜 As Integer)

        남은날짜 = 남은날짜 Mod 7
        Dim isWeekly As Boolean = False

        Dim 켈리지수비율 As String = txt_F2_켈리지수비율.Text


        If txt_week_정규.Text = "W" Then isWeekly = True

        Select Case 남은날짜
            Case 0
                켈리지수비율 = "0.21"
            Case 1
                켈리지수비율 = "0.01"
                chk_실거래실행.Checked = False
            Case 2
                켈리지수비율 = "0.01"
                chk_실거래실행.Checked = False
            Case 3
                켈리지수비율 = "0.21"
            Case 6
                켈리지수비율 = "0.01"
                chk_실거래실행.Checked = False

        End Select


        txt_F2_켈리지수비율.Text = 켈리지수비율

        txt_F2_켈리지수비율.Refresh()

    End Sub

    Private Sub cmb_selectedJongmokIndex_0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_0.SelectedIndexChanged
        Dim selectedIndex = cmb_selectedJongmokIndex_0.SelectedIndex


        If isRealFlag = True Then
            If selectedIndex > 0 And selectedJongmokIndex(0) <> selectedIndex - 1 Then

                selectedJongmokIndex(0) = selectedIndex - 1
                '여기다가 행사가 추출하는 로직 추가함
                콜선택된행사가(0) = 인덱스로부터행사가찾기(selectedJongmokIndex(0))
                chk_ChangeTargetIndex.Checked = False 'Clac_DisplayAllGrid에서 또 자동으로 selected를 계산하는 걸 방지하기 위해 false로 바꾼다
                F2_Clac_DisplayAllGrid()
                Add_Log("일반", "cmb_selectedJongmokIndex_0_SelectedIndexChanged  호출됨")
            End If

        Else  'DB 데이터를 읽어왔을 때는 간단히 처리한다
            If selectedIndex > 0 And selectedJongmokIndex(0) <> selectedIndex - 1 Then

                selectedJongmokIndex(0) = selectedIndex - 1

                DB에서일분옵션데이터채워넣기(selectedJongmokIndex(0), timeIndex_1Min, 0)

                F2_Clac_DisplayAllGrid()
                Add_Log("일반", "DB데이터 cmb_selectedJongmokIndex_0_SelectedIndexChanged  호출됨")
            End If
        End If

    End Sub

    Private Sub cmb_selectedJongmokIndex_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_1.SelectedIndexChanged

        Dim selectedIndex = cmb_selectedJongmokIndex_1.SelectedIndex

        If isRealFlag = True Then
            If selectedIndex > 0 And selectedJongmokIndex(1) <> selectedIndex - 1 Then

                selectedJongmokIndex(1) = selectedIndex - 1
                '여기다가 행사가 추출하는 로직 추가함

                콜선택된행사가(1) = 인덱스로부터행사가찾기(selectedJongmokIndex(1))
                chk_ChangeTargetIndex.Checked = False
                F2_Clac_DisplayAllGrid()
                Add_Log("일반", "cmb_selectedJongmokIndex_1_SelectedIndexChanged  호출됨")
            End If

        Else

            If selectedIndex > 0 And selectedJongmokIndex(1) <> selectedIndex - 1 Then

                selectedJongmokIndex(1) = selectedIndex - 1

                DB에서일분옵션데이터채워넣기(selectedJongmokIndex(1), timeIndex_1Min, 1)

                F2_Clac_DisplayAllGrid()
                Add_Log("일반", "DB데이터 cmb_selectedJongmokIndex_0_SelectedIndexChanged  호출됨")
            End If

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
            lbl_F2_최종투자금액.Text = Format(최종투자금액, "###,###,###,###,##0")
            'txt_금일투자금_A.Text = Format(최종투자금액, "###,###,###,###,##0")
            'txt_투자금_B.Text = Format(최종투자금액, "###,###,###,###,##0")
            'txt_투자금_D.Text = Format(투자금_D, "###,###,###,###,##0")
        End If
    End Sub

    Private Sub InitFirstGrid()

        grid1.Columns.Clear()
        grid1.Rows.Clear()

        '전체 크기 지정
        grid1.ColumnCount = 7
        grid1.RowCount = TotalCount

        For i As Integer = 0 To 6
            grid1.Columns(i).Width = 67
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

    Private Sub btn_call_매도_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub btn_put_매도_Click(sender As Object, e As EventArgs)


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
                        count = Math.Min(count, 매매1회최대수량)
                    Else
                        count = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                        count = Math.Min(count, 매매1회최대수량)
                    End If
                    If count > 0 Then 한종목매수(종목번호, it.A10_현재가, count, "매도를청산", "03") '호가유형 지정가 00, 시장가 03
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

                    Dim price As Single = it.A10_현재가

                    Dim 계산count As Integer = Val(txt_F2_1회최대매매수량.Text)
                    If price < 0.8 Then
                        계산count = Math.Max(CInt(-250 * price + 250), 계산count)
                    End If

                    매매1회최대수량 = Math.Max(매매1회최대수량, 계산count)

                    Dim count As Integer

                    count = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                    count = Math.Min(count, 매매1회최대수량)
                    한종목매도(종목번호, it.A10_현재가, count, "매수를청산", "03") '호가유형 지정가 00, 시장가 03
                End If

            Next
        End If
    End Sub

    Private Sub btn_call_구매가능수_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btn_put_구매가능수_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btn_F2_전체조건반복_Click(sender As Object, e As EventArgs) Handles btn_F2_전체조건반복.Click
        chk_실거래실행.Checked = False
        Form1.chk_양매도실행.Checked = False
        Form1.chk_중간청산.Checked = False
        당일반복중_flag = True

        '매도조건테스트()

        'fullTest_A()
        'fullTest_B()
        'fullTest_M()
        'fullTest_N()
        'fullTest_N1()

        'fullTest_C()

        'fullTest_C1()

        'fullTest_E()
        'fullTest_E2()
        'fullTest_F()
        'fullTest_G()

        'RSI_Test()

        fullTest_O()

        당일반복중_flag = False
        SoonMesuSimulation_조건 = ""
    End Sub

    '20240211 테스트 결과 'C_TEST_CNT_020_A_0.005_B_58_C_0.013_D_1215
    'M_선물기울기_기준

    Private Sub fullTest_M()

        Dim M_기울기최저기준_temp() As Single = {0.003, 0.005}
        Dim M_장기추세선기준일_temp() As Integer = {60}
        Dim M_기울기최고기준_temp() As Single = {0.015}
        Dim M_마감시간_temp() As Integer = {1500}
        Dim M_선물기울기_기준_temp() As Integer = {1, 3, 5, 7, 9, 11, 13}

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = True
        chk_Algorithm_N.Checked = False
        chk_Algorithm_O.Checked = False

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To M_기울기최저기준_temp.Length - 1
            For b As Integer = 0 To M_장기추세선기준일_temp.Length - 1
                For c As Integer = 0 To M_기울기최고기준_temp.Length - 1
                    For d As Integer = 0 To M_마감시간_temp.Length - 1
                        For e As Integer = 0 To M_선물기울기_기준_temp.Length - 1
                            Dim cntstr As String
                            If cnt < 10 Then
                                cntstr = "00" & cnt.ToString()
                            ElseIf cnt >= 10 And cnt < 100 Then
                                cntstr = "0" & cnt.ToString()
                            Else
                                cntstr = cnt.ToString()
                            End If


                            기울기최저기준 = M_기울기최저기준_temp(a)
                            MA_Interval(2) = M_장기추세선기준일_temp(b)
                            기울기최고기준 = M_기울기최고기준_temp(c)
                            M_마감시간 = M_마감시간_temp(d)
                            M_선물기울기_기준 = M_선물기울기_기준_temp(e)

                            SoonMesuSimulation_조건 = String.Format("C_TEST_CNT_{0}", cntstr)
                            SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}", M_기울기최저기준_temp(a), M_장기추세선기준일_temp(b), M_기울기최고기준_temp(c), M_마감시간_temp(d), M_선물기울기_기준)

                            Console.WriteLine(SoonMesuSimulation_조건)
                            Add_Log("", SoonMesuSimulation_조건)
                            자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                            cnt += 1
                        Next

                    Next

                Next

            Next

        Next

    End Sub

    '20240211 테스트 결과 C_TEST_CNT_012_A_0.005_B_58_C_0.007_D_1230

    Private Sub fullTest_N()
        '231225 이걸로 확정함
        Dim N_기울기최저기준_temp() As Single = {0.002, 0.003}
        Dim N_장기추세선기준일_temp() As Integer = {60}
        Dim N_기울기최고기준_temp() As Single = {0.015, 0.03}
        Dim N_마감시간_temp() As Integer = {1230, 1500}
        Dim N_시작시간_temp() As Integer = {1000}
        Dim M_선물기울기_기준_temp() As Integer = {10, 13}


        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = False
        chk_Algorithm_N.Checked = True
        chk_Algorithm_O.Checked = False


        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To N_기울기최저기준_temp.Length - 1
            For b As Integer = 0 To N_장기추세선기준일_temp.Length - 1
                For c As Integer = 0 To N_기울기최고기준_temp.Length - 1
                    For d As Integer = 0 To N_마감시간_temp.Length - 1
                        For e As Integer = 0 To N_시작시간_temp.Length - 1
                            For f As Integer = 0 To M_선물기울기_기준_temp.Length - 1

                                Dim cntstr As String
                                If cnt < 10 Then
                                    cntstr = "00" & cnt.ToString()
                                ElseIf cnt >= 10 And cnt < 100 Then
                                    cntstr = "0" & cnt.ToString()
                                Else
                                    cntstr = cnt.ToString()
                                End If


                                N_기울기최저기준 = N_기울기최저기준_temp(a)
                                MA_Interval(2) = N_장기추세선기준일_temp(b)
                                N_기울기최고기준 = N_기울기최고기준_temp(c)
                                N_마감시간 = N_마감시간_temp(d)
                                N_시작시간 = N_시작시간_temp(e)
                                M_선물기울기_기준 = M_선물기울기_기준_temp(f)

                                SoonMesuSimulation_조건 = String.Format("N_TEST_CNT_{0}", cntstr)
                                SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}_F_{5}", N_기울기최저기준_temp(a), N_장기추세선기준일_temp(b), N_기울기최고기준_temp(c), N_마감시간_temp(d), N_시작시간_temp(e), M_선물기울기_기준)

                                Console.WriteLine(SoonMesuSimulation_조건)
                                Add_Log("", SoonMesuSimulation_조건)
                                자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                                cnt += 1
                            Next


                        Next
                    Next
                Next
            Next
        Next






    End Sub



    'Public N1_기울기최저기준 As Single = 0.001  '기울기가 일정 기준 이상일때만 사도록 하는 기능임. 참고로 2023년 9월부터 12월까지 평균은 0.01, 최대값은 0.059 였음   - 최소 확인 23.09.03 ! 12.22   - 0,3일 대상


    'Public N1_마감시간 As Integer = 1230
    'Public N1_시작시간 As Integer = 1000

    'Public N1_최저MACD선값기준 As Single = -0.2

    'Public N1_tick_count_기준_선물 = 10
    'Public N1_선물기울기_기준 As Single = 10

    Private Sub fullTest_N1()
        '231225 이걸로 확정함
        Dim N1_최저MACD선값기준_temp() As Single = {-0.2, -0.25, -0.3, -0.4, -0.17, -0.14, -0.11, -0.08}
        Dim N1_tick_count_기준_선물_temp() As Integer = {10, 20, 30, 40}
        Dim N1_선물기울기_기준_temp() As Single = {1, 5, 10, 15, 20, 25}
        Dim N1_마감시간_temp() As Integer = {1500}
        Dim N1_시작시간_temp() As Integer = {920}



        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = False
        chk_Algorithm_N.Checked = False
        chk_Algorithm_O.Checked = False
        chk_Algorithm_N1.Checked = True



        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To N1_최저MACD선값기준_temp.Length - 1
            For b As Integer = 0 To N1_tick_count_기준_선물_temp.Length - 1
                For c As Integer = 0 To N1_선물기울기_기준_temp.Length - 1
                    For d As Integer = 0 To N1_마감시간_temp.Length - 1
                        For e As Integer = 0 To N1_시작시간_temp.Length - 1


                            Dim cntstr As String
                            If cnt < 10 Then
                                cntstr = "00" & cnt.ToString()
                            ElseIf cnt >= 10 And cnt < 100 Then
                                cntstr = "0" & cnt.ToString()
                            Else
                                cntstr = cnt.ToString()
                            End If


                            N1_최저MACD선값기준_선물우선 = N1_최저MACD선값기준_temp(a)
                            N1_tick_count_기준_선물 = N1_tick_count_기준_선물_temp(b)
                            N1_선물기울기_기준_선물우선 = N1_선물기울기_기준_temp(c)
                            N1_마감시간 = N1_마감시간_temp(d)
                            N1_시작시간 = N1_시작시간_temp(e)


                            SoonMesuSimulation_조건 = String.Format("N1_TEST_CNT_{0}", cntstr)
                            SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}", N1_선물기울기_기준_선물우선, N1_tick_count_기준_선물, N1_선물기울기_기준_선물우선, N1_마감시간_temp(d), N1_시작시간_temp(e))

                            Console.WriteLine(SoonMesuSimulation_조건)
                            Add_Log("", SoonMesuSimulation_조건)
                            자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                            cnt += 1
                        Next
                    Next
                Next
            Next
        Next






    End Sub


    Private Sub 매도조건테스트()

        '0일 3일
        Dim 익절차() As String = {"11", "09", "07", "05", "13"} 'L
        Dim 옵션기준손절매() As String = {"-0.23", "-0.20", "-0.26", "-0.28"} 'M
        Dim 중간청산이익목표() As String = {"0.34", "0.38", "0.30"} 'N
        Dim 중간매도후목표이익율_temp() As Single = {0.21, 0.25, 0.28}

        '1,2,6일
        'Dim 익절차() As String = {"11", "10"} 'L
        'Dim 옵션기준손절매() As String = {"-0.20", "-0.18", "-0.16", "-0.14", "-0.22", "-0.12"} 'M
        'Dim 중간청산이익목표() As String = {"0.30", "0.35", "0.20", "0.25"} 'N

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0


        For l As Integer = 0 To 익절차.Length - 1
            For m As Integer = 0 To 옵션기준손절매.Length - 1
                For n As Integer = 0 To 중간청산이익목표.Length - 1
                    For o As Integer = 0 To 중간매도후목표이익율_temp.Length - 1

                        txt_F2_익절차.Text = 익절차(l)
                        txt_F2_옵션가기준손절매.Text = 옵션기준손절매(m)

                        첫번째중간매도이익율 = 중간청산이익목표(n)
                        두번째중간매도이익율 = 첫번째중간매도이익율 + 0.3
                        세번째중간매도이익율 = 두번째중간매도이익율 + 0.3

                        중간매도후이익율차이 = 중간매도후목표이익율_temp(o)


                        txt_F2_익절차.Refresh()
                        txt_F2_옵션가기준손절매.Refresh()

                        Dim cntstr As String
                        If cnt < 10 Then
                            cntstr = "00" & cnt.ToString()
                        ElseIf cnt >= 10 And cnt < 100 Then
                            cntstr = "0" & cnt.ToString()
                        Else
                            cntstr = cnt.ToString()
                        End If

                        SoonMesuSimulation_조건 = String.Format("Sell_CNT_{0}", cntstr)

                        SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_L_{0}_M_{1}_N_{2}_O_{3}", 익절차(l), 옵션기준손절매(m), 중간청산이익목표(n), 중간매도후목표이익율_temp(o))

                        Console.WriteLine(SoonMesuSimulation_조건)
                        Add_Log("", SoonMesuSimulation_조건)
                        자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다

                        cnt += 1
                    Next
                Next
            Next
        Next

    End Sub

    Private Sub fullTest_C1()
        Dim C1_StartTime_temp() As Integer = {90400}               'A
        Dim C1_EndTime_temp() As Integer = {90600}                   'B
        Dim C1_개별금액_temp() As Single = {800, 900, 1000}       'C
        Dim C1_해제기울기_temp() As Integer = {10, 13, 16, 7, 4, 2}
        Dim b_최소유지INDEX_temp() As Integer = {6}

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = True

        For a As Integer = 0 To C1_StartTime_temp.Length - 1
            For b As Integer = 0 To C1_EndTime_temp.Length - 1
                For c As Integer = 0 To C1_개별금액_temp.Length - 1
                    For e As Integer = 0 To C1_해제기울기_temp.Length - 1
                        For f As Integer = 0 To b_최소유지INDEX_temp.Length - 1

                            C1_StartTime = C1_StartTime_temp(a)
                            C1_EndTime = C1_EndTime_temp(b)
                            C1_개별금액 = C1_개별금액_temp(c)
                            C1_해제기울기 = C1_해제기울기_temp(e)
                            신호최소유지시간index = b_최소유지INDEX_temp(f)

                            Dim cntstr As String
                            If cnt < 10 Then
                                cntstr = "00" & cnt.ToString()
                            ElseIf cnt >= 10 And cnt < 100 Then
                                cntstr = "0" & cnt.ToString()
                            Else
                                cntstr = cnt.ToString()
                            End If

                            SoonMesuSimulation_조건 = String.Format("C_TEST_CNT_{0}", cntstr)
                            SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}", C1_StartTime_temp(a), C1_EndTime_temp(b), C1_개별금액_temp(c), C1_해제기울기_temp(e), b_최소유지INDEX_temp(f))

                            Console.WriteLine(SoonMesuSimulation_조건)
                            Add_Log("", SoonMesuSimulation_조건)
                            자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                            cnt += 1
                        Next

                    Next
                Next
            Next
        Next

    End Sub
    Private Sub simpleTest()


    End Sub



    'Public B_StartTime As Integer = 92500
    'Public B_EndTime As Integer = 101900
    'Public B_기준기울기 As Single = 50.0
    'Public B_해제기울기 As Single = 2.0

    Private Sub fullTest_B()
        Dim B_StartIndex_temp() As Integer = {92500}               'A
        Dim B_EndIndex_temp() As Integer = {101900}   'B
        Dim B_기준기울기_temp() As Single = {50, 45, 40, 35, 30, 25}       'C
        Dim B_해제기울기_temp() As Single = {2}        'D
        Dim b_최소유지INDEX_temp() As Integer = {6}
        Dim B_마지막점과그앞점거리최소INDEX_temp() As Integer = {2}
        Dim B_마지막점과그앞점거리최대INDEX_temp() As Integer = {50}

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        chk_Algorithm_B.Checked = True
        chk_Algorithm_C.Checked = False
        chk_Algorithm_G.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False

        For a As Integer = 0 To B_StartIndex_temp.Length - 1
            For b As Integer = 0 To B_EndIndex_temp.Length - 1
                For c As Integer = 0 To B_기준기울기_temp.Length - 1
                    For d As Integer = 0 To B_해제기울기_temp.Length - 1
                        For e As Integer = 0 To b_최소유지INDEX_temp.Length - 1
                            For f As Integer = 0 To B_마지막점과그앞점거리최소INDEX_temp.Length - 1
                                For g As Integer = 0 To B_마지막점과그앞점거리최대INDEX_temp.Length - 1
                                    B_StartTime = B_StartIndex_temp(a)
                                    B_EndTime = B_EndIndex_temp(b)
                                    B_기준기울기 = B_기준기울기_temp(c)
                                    B_해제기울기 = B_해제기울기_temp(d)
                                    신호최소유지시간index = b_최소유지INDEX_temp(e)
                                    B_마지막점과그앞점거리최소INDEX = B_마지막점과그앞점거리최소INDEX_temp(f)
                                    B_마지막점과그앞점거리최대INDEX = B_마지막점과그앞점거리최대INDEX_temp(g)

                                    Dim cntstr As String
                                    If cnt < 10 Then
                                        cntstr = "00" & cnt.ToString()
                                    ElseIf cnt >= 10 And cnt < 100 Then
                                        cntstr = "0" & cnt.ToString()
                                    Else
                                        cntstr = cnt.ToString()
                                    End If

                                    SoonMesuSimulation_조건 = String.Format("CNT_{0}", cntstr)
                                    SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}_F_{5}_G_{6}", B_StartIndex_temp(a), B_EndIndex_temp(b), B_기준기울기_temp(c), B_해제기울기_temp(d), b_최소유지INDEX_temp(e), B_마지막점과그앞점거리최소INDEX_temp(f), B_마지막점과그앞점거리최대INDEX_temp(g))

                                    Console.WriteLine(SoonMesuSimulation_조건)
                                    Add_Log("", SoonMesuSimulation_조건)
                                    자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                                    cnt += 1

                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next

    End Sub

    'Public C_StartTime As Integer = 90200
    'Public C_EndTime As Integer = 90500
    'Public C_개별금액 As Integer = 500
    'Public C_합계금액 As Integer = 1000
    'Public C_해제기울기 As Integer = 20
    'Dim b_최소유지INDEX_temp() As Integer = {4, 6, 8}

    Private Sub fullTest_C()
        Dim C_StartTime_temp() As Integer = {90400, 90300}               'A
        Dim C_EndTime_temp() As Integer = {90500, 90600, 97000}                   'B
        Dim C_개별금액_temp() As Single = {50, 100, 150, 200}       'C
        Dim C_합계금액_temp() As Single = {700, 800, 900}        'D
        Dim C_해제기울기_temp() As Integer = {10, 5}
        Dim b_최소유지INDEX_temp() As Integer = {3}

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To C_StartTime_temp.Length - 1
            For b As Integer = 0 To C_EndTime_temp.Length - 1
                For c As Integer = 0 To C_개별금액_temp.Length - 1
                    For d As Integer = 0 To C_합계금액_temp.Length - 1
                        For e As Integer = 0 To C_해제기울기_temp.Length - 1
                            For f As Integer = 0 To b_최소유지INDEX_temp.Length - 1

                                C_StartTime = C_StartTime_temp(a)
                                C_EndTime = C_EndTime_temp(b)
                                C_개별금액 = C_개별금액_temp(c)
                                C_합계금액 = C_합계금액_temp(d)
                                C_해제기울기 = C_해제기울기_temp(e)
                                신호최소유지시간index = b_최소유지INDEX_temp(f)

                                Dim cntstr As String
                                If cnt < 10 Then
                                    cntstr = "00" & cnt.ToString()
                                ElseIf cnt >= 10 And cnt < 100 Then
                                    cntstr = "0" & cnt.ToString()
                                Else
                                    cntstr = cnt.ToString()
                                End If

                                SoonMesuSimulation_조건 = String.Format("C_TEST_CNT_{0}", cntstr)
                                SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}_F_{5}", C_StartTime_temp(a), C_EndTime_temp(b), C_개별금액_temp(c), C_합계금액_temp(d), C_해제기울기_temp(e), b_최소유지INDEX_temp(f))

                                Console.WriteLine(SoonMesuSimulation_조건)
                                Add_Log("", SoonMesuSimulation_조건)
                                자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                                cnt += 1
                            Next

                        Next
                    Next
                Next
            Next
        Next

    End Sub

    'Public 이동평균선_기준일자 As Integer = 50       '이동평균선 갯수 기준
    'Public X_계산기준봉비율 As Single = 0.55         '장대양봉의 크기를 계산하는 기준으로 X / 이동평균선_기준일자 비율을 의미함
    'Public Y_장대양봉기준비율 As Single = 0.6      'X_계산기준봉비율내의 캔들들의 최대최소값의 차에 비해 어느정도인지에 대한 비율

    Private Sub fullTest_D()
        Dim 이동평균선_기준일자_temp() As Integer = {50}               'A
        Dim X_계산기준봉비율_temp() As Single = {0.59, 0.62}
        Dim Y_장대양봉기준비율_temp() As Single = {0.56, 0.6, 0.64}
        Dim D_MAX이익율상한_temp() As Single = {0.33}
        Dim D_반대편음봉_양봉대비비율_temp() As Single = {0.2, 0.25, 0.3, 0.4, 0.35, 0.45}


        'Dim D신호_유지_IndexCount_temp() As Integer = {6, 10, 20, 30, 40}
        'Dim D신호_유지_비율_temp() As Single = {1.0, 0.9, 1.1, 1.2}

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = True
        chk_Algorithm_E.Checked = False
        chk_Algorithm_F.Checked = False
        chk_Algorithm_G.Checked = False


        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To 이동평균선_기준일자_temp.Length - 1
            For b As Integer = 0 To X_계산기준봉비율_temp.Length - 1
                For c As Integer = 0 To Y_장대양봉기준비율_temp.Length - 1
                    For d As Integer = 0 To D_MAX이익율상한_temp.Length - 1
                        For e As Integer = 0 To D_반대편음봉_양봉대비비율_temp.Length - 1

                            이동평균선_기준일자 = 이동평균선_기준일자_temp(a)
                            X_계산기준봉비율 = X_계산기준봉비율_temp(b)
                            Y_장대양봉기준비율 = Y_장대양봉기준비율_temp(c)
                            D_MAX이익율상한 = D_MAX이익율상한_temp(d)
                            D_반대편음봉_양봉대비비율 = D_반대편음봉_양봉대비비율_temp(e)

                            Dim cntstr As String
                            If cnt < 10 Then
                                cntstr = "00" & cnt.ToString()
                            ElseIf cnt >= 10 And cnt < 100 Then
                                cntstr = "0" & cnt.ToString()
                            Else
                                cntstr = cnt.ToString()
                            End If

                            SoonMesuSimulation_조건 = String.Format("CNT_{0}", cntstr)
                            SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}", 이동평균선_기준일자_temp(a), X_계산기준봉비율_temp(b), Y_장대양봉기준비율_temp(c), D_MAX이익율상한, D_반대편음봉_양봉대비비율)


                            Console.WriteLine(SoonMesuSimulation_조건)
                            Add_Log("", SoonMesuSimulation_조건)
                            자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                            cnt += 1

                        Next
                    Next
                Next
            Next
        Next

    End Sub

    '231228 9월이후 데이터로 시험한 결과     'CNT_004_A_4_B_7_C_2_D_120_E_105000_F_150000_G_4_H_1_I_65
    '240211 9월이하 테이터로 시험한 결과     'CNT_001_A_4_B_7_C_2_D_120_E_105000_F_150000_G_4_H_1_I_58_J_2.5
    Private Sub fullTest_E()
        Dim 최대포인트수() As String = {"4", "5"}               'A
        Dim E_신호발생기준기울기_temp() As Single = {7.0, 8.0, 9.0, 10.0}    'B
        Dim E_신호해제기준기울기_temp() As Single = {2.0}    'C
        Dim PIP_CALC_MAX_INDEX() As String = {"120"}        'D
        Dim 매수시작시간() As String = {"105000"}           'E
        Dim 매수마감시간() As String = {"150000"}           'F
        Dim 신호최소유지시간() As Integer = {4}             'G
        Dim E_DataSource_temp() As Integer = {1}  '외국인 + 기관, 1:외국인, 2: 기관  --------- 기관은 켈리지수가 항상 -로 나와서 완전히 제외함
        Dim 장기이평선_temp() As Integer = {58, 65, 72}
        Dim 기관반대순매수_허용크기비율() As Single = {1.5, 2.5, 3.5}

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = True
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = True
        chk_Algorithm_N.Checked = True


        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To 최대포인트수.Length - 1
            For b As Integer = 0 To E_신호발생기준기울기_temp.Length - 1
                For c As Integer = 0 To E_신호해제기준기울기_temp.Length - 1
                    For d As Integer = 0 To PIP_CALC_MAX_INDEX.Length - 1
                        For ee As Integer = 0 To 매수시작시간.Length - 1
                            For f As Integer = 0 To 매수마감시간.Length - 1
                                For g As Integer = 0 To 신호최소유지시간.Length - 1
                                    For h As Integer = 0 To E_DataSource_temp.Length - 1
                                        For i As Integer = 0 To 장기이평선_temp.Length - 1
                                            For j As Integer = 0 To 기관반대순매수_허용크기비율.Length - 1


                                                txt_F2_최대포인트수.Text = 최대포인트수(a)
                                                E_신호발생기준기울기 = E_신호발생기준기울기_temp(b)
                                                E_신호해제기준기울기 = E_신호해제기준기울기_temp(c)
                                                txt_F2_PIP_CALC_MAX_INDEX.Text = PIP_CALC_MAX_INDEX(d)
                                                txt_F2_매수시작시간.Text = 매수시작시간(ee)
                                                txt_F2_매수마감시간.Text = 매수마감시간(f)
                                                신호최소유지시간index = 신호최소유지시간(g)
                                                E_DataSource = E_DataSource_temp(h)
                                                MA_Interval(2) = 장기이평선_temp(i)
                                                E_기관반대순매수_허용크기비율 = 기관반대순매수_허용크기비율(j)

                                                txt_F2_최대포인트수.Refresh()
                                                txt_F2_PIP_CALC_MAX_INDEX.Refresh()
                                                txt_F2_매수시작시간.Refresh()
                                                txt_F2_매수마감시간.Refresh()


                                                Dim cntstr As String
                                                If cnt < 10 Then
                                                    cntstr = "00" & cnt.ToString()
                                                ElseIf cnt >= 10 And cnt < 100 Then
                                                    cntstr = "0" & cnt.ToString()
                                                Else
                                                    cntstr = cnt.ToString()
                                                End If

                                                SoonMesuSimulation_조건 = String.Format("CNT_{0}", cntstr)
                                                SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}_F_{5}_G_{6}_H_{7}_I_{8}_J_{9}", 최대포인트수(a), E_신호발생기준기울기_temp(b), E_신호해제기준기울기_temp(c), PIP_CALC_MAX_INDEX(d), 매수시작시간(ee), 매수마감시간(f), 신호최소유지시간(g), E_DataSource_temp(h), 장기이평선_temp(i), 기관반대순매수_허용크기비율(j))

                                                Add_Log("", SoonMesuSimulation_조건)
                                                자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다

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

    End Sub

    '20240212 E알고리즘 시험 결과 B240212_E202     E2_CNT_001_A_6_B_2_C_105000_D_150000_E_4_F_2.5_G_30 20승 11패 켈리지수 47.94

    Private Sub fullTest_E2()

        Dim E_신호발생기준기울기_temp() As Single = {6.0, 6.5, 5.5}    'A
        Dim E_신호해제기준기울기_temp() As Single = {2.0}    'B
        Dim 매수시작시간() As String = {"105000", "103000", "101000", "95000", "93000", "110000"}           'C
        Dim 매수마감시간() As String = {"150000"}           'D
        Dim 신호최소유지시간() As Integer = {4}             'E
        Dim 기관반대순매수_허용크기비율() As Single = {2.5} 'F
        Dim E2_tick_count_기준_temp() As Integer = {32, 30, 28}


        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = False
        chk_Algorithm_N.Checked = False
        chk_Algorithm_E2.Checked = True

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0


        For a As Integer = 0 To E_신호발생기준기울기_temp.Length - 1
            For b As Integer = 0 To E_신호해제기준기울기_temp.Length - 1
                For c As Integer = 0 To 매수시작시간.Length - 1
                    For d As Integer = 0 To 매수마감시간.Length - 1
                        For e As Integer = 0 To 신호최소유지시간.Length - 1
                            For f As Integer = 0 To 기관반대순매수_허용크기비율.Length - 1
                                For g As Integer = 0 To E2_tick_count_기준_temp.Length - 1

                                    E_신호발생기준기울기 = E_신호발생기준기울기_temp(a)
                                    E_신호해제기준기울기 = E_신호해제기준기울기_temp(b)
                                    txt_F2_매수시작시간.Text = 매수시작시간(c)
                                    txt_F2_매수마감시간.Text = 매수마감시간(d)
                                    신호최소유지시간index = 신호최소유지시간(e)
                                    E_기관반대순매수_허용크기비율 = 기관반대순매수_허용크기비율(f)
                                    E2_tick_count_기준 = E2_tick_count_기준_temp(g)

                                    txt_F2_최대포인트수.Refresh()
                                    txt_F2_PIP_CALC_MAX_INDEX.Refresh()
                                    txt_F2_매수시작시간.Refresh()
                                    txt_F2_매수마감시간.Refresh()


                                    Dim cntstr As String
                                    If cnt < 10 Then
                                        cntstr = "00" & cnt.ToString()
                                    ElseIf cnt >= 10 And cnt < 100 Then
                                        cntstr = "0" & cnt.ToString()
                                    Else
                                        cntstr = cnt.ToString()
                                    End If

                                    SoonMesuSimulation_조건 = String.Format("E2_CNT_{0}", cntstr)
                                    SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}_F_{5}_G_{6}", E_신호발생기준기울기_temp(a), E_신호해제기준기울기_temp(b), 매수시작시간(c), 매수마감시간(d), 신호최소유지시간(e), 기관반대순매수_허용크기비율(f), E2_tick_count_기준_temp(g))

                                    Add_Log("", SoonMesuSimulation_조건)
                                    자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다

                                    cnt += 1
                                Next
                            Next
                        Next
                    Next
                Next
            Next
        Next

    End Sub

    Private Sub fullTest_F()


        '20230820 최종 B230820_T007
        'CNT_000_A_22_B_75_C_1.2_D_1.05_E_1.3_F_0.9_G_1.2_H_0.95_I_50_J_1.18

        Dim F_PIP_최소카운트_temp() As Integer = {22}   '18  - 선이 6개가 각 3개로 구성됨  --------------------------------------- 변수
        Dim F_PIP_최대카운트_temp() As Integer = {75}    'PIP 계산하는 최종 길이          --------------------------------------- 변수
        Dim F_기본계곡최소깊이_temp() As Single = {1.27}  '헤드앤숄더에서 높은점과 낮은점의 기본 높이 차  ------------------------- 변수  ------------ 이걸 줄이면서 3일이나 6일 어찌되는지 봐야 함
        Dim F_현재점의최소높이_temp() As Single = {1.06}  '헤드앤숄더에서 6번째점에서 7번째점의 최소 높이 차  --------------------- 변수
        Dim F_현재점의최대높이_temp() As Single = {1.3}  '헤드앤숄더에서 6번째점에서 7번째점의 최대 높이 차  --------------------- 변수


        Dim F_손절배율_temp() As Single = {0.95}          'option_son 나는걸 방지하기 위해 최저점을 하향 돌파할 때 손절한다 ----- 변수
        Dim F_좌우골짜기깊이상한_temp() As Single = {1.25}  '중앙골짜기 대비 좌우 골짜기의 깊이 차 상한 --------------------------- 변수
        Dim F_좌골짜기깊이하한_temp() As Single = {0.92}  '중앙골짜기 대비 좌우 골짜기의 깊이 차 하한 --------------------------- 변수

        Dim F_최저점근접기간Index_temp() As Integer = {50}  '몇가지를 보니 최저점에 가까울 때 트리플바닥으로 상승하는 경우가 많음. 이를 위해 최저점 근처인지를 확인하는데 PIP 시작점으로부터 이전 index count 변수 
        Dim F_최저점대비높은비율_temp() As Single = {1.1}  '최근 최저점 대비 PIP의 최저점이 높은 허용 정도

        Dim F_우골짜기깊이하한_temp() As Single = {0.95}


        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_F.Checked = True
        chk_Algorithm_G.Checked = False


        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To F_PIP_최소카운트_temp.Length - 1
            For b As Integer = 0 To F_PIP_최대카운트_temp.Length - 1
                For c As Integer = 0 To F_기본계곡최소깊이_temp.Length - 1
                    For d As Integer = 0 To F_현재점의최소높이_temp.Length - 1
                        For e As Integer = 0 To F_현재점의최대높이_temp.Length - 1
                            For f As Integer = 0 To F_손절배율_temp.Length - 1
                                For g As Integer = 0 To F_좌우골짜기깊이상한_temp.Length - 1
                                    For h As Integer = 0 To F_좌골짜기깊이하한_temp.Length - 1
                                        For i As Integer = 0 To F_최저점근접기간Index_temp.Length - 1
                                            For j As Integer = 0 To F_최저점대비높은비율_temp.Length - 1
                                                For k As Integer = 0 To F_우골짜기깊이하한_temp.Length - 1
                                                    F_PIP_최소카운트 = F_PIP_최소카운트_temp(a)
                                                    F_PIP_최대카운트 = F_PIP_최대카운트_temp(b)
                                                    F_기본계곡최소깊이 = F_기본계곡최소깊이_temp(c)
                                                    F_현재점의최소높이 = F_현재점의최소높이_temp(d)
                                                    F_현재점의최대높이 = F_현재점의최대높이_temp(e)
                                                    F_손절배율 = F_손절배율_temp(f)
                                                    F_좌우골짜기깊이상한 = F_좌우골짜기깊이상한_temp(g)
                                                    F_좌골짜기깊이하한 = F_좌골짜기깊이하한_temp(h)
                                                    F_최저점근접기간Index = F_최저점근접기간Index_temp(i)
                                                    F_최저점대비높은비율 = F_최저점대비높은비율_temp(j)
                                                    F_우골짜기깊이하한 = F_우골짜기깊이하한_temp(k)

                                                    Dim cntstr As String
                                                    If cnt < 10 Then
                                                        cntstr = "00" & cnt.ToString()
                                                    ElseIf cnt >= 10 And cnt < 100 Then
                                                        cntstr = "0" & cnt.ToString()
                                                    Else
                                                        cntstr = cnt.ToString()
                                                    End If

                                                    SoonMesuSimulation_조건 = String.Format("CNT_{0}", cntstr)
                                                    SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}", F_PIP_최소카운트, F_PIP_최대카운트, F_기본계곡최소깊이, F_현재점의최소높이, F_현재점의최대높이)
                                                    SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_F_{0}_G_{1}_H_{2}_I_{3}_J_{4}_K_{5}", F_손절배율, F_좌우골짜기깊이상한, F_좌골짜기깊이하한, F_최저점근접기간Index, F_최저점대비높은비율, F_우골짜기깊이하한)

                                                    Console.WriteLine(SoonMesuSimulation_조건)
                                                    Add_Log("", SoonMesuSimulation_조건)
                                                    자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
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




    End Sub


    'G 알고리즘 - 연속 음봉 후 양봉 출현
    'Public G_첫번째시작시간 As Integer = 950   '시작시간
    'Public G_첫번째종료시간 As Integer = 1500   '종료시간
    'Public G_최대유지기간Index As Integer = 40    '20분이 지나면 무조건 매도  - 순매수 인덱스가 기준이라서 2배가 된다
    'Public G_RSI최저기준 As Single = 0.25           'RSI가 해당 기준 이하이면
    'Public G_양봉막대크기 As Single = 1.04      '직전 틱의 양봉 크기

    'RSI 기준을 하회 + 이전봉이 양봉일 때 매수하는 로직이다


    '202402 test 결과 :  B240211_T002 --------- C_TEST_CNT_019_A_1430_B_1500_C_60_D_0.25_E_1.06     5승2패로 아직 데이터가 작음
    '14시 30분 이후에 유효한 결과 도출함

    Private Sub fullTest_G()

        Dim G_첫번째시작시간_temp() As Integer = {1430}
        Dim G_첫번째종료시간_temp() As Integer = {1500}
        Dim G_최대유지기간Index_temp() As Integer = {40, 60, 80, 100}
        Dim G_RSI최저기준_temp() As Single = {0.27, 0.25, 0.23, 0.21, 0.19}
        Dim G_양봉막대크기_temp() As Single = {1.04, 1.06, 1.08}

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = True
        chk_Algorithm_M.Checked = False
        chk_Algorithm_N.Checked = False

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To G_첫번째시작시간_temp.Length - 1
            For b As Integer = 0 To G_첫번째종료시간_temp.Length - 1
                For c As Integer = 0 To G_최대유지기간Index_temp.Length - 1
                    For d As Integer = 0 To G_RSI최저기준_temp.Length - 1
                        For e As Integer = 0 To G_양봉막대크기_temp.Length - 1

                            Dim cntstr As String
                            If cnt < 10 Then
                                cntstr = "00" & cnt.ToString()
                            ElseIf cnt >= 10 And cnt < 100 Then
                                cntstr = "0" & cnt.ToString()
                            Else
                                cntstr = cnt.ToString()
                            End If


                            G_첫번째시작시간 = G_첫번째시작시간_temp(a)
                            G_첫번째종료시간 = G_첫번째종료시간_temp(b)
                            G_최대유지기간Index = G_최대유지기간Index_temp(c)
                            G_RSI최저기준 = G_RSI최저기준_temp(d)
                            G_양봉막대크기 = G_양봉막대크기_temp(e)

                            SoonMesuSimulation_조건 = String.Format("C_TEST_CNT_{0}", cntstr)
                            SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}", G_첫번째시작시간, G_첫번째종료시간, G_최대유지기간Index, G_RSI최저기준, G_양봉막대크기)

                            Console.WriteLine(SoonMesuSimulation_조건)
                            Add_Log("", SoonMesuSimulation_조건)
                            자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다
                            cnt += 1
                        Next

                    Next

                Next

            Next

        Next

    End Sub

    '삼위일체 - 외국인선물, 외국인현물, 이평선 위 3개가 맞을때만 매수하는 로직

    'Public O_선물발생기준기울기 As Single = 16.0
    'Public O_외국인현물발생기준기울기 As Single = 5.0

    'Public O_선물해제기준기울기 As Single = 2.0
    'Public O_외국인현물해제기준기울기 As Single = 2.0

    'Public O_tick_count_기준 As Integer = 40
    'Public O_해제tick_count_기준 As Integer = 25


    'Public O_시작시간 As Integer = 100000
    'Public O_마감시간 As Integer = 150000

    'Public 선물상관계수최저 As Double = 0.3
    'Public 외국인현물상관계수최저 As Double = 0.3
    'Public 상관계수계산인덱스길이 As Integer = 60

    Private Sub fullTest_O()

        Dim O_선물발생기준기울기_temp() As Single = {14, 12} ', 14, 16}    'A
        Dim O_외국인현물발생기준기울기_temp() As Single = {3, 5} ', 4, 5}    'B

        Dim O_선물해제기준기울기_temp() As Single = {2.0}    'A
        Dim O_외국인현물해제기준기울기_temp() As Single = {2.0}    'B

        Dim O_시작시간_temp() As String = {"100000"} ', "94000", "100000", "103000", "110000"}           'C
        Dim O_마감시간_temp() As String = {"150000"}           'D


        Dim O_tick_count_기준_temp() As Integer = {40} ', 36, 40}
        Dim O_해제tick_count_기준_temp() As Integer = {25}

        Dim 선물상관계수최저_temp() As Single = {0.5, 0.6, 0.7}
        Dim 외국인현물상관계수최저_temp() As Single = {0.5, 0.6, 0.7}
        Dim 상관계수계산인덱스길이_temp() As Integer = {80}

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = False
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = False
        chk_Algorithm_N.Checked = False
        chk_Algorithm_E2.Checked = False
        chk_Algorithm_N1.Checked = False
        chk_Algorithm_O.Checked = True

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0


        For a As Integer = 0 To O_선물발생기준기울기_temp.Length - 1
            For b As Integer = 0 To O_외국인현물발생기준기울기_temp.Length - 1
                For c As Integer = 0 To O_선물해제기준기울기_temp.Length - 1
                    For d As Integer = 0 To O_외국인현물해제기준기울기_temp.Length - 1
                        For e As Integer = 0 To O_시작시간_temp.Length - 1
                            For f As Integer = 0 To O_마감시간_temp.Length - 1
                                For g As Integer = 0 To O_tick_count_기준_temp.Length - 1
                                    For h As Integer = 0 To O_해제tick_count_기준_temp.Length - 1
                                        For i As Integer = 0 To 선물상관계수최저_temp.Length - 1
                                            For j As Integer = 0 To 외국인현물상관계수최저_temp.Length - 1
                                                For k As Integer = 0 To 상관계수계산인덱스길이_temp.Length - 1
                                                    O_선물발생기준기울기 = O_선물발생기준기울기_temp(a)
                                                    O_외국인현물발생기준기울기 = O_외국인현물발생기준기울기_temp(b)
                                                    O_선물해제기준기울기 = O_선물해제기준기울기_temp(c)
                                                    O_외국인현물해제기준기울기 = O_외국인현물해제기준기울기_temp(d)
                                                    O_시작시간 = O_시작시간_temp(e)
                                                    O_마감시간 = O_마감시간_temp(f)
                                                    O_tick_count_기준 = O_tick_count_기준_temp(g)
                                                    O_해제tick_count_기준 = O_해제tick_count_기준_temp(h)
                                                    선물상관계수최저 = 선물상관계수최저_temp(i)
                                                    외국인현물상관계수최저 = 외국인현물상관계수최저_temp(j)
                                                    상관계수계산인덱스길이 = 상관계수계산인덱스길이_temp(k)

                                                    Dim cntstr As String
                                                    If cnt < 10 Then
                                                        cntstr = "00" & cnt.ToString()
                                                    ElseIf cnt >= 10 And cnt < 100 Then
                                                        cntstr = "0" & cnt.ToString()
                                                    Else
                                                        cntstr = cnt.ToString()
                                                    End If

                                                    SoonMesuSimulation_조건 = String.Format("O_CNT_{0}", cntstr)
                                                    SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}_D_{3}_E_{4}_F_{5}_G_{6}_H_{7}_I_{8}_J_{9}_K_{10}", O_선물발생기준기울기, O_외국인현물발생기준기울기, O_선물해제기준기울기, O_외국인현물해제기준기울기, O_시작시간, O_마감시간, O_tick_count_기준, O_해제tick_count_기준, Math.Round(선물상관계수최저, 1), Math.Round(외국인현물상관계수최저, 1), 상관계수계산인덱스길이)

                                                    Add_Log("", SoonMesuSimulation_조건)
                                                    자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다

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

    End Sub

    '    Public RSI_기준일 As Integer = 14
    '   Public RSI_과열기준 As Single = 0.8
    '   Public RSI_익절기준 As Single = 0.5  '이정도 수익 이상일때만 RSI로 익절을 한다

    Private Sub RSI_Test()

        Dim RSI_기준일_temp() As String = {23, 25, 26}                  'A
        Dim RSI_과열기준_temp() As Single = {0.78, 0.8, 0.82}                      'B
        Dim RSI_익절기준_temp() As Single = {0.6, 0.75, 0.9, 1.0}         'C

        chk_Algorithm_A.Checked = False
        chk_Algorithm_B.Checked = False
        chk_Algorithm_C.Checked = False
        chk_Algorithm_D.Checked = False
        chk_Algorithm_E.Checked = True
        chk_Algorithm_G.Checked = False
        chk_Algorithm_M.Checked = True
        chk_Algorithm_N.Checked = True

        If SoonMesuSimulationTotalShinhoList Is Nothing Then
            SoonMesuSimulationTotalShinhoList = New List(Of 순매수신호_탬플릿)
        Else
            SoonMesuSimulationTotalShinhoList.Clear()
        End If

        Dim cnt As Integer = 0

        For a As Integer = 0 To RSI_기준일_temp.Length - 1
            For b As Integer = 0 To RSI_과열기준_temp.Length - 1
                For c As Integer = 0 To RSI_익절기준_temp.Length - 1

                    RSI_기준일 = RSI_기준일_temp(a)
                    RSI_과열기준 = RSI_과열기준_temp(b)
                    RSI_익절기준 = RSI_익절기준_temp(c)

                    Dim cntstr As String
                    If cnt < 10 Then
                        cntstr = "00" & cnt.ToString()
                    ElseIf cnt >= 10 And cnt < 100 Then
                        cntstr = "0" & cnt.ToString()
                    Else
                        cntstr = cnt.ToString()
                    End If

                    SoonMesuSimulation_조건 = String.Format("CNT_{0}", cntstr)
                    SoonMesuSimulation_조건 = SoonMesuSimulation_조건 + String.Format("_A_{0}_B_{1}_C_{2}", RSI_기준일, RSI_과열기준, RSI_익절기준)

                    Add_Log("", SoonMesuSimulation_조건)
                    자동반복계산로직(cnt, False) '이걸 true로 하면 남은일자별로 조건을 맞추면서 시험한다

                    cnt += 1
                Next

            Next
        Next
    End Sub


    Public Sub Timer_Change()
        If btn_TimerStart.Text = "START" Then
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

    End Sub

    Private Sub 일일조건설정(ByVal strToday As String)

        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)
        sMonth = 월물
        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)

        월목에따른텍스트입력하기(남은날짜) 'txt_월물과 txt_weekly_정규 텍스트박스에 값을 입력한다
        손절매수준설정(남은날짜)
    End Sub




    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        매매신호처리함수()





    End Sub

    Private Sub rdo_목요일_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_목요일.CheckedChanged

        Dim strToday As String = Format(Today, "yyMMdd")
        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)
        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)
        월목에따른텍스트입력하기(남은날짜) 'txt_월물과 txt_weekly_정규 텍스트박스에 값을 입력한다

    End Sub

    Private Sub rdo_월요일_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_월요일.CheckedChanged

        Dim strToday As String = Format(Today, "yyMMdd")
        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)

        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)
        월목에따른텍스트입력하기(남은날짜) 'txt_월물과 txt_weekly_정규 텍스트박스에 값을 입력한다

    End Sub

    Private Sub btn_신호를저장_Click(sender As Object, e As EventArgs) Handles btn_신호를저장.Click
        InsertRealShinhoList()
    End Sub


End Class