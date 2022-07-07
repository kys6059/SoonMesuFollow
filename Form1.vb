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

        Dim ConnectionState = FindTargetDate() '현재 사이보스에 접속된 상태라면

        isRealFlag = True '실시간 로직임을 기억한다

        If ConnectionState = True Then

            TotalCount = GetTotalJongmokCount()

            Do While TotalCount > 25                  '15초당 60개 TR 제한을 고려하여 최대 각 28개만 받아온다
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

            Else
                MsgBox("가져올 수 있는 종목이 없습니다")
            End If
        Else
            MsgBox("사이보스에 연결되지 않았습니다")
        End If

        Return False

    End Function

    Public Sub DrawScrollData()

        If DBTotalDateCount > 1 Then lbl_DBDateInfo.Text = "총 " + DBTotalDateCount.ToString() + "일 중 " + (gTargetDateIndex + 1).ToString() + " 번째(" + DBDateList(gTargetDateIndex).ToString() + ")"

    End Sub

    Private Sub Clac_DisplayAllGrid()         'selectedJongmokIndex 계산

        If selectedJongmokIndex(0) < 0 Or chk_ChangeTargetIndex.Checked = True Then '아직 한번도 선택하지 않았거나 Checked가 True일 때만 자동으로 변경함
            selectedJongmokIndex(0) = CalcTargetJonhmokIndex(0)
            selectedJongmokIndex(1) = CalcTargetJonhmokIndex(1)
        End If

        SetScrolData() '타임 스크롤의 최대최소값을 지정한다

        CalcSumPrice() '콜풋 시가종가의 합계를 구한다

        CalcColorData()        '최대최소,제2저가 계산

        신호현재상태확인하기()        '신호 만들고 해제 판단하기

        Dim 신호발생flag As Boolean = CalcAlrotithmAll()
        If 신호발생flag = True Then
            chk_ChangeTargetIndex.Checked = False '양매도 당시의 기준종목이 변경되지 않도록 고정한다
        End If

        If chk_화면끄기.Checked = False Then
            RedrawAll() 'Grid 그리기
            DrawScrollData()
        End If

        DrawGraph() '그래프 그리기

    End Sub

    Private Sub SetScrolData()

        Hscroll_1.Maximum = timeIndex - 1
        Hscroll_1.Minimum = 0
        Hscroll_1.Refresh

    End Sub

    Private Sub RedrawAll()

        Dim tempIndex As Integer

        If currentIndex >= 0 Then

            'grd_selected 조절하기
            'combo에 전체 종목을 Add한다 인덱스, 행사가, 현재가격
            cmb_selectedJongmokIndex_0.Items.Clear()
            cmb_selectedJongmokIndex_1.Items.Clear()

            tempIndex = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다

            cmb_selectedJongmokIndex_0.Items.Add(" ") '0번이 선택되는게 초기화인지 명시적으로 0번을 선택했는지를 확인하기 위해서 제일 앞에 널값을 하나 넣는다
            cmb_selectedJongmokIndex_1.Items.Add(" ")
            For i As Integer = 0 To TotalCount - 1
                Dim str As String
                str = i.ToString() & ". 행사가 : " & Data(i).HangSaGa & " (" & Data(i).price(0, tempIndex, 3).ToString() & ")-" & 콜구매가능개수.ToString()
                cmb_selectedJongmokIndex_0.Items.Add(str)
                str = i.ToString() & ". 행사가 : " & Data(i).HangSaGa & " (" & Data(i).price(1, tempIndex, 3).ToString() & ")-" & 풋구매가능개수.ToString()
                cmb_selectedJongmokIndex_1.Items.Add(str)
            Next

            cmb_selectedJongmokIndex_1.SelectedIndex = selectedJongmokIndex(1) + 1
            cmb_selectedJongmokIndex_0.SelectedIndex = selectedJongmokIndex(0) + 1

            If chk_화면끄기.Checked = False Then
                grd_selected.Visible = False
                grid1.Visible = False
                InitFirstGrid()
                DrawGrid1Data()
                DrawColorAll()

                InitDrawSelectedGird()
                DrawSelectedData()
                DrawColor_Selected()            '색깔 실제로 grid에 입히기
                grd_selected.Visible = True
                grid1.Visible = True

            End If

            InitShinHoGird()
            DrawShinhoGridData() '신호를 추가한다

            '오늘날짜를 DBDate 텍스트박스에 넣기
            txt_DBDate.Text = TargetDate
            lbl_ScrolValue.Text = "CurrentIndex : " & currentIndex.ToString() & ", Time = " & Data(0).ctime(currentIndex)


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

            For i = 0 To txt_ebest_id.Series.Count - 1
                txt_ebest_id.Series(i).Points.Clear()
            Next

            tempindex = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다
            Dim maxValue As Single = Single.MinValue
            Dim minValue As Single = Single.MaxValue

            For callput = 0 To 1

                Dim CandlestrickSeries As String = "CandleStick_" + callput.ToString()
                Dim UpperSeries As String = "Upper_" + callput.ToString()
                Dim LowerSeries As String = "Lower_" + callput.ToString()
                For i = 0 To tempindex

                    '                main Series 입력
                    retindex = txt_ebest_id.Series(CandlestrickSeries).Points.AddXY(i, Data(selectedJongmokIndex(callput)).price(callput, i, 1)) '고가를 처음 넣는다
                    txt_ebest_id.Series(CandlestrickSeries).Points(retindex).YValues(1) = Data(selectedJongmokIndex(callput)).price(callput, i, 2) '저가
                    txt_ebest_id.Series(CandlestrickSeries).Points(retindex).YValues(2) = Data(selectedJongmokIndex(callput)).price(callput, i, 0) '시가
                    txt_ebest_id.Series(CandlestrickSeries).Points(retindex).YValues(3) = Data(selectedJongmokIndex(callput)).price(callput, i, 3) '종가

                    If Data(selectedJongmokIndex(callput)).price(callput, i, 0) < Data(selectedJongmokIndex(callput)).price(callput, i, 3) Then '시가보다 종가가 크면 
                        txt_ebest_id.Series(CandlestrickSeries).Points(retindex).Color = Color.Red
                        txt_ebest_id.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Red
                    ElseIf Data(selectedJongmokIndex(callput)).price(callput, i, 0) > Data(selectedJongmokIndex(callput)).price(callput, i, 3) Then
                        txt_ebest_id.Series(CandlestrickSeries).Points(retindex).Color = Color.Blue
                        txt_ebest_id.Series(CandlestrickSeries).Points(retindex).BorderColor = Color.Blue
                    End If

                    Dim str As String = "시간:" & Data(0).ctime(i) & vbCrLf & "시가:" & Data(selectedJongmokIndex(callput)).price(callput, i, 0) & vbCrLf & "종가:" & Data(selectedJongmokIndex(callput)).price(callput, i, 3)
                    txt_ebest_id.Series(CandlestrickSeries).Points(retindex).ToolTip = str


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
                    txt_ebest_id.Series(UpperSeries).Points.AddXY(i, Data(selectedJongmokIndex(callput)).Big(callput, 2)) '저가중의 고가를 입력한다
                    'low Line 입력
                    txt_ebest_id.Series(LowerSeries).Points.AddXY(i, Data(selectedJongmokIndex(callput)).Small(callput, 1)) '고가 중의 저가를 입력한다

                    If maxValue < Data(selectedJongmokIndex(callput)).price(callput, i, 1) Then maxValue = Data(selectedJongmokIndex(callput)).price(callput, i, 1) '계산해놓은 big, small로 보니 마지막 CurrentIndex의 값이 반영이 안되어 여기서 일일이 계산해서 처리하도록 변경 20220607
                    If minValue > Data(selectedJongmokIndex(callput)).price(callput, i, 2) Then minValue = Data(selectedJongmokIndex(callput)).price(callput, i, 2)

                Next
                'Chart1.Series(CandlestrickSeries).ToolTip = Format("0.00", "#VALY") '이렇게 하면 시리즈 전체에 같은 형태의 ToopTip을 추가할 수 있으나 Point 각각 입력하는 방식을 선택했다

            Next

            '콜 풋 차트의 크기를 똑같이 하기 위해서 최대,최소값을 맞춘다
            maxValue = maxValue + 0.1
            minValue = minValue - 0.1
            For i = 0 To 1
                txt_ebest_id.ChartAreas(i).AxisY.Minimum = minValue
                txt_ebest_id.ChartAreas(i).AxisY.Maximum = maxValue
                txt_ebest_id.ChartAreas(i).AxisY.Interval = 0.2
            Next


            'SumGraph 그리기
            For i = 0 To tempindex

                txt_ebest_id.Series("SiSum").Points.AddXY(i, SumDataSet.siSum(i)) '시가를 처음 넣는다
                txt_ebest_id.Series("JongSum").Points.AddXY(i, SumDataSet.jongSum(i))

                Dim str As String = Data(0).ctime(i) & vbCrLf & "시가합계:" & SumDataSet.siSum(i).ToString() & vbCrLf & "종가합계:" & SumDataSet.jongSum(i).ToString()
                txt_ebest_id.Series("SiSum").Points(i).ToolTip = str
                txt_ebest_id.Series("JongSum").Points(i).ToolTip = str

                If SumDataSet.siSum(i) = SumDataSet.siMax Then
                    Dim ann = New CalloutAnnotation
                    ann.Text = Data(0).ctime(i) & vbCrLf & "시가최고:" & SumDataSet.siMax
                    ann.ForeColor = Color.Red
                    ann.AnchorDataPoint = txt_ebest_id.Series("SiSum").Points(i)
                    txt_ebest_id.Annotations.Add(ann)
                End If

                If SumDataSet.jongSum(i) = SumDataSet.jongMax Then
                    Dim ann = New CalloutAnnotation
                    ann.Text = Data(0).ctime(i) & vbCrLf & "종가최고:" & SumDataSet.jongMax
                    ann.ForeColor = Color.Magenta
                    ann.AnchorDataPoint = txt_ebest_id.Series("JongSum").Points(i)
                    txt_ebest_id.Annotations.Add(ann)
                End If

                If SumDataSet.jongSum(i) = SumDataSet.jongmin Then
                    Dim ann = New CalloutAnnotation
                    ann.Text = Data(0).ctime(i) & vbCrLf & "종가최저:" & SumDataSet.jongmin
                    ann.ForeColor = Color.Green
                    ann.AnchorDataPoint = txt_ebest_id.Series("JongSum").Points(i)
                    txt_ebest_id.Annotations.Add(ann)
                End If

                If ShinhoList.Count > 0 Then   '신호가 있으면 관련 정보를 표시한다
                    txt_ebest_id.Series("Shinho").Points.AddXY(i, ShinhoList(0).A31_신호합계가격)
                    txt_ebest_id.Series("Shinho").Points(i).ToolTip = "매수가격:" & Format(ShinhoList(0).A31_신호합계가격, "0.00")
                    If i = currentIndex Then
                        Dim ann = New CalloutAnnotation
                        ann.Text = Data(0).ctime(i) & vbCrLf & "매수가격:" & Format(ShinhoList(0).A31_신호합계가격, "0.00") & vbCrLf & "현재가격:" & Format(ShinhoList(0).A32_현재합계가격, "0.00") & vbCrLf & "환산이익률:" & Format(ShinhoList(0).A46_환산이익율, "0.000")
                        ann.BackColor = Color.IndianRed
                        ann.ForeColor = Color.Yellow
                        ann.AnchorDataPoint = txt_ebest_id.Series("Shinho").Points(i)
                        txt_ebest_id.Annotations.Add(ann)
                    End If

                End If

            Next

            txt_ebest_id.ChartAreas("SUMGraph").AxisY.Minimum = Math.Min(SumDataSet.jongmin, SumDataSet.siMin) - 0.1
            txt_ebest_id.ChartAreas("SUMGraph").AxisY.Maximum = Math.Max(SumDataSet.siMax, SumDataSet.jongMax) + 0.1
            txt_ebest_id.ChartAreas("SUMGraph").AxisY.Interval = 0.1

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

        ann.AnchorDataPoint = txt_ebest_id.Series("CandleStick_" + callput.ToString()).Points(index)
        ann.Visible = True

        txt_ebest_id.Annotations.Add(ann)

    End Sub

    Private Sub InitGraph()

        Dim str, ChartAreaStr As String

        txt_ebest_id.Series.Clear()
        txt_ebest_id.ChartAreas.Clear()
        txt_ebest_id.Legends.Clear()
        txt_ebest_id.Annotations.Clear()

        For i As Integer = 0 To 1

            ChartAreaStr = "ChartArea_" + i.ToString()
            txt_ebest_id.ChartAreas.Add(ChartAreaStr)

            str = "CandleStick_" + i.ToString()
            txt_ebest_id.Series.Add(str)
            txt_ebest_id.Series(str).ChartArea = ChartAreaStr
            txt_ebest_id.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Candlestick
            txt_ebest_id.Series(str).CustomProperties = “PriceDownColor=Blue, PriceUpColor=Red”

            str = "Upper_" + i.ToString()
            txt_ebest_id.Series.Add(str)
            txt_ebest_id.Series(str).ChartArea = ChartAreaStr
            txt_ebest_id.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            txt_ebest_id.Series(str).Color = Color.Red

            str = "Lower_" + i.ToString()
            txt_ebest_id.Series.Add(str)
            txt_ebest_id.Series(str).ChartArea = ChartAreaStr
            txt_ebest_id.Series(str).ChartType = DataVisualization.Charting.SeriesChartType.Line
            txt_ebest_id.Series(str).Color = Color.Blue

            'ChartArea 속성 설정

            ''Lebel 설정
            txt_ebest_id.ChartAreas(i).AxisY.LabelStyle.Format = "{0:0.00}"
            '축 선 속성 설정
            txt_ebest_id.ChartAreas(i).AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            txt_ebest_id.ChartAreas(i).AxisX.MajorGrid.LineColor = Color.Gray
            txt_ebest_id.ChartAreas(i).AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            txt_ebest_id.ChartAreas(i).AxisY.MajorGrid.LineColor = Color.Gray
        Next

        '합계그래프 정의
        txt_ebest_id.ChartAreas.Add("SUMGraph")
        txt_ebest_id.ChartAreas("SUMGraph").AxisY.LabelStyle.Format = "{0:0.00}"
        txt_ebest_id.ChartAreas("SUMGraph").AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        txt_ebest_id.ChartAreas("SUMGraph").AxisX.MajorGrid.LineColor = Color.Gray
        txt_ebest_id.ChartAreas("SUMGraph").AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        txt_ebest_id.ChartAreas("SUMGraph").AxisY.MajorGrid.LineColor = Color.Gray

        Dim chartName() As String = {"SiSum", "JongSum"}
        For i As Integer = 0 To 1
            txt_ebest_id.Series.Add(chartName(i))
            txt_ebest_id.Series(chartName(i)).ChartArea = "SUMGraph"
            txt_ebest_id.Series(chartName(i)).ChartType = DataVisualization.Charting.SeriesChartType.Line
            txt_ebest_id.Series(chartName(i)).Color = Color.Black
        Next
        txt_ebest_id.Series(chartName(1)).Color = Color.Red

        If ShinhoList.Count > 0 Then            '신호가 있으면 매수가에 직선을 긋는다
            txt_ebest_id.Series.Add("Shinho")
            txt_ebest_id.Series("Shinho").ChartArea = "SUMGraph"
            txt_ebest_id.Series("Shinho").ChartType = DataVisualization.Charting.SeriesChartType.Line
            txt_ebest_id.Series("Shinho").Color = Color.DodgerBlue
            txt_ebest_id.Series("Shinho").BorderDashStyle = ChartDashStyle.Dash
        End If

    End Sub

    Private Sub DrawGraph()
        txt_ebest_id.Visible = False
        InitGraph()
        DrawWinFormGraph()
        txt_ebest_id.Visible = True
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

        Dim rowCount As Integer = 40

        grd_ShinHo.Columns.Clear()
        grd_ShinHo.Rows.Clear()

        grd_ShinHo.ColumnCount = 2
        grd_ShinHo.RowCount = rowCount
        grd_ShinHo.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        grd_ShinHo.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        grd_ShinHo.Columns(0).HeaderText = "항목"
        grd_ShinHo.Columns(1).HeaderText = "내용"
        grd_ShinHo.Columns(0).Width = 100
        grd_ShinHo.Columns(1).Width = 100

        grd_ShinHo.RowHeadersWidth = 30
        grd_ShinHo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        grd_ShinHo.RowHeadersVisible = False
        grd_ShinHo.RowHeadersDefaultCellStyle.BackColor = Color.Yellow
        grd_ShinHo.RowHeadersDefaultCellStyle.ForeColor = Color.Black

        For i As Integer = 0 To rowCount - 1
            grd_ShinHo.Rows(i).Height = 22 '전체 Row 높이 지정
        Next

        grd_ShinHo.Rows(0).Cells(0).Value = "월물"
        grd_ShinHo.Rows(1).Cells(0).Value = "날짜"
        grd_ShinHo.Rows(2).Cells(0).Value = "interval"
        grd_ShinHo.Rows(3).Cells(0).Value = "남은날짜"
        grd_ShinHo.Rows(4).Cells(0).Value = "발생Index"
        grd_ShinHo.Rows(5).Cells(0).Value = "발생시간"
        grd_ShinHo.Rows(6).Cells(0).Value = "신호ID"
        grd_ShinHo.Rows(7).Cells(0).Value = "호차수"
        grd_ShinHo.Rows(8).Cells(0).Value = "콜인덱스"
        grd_ShinHo.Rows(9).Cells(0).Value = "콜행사가"
        grd_ShinHo.Rows(10).Cells(0).Value = "콜신호발생가격"
        grd_ShinHo.Rows(11).Cells(0).Value = "콜매수가격"
        grd_ShinHo.Rows(12).Cells(0).Value = "콜주문번호"
        grd_ShinHo.Rows(13).Cells(0).Value = "콜종목코드"
        grd_ShinHo.Rows(14).Cells(0).Value = "콜체결상태"
        grd_ShinHo.Rows(15).Cells(0).Value = "풋인덱스"
        grd_ShinHo.Rows(16).Cells(0).Value = "풋행사가"
        grd_ShinHo.Rows(17).Cells(0).Value = "풋신호발생가격"
        grd_ShinHo.Rows(18).Cells(0).Value = "풋매수가격"
        grd_ShinHo.Rows(19).Cells(0).Value = "풋주문번호"
        grd_ShinHo.Rows(20).Cells(0).Value = "풋종목코드"
        grd_ShinHo.Rows(21).Cells(0).Value = "풋체결상태"
        grd_ShinHo.Rows(22).Cells(0).Value = "신호합계가격"
        grd_ShinHo.Rows(23).Cells(0).Value = "현재합계가격"
        grd_ShinHo.Rows(24).Cells(0).Value = "현재상태"
        grd_ShinHo.Rows(25).Cells(0).Value = "이익률"
        grd_ShinHo.Rows(26).Cells(0).Value = "손절기준가격"
        grd_ShinHo.Rows(27).Cells(0).Value = "익절기준가격"
        grd_ShinHo.Rows(28).Cells(0).Value = "손절기준비율"
        grd_ShinHo.Rows(29).Cells(0).Value = "익절기준비율"
        grd_ShinHo.Rows(30).Cells(0).Value = "중간매도Flag"
        grd_ShinHo.Rows(31).Cells(0).Value = "TimeoutTime"
        grd_ShinHo.Rows(32).Cells(0).Value = "매도시간"
        grd_ShinHo.Rows(33).Cells(0).Value = "매도Index"
        grd_ShinHo.Rows(34).Cells(0).Value = "매도사유"
        grd_ShinHo.Rows(35).Cells(0).Value = "메모"
        grd_ShinHo.Rows(36).Cells(0).Value = "기준가격"
        grd_ShinHo.Rows(37).Cells(0).Value = "환산이익율"
        grd_ShinHo.Rows(38).Cells(0).Value = "실시간여부"
        grd_ShinHo.Rows(39).Cells(0).Value = "조건전체"
    End Sub

    'selectedgrid 초기화
    Private Sub InitDrawSelectedGird()
        Dim defaultWidth As Integer
        Dim i, j As Integer

        defaultWidth = 44

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
        grd_selected.Columns(5).Width = defaultWidth + 5
        grd_selected.Columns(6).Width = defaultWidth
        grd_selected.Columns(7).Width = defaultWidth
        grd_selected.Columns(8).Width = defaultWidth
        grd_selected.Columns(9).Width = defaultWidth
        grd_selected.Columns(10).Width = defaultWidth + 5
        grd_selected.Columns(11).Width = defaultWidth + 15
        grd_selected.Columns(12).Width = defaultWidth + 15

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
        grd_ShinHo.Rows(37).Cells(1).Value = Format(shinho.A46_환산이익율, "##0.00#")
        grd_ShinHo.Rows(38).Cells(1).Value = shinho.A47_IsReal.ToString()
        grd_ShinHo.Rows(39).Cells(1).Value = shinho.A48_조건전체

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
            grd_ShinHo.Rows(37).Cells(1).Style.BackColor = Color.LightGreen
            grd_ShinHo.Rows(37).Cells(1).Style.ForeColor = Color.Black
        Else
            grd_ShinHo.Rows(25).Cells(1).Style.BackColor = Color.LightGreen
            grd_ShinHo.Rows(25).Cells(1).Style.ForeColor = Color.Black
            grd_ShinHo.Rows(37).Cells(1).Style.BackColor = Color.Yellow
            grd_ShinHo.Rows(37).Cells(1).Style.ForeColor = Color.Red

        End If

        grd_ShinHo.Refresh()

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

        If selectedIndex > 0 And selectedJongmokIndex(0) <> selectedIndex - 1 Then

            selectedJongmokIndex(0) = selectedIndex - 1
            chk_ChangeTargetIndex.Checked = False 'Clac_DisplayAllGrid에서 또 자동으로 selected를 계산하는 걸 방지하기 위해 false로 바꾼다
            Clac_DisplayAllGrid()
            Add_Log("일반", "cmb_selectedJongmokIndex_0_SelectedIndexChanged  호출됨")
        End If
    End Sub

    Private Sub cmb_selectedJongmokIndex_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_selectedJongmokIndex_1.SelectedIndexChanged

        Dim selectedIndex = cmb_selectedJongmokIndex_1.SelectedIndex

        If selectedIndex > 0 And selectedJongmokIndex(1) <> selectedIndex - 1 Then

            selectedJongmokIndex(1) = selectedIndex - 1
            chk_ChangeTargetIndex.Checked = False
            Clac_DisplayAllGrid()
            Add_Log("일반", "cmb_selectedJongmokIndex_1_SelectedIndexChanged  호출됨")
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        timerCount = timerCount + 1
        label_timerCounter.Text = timerCount.ToString()

        If timerCount >= timerMaxInterval Then

            timerCount = 0

            '계좌정보 가져오기
            If EBESTisConntected = True Then 계좌조회()

            '옵션 정보 가져와서 화면 표시
            GetAllData()
            Clac_DisplayAllGrid()

            'DB에 자동 저장 기능 추가 필요

        End If


    End Sub

    '계좌정보를 갖고오고 난 후 2초 후에 Timer를 통해 구매가능개수를 가져온다 - 아마 1초에 1개만 조회 가능한 듯

    Private Sub Timer구매가능개수찾기_Tick(sender As Object, e As EventArgs) Handles Timer구매가능개수찾기.Tick

        Dim tempIndex As Integer = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다
        If tempIndex >= 0 Then
            Dim callcode As String = Data(selectedJongmokIndex(0)).Code(0)
            Dim callprice As Single = Data(selectedJongmokIndex(0)).price(0, tempIndex, 3)

            구매가능수량조회(callcode, callprice)

            If Timer구매가능개수찾기_2.Enabled = False Then
                Timer구매가능개수찾기_2.Interval = 2000
                Timer구매가능개수찾기_2.Enabled = True
                Console.WriteLine("타이머 2번 스타트")
            End If
        End If


        Timer구매가능개수찾기.Enabled = False

    End Sub

    Private Sub Timer구매가능개수찾기_2_Tick(sender As Object, e As EventArgs) Handles Timer구매가능개수찾기_2.Tick

        Dim tempIndex As Integer = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다
        If tempIndex >= 0 Then
            Dim putcode As String = Data(selectedJongmokIndex(1)).Code(1)
            Dim putprice As Single = Data(selectedJongmokIndex(1)).price(1, tempIndex, 3)

            구매가능수량조회(putcode, putprice)
            선물옵션_잔고평가_이동평균조회()
        End If


        Timer구매가능개수찾기_2.Enabled = False

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

        Dim today As Date = Now()
        Dim strToday As String = Format(today, "yyMMdd")
        Dim lDate As Long = Val(strToday)
        Dim 월물 As Long = getsMonth(lDate)
        Dim 남은날짜 As Integer = getRemainDate(월물.ToString(), lDate)

        If 남은날짜 <= 3 Then  '남은날짜가 작으면 0.15로 바꾼다
            txt_손절매비율.Text = "1.2"
            Label15.Text = "손절매비율(3일이하)"
        ElseIf 남은날짜 > 3 And 남은날짜 <= 10 Then
            txt_손절매비율.Text = "1.17"
            Label15.Text = "손절매비율(10일이하)"
        Else
            txt_손절매비율.Text = "1.14"
            Label15.Text = "손절매비율(10일초과)"
        End If

        txt_실험조건.Text = "A" + strToday

        currentIndex = -1

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
            sMonth = getsMonth(TargetDate).ToString() 'DB에서 읽은 날짜로부터 월물을 찾아낸다

            TotalCount = GetDataFromDBHandler(TargetDate)

            If TotalCount > 0 Then

                Clac_DisplayAllGrid()

            End If

        Else
            MsgBox("DB에 데이터가 없습니다")
        End If

    End Sub

    '    Private Sub grd_selected_Scroll(sender As Object, e As ScrollEventArgs) Handles grd_selected.Scroll
    '        grid1.FirstDisplayedScrollingRowIndex = grd_selected.FirstDisplayedScrollingRowIndex
    '    End Sub

    Private Sub Hscroll_1_ValueChanged(sender As Object, e As EventArgs) Handles Hscroll_1.ValueChanged
        If currentIndex >= 0 Then
            Dim value = Hscroll_1.Value
            If value <> currentIndex Then
                currentIndex = value
                Clac_DisplayAllGrid()
            End If
        End If
    End Sub

    Private Sub btn_당일반복_Click(sender As Object, e As EventArgs) Handles btn_당일반복.Click

        Dim i As Integer

        chk_화면끄기.Checked = True
        For i = 0 To timeIndex - 1
            currentIndex = i

            Clac_DisplayAllGrid()
        Next
        chk_화면끄기.Checked = False
        Clac_DisplayAllGrid()

        Add_Log("일반", "당일 자동반복 완료")
    End Sub

    Private Sub btn_동일조건반복_Click(sender As Object, e As EventArgs) Handles btn_동일조건반복.Click

        If SimulationTotalShinhoList Is Nothing Then
            SimulationTotalShinhoList = New List(Of ShinhoType)
        Else
            SimulationTotalShinhoList.Clear()
        End If

        자동반복계산로직(0)
        Add_Log("자동 반복 계산로직 완료", "")

    End Sub

    Private Sub 자동반복계산로직(ByVal cnt As Integer)

        isRealFlag = False

        For i As Integer = 0 To DBTotalDateCount - 1

            DBDate_HScrollBar.Value = i     ' 이 안에서도 Clac_DisplayAllGrid  호출하지만 그건 그날짜 data의 첫번째만 호출하는 것임
            DBDate_HScrollBar.Refresh()

            chk_화면끄기.Checked = True

            '당일 내부에서 변경
            For j As Integer = 0 To timeIndex - 1

                currentIndex = j
                If currentIndex = timeIndex - 1 Then chk_화면끄기.Checked = False
                Clac_DisplayAllGrid()
            Next

            '매일매일 신호리스트를 시뮬레이션전체신호리스트에 복사한다
            For j = 0 To ShinhoList.Count - 1
                SimulationTotalShinhoList.Add(ShinhoList(j))
            Next

            Threading.Thread.Sleep(50)

        Next

        '여기서 DB에 입력하면 완료됨. 만약 입력하면 반드시 clear할 것
        If SimulationTotalShinhoList.Count > 0 Then

            'Add_Log(cnt.ToString() + "차 자동계산완료", " : " + " Total 신호건수 = " + SimulationTotalShinhoList.Count.ToString())
            InsertShinhoResult()
            SimulationTotalShinhoList.Clear()

        End If

    End Sub


    Private Sub DBDate_HScrollBar_ValueChanged(sender As Object, e As EventArgs) Handles DBDate_HScrollBar.ValueChanged

        Dim newValue As Integer = DBDate_HScrollBar.Value

        If gTargetDateIndex <> newValue Then

            InitDataStructure()
            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다

            gTargetDateIndex = newValue

            TargetDate = DBDateList(gTargetDateIndex)
            'TotalCount = GetDailyRawData(TargetDate) '이걸하면 Data() 구조체에 해당하는 날짜의 data를 집어넣고 종목의 Count를 리턴한다

            TotalCount = GetDataFromDBHandler(TargetDate) '이걸하면 딕셔너리의 data에서 해당 날짜의 Data를 가져온다

            '날짜로부터 월물 계산하기
            sMonth = getsMonth(TargetDate).ToString()

            Dim 남은날짜 As Integer = getRemainDate(sMonth, TargetDate)

            If 남은날짜 <= 3 Then  '남은날짜가 작으면 0.15로 바꾼다
                txt_손절매비율.Text = "1.2"
                Label15.Text = "손절매비율(3일이하)"
            ElseIf 남은날짜 > 3 And 남은날짜 <= 10 Then
                txt_손절매비율.Text = "1.16"
                Label15.Text = "손절매비율(10일이하)"
            Else
                txt_손절매비율.Text = "1.12"
                Label15.Text = "손절매비율(10일초과)"
            End If

            txt_손절매비율.Refresh()

            If TotalCount > 0 Then

                Clac_DisplayAllGrid()

            End If

        End If

    End Sub

    Private Sub btn_전체조건반복_Click(sender As Object, eee As EventArgs) Handles btn_전체조건반복.Click

        Dim a, b, cnt As Integer

        Dim 손절비율() As String = {"1.12", "1.14", "1.16", "1.18", "1.20", "1.22", "1.24", "1.26"}
        Dim 익절비율() As String = {"0.3", "0.76"}
        Dim 발생Index() As String = {"0", "1"}
        Dim TimeoutTime() As String = {"1520", "1525"}
        'Dim 기준가격() As String = {"2.3", "2.6", "2.9"}

        If SimulationTotalShinhoList Is Nothing Then
            SimulationTotalShinhoList = New List(Of ShinhoType)
        Else
            SimulationTotalShinhoList.Clear()
        End If

        cnt = 0

        For a = 0 To 발생Index.Length - 1
            For b = 0 To TimeoutTime.Length - 1

                'txt_손절매비율.Text = 손절비율(a)
                'txt_익절목표.Text = 익절비율(b)

                txt_양매도Target시간Index.Text = 발생Index(a)
                txt_신호TimeOut시간.Text = TimeoutTime(b)
                Simulation_조건 = "cnt_" + cnt.ToString() + "_occur_" + 발생Index(a) + "_expire_" + TimeoutTime(b)

                'Simulation_조건 = "cnt_" + cnt.ToString() + "_son_" + 손절비율(a) + "_ik_" + 익절비율(b) + "_Index_" + 발생Index(c) + "_Timeout_" + TimeoutTime(d) + "_price_" + 기준가격(e)
                'Simulation_조건 = "cnt_" + cnt.ToString() + "_son_" + 손절비율(a) + "_ik_" + 익절비율(b)

                Console.WriteLine(Simulation_조건)
                Add_Log("시뮬레이션 진행 : ", Simulation_조건)
                자동반복계산로직(cnt)
                cnt += 1

            Next
        Next

        Simulation_조건 = ""

    End Sub

    Private Sub btn_이베스트로그인_Click(sender As Object, e As EventArgs) Handles btn_이베스트로그인.Click
        이베스트로그인함수()
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
            grd_잔고조회.RowCount = List잔고.Count + 1
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
        grd_잔고조회.Columns(12).HeaderText = "수익율"

        Dim defaultWidth As Integer = 68
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
            grd_잔고조회.Rows(i).Cells(12).Value = List잔고(i).A13_수익율
        Next

        If List잔고.Count = 2 Then

            grd_잔고조회.Rows(2).Cells(5).Value = Format(List잔고(0).A06_총매입금액 + List잔고(1).A06_총매입금액, "###,###,###,##0")
            grd_잔고조회.Rows(2).Cells(11).Value = Format(List잔고(0).A12_평가손익 + List잔고(1).A12_평가손익, "###,###,###,##0")
            Dim 수익율 As Single = (List잔고(0).A12_평가손익 + List잔고(1).A12_평가손익) / (List잔고(0).A06_총매입금액 + List잔고(1).A06_총매입금액)
            grd_잔고조회.Rows(2).Cells(12).Value = Format(수익율, "##0.0%")

            If 수익율 > 0 Then
                grd_잔고조회.Rows(2).Cells(12).Style.BackColor = Color.Yellow
                grd_잔고조회.Rows(2).Cells(12).Style.ForeColor = Color.Red
            Else
                grd_잔고조회.Rows(2).Cells(12).Style.BackColor = Color.LightGreen
                grd_잔고조회.Rows(2).Cells(12).Style.ForeColor = Color.Black
            End If
        End If

    End Sub

    Private Sub btn_call_매도_Click(sender As Object, e As EventArgs) Handles btn_call_매도.Click

        If 진짜할건지확인() = False Then Return
        If 매도실행호출(0) = False Then Add_Log("일반", "매도 시 최소구매가능개수 부족. 방향 = 콜")

    End Sub

    Private Sub btn_put_매도_Click(sender As Object, e As EventArgs) Handles btn_put_매도.Click

        If 진짜할건지확인() = False Then Return
        If 매도실행호출(1) = False Then Add_Log("일반", "매도 시 최소구매가능개수 부족. 방향 = 풋")

    End Sub

    Private Sub btn_call_구매가능수_Click(sender As Object, e As EventArgs) Handles btn_call_구매가능수.Click
        Dim tempIndex As Integer = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다
        If tempIndex >= 0 Then
            Dim code As String = Data(selectedJongmokIndex(0)).Code(0)
            Dim price As Single = Data(selectedJongmokIndex(0)).price(0, tempIndex, 3)

            구매가능수량조회(code, price)
        End If
    End Sub

    Private Sub btn_put_구매가능수_Click(sender As Object, e As EventArgs) Handles btn_put_구매가능수.Click
        Dim tempIndex As Integer = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다
        If tempIndex >= 0 Then
            Dim code As String = Data(selectedJongmokIndex(1)).Code(1)
            Dim price As Single = Data(selectedJongmokIndex(1)).price(1, tempIndex, 3)
            구매가능수량조회(code, price)
        End If
    End Sub

    Private Sub btn_call_매수_Click(sender As Object, e As EventArgs) Handles btn_call_매수.Click

        If 진짜할건지확인() = False Then Return

        Add_Log("일반", "콜 환매수를 눌렀음 현재 잔고 종류의 갯수: " & List잔고.Count.ToString())

        For i As Integer = 0 To List잔고.Count - 1
            Dim it As 잔고Type = List잔고(i)
            Dim 종목구분 As String = Strings.Left(it.A01_종복번호, 1) '"2" - 콜, "3" - 풋
            If 종목구분 = "2" Then
                한종목매수(it.A01_종복번호, it.A10_현재가, it.A03_잔고수량)
            End If
        Next

    End Sub

    Private Sub btn_put_매수_Click(sender As Object, e As EventArgs) Handles btn_put_매수.Click

        If 진짜할건지확인() = False Then Return

        Add_Log("일반", "풋 환매수를 눌렀음 현재 잔고 종류의 갯수: " & List잔고.Count.ToString())

        For i As Integer = 0 To List잔고.Count - 1
            Dim it As 잔고Type = List잔고(i)
            Dim 종목구분 As String = Strings.Left(it.A01_종복번호, 1) '"2" - 콜, "3" - 풋
            If 종목구분 = "3" Then
                한종목매수(it.A01_종복번호, it.A10_현재가, it.A03_잔고수량)
            End If
        Next

    End Sub

    Private Sub btn_전체정리_Click(sender As Object, e As EventArgs) Handles btn_전체정리.Click

        If 진짜할건지확인() = False Then Return

        Add_Log("일반", "전체 환매수를 눌렀음 현재 잔고의 갯수: " & List잔고.Count.ToString())

        For i As Integer = 0 To List잔고.Count - 1
            Dim it As 잔고Type = List잔고(i)
            한종목매수(it.A01_종복번호, it.A10_현재가, it.A03_잔고수량)
        Next
    End Sub

    Public Function 진짜할건지확인() As Boolean

        Dim dr As DialogResult
        dr = MessageBox.Show("진짜 매매할건가?", "매매여부", MessageBoxButtons.YesNo)

        If dr = DialogResult.No Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btn_인덱스업_Click(sender As Object, e As EventArgs) Handles btn_인덱스업.Click
        Dim 현재양매도Index As Integer = Val(txt_양매도Target시간Index.Text)
        If 현재양매도Index < 30 Then
            Dim 신규양매도index As Integer = 현재양매도Index + 1
            txt_양매도Target시간Index.Text = 신규양매도index.ToString()
        End If
    End Sub

    Private Sub btn_인덱스다운_Click(sender As Object, e As EventArgs) Handles btn_인덱스다운.Click
        Dim 현재양매도Index As Integer = Val(txt_양매도Target시간Index.Text)
        If 현재양매도Index > 0 Then
            Dim 신규양매도index As Integer = 현재양매도Index - 1
            txt_양매도Target시간Index.Text = 신규양매도index.ToString()
        End If
    End Sub

    Private Sub btn_아침시작버튼_Click(sender As Object, e As EventArgs) Handles btn_아침시작버튼.Click

        'Dim ret As Boolean
        'ret = realTime_Start()
        'txt_TargetDate.Text = TargetDate

        이베스트로그인함수()

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
End Class
