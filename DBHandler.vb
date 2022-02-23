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

    Public Function InsertTargetDateData(ByVal iDate As Integer) As Integer

        Dim retCount As Integer = 0
        Dim i, callput As Integer
        Dim iFlag As Integer
        Dim client As BigQueryClient = BigQueryClient.Create(projectID)
        Dim 영보다큰갯수 As Integer = 0
        Dim dateaset_id = "option5"
        Dim table_id = "option"


        For callput = 0 To 1
            For i = 0 To TotalCount - 1
                For j = 0 To currentIndex
                    If Data(i).price(callput, j, 0) > 0 Then
                        영보다큰갯수 += 1
                    End If
                Next
            Next
        Next

        Dim rows(영보다큰갯수 - 1) As BigQueryInsertRow   '배열 갯수 주의해야 함. 1을 빼지 않으면 마지막 열이 nothing이 되어 아래 Insert에서 오류가 남

        For callput = 0 To 1
            For i = 0 To TotalCount - 1
                For j = 0 To currentIndex

                    If callput = 0 Then
                        iFlag = 1
                    Else
                        iFlag = 6
                    End If

                    If Data(i).price(callput, j, 0) > 0 Then   '영보다큰갯수와 동일한 로직으로 입력

                        rows(retCount) = New BigQueryInsertRow(retCount)
                        rows(retCount).Add("cdate", iDate)
                        rows(retCount).Add("index", i)
                        rows(retCount).Add("hangsaga", Data(i).HangSaGa)
                        rows(retCount).Add("iFlag", iFlag)
                        rows(retCount).Add("ctime", Data(i).ctime(j))
                        rows(retCount).Add("interval", Interval)
                        rows(retCount).Add("si", Data(i).price(callput, j, 0))
                        rows(retCount).Add("go", Data(i).price(callput, j, 1))
                        rows(retCount).Add("jue", Data(i).price(callput, j, 2))
                        rows(retCount).Add("jong", Data(i).price(callput, j, 3))
                        rows(retCount).Add("volume", Data(i).거래량(callput, j))

                        retCount += 1
                    End If
                Next
            Next
        Next
        Console.WriteLine(retCount.ToString() + "개의 row가 등록예정 at " + Now.ToString)
        client.InsertRows(dateaset_id, table_id, rows)
        Console.WriteLine("등록 완료 at " + Now.ToString)

        Return retCount
    End Function

End Module
