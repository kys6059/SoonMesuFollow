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
            End If


        End If

        Return False

    End Function
End Class
