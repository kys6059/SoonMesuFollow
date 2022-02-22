Option Explicit On

Structure DataSet

    Dim HangSaGa As String '콜풋
    Dim Code() As String '콜풋
    Dim ctime() As String  '시간 100개 콜풋 구분 없음

    Dim price(,,) As Single '콜풋, 시간, 시고저종

    Dim Big(,) As Single '콜풋 시고저종
    Dim Small(,) As Single '콜풋 시고저종
    Dim 거래량(,) As Long '콜풋 100개
    Dim secondMin() As Single '콜풋

    Dim 증거금() As Long

    Public Sub Initialize()

        ReDim Code(1)
        ReDim 증거금(1)
        ReDim ctime(100) '
        ReDim price(1, 100, 3)  '콜풋, 시간, 시고저종
        ReDim Big(1, 3) '시고저종을 기록해야해서 4개
        ReDim Small(1, 3) '시고저종을 기록해야해서 4개
        ReDim 거래량(1, 100) '시간대별 거래량을 기록해야 해서 100개가 필요함
        ReDim secondMin(1) '시고저종 관계없이 저가에서만 제2저가를 구함

    End Sub

End Structure


'해야할 일 정리 ------------------- 20220211

'그래프 그리기
'설정 - 입력 항목들 만들기  ------------------------------- 완료
'실시간 타이머 넣기 --------------------------------------- 완료 (화면 그리는데 시간이 많이 걸려서 좀 느림)
'DB에 저장, 불러오기
'신호 넣기
'특정 날짜 Data 가져오기 기능 추가 ------------------------ 완료 (월물이 바뀌는 날은 안됨, CYBOS에서 data가 지난달거는 조회 안됨)
'일정 시간 후 자동 저장 기능 ---- Timer에 구현함



Module Module_common

    '전역변수 선언
    Public Data() As DataSet  '-------------------- 전체가 들어있는 자료형
    Public TargetDate As Integer
    Public sMonth As String
    Public Interval As Integer
    Public TotalJongMokCount As Integer
    Public CurrentTime As Integer
    Public isRealFlag As Boolean
    Public TotalCount As Integer
    Public UpperLimit, LowerLimt As Single
    Public timeIndex As Integer '시간이 내려감에따라 증가하는 인덱스  - 항상 초기화 필요(DB에서 가져올 때, 대신에서 가져올때)
    Public currentIndex As Integer '시뮬레이션할 때 현재커서 위치를 나타냄
    Public selectedJongmokIndex(1) As Integer
    Public JongmokTargetPrice As Single  '기준이 되는 targetprice - default 2.0
    Public timerCount As Integer
    Public timerMaxInterval As Integer





    Public Sub InitDataStructure()

        ReDim Data(100) '최대 100개지만 실제로는 28개까지만 지원한다 - 콜풋 각각
        For i As Integer = 0 To 100
            Data(i).Initialize()
        Next

        TargetDate = 0
        sMonth = "0"
        Interval = Val(Form1.txt_Interval.Text)
        TotalJongMokCount = 0
        UpperLimit = Val(Form1.txt_UpperLimit.Text)
        LowerLimt = Val(Form1.txt_LowerLimit.Text)
        timeIndex = 0
        timerCount = 0 '16초마다 돌아가는 타이머 주기
        timerMaxInterval = 16 '16초보다 크면 실행함

        JongmokTargetPrice = 2.0
        selectedJongmokIndex(0) = 0
        selectedJongmokIndex(1) = 0

    End Sub


    Public Function CalcTargetJonhmokIndex(ByVal callput As Integer) As Integer

        Dim temptarget, i, tempIndex As Integer
        Dim mingap, gap As Single

        mingap = 1000.0
        tempIndex = GetMaxIndex() '장이 끝나면 마지막에 0만 들어있는 값이 와서 그 앞에 걸 기준으로 바꾼다

        For i = 0 To TotalCount

            If Data(i).price(callput, tempIndex, 3) > 0 Then
                gap = Math.Abs(Data(i).price(callput, tempIndex, 3) - JongmokTargetPrice)
                If gap < mingap Then
                    mingap = gap
                    temptarget = i
                End If
            End If

        Next

        Return temptarget

    End Function

    '일단 최고만 구현됨
    Public Sub CalcColorData()
        Dim i, j, k, callput As Integer
        Dim min, max, secondmin As Single

        For callput = 0 To 1 '콜 풋 루프

            For i = 0 To TotalCount - 1 '종목루프

                secondmin = 1000

                For j = 0 To 3 ' 시고저종 루프

                    min = 1000
                    max = 0

                    For k = 0 To currentIndex - 1 '시간 루프

                        '최고값 계산
                        If Data(i).price(callput, k, j) > max And Data(i).price(callput, k, j) > 0 Then
                            max = Data(i).price(callput, k, j)
                        End If

                        '최저값 계산
                        If Data(i).price(callput, k, j) < min And Data(i).price(callput, k, j) > 0 Then
                            min = Data(i).price(callput, k, j)
                        End If

                        '제2저가 계산
                        If j = 2 Then
                            If Data(i).price(callput, k, j) < secondmin And Data(i).price(callput, k, j) > min Then
                                secondmin = Data(i).price(callput, k, j)
                            End If
                        End If

                    Next

                    '최고,최저값 입력
                    Data(i).Big(callput, j) = max
                    Data(i).Small(callput, j) = min
                    Data(i).secondMin(callput) = secondmin

                Next

            Next

        Next

    End Sub

    '칼러를 반환하는 함수
    '칼라 0번은 빨강색
    '칼라 1번은 파란색 Chk_Display_Blue
    '칼라 2번은 까만색
    '칼라 3번은 하늘색 - 제2저가
    '칼라 4번은 그냥 흰색
    '칼라 5번은 오렌지색
    '칼라 6번은 분홍색
    '칼라 7번은 녹색 Chk_Display_Green
    Public Function ItsColor(ByVal jongMok As Integer, ByVal iFlag As Integer, ByVal iIndex As Integer, ByVal sigojuejong As Integer) As Integer

        Dim SkyBlueValue As Single
        Dim color As Integer

        color = 4 '아래 조건에 아무데도 안걸리면 흰색

        If (Data(jongMok).price(iFlag, iIndex, sigojuejong) > 0) Then

            '제2저가 - 하늘색
            If (Data(jongMok).price(iFlag, iIndex, sigojuejong) > (Data(jongMok).secondMin(iFlag) + 0.005)) Then
                color = 3
            End If

            '최고가
            If (Math.Abs(Data(jongMok).price(iFlag, iIndex, sigojuejong) - Data(jongMok).Big(iFlag, sigojuejong)) < 0.005) Then
                color = 0
            End If

            '최저가
            If (Math.Abs(Data(jongMok).price(iFlag, iIndex, sigojuejong) - Data(jongMok).Small(iFlag, sigojuejong)) < 0.005) Then
                color = 1
            End If

        End If


        Return color

    End Function

    '마지막에 currentIndex에서는 0 값이 들어와서 마지막 조건일 때는 그 앞에걸로 하는 걸로 바꾼다
    Public Function GetMaxIndex() As Integer

        Dim ret As Integer

        If currentIndex > 78 Then
            ret = 78
        Else
            ret = currentIndex
        End If

        Return ret

    End Function

    Public Sub UIVisible(ByVal flag As Boolean)

        If flag = True Then

            Form1.grd_selected.Visible = True
            Form1.grid1.Visible = True
            Form1.Chart1.Visible = True

        Else
            Form1.grd_selected.Visible = False
            Form1.grid1.Visible = False
            Form1.Chart1.Visible = False

        End If

    End Sub


End Module
