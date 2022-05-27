Option Explicit On

Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form1

    Private Sub btn_RealTimeStart_Click(sender As Object, e As EventArgs) Handles btn_RealTimeStart.Click
        Dim ret As Boolean

        ret = realTime_Start()

        txt_TargetDate.Text = TargetDate

    End Sub

    Private Function realTime_Start() As Boolean

        InitDataStructure()
        InitObject()

        UIVisible(False)

        Dim ConnectionState = FindTargetDate() '현재 사이보스에 접속된 상태라면

        isRealFlag = True '실시간 로직임을 기억한다

        If ConnectionState = True Then

            TotalCount = GetTotalJongmokCount()

            Do While TotalCount > 28                  '15초당 60개 TR 제한을 고려하여 최대 각 28개만 받아온다
                UpperLimit = UpperLimit - 0.15
                LowerLimt = LowerLimt + 0.05

                TotalCount = GetTotalJongmokCount()
            Loop

            Dim tempMonth As Integer
            tempMonth = TargetDate Mod 20000000
            tempMonth = tempMonth / 100


            If TotalCount > 0 And (sMonth - tempMonth) <= 1 Then

                SetTimeDataForData(Data) '미리 data구조체에 시간을 다 입력해 놓는다. 카운트만큼

                GetAllData() '대신으로부터 Data 가져오기
                Clac_DisplayAllGrid()


                '신호 만들고 해제 판단하기
                Dim 신호발생flag As Boolean = CalcAlrotithmAll()
                If 신호발생flag = True Then
                    chk_ChangeTargetIndex.Checked = False '양매도 당시의 기준종목이 변경되지 않도록 고정한다
                End If


                RedrawAll() 'Grid 그리기
                'DrawGraph() '그래프 그리기
                DrawScrollData() 'Scroll 및 기타 DB 관련 UI 표시하기

            Else
                MsgBox("가져올 수 있는 종목이 없습니다")
            End If
        Else
            MsgBox("사이보스에 연결되지 않았습니다")
        End If

        UIVisible(True)

        Return False

    End Function

    Public Sub DrawScrollData()

        If DBTotalDateCount > 1 Then

            lbl_DBDateInfo.Text = "총 " + DBTotalDateCount.ToString() + "일 중 " + (gTargetDateIndex + 1).ToString() + " 번째(" + DBDateList(gTargetDateIndex).ToString() + ")"

        End If

    End Sub



    Private Sub Clac_DisplayAllGrid()         'selectedJongmokIndex 계산

        If selectedJongmokIndex(0) < 0 Or chk_ChangeTargetIndex.Checked = True Then '아직 한번도 선택하지 않았거나 Checked가 True일 때만 자동으로 변경함
            selectedJongmokIndex(0) = CalcTargetJonhmokIndex(0)
            selectedJongmokIndex(1) = CalcTargetJonhmokIndex(1)
        End If

        CalcSumPrice() '콜풋 시가종가의 합계를 구한다

        '최대최소,제2저가 계산
        CalcColorData()

    End Sub

    Private Sub RedrawAll()

        Dim tempIndex As Integer

        If currentIndex > 0 Then

            UIVisible(False)

            InitFirstGrid()
            DrawGrid1Data()

            'grd_selected 조절하기
            'combo에 전체 종목을 Add한다 인덱스, 행사가, 현재가격
            cmb_selectedJongmokIndex_0.Items.Clear()
            cmb_selectedJongmokIndex_1.Items.Clear()

            tempIndex = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다

            cmb_selectedJongmokIndex_0.Items.Add(" ") '0번이 선택되는게 초기화인지 명시적으로 0번을 선택했는지를 확인하기 위해서 제일 앞에 널값을 하나 넣는다
            cmb_selectedJongmokIndex_1.Items.Add(" ")
            For i As Integer = 0 To TotalCount - 1
                Dim str As String
                str = i.ToString() & ". 행사가 : " & Data(i).HangSaGa & " (" & Data(i).price(0, tempIndex, 3).ToString() & ")"
                cmb_selectedJongmokIndex_0.Items.Add(str)
                str = i.ToString() & ". 행사가 : " & Data(i).HangSaGa & " (" & Data(i).price(1, tempIndex, 3).ToString() & ")"
                cmb_selectedJongmokIndex_1.Items.Add(str)
            Next

            cmb_selectedJongmokIndex_1.SelectedIndex = selectedJongmokIndex(1) + 1
            cmb_selectedJongmokIndex_0.SelectedIndex = selectedJongmokIndex(0) + 1

            InitDrawSelectedGird()
            DrawSelectedData()
            DrawShinhoGridData() '신호를 추가한다

            '색깔 실제로 grid에 입히기
            DrawColorAll()
            DrawColor_Selected()

            '오늘날짜를 DBDate 텍스트박스에 넣기
            txt_DBDate.Text = TargetDate

            UIVisible(True)
            grid1.Enabled = True
        End If

    End Sub

    '차트 관련 Reference
    'https://msdn.microsoft.com/en-us/library/dd456671.aspx
    'https://m.blog.naver.com/kimmingul/221877447894  여기에 속성들이 잘 정리되어 있음

    'X축 입력을 시간으로 바꾼다
    'Annotation을 추가한다

    Private Sub DrawWinFormGraph()
        Dim i, callput, tempindex, retindex As Integer '

        If currentIndex > 0 Then

            For i = 0 To Chart1.Series.Count - 1
                Chart1.Series(i).Points.Clear()
            Next

            tempindex = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다

            For callput = 0 To 1

                Dim CandlestrickSeries As String = "CandleStick_" + callput.ToString()
                Dim UpperSeries As String = "Upper_" + callput.ToString()
                Dim LowerSeries As String = "Lower_" + callput.ToString()
                For i = 0 To tempindex

                    '                main Series 입력
                    retindex = Chart1.Series(CandlestrickSeries).Points.AddXY(i, Data(selectedJongmokIndex(callput)).price(callput, i, 1)) '고가를 처음 넣는다
                    Chart1.Series(CandlestrickSeries).Points(retindex).YValues(1) = Data(selectedJongmokIndex(callput)).price(callput, i, 2) '저가
                    Chart1.Series(CandlestrickSeries).Points(retindex).YValues(2) = Data(selectedJongmokIndex(callput)).price(callput, i, 0) '시가
                    Chart1.Series(CandlestrickSeries).Points(retindex).YValues(3) = Data(selectedJongmokIndex(callput)).price(callput, i, 3) '종가

                    If Data(selectedJongmokIndex(callput)).price(callput, i, 0) < Data(selectedJongmokIndex(callput)).price(callput, i, 3) Then '시가보다 종가가 크면 
                        Chart1.Series(CandlestrickSeries).Points(retindex).Color = Color.Red
                        Chart1.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Red
                    ElseIf Data(selectedJongmokIndex(callput)).price(callput, i, 0) > Data(selectedJongmokIndex(callput)).price(callput, i, 3) Then
                        Chart1.Series(CandlestrickSeries).Points(retindex).Color = Color.Blue
                        Chart1.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Blue
                    End If

                    Dim str As String = "시간:" & Data(0).ctime(i) & vbCrLf & "시가:" & Data(selectedJongmokIndex(callput)).price(callput, i, 0) & vbCrLf & "종가:" & Data(selectedJongmokIndex(callput)).price(callput, i, 3)
                    Chart1.Series(CandlestrickSeries).Points(retindex).ToolTip = str


                    'Annotation 추가 - 고가저가
                    Dim annstr As String
                    If Data(selectedJongmokIndex(callput)).price(callput, i, 1) = Data(selectedJongmokIndex(callput)).Small(callput, 1) Then
                        annstr = "고가 최저가:" & Data(selectedJongmokIndex(callput)).Small(callput, 1).ToString() & vbCr & vbLf & i.ToString() & "(" & Data(0).ctime(i) & ")"
                        AddAnnotation(callput, i, annstr, 0)
                    End If

                    If Data(selectedJongmokIndex(callput)).price(callput, i, 2) = Data(selectedJongmokIndex(callput)).Big(callput, 2) Then
                        annstr = "저가 최고가:" & Data(selectedJongmokIndex(callput)).Big(callput, 2).ToString() & vbCr & vbLf & i.ToString() & "(" & Data(0).ctime(i) & ")"
                        AddAnnotation(callput, i, annstr, 1)
                    End If

                    'high Line 입력
                    Chart1.Series(UpperSeries).Points.AddXY(i, Data(selectedJongmokIndex(callput)).Big(callput, 2)) '저가중의 고가를 입력한다
                    'low Line 입력
                    Chart1.Series(LowerSeries).Points.AddXY(i, Data(selectedJongmokIndex(callput)).Small(callput, 1)) '고가 중의 저가를 입력한다

                Next
                'Chart1.Series(CandlestrickSeries).ToolTip = Format("0.00", "#VALY") '이렇게 하면 시리즈 전체에 같은 형태의 ToopTip을 추가할 수 있으나 Point 각각 입력하는 방식을 선택했다
            Next

            '콜 풋 차트의 크기를 똑같이 하기 위해서 최대,최소값을 맞춘다
            Dim maxValue As Single = Math.Max(Data(selectedJongmokIndex(0)).Big(0, 1), Data(selectedJongmokIndex(1)).Big(1, 1)) + 0.1
            Dim minValue As Single = Math.Min(Data(selectedJongmokIndex(0)).Small(0, 2), Data(selectedJongmokIndex(1)).Small(1, 2)) - 0.1
            For i = 0 To 1
                Chart1.ChartAreas(i).AxisY.Minimum = minValue
                Chart1.ChartAreas(i).AxisY.Maximum = maxValue
                Chart1.ChartAreas(i).AxisY.Interval = 0.1
            Next
        End If
    End Sub

    Private Sub AddAnnotation(ByVal callput As Integer, ByVal index As Integer, ByVal targetStr As String, ByVal targetCase As Integer)

        Dim ann = New CalloutAnnotation

        ann.Text = targetStr
        'ann.ToolTip = "Annootation Tooltip"

        If targetCase = 0 Then
            ann.ForeColor = Color.Magenta
        Else
            ann.ForeColor = Color.Green
        End If

        ann.AnchorDataPoint = Chart1.Series("CandleStick_" + callput.ToString()).Points(index)
        ann.Visible = True

        Chart1.Annotations.Add(ann)



    End Sub

    Private Sub InitGraph()

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

            str = "Upper_" + i.ToString()
            Chart1.Series.Add(str)
            Chart1.Series(str).ChartArea = ChartAreaStr
            Chart1.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(str).Color = Color.Red

            str = "Lower_" + i.ToString()
            Chart1.Series.Add(str)
            Chart1.Series(str).ChartArea = ChartAreaStr
            Chart1.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(str).Color = Color.Blue

            'ChartArea 속성 설정

            ''Lebel 설정
            Chart1.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"
            '축 선 속성 설정
            Chart1.ChartAreas(i).AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart1.ChartAreas(i).AxisX.MajorGrid.LineColor = Color.Gray
            Chart1.ChartAreas(i).AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            Chart1.ChartAreas(i).AxisY.MajorGrid.LineColor = Color.Gray


        Next

    End Sub


    Private Sub DrawGraph()

        InitGraph()
        DrawWinFormGraph()
        'DrawHippoGraph()

    End Sub

    Private Sub DrawColor_Selected()

        Dim i, j, k, callput As Integer
        Dim color As Integer
        Dim point As Integer

        For callput = 0 To 1


            i = selectedJongmokIndex(callput)

            For k = 0 To 3

                For j = 0 To currentIndex - 1

                    color = ItsColor(i, callput, j, k)
                    If callput = 0 Then
                        point = 1
                    Else
                        point = 6
                    End If

                    DrawColorOne(j, point + k, color, grd_selected)

                Next
            Next
        Next

        'Dim max As Single = Single.MinValue
        'Dim min As Single = Single.MaxValue
        'Dim 종가max As Single = Single.MinValue
        'Dim 종가min As Single = Single.MaxValue

        'For j = 0 To currentIndex - 1

        '    Dim 시가합계 As Single = Data(selectedJongmokIndex(0)).price(0, j, 0) + Data(selectedJongmokIndex(1)).price(1, j, 0)
        '    If max < 시가합계 Then max = 시가합계
        '    If min > 시가합계 Then min = 시가합계

        '    Dim 종가합계 As Single = Data(selectedJongmokIndex(0)).price(0, j, 3) + Data(selectedJongmokIndex(1)).price(1, j, 3)
        '    If 종가max < 종가합계 Then 종가max = 종가합계
        '    If 종가min > 종가합계 Then 종가min = 종가합계

        'Next

        For j = 0 To currentIndex - 1
            If SumDataSet.siMax = SumDataSet.siSum(j) Then DrawColorOne(j, 11, 0, grd_selected)
            If SumDataSet.siMin = SumDataSet.siSum(j) Then DrawColorOne(j, 11, 1, grd_selected)

            If SumDataSet.jongMax = SumDataSet.jongSum(j) Then DrawColorOne(j, 12, 0, grd_selected)
            If SumDataSet.jongmin = SumDataSet.jongSum(j) Then DrawColorOne(j, 12, 1, grd_selected)
        Next

    End Sub

    Private Sub DrawColorAll()
        Dim i, j, k, callput As Integer
        Dim color As Integer
        Dim point As Integer

        For callput = 0 To 1
            For i = 0 To TotalCount - 1
                For k = 0 To 3
                    For j = 0 To currentIndex - 1


                        color = ItsColor(i, callput, j, k)
                        If callput = 0 Then
                            point = 1
                        Else
                            point = 6
                        End If

                        DrawColorOne(j, i * 10 + point + k, color, grid1)

                    Next
                Next
            Next
        Next
    End Sub

    Private Sub DrawColorOne(i As Integer, j As Integer, colorNum As Integer, ByRef grd As DataGridView)

        Select Case colorNum

            Case 0 '빨강 - 최대값
                grd.Rows(i).Cells(j).Style.BackColor = Color.DarkRed
                grd.Rows(i).Cells(j).Style.ForeColor = Color.LightYellow
            Case 1 '파랑 - 최저값
                grd.Rows(i).Cells(j).Style.BackColor = Color.DarkBlue
                grd.Rows(i).Cells(j).Style.ForeColor = Color.LightYellow
            Case 3 '하늘색 - 제2저가
                grd.Rows(i).Cells(j).Style.BackColor = Color.LightBlue
                grd.Rows(i).Cells(j).Style.ForeColor = Color.Black

        End Select

    End Sub

    '신호그리드 초기화
    Private Sub InitShinHoGird()

        Dim rowCount As Integer = 38

        grd_ShinHo.Columns.Clear()
        grd_ShinHo.Rows.Clear()

        grd_ShinHo.ColumnCount = 2
        grd_ShinHo.RowCount = rowCount
        grd_ShinHo.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        grd_ShinHo.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        grd_ShinHo.Columns(0).HeaderText = "항목"
        grd_ShinHo.Columns(1).HeaderText = "내용"
        grd_ShinHo.Columns(0).Width = 150
        grd_ShinHo.Columns(1).Width = 150

        grd_ShinHo.RowHeadersWidth = 30
        grd_ShinHo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        grd_ShinHo.RowHeadersVisible = False
        grd_ShinHo.RowHeadersDefaultCellStyle.BackColor = Color.Yellow
        grd_ShinHo.RowHeadersDefaultCellStyle.ForeColor = Color.Black

        For i As Integer = 0 To rowCount - 1
            grd_ShinHo.Rows(i).Height = 23 '전체 Row 높이 지정
        Next

        grd_ShinHo.Rows(0).Cells(0).Value = "A00_월물"
        grd_ShinHo.Rows(1).Cells(0).Value = "A01_날짜"
        grd_ShinHo.Rows(2).Cells(0).Value = "A02_interval"
        grd_ShinHo.Rows(3).Cells(0).Value = "A03_남은날짜"
        grd_ShinHo.Rows(4).Cells(0).Value = "A04_발생Index"
        grd_ShinHo.Rows(5).Cells(0).Value = "A05_발생시간"
        grd_ShinHo.Rows(6).Cells(0).Value = "A06_신호ID"
        grd_ShinHo.Rows(7).Cells(0).Value = "A07신호차수"
        grd_ShinHo.Rows(8).Cells(0).Value = "A11_콜인덱스"
        grd_ShinHo.Rows(9).Cells(0).Value = "A12_콜행사가"
        grd_ShinHo.Rows(10).Cells(0).Value = "A13_콜신호발생가격"
        grd_ShinHo.Rows(11).Cells(0).Value = "A14_콜매수가격"
        grd_ShinHo.Rows(12).Cells(0).Value = "A15_콜주문번호"
        grd_ShinHo.Rows(13).Cells(0).Value = "A16_콜종목코드"
        grd_ShinHo.Rows(14).Cells(0).Value = "A17_콜체결상태"
        grd_ShinHo.Rows(15).Cells(0).Value = "A21_풋인덱스"
        grd_ShinHo.Rows(16).Cells(0).Value = "A22_풋행사가"
        grd_ShinHo.Rows(17).Cells(0).Value = "A23_풋신호발생가격"
        grd_ShinHo.Rows(18).Cells(0).Value = "A24_풋매수가격"
        grd_ShinHo.Rows(19).Cells(0).Value = "A25_풋주문번호"
        grd_ShinHo.Rows(20).Cells(0).Value = "A26_풋종목코드"
        grd_ShinHo.Rows(21).Cells(0).Value = "A27_풋체결상태"
        grd_ShinHo.Rows(22).Cells(0).Value = "A31_신호합계가격"
        grd_ShinHo.Rows(23).Cells(0).Value = "A32_현재합계가격"
        grd_ShinHo.Rows(24).Cells(0).Value = "A33_현재상태"
        grd_ShinHo.Rows(25).Cells(0).Value = "A34_이익률"
        grd_ShinHo.Rows(26).Cells(0).Value = "A35_손절기준가격"
        grd_ShinHo.Rows(27).Cells(0).Value = "A36_익절기준가격"
        grd_ShinHo.Rows(28).Cells(0).Value = "A37_손절기준비율"
        grd_ShinHo.Rows(29).Cells(0).Value = "A38_익절기준비율"
        grd_ShinHo.Rows(30).Cells(0).Value = "A39_중간매도Flag"
        grd_ShinHo.Rows(31).Cells(0).Value = "A40_TimeoutTime"
        grd_ShinHo.Rows(32).Cells(0).Value = "A41_매도시간"
        grd_ShinHo.Rows(33).Cells(0).Value = "A42_매도Index"
        grd_ShinHo.Rows(34).Cells(0).Value = "A43_매도사유"
        grd_ShinHo.Rows(35).Cells(0).Value = "A44_메모"
        grd_ShinHo.Rows(36).Cells(0).Value = "A45_기준가격"
        grd_ShinHo.Rows(37).Cells(0).Value = "A46_신호Timeout"
    End Sub


    'selectedgrid 초기화
    Private Sub InitDrawSelectedGird()
        Dim defaultWidth As Integer
        Dim i, j As Integer

        defaultWidth = 45

        grd_selected.Columns.Clear()
        grd_selected.Rows.Clear()


        '전체 크기 지정
        grd_selected.ColumnCount = 13
        grd_selected.RowCount = timeIndex

        grd_selected.Columns(0).HeaderText = "No"
        grd_selected.Columns(0).Width = 30
        grd_selected.RowHeadersWidth = 70

        grd_selected.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Row HeaderCell에 시간을 넣는다
        For j = 0 To timeIndex - 1
            grd_selected.Rows(j).HeaderCell.Value = Data(0).ctime(j)
            grd_selected.Rows(j).Height = 21 '전체 Row 높이 지정

            grd_selected.Rows(j).Cells(0).Value = j
        Next
        grd_selected.RowHeadersDefaultCellStyle.BackColor = Color.Yellow
        grd_selected.RowHeadersDefaultCellStyle.ForeColor = Color.Black

        grd_selected.Columns(1).HeaderText = "시"
        grd_selected.Columns(2).HeaderText = "고"
        grd_selected.Columns(3).HeaderText = "저"
        grd_selected.Columns(4).HeaderText = "종"
        grd_selected.Columns(5).HeaderText = "거래량"
        grd_selected.Columns(6).HeaderText = "시"
        grd_selected.Columns(7).HeaderText = "고"
        grd_selected.Columns(8).HeaderText = "저"
        grd_selected.Columns(9).HeaderText = "종"
        grd_selected.Columns(10).HeaderText = "거래량"
        grd_selected.Columns(11).HeaderText = "시가합계"
        grd_selected.Columns(12).HeaderText = "종가합계"

        grd_selected.Columns(1).Width = defaultWidth
        grd_selected.Columns(2).Width = defaultWidth
        grd_selected.Columns(3).Width = defaultWidth
        grd_selected.Columns(4).Width = defaultWidth
        grd_selected.Columns(5).Width = defaultWidth + 15
        grd_selected.Columns(6).Width = defaultWidth
        grd_selected.Columns(7).Width = defaultWidth
        grd_selected.Columns(8).Width = defaultWidth
        grd_selected.Columns(9).Width = defaultWidth
        grd_selected.Columns(10).Width = defaultWidth + 15
        grd_selected.Columns(11).Width = defaultWidth + 20
        grd_selected.Columns(12).Width = defaultWidth + 20

        grd_selected.Columns(5).DefaultCellStyle.BackColor = Color.Yellow
        grd_selected.Columns(10).DefaultCellStyle.BackColor = Color.Yellow


        grd_selected.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        For i = 0 To 11   '헤더 가운데 정렬
            grd_selected.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grd_selected.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Sub DrawShinhoGridData()

        Dim selectedShinhoIndex As Integer = 0

        If ShinhoList Is Nothing Or ShinhoList.Count = 0 Then Return

        Dim shinho As ShinhoType = ShinhoList(selectedShinhoIndex)

        grd_ShinHo.Rows(0).Cells(1).Value = shinho.A00_월물
        grd_ShinHo.Rows(1).Cells(1).Value = shinho.A01_날짜
        grd_ShinHo.Rows(2).Cells(1).Value = shinho.A02_interval
        grd_ShinHo.Rows(3).Cells(1).Value = shinho.A03_남은날짜
        grd_ShinHo.Rows(4).Cells(1).Value = shinho.A04_발생Index
        grd_ShinHo.Rows(5).Cells(1).Value = shinho.A05_발생시간
        grd_ShinHo.Rows(6).Cells(1).Value = shinho.A06_신호ID
        grd_ShinHo.Rows(7).Cells(1).Value = shinho.A07신호차수
        grd_ShinHo.Rows(8).Cells(1).Value = shinho.A11_콜인덱스
        grd_ShinHo.Rows(9).Cells(1).Value = shinho.A12_콜행사가
        grd_ShinHo.Rows(10).Cells(1).Value = Format(shinho.A13_콜신호발생가격, "##0.00")
        grd_ShinHo.Rows(11).Cells(1).Value = Format(shinho.A14_콜매수가격, "##0.00")
        grd_ShinHo.Rows(12).Cells(1).Value = shinho.A15_콜주문번호
        grd_ShinHo.Rows(13).Cells(1).Value = shinho.A16_콜종목코드
        grd_ShinHo.Rows(14).Cells(1).Value = shinho.A17_콜체결상태
        grd_ShinHo.Rows(15).Cells(1).Value = shinho.A21_풋인덱스
        grd_ShinHo.Rows(16).Cells(1).Value = shinho.A22_풋행사가
        grd_ShinHo.Rows(17).Cells(1).Value = Format(shinho.A23_풋신호발생가격, "##0.00")
        grd_ShinHo.Rows(18).Cells(1).Value = Format(shinho.A24_풋매수가격, "##0.00")
        grd_ShinHo.Rows(19).Cells(1).Value = shinho.A25_풋주문번호
        grd_ShinHo.Rows(20).Cells(1).Value = shinho.A26_풋종목코드
        grd_ShinHo.Rows(21).Cells(1).Value = shinho.A27_풋체결상태
        grd_ShinHo.Rows(22).Cells(1).Value = Format(shinho.A31_신호합계가격, "##0.00")
        grd_ShinHo.Rows(23).Cells(1).Value = Format(shinho.A32_현재합계가격, "##0.00")
        grd_ShinHo.Rows(24).Cells(1).Value = shinho.A33_현재상태

        grd_ShinHo.Rows(25).Cells(1).Value = Format(shinho.A34_이익률, "##0.00#")
        grd_ShinHo.Rows(26).Cells(1).Value = Format(shinho.A35_손절기준가격, "##0.000")
        grd_ShinHo.Rows(27).Cells(1).Value = Format(shinho.A36_익절기준가격, "##0.000")
        grd_ShinHo.Rows(28).Cells(1).Value = shinho.A37_손절기준비율
        grd_ShinHo.Rows(29).Cells(1).Value = shinho.A38_익절기준비율
        grd_ShinHo.Rows(30).Cells(1).Value = shinho.A39_중간매도Flag
        grd_ShinHo.Rows(31).Cells(1).Value = shinho.A40_TimeoutTime
        grd_ShinHo.Rows(32).Cells(1).Value = shinho.A41_매도시간
        grd_ShinHo.Rows(33).Cells(1).Value = shinho.A42_매도Index
        grd_ShinHo.Rows(34).Cells(1).Value = shinho.A43_매도사유
        grd_ShinHo.Rows(35).Cells(1).Value = shinho.A44_메모
        grd_ShinHo.Rows(36).Cells(1).Value = shinho.A45_기준가격
        grd_ShinHo.Rows(37).Cells(1).Value = shinho.A46_신호Timeout

        If shinho.A33_현재상태 = 1 Then
            grd_ShinHo.Rows(24).Cells(1).Style.BackColor = Color.Yellow
            grd_ShinHo.Rows(24).Cells(1).Style.ForeColor = Color.Red
        Else
            grd_ShinHo.Rows(24).Cells(1).Style.BackColor = Color.LightGreen
            grd_ShinHo.Rows(24).Cells(1).Style.ForeColor = Color.Black
        End If

        If shinho.A34_이익률 > 1 Then
            grd_ShinHo.Rows(25).Cells(1).Style.BackColor = Color.Yellow
            grd_ShinHo.Rows(25).Cells(1).Style.ForeColor = Color.Red
        Else
            grd_ShinHo.Rows(25).Cells(1).Style.BackColor = Color.LightGreen
            grd_ShinHo.Rows(25).Cells(1).Style.ForeColor = Color.Black
        End If

    End Sub

    Private Sub DrawSelectedData()
        Dim selectedCallIndex, selectedPutIndex, j As Integer

        selectedCallIndex = selectedJongmokIndex(0)
        selectedPutIndex = selectedJongmokIndex(1)


        For j = 0 To currentIndex

            If Val(Data(selectedCallIndex).ctime(j)) > 0 Then
                If Data(selectedCallIndex).price(0, j, 0) > 0 Then  '시가가 0보다 크면 입력, 즉 4개다 데이터가 있을 때만 입력 - 콜
                    grd_selected.Rows(j).Cells(1).Value = Data(selectedCallIndex).price(0, j, 0)
                    grd_selected.Rows(j).Cells(2).Value = Data(selectedCallIndex).price(0, j, 1)
                    grd_selected.Rows(j).Cells(3).Value = Data(selectedCallIndex).price(0, j, 2)
                    grd_selected.Rows(j).Cells(4).Value = Data(selectedCallIndex).price(0, j, 3)
                    grd_selected.Rows(j).Cells(5).Value = Data(selectedCallIndex).거래량(0, j).ToString()
                End If
            End If

            If Val(Data(selectedPutIndex).ctime(j)) > 0 Then
                If Data(selectedPutIndex).price(1, j, 0) > 0 Then  '시가가 0보다 크면 입력, 즉 4개다 데이터가 있을 때만 입력 - 풋
                    grd_selected.Rows(j).Cells(6).Value = Data(selectedPutIndex).price(1, j, 0)
                    grd_selected.Rows(j).Cells(7).Value = Data(selectedPutIndex).price(1, j, 1)
                    grd_selected.Rows(j).Cells(8).Value = Data(selectedPutIndex).price(1, j, 2)
                    grd_selected.Rows(j).Cells(9).Value = Data(selectedPutIndex).price(1, j, 3)
                    grd_selected.Rows(j).Cells(10).Value = Data(selectedPutIndex).거래량(1, j).ToString()
                End If
            End If

            '시가 종가 합계 적용
            If Val(Data(selectedCallIndex).ctime(j)) = Val(Data(selectedPutIndex).ctime(j)) Then
                If SumDataSet.siSum(j) > 0 Then
                    grd_selected.Rows(j).Cells(11).Value = SumDataSet.siSum(j)
                    grd_selected.Rows(j).Cells(12).Value = SumDataSet.jongSum(j)
                End If
            End If
        Next

    End Sub

    Private Sub InitFirstGrid()
        Dim jongMok As String
        Dim i, j As Integer

        grid1.Columns.Clear()
        grid1.Rows.Clear()

        '전체 크기 지정
        grid1.ColumnCount = TotalCount * 10 + 1
        grid1.RowCount = timeIndex

        grid1.Columns(0).HeaderText = "No"
        grid1.Columns(TotalCount * 10).HeaderText = "시간"

        grid1.Columns(0).Width = 40
        grid1.Columns(TotalCount * 10).Width = 40

        grid1.Columns(0).HeaderText = "No"
        grid1.Columns(0).Width = 30
        grid1.RowHeadersWidth = 70

        grid1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Row HeaderCell에 시간을 넣는다
        For j = 0 To timeIndex - 1 '-1을 하면 currentIndex와 같아진다
            grid1.Rows(j).HeaderCell.Value = Data(0).ctime(j)
            grid1.Rows(j).Height = 21 '전체 Row 높이 지정

            grid1.Rows(j).Cells(0).Value = j

            grid1.Rows(j).Cells(TotalCount * 10).Value = Data(0).ctime(j)
        Next


        For i = 0 To TotalCount - 1
            jongMok = Data(i).HangSaGa

            grid1.Columns(i * 10 + 1).HeaderText = "시"
            grid1.Columns(i * 10 + 2).HeaderText = "고"
            grid1.Columns(i * 10 + 3).HeaderText = "저"
            grid1.Columns(i * 10 + 4).HeaderText = "종"
            grid1.Columns(i * 10 + 5).HeaderText = jongMok
            grid1.Columns(i * 10 + 5).DefaultCellStyle.BackColor = Color.Yellow
            grid1.Columns(i * 10 + 6).HeaderText = "시"
            grid1.Columns(i * 10 + 7).HeaderText = "고"
            grid1.Columns(i * 10 + 8).HeaderText = "저"
            grid1.Columns(i * 10 + 9).HeaderText = "종"

            grid1.Columns(i * 10 + 1).Width = 50
            grid1.Columns(i * 10 + 2).Width = 50
            grid1.Columns(i * 10 + 3).Width = 50
            grid1.Columns(i * 10 + 4).Width = 50
            grid1.Columns(i * 10 + 5).Width = 50
            grid1.Columns(i * 10 + 6).Width = 50
            grid1.Columns(i * 10 + 7).Width = 50
            grid1.Columns(i * 10 + 8).Width = 50
            grid1.Columns(i * 10 + 9).Width = 50

            'grd_selected.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid1.Columns(i * 10 + 9).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            'dataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable ------------ 이걸해야 중앙정렬이 정상적으로 됨
            grid1.Columns(i * 10 + 1).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 2).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 3).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 4).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 5).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 6).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 7).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 8).SortMode = DataGridViewColumnSortMode.NotSortable
            grid1.Columns(i * 10 + 9).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For i = 0 To TotalCount - 2
            grid1.Columns(i * 10 + 10).Width = 10
        Next


    End Sub

    Private Sub DrawGrid1Data()
        Dim i, j As Integer

        For i = 0 To TotalCount - 1
            For j = 0 To currentIndex

                If Val(Data(i).ctime(j)) > 0 Then

                    grid1.Rows(j).Cells(i * 10 + 5).Value = Data(i).HangSaGa

                    If Data(i).price(0, j, 0) > 0 Then  '시가가 0보다 크면 입력, 즉 4개다 데이터가 있을 때만 입력 - 콜

                        grid1.Rows(j).Cells(i * 10 + 1).Value = Data(i).price(0, j, 0)
                        grid1.Rows(j).Cells(i * 10 + 2).Value = Data(i).price(0, j, 1)
                        grid1.Rows(j).Cells(i * 10 + 3).Value = Data(i).price(0, j, 2)
                        grid1.Rows(j).Cells(i * 10 + 4).Value = Data(i).price(0, j, 3)
                    End If

                    If Data(i).price(1, j, 0) > 0 Then  '시가가 0보다 크면 입력, 즉 4개다 데이터가 있을 때만 입력 - 풋
                        grid1.Rows(j).Cells(i * 10 + 6).Value = Data(i).price(1, j, 0)
                        grid1.Rows(j).Cells(i * 10 + 7).Value = Data(i).price(1, j, 1)
                        grid1.Rows(j).Cells(i * 10 + 8).Value = Data(i).price(1, j, 2)
                        grid1.Rows(j).Cells(i * 10 + 9).Value = Data(i).price(1, j, 3)
                    End If

                End If
            Next
        Next
    End Sub

    Private Sub cmb_selectedJongmokIndex_0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_0.SelectedIndexChanged

        Dim selectedIndex = cmb_selectedJongmokIndex_0.SelectedIndex

        If selectedIndex > 0 Then
            selectedJongmokIndex(0) = selectedIndex - 1

            '여기에 합산 Data 계산하는 로직 추가해야 함
            CalcSumPrice() '콜풋 시가종가의 합계를 구한다
            InitDrawSelectedGird()
            DrawSelectedData()
            DrawColor_Selected()
            DrawGraph() '그래프 그리기
        End If
    End Sub

    Private Sub cmb_selectedJongmokIndex_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_1.SelectedIndexChanged

        Dim selectedIndex = cmb_selectedJongmokIndex_1.SelectedIndex

        If selectedIndex > 0 Then

            selectedJongmokIndex(1) = selectedIndex - 1
            CalcSumPrice() '콜풋 시가종가의 합계를 구한다
            InitDrawSelectedGird()
            DrawSelectedData()
            DrawColor_Selected()
            DrawGraph() '그래프 그리기

        End If

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        timerCount = timerCount + 1
        label_timerCounter.Text = timerCount.ToString()

        If timerCount >= timerMaxInterval Then

            timerCount = 0

            '계좌정보 가져오기

            '옵션 정보 가져와서 화면 표시
            GetAllData()
            Clac_DisplayAllGrid()
            RedrawAll()

            'DB에 자동 저장 기능 추가 필요

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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txt_TableName.Text = "option_190628"

        Dim dt As Date = Now.AddDays(-30)  '여기 원래 -30을 넣어야 함
        Dim strdt As String = Format(dt, "yyMM01")
        txt_DB_Date_Limit.Text = "WHERE cdate >= " + strdt

        Dim str As String = "1.0.1_20220524"

        txt_programversion.Text = str

        InitShinHoGird()

    End Sub

    Private Sub btn_InsertDB_Click(sender As Object, e As EventArgs) Handles btn_InsertDB.Click
        Dim tempTargetDate As Integer = Val(txt_DBDate.Text)

        If tempTargetDate > 20000000 Then
            tempTargetDate = tempTargetDate Mod 20000000
        End If

        Dim rowCount As Integer

        If tempTargetDate > 0 Then
            rowCount = GetRowCount(tempTargetDate)
        End If

        If rowCount = 0 Then '오늘 날짜에 등록된게 없으면 입력한다

            InsertTargetDateData(tempTargetDate)

        Else
            MsgBox(tempTargetDate.ToString() + " 날에는 이미 등록되어 있습니다")
            'InsertTargetDateData(tempTargetDate)
        End If

    End Sub

    '하루씩 DB에서 읽어오는 방식 샘플  -- 현재는 사용하지 않고 전체를 딕셔너리에 넣는 방식을 적용함
    Private Sub sample_btn_SelectDB_Click()
        Dim dateCount As Integer

        dateCount = GetDateList() '이걸하면 DBDateList() 배열에 전역변수 DateList를 입력한다

        Add_Log("", "DB 전체 일 수는 " + dateCount.ToString() + " 일")

        If dateCount > 0 Then
            DBDate_HScrollBar.Maximum = dateCount - 1
            DBDate_HScrollBar.LargeChange = 1

            DBDate_HScrollBar.Refresh()

            InitDataStructure()
            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다

            gTargetDateIndex = 0 '이것도 전역변수
            TargetDate = DBDateList(gTargetDateIndex)

            TotalCount = GetDailyRawData(TargetDate) '이걸하면 Data() 구조체에 해당하는 날짜의 data를 집어넣는다

            If TotalCount > 0 Then

                Clac_DisplayAllGrid()
                RedrawAll() 'Grid 그리기
                DrawGraph() '그래프 그리기
                DrawScrollData()
            End If

        Else
            MsgBox("DB에 데이터가 없습니다")
        End If
    End Sub

    Private Sub btn_SelectDB_Click(sender As Object, e As EventArgs) Handles btn_SelectDB.Click
        Dim dateCount As Integer

        Add_Log("일반", "전체 Data 취합 Click")

        dateCount = GetRawData(txt_DB_Date_Limit.Text) '이걸하면 딕셔너리에 데이터를 넣고 날짜수를 리턴해줌
        Add_Log("일반", "전체 Data 취합 끝. 날짜수는 " + dateCount.ToString())

        Add_Log("", "DB 전체 일 수는 " + dateCount.ToString() + " 일")

        If dateCount > 0 Then
            DBDate_HScrollBar.Maximum = dateCount - 1
            DBDate_HScrollBar.LargeChange = 1

            DBDate_HScrollBar.Refresh()

            InitDataStructure()
            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다

            gTargetDateIndex = 0 '이것도 전역변수
            TargetDate = DBDateList(gTargetDateIndex)

            TotalCount = GetDataFromDBHandler(TargetDate)

            If TotalCount > 0 Then

                Clac_DisplayAllGrid()
                RedrawAll() 'Grid 그리기
                DrawGraph() '그래프 그리기
                DrawScrollData()
            End If

        Else
            MsgBox("DB에 데이터가 없습니다")
        End If

    End Sub

    Private Sub DBDate_HScrollBar_Scroll(sender As Object, e As ScrollEventArgs) Handles DBDate_HScrollBar.Scroll

        If gTargetDateIndex <> e.NewValue Then

            Add_Log("일반", "DBDate_HScrollBar_ValueChanged 호출됨 " + e.NewValue.ToString())
            InitDataStructure()
            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다

            gTargetDateIndex = e.NewValue

            TargetDate = DBDateList(gTargetDateIndex)
            'TotalCount = GetDailyRawData(TargetDate) '이걸하면 Data() 구조체에 해당하는 날짜의 data를 집어넣고 종목의 Count를 리턴한다

            TotalCount = GetDataFromDBHandler(TargetDate) '이걸하면 딕셔너리의 data에서 해당 날짜의 Data를 가져온다

            If TotalCount > 0 Then

                Clac_DisplayAllGrid()
                RedrawAll() 'Grid 그리기
                DrawGraph() '그래프 그리기
                DrawScrollData()
            End If

        End If


    End Sub

    Private Sub grd_selected_Scroll(sender As Object, e As ScrollEventArgs) Handles grd_selected.Scroll
        grid1.FirstDisplayedScrollingRowIndex = grd_selected.FirstDisplayedScrollingRowIndex
    End Sub


End Class
