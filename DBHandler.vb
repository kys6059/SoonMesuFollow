Option Explicit On
Imports Google.Cloud.BigQuery.V2

Module DBHandler

    Dim projectID As String = "kys1-244000"
    Dim tableName As String = "kys1-244000.option5.option"

    '그날 데이터를 입력하기 전에 해당 날짜에 이미 Data가 있는지 확인하기 위해 그 날짜 Data 건수를 가져온다
    Public Function GetRowCount(ByVal iDate As Integer) As Integer

        Dim client As BigQueryClient
        Dim query As String = "select count(*) as cnt from " + tableName + " Where cdate = " + iDate.ToString()
        Dim cnt As Integer = -1

        Try
            client = BigQueryClient.Create(projectID)
            Dim job As BigQueryJob = client.CreateQueryJob(query, Nothing)

            'wait for the job to complete
            job.PollUntilCompleted()

            For Each row In client.GetQueryResults(job.Reference)
                cnt = Val(row("cnt"))
                Console.WriteLine(cnt.ToString())
            Next

        Catch ex As Exception
            MsgBox("인증서가 없습니다")
        End Try













        Return cnt
    End Function

End Module
