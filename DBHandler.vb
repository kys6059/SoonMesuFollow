Option Explicit On
Imports Google.Cloud.BigQuery.V2

Module DBHandler

    Public projectID As String = "kys1-244000"
    Public DataSetName As String = "kys1-244000.option5."
    Public tableName As String = ""

    Public DBTotalDateCount As Integer
    Public DBDateList() As Integer
    Public DBTotalRawDataList As Dictionary(Of Integer, DataSet())
    Public gTargetDateIndex As Integer

    Public Function MakeTableName() As String

        Dim str As String
        str = DataSetName + Form1.txt_TableName.Text + " "

        Return str

    End Function

    '그날 데이터를 입력하기 전에 해당 날짜에 이미 Data가 있는지 확인하기 위해 그 날짜 Data 건수를 가져온다
    Public Function GetRowCount(ByVal iDate As Integer) As Integer

        Dim client As BigQueryClient

        tableName = MakeTableName()

        Dim query As String = "select count(*) as cnt from " + tableName + " Where cdate = " + iDate.ToString()
        Dim cnt As Integer = -1
        Dim job As BigQueryJob
        Try
            client = BigQueryClient.Create(projectID)
            job = client.CreateQueryJob(query, Nothing)

            'wait for the job to complete
            job.PollUntilCompleted()

            For Each row In client.GetQueryResults(job.Reference)
                cnt = Val(row("cnt"))
                Dim str As String = iDate.ToString() & " Data Count = " & cnt.ToString() + "개"
                Console.WriteLine(str)
                Add_Log("일반", str)
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
        Dim table_id = Form1.txt_TableName.Text


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



    Public Function GetDateList() As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob

        tableName = MakeTableName()
        Dim query As String = "select distinct(cdate) as cdate from " + tableName + " order by cdate "

        Dim datelist As Collections.Generic.List(Of Integer) = New Collections.Generic.List(Of Integer)

        Try
            client = BigQueryClient.Create(projectID)
            job = client.CreateQueryJob(query, Nothing)

            job.PollUntilCompleted()

            For Each row In client.GetQueryResults(job.Reference)
                Dim iDate As Integer = Val(row("cdate"))
                Console.WriteLine(iDate.ToString())
                datelist.Add(iDate)
            Next

        Catch ex As Exception
            MsgBox("빅쿼리 DB용 인증서가 없습니다")
        End Try

        DBTotalDateCount = datelist.Count

        DBDateList = datelist.ToArray()

        Return DBTotalDateCount

    End Function


    '날짜를 입력받아 Data구조체에다가 입력하는 함수
    Public Function GetDailyRawData(ByVal iDate As Integer) As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob
        tableName = MakeTableName()
        Dim query As String = "select * from " + tableName + " where cdate = " + iDate.ToString() + " order by cdate, iFlag, `index`, ctime"
        Dim cnt As Integer = 0
        Dim jongmokIndex As Integer = -1

        Console.WriteLine("query = " + query)

        Try
            client = BigQueryClient.Create(projectID)
            job = client.CreateQueryJob(query, Nothing)

            job.PollUntilCompleted()

            Dim row_cnt As Integer = client.GetQueryResults(job.Reference).Count

            For Each row In client.GetQueryResults(job.Reference)

                '값을 읽어온다
                Dim tempDate As Integer = Val(row("cdate"))
                Dim index As Integer = Val(row("index"))
                Dim hangsaga As Integer = Val(row("hangsaga"))
                Dim iFlag As Integer = Val(row("iFlag"))
                Dim ctime As Integer = Val(row("ctime"))
                Dim interval As Integer = Val(row("interval"))
                Dim si As Single = Val(row("si"))
                Dim go As Single = Val(row("go"))
                Dim jue As Single = Val(row("jue"))
                Dim jong As Single = Val(row("jong"))
                Dim volume As Integer = Val(row("volume"))

                Dim callput As Integer
                If iFlag = 1 Then
                    callput = 0
                ElseIf iFlag = 6 Then
                    callput = 1
                Else
                    Console.WriteLine(iDate + " 날에 iFlag가 0인게 섞여있음")
                    callput = 0
                End If

                If index <> jongmokIndex Then
                    jongmokIndex = index
                    '시간입력
                    SetTimeDataForDataForDBData(Data, jongmokIndex)
                    Data(jongmokIndex).HangSaGa = hangsaga
                End If

                Dim iIndex As Integer = FindIndexFormTime(ctime.ToString()) '해당 시간이 몇번째 인덱스인지 찾아온다
                currentIndex = Math.Max(currentIndex, iIndex)
                timeIndex = Math.Max(timeIndex, currentIndex + 1)

                Data(jongmokIndex).price(callput, iIndex, 0) = si
                Data(jongmokIndex).price(callput, iIndex, 1) = go
                Data(jongmokIndex).price(callput, iIndex, 2) = jue
                Data(jongmokIndex).price(callput, iIndex, 3) = jong
                Data(jongmokIndex).거래량(callput, iIndex) = volume

                cnt += 1
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Console.WriteLine("Row 개수는 " + cnt.ToString())


        Return jongmokIndex + 1

    End Function

    '대신에서 가져오면 옵션 종목수를 미리 알고 있어서 TotalCount 만큼 For문 돌리면 되지만
    'DB에서 가져오면 종목수를 알 수 없기 때문에 종목이 추가될 때마다 시간을 Data 구조체에 입력하는 방식으로 처리한다
    Public Sub SetTimeDataForDataForDBData(ByRef tempData() As DataSet, ByVal jongmokIndex As Integer)
        Dim si, bun As Integer
        Dim strTemp As String
        Dim totalTimeCount, num As Integer

        totalTimeCount = Int(420 / Interval)

        For num = 1 To totalTimeCount

            If Interval = 1 Then
                si = Int(num / 60) + 9
                bun = Int(num Mod 60)
            ElseIf Interval = 5 Then
                si = Int(num / 12) + 9
                bun = Int((num Mod 12) * 5)
            End If

            si = si * 100 + bun
            strTemp = Str(si)
            tempData(jongmokIndex).ctime(num - 1) = strTemp '그리드 왼쪽 기준 시간의 값을 입력한다. 이건 나중에 SearchLine에서 쓰인다
        Next

    End Sub

    '빅쿼리 DB에 들어있는 전체 data를 가져온다  -------------- 일단 너무 복잡해지니 사용하지 않고 하루하루치 Data를 가져오는 걸 먼저 구현한다
    '왜냐하면 하루씩 가져오면 너무 느려진다 
    Public Function GetRawData() As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob
        Dim query As String = "select * from " + tableName + " order by cdate, iFlag, `index`, ctime "
        Dim cnt As Integer = 0
        Dim tempDate As Integer

        Dim prevDate As Integer = 0
        Dim i As Integer

        If DBTotalRawDataList Is Nothing Then
            DBTotalRawDataList = New Dictionary(Of Integer, DataSet())
        Else
            DBTotalRawDataList.Clear()
        End If

        Try
            client = BigQueryClient.Create(projectID)
            job = client.CreateQueryJob(query, Nothing)

            job.PollUntilCompleted()

            Dim row_cnt As Integer = client.GetQueryResults(job.Reference).Count

            For i = 0 To row_cnt - 1

                Dim row As BigQueryResults = client.GetQueryResults(job.Reference)

                tempDate = Val(row("cdate"))

                If tempDate <> prevDate And prevDate <> 0 Then 'Data의 처음이 아니고 날짜가 바뀌었다 - 앞에 모아놓은 것들 다 처리해서 Dic에 넣는다



                End If


                '중간에는 Data()에 모아놓는다



                'Data의 끝이라면 dic에 넣는다




                'DBTotalRawDataList.Add(row)
                cnt += 1
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return cnt

    End Function

    '인수로 받은 날의 데이터를 자료구조로 올린다
    Public Function GetDataFromDBHandler(ByVal iDate As Integer) As Integer



        Return 0
    End Function

End Module
