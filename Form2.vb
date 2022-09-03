Option Explicit On
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form2

    Private Sub btn_f2_폼닫기_Click(sender As Object, e As EventArgs) Handles btn_f2_폼닫기.Click
        Me.Hide()
    End Sub

    Private Sub btn_F2_SelectDB_Click(sender As Object, e As EventArgs) Handles btn_F2_SelectDB.Click



        Add_Log("일반", "전체 Data 취합 Click")

        Dim dateCount As Integer = GetRawData_1min(txt_F2_DB_Date_Limit.Text, txt_F2_TableName.Text) '이걸하면 딕셔너리에 데이터를 넣고 날짜수를 리턴해줌
        Dim 순매수dateCount As Integer = GetRawData_순매수(txt_F2_DB_Date_Limit.Text, "soonMeSuTable") '이걸하면 딕셔너리에 데이터를 넣고 날짜수를 리턴해줌 - 순매수리스트
        If dateCount <> 순매수dateCount Then
            Add_Log("에러", "1분 데이터 카운트와 순매수 DateCount가 다름" & dateCount.ToString() & " : " & 순매수dateCount.ToString())
            Return
        End If

        Add_Log("일반", "DB 전체 일 수는 " + dateCount.ToString() + " 일")

        If dateCount > 0 Then
            HSc_F2_날짜조절.Maximum = dateCount - 1
            HSc_F2_날짜조절.LargeChange = 1

            HSc_F2_날짜조절.Refresh()

            InitDataStructure_1Min()
            isRealFlag = False   'DB에서 읽어서 분석하면 false를 한다

            Dim targetDateIndex_1min As Integer = 0

            TargetDate = DBDateList_1Min(targetDateIndex_1min)
            sMonth = getsMonth(TargetDate).ToString() 'DB에서 읽은 날짜로부터 월물을 찾아낸다

            TotalCount = GetDataFromDBHandler_1Min(TargetDate)
            순매수리스트카운트 = Get순매수데이터(TargetDate) '전역변수 순매수리스트에 하루치 Data를 입력한다

            If TotalCount > 0 And 순매수리스트카운트 > 0 Then
                'MakeOptinList()  이건 리스트를 만드는 기능인데 이건 매수할 때 필요가 없다. 
                'F2_Clac_DisplayAllGrid()

            End If

        Else
            MsgBox("DB에 데이터가 없습니다")
        End If

    End Sub

End Class