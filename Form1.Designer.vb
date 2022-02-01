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
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_RealTimeStart
        '
        Me.btn_RealTimeStart.Location = New System.Drawing.Point(86, 36)
        Me.btn_RealTimeStart.Name = "btn_RealTimeStart"
        Me.btn_RealTimeStart.Size = New System.Drawing.Size(87, 21)
        Me.btn_RealTimeStart.TabIndex = 0
        Me.btn_RealTimeStart.Text = "대신 연결"
        Me.btn_RealTimeStart.UseVisualStyleBackColor = True
        '
        'grid1
        '
        Me.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid1.Location = New System.Drawing.Point(12, 108)
        Me.grid1.Name = "grid1"
        Me.grid1.ReadOnly = True
        Me.grid1.RowTemplate.Height = 23
        Me.grid1.Size = New System.Drawing.Size(1221, 836)
        Me.grid1.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2362, 956)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.btn_RealTimeStart)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_RealTimeStart As Button
    Friend WithEvents grid1 As DataGridView
End Class
