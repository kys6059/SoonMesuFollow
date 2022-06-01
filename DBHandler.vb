Option Explicit On
Imports Google.Cloud.BigQuery.V2

Module DBHandler

    Public projectID As String = "kys1-244000"
    Public DataSetName As String = "kys1-244000.option5."
    Public tableName As String = ""

    Public DBTotalDateCount As Integer
    Public DBDateList() As Integer
    Public DBTotalRawDataList As Dictionary(Of Integer, List(Of BigQueryRow))
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
                Dim str As String = iDate.ToString() & " 해당 날짜 Data Count = " & cnt.ToString() + "개가 이미 등록되어 있습니다"
                Console.WriteLine(str)
                Add_Log("일반", str)
            Next

        Catch ex As Exception
            MsgBox("인증서가 없습니다")
        End Try

        Return cnt
    End Function

    '시뮬레이션이나 실시간으로 발생한 신호의 결과를 빅쿼리에 저장한다
    Public Sub InsertShinhoResult()
        Dim client As BigQueryClient = BigQueryClient.Create(projectID)
        Dim dateaset_id = "option5"
        Dim table_id = "Option_ShinhoResultTable"

        Dim totalCount As Integer = SimulationTotalShinhoList.Count - 1

        Dim rows(totalCount) As BigQueryInsertRow

        For i As Integer = 0 To totalCount

            Dim shinho As ShinhoType = SimulationTotalShinhoList(i)

            rows(i) = New BigQueryInsertRow
            rows(i).Add("A00_Month", shinho.A00_월물)
            rows(i).Add("A01_Date", shinho.A01_날짜)
            rows(i).Add("A02_Interval", shinho.A02_interval)
            rows(i).Add("A03_RemainDate", shinho.A03_남은날짜)
            rows(i).Add("A04_OccurIndex", shinho.A04_발생Index)
            rows(i).Add("A05_OccurTime", shinho.A05_발생시간)
            rows(i).Add("A06_ShinhoID", shinho.A06_신호ID)
            rows(i).Add("A07_ShinhoCount", shinho.A07신호차수)
            rows(i).Add("A11_CallIndex", shinho.A11_콜인덱스)
            rows(i).Add("A12_CallHangsaga", shinho.A12_콜행사가)
            rows(i).Add("A13_CallShinhoPrice", shinho.A13_콜신호발생가격)
            rows(i).Add("A14_CallBuyingPrice", shinho.A14_콜매수가격)
            rows(i).Add("A15_CallOrderNumber", shinho.A15_콜주문번호)
            rows(i).Add("A16_CallJongmokCode", shinho.A16_콜종목코드)
            rows(i).Add("A17_CallbuyingStatus", shinho.A17_콜체결상태)
            rows(i).Add("A21_PutIndex", shinho.A21_풋인덱스)
            rows(i).Add("A22_PutHangsaga", shinho.A22_풋행사가)
            rows(i).Add("A23_PutShinhoPrice", shinho.A23_풋신호발생가격)
            rows(i).Add("A24_PutBuyingPrice", shinho.A24_풋매수가격)
            rows(i).Add("A25_PutOrderNumber", shinho.A25_풋주문번호)
            rows(i).Add("A26_PutJongmokCode", shinho.A26_풋종목코드)
            rows(i).Add("A27_PutBuyingStatus", shinho.A27_풋체결상태)
            rows(i).Add("A31_ShinhoSumPrice", shinho.A31_신호합계가격)
            rows(i).Add("A32_CurrentSumPrice", shinho.A32_현재합계가격)
            rows(i).Add("A33_CurrentStatus", shinho.A33_현재상태)
            rows(i).Add("A34_ProfitRatio", shinho.A34_이익률)
            rows(i).Add("A35_SonjulPrice", shinho.A35_손절기준가격)
            rows(i).Add("A36_IkjulPrice", shinho.A36_익절기준가격)
            rows(i).Add("A37_SonjulRatio", shinho.A37_손절기준비율)
            rows(i).Add("A38_IkjulRatio", shinho.A38_익절기준비율)
            rows(i).Add("A39_MidSellFlag", shinho.A39_중간매도Flag)
            rows(i).Add("A40_TimeoutTime", shinho.A40_TimeoutTime)
            rows(i).Add("A41_SellTime", shinho.A41_매도시간)
            rows(i).Add("A42_SellIndex", shinho.A42_매도Index)
            rows(i).Add("A43_SellReason", shinho.A43_매도사유)
            rows(i).Add("A44_Memo", shinho.A44_메모)
            rows(i).Add("A45_OrderingPrice", shinho.A45_기준가격)
            rows(i).Add("A46_LastProfitRatio", shinho.A46_환산이익율)
            rows(i).Add("A47_IsReal", shinho.A47_IsReal)

        Next

        client.InsertRows(dateaset_id, table_id, rows)

        'Dim str As String = " 신호결과 저장완료 " & SimulationTotalShinhoList.Count.ToString() & " 개의 row가 등록"
        'Console.WriteLine(str)
        'Add_Log("일반", str)

    End Sub


    '일일 데이터를 빅쿼리에 저장한다
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

                        rows(retCount) = New BigQueryInsertRow '(retCount) 여기에 (retCount)이게 붙으면 배열의 크기만큼 잡는건데 이건 아닌거 같아서 일단 제거하고 살펴보기로 함 (20220601)
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

        client.InsertRows(dateaset_id, table_id, rows)

        Dim str As String = iDate.ToString() & " 해당 날짜 " & retCount.ToString() & " 개의 row가 등록"
        Console.WriteLine(str)
        Add_Log("일반", str)

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

    '빅쿼리 DB에 들어있는 전체 data를 가져온다  
    '왜냐하면 하루씩 가져오면 너무 느려진다 
    Public Function GetRawData(ByVal dateLimitText As String) As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob
        tableName = MakeTableName()
        Dim query As String = "select * from " + tableName + " " + dateLimitText + " order by cdate, iFlag, `index`, ctime  "
        Dim cnt As Integer = 0
        Dim dateCount As Integer = 0

        Dim prevDate As Integer = 0

        If DBTotalRawDataList Is Nothing Then
            DBTotalRawDataList = New Dictionary(Of Integer, List(Of BigQueryRow))
        Else
            DBTotalRawDataList.Clear()
        End If
        Dim datelist As Collections.Generic.List(Of Integer) = New Collections.Generic.List(Of Integer)

        Try
            client = BigQueryClient.Create(projectID)
            job = client.CreateQueryJob(query, Nothing)

            job.PollUntilCompleted()
            Add_Log("SQL = ", query)
            Dim row_cnt As Integer = client.GetQueryResults(job.Reference).Count

            Dim list = New List(Of BigQueryRow)

            For Each row In client.GetQueryResults(job.Reference)

                '값을 읽어온다
                Dim tempDate As Integer = Val(row("cdate"))

                If tempDate <> prevDate Then 'Data의 처음이 아니고 날짜가 바뀌었다 - 앞에 모아놓은 것들 다 처리해서 Dic에 넣는다

                    If prevDate <> 0 Then

                        dateCount += 1

                        DBTotalRawDataList.Add(prevDate, list)
                        datelist.Add(prevDate)

                        list = Nothing
                        list = New List(Of BigQueryRow)
                    End If

                    prevDate = tempDate

                End If


                '중간에는 Data()에 모아놓는다
                list.Add(row)
                cnt += 1
            Next

            If dateCount > 0 Then '마지막 날짜까지 가져온다

                dateCount += 1

                DBTotalRawDataList.Add(prevDate, list)
                datelist.Add(prevDate)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        DBDateList = datelist.ToArray()
        DBTotalDateCount = dateCount
        Return dateCount

    End Function

    '인수로 받은 날의 데이터를 자료구조로 올린다
    Public Function GetDataFromDBHandler(ByVal iDate As Integer) As Integer

        Dim cnt As Integer = 0
        Dim jongmokIndex As Integer = -1

        Dim iList = DBTotalRawDataList.Item(iDate)

        For Each row In iList

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

        Return jongmokIndex + 1
    End Function

End Module
