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
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
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
        Me.txt_F2_Log = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_선행_포인트_마진 = New System.Windows.Forms.RichTextBox()
        Me.txt_신호허용거리배수 = New System.Windows.Forms.RichTextBox()
        Me.txt_최대포인트수대비비율 = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.F2_Chart_순매수, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(673, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(732, 154)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'HSc_F2_날짜조절
        '
        Me.HSc_F2_날짜조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_날짜조절.Location = New System.Drawing.Point(557, 52)
        Me.HSc_F2_날짜조절.Name = "HSc_F2_날짜조절"
        Me.HSc_F2_날짜조절.Size = New System.Drawing.Size(173, 48)
        Me.HSc_F2_날짜조절.TabIndex = 8
        '
        'Lbl_F2_현재시간Index
        '
        Me.Lbl_F2_현재시간Index.AutoSize = True
        Me.Lbl_F2_현재시간Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재시간Index.Location = New System.Drawing.Point(3, 103)
        Me.Lbl_F2_현재시간Index.Margin = New System.Windows.Forms.Padding(1)
        Me.Lbl_F2_현재시간Index.Name = "Lbl_F2_현재시간Index"
        Me.Lbl_F2_현재시간Index.Size = New System.Drawing.Size(273, 48)
        Me.Lbl_F2_현재시간Index.TabIndex = 6
        Me.Lbl_F2_현재시간Index.Text = "X건 중 Y"
        Me.Lbl_F2_현재시간Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lbl_F2_현재날짜Index
        '
        Me.Lbl_F2_현재날짜Index.AutoSize = True
        Me.Lbl_F2_현재날짜Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lbl_F2_현재날짜Index.Location = New System.Drawing.Point(560, 102)
        Me.Lbl_F2_현재날짜Index.Name = "Lbl_F2_현재날짜Index"
        Me.Lbl_F2_현재날짜Index.Size = New System.Drawing.Size(167, 50)
        Me.Lbl_F2_현재날짜Index.TabIndex = 4
        Me.Lbl_F2_현재날짜Index.Text = "X일 중 Y일"
        Me.Lbl_F2_현재날짜Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_F2_DB_Date_Limit
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.txt_F2_DB_Date_Limit, 3)
        Me.txt_F2_DB_Date_Limit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_DB_Date_Limit.Location = New System.Drawing.Point(282, 5)
        Me.txt_F2_DB_Date_Limit.Name = "txt_F2_DB_Date_Limit"
        Me.txt_F2_DB_Date_Limit.Size = New System.Drawing.Size(445, 21)
        Me.txt_F2_DB_Date_Limit.TabIndex = 0
        Me.txt_F2_DB_Date_Limit.Text = "where cdate >= 220801"
        '
        'txt_F2_TableName
        '
        Me.txt_F2_TableName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_F2_TableName.Location = New System.Drawing.Point(387, 55)
        Me.txt_F2_TableName.Name = "txt_F2_TableName"
        Me.txt_F2_TableName.Size = New System.Drawing.Size(165, 21)
        Me.txt_F2_TableName.TabIndex = 2
        Me.txt_F2_TableName.Text = "option_one_minute"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(282, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 48)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "테이블명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HSc_F2_시간조절
        '
        Me.HSc_F2_시간조절.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HSc_F2_시간조절.Location = New System.Drawing.Point(2, 52)
        Me.HSc_F2_시간조절.Name = "HSc_F2_시간조절"
        Me.HSc_F2_시간조절.Size = New System.Drawing.Size(275, 48)
        Me.HSc_F2_시간조절.TabIndex = 5
        '
        'btn_F2_SelectDB
        '
        Me.btn_F2_SelectDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_F2_SelectDB.Location = New System.Drawing.Point(387, 105)
        Me.btn_F2_SelectDB.Name = "btn_F2_SelectDB"
        Me.btn_F2_SelectDB.Size = New System.Drawing.Size(165, 44)
        Me.btn_F2_SelectDB.TabIndex = 7
        Me.btn_F2_SelectDB.Text = "DB_가져오기"
        Me.btn_F2_SelectDB.UseVisualStyleBackColor = True
        '
        'F2_Chart_순매수
        '
        ChartArea3.Name = "ChartArea1"
        Me.F2_Chart_순매수.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.F2_Chart_순매수.Legends.Add(Legend3)
        Me.F2_Chart_순매수.Location = New System.Drawing.Point(12, 172)
        Me.F2_Chart_순매수.Name = "F2_Chart_순매수"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.F2_Chart_순매수.Series.Add(Series3)
        Me.F2_Chart_순매수.Size = New System.Drawing.Size(1393, 878)
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(1411, 172)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(305, 39)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'btn_점의수늘리기
        '
        Me.btn_점의수늘리기.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_점의수늘리기.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_점의수늘리기.Location = New System.Drawing.Point(205, 3)
        Me.btn_점의수늘리기.Name = "btn_점의수늘리기"
        Me.btn_점의수늘리기.Size = New System.Drawing.Size(97, 33)
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
        'txt_F2_Log
        '
        Me.txt_F2_Log.Location = New System.Drawing.Point(1411, 228)
        Me.txt_F2_Log.Name = "txt_F2_Log"
        Me.txt_F2_Log.Size = New System.Drawing.Size(727, 277)
        Me.txt_F2_Log.TabIndex = 5
        Me.txt_F2_Log.Text = ""
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Controls.Add(Me.txt_최대포인트수대비비율, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_신호허용거리배수, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label6, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txt_선행_포인트_마진, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(148, 16)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(280, 115)
        Me.TableLayoutPanel3.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 33)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "선행포인트수 마진"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 40)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(180, 33)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "신호 허용거리 배수"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(3, 77)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(180, 35)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "최대포인트수_배율"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_선행_포인트_마진
        '
        Me.txt_선행_포인트_마진.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_선행_포인트_마진.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_선행_포인트_마진.Location = New System.Drawing.Point(189, 5)
        Me.txt_선행_포인트_마진.Name = "txt_선행_포인트_마진"
        Me.txt_선행_포인트_마진.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txt_선행_포인트_마진.Size = New System.Drawing.Size(86, 29)
        Me.txt_선행_포인트_마진.TabIndex = 5
        Me.txt_선행_포인트_마진.Text = "0.9"
        '
        'txt_신호허용거리배수
        '
        Me.txt_신호허용거리배수.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_신호허용거리배수.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_신호허용거리배수.Location = New System.Drawing.Point(189, 42)
        Me.txt_신호허용거리배수.Name = "txt_신호허용거리배수"
        Me.txt_신호허용거리배수.Size = New System.Drawing.Size(86, 29)
        Me.txt_신호허용거리배수.TabIndex = 6
        Me.txt_신호허용거리배수.Text = "3.0"
        '
        'txt_최대포인트수대비비율
        '
        Me.txt_최대포인트수대비비율.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_최대포인트수대비비율.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txt_최대포인트수대비비율.Location = New System.Drawing.Point(189, 79)
        Me.txt_최대포인트수대비비율.Name = "txt_최대포인트수대비비율"
        Me.txt_최대포인트수대비비율.Size = New System.Drawing.Size(86, 31)
        Me.txt_최대포인트수대비비율.TabIndex = 7
        Me.txt_최대포인트수대비비율.Text = "4.0"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(2484, 1062)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.txt_F2_Log)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.F2_Chart_순매수)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btn_f2_폼닫기)
        Me.Name = "Form2"
        Me.Text = "매수기능"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.F2_Chart_순매수, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
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
    Friend WithEvents txt_F2_Log As RichTextBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents txt_신호허용거리배수 As RichTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_선행_포인트_마진 As RichTextBox
    Friend WithEvents txt_최대포인트수대비비율 As RichTextBox
End Class
