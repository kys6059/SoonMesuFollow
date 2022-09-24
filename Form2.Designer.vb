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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.btn_f2_폼닫기 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
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
        Me.cmb_F2_순매수기준 = New System.Windows.Forms.ComboBox()
        Me.txt_상승하락기울기기준 = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_최대포인트수대비비율 = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_선행_포인트_마진 = New System.Windows.Forms.RichTextBox()
        Me.grid_3 = New System.Windows.Forms.DataGridView()
        Me.grid_shinho = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_F2_최종방향 = New System.Windows.Forms.RichTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_F2_기준가격 = New System.Windows.Forms.RichTextBox()
        Me.chk_F2_매수실행 = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_F2_매수시작시간 = New System.Windows.Forms.RichTextBox()
        Me.txt_F2_신호시작시간 = New System.Windows.Forms.RichTextBox()
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
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.F2_Chart_순매수, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.grid_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid_shinho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_f2_폼닫기
        '
        Me.btn_f2_폼닫기.Location = New System.Drawing.Point(12, 12)
        Me.btn_f2_폼닫기.Name = "btn_f2_폼닫기"
        Me.btn_f2_폼닫기.Size = New System.Drawing.Size(93, 51)
        Me.btn_f2_폼닫기.TabIndex = 0
        Me.btn_f2_폼닫기.Text = "폼닫기"
        Me.btn_f2_폼닫기.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.09524!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TableLayoutPanel1.Controls.Add(Me.HSc_F2_날짜조절, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Lbl_F2_현재시간Index, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Lbl_F2_현재날짜Index, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txt_F2_DB_Date_Limit, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txt_F2_TableName, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.HSc_F2_시간조절, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_F2_SelectDB, 2, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1113, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(731, 154)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'HSc_F2_날짜조절
        '
        Me.HSc_F2_날짜조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_날짜조절.Location = New System.Drawing.Point(555, 52)
        Me.HSc_F2_날짜조절.Name = "HSc_F2_날짜조절"
        Me.HSc_F2_날짜조절.Size = New System.Drawing.Size(174, 48)
        Me.HSc_F2_날짜조절.TabIndex = 8
        '
        'Lbl_F2_현재시간Index
        '
        Me.Lbl_F2_현재시간Index.AutoSize = True
        Me.Lbl_F2_현재시간Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재시간Index.Location = New System.Drawing.Point(3, 103)
        Me.Lbl_F2_현재시간Index.Margin = New System.Windows.Forms.Padding(1)
        Me.Lbl_F2_현재시간Index.Name = "Lbl_F2_현재시간Index"
        Me.Lbl_F2_현재시간Index.Size = New System.Drawing.Size(272, 48)
        Me.Lbl_F2_현재시간Index.TabIndex = 6
        Me.Lbl_F2_현재시간Index.Text = "X건 중 Y"
        Me.Lbl_F2_현재시간Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lbl_F2_현재날짜Index
        '
        Me.Lbl_F2_현재날짜Index.AutoSize = True
        Me.Lbl_F2_현재날짜Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재날짜Index.Location = New System.Drawing.Point(558, 102)
        Me.Lbl_F2_현재날짜Index.Name = "Lbl_F2_현재날짜Index"
        Me.Lbl_F2_현재날짜Index.Size = New System.Drawing.Size(168, 50)
        Me.Lbl_F2_현재날짜Index.TabIndex = 4
        Me.Lbl_F2_현재날짜Index.Text = "X일 중 Y일"
        Me.Lbl_F2_현재날짜Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_DB_Date_Limit
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txt_F2_DB_Date_Limit, 3)
        Me.txt_F2_DB_Date_Limit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_DB_Date_Limit.Location = New System.Drawing.Point(281, 5)
        Me.txt_F2_DB_Date_Limit.Name = "txt_F2_DB_Date_Limit"
        Me.txt_F2_DB_Date_Limit.Size = New System.Drawing.Size(445, 21)
        Me.txt_F2_DB_Date_Limit.TabIndex = 0
        Me.txt_F2_DB_Date_Limit.Text = "where cdate >= 220801"
        '
        'txt_F2_TableName
        '
        Me.txt_F2_TableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_TableName.Location = New System.Drawing.Point(385, 55)
        Me.txt_F2_TableName.Name = "txt_F2_TableName"
        Me.txt_F2_TableName.Size = New System.Drawing.Size(165, 21)
        Me.txt_F2_TableName.TabIndex = 2
        Me.txt_F2_TableName.Text = "option_one_minute"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(281, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 48)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "테이블명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HSc_F2_시간조절
        '
        Me.HSc_F2_시간조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_시간조절.Location = New System.Drawing.Point(2, 52)
        Me.HSc_F2_시간조절.Name = "HSc_F2_시간조절"
        Me.HSc_F2_시간조절.Size = New System.Drawing.Size(274, 48)
        Me.HSc_F2_시간조절.TabIndex = 5
        '
        'btn_F2_SelectDB
        '
        Me.btn_F2_SelectDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_SelectDB.Location = New System.Drawing.Point(385, 105)
        Me.btn_F2_SelectDB.Name = "btn_F2_SelectDB"
        Me.btn_F2_SelectDB.Size = New System.Drawing.Size(165, 44)
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
        Me.F2_Chart_순매수.Location = New System.Drawing.Point(12, 230)
        Me.F2_Chart_순매수.Name = "F2_Chart_순매수"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.F2_Chart_순매수.Series.Add(Series1)
        Me.F2_Chart_순매수.Size = New System.Drawing.Size(1393, 749)
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1414, 185)
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
        Me.TableLayoutPanel3.Controls.Add(Me.cmb_F2_순매수기준, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_상승하락기울기기준, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_최대포인트수대비비율, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label6, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_선행_포인트_마진, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(148, 16)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(318, 150)
        Me.TableLayoutPanel3.TabIndex = 6
        '
        'cmb_F2_순매수기준
        '
        Me.cmb_F2_순매수기준.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_F2_순매수기준.FormattingEnabled = True
        Me.cmb_F2_순매수기준.Location = New System.Drawing.Point(185, 42)
        Me.cmb_F2_순매수기준.Name = "cmb_F2_순매수기준"
        Me.cmb_F2_순매수기준.Size = New System.Drawing.Size(128, 20)
        Me.cmb_F2_순매수기준.TabIndex = 11
        '
        'txt_상승하락기울기기준
        '
        Me.txt_상승하락기울기기준.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_상승하락기울기기준.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_상승하락기울기기준.Location = New System.Drawing.Point(185, 116)
        Me.txt_상승하락기울기기준.Name = "txt_상승하락기울기기준"
        Me.txt_상승하락기울기기준.Size = New System.Drawing.Size(128, 29)
        Me.txt_상승하락기울기기준.TabIndex = 9
        Me.txt_상승하락기울기기준.Text = "5.0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 114)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(176, 33)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "상승하락 기울기 기준"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_최대포인트수대비비율
        '
        Me.txt_최대포인트수대비비율.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_최대포인트수대비비율.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_최대포인트수대비비율.Location = New System.Drawing.Point(185, 79)
        Me.txt_최대포인트수대비비율.Name = "txt_최대포인트수대비비율"
        Me.txt_최대포인트수대비비율.Size = New System.Drawing.Size(128, 29)
        Me.txt_최대포인트수대비비율.TabIndex = 7
        Me.txt_최대포인트수대비비율.Text = "4.0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(3, 77)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(176, 33)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "최대포인트수"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 40)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(176, 33)
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
        Me.Label1.Size = New System.Drawing.Size(176, 33)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "선행포인트수 마진"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_선행_포인트_마진
        '
        Me.txt_선행_포인트_마진.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_선행_포인트_마진.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_선행_포인트_마진.Location = New System.Drawing.Point(185, 5)
        Me.txt_선행_포인트_마진.Name = "txt_선행_포인트_마진"
        Me.txt_선행_포인트_마진.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_선행_포인트_마진.Size = New System.Drawing.Size(128, 29)
        Me.txt_선행_포인트_마진.TabIndex = 5
        Me.txt_선행_포인트_마진.Text = "0.8"
        '
        'grid_3
        '
        Me.grid_3.AllowUserToAddRows = False
        Me.grid_3.AllowUserToDeleteRows = False
        Me.grid_3.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grid_3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid_3.Location = New System.Drawing.Point(1414, 230)
        Me.grid_3.Name = "grid_3"
        Me.grid_3.ReadOnly = True
        Me.grid_3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid_3.RowTemplate.Height = 23
        Me.grid_3.Size = New System.Drawing.Size(804, 283)
        Me.grid_3.TabIndex = 7
        '
        'grid_shinho
        '
        Me.grid_shinho.AllowUserToAddRows = False
        Me.grid_shinho.AllowUserToDeleteRows = False
        Me.grid_shinho.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grid_shinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid_shinho.Location = New System.Drawing.Point(12, 985)
        Me.grid_shinho.Name = "grid_shinho"
        Me.grid_shinho.ReadOnly = True
        Me.grid_shinho.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid_shinho.RowTemplate.Height = 23
        Me.grid_shinho.Size = New System.Drawing.Size(2056, 265)
        Me.grid_shinho.TabIndex = 8
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel4.Controls.Add(Me.txt_F2_최종방향, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.RichTextBox2, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label8, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label9, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_F2_기준가격, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.chk_F2_매수실행, 1, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(468, 16)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 4
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(318, 150)
        Me.TableLayoutPanel4.TabIndex = 9
        '
        'txt_F2_최종방향
        '
        Me.txt_F2_최종방향.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_최종방향.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_최종방향.Location = New System.Drawing.Point(214, 116)
        Me.txt_F2_최종방향.Name = "txt_F2_최종방향"
        Me.txt_F2_최종방향.Size = New System.Drawing.Size(99, 29)
        Me.txt_F2_최종방향.TabIndex = 9
        Me.txt_F2_최종방향.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(3, 114)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(205, 33)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "최종방향"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox2.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.RichTextBox2.Location = New System.Drawing.Point(214, 79)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(99, 29)
        Me.RichTextBox2.TabIndex = 7
        Me.RichTextBox2.Text = "100"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(3, 77)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(205, 33)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "투자 금액"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(3, 40)
        Me.Label8.Margin = New System.Windows.Forms.Padding(1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(205, 33)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "실제 매수 실행"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Margin = New System.Windows.Forms.Padding(1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(205, 33)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "콜풋 기준가격"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_기준가격
        '
        Me.txt_F2_기준가격.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_기준가격.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_기준가격.Location = New System.Drawing.Point(214, 5)
        Me.txt_F2_기준가격.Name = "txt_F2_기준가격"
        Me.txt_F2_기준가격.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_F2_기준가격.Size = New System.Drawing.Size(99, 29)
        Me.txt_F2_기준가격.TabIndex = 5
        Me.txt_F2_기준가격.Text = "1.25"
        '
        'chk_F2_매수실행
        '
        Me.chk_F2_매수실행.AutoSize = True
        Me.chk_F2_매수실행.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_F2_매수실행.Location = New System.Drawing.Point(214, 42)
        Me.chk_F2_매수실행.Name = "chk_F2_매수실행"
        Me.chk_F2_매수실행.Size = New System.Drawing.Size(99, 29)
        Me.chk_F2_매수실행.TabIndex = 10
        Me.chk_F2_매수실행.Text = "매수 실행"
        Me.chk_F2_매수실행.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_매수시작시간, 1, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.txt_F2_신호시작시간, 1, 2)
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
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(787, 2)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 6
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(318, 222)
        Me.TableLayoutPanel5.TabIndex = 10
        '
        'txt_F2_매수시작시간
        '
        Me.txt_F2_매수시작시간.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_매수시작시간.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_매수시작시간.Location = New System.Drawing.Point(214, 113)
        Me.txt_F2_매수시작시간.Name = "txt_F2_매수시작시간"
        Me.txt_F2_매수시작시간.Size = New System.Drawing.Size(99, 28)
        Me.txt_F2_매수시작시간.TabIndex = 13
        Me.txt_F2_매수시작시간.Text = "90300"
        '
        'txt_F2_신호시작시간
        '
        Me.txt_F2_신호시작시간.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_신호시작시간.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_신호시작시간.Location = New System.Drawing.Point(214, 77)
        Me.txt_F2_신호시작시간.Name = "txt_F2_신호시작시간"
        Me.txt_F2_신호시작시간.Size = New System.Drawing.Size(99, 28)
        Me.txt_F2_신호시작시간.TabIndex = 12
        Me.txt_F2_신호시작시간.Text = "90300"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Location = New System.Drawing.Point(3, 111)
        Me.Label15.Margin = New System.Windows.Forms.Padding(1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(205, 32)
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
        Me.Label14.Size = New System.Drawing.Size(205, 32)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "신호시작시간"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_TimeoutTime
        '
        Me.txt_F2_TimeoutTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_TimeoutTime.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_TimeoutTime.Location = New System.Drawing.Point(214, 185)
        Me.txt_F2_TimeoutTime.Name = "txt_F2_TimeoutTime"
        Me.txt_F2_TimeoutTime.Size = New System.Drawing.Size(99, 32)
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
        Me.Label10.Size = New System.Drawing.Size(205, 36)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Timeout 시간"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_매수마감시간
        '
        Me.txt_F2_매수마감시간.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_매수마감시간.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_매수마감시간.Location = New System.Drawing.Point(214, 149)
        Me.txt_F2_매수마감시간.Name = "txt_F2_매수마감시간"
        Me.txt_F2_매수마감시간.Size = New System.Drawing.Size(99, 28)
        Me.txt_F2_매수마감시간.TabIndex = 7
        Me.txt_F2_매수마감시간.Text = "150000"
        '
        'txt_F2_익절차
        '
        Me.txt_F2_익절차.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_익절차.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_익절차.Location = New System.Drawing.Point(214, 41)
        Me.txt_F2_익절차.Name = "txt_F2_익절차"
        Me.txt_F2_익절차.Size = New System.Drawing.Size(99, 28)
        Me.txt_F2_익절차.TabIndex = 6
        Me.txt_F2_익절차.Text = "20"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Location = New System.Drawing.Point(3, 147)
        Me.Label11.Margin = New System.Windows.Forms.Padding(1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(205, 32)
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
        Me.Label12.Size = New System.Drawing.Size(205, 32)
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
        Me.Label13.Size = New System.Drawing.Size(205, 32)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "종합주가지수 손절매차"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_손절매차
        '
        Me.txt_F2_손절매차.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_손절매차.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_F2_손절매차.Location = New System.Drawing.Point(214, 5)
        Me.txt_F2_손절매차.Name = "txt_F2_손절매차"
        Me.txt_F2_손절매차.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_F2_손절매차.Size = New System.Drawing.Size(99, 28)
        Me.txt_F2_손절매차.TabIndex = 5
        Me.txt_F2_손절매차.Text = "10"
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
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(1851, 67)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.73265!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.26735!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(556, 102)
        Me.TableLayoutPanel6.TabIndex = 19
        '
        'btn_F2_전체조건반복
        '
        Me.btn_F2_전체조건반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_전체조건반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_F2_전체조건반복.Location = New System.Drawing.Point(310, 40)
        Me.btn_F2_전체조건반복.Name = "btn_F2_전체조건반복"
        Me.btn_F2_전체조건반복.Size = New System.Drawing.Size(242, 58)
        Me.btn_F2_전체조건반복.TabIndex = 5
        Me.btn_F2_전체조건반복.Text = "전체조건 반복"
        Me.btn_F2_전체조건반복.UseVisualStyleBackColor = True
        '
        'btn_F2_동일조건반복
        '
        Me.btn_F2_동일조건반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_동일조건반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_F2_동일조건반복.Location = New System.Drawing.Point(142, 40)
        Me.btn_F2_동일조건반복.Name = "btn_F2_동일조건반복"
        Me.btn_F2_동일조건반복.Size = New System.Drawing.Size(161, 58)
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
        Me.chk_F2_화면끄기.Size = New System.Drawing.Size(131, 29)
        Me.chk_F2_화면끄기.TabIndex = 0
        Me.chk_F2_화면끄기.Text = "화면끄기"
        Me.chk_F2_화면끄기.UseVisualStyleBackColor = True
        '
        'btn_당일반복
        '
        Me.btn_당일반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_당일반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_당일반복.Location = New System.Drawing.Point(4, 40)
        Me.btn_당일반복.Name = "btn_당일반복"
        Me.btn_당일반복.Size = New System.Drawing.Size(131, 58)
        Me.btn_당일반복.TabIndex = 1
        Me.btn_당일반복.Text = "당일반복"
        Me.btn_당일반복.UseVisualStyleBackColor = True
        '
        'txt_F2_실험조건
        '
        Me.txt_F2_실험조건.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_실험조건.Location = New System.Drawing.Point(310, 4)
        Me.txt_F2_실험조건.Name = "txt_F2_실험조건"
        Me.txt_F2_실험조건.Size = New System.Drawing.Size(242, 29)
        Me.txt_F2_실험조건.TabIndex = 7
        Me.txt_F2_실험조건.Text = ""
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(2484, 1262)
        Me.Controls.Add(Me.TableLayoutPanel6)
        Me.Controls.Add(Me.TableLayoutPanel5)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.grid_shinho)
        Me.Controls.Add(Me.grid_3)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.F2_Chart_순매수)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btn_f2_폼닫기)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Name = "Form2"
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
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_f2_폼닫기 As Button
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
    Friend WithEvents txt_최대포인트수대비비율 As RichTextBox
    Friend WithEvents grid_3 As DataGridView
    Friend WithEvents txt_상승하락기울기기준 As RichTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents grid_shinho As DataGridView
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents txt_F2_최종방향 As RichTextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_F2_기준가격 As RichTextBox
    Friend WithEvents chk_F2_매수실행 As CheckBox
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
    Friend WithEvents txt_F2_신호시작시간 As RichTextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
End Class
