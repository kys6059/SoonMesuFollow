Option Explicit On

Imports CPSYSDIBLib
Imports CPUTILLib
Imports DSCBO1Lib

Module realtime_daishin

    Dim callputobj = New OptionCallput
    Dim oCur = New OptionCurOnly
    Dim optcodeobj As New CpOptionCode



    Public Sub InitObject()

        sMonth = optcodeobj.GetData(3, 0)
        Form1.Text = sMonth

    End Sub

    Public Function FindTargetDate() As Boolean
        Dim futureCode As String
        Dim CurrentTime As String
        Dim ret As Boolean

        Dim futurelist As New CpFutureCode  '선물
        Dim fMst As New FutureMst

        futureCode = futurelist.GetData(0, 0) '선물 코드
        fMst.SetInputValue(0, futureCode)
        ret = True



        fMst.BlockRequest()

        ''MsgBox("사이보스에 접속되지 않은 상태입니다")
        ret = False

        If ret = True Then
            CurrentTime = fMst.GetHeaderValue(82) '체결시간
            TargetDate = fMst.GetHeaderValue(31) '입회일자
        End If

        Return ret

    End Function



End Module
