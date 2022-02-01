Option Explicit On

Public Class Form1

    Private Sub btn_RealTimeStart_Click(sender As Object, e As EventArgs) Handles btn_RealTimeStart.Click
        Dim ret As Boolean

        ret = realTime_Start()

        If ret = False Then
            ' MsgBox("실시간 연결 종목이 없습니다")
        End If

    End Sub

    Private Function realTime_Start() As Boolean

        InitDataStructure()
        InitObject()

        isRealFlag = True '실시간 로직임을 기억한다

        Dim ConnectionState = FindTargetDate() '현재 사이보스에 접속된 상태라면

        If ConnectionState = True Then

            TotalCount = GetTotalJongmokCount()

            Do While TotalCount > 28                  '15초당 60개 TR 제한을 고려하여 최대 각 28개만 받아온다
                UpperLimit = UpperLimit - 0.15
                LowerLimt = LowerLimt + 0.05

                TotalCount = GetTotalJongmokCount()
            Loop

            If TotalCount > 0 Then

                SetTimeDataForData() '미리 data구조체에 시간을 다 입력해 놓는다. 카운트만큼

                GetAllData()

                RedrawAll()
            End If


        End If

        Return False

    End Function

    Private Sub RedrawAll()

        If currentIndex > 0 Then
            'grid1.Visible = False
            InitFirstGrid()
            DrawGrid1Data()
        End If

    End Sub

    Private Sub InitFirstGrid()
        Dim jongMok As String
        Dim i, j As Integer

        '전체 크기 지정
        grid1.ColumnCount = TotalCount * 10 + 1
        grid1.RowCount = timeIndex + 3

        grid1.Columns(0).HeaderText = "시간"
        grid1.Columns(TotalCount * 10).HeaderText = "시간"
        grid1.Columns(0).Width = 40
        grid1.Columns(TotalCount * 10).Width = 40

        grid1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '0번 컬럼과 마지막 컬럼에 시간을 넣고 5번열에는 종목행사가를 넣는다
        For j = 0 To timeIndex + 1
            grid1.Rows(j).Height = 21 '전체 Row 높이 지정
            grid1.Rows(j).Cells(0).Value = Data(0).ctime(j)
            grid1.Rows(j).Cells(TotalCount * 10).Value = Data(0).ctime(j)

        Next

        For i = 0 To TotalCount - 1
            jongMok = Data(i).HangSaGa

            grid1.Columns(i * 10 + 1).HeaderText = "시"
            grid1.Columns(i * 10 + 2).HeaderText = "고"
            grid1.Columns(i * 10 + 3).HeaderText = "저"
            grid1.Columns(i * 10 + 4).HeaderText = "종"
            grid1.Columns(i * 10 + 5).HeaderText = jongMok
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

        Next

        For i = 0 To TotalCount - 2
            grid1.Columns(i * 10 + 10).Width = 10
        Next

        grid1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub DrawGrid1Data()
        Dim i, j As Integer

        For i = 0 To TotalCount - 1
            For j = 0 To currentIndex + 1

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


End Class
