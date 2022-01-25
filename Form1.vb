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

        Dim ConnectionState = FindTargetDate() '현재 사이보스에 접속된 상태라면

        If ConnectionState = True Then




        End If

        Return False

    End Function
End Class
