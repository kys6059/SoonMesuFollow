<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btn_RealTimeStart = New System.Windows.Forms.Button()
        Me.grid1 = New System.Windows.Forms.DataGridView()
        Me.grd_selected = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmb_selectedJongmokIndex_1 = New System.Windows.Forms.ComboBox()
        Me.lbl_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_selectedJongmokIndex_0 = New System.Windows.Forms.ComboBox()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.grid1.Size = New System.Drawing.Size(1047, 906)
        Me.grid1.TabIndex = 1
        '
        'grd_selected
        '
        Me.grd_selected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_selected.Location = New System.Drawing.Point(1082, 162)
        Me.grd_selected.Name = "grd_selected"
        Me.grd_selected.RowTemplate.Height = 23
        Me.grd_selected.Size = New System.Drawing.Size(646, 906)
        Me.grd_selected.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmb_selectedJongmokIndex_1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmb_selectedJongmokIndex_0, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1082, 120)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(646, 36)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'cmb_selectedJongmokIndex_1
        '
        Me.cmb_selectedJongmokIndex_1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_1.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_1.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_1.Location = New System.Drawing.Point(486, 3)
        Me.cmb_selectedJongmokIndex_1.Name = "cmb_selectedJongmokIndex_1"
        Me.cmb_selectedJongmokIndex_1.Size = New System.Drawing.Size(157, 23)
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
        Me.lbl_1.Size = New System.Drawing.Size(155, 36)
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
        Me.Label1.Location = New System.Drawing.Point(325, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Put 선택"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_selectedJongmokIndex_0
        '
        Me.cmb_selectedJongmokIndex_0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmb_selectedJongmokIndex_0.Font = New System.Drawing.Font("굴림", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cmb_selectedJongmokIndex_0.FormattingEnabled = True
        Me.cmb_selectedJongmokIndex_0.Location = New System.Drawing.Point(164, 3)
        Me.cmb_selectedJongmokIndex_0.Name = "cmb_selectedJongmokIndex_0"
        Me.cmb_selectedJongmokIndex_0.Size = New System.Drawing.Size(155, 23)
        Me.cmb_selectedJongmokIndex_0.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2025, 1121)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.grd_selected)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.btn_RealTimeStart)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd_selected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
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
End Class
