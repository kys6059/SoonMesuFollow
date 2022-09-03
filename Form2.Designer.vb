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
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.HSc_F2_날짜조절, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Lbl_F2_현재시간Index, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Lbl_F2_현재날짜Index, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txt_F2_DB_Date_Limit, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txt_F2_TableName, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.HSc_F2_시간조절, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_F2_SelectDB, 2, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1114, 24)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(732, 126)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'HSc_F2_날짜조절
        '
        Me.HSc_F2_날짜조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_날짜조절.Location = New System.Drawing.Point(547, 42)
        Me.HSc_F2_날짜조절.Name = "HSc_F2_날짜조절"
        Me.HSc_F2_날짜조절.Size = New System.Drawing.Size(184, 40)
        Me.HSc_F2_날짜조절.TabIndex = 8
        '
        'Lbl_F2_현재시간Index
        '
        Me.Lbl_F2_현재시간Index.AutoSize = True
        Me.Lbl_F2_현재시간Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재시간Index.Location = New System.Drawing.Point(2, 84)
        Me.Lbl_F2_현재시간Index.Margin = New System.Windows.Forms.Padding(1)
        Me.Lbl_F2_현재시간Index.Name = "Lbl_F2_현재시간Index"
        Me.Lbl_F2_현재시간Index.Size = New System.Drawing.Size(179, 40)
        Me.Lbl_F2_현재시간Index.TabIndex = 6
        Me.Lbl_F2_현재시간Index.Text = "X건 중 Y"
        Me.Lbl_F2_현재시간Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lbl_F2_현재날짜Index
        '
        Me.Lbl_F2_현재날짜Index.AutoSize = True
        Me.Lbl_F2_현재날짜Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재날짜Index.Location = New System.Drawing.Point(550, 83)
        Me.Lbl_F2_현재날짜Index.Name = "Lbl_F2_현재날짜Index"
        Me.Lbl_F2_현재날짜Index.Size = New System.Drawing.Size(178, 42)
        Me.Lbl_F2_현재날짜Index.TabIndex = 4
        Me.Lbl_F2_현재날짜Index.Text = "X일 중 Y일"
        Me.Lbl_F2_현재날짜Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_DB_Date_Limit
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txt_F2_DB_Date_Limit, 3)
        Me.txt_F2_DB_Date_Limit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_DB_Date_Limit.Location = New System.Drawing.Point(186, 4)
        Me.txt_F2_DB_Date_Limit.Name = "txt_F2_DB_Date_Limit"
        Me.txt_F2_DB_Date_Limit.Size = New System.Drawing.Size(542, 21)
        Me.txt_F2_DB_Date_Limit.TabIndex = 0
        Me.txt_F2_DB_Date_Limit.Text = "where cdate > 220801"
        '
        'txt_F2_TableName
        '
        Me.txt_F2_TableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_TableName.Location = New System.Drawing.Point(368, 45)
        Me.txt_F2_TableName.Name = "txt_F2_TableName"
        Me.txt_F2_TableName.Size = New System.Drawing.Size(175, 21)
        Me.txt_F2_TableName.TabIndex = 2
        Me.txt_F2_TableName.Text = "option_one_minute"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(186, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 40)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "테이블명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HSc_F2_시간조절
        '
        Me.HSc_F2_시간조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_시간조절.Location = New System.Drawing.Point(1, 42)
        Me.HSc_F2_시간조절.Name = "HSc_F2_시간조절"
        Me.HSc_F2_시간조절.Size = New System.Drawing.Size(181, 40)
        Me.HSc_F2_시간조절.TabIndex = 5
        '
        'btn_F2_SelectDB
        '
        Me.btn_F2_SelectDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_SelectDB.Location = New System.Drawing.Point(368, 86)
        Me.btn_F2_SelectDB.Name = "btn_F2_SelectDB"
        Me.btn_F2_SelectDB.Size = New System.Drawing.Size(175, 36)
        Me.btn_F2_SelectDB.TabIndex = 7
        Me.btn_F2_SelectDB.Text = "DB_가져오기"
        Me.btn_F2_SelectDB.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(2484, 1062)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btn_f2_폼닫기)
        Me.Name = "Form2"
        Me.Text = "매수기능"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
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
End Class
