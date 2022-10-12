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

    '1분Data를 위한 자료구조 추가 20220903
    Public DBTotalRawDataList_1min As Dictionary(Of Integer, List(Of BigQueryRow))
    Public DBDateList_1Min() As Integer



    Public DBTotalRawDataList_순매수 As Dictionary(Of Integer, List(Of BigQueryRow))


    Public Function MakeTableName(ByVal tableName As String) As String

        Dim str As String
        str = DataSetName + tableName + " "
        Return str

    End Function

    '그날 데이터를 입력하기 전에 해당 날짜에 이미 Data가 있는지 확인하기 위해 그 날짜 Data 건수를 가져온다
    Public Function GetRowCount(ByVal iDate As Integer, ByVal TableName As String) As Integer

        Dim client As BigQueryClient

        Dim TableFullName As String = MakeTableName(TableName)

        Dim query As String = "select count(*) as cnt from " + TableFullName + " Where cdate = " + iDate.ToString()
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

        Dim totalCount1 As Integer = SimulationTotalShinhoList.Count - 1

        Dim rows(totalCount1) As BigQueryInsertRow

        For i As Integer = 0 To totalCount1

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
            rows(i).Add("A48_TotalCondition", shinho.A48_조건전체)

        Next

        client.InsertRows(dateaset_id, table_id, rows)

        'Dim str As String = " 신호결과 저장완료 " & SimulationTotalShinhoList.Count.ToString() & " 개의 row가 등록"
        'Console.WriteLine(str)
        'Add_Log("일반", str)

    End Sub


    '일일 데이터를 빅쿼리에 저장한다
    Public Function InsertTargetDateData(ByVal iDate As Integer, ByVal table_id As String) As Integer

        Dim retCount As Integer = 0
        Dim i, callput As Integer
        Dim iFlag As Integer
        Dim client As BigQueryClient = BigQueryClient.Create(projectID)
        Dim 영보다큰갯수 As Integer = 0
        Dim dateaset_id = "option5"

        For callput = 0 To 1
            For j = 0 To currentIndex
                If Data(callput).price(j, 0) > 0 Then
                    영보다큰갯수 += 1
                End If
            Next
        Next

        Dim rows(영보다큰갯수 - 1) As BigQueryInsertRow   '배열 갯수 주의해야 함. 1을 빼지 않으면 마지막 열이 nothing이 되어 아래 Insert에서 오류가 남

        For callput = 0 To 1

            Dim it As ListTemplate = optionList(selectedJongmokIndex(callput))
            Dim hangsaga As Integer = Val(it.HangSaGa)

            For j = 0 To currentIndex

                If callput = 0 Then
                    iFlag = 1
                Else
                    iFlag = 6
                End If

                If Data(callput).price(j, 0) > 0 Then   '영보다큰갯수와 동일한 로직으로 입력

                    rows(retCount) = New BigQueryInsertRow '(retCount) 여기에 (retCount)이게 붙으면 배열의 크기만큼 잡는건데 이건 아닌거 같아서 일단 제거하고 살펴보기로 함 (20220601)
                    rows(retCount).Add("cdate", iDate)
                    rows(retCount).Add("index", 0)
                    rows(retCount).Add("hangsaga", hangsaga)
                    rows(retCount).Add("iFlag", iFlag)
                    rows(retCount).Add("ctime", Data(i).ctime(j))
                    rows(retCount).Add("interval", Interval)
                    rows(retCount).Add("si", Data(callput).price(j, 0))
                    rows(retCount).Add("go", Data(callput).price(j, 1))
                    rows(retCount).Add("jue", Data(callput).price(j, 2))
                    rows(retCount).Add("jong", Data(callput).price(j, 3))
                    rows(retCount).Add("volume", Data(callput).거래량(j))

                    retCount += 1
                End If
            Next
        Next

        client.InsertRows(dateaset_id, table_id, rows)

        Dim str As String = iDate.ToString() & "5분데이터 - 해당 날짜 " & retCount.ToString() & " 개의 row가 등록"
        Console.WriteLine(str)
        Add_Log("일반", str)

        Return retCount
    End Function



    Public Function GetDateList() As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob

        tableName = MakeTableName(Form1.txt_TableName.Text)
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

        Return 0

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
    Public Function GetRawData(ByVal dateLimitText As String, ByVal tableNameStr As String) As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob
        tableName = MakeTableName(tableNameStr)
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

                        DBTotalRawDataList.Add(prevDate, list)
                        datelist.Add(prevDate)

                        list = Nothing
                        list = New List(Of BigQueryRow)
                    End If
                    dateCount += 1
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
        Dim callput_1 As Integer = -1

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

            If ctime <= 1535 And ctime > 0 Then  '과거 DB Data가 79번인덱스에 0이 들어오는 경우가 있어서 이걸 제외하기 위해 이 로직 추가함 'EBEST 데이터에는 ctime이 null인게 섞여 있어 0보다 큰거 확인 로직 추가

                Dim callput As Integer
                If iFlag = 1 Then
                    callput = 0
                ElseIf iFlag = 6 Then
                    callput = 1
                Else
                    Console.WriteLine(iDate + " 날에 iFlag가 0인게 섞여있음")
                    callput = 0
                End If

                If callput <> callput_1 Then   ' 콜이 쭉 들오오고 나서, 풋이 들어올 때 Data(0), Data(1) 번에 각각 시간을 미리 입력한다
                    callput_1 = callput
                    '시간입력
                    SetTimeDataForDataForDBData(Data, callput)
                    Data(callput).HangSaGa = hangsaga
                End If

                Dim iIndex As Integer = FindIndexFormTime(ctime.ToString()) '해당 시간이 몇번째 인덱스인지 찾아온다
                currentIndex = Math.Max(currentIndex, iIndex)
                timeIndex = Math.Max(timeIndex, currentIndex + 1)

                Data(callput).price(iIndex, 0) = si
                Data(callput).price(iIndex, 1) = go
                Data(callput).price(iIndex, 2) = jue
                Data(callput).price(iIndex, 3) = jong
                Data(callput).거래량(iIndex) = volume

                cnt += 1
            End If

        Next

        Return callput_1 + 1

    End Function

    Public Sub AutoSave()

        If Val(순매수리스트(currentIndex_순매수).sTime) >= 153000 Then

            '타이머를 끈다
            Form2.Timer1.Enabled = False
            Form2.btn_TimerStart.Text = "START"
            Form2.label_timerCounter.Text = "---"

            Dim tempTargetDate As Integer = TargetDate

            If tempTargetDate > 20000000 Then
                tempTargetDate = tempTargetDate Mod 20000000
            End If

            Dim rowCount As Integer

            If tempTargetDate > 0 Then rowCount = GetRowCount(tempTargetDate, Form2.txt_F2_TableName.Text)

            If rowCount = 0 Then '오늘 날짜에 등록된게 없으면 입력한다
                InsertTargetDateData_1분(tempTargetDate)
                Insert순매수이력데이터(tempTargetDate)

                Threading.Thread.Sleep(2000)
                XAQuery_EBEST_분봉데이터호출함수(0)

                Threading.Thread.Sleep(2000)
                XAQuery_EBEST_분봉데이터호출함수(1)

                '이 결과는 분봉데이터 수신하는 realtime_ebest 모듈에서 DB에 저장하는 함수를 호출하여 저장한다

            Else
                Add_Log("일반", tempTargetDate.ToString() & " 날에는 이미 순매수데이터가 등록되어 있습니다")
            End If
        End If

    End Sub

    '일일 1분 데이터를 빅쿼리에 저장한다 20220821 추가
    Public Function InsertTargetDateData_1분(ByVal iDate As Integer) As Integer

        Dim retCount As Integer = 0
        Dim i, callput As Integer
        Dim iFlag As Integer
        Dim client As BigQueryClient = BigQueryClient.Create(projectID)
        Dim 영보다큰갯수 As Integer = 0
        Dim dateaset_id = "option5"
        Dim table_id = "option_one_minute"   '1분데이터가 저장되는 테이블 이름

        iDate = iDate Mod 20000000

        For callput = 0 To 1
            For j = 0 To 399
                If 일분옵션데이터(callput).price(j, 0) > 0 Then
                    영보다큰갯수 += 1
                End If
            Next
        Next

        Dim rows(영보다큰갯수 - 1) As BigQueryInsertRow   '배열 갯수 주의해야 함. 1을 빼지 않으면 마지막 열이 nothing이 되어 아래 Insert에서 오류가 남

        For callput = 0 To 1

            Dim hangsaga As Integer = Val(일분옵션데이터(callput).HangSaGa)

            For j = 0 To 399

                If callput = 0 Then
                    iFlag = 1
                Else
                    iFlag = 6
                End If

                If 일분옵션데이터(callput).price(j, 0) > 0 Then   '영보다큰갯수와 동일한 로직으로 입력

                    rows(retCount) = New BigQueryInsertRow
                    rows(retCount).Add("cdate", iDate)
                    rows(retCount).Add("index", 0)
                    rows(retCount).Add("hangsaga", hangsaga)
                    rows(retCount).Add("iFlag", iFlag)
                    rows(retCount).Add("ctime", 일분옵션데이터(i).ctime(j))
                    rows(retCount).Add("interval", 1)
                    rows(retCount).Add("si", 일분옵션데이터(callput).price(j, 0))
                    rows(retCount).Add("go", 일분옵션데이터(callput).price(j, 1))
                    rows(retCount).Add("jue", 일분옵션데이터(callput).price(j, 2))
                    rows(retCount).Add("jong", 일분옵션데이터(callput).price(j, 3))
                    rows(retCount).Add("volume", 일분옵션데이터(callput).거래량(j))

                    retCount += 1
                End If
            Next
        Next


        client.InsertRows(dateaset_id, table_id, rows)

        Dim str As String = "1분 옵션데이터 저장 : " & iDate.ToString() & " 해당 날짜 " & retCount.ToString() & " 개의 row가 등록"
        Console.WriteLine(str)
        Add_Log("일반", str)

        Return retCount
    End Function

    '순매수이력데이터를 저장한다 - 장종료 후 809건이었음 20220821 추가
    Public Sub Insert순매수이력데이터(ByVal iDate As Integer)

        Dim client As BigQueryClient = BigQueryClient.Create(projectID)
        Dim dateaset_id = "option5"
        Dim table_id = "soonMeSuTable"   '순매수데이터가 저장되는 테이블 이름

        iDate = iDate Mod 20000000

        Dim rows(순매수리스트카운트 - 1) As BigQueryInsertRow   '배열 갯수 주의해야 함. 1을 빼지 않으면 마지막 열이 nothing이 되어 아래 Insert에서 오류가 남

        For j = 0 To 순매수리스트카운트 - 1
            rows(j) = New BigQueryInsertRow
            rows(j).Add("cdate", iDate)
            rows(j).Add("ctime", Val(순매수리스트(j).sTime))
            rows(j).Add("indamount", 순매수리스트(j).개인순매수)
            rows(j).Add("sysamount", 순매수리스트(j).기관순매수)
            rows(j).Add("foramount", 순매수리스트(j).외국인순매수)
            rows(j).Add("kigamount", 순매수리스트(j).연기금순매수)
            rows(j).Add("kospiIndex", 순매수리스트(j).코스피지수)
        Next

        client.InsertRows(dateaset_id, table_id, rows)

        Dim str As String = "주체별 순매수 이력 저장 : " & iDate.ToString() & " 해당 날짜 " & 순매수리스트카운트.ToString() & " 개의 row가 등록"
        Console.WriteLine(str)
        Add_Log("일반", str)

    End Sub


    Public Function GetRawData_1min(ByVal dateLimitText As String, ByVal tableNameStr As String) As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob
        tableName = MakeTableName(tableNameStr)
        Dim query As String = "select * from " + tableName + " " + dateLimitText + " order by cdate, iFlag, `index`, ctime  "
        Dim cnt As Integer = 0
        Dim dateCount As Integer = 0

        Dim prevDate As Integer = 0

        If DBTotalRawDataList_1min Is Nothing Then
            DBTotalRawDataList_1min = New Dictionary(Of Integer, List(Of BigQueryRow))
        Else
            DBTotalRawDataList_1min.Clear()
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

                        DBTotalRawDataList_1min.Add(prevDate, list)
                        datelist.Add(prevDate)

                        list = Nothing
                        list = New List(Of BigQueryRow)
                    End If
                    dateCount += 1
                    prevDate = tempDate

                End If


                '중간에는 Data()에 모아놓는다
                list.Add(row)
                cnt += 1
            Next

            If dateCount > 0 Then '마지막 날짜까지 가져온다

                DBTotalRawDataList_1min.Add(prevDate, list)
                datelist.Add(prevDate)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        DBDateList_1min = datelist.ToArray()
        Return dateCount

    End Function

    '1분데이터 인수로 받은 날의 데이터를 자료구조로 올린다 - -------------------- 1분
    Public Function GetDataFromDBHandler_1Min(ByVal iDate As Integer) As Integer
        Dim cnt As Integer = 0
        Dim callput_1 As Integer = -1

        Dim iList = DBTotalRawDataList_1min.Item(iDate)

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

            If ctime <= 1530 And ctime > 0 Then  '과거 DB Data가 79번인덱스에 0이 들어오는 경우가 있어서 이걸 제외하기 위해 이 로직 추가함

                Dim callput As Integer
                If iFlag = 1 Then
                    callput = 0
                ElseIf iFlag = 6 Then
                    callput = 1
                Else
                    Console.WriteLine(iDate + " 날에 iFlag가 0인게 섞여있음")
                    callput = 0
                End If

                If callput <> callput_1 Then   ' 콜이 쭉 들오오고 나서, 풋이 들어올 때 Data(0), Data(1) 번에 각각 시간을 미리 입력한다
                    callput_1 = callput
                    '시간입력
                    SetTimeDataForDataForDBData_1Min(일분옵션데이터, callput)
                    일분옵션데이터(callput).HangSaGa = hangsaga
                End If

                Dim iIndex As Integer = FindIndexFormTime_1Min(ctime.ToString()) '해당 시간이 몇번째 인덱스인지 찾아온다
                currentIndex_1MIn = Math.Max(currentIndex_1MIn, iIndex)
                timeIndex_1Min = Math.Max(timeIndex_1Min, currentIndex_1MIn + 1)

                일분옵션데이터(callput).price(iIndex, 0) = si
                일분옵션데이터(callput).price(iIndex, 1) = go
                일분옵션데이터(callput).price(iIndex, 2) = jue
                일분옵션데이터(callput).price(iIndex, 3) = jong
                일분옵션데이터(callput).거래량(iIndex) = volume

                cnt += 1
            End If

        Next

        Return callput_1 + 1

    End Function

    '1분데이터를 위해 별도로 함수를 만든다
    Public Sub SetTimeDataForDataForDBData_1Min(ByRef tempData() As 일분데이터템플릿, ByVal jongmokIndex As Integer)
        Dim si, bun As Integer
        Dim strTemp As String
        Dim totalTimeCount, num As Integer

        Dim interval_1min As Integer = 1
        totalTimeCount = 400

        For num = 1 To totalTimeCount

            si = Int(num / 60) + 9
            bun = Int(num Mod 60)

            si = si * 100 + bun
            strTemp = Str(si)
            tempData(jongmokIndex).ctime(num - 1) = strTemp '그리드 왼쪽 기준 시간의 값을 입력한다. 이건 나중에 SearchLine에서 쓰인다
        Next

    End Sub

    Public Function GetRawData_순매수(ByVal dateLimitText As String, ByVal tableNameStr As String) As Integer

        Dim client As BigQueryClient
        Dim job As BigQueryJob
        tableName = MakeTableName(tableNameStr)
        Dim query As String = "select * from " + tableName + " " + dateLimitText + " order by cdate,  ctime  "
        Dim cnt As Integer = 0
        Dim dateCount As Integer = 0

        Dim prevDate As Integer = 0

        If DBTotalRawDataList_순매수 Is Nothing Then
            DBTotalRawDataList_순매수 = New Dictionary(Of Integer, List(Of BigQueryRow))
        Else
            DBTotalRawDataList_순매수.Clear()
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

                        DBTotalRawDataList_순매수.Add(prevDate, list)
                        datelist.Add(prevDate)
                        list = Nothing
                        list = New List(Of BigQueryRow)
                    End If
                    dateCount += 1
                    prevDate = tempDate

                End If

                '중간에는 Data()에 모아놓는다
                list.Add(row)
                cnt += 1
            Next

            If dateCount > 0 Then '마지막 날짜까지 가져온다

                DBTotalRawDataList_순매수.Add(prevDate, list)
                datelist.Add(prevDate)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Return dateCount

    End Function

    '순매수 데이터를 순매수구조체로 올리는 함수 - 인수로 받은 날의 데이터를 자료구조로 올린다 
    Public Function Get순매수데이터(ByVal iDate As Integer) As Integer

        Dim iList = DBTotalRawDataList_순매수.Item(iDate)
        Dim iIndex As Integer = 0
        For Each row In iList

            '값을 읽어온다
            Dim tempDate As Integer = Val(row("cdate"))
            Dim ctime As Integer = Val(row("ctime"))
            Dim 개인순매수 As Single = Val(row("indamount"))
            Dim 기관순매수 As Single = Val(row("sysamount"))
            Dim 외국인순매수 As Single = Val(row("foramount"))
            Dim 연기금순매수 As Single = Val(row("kigamount"))
            Dim 코스피지수 As Single = Val(row("kospiindex"))

            순매수리스트(iIndex).sDate = tempDate
            순매수리스트(iIndex).sTime = ctime
            순매수리스트(iIndex).개인순매수 = 개인순매수
            순매수리스트(iIndex).기관순매수 = 기관순매수
            순매수리스트(iIndex).외국인순매수 = 외국인순매수
            순매수리스트(iIndex).연기금순매수 = 연기금순매수
            순매수리스트(iIndex).코스피지수 = 코스피지수
            순매수리스트(iIndex).외국인_연기금_순매수 = 외국인순매수 + 연기금순매수
            순매수리스트(iIndex).외국인_기관_순매수 = 외국인순매수 + 기관순매수
            iIndex += 1

        Next

        timeIndex_순매수 = iIndex
        currentIndex_순매수 = timeIndex_순매수 - 1
        Return iIndex

    End Function

    '시뮬레이션이나 실시간으로 발생한 신호의 결과를 빅쿼리에 저장한다
    Public Sub InsertSoonMeSuShinhoResult()
        Dim client As BigQueryClient = BigQueryClient.Create(projectID)
        Dim dateaset_id = "option5"
        Dim table_id = "Option_SoonMeSuShinhoResultTable"

        Dim totalCount1 As Integer = SoonMesuSimulationTotalShinhoList.Count - 1

        Dim rows(totalCount1) As BigQueryInsertRow

        For i As Integer = 0 To totalCount1

            Dim s As 순매수신호_탬플릿 = SoonMesuSimulationTotalShinhoList(i)

            rows(i) = New BigQueryInsertRow
            rows(i).Add("A00_ShinhoCount", s.A00_신호차수)
            rows(i).Add("A01_OccurIndex", s.A01_발생Index)
            rows(i).Add("A02_OccurTime", s.A02_발생시간)
            rows(i).Add("A03_ShinhoID", s.A03_신호ID)
            rows(i).Add("A04_OccuredSoonMesu", s.A04_신호발생순매수)
            rows(i).Add("A05_DestroySoonMesu", s.A05_신호해제순매수)
            rows(i).Add("A06_OccuredKOSPI", s.A06_신호발생종합주가지수)
            rows(i).Add("A07_DestroyKOSPI", s.A07_신호해제종합주가지수)
            rows(i).Add("A08_Callput", s.A08_콜풋)
            rows(i).Add("A09_Hangsaga", s.A09_행사가)
            rows(i).Add("A10_Price", s.A10_신호발생가격)
            rows(i).Add("A11_OrderNumber", s.A11_주문번호)
            rows(i).Add("A12_OptionCode", s.A12_종목코드)
            rows(i).Add("A13_BuyingStatus", s.A13_체결상태)
            rows(i).Add("A14_CurrentPrice", s.A14_현재가격)
            rows(i).Add("A15_CurrentStatus", s.A15_현재상태)
            rows(i).Add("A16_ProfitRatio", s.A16_이익률)

            rows(i).Add("A17_MidSellFlag", s.A17_중간매도Flag)
            rows(i).Add("A18_SellTime", s.A18_매도시간)
            rows(i).Add("A19_SellIndex", s.A19_매도Index)
            rows(i).Add("A20_SellReason", s.A20_매도사유)
            rows(i).Add("A21_LastProfitRatio", s.A21_환산이익율)

            rows(i).Add("A50_ConditionAll", s.A50_조건전체)
            rows(i).Add("A51_SoonMesuItems", s.A51_순매수기준)
            rows(i).Add("A52_Slope", s.A52_기울기)
            rows(i).Add("A53_prevPointMargin", s.A53_선행포인트수_마진)
            rows(i).Add("A54_IsReal", s.A54_IsReal)
            rows(i).Add("A55_Memo", s.A55_메모)
            rows(i).Add("A56_TargetPrice", s.A56_기준가격)
            rows(i).Add("A57_Month", s.A57_월물)
            rows(i).Add("A58_Date", s.A58_날짜)
            rows(i).Add("A59_RemainDate", s.A59_남은날짜)
            rows(i).Add("A60_SonjulValue", s.A60_손절기준차)
            rows(i).Add("A61_IkjulValue", s.A61_익절기준차)
            rows(i).Add("A62_TimeoutTime", s.A62_TimeoutTime)
            rows(i).Add("B00_etc", s.B00_etc)

        Next

        client.InsertRows(dateaset_id, table_id, rows)

        Dim str As String = " F2 신호결과 저장완료 " & SoonMesuSimulationTotalShinhoList.Count.ToString() & " 개의 row가 등록"
        Console.WriteLine(str)
        Add_Log("일반", str)

    End Sub
End Module
