<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_InsertDB = New System.Windows.Forms.Button()
        Me.HSc_F2_날짜조절 = New System.Windows.Forms.HScrollBar()
        Me.Lbl_F2_현재시간Index = New System.Windows.Forms.Label()
        Me.Lbl_F2_현재날짜Index = New System.Windows.Forms.Label()
        Me.txt_F2_DB_Date_Limit = New System.Windows.Forms.TextBox()
        Me.txt_F2_TableName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.HSc_F2_시간조절 = New System.Windows.Forms.HScrollBar()
        Me.btn_F2_SelectDB = New System.Windows.Forms.Button()
        Me.F2_Chart_순매수 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.txt_TargetPointCount = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_점의수늘리기 = New System.Windows.Forms.Button()
        Me.btn_점의수줄이기 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_F2_PIP_CALC_MAX_INDEX = New System.Windows.Forms.RichTextBox()
        Me.txt_F2_마감시간이후기울기 = New System.Windows.Forms.RichTextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmb_F2_순매수기준 = New System.Windows.Forms.ComboBox()
        Me.txt_F2_상승하락기울기기준 = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_F2_최대포인트수 = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_선행_포인트_마진 = New System.Windows.Forms.RichTextBox()
        Me.grid_3 = New System.Windows.Forms.DataGridView()
        Me.grid_shinho = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_F2_매매신호 = New System.Windows.Forms.Label()
        Me.txt_F2_최종방향 = New System.Windows.Forms.RichTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_F2_매수시작시간 = New System.Windows.Forms.RichTextBox()
        Me.txt_F2_최초매매시작시간 = New System.Windows.Forms.RichTextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_F2_TimeoutTime = New System.Windows.Forms.RichTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_F2_매수마감시간 = New System.Windows.Forms.RichTextBox()
        Me.txt_F2_익절차 = New System.Windows.Forms.RichTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_F2_손절매차 = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_F2_전체조건반복 = New System.Windows.Forms.Button()
        Me.btn_F2_동일조건반복 = New System.Windows.Forms.Button()
        Me.chk_F2_화면끄기 = New System.Windows.Forms.CheckBox()
        Me.btn_당일반복 = New System.Windows.Forms.Button()
        Me.txt_F2_실험조건 = New System.Windows.Forms.RichTextBox()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_ebest_id1 = New System.Windows.Forms.TextBox()
        Me.txt_ebest인증비밀번호 = New System.Windows.Forms.TextBox()
        Me.txt_ebest_pwd = New System.Windows.Forms.TextBox()
        Me.chk_모의투자연결 = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btn_이베스트로그인 = New System.Windows.Forms.Button()
        Me.label_timerCounter = New System.Windows.Forms.Label()
        Me.btn_TimerStart = New System.Windows.Forms.Button()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_week_정규 = New System.Windows.Forms.RichTextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txt_월물 = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmb_selectedJongmokIndex_1 = New System.Windows.Forms.ComboBox()
        Me.lbl_1 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmb_selectedJongmokIndex_0 = New System.Windows.Forms.ComboBox()
        Me.grid1 = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_JongmokTargetPrice = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txt_LowerLimit = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txt_UpperLimit = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txt_Interval = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txt_TargetDate = New System.Windows.Forms.TextBox()
        Me.chk_ChangeTargetIndex = New System.Windows.Forms.CheckBox()
        Me.txt_programversion = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_중간청산비율 = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.chk_중간청산 = New System.Windows.Forms.CheckBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txt_F2_1회최대매매수량 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.chk_실거래실행 = New System.Windows.Forms.CheckBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txt_투자비율 = New System.Windows.Forms.TextBox()
        Me.txt_Log = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lbl_계좌번호 = New System.Windows.Forms.Label()
        Me.lbl_평가손익 = New System.Windows.Forms.Label()
        Me.lbl_평가금액 = New System.Windows.Forms.Label()
        Me.lbl_매매손익합계 = New System.Windows.Forms.Label()
        Me.lbl_인출가능금액 = New System.Windows.Forms.Label()
        Me.lbl_주문가능금액 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txt_구매가능대비비율 = New System.Windows.Forms.RichTextBox()
        Me.grd_잔고조회 = New System.Windows.Forms.DataGridView()
        Me.TLP_BuySell = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_put_매도 = New System.Windows.Forms.Button()
        Me.btn_매수를청산 = New System.Windows.Forms.Button()
        Me.btn_put_구매가능수 = New System.Windows.Forms.Button()
        Me.btn_전체정리 = New System.Windows.Forms.Button()
        Me.btn_call_매도 = New System.Windows.Forms.Button()
        Me.btn_매도를청산 = New System.Windows.Forms.Button()
        Me.btn_call_구매가능수 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lbl_ReceiveCounter = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.F2_Chart_순매수, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.grid_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid_shinho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        CType(Me.grd_잔고조회, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLP_BuySell.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.40861!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.58065!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.50537!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.50537!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_InsertDB, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.HSc_F2_날짜조절, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Lbl_F2_현재시간Index, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Lbl_F2_현재날짜Index, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txt_F2_DB_Date_Limit, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txt_F2_TableName, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.HSc_F2_시간조절, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_F2_SelectDB, 2, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2627, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(645, 112)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'btn_InsertDB
        '
        Me.btn_InsertDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_InsertDB.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_InsertDB.Location = New System.Drawing.Point(225, 77)
        Me.btn_InsertDB.Name = "btn_InsertDB"
        Me.btn_InsertDB.Size = New System.Drawing.Size(137, 30)
        Me.btn_InsertDB.TabIndex = 38
        Me.btn_InsertDB.Text = "DB에 입력"
        Me.btn_InsertDB.UseVisualStyleBackColor = True
        '
        'HSc_F2_날짜조절
        '
        Me.HSc_F2_날짜조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_날짜조절.Location = New System.Drawing.Point(505, 38)
        Me.HSc_F2_날짜조절.Name = "HSc_F2_날짜조절"
        Me.HSc_F2_날짜조절.Size = New System.Drawing.Size(138, 34)
        Me.HSc_F2_날짜조절.TabIndex = 8
        '
        'Lbl_F2_현재시간Index
        '
        Me.Lbl_F2_현재시간Index.AutoSize = True
        Me.Lbl_F2_현재시간Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재시간Index.Location = New System.Drawing.Point(3, 75)
        Me.Lbl_F2_현재시간Index.Margin = New System.Windows.Forms.Padding(1)
        Me.Lbl_F2_현재시간Index.Name = "Lbl_F2_현재시간Index"
        Me.Lbl_F2_현재시간Index.Size = New System.Drawing.Size(216, 34)
        Me.Lbl_F2_현재시간Index.TabIndex = 6
        Me.Lbl_F2_현재시간Index.Text = "X건 중 Y"
        Me.Lbl_F2_현재시간Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lbl_F2_현재날짜Index
        '
        Me.Lbl_F2_현재날짜Index.AutoSize = True
        Me.Lbl_F2_현재날짜Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재날짜Index.Location = New System.Drawing.Point(508, 74)
        Me.Lbl_F2_현재날짜Index.Name = "Lbl_F2_현재날짜Index"
        Me.Lbl_F2_현재날짜Index.Size = New System.Drawing.Size(132, 36)
        Me.Lbl_F2_현재날짜Index.TabIndex = 4
        Me.Lbl_F2_현재날짜Index.Text = "X일 중 Y일"
        Me.Lbl_F2_현재날짜Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_DB_Date_Limit
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txt_F2_DB_Date_Limit, 3)
        Me.txt_F2_DB_Date_Limit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_DB_Date_Limit.Location = New System.Drawing.Point(225, 5)
        Me.txt_F2_DB_Date_Limit.Name = "txt_F2_DB_Date_Limit"
        Me.txt_F2_DB_Date_Limit.Size = New System.Drawing.Size(415, 21)
        Me.txt_F2_DB_Date_Limit.TabIndex = 0
        Me.txt_F2_DB_Date_Limit.Text = "where cdate >= 220801"
        '
        'txt_F2_TableName
        '
        Me.txt_F2_TableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_TableName.Location = New System.Drawing.Point(370, 41)
        Me.txt_F2_TableName.Name = "txt_F2_TableName"
        Me.txt_F2_TableName.Size = New System.Drawing.Size(130, 21)
        Me.txt_F2_TableName.TabIndex = 2
        Me.txt_F2_TableName.Text = "option_one_minute"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(225, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(137, 34)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "테이블명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HSc_F2_시간조절
        '
        Me.HSc_F2_시간조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_시간조절.Location = New System.Drawing.Point(2, 38)
        Me.HSc_F2_시간조절.Name = "HSc_F2_시간조절"
        Me.HSc_F2_시간조절.Size = New System.Drawing.Size(218, 34)
        Me.HSc_F2_시간조절.TabIndex = 5
        '
        'btn_F2_SelectDB
        '
        Me.btn_F2_SelectDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_SelectDB.Location = New System.Drawing.Point(370, 77)
        Me.btn_F2_SelectDB.Name = "btn_F2_SelectDB"
        Me.btn_F2_SelectDB.Size = New System.Drawing.Size(130, 30)
        Me.btn_F2_SelectDB.TabIndex = 7
        Me.btn_F2_SelectDB.Text = "DB_가져오기"
        Me.btn_F2_SelectDB.UseVisualStyleBackColor = True
        '
        'F2_Chart_순매수
        '
        ChartArea1.Name = "ChartArea1"
        Me.F2_Chart_순매수.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.F2_Chart_순매수.Legends.Add(Legend1)
        Me.F2_Chart_순매수.Location = New System.Drawing.Point(652, 205)
        Me.F2_Chart_순매수.Name = "F2_Chart_순매수"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.F2_Chart_순매수.Series.Add(Series1)
        Me.F2_Chart_순매수.Size = New System.Drawing.Size(1396, 635)
        Me.F2_Chart_순매수.TabIndex = 2
        Me.F2_Chart_순매수.Text = "Chart1"
        '
        'txt_TargetPointCount
        '
        Me.txt_TargetPointCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TargetPointCount.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_TargetPointCount.Location = New System.Drawing.Point(104, 3)
        Me.txt_TargetPointCount.Name = "txt_TargetPointCount"
        Me.txt_TargetPointCount.Size = New System.Drawing.Size(95, 29)
        Me.txt_TargetPointCount.TabIndex = 3
        Me.txt_TargetPointCount.Text = "5"
        Me.txt_TargetPointCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.btn_점의수늘리기, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_TargetPointCount, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_점의수줄이기, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(652, 6)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(304, 39)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'btn_점의수늘리기
        '
        Me.btn_점의수늘리기.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_점의수늘리기.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_점의수늘리기.Location = New System.Drawing.Point(205, 3)
        Me.btn_점의수늘리기.Name = "btn_점의수늘리기"
        Me.btn_점의수늘리기.Size = New System.Drawing.Size(96, 33)
        Me.btn_점의수늘리기.TabIndex = 5
        Me.btn_점의수늘리기.Text = "UP"
        Me.btn_점의수늘리기.UseVisualStyleBackColor = True
        '
        'btn_점의수줄이기
        '
        Me.btn_점의수줄이기.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_점의수줄이기.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_점의수줄이기.Location = New System.Drawing.Point(3, 3)
        Me.btn_점의수줄이기.Name = "btn_점의수줄이기"
        Me.btn_점의수줄이기.Size = New System.Drawing.Size(95, 33)
        Me.btn_점의수줄이기.TabIndex = 4
        Me.btn_점의수줄이기.Text = "DOWN"
        Me.btn_점의수줄이기.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.14285!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85715!))
        Me.TableLayoutPanel3.Controls.Add(Me.txt_F2_PIP_CALC_MAX_INDEX, 1, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_F2_마감시간이후기울기, 1, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.Label17, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.Label16, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.cmb_F2_순매수기준, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_F2_상승하락기울기기준, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_F2_최대포인트수, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label6, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_선행_포인트_마진, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2649, 849)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 6
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(331, 222)
        Me.TableLayoutPanel3.TabIndex = 6
        '
        'txt_F2_PIP_CALC_MAX_INDEX
        '
        Me.txt_F2_PIP_CALC_MAX_INDEX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_PIP_CALC_MAX_INDEX.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_PIP_CALC_MAX_INDEX.Location = New System.Drawing.Point(192, 185)
        Me.txt_F2_PIP_CALC_MAX_INDEX.Name = "txt_F2_PIP_CALC_MAX_INDEX"
        Me.txt_F2_PIP_CALC_MAX_INDEX.Size = New System.Drawing.Size(134, 32)
        Me.txt_F2_PIP_CALC_MAX_INDEX.TabIndex = 15
        Me.txt_F2_PIP_CALC_MAX_INDEX.Text = "190"
        '
        'txt_F2_마감시간이후기울기
        '
        Me.txt_F2_마감시간이후기울기.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_마감시간이후기울기.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_마감시간이후기울기.Location = New System.Drawing.Point(192, 149)
        Me.txt_F2_마감시간이후기울기.Name = "txt_F2_마감시간이후기울기"
        Me.txt_F2_마감시간이후기울기.Size = New System.Drawing.Size(134, 28)
        Me.txt_F2_마감시간이후기울기.TabIndex = 14
        Me.txt_F2_마감시간이후기울기.Text = "10.0"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label17.Location = New System.Drawing.Point(3, 183)
        Me.Label17.Margin = New System.Windows.Forms.Padding(1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(183, 36)
        Me.Label17.TabIndex = 13
        Me.Label17.Text = "기준선의 최대길이"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label16.Location = New System.Drawing.Point(3, 147)
        Me.Label16.Margin = New System.Windows.Forms.Padding(1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(183, 32)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "마감시간 이후 기울기"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_F2_순매수기준
        '
        Me.cmb_F2_순매수기준.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_F2_순매수기준.FormattingEnabled = True
        Me.cmb_F2_순매수기준.Location = New System.Drawing.Point(192, 41)
        Me.cmb_F2_순매수기준.Name = "cmb_F2_순매수기준"
        Me.cmb_F2_순매수기준.Size = New System.Drawing.Size(134, 20)
        Me.cmb_F2_순매수기준.TabIndex = 11
        '
        'txt_F2_상승하락기울기기준
        '
        Me.txt_F2_상승하락기울기기준.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_상승하락기울기기준.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_상승하락기울기기준.Location = New System.Drawing.Point(192, 113)
        Me.txt_F2_상승하락기울기기준.Name = "txt_F2_상승하락기울기기준"
        Me.txt_F2_상승하락기울기기준.Size = New System.Drawing.Size(134, 28)
        Me.txt_F2_상승하락기울기기준.TabIndex = 9
        Me.txt_F2_상승하락기울기기준.Text = "4.0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 111)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(183, 32)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "상승하락 기울기 기준"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_최대포인트수
        '
        Me.txt_F2_최대포인트수.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_최대포인트수.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_최대포인트수.Location = New System.Drawing.Point(192, 77)
        Me.txt_F2_최대포인트수.Name = "txt_F2_최대포인트수"
        Me.txt_F2_최대포인트수.Size = New System.Drawing.Size(134, 28)
        Me.txt_F2_최대포인트수.TabIndex = 7
        Me.txt_F2_최대포인트수.Text = "6"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(3, 75)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(183, 32)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "최대포인트수"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 39)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(183, 32)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "순매수 판정기준"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(183, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "선행포인트수 마진"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_선행_포인트_마진
        '
        Me.txt_선행_포인트_마진.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_선행_포인트_마진.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_선행_포인트_마진.Location = New System.Drawing.Point(192, 5)
        Me.txt_선행_포인트_마진.Name = "txt_선행_포인트_마진"
        Me.txt_선행_포인트_마진.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_선행_포인트_마진.Size = New System.Drawing.Size(134, 28)
        Me.txt_선행_포인트_마진.TabIndex = 5
        Me.txt_선행_포인트_마진.Text = "0.90"
        '
        'grid_3
        '
        Me.grid_3.AllowUserToAddRows = False
        Me.grid_3.AllowUserToDeleteRows = False
        Me.grid_3.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grid_3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid_3.Location = New System.Drawing.Point(652, 45)
        Me.grid_3.Name = "grid_3"
        Me.grid_3.ReadOnly = True
        Me.grid_3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid_3.RowTemplate.Height = 23
        Me.grid_3.Size = New System.Drawing.Size(625, 154)
        Me.grid_3.TabIndex = 7
        '
        'grid_shinho
        '
        Me.grid_shinho.AllowUserToAddRows = False
        Me.grid_shinho.AllowUserToDeleteRows = False
        Me.grid_shinho.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grid_shinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid_shinho.Location = New System.Drawing.Point(648, 846)
        Me.grid_shinho.Name = "grid_shinho"
        Me.grid_shinho.ReadOnly = True
        Me.grid_shinho.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid_shinho.RowTemplate.Height = 23
        Me.grid_shinho.Size = New System.Drawing.Size(1997, 233)
        Me.grid_shinho.TabIndex = 8
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_F2_매매신호, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_F2_최종방향, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label8, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label9, 0, 0)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(1283, 45)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 4
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(318, 154)
        Me.TableLayoutPanel4.TabIndex = 9
        '
        'lbl_F2_매매신호
        '
        Me.lbl_F2_매매신호.AutoSize = True
        Me.lbl_F2_매매신호.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lbl_F2_매매신호.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_F2_매매신호.Font = New System.Drawing.Font("굴림", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_F2_매매신호.Location = New System.Drawing.Point(214, 81)
        Me.lbl_F2_매매신호.Margin = New System.Windows.Forms.Padding(3)
        Me.lbl_F2_매매신호.Name = "lbl_F2_매매신호"
        Me.lbl_F2_매매신호.Size = New System.Drawing.Size(99, 30)
        Me.lbl_F2_매매신호.TabIndex = 10
        Me.lbl_F2_매매신호.Text = "0"
        Me.lbl_F2_매매신호.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_최종방향
        '
        Me.txt_F2_최종방향.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_최종방향.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_최종방향.Location = New System.Drawing.Point(214, 119)
        Me.txt_F2_최종방향.Name = "txt_F2_최종방향"
        Me.txt_F2_최종방향.Size = New System.Drawing.Size(99, 30)
        Me.txt_F2_최종방향.TabIndex = 9
        Me.txt_F2_최종방향.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(3, 117)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(205, 34)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "최종방향"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(3, 79)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(205, 34)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "매매 신호"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(3, 41)
        Me.Label8.Margin = New System.Windows.Forms.Padding(1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(205, 34)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "-------------"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Margin = New System.Windows.Forms.Padding(1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(205, 34)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "----------"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_매수시작시간, 1, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_최초매매시작시간, 1, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.Label15, 0, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.Label14, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_TimeoutTime, 1, 5)
        Me.TableLayoutPanel5.Controls.Add(Me.Label10, 0, 5)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_매수마감시간, 1, 4)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_익절차, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.Label11, 0, 4)
        Me.TableLayoutPanel5.Controls.Add(Me.Label12, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.Label13, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_손절매차, 1, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(2986, 849)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 6
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(292, 222)
        Me.TableLayoutPanel5.TabIndex = 10
        '
        'txt_F2_매수시작시간
        '
        Me.txt_F2_매수시작시간.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_매수시작시간.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_매수시작시간.Location = New System.Drawing.Point(197, 113)
        Me.txt_F2_매수시작시간.Name = "txt_F2_매수시작시간"
        Me.txt_F2_매수시작시간.Size = New System.Drawing.Size(90, 28)
        Me.txt_F2_매수시작시간.TabIndex = 13
        Me.txt_F2_매수시작시간.Text = "101000"
        '
        'txt_F2_최초매매시작시간
        '
        Me.txt_F2_최초매매시작시간.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_최초매매시작시간.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_최초매매시작시간.Location = New System.Drawing.Point(197, 77)
        Me.txt_F2_최초매매시작시간.Name = "txt_F2_최초매매시작시간"
        Me.txt_F2_최초매매시작시간.Size = New System.Drawing.Size(90, 28)
        Me.txt_F2_최초매매시작시간.TabIndex = 12
        Me.txt_F2_최초매매시작시간.Text = "90500"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Location = New System.Drawing.Point(3, 111)
        Me.Label15.Margin = New System.Windows.Forms.Padding(1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(188, 32)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "매수시작시간"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Location = New System.Drawing.Point(3, 75)
        Me.Label14.Margin = New System.Windows.Forms.Padding(1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(188, 32)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "최초매매시작시간"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_TimeoutTime
        '
        Me.txt_F2_TimeoutTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_TimeoutTime.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_TimeoutTime.Location = New System.Drawing.Point(197, 185)
        Me.txt_F2_TimeoutTime.Name = "txt_F2_TimeoutTime"
        Me.txt_F2_TimeoutTime.Size = New System.Drawing.Size(90, 32)
        Me.txt_F2_TimeoutTime.TabIndex = 9
        Me.txt_F2_TimeoutTime.Text = "151500"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Location = New System.Drawing.Point(3, 183)
        Me.Label10.Margin = New System.Windows.Forms.Padding(1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(188, 36)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Timeout 시간"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_매수마감시간
        '
        Me.txt_F2_매수마감시간.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_매수마감시간.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_매수마감시간.Location = New System.Drawing.Point(197, 149)
        Me.txt_F2_매수마감시간.Name = "txt_F2_매수마감시간"
        Me.txt_F2_매수마감시간.Size = New System.Drawing.Size(90, 28)
        Me.txt_F2_매수마감시간.TabIndex = 7
        Me.txt_F2_매수마감시간.Text = "113000"
        '
        'txt_F2_익절차
        '
        Me.txt_F2_익절차.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_익절차.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_익절차.Location = New System.Drawing.Point(197, 41)
        Me.txt_F2_익절차.Name = "txt_F2_익절차"
        Me.txt_F2_익절차.Size = New System.Drawing.Size(90, 28)
        Me.txt_F2_익절차.TabIndex = 6
        Me.txt_F2_익절차.Text = "16"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Location = New System.Drawing.Point(3, 147)
        Me.Label11.Margin = New System.Windows.Forms.Padding(1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(188, 32)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "매수마감시간"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Location = New System.Drawing.Point(3, 39)
        Me.Label12.Margin = New System.Windows.Forms.Padding(1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(188, 32)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "종합주가지수 익절차"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label13.Location = New System.Drawing.Point(3, 3)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(188, 32)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "종합주가지수 손절매차"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_손절매차
        '
        Me.txt_F2_손절매차.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_손절매차.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_손절매차.Location = New System.Drawing.Point(197, 5)
        Me.txt_F2_손절매차.Name = "txt_F2_손절매차"
        Me.txt_F2_손절매차.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_F2_손절매차.Size = New System.Drawing.Size(90, 28)
        Me.txt_F2_손절매차.TabIndex = 5
        Me.txt_F2_손절매차.Text = "9"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.9004!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.27888!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.82072!))
        Me.TableLayoutPanel6.Controls.Add(Me.btn_F2_전체조건반복, 2, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.btn_F2_동일조건반복, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.chk_F2_화면끄기, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btn_당일반복, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.txt_F2_실험조건, 2, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(2627, 130)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.75703!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.24297!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(645, 73)
        Me.TableLayoutPanel6.TabIndex = 19
        '
        'btn_F2_전체조건반복
        '
        Me.btn_F2_전체조건반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_전체조건반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_F2_전체조건반복.Location = New System.Drawing.Point(359, 35)
        Me.btn_F2_전체조건반복.Name = "btn_F2_전체조건반복"
        Me.btn_F2_전체조건반복.Size = New System.Drawing.Size(282, 34)
        Me.btn_F2_전체조건반복.TabIndex = 5
        Me.btn_F2_전체조건반복.Text = "전체조건 반복"
        Me.btn_F2_전체조건반복.UseVisualStyleBackColor = True
        '
        'btn_F2_동일조건반복
        '
        Me.btn_F2_동일조건반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_동일조건반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_F2_동일조건반복.Location = New System.Drawing.Point(164, 35)
        Me.btn_F2_동일조건반복.Name = "btn_F2_동일조건반복"
        Me.btn_F2_동일조건반복.Size = New System.Drawing.Size(188, 34)
        Me.btn_F2_동일조건반복.TabIndex = 4
        Me.btn_F2_동일조건반복.Text = "동일조건반복"
        Me.btn_F2_동일조건반복.UseVisualStyleBackColor = True
        '
        'chk_F2_화면끄기
        '
        Me.chk_F2_화면끄기.AutoSize = True
        Me.chk_F2_화면끄기.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_F2_화면끄기.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_F2_화면끄기.Location = New System.Drawing.Point(4, 4)
        Me.chk_F2_화면끄기.Name = "chk_F2_화면끄기"
        Me.chk_F2_화면끄기.Size = New System.Drawing.Size(153, 24)
        Me.chk_F2_화면끄기.TabIndex = 0
        Me.chk_F2_화면끄기.Text = "화면끄기"
        Me.chk_F2_화면끄기.UseVisualStyleBackColor = True
        '
        'btn_당일반복
        '
        Me.btn_당일반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_당일반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_당일반복.Location = New System.Drawing.Point(4, 35)
        Me.btn_당일반복.Name = "btn_당일반복"
        Me.btn_당일반복.Size = New System.Drawing.Size(153, 34)
        Me.btn_당일반복.TabIndex = 1
        Me.btn_당일반복.Text = "당일반복"
        Me.btn_당일반복.UseVisualStyleBackColor = True
        '
        'txt_F2_실험조건
        '
        Me.txt_F2_실험조건.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_실험조건.Location = New System.Drawing.Point(359, 4)
        Me.txt_F2_실험조건.Name = "txt_F2_실험조건"
        Me.txt_F2_실험조건.Size = New System.Drawing.Size(282, 24)
        Me.txt_F2_실험조건.TabIndex = 7
        Me.txt_F2_실험조건.Text = ""
        '
        'Chart1
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend2)
        Me.Chart1.Location = New System.Drawing.Point(2054, 205)
        Me.Chart1.Name = "Chart1"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Size = New System.Drawing.Size(1229, 635)
        Me.Chart1.TabIndex = 20
        Me.Chart1.Text = "Chart1"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(2502, 114)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(109, 32)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(2502, 154)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(109, 30)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel7.ColumnCount = 3
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.87754!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.06856!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.0539!))
        Me.TableLayoutPanel7.Controls.Add(Me.txt_ebest_id1, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txt_ebest인증비밀번호, 1, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.txt_ebest_pwd, 1, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.chk_모의투자연결, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label21, 0, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.Label18, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.Label19, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.btn_이베스트로그인, 2, 1)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(126, 15)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(500, 135)
        Me.TableLayoutPanel7.TabIndex = 23
        '
        'txt_ebest_id1
        '
        Me.txt_ebest_id1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_ebest_id1.Location = New System.Drawing.Point(148, 4)
        Me.txt_ebest_id1.Name = "txt_ebest_id1"
        Me.txt_ebest_id1.Size = New System.Drawing.Size(138, 21)
        Me.txt_ebest_id1.TabIndex = 24
        Me.txt_ebest_id1.Text = "f92887"
        '
        'txt_ebest인증비밀번호
        '
        Me.txt_ebest인증비밀번호.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_ebest인증비밀번호.Location = New System.Drawing.Point(148, 92)
        Me.txt_ebest인증비밀번호.Name = "txt_ebest인증비밀번호"
        Me.txt_ebest인증비밀번호.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_ebest인증비밀번호.Size = New System.Drawing.Size(138, 21)
        Me.txt_ebest인증비밀번호.TabIndex = 23
        Me.txt_ebest인증비밀번호.Text = "youngsookim6059!"
        '
        'txt_ebest_pwd
        '
        Me.txt_ebest_pwd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_ebest_pwd.Location = New System.Drawing.Point(148, 48)
        Me.txt_ebest_pwd.Name = "txt_ebest_pwd"
        Me.txt_ebest_pwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_ebest_pwd.Size = New System.Drawing.Size(138, 21)
        Me.txt_ebest_pwd.TabIndex = 22
        Me.txt_ebest_pwd.Text = "kys6059!"
        '
        'chk_모의투자연결
        '
        Me.chk_모의투자연결.AutoSize = True
        Me.chk_모의투자연결.Checked = True
        Me.chk_모의투자연결.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_모의투자연결.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_모의투자연결.Location = New System.Drawing.Point(293, 4)
        Me.chk_모의투자연결.Name = "chk_모의투자연결"
        Me.chk_모의투자연결.Size = New System.Drawing.Size(203, 37)
        Me.chk_모의투자연결.TabIndex = 21
        Me.chk_모의투자연결.Text = "모의투자서버"
        Me.chk_모의투자연결.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Location = New System.Drawing.Point(4, 91)
        Me.Label21.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(137, 41)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "공인인증서 암호"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label18.Location = New System.Drawing.Point(4, 47)
        Me.Label18.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(137, 39)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "PWD"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label19.Location = New System.Drawing.Point(4, 3)
        Me.Label19.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(137, 39)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "ID"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btn_이베스트로그인
        '
        Me.btn_이베스트로그인.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btn_이베스트로그인.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_이베스트로그인.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_이베스트로그인.Location = New System.Drawing.Point(293, 48)
        Me.btn_이베스트로그인.Name = "btn_이베스트로그인"
        Me.TableLayoutPanel7.SetRowSpan(Me.btn_이베스트로그인, 2)
        Me.btn_이베스트로그인.Size = New System.Drawing.Size(203, 83)
        Me.btn_이베스트로그인.TabIndex = 20
        Me.btn_이베스트로그인.Text = "이베스트 시작버튼"
        Me.btn_이베스트로그인.UseVisualStyleBackColor = False
        '
        'label_timerCounter
        '
        Me.label_timerCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_timerCounter.Location = New System.Drawing.Point(31, 103)
        Me.label_timerCounter.Name = "label_timerCounter"
        Me.label_timerCounter.Size = New System.Drawing.Size(72, 21)
        Me.label_timerCounter.TabIndex = 25
        Me.label_timerCounter.Text = "0"
        Me.label_timerCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_TimerStart
        '
        Me.btn_TimerStart.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_TimerStart.Location = New System.Drawing.Point(12, 15)
        Me.btn_TimerStart.Name = "btn_TimerStart"
        Me.btn_TimerStart.Size = New System.Drawing.Size(108, 80)
        Me.btn_TimerStart.TabIndex = 24
        Me.btn_TimerStart.Text = "START"
        Me.btn_TimerStart.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 4
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.txt_week_정규, 3, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.Label28, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.Label25, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.txt_월물, 1, 0)
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(9, 154)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(617, 36)
        Me.TableLayoutPanel9.TabIndex = 27
        '
        'txt_week_정규
        '
        Me.txt_week_정규.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_week_정규.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_week_정규.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.txt_week_정규.Location = New System.Drawing.Point(465, 3)
        Me.txt_week_정규.Multiline = False
        Me.txt_week_정규.Name = "txt_week_정규"
        Me.txt_week_정규.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_week_정규.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_week_정규.Size = New System.Drawing.Size(149, 30)
        Me.txt_week_정규.TabIndex = 4
        Me.txt_week_정규.Text = ""
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label28.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label28.Location = New System.Drawing.Point(309, 1)
        Me.Label28.Margin = New System.Windows.Forms.Padding(1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(152, 34)
        Me.Label28.TabIndex = 2
        Me.Label28.Text = "Week/정규"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label25.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label25.Location = New System.Drawing.Point(1, 1)
        Me.Label25.Margin = New System.Windows.Forms.Padding(1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(152, 34)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "월물"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_월물
        '
        Me.txt_월물.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_월물.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_월물.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.txt_월물.Location = New System.Drawing.Point(157, 3)
        Me.txt_월물.Name = "txt_월물"
        Me.txt_월물.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_월물.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_월물.Size = New System.Drawing.Size(148, 30)
        Me.txt_월물.TabIndex = 3
        Me.txt_월물.Text = ""
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 4
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12121!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.87879!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12121!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.87879!))
        Me.TableLayoutPanel8.Controls.Add(Me.cmb_selectedJongmokIndex_1, 3, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_1, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label20, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.cmb_selectedJongmokIndex_0, 1, 0)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(10, 193)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(614, 36)
        Me.TableLayoutPanel8.TabIndex = 28
        '
        'cmb_selectedJongmokIndex_1
        '
        Me.cmb_selectedJongmokIndex_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_1.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_1.Location = New System.Drawing.Point(383, 3)
        Me.cmb_selectedJongmokIndex_1.Name = "cmb_selectedJongmokIndex_1"
        Me.cmb_selectedJongmokIndex_1.Size = New System.Drawing.Size(228, 23)
        Me.cmb_selectedJongmokIndex_1.TabIndex = 3
        '
        'lbl_1
        '
        Me.lbl_1.AutoSize = True
        Me.lbl_1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lbl_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_1.Location = New System.Drawing.Point(3, 0)
        Me.lbl_1.Name = "lbl_1"
        Me.lbl_1.Size = New System.Drawing.Size(68, 36)
        Me.lbl_1.TabIndex = 0
        Me.lbl_1.Text = "Call"
        Me.lbl_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label20.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label20.Location = New System.Drawing.Point(309, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(68, 36)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Put"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_selectedJongmokIndex_0
        '
        Me.cmb_selectedJongmokIndex_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_0.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_0.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_0.Location = New System.Drawing.Point(77, 3)
        Me.cmb_selectedJongmokIndex_0.Name = "cmb_selectedJongmokIndex_0"
        Me.cmb_selectedJongmokIndex_0.Size = New System.Drawing.Size(226, 23)
        Me.cmb_selectedJongmokIndex_0.TabIndex = 2
        '
        'grid1
        '
        Me.grid1.AllowUserToAddRows = False
        Me.grid1.AllowUserToDeleteRows = False
        Me.grid1.AllowUserToResizeColumns = False
        Me.grid1.AllowUserToResizeRows = False
        Me.grid1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid1.Location = New System.Drawing.Point(10, 233)
        Me.grid1.Margin = New System.Windows.Forms.Padding(1)
        Me.grid1.Name = "grid1"
        Me.grid1.ReadOnly = True
        Me.grid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid1.RowTemplate.Height = 23
        Me.grid1.ShowCellErrors = False
        Me.grid1.ShowCellToolTips = False
        Me.grid1.ShowEditingIcon = False
        Me.grid1.ShowRowErrors = False
        Me.grid1.Size = New System.Drawing.Size(630, 423)
        Me.grid1.TabIndex = 29
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.Label22, 0, 6)
        Me.TableLayoutPanel10.Controls.Add(Me.Label23, 0, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.Label24, 0, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.txt_JongmokTargetPrice, 1, 4)
        Me.TableLayoutPanel10.Controls.Add(Me.Label26, 0, 3)
        Me.TableLayoutPanel10.Controls.Add(Me.txt_LowerLimit, 1, 3)
        Me.TableLayoutPanel10.Controls.Add(Me.Label27, 0, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.txt_UpperLimit, 1, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.Label29, 0, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.txt_Interval, 1, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.Label30, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.txt_TargetDate, 1, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.chk_ChangeTargetIndex, 1, 5)
        Me.TableLayoutPanel10.Controls.Add(Me.txt_programversion, 1, 6)
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(9, 659)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 7
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28816!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(320, 246)
        Me.TableLayoutPanel10.TabIndex = 30
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 205)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(152, 40)
        Me.Label22.TabIndex = 12
        Me.Label22.Text = "프로그램 버전"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(4, 171)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(152, 33)
        Me.Label23.TabIndex = 10
        Me.Label23.Text = "기준종목 변경"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(4, 137)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(152, 33)
        Me.Label24.TabIndex = 8
        Me.Label24.Text = "기준가"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_JongmokTargetPrice
        '
        Me.txt_JongmokTargetPrice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_JongmokTargetPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JongmokTargetPrice.Location = New System.Drawing.Point(164, 141)
        Me.txt_JongmokTargetPrice.Name = "txt_JongmokTargetPrice"
        Me.txt_JongmokTargetPrice.Size = New System.Drawing.Size(151, 24)
        Me.txt_JongmokTargetPrice.TabIndex = 9
        Me.txt_JongmokTargetPrice.Text = "1.4"
        Me.txt_JongmokTargetPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(4, 103)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(152, 33)
        Me.Label26.TabIndex = 6
        Me.Label26.Text = "가격 하한"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_LowerLimit
        '
        Me.txt_LowerLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_LowerLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LowerLimit.Location = New System.Drawing.Point(164, 107)
        Me.txt_LowerLimit.Name = "txt_LowerLimit"
        Me.txt_LowerLimit.Size = New System.Drawing.Size(151, 24)
        Me.txt_LowerLimit.TabIndex = 7
        Me.txt_LowerLimit.Text = "0.05"
        Me.txt_LowerLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(4, 69)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(152, 33)
        Me.Label27.TabIndex = 4
        Me.Label27.Text = "가격 상한"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_UpperLimit
        '
        Me.txt_UpperLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_UpperLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UpperLimit.Location = New System.Drawing.Point(164, 73)
        Me.txt_UpperLimit.Name = "txt_UpperLimit"
        Me.txt_UpperLimit.Size = New System.Drawing.Size(151, 24)
        Me.txt_UpperLimit.TabIndex = 5
        Me.txt_UpperLimit.Text = "7.0"
        Me.txt_UpperLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(4, 35)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(152, 33)
        Me.Label29.TabIndex = 2
        Me.Label29.Text = "인터벌"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_Interval
        '
        Me.txt_Interval.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_Interval.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Interval.Location = New System.Drawing.Point(164, 39)
        Me.txt_Interval.Name = "txt_Interval"
        Me.txt_Interval.Size = New System.Drawing.Size(151, 24)
        Me.txt_Interval.TabIndex = 3
        Me.txt_Interval.Text = "1"
        Me.txt_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(4, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(152, 33)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "목표날짜"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_TargetDate
        '
        Me.txt_TargetDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TargetDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TargetDate.Location = New System.Drawing.Point(163, 4)
        Me.txt_TargetDate.Name = "txt_TargetDate"
        Me.txt_TargetDate.Size = New System.Drawing.Size(153, 24)
        Me.txt_TargetDate.TabIndex = 1
        Me.txt_TargetDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chk_ChangeTargetIndex
        '
        Me.chk_ChangeTargetIndex.AutoSize = True
        Me.chk_ChangeTargetIndex.Checked = True
        Me.chk_ChangeTargetIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_ChangeTargetIndex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_ChangeTargetIndex.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_ChangeTargetIndex.Location = New System.Drawing.Point(163, 174)
        Me.chk_ChangeTargetIndex.Name = "chk_ChangeTargetIndex"
        Me.chk_ChangeTargetIndex.Size = New System.Drawing.Size(153, 27)
        Me.chk_ChangeTargetIndex.TabIndex = 11
        Me.chk_ChangeTargetIndex.Text = "자동변경 허용"
        Me.chk_ChangeTargetIndex.UseVisualStyleBackColor = True
        '
        'txt_programversion
        '
        Me.txt_programversion.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_programversion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_programversion.Location = New System.Drawing.Point(164, 213)
        Me.txt_programversion.Name = "txt_programversion"
        Me.txt_programversion.Size = New System.Drawing.Size(151, 24)
        Me.txt_programversion.TabIndex = 13
        Me.txt_programversion.Text = "0.1.1_20221002"
        Me.txt_programversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454!))
        Me.TableLayoutPanel11.Controls.Add(Me.txt_중간청산비율, 1, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.Label35, 0, 2)
        Me.TableLayoutPanel11.Controls.Add(Me.chk_중간청산, 1, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.Label34, 0, 1)
        Me.TableLayoutPanel11.Controls.Add(Me.txt_F2_1회최대매매수량, 1, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.Label33, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.chk_실거래실행, 1, 5)
        Me.TableLayoutPanel11.Controls.Add(Me.Label31, 0, 6)
        Me.TableLayoutPanel11.Controls.Add(Me.Label32, 0, 5)
        Me.TableLayoutPanel11.Controls.Add(Me.txt_투자비율, 1, 6)
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(334, 660)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 7
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28816!))
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(306, 245)
        Me.TableLayoutPanel11.TabIndex = 31
        '
        'txt_중간청산비율
        '
        Me.txt_중간청산비율.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_중간청산비율.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_중간청산비율.Location = New System.Drawing.Point(170, 73)
        Me.txt_중간청산비율.Name = "txt_중간청산비율"
        Me.txt_중간청산비율.Size = New System.Drawing.Size(132, 24)
        Me.txt_중간청산비율.TabIndex = 21
        Me.txt_중간청산비율.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(2, 70)
        Me.Label35.Margin = New System.Windows.Forms.Padding(1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(163, 31)
        Me.Label35.TabIndex = 20
        Me.Label35.Text = "중간청산 비율"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chk_중간청산
        '
        Me.chk_중간청산.AutoSize = True
        Me.chk_중간청산.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_중간청산.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_중간청산.Location = New System.Drawing.Point(170, 38)
        Me.chk_중간청산.Name = "chk_중간청산"
        Me.chk_중간청산.Size = New System.Drawing.Size(132, 27)
        Me.chk_중간청산.TabIndex = 19
        Me.chk_중간청산.Text = "중간청산 허용"
        Me.chk_중간청산.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(2, 36)
        Me.Label34.Margin = New System.Windows.Forms.Padding(1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(163, 31)
        Me.Label34.TabIndex = 18
        Me.Label34.Text = "중간청산 허용"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_1회최대매매수량
        '
        Me.txt_F2_1회최대매매수량.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_1회최대매매수량.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_1회최대매매수량.Location = New System.Drawing.Point(170, 4)
        Me.txt_F2_1회최대매매수량.Name = "txt_F2_1회최대매매수량"
        Me.txt_F2_1회최대매매수량.Size = New System.Drawing.Size(132, 25)
        Me.txt_F2_1회최대매매수량.TabIndex = 16
        Me.txt_F2_1회최대매매수량.Text = "20"
        Me.txt_F2_1회최대매매수량.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(4, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(159, 33)
        Me.Label33.TabIndex = 15
        Me.Label33.Text = "1회최대매매수량"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chk_실거래실행
        '
        Me.chk_실거래실행.AutoSize = True
        Me.chk_실거래실행.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_실거래실행.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_실거래실행.Location = New System.Drawing.Point(170, 174)
        Me.chk_실거래실행.Name = "chk_실거래실행"
        Me.chk_실거래실행.Size = New System.Drawing.Size(132, 27)
        Me.chk_실거래실행.TabIndex = 13
        Me.chk_실거래실행.Text = "실거래 실행"
        Me.chk_실거래실행.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(4, 205)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(159, 39)
        Me.Label31.TabIndex = 12
        Me.Label31.Text = "투자비율"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(4, 171)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(159, 33)
        Me.Label32.TabIndex = 10
        Me.Label32.Text = "실거래 실행"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_투자비율
        '
        Me.txt_투자비율.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_투자비율.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_투자비율.Location = New System.Drawing.Point(170, 208)
        Me.txt_투자비율.Name = "txt_투자비율"
        Me.txt_투자비율.Size = New System.Drawing.Size(132, 25)
        Me.txt_투자비율.TabIndex = 14
        Me.txt_투자비율.Text = "1.0"
        Me.txt_투자비율.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Log
        '
        Me.txt_Log.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txt_Log.Location = New System.Drawing.Point(9, 906)
        Me.txt_Log.Multiline = True
        Me.txt_Log.Name = "txt_Log"
        Me.txt_Log.ReadOnly = True
        Me.txt_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Log.Size = New System.Drawing.Size(633, 348)
        Me.txt_Log.TabIndex = 32
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel12.ColumnCount = 7
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.35484!))
        Me.TableLayoutPanel12.Controls.Add(Me.Label37, 6, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.lbl_계좌번호, 6, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.lbl_평가손익, 5, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.lbl_평가금액, 4, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.lbl_매매손익합계, 3, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.lbl_인출가능금액, 2, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.lbl_주문가능금액, 1, 1)
        Me.TableLayoutPanel12.Controls.Add(Me.Label38, 5, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label39, 4, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label40, 3, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label41, 2, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label42, 1, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label43, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.txt_구매가능대비비율, 0, 1)
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(1403, 1085)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 2
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.51515!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.48485!))
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(1242, 62)
        Me.TableLayoutPanel12.TabIndex = 33
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label37.Location = New System.Drawing.Point(1000, 3)
        Me.Label37.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(238, 26)
        Me.Label37.TabIndex = 35
        Me.Label37.Text = "계좌번호"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_계좌번호
        '
        Me.lbl_계좌번호.AutoSize = True
        Me.lbl_계좌번호.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_계좌번호.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_계좌번호.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_계좌번호.Location = New System.Drawing.Point(1000, 34)
        Me.lbl_계좌번호.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_계좌번호.Name = "lbl_계좌번호"
        Me.lbl_계좌번호.Size = New System.Drawing.Size(238, 25)
        Me.lbl_계좌번호.TabIndex = 34
        Me.lbl_계좌번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_평가손익
        '
        Me.lbl_평가손익.AutoSize = True
        Me.lbl_평가손익.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_평가손익.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_평가손익.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_평가손익.Location = New System.Drawing.Point(834, 34)
        Me.lbl_평가손익.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_평가손익.Name = "lbl_평가손익"
        Me.lbl_평가손익.Size = New System.Drawing.Size(159, 25)
        Me.lbl_평가손익.TabIndex = 32
        Me.lbl_평가손익.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_평가금액
        '
        Me.lbl_평가금액.AutoSize = True
        Me.lbl_평가금액.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_평가금액.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_평가금액.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_평가금액.Location = New System.Drawing.Point(668, 34)
        Me.lbl_평가금액.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_평가금액.Name = "lbl_평가금액"
        Me.lbl_평가금액.Size = New System.Drawing.Size(159, 25)
        Me.lbl_평가금액.TabIndex = 31
        Me.lbl_평가금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_매매손익합계
        '
        Me.lbl_매매손익합계.AutoSize = True
        Me.lbl_매매손익합계.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_매매손익합계.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_매매손익합계.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_매매손익합계.Location = New System.Drawing.Point(502, 34)
        Me.lbl_매매손익합계.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_매매손익합계.Name = "lbl_매매손익합계"
        Me.lbl_매매손익합계.Size = New System.Drawing.Size(159, 25)
        Me.lbl_매매손익합계.TabIndex = 30
        Me.lbl_매매손익합계.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_인출가능금액
        '
        Me.lbl_인출가능금액.AutoSize = True
        Me.lbl_인출가능금액.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_인출가능금액.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_인출가능금액.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_인출가능금액.Location = New System.Drawing.Point(336, 34)
        Me.lbl_인출가능금액.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_인출가능금액.Name = "lbl_인출가능금액"
        Me.lbl_인출가능금액.Size = New System.Drawing.Size(159, 25)
        Me.lbl_인출가능금액.TabIndex = 29
        Me.lbl_인출가능금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_주문가능금액
        '
        Me.lbl_주문가능금액.AutoSize = True
        Me.lbl_주문가능금액.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_주문가능금액.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_주문가능금액.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_주문가능금액.Location = New System.Drawing.Point(170, 34)
        Me.lbl_주문가능금액.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_주문가능금액.Name = "lbl_주문가능금액"
        Me.lbl_주문가능금액.Size = New System.Drawing.Size(159, 25)
        Me.lbl_주문가능금액.TabIndex = 28
        Me.lbl_주문가능금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label38.Location = New System.Drawing.Point(834, 3)
        Me.Label38.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(159, 26)
        Me.Label38.TabIndex = 27
        Me.Label38.Text = "평가손익"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label39.Location = New System.Drawing.Point(668, 3)
        Me.Label39.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(159, 26)
        Me.Label39.TabIndex = 26
        Me.Label39.Text = "평가금액"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label40.Location = New System.Drawing.Point(502, 3)
        Me.Label40.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(159, 26)
        Me.Label40.TabIndex = 25
        Me.Label40.Text = "매매손익합계"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label41.Location = New System.Drawing.Point(336, 3)
        Me.Label41.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(159, 26)
        Me.Label41.TabIndex = 24
        Me.Label41.Text = "인출가능금액"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label42.Location = New System.Drawing.Point(170, 3)
        Me.Label42.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(159, 26)
        Me.Label42.TabIndex = 23
        Me.Label42.Text = "주문가능금액"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label43.Location = New System.Drawing.Point(4, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(159, 30)
        Me.Label43.TabIndex = 37
        Me.Label43.Text = "통신 1회당 매도비율"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_구매가능대비비율
        '
        Me.txt_구매가능대비비율.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_구매가능대비비율.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_구매가능대비비율.Location = New System.Drawing.Point(4, 35)
        Me.txt_구매가능대비비율.Name = "txt_구매가능대비비율"
        Me.txt_구매가능대비비율.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_구매가능대비비율.Size = New System.Drawing.Size(159, 23)
        Me.txt_구매가능대비비율.TabIndex = 38
        Me.txt_구매가능대비비율.Text = "0.4"
        '
        'grd_잔고조회
        '
        Me.grd_잔고조회.AllowUserToAddRows = False
        Me.grd_잔고조회.AllowUserToDeleteRows = False
        Me.grd_잔고조회.AllowUserToResizeColumns = False
        Me.grd_잔고조회.AllowUserToResizeRows = False
        Me.grd_잔고조회.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.grd_잔고조회.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grd_잔고조회.Location = New System.Drawing.Point(1407, 1162)
        Me.grd_잔고조회.Name = "grd_잔고조회"
        Me.grd_잔고조회.ReadOnly = True
        Me.grd_잔고조회.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grd_잔고조회.RowTemplate.Height = 23
        Me.grd_잔고조회.ShowCellErrors = False
        Me.grd_잔고조회.ShowCellToolTips = False
        Me.grd_잔고조회.ShowEditingIcon = False
        Me.grd_잔고조회.ShowRowErrors = False
        Me.grd_잔고조회.Size = New System.Drawing.Size(1242, 96)
        Me.grd_잔고조회.TabIndex = 34
        Me.grd_잔고조회.TabStop = False
        '
        'TLP_BuySell
        '
        Me.TLP_BuySell.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TLP_BuySell.ColumnCount = 4
        Me.TLP_BuySell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLP_BuySell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLP_BuySell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLP_BuySell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLP_BuySell.Controls.Add(Me.btn_put_매도, 2, 1)
        Me.TLP_BuySell.Controls.Add(Me.btn_매수를청산, 1, 1)
        Me.TLP_BuySell.Controls.Add(Me.btn_put_구매가능수, 0, 1)
        Me.TLP_BuySell.Controls.Add(Me.btn_전체정리, 3, 0)
        Me.TLP_BuySell.Controls.Add(Me.btn_call_매도, 2, 0)
        Me.TLP_BuySell.Controls.Add(Me.btn_매도를청산, 1, 0)
        Me.TLP_BuySell.Controls.Add(Me.btn_call_구매가능수, 0, 0)
        Me.TLP_BuySell.Location = New System.Drawing.Point(783, 1125)
        Me.TLP_BuySell.Name = "TLP_BuySell"
        Me.TLP_BuySell.RowCount = 2
        Me.TLP_BuySell.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLP_BuySell.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLP_BuySell.Size = New System.Drawing.Size(617, 125)
        Me.TLP_BuySell.TabIndex = 35
        '
        'btn_put_매도
        '
        Me.btn_put_매도.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_put_매도.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_put_매도.Location = New System.Drawing.Point(312, 66)
        Me.btn_put_매도.Name = "btn_put_매도"
        Me.btn_put_매도.Size = New System.Drawing.Size(147, 55)
        Me.btn_put_매도.TabIndex = 6
        Me.btn_put_매도.Text = "풋 매도"
        Me.btn_put_매도.UseVisualStyleBackColor = True
        '
        'btn_매수를청산
        '
        Me.btn_매수를청산.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_매수를청산.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_매수를청산.Location = New System.Drawing.Point(158, 66)
        Me.btn_매수를청산.Name = "btn_매수를청산"
        Me.btn_매수를청산.Size = New System.Drawing.Size(147, 55)
        Me.btn_매수를청산.TabIndex = 5
        Me.btn_매수를청산.Text = "매수를 청산"
        Me.btn_매수를청산.UseVisualStyleBackColor = True
        '
        'btn_put_구매가능수
        '
        Me.btn_put_구매가능수.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_put_구매가능수.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_put_구매가능수.Location = New System.Drawing.Point(4, 66)
        Me.btn_put_구매가능수.Name = "btn_put_구매가능수"
        Me.btn_put_구매가능수.Size = New System.Drawing.Size(147, 55)
        Me.btn_put_구매가능수.TabIndex = 4
        Me.btn_put_구매가능수.Text = "풋 구매가능"
        Me.btn_put_구매가능수.UseVisualStyleBackColor = True
        '
        'btn_전체정리
        '
        Me.btn_전체정리.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_전체정리.Font = New System.Drawing.Font("굴림", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_전체정리.Location = New System.Drawing.Point(466, 4)
        Me.btn_전체정리.Name = "btn_전체정리"
        Me.TLP_BuySell.SetRowSpan(Me.btn_전체정리, 2)
        Me.btn_전체정리.Size = New System.Drawing.Size(147, 117)
        Me.btn_전체정리.TabIndex = 3
        Me.btn_전체정리.Text = "전체 정리"
        Me.btn_전체정리.UseVisualStyleBackColor = True
        '
        'btn_call_매도
        '
        Me.btn_call_매도.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_call_매도.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_call_매도.Location = New System.Drawing.Point(312, 4)
        Me.btn_call_매도.Name = "btn_call_매도"
        Me.btn_call_매도.Size = New System.Drawing.Size(147, 55)
        Me.btn_call_매도.TabIndex = 2
        Me.btn_call_매도.Text = "콜 매도"
        Me.btn_call_매도.UseVisualStyleBackColor = True
        '
        'btn_매도를청산
        '
        Me.btn_매도를청산.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_매도를청산.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_매도를청산.Location = New System.Drawing.Point(158, 4)
        Me.btn_매도를청산.Name = "btn_매도를청산"
        Me.btn_매도를청산.Size = New System.Drawing.Size(147, 55)
        Me.btn_매도를청산.TabIndex = 1
        Me.btn_매도를청산.Text = "매도를 청산"
        Me.btn_매도를청산.UseVisualStyleBackColor = True
        '
        'btn_call_구매가능수
        '
        Me.btn_call_구매가능수.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_call_구매가능수.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_call_구매가능수.Location = New System.Drawing.Point(4, 4)
        Me.btn_call_구매가능수.Name = "btn_call_구매가능수"
        Me.btn_call_구매가능수.Size = New System.Drawing.Size(147, 55)
        Me.btn_call_구매가능수.TabIndex = 0
        Me.btn_call_구매가능수.Text = "콜 구매가능"
        Me.btn_call_구매가능수.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'lbl_ReceiveCounter
        '
        Me.lbl_ReceiveCounter.AutoSize = True
        Me.lbl_ReceiveCounter.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_ReceiveCounter.Location = New System.Drawing.Point(1618, 181)
        Me.lbl_ReceiveCounter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_ReceiveCounter.Name = "lbl_ReceiveCounter"
        Me.lbl_ReceiveCounter.Size = New System.Drawing.Size(15, 13)
        Me.lbl_ReceiveCounter.TabIndex = 37
        Me.lbl_ReceiveCounter.Text = "0"
        Me.lbl_ReceiveCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(3284, 1262)
        Me.Controls.Add(Me.lbl_ReceiveCounter)
        Me.Controls.Add(Me.TLP_BuySell)
        Me.Controls.Add(Me.grd_잔고조회)
        Me.Controls.Add(Me.TableLayoutPanel12)
        Me.Controls.Add(Me.txt_Log)
        Me.Controls.Add(Me.TableLayoutPanel11)
        Me.Controls.Add(Me.TableLayoutPanel10)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.TableLayoutPanel8)
        Me.Controls.Add(Me.TableLayoutPanel9)
        Me.Controls.Add(Me.label_timerCounter)
        Me.Controls.Add(Me.btn_TimerStart)
        Me.Controls.Add(Me.TableLayoutPanel7)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.TableLayoutPanel6)
        Me.Controls.Add(Me.TableLayoutPanel5)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.grid_shinho)
        Me.Controls.Add(Me.grid_3)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.F2_Chart_순매수)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Location = New System.Drawing.Point(10, 0)
        Me.Name = "Form2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "매수기능"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.F2_Chart_순매수, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.grid_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid_shinho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        CType(Me.grd_잔고조회, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLP_BuySell.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Lbl_F2_현재날짜Index As Label
    Friend WithEvents txt_F2_DB_Date_Limit As TextBox
    Friend WithEvents txt_F2_TableName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents HSc_F2_날짜조절 As HScrollBar
    Friend WithEvents Lbl_F2_현재시간Index As Label
    Friend WithEvents HSc_F2_시간조절 As HScrollBar
    Friend WithEvents btn_F2_SelectDB As Button
    Friend WithEvents F2_Chart_순매수 As DataVisualization.Charting.Chart
    Friend WithEvents txt_TargetPointCount As TextBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btn_점의수늘리기 As Button
    Friend WithEvents btn_점의수줄이기 As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_선행_포인트_마진 As RichTextBox
    Friend WithEvents txt_F2_최대포인트수 As RichTextBox
    Friend WithEvents grid_3 As DataGridView
    Friend WithEvents txt_F2_상승하락기울기기준 As RichTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents grid_shinho As DataGridView
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents txt_F2_최종방향 As RichTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents txt_F2_TimeoutTime As RichTextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_F2_매수마감시간 As RichTextBox
    Friend WithEvents txt_F2_익절차 As RichTextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txt_F2_손절매차 As RichTextBox
    Friend WithEvents cmb_F2_순매수기준 As ComboBox
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents btn_F2_전체조건반복 As Button
    Friend WithEvents btn_F2_동일조건반복 As Button
    Friend WithEvents chk_F2_화면끄기 As CheckBox
    Friend WithEvents btn_당일반복 As Button
    Friend WithEvents txt_F2_실험조건 As RichTextBox
    Friend WithEvents txt_F2_매수시작시간 As RichTextBox
    Friend WithEvents txt_F2_최초매매시작시간 As RichTextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txt_F2_PIP_CALC_MAX_INDEX As RichTextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents txt_F2_마감시간이후기울기 As RichTextBox
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents txt_ebest_id1 As TextBox
    Friend WithEvents txt_ebest인증비밀번호 As TextBox
    Friend WithEvents txt_ebest_pwd As TextBox
    Friend WithEvents chk_모의투자연결 As CheckBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents btn_이베스트로그인 As Button
    Friend WithEvents label_timerCounter As Label
    Friend WithEvents btn_TimerStart As Button
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents txt_week_정규 As RichTextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents txt_월물 As RichTextBox
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents cmb_selectedJongmokIndex_1 As ComboBox
    Friend WithEvents lbl_1 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents cmb_selectedJongmokIndex_0 As ComboBox
    Friend WithEvents grid1 As DataGridView
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_JongmokTargetPrice As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents txt_LowerLimit As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents txt_UpperLimit As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents txt_Interval As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents txt_TargetDate As TextBox
    Friend WithEvents chk_ChangeTargetIndex As CheckBox
    Friend WithEvents txt_programversion As TextBox
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents chk_실거래실행 As CheckBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents txt_투자비율 As TextBox
    Friend WithEvents txt_Log As TextBox
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents Label37 As Label
    Friend WithEvents lbl_계좌번호 As Label
    Friend WithEvents lbl_평가손익 As Label
    Friend WithEvents lbl_평가금액 As Label
    Friend WithEvents lbl_매매손익합계 As Label
    Friend WithEvents lbl_인출가능금액 As Label
    Friend WithEvents lbl_주문가능금액 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents txt_구매가능대비비율 As RichTextBox
    Friend WithEvents grd_잔고조회 As DataGridView
    Friend WithEvents TLP_BuySell As TableLayoutPanel
    Friend WithEvents btn_put_매도 As Button
    Friend WithEvents btn_매수를청산 As Button
    Friend WithEvents btn_put_구매가능수 As Button
    Friend WithEvents btn_전체정리 As Button
    Friend WithEvents btn_call_매도 As Button
    Friend WithEvents btn_매도를청산 As Button
    Friend WithEvents btn_call_구매가능수 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lbl_ReceiveCounter As Label
    Friend WithEvents txt_중간청산비율 As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents chk_중간청산 As CheckBox
    Friend WithEvents Label34 As Label
    Friend WithEvents txt_F2_1회최대매매수량 As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents lbl_F2_매매신호 As Label
    Friend WithEvents btn_InsertDB As Button
End Class
