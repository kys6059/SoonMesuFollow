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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btn_RealTimeStart = New System.Windows.Forms.Button()
        Me.grid1 = New System.Windows.Forms.DataGridView()
        Me.grd_selected = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmb_selectedJongmokIndex_1 = New System.Windows.Forms.ComboBox()
        Me.lbl_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_selectedJongmokIndex_0 = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_JongmokTargetPrice = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_LowerLimit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_UpperLimit = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Interval = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_TargetDate = New System.Windows.Forms.TextBox()
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
        Me.HHippoChart1 = New Hippo.WindowsForm4.hHippoChart()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chk_ChangeTargetIndex = New System.Windows.Forms.CheckBox()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_RealTimeStart
        '
        Me.btn_RealTimeStart.Location = New System.Drawing.Point(21, 36)
        Me.btn_RealTimeStart.Name = "btn_RealTimeStart"
        Me.btn_RealTimeStart.Size = New System.Drawing.Size(87, 21)
        Me.btn_RealTimeStart.TabIndex = 0
        Me.btn_RealTimeStart.Text = "대신 연결"
        Me.btn_RealTimeStart.UseVisualStyleBackColor = True
        '
        'grid1
        '
        Me.grid1.AllowUserToAddRows = False
        Me.grid1.AllowUserToDeleteRows = False
        Me.grid1.AllowUserToResizeColumns = False
        Me.grid1.AllowUserToResizeRows = False
        Me.grid1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid1.Location = New System.Drawing.Point(21, 150)
        Me.grid1.Margin = New System.Windows.Forms.Padding(1)
        Me.grid1.Name = "grid1"
        Me.grid1.ReadOnly = True
        Me.grid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grid1.RowTemplate.Height = 23
        Me.grid1.ShowCellErrors = False
        Me.grid1.ShowCellToolTips = False
        Me.grid1.ShowEditingIcon = False
        Me.grid1.ShowRowErrors = False
        Me.grid1.Size = New System.Drawing.Size(1047, 906)
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
        Me.grd_selected.Location = New System.Drawing.Point(1094, 150)
        Me.grd_selected.Name = "grd_selected"
        Me.grd_selected.ReadOnly = True
        Me.grd_selected.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grd_selected.RowTemplate.Height = 23
        Me.grd_selected.ShowCellErrors = False
        Me.grd_selected.ShowCellToolTips = False
        Me.grd_selected.ShowEditingIcon = False
        Me.grd_selected.ShowRowErrors = False
        Me.grd_selected.Size = New System.Drawing.Size(666, 906)
        Me.grd_selected.TabIndex = 2
        Me.grd_selected.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.75!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.25!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.75!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.25!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmb_selectedJongmokIndex_1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmb_selectedJongmokIndex_0, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1094, 108)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(666, 36)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'cmb_selectedJongmokIndex_1
        '
        Me.cmb_selectedJongmokIndex_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_1.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_1.Location = New System.Drawing.Point(459, 3)
        Me.cmb_selectedJongmokIndex_1.Name = "cmb_selectedJongmokIndex_1"
        Me.cmb_selectedJongmokIndex_1.Size = New System.Drawing.Size(204, 23)
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
        Me.lbl_1.Size = New System.Drawing.Size(118, 36)
        Me.lbl_1.TabIndex = 0
        Me.lbl_1.Text = "Call 선택"
        Me.lbl_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(335, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Put 선택"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_selectedJongmokIndex_0
        '
        Me.cmb_selectedJongmokIndex_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_0.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_0.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_0.Location = New System.Drawing.Point(127, 3)
        Me.cmb_selectedJongmokIndex_0.Name = "cmb_selectedJongmokIndex_0"
        Me.cmb_selectedJongmokIndex_0.Size = New System.Drawing.Size(202, 23)
        Me.cmb_selectedJongmokIndex_0.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(21, 1063)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 6
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(386, 215)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(185, 34)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "기준가"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_JongmokTargetPrice
        '
        Me.txt_JongmokTargetPrice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_JongmokTargetPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JongmokTargetPrice.Location = New System.Drawing.Point(197, 146)
        Me.txt_JongmokTargetPrice.Name = "txt_JongmokTargetPrice"
        Me.txt_JongmokTargetPrice.Size = New System.Drawing.Size(184, 24)
        Me.txt_JongmokTargetPrice.TabIndex = 9
        Me.txt_JongmokTargetPrice.Text = "2.0"
        Me.txt_JongmokTargetPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(185, 34)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "가격 하한"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_LowerLimit
        '
        Me.txt_LowerLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_LowerLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LowerLimit.Location = New System.Drawing.Point(197, 111)
        Me.txt_LowerLimit.Name = "txt_LowerLimit"
        Me.txt_LowerLimit.Size = New System.Drawing.Size(184, 24)
        Me.txt_LowerLimit.TabIndex = 7
        Me.txt_LowerLimit.Text = "0.4"
        Me.txt_LowerLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(185, 34)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "가격 상한"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_UpperLimit
        '
        Me.txt_UpperLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_UpperLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UpperLimit.Location = New System.Drawing.Point(197, 76)
        Me.txt_UpperLimit.Name = "txt_UpperLimit"
        Me.txt_UpperLimit.Size = New System.Drawing.Size(184, 24)
        Me.txt_UpperLimit.TabIndex = 5
        Me.txt_UpperLimit.Text = "4.0"
        Me.txt_UpperLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(185, 34)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Interval"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_Interval
        '
        Me.txt_Interval.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_Interval.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Interval.Location = New System.Drawing.Point(197, 41)
        Me.txt_Interval.Name = "txt_Interval"
        Me.txt_Interval.Size = New System.Drawing.Size(184, 24)
        Me.txt_Interval.TabIndex = 3
        Me.txt_Interval.Text = "5"
        Me.txt_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(185, 34)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "목표날짜"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_TargetDate
        '
        Me.txt_TargetDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TargetDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TargetDate.Location = New System.Drawing.Point(196, 4)
        Me.txt_TargetDate.Name = "txt_TargetDate"
        Me.txt_TargetDate.Size = New System.Drawing.Size(186, 24)
        Me.txt_TargetDate.TabIndex = 1
        Me.txt_TargetDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'btn_TimerStart
        '
        Me.btn_TimerStart.Location = New System.Drawing.Point(125, 36)
        Me.btn_TimerStart.Name = "btn_TimerStart"
        Me.btn_TimerStart.Size = New System.Drawing.Size(87, 21)
        Me.btn_TimerStart.TabIndex = 5
        Me.btn_TimerStart.Text = "START"
        Me.btn_TimerStart.UseVisualStyleBackColor = True
        '
        'label_timerCounter
        '
        Me.label_timerCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_timerCounter.Location = New System.Drawing.Point(140, 60)
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
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(424, 21)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.29851!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.70149!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(644, 100)
        Me.TableLayoutPanel3.TabIndex = 8
        '
        'chk_AutoTurnOff
        '
        Me.chk_AutoTurnOff.AutoSize = True
        Me.chk_AutoTurnOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_AutoTurnOff.Location = New System.Drawing.Point(4, 44)
        Me.chk_AutoTurnOff.Name = "chk_AutoTurnOff"
        Me.chk_AutoTurnOff.Size = New System.Drawing.Size(110, 52)
        Me.chk_AutoTurnOff.TabIndex = 11
        Me.chk_AutoTurnOff.Text = "자동PC끄기"
        Me.chk_AutoTurnOff.UseVisualStyleBackColor = True
        '
        'txt_TableName
        '
        Me.txt_TableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TableName.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_TableName.Location = New System.Drawing.Point(238, 4)
        Me.txt_TableName.Name = "txt_TableName"
        Me.txt_TableName.Size = New System.Drawing.Size(168, 21)
        Me.txt_TableName.TabIndex = 8
        Me.txt_TableName.Text = "option_190628"
        '
        'btn_SelectDB
        '
        Me.btn_SelectDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_SelectDB.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_SelectDB.Location = New System.Drawing.Point(238, 44)
        Me.btn_SelectDB.Name = "btn_SelectDB"
        Me.btn_SelectDB.Size = New System.Drawing.Size(168, 52)
        Me.btn_SelectDB.TabIndex = 6
        Me.btn_SelectDB.Text = "DB 가져오기"
        Me.btn_SelectDB.UseVisualStyleBackColor = True
        '
        'txt_DBDate
        '
        Me.txt_DBDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_DBDate.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_DBDate.Location = New System.Drawing.Point(121, 4)
        Me.txt_DBDate.Name = "txt_DBDate"
        Me.txt_DBDate.Size = New System.Drawing.Size(110, 25)
        Me.txt_DBDate.TabIndex = 0
        '
        'btn_InsertDB
        '
        Me.btn_InsertDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_InsertDB.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_InsertDB.Location = New System.Drawing.Point(121, 44)
        Me.btn_InsertDB.Name = "btn_InsertDB"
        Me.btn_InsertDB.Size = New System.Drawing.Size(110, 52)
        Me.btn_InsertDB.TabIndex = 1
        Me.btn_InsertDB.Text = "DB에 입력"
        Me.btn_InsertDB.UseVisualStyleBackColor = True
        '
        'chk_AutoSave
        '
        Me.chk_AutoSave.AutoSize = True
        Me.chk_AutoSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_AutoSave.Location = New System.Drawing.Point(4, 4)
        Me.chk_AutoSave.Name = "chk_AutoSave"
        Me.chk_AutoSave.Size = New System.Drawing.Size(110, 33)
        Me.chk_AutoSave.TabIndex = 9
        Me.chk_AutoSave.Text = "자동저장(1530)"
        Me.chk_AutoSave.UseVisualStyleBackColor = True
        '
        'lbl_DBDateInfo
        '
        Me.lbl_DBDateInfo.AutoSize = True
        Me.lbl_DBDateInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_DBDateInfo.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbl_DBDateInfo.Location = New System.Drawing.Point(413, 1)
        Me.lbl_DBDateInfo.Name = "lbl_DBDateInfo"
        Me.lbl_DBDateInfo.Size = New System.Drawing.Size(227, 39)
        Me.lbl_DBDateInfo.TabIndex = 12
        Me.lbl_DBDateInfo.Text = "X일 중 Y일"
        Me.lbl_DBDateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DBDate_HScrollBar
        '
        Me.DBDate_HScrollBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DBDate_HScrollBar.LargeChange = 1
        Me.DBDate_HScrollBar.Location = New System.Drawing.Point(410, 41)
        Me.DBDate_HScrollBar.Name = "DBDate_HScrollBar"
        Me.DBDate_HScrollBar.Size = New System.Drawing.Size(233, 58)
        Me.DBDate_HScrollBar.TabIndex = 13
        '
        'txt_Log
        '
        Me.txt_Log.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txt_Log.Location = New System.Drawing.Point(1094, 1080)
        Me.txt_Log.Multiline = True
        Me.txt_Log.Name = "txt_Log"
        Me.txt_Log.ReadOnly = True
        Me.txt_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Log.Size = New System.Drawing.Size(663, 159)
        Me.txt_Log.TabIndex = 9
        '
        'txt_DB_Date_Limit
        '
        Me.txt_DB_Date_Limit.Location = New System.Drawing.Point(545, 127)
        Me.txt_DB_Date_Limit.Name = "txt_DB_Date_Limit"
        Me.txt_DB_Date_Limit.Size = New System.Drawing.Size(309, 21)
        Me.txt_DB_Date_Limit.TabIndex = 11
        Me.txt_DB_Date_Limit.Text = " where cdate > 220301"
        '
        'HHippoChart1
        '
        Me.HHippoChart1.BackColor = System.Drawing.Color.Transparent
        Me.HHippoChart1.ChartGraphic = Nothing
        Me.HHippoChart1.ChartID = Nothing
        Me.HHippoChart1.ChartImage = Nothing
        Me.HHippoChart1.ChartRectangle = CType(resources.GetObject("HHippoChart1.ChartRectangle"), System.Drawing.RectangleF)
        Me.HHippoChart1.Designer = CType(resources.GetObject("HHippoChart1.Designer"), Hippo.ChartDesigner)
        Me.HHippoChart1.DesignType = Hippo.ChartDesignType.None
        Me.HHippoChart1.Direction = Hippo.GraphAreaLocation.Vertical
        Me.HHippoChart1.Height2 = 270.0!
        Me.HHippoChart1.IsDrag = False
        Me.HHippoChart1.IsEmptyanalysis = True
        Me.HHippoChart1.IsUseContextMenu = True
        Me.HHippoChart1.Layout = CType(resources.GetObject("HHippoChart1.Layout"), Hippo.Layout)
        Me.HHippoChart1.Left = 0!
        Me.HHippoChart1.LegendBox = CType(resources.GetObject("HHippoChart1.LegendBox"), Hippo.LegendBox)
        Me.HHippoChart1.Location = New System.Drawing.Point(1787, 150)
        Me.HHippoChart1.Logo = CType(resources.GetObject("HHippoChart1.Logo"), Hippo.Logo)
        Me.HHippoChart1.MinimumSize = New System.Drawing.Size(200, 140)
        Me.HHippoChart1.Name = "HHippoChart1"
        Me.HHippoChart1.PaletteType = Hippo.PaletteType.[Default]
        Me.HHippoChart1.ProcessorType = "64"
        Me.HHippoChart1.SeriesAreaRate = Nothing
        Me.HHippoChart1.SeriesListDictionary = CType(resources.GetObject("HHippoChart1.SeriesListDictionary"), Hippo.SeriesListDictionary)
        Me.HHippoChart1.Size = New System.Drawing.Size(910, 906)
        Me.HHippoChart1.TabIndex = 12
        Me.HHippoChart1.Titles = CType(resources.GetObject("HHippoChart1.Titles"), Hippo.Title)
        Me.HHippoChart1.Top = 150.0!
        Me.HHippoChart1.Width2 = 400.0!
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(185, 38)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "기준종목 자동 변경"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chk_ChangeTargetIndex
        '
        Me.chk_ChangeTargetIndex.AutoSize = True
        Me.chk_ChangeTargetIndex.Checked = True
        Me.chk_ChangeTargetIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_ChangeTargetIndex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_ChangeTargetIndex.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_ChangeTargetIndex.Location = New System.Drawing.Point(196, 179)
        Me.chk_ChangeTargetIndex.Name = "chk_ChangeTargetIndex"
        Me.chk_ChangeTargetIndex.Size = New System.Drawing.Size(186, 32)
        Me.chk_ChangeTargetIndex.TabIndex = 11
        Me.chk_ChangeTargetIndex.Text = "자동변경 허용"
        Me.chk_ChangeTargetIndex.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2738, 1405)
        Me.Controls.Add(Me.HHippoChart1)
        Me.Controls.Add(Me.txt_DB_Date_Limit)
        Me.Controls.Add(Me.txt_Log)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.label_timerCounter)
        Me.Controls.Add(Me.btn_TimerStart)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.grd_selected)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.btn_RealTimeStart)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_RealTimeStart As Button
    Friend WithEvents grid1 As DataGridView
    Friend WithEvents grd_selected As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_1 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_selectedJongmokIndex_1 As ComboBox
    Friend WithEvents cmb_selectedJongmokIndex_0 As ComboBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_JongmokTargetPrice As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_LowerLimit As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_UpperLimit As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_Interval As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_TargetDate As TextBox
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
    Friend WithEvents HHippoChart1 As Hippo.WindowsForm4.hHippoChart
    Friend WithEvents Label7 As Label
    Friend WithEvents chk_ChangeTargetIndex As CheckBox
    'Friend WithEvents HHippoChart1 As Hippo.WindowsForm4.hHippoChart
End Class
