﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Dim CustomLabel1 As System.Windows.Forms.DataVisualization.Charting.CustomLabel = New System.Windows.Forms.DataVisualization.Charting.CustomLabel()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
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
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_RealTimeStart
        '
        Me.btn_RealTimeStart.Location = New System.Drawing.Point(74, 39)
        Me.btn_RealTimeStart.Name = "btn_RealTimeStart"
        Me.btn_RealTimeStart.Size = New System.Drawing.Size(75, 23)
        Me.btn_RealTimeStart.TabIndex = 0
        Me.btn_RealTimeStart.Text = "대신 연결"
        Me.btn_RealTimeStart.UseVisualStyleBackColor = True
        '
        'grid1
        '
        Me.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid1.Location = New System.Drawing.Point(10, 162)
        Me.grid1.Margin = New System.Windows.Forms.Padding(1)
        Me.grid1.Name = "grid1"
        Me.grid1.ReadOnly = True
        Me.grid1.RowTemplate.Height = 23
        Me.grid1.ShowCellToolTips = False
        Me.grid1.Size = New System.Drawing.Size(1047, 906)
        Me.grid1.TabIndex = 1
        '
        'grd_selected
        '
        Me.grd_selected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_selected.Location = New System.Drawing.Point(1069, 162)
        Me.grd_selected.Name = "grd_selected"
        Me.grd_selected.RowTemplate.Height = 23
        Me.grd_selected.Size = New System.Drawing.Size(571, 906)
        Me.grd_selected.TabIndex = 2
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1069, 120)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(571, 36)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'cmb_selectedJongmokIndex_1
        '
        Me.cmb_selectedJongmokIndex_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_1.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_1.Location = New System.Drawing.Point(395, 3)
        Me.cmb_selectedJongmokIndex_1.Name = "cmb_selectedJongmokIndex_1"
        Me.cmb_selectedJongmokIndex_1.Size = New System.Drawing.Size(173, 23)
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
        Me.lbl_1.Size = New System.Drawing.Size(101, 36)
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
        Me.Label1.Location = New System.Drawing.Point(288, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Put 선택"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_selectedJongmokIndex_0
        '
        Me.cmb_selectedJongmokIndex_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_0.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_0.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_0.Location = New System.Drawing.Point(110, 3)
        Me.cmb_selectedJongmokIndex_0.Name = "cmb_selectedJongmokIndex_0"
        Me.cmb_selectedJongmokIndex_0.Size = New System.Drawing.Size(172, 23)
        Me.cmb_selectedJongmokIndex_0.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 1086)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(331, 191)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 153)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(158, 37)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "기준가"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_JongmokTargetPrice
        '
        Me.txt_JongmokTargetPrice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_JongmokTargetPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JongmokTargetPrice.Location = New System.Drawing.Point(169, 159)
        Me.txt_JongmokTargetPrice.Name = "txt_JongmokTargetPrice"
        Me.txt_JongmokTargetPrice.Size = New System.Drawing.Size(158, 24)
        Me.txt_JongmokTargetPrice.TabIndex = 9
        Me.txt_JongmokTargetPrice.Text = "2.0"
        Me.txt_JongmokTargetPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(158, 37)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "가격 하한"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_LowerLimit
        '
        Me.txt_LowerLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_LowerLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_LowerLimit.Location = New System.Drawing.Point(169, 121)
        Me.txt_LowerLimit.Name = "txt_LowerLimit"
        Me.txt_LowerLimit.Size = New System.Drawing.Size(158, 24)
        Me.txt_LowerLimit.TabIndex = 7
        Me.txt_LowerLimit.Text = "0.4"
        Me.txt_LowerLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(158, 37)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "가격 상한"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_UpperLimit
        '
        Me.txt_UpperLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_UpperLimit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UpperLimit.Location = New System.Drawing.Point(169, 83)
        Me.txt_UpperLimit.Name = "txt_UpperLimit"
        Me.txt_UpperLimit.Size = New System.Drawing.Size(158, 24)
        Me.txt_UpperLimit.TabIndex = 5
        Me.txt_UpperLimit.Text = "4.0"
        Me.txt_UpperLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(158, 37)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Interval"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_Interval
        '
        Me.txt_Interval.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txt_Interval.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Interval.Location = New System.Drawing.Point(169, 45)
        Me.txt_Interval.Name = "txt_Interval"
        Me.txt_Interval.Size = New System.Drawing.Size(158, 24)
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
        Me.Label2.Size = New System.Drawing.Size(158, 37)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "목표날짜"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_TargetDate
        '
        Me.txt_TargetDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_TargetDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TargetDate.Location = New System.Drawing.Point(169, 4)
        Me.txt_TargetDate.Name = "txt_TargetDate"
        Me.txt_TargetDate.Size = New System.Drawing.Size(158, 24)
        Me.txt_TargetDate.TabIndex = 1
        Me.txt_TargetDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'btn_TimerStart
        '
        Me.btn_TimerStart.Location = New System.Drawing.Point(168, 39)
        Me.btn_TimerStart.Name = "btn_TimerStart"
        Me.btn_TimerStart.Size = New System.Drawing.Size(75, 23)
        Me.btn_TimerStart.TabIndex = 5
        Me.btn_TimerStart.Text = "START"
        Me.btn_TimerStart.UseVisualStyleBackColor = True
        '
        'label_timerCounter
        '
        Me.label_timerCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_timerCounter.Location = New System.Drawing.Point(178, 65)
        Me.label_timerCounter.Name = "label_timerCounter"
        Me.label_timerCounter.Size = New System.Drawing.Size(62, 23)
        Me.label_timerCounter.TabIndex = 6
        Me.label_timerCounter.Text = "0"
        Me.label_timerCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Chart1
        '
        ChartArea1.AxisY.CustomLabels.Add(CustomLabel1)
        ChartArea1.AxisY.IsStartedFromZero = False
        ChartArea1.AxisY.LineColor = System.Drawing.Color.WhiteSmoke
        ChartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet
        ChartArea1.AxisY.MaximumAutoSize = 10.0!
        ChartArea1.Name = "ChartArea1"
        ChartArea1.Position.Auto = False
        ChartArea1.Position.Height = 37.0!
        ChartArea1.Position.Width = 94.0!
        ChartArea1.Position.X = 3.0!
        ChartArea1.Position.Y = 3.0!
        ChartArea2.AlignWithChartArea = "ChartArea1"
        ChartArea2.AxisX.IsStartedFromZero = False
        ChartArea2.AxisY.IsLabelAutoFit = False
        ChartArea2.AxisY.IsStartedFromZero = False
        ChartArea2.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
        ChartArea2.AxisY.MaximumAutoSize = 10.0!
        ChartArea2.Name = "ChartArea2"
        ChartArea2.Position.Auto = False
        ChartArea2.Position.Height = 37.0!
        ChartArea2.Position.Width = 94.0!
        ChartArea2.Position.X = 3.0!
        ChartArea2.Position.Y = 40.0!
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Me.Chart1.Location = New System.Drawing.Point(1653, 162)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick
        Series1.Name = "CALL_MAIN"
        Series1.ToolTip = "#VALY1{G4}"
        Series1.YValuesPerPoint = 4
        Series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Series2.Name = "CALL_HIGH"
        Series2.ToolTip = "#VALY1{G4}"
        Series2.YValuesPerPoint = 4
        Series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series3.Name = "CALL_LOW"
        Series4.ChartArea = "ChartArea2"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick
        Series4.Name = "PUT_MAIN"
        Series4.YValuesPerPoint = 4
        Series5.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot
        Series5.ChartArea = "ChartArea2"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Color = System.Drawing.Color.Maroon
        Series5.Name = "PUT_HIGH"
        Series6.BorderColor = System.Drawing.Color.White
        Series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot
        Series6.ChartArea = "ChartArea2"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Color = System.Drawing.Color.Navy
        Series6.Name = "PUT_LOW"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Series.Add(Series3)
        Me.Chart1.Series.Add(Series4)
        Me.Chart1.Series.Add(Series5)
        Me.Chart1.Series.Add(Series6)
        Me.Chart1.Size = New System.Drawing.Size(801, 906)
        Me.Chart1.TabIndex = 7
        Me.Chart1.Text = "Chart1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2184, 1405)
        Me.Controls.Add(Me.Chart1)
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
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
End Class
