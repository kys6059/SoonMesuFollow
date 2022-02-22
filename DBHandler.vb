Option Explicit On
Imports Google.Cloud.BigQuery.V2

Module DBHandler

    Dim projectID As String = "kys1-244000"
    Dim tableName As String = "kys1-244000.option5.option"

    Public Function GetRowCount(ByVal iDate As Integer) As Integer

        Dim client As BigQueryClient = BigQueryClient.Create(projectID)

        Dim query As String = "select count(*) as cnt from " + tableName + " Where cdate = " + iDate.ToString()

        'Dim job As BigQueryJob = client.CreateQueryJob(query)
        'Dim job As BigQueryJob = client.CreateQueryJob(query, )



        Return 0
    End Function

End Module
