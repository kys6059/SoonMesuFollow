<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.grid1 = New System.Windows.Forms.DataGridView()
        Me.grd_selected = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmb_selectedJongmokIndex_1 = New System.Windows.Forms.ComboBox()
        Me.lbl_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_selectedJongmokIndex_0 = New System.Windows.Forms.ComboBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btn_TimerStart = New System.Windows.Forms.Button()
        Me.label_timerCounter = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_AutoTurnOff = New System.Windows.Forms.CheckBox()
        Me.txt_TableName = New System.Windows.Forms.TextBox()
        Me.btn_SelectDB = New System.Windows.Forms.Button()
        Me.txt_DBDate = New System.Windows.Forms.TextBox()
        Me.btn_InsertDB = New System.Windows.Forms.Button()
        Me.chk_AutoSave = New System.Windows.Forms.CheckBox()
        Me.lbl_DBDateInfo = New System.Windows.Forms.Label()
        Me.DBDate_HScrollBar = New System.Windows.Forms.HScrollBar()
        Me.txt_Log = New System.Windows.Forms.TextBox()
        Me.txt_DB_Date_Limit = New System.Windows.Forms.TextBox()
        Me.txt_ebest_id = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_양매도실행 = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_신호TimeOut시간 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_양매도Target시간Index = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_익절목표 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_손절매비율 = New System.Windows.Forms.TextBox()
        Me.txt_투자비율 = New System.Windows.Forms.TextBox()
        Me.chk_중간청산 = New System.Windows.Forms.CheckBox()
        Me.grd_ShinHo = New System.Windows.Forms.DataGridView()
        Me.Hscroll_1 = New System.Windows.Forms.HScrollBar()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ReceiveCounter = New System.Windows.Forms.Label()
        Me.lbl_ScrolValue = New System.Windows.Forms.Label()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_전체조건반복 = New System.Windows.Forms.Button()
        Me.btn_동일조건반복 = New System.Windows.Forms.Button()
        Me.chk_화면끄기 = New System.Windows.Forms.CheckBox()
        Me.btn_당일반복 = New System.Windows.Forms.Button()
        Me.txt_실험조건 = New System.Windows.Forms.RichTextBox()
        Me.Chk_실험중지 = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_ebest_id1 = New System.Windows.Forms.TextBox()
        Me.txt_ebest인증비밀번호 = New System.Windows.Forms.TextBox()
        Me.txt_ebest_pwd = New System.Windows.Forms.TextBox()
        Me.chk_모의투자연결 = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btn_이베스트로그인 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lbl_계좌번호 = New System.Windows.Forms.Label()
        Me.lbl_평가손익 = New System.Windows.Forms.Label()
        Me.lbl_평가금액 = New System.Windows.Forms.Label()
        Me.lbl_매매손익합계 = New System.Windows.Forms.Label()
        Me.lbl_인출가능금액 = New System.Windows.Forms.Label()
        Me.lbl_주문가능금액 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
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
        Me.Timer구매가능개수찾기 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer구매가능개수찾기_2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.txt_programversion = New System.Windows.Forms.TextBox()
        Me.chk_ChangeTargetIndex = New System.Windows.Forms.CheckBox()
        Me.txt_TargetDate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Interval = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_UpperLimit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_LowerLimit = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_JongmokTargetPrice = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Timer_AutoSave111 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.txt_week_정규 = New System.Windows.Forms.RichTextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txt_월물 = New System.Windows.Forms.RichTextBox()
        Me.btn_순매수데이터백업 = New System.Windows.Forms.Button()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.txt_ebest_id, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.grd_ShinHo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.grd_잔고조회, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLP_BuySell.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'grid1
        '
        Me.grid1.AllowUserToAddRows = False
        Me.grid1.AllowUserToDeleteRows = False
        Me.grid1.AllowUserToResizeColumns = False
        Me.grid1.AllowUserToResizeRows = False
        Me.grid1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid1.Location = New System.Drawing.Point(10, 168)
        Me.grid1.Margin = New System.Windows.Forms.Padding(1)
        Me.grid1.Name = "grid1"
        Me.grid1.ReadOnly = True
        Me.grid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid1.RowTemplate.Height = 23
        Me.grid1.ShowCellErrors = False
        Me.grid1.ShowCellToolTips = False
        Me.grid1.ShowEditingIcon = False
        Me.grid1.ShowRowErrors = False
        Me.grid1.Size = New System.Drawing.Size(630, 459)
        Me.grid1.TabIndex = 1
        '
        'grd_selected
        '
        Me.grd_selected.AllowUserToAddRows = False
        Me.grd_selected.AllowUserToDeleteRows = False
        Me.grd_selected.AllowUserToResizeColumns = False
        Me.grd_selected.AllowUserToResizeRows = False
        Me.grd_selected.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.grd_selected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grd_selected.Location = New System.Drawing.Point(644, 167)
        Me.grd_selected.Name = "grd_selected"
        Me.grd_selected.ReadOnly = True
        Me.grd_selected.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grd_selected.RowTemplate.Height = 23
        Me.grd_selected.ShowCellErrors = False
        Me.grd_selected.ShowCellToolTips = False
        Me.grd_selected.ShowEditingIcon = False
        Me.grd_selected.ShowRowErrors = False
        Me.grd_selected.Size = New System.Drawing.Size(840, 885)
        Me.grd_selected.TabIndex = 2
        Me.grd_selected.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12121!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.87879!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12121!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.87879!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmb_selectedJongmokIndex_1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmb_selectedJongmokIndex_0, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(644, 127)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(834, 36)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'cmb_selectedJongmokIndex_1
        '
        Me.cmb_selectedJongmokIndex_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_1.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_1.Location = New System.Drawing.Point(520, 3)
        Me.cmb_selectedJongmokIndex_1.Name = "cmb_selectedJongmokIndex_1"
        Me.cmb_selectedJongmokIndex_1.Size = New System.Drawing.Size(311, 23)
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
        Me.lbl_1.Size = New System.Drawing.Size(95, 36)
        Me.lbl_1.TabIndex = 0
        Me.lbl_1.Text = "Call"
        Me.lbl_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(419, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Put"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_selectedJongmokIndex_0
        '
        Me.cmb_selectedJongmokIndex_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_0.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_0.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_0.Location = New System.Drawing.Point(104, 3)
        Me.cmb_selectedJongmokIndex_0.Name = "cmb_selectedJongmokIndex_0"
        Me.cmb_selectedJongmokIndex_0.Size = New System.Drawing.Size(309, 23)
        Me.cmb_selectedJongmokIndex_0.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'btn_TimerStart
        '
        Me.btn_TimerStart.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_TimerStart.Location = New System.Drawing.Point(17, 8)
        Me.btn_TimerStart.Name = "btn_TimerStart"
        Me.btn_TimerStart.Size = New System.Drawing.Size(108, 80)
        Me.btn_TimerStart.TabIndex = 5
        Me.btn_TimerStart.Text = "START"
        Me.btn_TimerStart.UseVisualStyleBackColor = True
        '
        'label_timerCounter
        '
        Me.label_timerCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_timerCounter.Location = New System.Drawing.Point(36, 96)
        Me.label_timerCounter.Name = "label_timerCounter"
        Me.label_timerCounter.Size = New System.Drawing.Size(72, 21)
        Me.label_timerCounter.TabIndex = 6
        Me.label_timerCounter.Text = "0"
        Me.label_timerCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364!))
        Me.TableLayoutPanel3.Controls.Add(Me.chk_AutoTurnOff, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_TableName, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_SelectDB, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_DBDate, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btn_InsertDB, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.chk_AutoSave, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lbl_DBDateInfo, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.DBDate_HScrollBar, 3, 1)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1777, 61)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.29851!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.70149!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(623, 102)
        Me.TableLayoutPanel3.TabIndex = 8
        '
        'chk_AutoTurnOff
        '
        Me.chk_AutoTurnOff.AutoSize = True
        Me.chk_AutoTurnOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_AutoTurnOff.Location = New System.Drawing.Point(4, 44)
        Me.chk_AutoTurnOff.Name = "chk_AutoTurnOff"
        Me.chk_AutoTurnOff.Size = New System.Drawing.Size(106, 54)
        Me.chk_AutoTurnOff.TabIndex = 11
        Me.chk_AutoTurnOff.Text = "자동PC끄기"
        Me.chk_AutoTurnOff.UseVisualStyleBackColor = True
        '
        'txt_TableName
        '
        Me.txt_TableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TableName.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_TableName.Location = New System.Drawing.Point(230, 4)
        Me.txt_TableName.Name = "txt_TableName"
        Me.txt_TableName.Size = New System.Drawing.Size(162, 21)
        Me.txt_TableName.TabIndex = 8
        Me.txt_TableName.Text = "option_weekly"
        '
        'btn_SelectDB
        '
        Me.btn_SelectDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_SelectDB.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_SelectDB.Location = New System.Drawing.Point(230, 44)
        Me.btn_SelectDB.Name = "btn_SelectDB"
        Me.btn_SelectDB.Size = New System.Drawing.Size(162, 54)
        Me.btn_SelectDB.TabIndex = 6
        Me.btn_SelectDB.Text = "DB 가져오기"
        Me.btn_SelectDB.UseVisualStyleBackColor = True
        '
        'txt_DBDate
        '
        Me.txt_DBDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_DBDate.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_DBDate.Location = New System.Drawing.Point(117, 4)
        Me.txt_DBDate.Name = "txt_DBDate"
        Me.txt_DBDate.Size = New System.Drawing.Size(106, 25)
        Me.txt_DBDate.TabIndex = 0
        '
        'btn_InsertDB
        '
        Me.btn_InsertDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_InsertDB.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_InsertDB.Location = New System.Drawing.Point(117, 44)
        Me.btn_InsertDB.Name = "btn_InsertDB"
        Me.btn_InsertDB.Size = New System.Drawing.Size(106, 54)
        Me.btn_InsertDB.TabIndex = 1
        Me.btn_InsertDB.Text = "DB에 입력"
        Me.btn_InsertDB.UseVisualStyleBackColor = True
        '
        'chk_AutoSave
        '
        Me.chk_AutoSave.AutoSize = True
        Me.chk_AutoSave.Checked = True
        Me.chk_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_AutoSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_AutoSave.Location = New System.Drawing.Point(4, 4)
        Me.chk_AutoSave.Name = "chk_AutoSave"
        Me.chk_AutoSave.Size = New System.Drawing.Size(106, 33)
        Me.chk_AutoSave.TabIndex = 9
        Me.chk_AutoSave.Text = "자동저장(1530)"
        Me.chk_AutoSave.UseVisualStyleBackColor = True
        '
        'lbl_DBDateInfo
        '
        Me.lbl_DBDateInfo.AutoSize = True
        Me.lbl_DBDateInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_DBDateInfo.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_DBDateInfo.Location = New System.Drawing.Point(399, 1)
        Me.lbl_DBDateInfo.Name = "lbl_DBDateInfo"
        Me.lbl_DBDateInfo.Size = New System.Drawing.Size(220, 39)
        Me.lbl_DBDateInfo.TabIndex = 12
        Me.lbl_DBDateInfo.Text = "X일 중 Y일"
        Me.lbl_DBDateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DBDate_HScrollBar
        '
        Me.DBDate_HScrollBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DBDate_HScrollBar.LargeChange = 1
        Me.DBDate_HScrollBar.Location = New System.Drawing.Point(396, 41)
        Me.DBDate_HScrollBar.Name = "DBDate_HScrollBar"
        Me.DBDate_HScrollBar.Size = New System.Drawing.Size(226, 60)
        Me.DBDate_HScrollBar.TabIndex = 13
        '
        'txt_Log
        '
        Me.txt_Log.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txt_Log.Location = New System.Drawing.Point(10, 892)
        Me.txt_Log.Multiline = True
        Me.txt_Log.Name = "txt_Log"
        Me.txt_Log.ReadOnly = True
        Me.txt_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Log.Size = New System.Drawing.Size(628, 373)
        Me.txt_Log.TabIndex = 9
        '
        'txt_DB_Date_Limit
        '
        Me.txt_DB_Date_Limit.Location = New System.Drawing.Point(1777, 38)
        Me.txt_DB_Date_Limit.Name = "txt_DB_Date_Limit"
        Me.txt_DB_Date_Limit.Size = New System.Drawing.Size(413, 21)
        Me.txt_DB_Date_Limit.TabIndex = 11
        Me.txt_DB_Date_Limit.Text = " where cdate > 220301"
        '
        'txt_ebest_id
        '
        ChartArea1.Name = "ChartArea1"
        Me.txt_ebest_id.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.txt_ebest_id.Legends.Add(Legend1)
        Me.txt_ebest_id.Location = New System.Drawing.Point(1767, 169)
        Me.txt_ebest_id.Name = "txt_ebest_id"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.txt_ebest_id.Series.Add(Series1)
        Me.txt_ebest_id.Size = New System.Drawing.Size(1242, 885)
        Me.txt_ebest_id.TabIndex = 13
        Me.txt_ebest_id.Text = "Chart1"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.54546!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454!))
        Me.TableLayoutPanel4.Controls.Add(Me.chk_양매도실행, 1, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.Label9, 0, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.Label10, 0, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.Label11, 0, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.Label12, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_신호TimeOut시간, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Label13, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_양매도Target시간Index, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label14, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_익절목표, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label15, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_손절매비율, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txt_투자비율, 1, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.chk_중간청산, 1, 4)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(332, 632)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 7
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28816!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(306, 245)
        Me.TableLayoutPanel4.TabIndex = 14
        '
        'chk_양매도실행
        '
        Me.chk_양매도실행.AutoSize = True
        Me.chk_양매도실행.Checked = True
        Me.chk_양매도실행.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_양매도실행.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_양매도실행.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_양매도실행.Location = New System.Drawing.Point(170, 174)
        Me.chk_양매도실행.Name = "chk_양매도실행"
        Me.chk_양매도실행.Size = New System.Drawing.Size(132, 27)
        Me.chk_양매도실행.TabIndex = 13
        Me.chk_양매도실행.Text = "양매도 실행"
        Me.chk_양매도실행.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 205)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(159, 39)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "투자비율"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 171)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(159, 33)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "양매도 실행"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 137)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(159, 33)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "중간청산 비율"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 103)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(159, 33)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "신호유지 TimeOut"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_신호TimeOut시간
        '
        Me.txt_신호TimeOut시간.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_신호TimeOut시간.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_신호TimeOut시간.Location = New System.Drawing.Point(170, 107)
        Me.txt_신호TimeOut시간.Name = "txt_신호TimeOut시간"
        Me.txt_신호TimeOut시간.Size = New System.Drawing.Size(132, 24)
        Me.txt_신호TimeOut시간.TabIndex = 7
        Me.txt_신호TimeOut시간.Text = "1515"
        Me.txt_신호TimeOut시간.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(4, 69)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(159, 33)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "신호 TargetIndex"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_양매도Target시간Index
        '
        Me.txt_양매도Target시간Index.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_양매도Target시간Index.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_양매도Target시간Index.Location = New System.Drawing.Point(170, 73)
        Me.txt_양매도Target시간Index.Name = "txt_양매도Target시간Index"
        Me.txt_양매도Target시간Index.Size = New System.Drawing.Size(132, 24)
        Me.txt_양매도Target시간Index.TabIndex = 5
        Me.txt_양매도Target시간Index.Text = "0"
        Me.txt_양매도Target시간Index.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(4, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(159, 33)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "익절목표"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_익절목표
        '
        Me.txt_익절목표.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_익절목표.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_익절목표.Location = New System.Drawing.Point(170, 39)
        Me.txt_익절목표.Name = "txt_익절목표"
        Me.txt_익절목표.Size = New System.Drawing.Size(132, 24)
        Me.txt_익절목표.TabIndex = 3
        Me.txt_익절목표.Text = "0.6"
        Me.txt_익절목표.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(159, 33)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "손절매비율"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_손절매비율
        '
        Me.txt_손절매비율.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_손절매비율.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_손절매비율.Location = New System.Drawing.Point(170, 4)
        Me.txt_손절매비율.Name = "txt_손절매비율"
        Me.txt_손절매비율.Size = New System.Drawing.Size(132, 24)
        Me.txt_손절매비율.TabIndex = 1
        Me.txt_손절매비율.Text = "1.18"
        Me.txt_손절매비율.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'chk_중간청산
        '
        Me.chk_중간청산.AutoSize = True
        Me.chk_중간청산.Checked = True
        Me.chk_중간청산.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_중간청산.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_중간청산.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_중간청산.Location = New System.Drawing.Point(170, 140)
        Me.chk_중간청산.Name = "chk_중간청산"
        Me.chk_중간청산.Size = New System.Drawing.Size(132, 27)
        Me.chk_중간청산.TabIndex = 15
        Me.chk_중간청산.Text = "중간청산-50%"
        Me.chk_중간청산.UseVisualStyleBackColor = True
        '
        'grd_ShinHo
        '
        Me.grd_ShinHo.AllowUserToAddRows = False
        Me.grd_ShinHo.AllowUserToDeleteRows = False
        Me.grd_ShinHo.AllowUserToResizeColumns = False
        Me.grd_ShinHo.AllowUserToResizeRows = False
        Me.grd_ShinHo.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.grd_ShinHo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grd_ShinHo.Location = New System.Drawing.Point(1490, 168)
        Me.grd_ShinHo.Name = "grd_ShinHo"
        Me.grd_ShinHo.ReadOnly = True
        Me.grd_ShinHo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grd_ShinHo.RowTemplate.Height = 23
        Me.grd_ShinHo.ShowCellErrors = False
        Me.grd_ShinHo.ShowCellToolTips = False
        Me.grd_ShinHo.ShowEditingIcon = False
        Me.grd_ShinHo.ShowRowErrors = False
        Me.grd_ShinHo.Size = New System.Drawing.Size(271, 885)
        Me.grd_ShinHo.TabIndex = 15
        Me.grd_ShinHo.TabStop = False
        '
        'Hscroll_1
        '
        Me.Hscroll_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Hscroll_1.LargeChange = 1
        Me.Hscroll_1.Location = New System.Drawing.Point(0, 0)
        Me.Hscroll_1.Name = "Hscroll_1"
        Me.Hscroll_1.Size = New System.Drawing.Size(271, 38)
        Me.Hscroll_1.TabIndex = 16
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_ReceiveCounter, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.Hscroll_1, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_ScrolValue, 0, 1)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(1490, 47)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 3
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(271, 116)
        Me.TableLayoutPanel5.TabIndex = 17
        '
        'lbl_ReceiveCounter
        '
        Me.lbl_ReceiveCounter.AutoSize = True
        Me.lbl_ReceiveCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ReceiveCounter.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_ReceiveCounter.Location = New System.Drawing.Point(3, 78)
        Me.lbl_ReceiveCounter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_ReceiveCounter.Name = "lbl_ReceiveCounter"
        Me.lbl_ReceiveCounter.Size = New System.Drawing.Size(265, 36)
        Me.lbl_ReceiveCounter.TabIndex = 18
        Me.lbl_ReceiveCounter.Text = "0"
        Me.lbl_ReceiveCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ScrolValue
        '
        Me.lbl_ScrolValue.AutoSize = True
        Me.lbl_ScrolValue.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lbl_ScrolValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ScrolValue.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_ScrolValue.Location = New System.Drawing.Point(3, 38)
        Me.lbl_ScrolValue.Name = "lbl_ScrolValue"
        Me.lbl_ScrolValue.Size = New System.Drawing.Size(265, 38)
        Me.lbl_ScrolValue.TabIndex = 17
        Me.lbl_ScrolValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.9004!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.27888!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.82072!))
        Me.TableLayoutPanel6.Controls.Add(Me.btn_전체조건반복, 2, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.btn_동일조건반복, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.chk_화면끄기, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btn_당일반복, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.txt_실험조건, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Chk_실험중지, 1, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(2406, 61)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.73265!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.26735!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(557, 102)
        Me.TableLayoutPanel6.TabIndex = 18
        '
        'btn_전체조건반복
        '
        Me.btn_전체조건반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_전체조건반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_전체조건반복.Location = New System.Drawing.Point(310, 40)
        Me.btn_전체조건반복.Name = "btn_전체조건반복"
        Me.btn_전체조건반복.Size = New System.Drawing.Size(243, 58)
        Me.btn_전체조건반복.TabIndex = 5
        Me.btn_전체조건반복.Text = "전체조건 반복"
        Me.btn_전체조건반복.UseVisualStyleBackColor = True
        '
        'btn_동일조건반복
        '
        Me.btn_동일조건반복.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_동일조건반복.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_동일조건반복.Location = New System.Drawing.Point(142, 40)
        Me.btn_동일조건반복.Name = "btn_동일조건반복"
        Me.btn_동일조건반복.Size = New System.Drawing.Size(161, 58)
        Me.btn_동일조건반복.TabIndex = 4
        Me.btn_동일조건반복.Text = "동일조건반복"
        Me.btn_동일조건반복.UseVisualStyleBackColor = True
        '
        'chk_화면끄기
        '
        Me.chk_화면끄기.AutoSize = True
        Me.chk_화면끄기.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_화면끄기.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_화면끄기.Location = New System.Drawing.Point(4, 4)
        Me.chk_화면끄기.Name = "chk_화면끄기"
        Me.chk_화면끄기.Size = New System.Drawing.Size(131, 29)
        Me.chk_화면끄기.TabIndex = 0
        Me.chk_화면끄기.Text = "화면끄기"
        Me.chk_화면끄기.UseVisualStyleBackColor = True
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
        'txt_실험조건
        '
        Me.txt_실험조건.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_실험조건.Location = New System.Drawing.Point(310, 4)
        Me.txt_실험조건.Name = "txt_실험조건"
        Me.txt_실험조건.Size = New System.Drawing.Size(243, 29)
        Me.txt_실험조건.TabIndex = 7
        Me.txt_실험조건.Text = ""
        '
        'Chk_실험중지
        '
        Me.Chk_실험중지.AutoSize = True
        Me.Chk_실험중지.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chk_실험중지.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Chk_실험중지.Location = New System.Drawing.Point(142, 4)
        Me.Chk_실험중지.Name = "Chk_실험중지"
        Me.Chk_실험중지.Size = New System.Drawing.Size(161, 29)
        Me.Chk_실험중지.TabIndex = 8
        Me.Chk_실험중지.Text = "실험중지"
        Me.Chk_실험중지.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Chk_실험중지.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel7.Controls.Add(Me.Label16, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.btn_이베스트로그인, 2, 1)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(134, 8)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(500, 117)
        Me.TableLayoutPanel7.TabIndex = 19
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
        Me.txt_ebest인증비밀번호.Location = New System.Drawing.Point(148, 80)
        Me.txt_ebest인증비밀번호.Name = "txt_ebest인증비밀번호"
        Me.txt_ebest인증비밀번호.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_ebest인증비밀번호.Size = New System.Drawing.Size(138, 21)
        Me.txt_ebest인증비밀번호.TabIndex = 23
        Me.txt_ebest인증비밀번호.Text = "youngsookim6059!"
        '
        'txt_ebest_pwd
        '
        Me.txt_ebest_pwd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_ebest_pwd.Location = New System.Drawing.Point(148, 42)
        Me.txt_ebest_pwd.Name = "txt_ebest_pwd"
        Me.txt_ebest_pwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_ebest_pwd.Size = New System.Drawing.Size(138, 21)
        Me.txt_ebest_pwd.TabIndex = 22
        Me.txt_ebest_pwd.Text = "kys6059!"
        '
        'chk_모의투자연결
        '
        Me.chk_모의투자연결.AutoSize = True
        Me.chk_모의투자연결.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_모의투자연결.Location = New System.Drawing.Point(293, 4)
        Me.chk_모의투자연결.Name = "chk_모의투자연결"
        Me.chk_모의투자연결.Size = New System.Drawing.Size(203, 31)
        Me.chk_모의투자연결.TabIndex = 21
        Me.chk_모의투자연결.Text = "모의투자서버"
        Me.chk_모의투자연결.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Location = New System.Drawing.Point(4, 79)
        Me.Label21.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(137, 35)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "공인인증서 암호"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label18.Location = New System.Drawing.Point(4, 41)
        Me.Label18.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(137, 33)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "PWD"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label16.Location = New System.Drawing.Point(4, 3)
        Me.Label16.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(137, 33)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "ID"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btn_이베스트로그인
        '
        Me.btn_이베스트로그인.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btn_이베스트로그인.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_이베스트로그인.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_이베스트로그인.Location = New System.Drawing.Point(293, 42)
        Me.btn_이베스트로그인.Name = "btn_이베스트로그인"
        Me.TableLayoutPanel7.SetRowSpan(Me.btn_이베스트로그인, 2)
        Me.btn_이베스트로그인.Size = New System.Drawing.Size(203, 71)
        Me.btn_이베스트로그인.TabIndex = 20
        Me.btn_이베스트로그인.Text = "이베스트 시작버튼"
        Me.btn_이베스트로그인.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel8.ColumnCount = 7
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.44086!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.35484!))
        Me.TableLayoutPanel8.Controls.Add(Me.Label24, 6, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_계좌번호, 6, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_평가손익, 5, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_평가금액, 4, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_매매손익합계, 3, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_인출가능금액, 2, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.lbl_주문가능금액, 1, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.Label23, 5, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label22, 4, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label20, 3, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label19, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label17, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label26, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txt_구매가능대비비율, 0, 1)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(1767, 1065)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.51515!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.48485!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(1242, 61)
        Me.TableLayoutPanel8.TabIndex = 22
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label24.Location = New System.Drawing.Point(1000, 3)
        Me.Label24.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(238, 25)
        Me.Label24.TabIndex = 35
        Me.Label24.Text = "계좌번호"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_계좌번호
        '
        Me.lbl_계좌번호.AutoSize = True
        Me.lbl_계좌번호.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_계좌번호.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_계좌번호.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_계좌번호.Location = New System.Drawing.Point(1000, 33)
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
        Me.lbl_평가손익.Location = New System.Drawing.Point(834, 33)
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
        Me.lbl_평가금액.Location = New System.Drawing.Point(668, 33)
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
        Me.lbl_매매손익합계.Location = New System.Drawing.Point(502, 33)
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
        Me.lbl_인출가능금액.Location = New System.Drawing.Point(336, 33)
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
        Me.lbl_주문가능금액.Location = New System.Drawing.Point(170, 33)
        Me.lbl_주문가능금액.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbl_주문가능금액.Name = "lbl_주문가능금액"
        Me.lbl_주문가능금액.Size = New System.Drawing.Size(159, 25)
        Me.lbl_주문가능금액.TabIndex = 28
        Me.lbl_주문가능금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label23.Location = New System.Drawing.Point(834, 3)
        Me.Label23.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(159, 25)
        Me.Label23.TabIndex = 27
        Me.Label23.Text = "평가손익"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label22.Location = New System.Drawing.Point(668, 3)
        Me.Label22.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(159, 25)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "평가금액"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label20.Location = New System.Drawing.Point(502, 3)
        Me.Label20.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(159, 25)
        Me.Label20.TabIndex = 25
        Me.Label20.Text = "매매손익합계"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label19.Location = New System.Drawing.Point(336, 3)
        Me.Label19.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(159, 25)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "인출가능금액"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label17.Location = New System.Drawing.Point(170, 3)
        Me.Label17.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(159, 25)
        Me.Label17.TabIndex = 23
        Me.Label17.Text = "주문가능금액"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label26.Location = New System.Drawing.Point(4, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(159, 29)
        Me.Label26.TabIndex = 37
        Me.Label26.Text = "통신 1회당 매도비율"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_구매가능대비비율
        '
        Me.txt_구매가능대비비율.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_구매가능대비비율.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_구매가능대비비율.Location = New System.Drawing.Point(4, 34)
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
        Me.grd_잔고조회.Location = New System.Drawing.Point(1767, 1132)
        Me.grd_잔고조회.Name = "grd_잔고조회"
        Me.grd_잔고조회.ReadOnly = True
        Me.grd_잔고조회.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grd_잔고조회.RowTemplate.Height = 23
        Me.grd_잔고조회.ShowCellErrors = False
        Me.grd_잔고조회.ShowCellToolTips = False
        Me.grd_잔고조회.ShowEditingIcon = False
        Me.grd_잔고조회.ShowRowErrors = False
        Me.grd_잔고조회.Size = New System.Drawing.Size(1242, 133)
        Me.grd_잔고조회.TabIndex = 23
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
        Me.TLP_BuySell.Location = New System.Drawing.Point(1146, 1065)
        Me.TLP_BuySell.Name = "TLP_BuySell"
        Me.TLP_BuySell.RowCount = 2
        Me.TLP_BuySell.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLP_BuySell.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLP_BuySell.Size = New System.Drawing.Size(615, 117)
        Me.TLP_BuySell.TabIndex = 24
        '
        'btn_put_매도
        '
        Me.btn_put_매도.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_put_매도.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_put_매도.Location = New System.Drawing.Point(310, 62)
        Me.btn_put_매도.Name = "btn_put_매도"
        Me.btn_put_매도.Size = New System.Drawing.Size(146, 51)
        Me.btn_put_매도.TabIndex = 6
        Me.btn_put_매도.Text = "풋 매도"
        Me.btn_put_매도.UseVisualStyleBackColor = True
        '
        'btn_매수를청산
        '
        Me.btn_매수를청산.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_매수를청산.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_매수를청산.Location = New System.Drawing.Point(157, 62)
        Me.btn_매수를청산.Name = "btn_매수를청산"
        Me.btn_매수를청산.Size = New System.Drawing.Size(146, 51)
        Me.btn_매수를청산.TabIndex = 5
        Me.btn_매수를청산.Text = "매수를 청산"
        Me.btn_매수를청산.UseVisualStyleBackColor = True
        '
        'btn_put_구매가능수
        '
        Me.btn_put_구매가능수.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_put_구매가능수.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_put_구매가능수.Location = New System.Drawing.Point(4, 62)
        Me.btn_put_구매가능수.Name = "btn_put_구매가능수"
        Me.btn_put_구매가능수.Size = New System.Drawing.Size(146, 51)
        Me.btn_put_구매가능수.TabIndex = 4
        Me.btn_put_구매가능수.Text = "풋 구매가능"
        Me.btn_put_구매가능수.UseVisualStyleBackColor = True
        '
        'btn_전체정리
        '
        Me.btn_전체정리.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_전체정리.Font = New System.Drawing.Font("굴림", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_전체정리.Location = New System.Drawing.Point(463, 4)
        Me.btn_전체정리.Name = "btn_전체정리"
        Me.TLP_BuySell.SetRowSpan(Me.btn_전체정리, 2)
        Me.btn_전체정리.Size = New System.Drawing.Size(148, 109)
        Me.btn_전체정리.TabIndex = 3
        Me.btn_전체정리.Text = "전체 정리"
        Me.btn_전체정리.UseVisualStyleBackColor = True
        '
        'btn_call_매도
        '
        Me.btn_call_매도.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_call_매도.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_call_매도.Location = New System.Drawing.Point(310, 4)
        Me.btn_call_매도.Name = "btn_call_매도"
        Me.btn_call_매도.Size = New System.Drawing.Size(146, 51)
        Me.btn_call_매도.TabIndex = 2
        Me.btn_call_매도.Text = "콜 매도"
        Me.btn_call_매도.UseVisualStyleBackColor = True
        '
        'btn_매도를청산
        '
        Me.btn_매도를청산.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_매도를청산.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_매도를청산.Location = New System.Drawing.Point(157, 4)
        Me.btn_매도를청산.Name = "btn_매도를청산"
        Me.btn_매도를청산.Size = New System.Drawing.Size(146, 51)
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
        Me.btn_call_구매가능수.Size = New System.Drawing.Size(146, 51)
        Me.btn_call_구매가능수.TabIndex = 0
        Me.btn_call_구매가능수.Text = "콜 구매가능"
        Me.btn_call_구매가능수.UseVisualStyleBackColor = True
        '
        'txt_programversion
        '
        Me.txt_programversion.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_programversion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_programversion.Location = New System.Drawing.Point(163, 213)
        Me.txt_programversion.Name = "txt_programversion"
        Me.txt_programversion.Size = New System.Drawing.Size(153, 24)
        Me.txt_programversion.TabIndex = 13
        Me.txt_programversion.Text = "1.1.5_20220902"
        Me.txt_programversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 33)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "목표날짜"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_Interval
        '
        Me.txt_Interval.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_Interval.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Interval.Location = New System.Drawing.Point(163, 39)
        Me.txt_Interval.Name = "txt_Interval"
        Me.txt_Interval.Size = New System.Drawing.Size(153, 24)
        Me.txt_Interval.TabIndex = 3
        Me.txt_Interval.Text = "5"
        Me.txt_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 33)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Interval"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_UpperLimit
        '
        Me.txt_UpperLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_UpperLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UpperLimit.Location = New System.Drawing.Point(163, 73)
        Me.txt_UpperLimit.Name = "txt_UpperLimit"
        Me.txt_UpperLimit.Size = New System.Drawing.Size(153, 24)
        Me.txt_UpperLimit.TabIndex = 5
        Me.txt_UpperLimit.Text = "7.0"
        Me.txt_UpperLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 33)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "가격 상한"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_LowerLimit
        '
        Me.txt_LowerLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_LowerLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LowerLimit.Location = New System.Drawing.Point(163, 107)
        Me.txt_LowerLimit.Name = "txt_LowerLimit"
        Me.txt_LowerLimit.Size = New System.Drawing.Size(153, 24)
        Me.txt_LowerLimit.TabIndex = 7
        Me.txt_LowerLimit.Text = "0.05"
        Me.txt_LowerLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 33)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "가격 하한"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_JongmokTargetPrice
        '
        Me.txt_JongmokTargetPrice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_JongmokTargetPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JongmokTargetPrice.Location = New System.Drawing.Point(163, 141)
        Me.txt_JongmokTargetPrice.Name = "txt_JongmokTargetPrice"
        Me.txt_JongmokTargetPrice.Size = New System.Drawing.Size(153, 24)
        Me.txt_JongmokTargetPrice.TabIndex = 9
        Me.txt_JongmokTargetPrice.Text = "2.0"
        Me.txt_JongmokTargetPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 33)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "기준가"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 171)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(152, 33)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "기준종목 변경"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 205)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(152, 40)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "프로그램 버전"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_JongmokTargetPrice, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_LowerLimit, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_UpperLimit, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_Interval, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_TargetDate, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.chk_ChangeTargetIndex, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.txt_programversion, 1, 6)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(10, 631)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28816!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(320, 246)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'Timer_AutoSave111
        '
        Me.Timer_AutoSave111.Interval = 10000
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
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(139, 128)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(501, 36)
        Me.TableLayoutPanel9.TabIndex = 26
        '
        'txt_week_정규
        '
        Me.txt_week_정규.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_week_정규.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_week_정규.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.txt_week_정규.Location = New System.Drawing.Point(378, 3)
        Me.txt_week_정규.Multiline = False
        Me.txt_week_정규.Name = "txt_week_정규"
        Me.txt_week_정규.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_week_정규.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_week_정규.Size = New System.Drawing.Size(120, 30)
        Me.txt_week_정규.TabIndex = 4
        Me.txt_week_정규.Text = ""
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label28.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label28.Location = New System.Drawing.Point(251, 1)
        Me.Label28.Margin = New System.Windows.Forms.Padding(1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(123, 34)
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
        Me.Label25.Size = New System.Drawing.Size(123, 34)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "월물"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_월물
        '
        Me.txt_월물.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_월물.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_월물.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.txt_월물.Location = New System.Drawing.Point(128, 3)
        Me.txt_월물.Name = "txt_월물"
        Me.txt_월물.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_월물.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_월물.Size = New System.Drawing.Size(119, 30)
        Me.txt_월물.TabIndex = 3
        Me.txt_월물.Text = ""
        '
        'btn_순매수데이터백업
        '
        Me.btn_순매수데이터백업.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_순매수데이터백업.Location = New System.Drawing.Point(2209, 16)
        Me.btn_순매수데이터백업.Name = "btn_순매수데이터백업"
        Me.btn_순매수데이터백업.Size = New System.Drawing.Size(163, 39)
        Me.btn_순매수데이터백업.TabIndex = 27
        Me.btn_순매수데이터백업.Text = "테스트"
        Me.btn_순매수데이터백업.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(3031, 1277)
        Me.Controls.Add(Me.btn_순매수데이터백업)
        Me.Controls.Add(Me.TableLayoutPanel9)
        Me.Controls.Add(Me.TLP_BuySell)
        Me.Controls.Add(Me.grd_잔고조회)
        Me.Controls.Add(Me.TableLayoutPanel8)
        Me.Controls.Add(Me.TableLayoutPanel7)
        Me.Controls.Add(Me.TableLayoutPanel6)
        Me.Controls.Add(Me.TableLayoutPanel5)
        Me.Controls.Add(Me.grd_ShinHo)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.txt_ebest_id)
        Me.Controls.Add(Me.txt_DB_Date_Limit)
        Me.Controls.Add(Me.txt_Log)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.label_timerCounter)
        Me.Controls.Add(Me.btn_TimerStart)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.grd_selected)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Form1"
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.txt_ebest_id, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.grd_ShinHo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        CType(Me.grd_잔고조회, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLP_BuySell.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grid1 As DataGridView
    Friend WithEvents grd_selected As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_1 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_selectedJongmokIndex_1 As ComboBox
    Friend WithEvents cmb_selectedJongmokIndex_0 As ComboBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btn_TimerStart As Button
    Friend WithEvents label_timerCounter As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents chk_AutoTurnOff As CheckBox
    Friend WithEvents txt_TableName As TextBox
    Friend WithEvents btn_SelectDB As Button
    Friend WithEvents txt_DBDate As TextBox
    Friend WithEvents btn_InsertDB As Button
    Friend WithEvents chk_AutoSave As CheckBox
    Friend WithEvents lbl_DBDateInfo As Label
    Friend WithEvents DBDate_HScrollBar As HScrollBar
    Friend WithEvents txt_Log As TextBox
    Friend WithEvents txt_DB_Date_Limit As TextBox
    Friend WithEvents txt_ebest_id As DataVisualization.Charting.Chart
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_신호TimeOut시간 As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txt_양매도Target시간Index As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txt_익절목표 As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txt_손절매비율 As TextBox
    Friend WithEvents grd_ShinHo As DataGridView
    Friend WithEvents Hscroll_1 As HScrollBar
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lbl_ScrolValue As Label
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents btn_전체조건반복 As Button
    Friend WithEvents btn_동일조건반복 As Button
    Friend WithEvents chk_화면끄기 As CheckBox
    Friend WithEvents btn_당일반복 As Button
    Friend WithEvents txt_실험조건 As RichTextBox
    Friend WithEvents Chk_실험중지 As CheckBox
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents Label21 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents btn_이베스트로그인 As Button
    Friend WithEvents chk_모의투자연결 As CheckBox
    Friend WithEvents txt_ebest인증비밀번호 As TextBox
    Friend WithEvents txt_ebest_pwd As TextBox
    Friend WithEvents txt_ebest_id1 As TextBox
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents lbl_평가손익 As Label
    Friend WithEvents lbl_평가금액 As Label
    Friend WithEvents lbl_매매손익합계 As Label
    Friend WithEvents lbl_인출가능금액 As Label
    Friend WithEvents lbl_주문가능금액 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents grd_잔고조회 As DataGridView
    Friend WithEvents TLP_BuySell As TableLayoutPanel
    Friend WithEvents btn_put_매도 As Button
    Friend WithEvents btn_매수를청산 As Button
    Friend WithEvents btn_put_구매가능수 As Button
    Friend WithEvents btn_전체정리 As Button
    Friend WithEvents btn_call_매도 As Button
    Friend WithEvents btn_매도를청산 As Button
    Friend WithEvents btn_call_구매가능수 As Button
    Friend WithEvents Label24 As Label
    Friend WithEvents lbl_계좌번호 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents txt_구매가능대비비율 As RichTextBox
    Friend WithEvents chk_양매도실행 As CheckBox
    Friend WithEvents Timer구매가능개수찾기 As Timer
    Friend WithEvents Timer구매가능개수찾기_2 As Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_programversion As TextBox
    Friend WithEvents chk_ChangeTargetIndex As CheckBox
    Friend WithEvents txt_TargetDate As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Interval As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_UpperLimit As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_LowerLimit As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_JongmokTargetPrice As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Timer_AutoSave111 As Timer
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents txt_week_정규 As RichTextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents txt_월물 As RichTextBox
    Friend WithEvents lbl_ReceiveCounter As Label
    Friend WithEvents txt_투자비율 As TextBox
    Friend WithEvents btn_순매수데이터백업 As Button
    Friend WithEvents chk_중간청산 As CheckBox
End Class
