Option Explicit On
Imports System.Security.Policy
Imports Google.Api.Gax

Module Algorithm_SoonMeSu
    Structure 순매수신호_탬플릿

        Dim A00_신호차수 As Integer
        Dim A01_발생Index As Integer
        Dim A02_발생시간 As String
        Dim A03_신호ID As String
        Dim A04_신호발생순매수 As Integer
        Dim A05_신호해제순매수 As Integer
        Dim A06_신호발생종합주가지수 As Single
        Dim A07_신호해제종합주가지수 As Single
        Dim A08_콜풋 As Integer
        Dim A09_행사가 As String
        Dim A10_신호발생가격 As Single
        Dim A11_주문번호 As String
        Dim A12_종목코드 As String
        Dim A13_체결상태 As Integer
        Dim A14_현재가격 As Single
        Dim A15_현재상태 As String
        Dim A16_이익률 As Single

        Dim A17_중간매도Flag As Integer
        Dim A18_매도시간 As String
        Dim A19_매도Index As Integer
        Dim A20_매도사유 As String  '익절, 손절, TimeOver 구분
        Dim A21_환산이익율 As Single '매수 관점에서 이익률
        Dim A22_신호해제가격 As Single '옵션 환매(매수)가격


        Dim A50_조건전체 As String
        Dim A51_순매수기준 As String '0.외국인_기관, 1.외국인, .외국인_연기금
        Dim A52_기울기 As Single
        Dim A53_장대양봉손절가 As Single  '이걸 안쓰니까 장대양봉손절가로 사용함
        Dim A54_IsReal As Integer
        Dim A55_메모 As String
        Dim A56_기준가격 As String '매수할 때 기준가격
        Dim A57_월물 As String
        Dim A58_날짜 As String
        Dim A59_남은날짜 As Integer
        Dim A60_손절기준차 As String
        Dim A61_익절기준차 As String
        Dim A62_TimeoutTime As String '장 중 매도할 최종 시간

        Dim B00_etc As String

    End Structure

    Public SoonMesuShinhoList As List(Of 순매수신호_탬플릿)
    Public SoonMesuSimulationTotalShinhoList As List(Of 순매수신호_탬플릿)
    Public SoonMesuSimulation_조건 As String

    Public 이전순매수방향 As String = "중립"
    Public 신규합계_마지막신호 As String = "중립"

    Public variable_1 As String

    'C알고리즘 시작 - 시작하자마자 순매수가 몰리면 바로 사는 것

    Public C_StartTime As Integer = 90200
    Public C_EndTime As Integer = 90700
    Public C_개별금액 As Integer = 300
    Public C_합계금액 As Integer = 700
    Public C_해제기울기 As Integer = 20
    'C알고리즘 끝


    'D 알고리즘용 시작
    Public 이동평균선_기준일자 As Integer = 50       '이동평균선 갯수 기준

    Public X_계산기준봉비율 As Single = 0.55         '장대양봉의 크기를 계산하는 기준으로 X / 이동평균선_기준일자 비율을 의미함
    Public Y_장대양봉기준비율 As Single = 0.6      'X_계산기준봉비율내의 캔들들의 최대최소값의 차에 비해 어느정도인지에 대한 비율

    Public 손절기준차 As String = "-0.3"
    Public 익절기준차 As String = "1.0"
    Public 마감시간 As String = "1515"

    Public 장대양봉손절기준비율 As Single = 2.5    '장대양봉의 크기를 1로 두고 장대양봉 위에서부터 몇%에서 손절할지 결정함  - 추가
    Public 이평선하향돌파익절기준 As Single = 0.5   '이평선위에서 아래로 하향돌파 시 익절 기준으로 적어도 이익이 이 기준 이상일 때 매도함
    'D 알고리즘용 끝

    Public Sub CalcAlgorithmAll()

        모든살아있는신호확인하기()

        If Form2.chk_Algorithm_A.Checked = True Then CalcAlgorithm_A()
        If Form2.chk_Algorithm_B.Checked = True Then CalcAlgorithm_B()
        If Form2.chk_Algorithm_C.Checked = True Then CalcAlgorithm_C()
        If Form2.chk_Algorithm_D.Checked = True Then CalcAlgorithm_D()

    End Sub

    Public Sub CalcAlgorithm_A()  '10시20분부터 11시30분까지 순매수 알고리즘

        Dim startTime As Integer = Val(Form2.txt_F2_매수시작시간.Text)
        Dim endTime As Integer = Val(Form2.txt_F2_매수마감시간.Text)
        Dim timeoutTime As Integer = Val(Form2.txt_F2_TimeoutTime.Text)



        Dim ret As String = CalcAlgorithm_AB()  '상승이던 하강이던 기준점수(현재 3점)을 넘으면 신호를 만든다

        If Val(순매수리스트(currentIndex_순매수).sTime) >= startTime And Val(순매수리스트(currentIndex_순매수).sTime) <= endTime Then

            If Val(순매수리스트(currentIndex_순매수).sTime) < timeoutTime + 500 Then

                '이전순매수와 지금인덱스를 비교하여 5보다 작으면(2분이하) 무시하는 로직 추가
                Dim 마지막순매수index As Integer = Get마지막순매수Index()
                If ret <> "중립" And 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then '중립 --> 상승 or 하강이 뜨거나 반대 방향 신호가 뜨면 여기를 진입한다

                    If ret = "상승" Then ' 중립에서 상승으로 전환 

                        반대방향신호죽이는함수("A", 1, "change")                                 '이전 신호확인해서 죽이기

                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A", 0)               '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("A 상승신호 발생 AT {0}", shinho.A02_발생시간))

                    ElseIf ret = "하락" Then '중립에서 하락으로 전환

                        반대방향신호죽이는함수("A", 0, "change")                                   '이전 신호확인해서 죽이기

                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A", 1)             '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("A 하락신호 발생 AT {0}", shinho.A02_발생시간))
                    End If

                    이전순매수방향 = ret '신호가 뜬걸 의미한다 이걸 바꿔 놓는다
                End If
            End If
        End If


    End Sub

    Public B_StartIndex As Integer = 8
    Public B_EndIndex As Integer = 20
    Public B_기준기울기 As Single = 20.0
    Public B_해제기울기 As Single = 10.0
    Public b_최소유지INDEX As Integer = 6

    Public Sub CalcAlgorithm_B() '좀더 짧은 시간에 더 급격한 커블 때 매수하는 로직으로 변경함. 외국인, 기관 모두 20230403

        Dim startTime As Integer = Val(Form2.txt_F2_매수시작시간.Text)

        If currentIndex_순매수 >= B_StartIndex And currentIndex_순매수 <= B_EndIndex Then  '최초 매우 강할 때 사서 낮아지면 바로 판다

            If SoonMesuShinhoList.Count <= 0 Then  '신호가 현재까지 한번도 안떳을 때만 수행한다

                Dim 최저기울기 As Single = Math.Min(Math.Abs(PIP_Point_Lists(1).마지막선기울기), Math.Abs(PIP_Point_Lists(2).마지막선기울기))
                Dim 기울기방향성 As Single = PIP_Point_Lists(1).마지막선기울기 * PIP_Point_Lists(2).마지막선기울기

                If 기울기방향성 > 0 Then   '외국인, 기관이 둘 다 같은 방향이고
                    If 최저기울기 > B_기준기울기 Then  '기준기울기보다 큰 경우
                        If PIP_Point_Lists(1).마지막선기울기 > 0 And PIP_Point_Lists(2).마지막선기울기 > 0 Then '기울기가 둘 다 양수이면
                            Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("B", 0)               '신규 신호 입력하기
                            SoonMesuShinhoList.Add(shinho)
                            If EBESTisConntected = True Then Add_Log("B 신호", String.Format("최초 상승신호 발생 AT {0}", shinho.A02_발생시간))

                        ElseIf PIP_Point_Lists(1).마지막선기울기 < 0 And PIP_Point_Lists(2).마지막선기울기 < 0 Then  '기울기가 둘 다 음수이면
                            Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("B", 1)             '신규 신호 입력하기
                            SoonMesuShinhoList.Add(shinho)
                            If EBESTisConntected = True Then Add_Log("B 신호", String.Format("최초 하락신호 발생 AT {0}", shinho.A02_발생시간))
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Public Sub CalcAlgorithm_C() '좀더 짧은 시간에 더 급격한 커블 때 매수하는 로직으로 변경함. 외국인, 기관 모두 20230403

        Dim currentTime As Integer = Val(순매수리스트(currentIndex_순매수).sTime)

        If currentTime >= C_StartTime And currentTime <= C_EndTime Then  '변수 2건

            If SoonMesuShinhoList.Count <= 0 Then  '신호가 현재까지 한번도 안떳을 때만 수행한다

                If 순매수리스트(currentIndex_순매수).외국인순매수 * 순매수리스트(currentIndex_순매수).기관순매수 > 0 Then   '둘 다 방향이 같고

                    If Math.Abs(순매수리스트(currentIndex_순매수).외국인순매수) > C_개별금액 And Math.Abs(순매수리스트(currentIndex_순매수).기관순매수) > C_개별금액 Then  '개별 항목들이 기준 금액을 넘었고

                        If Math.Abs(순매수리스트(currentIndex_순매수).외국인_기관_순매수) > C_합계금액 Then '합계를 초과하면 신호 발생

                            If 순매수리스트(currentIndex_순매수).외국인순매수 > 0 Then

                                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("C", 0)               '신규 신호 입력하기
                                SoonMesuShinhoList.Add(shinho)
                                If EBESTisConntected = True Then Add_Log("B 신호", String.Format("C - 최초 상승신호 발생 AT {0}", shinho.A02_발생시간))

                            Else

                                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("C", 1)             '신규 신호 입력하기
                                SoonMesuShinhoList.Add(shinho)
                                If EBESTisConntected = True Then Add_Log("B 신호", String.Format("C - 최초 하락신호 발생 AT {0}", shinho.A02_발생시간))

                            End If

                        End If

                    End If

                End If

            End If

        End If

    End Sub


    Public Function CalcAlgorithm_AB() As String

        Dim ret As String = "중립"

        Dim CurrentIndex_순매수_마지막방향 As String = 신규합계_마지막신호

        If 이전순매수방향 <> CurrentIndex_순매수_마지막방향 Then

            If 이전순매수방향 = "중립" Then
                If CurrentIndex_순매수_마지막방향 = "상승" Then
                    ret = "상승" '상승 베팅
                ElseIf CurrentIndex_순매수_마지막방향 = "하락" Then
                    ret = "하락" '하락 베팅
                End If
            ElseIf 이전순매수방향 = "상승" Then
                If CurrentIndex_순매수_마지막방향 = "중립" Then
                    '유지
                ElseIf CurrentIndex_순매수_마지막방향 = "하락" Then
                    ret = "하락" '하락 베팅
                End If
            ElseIf 이전순매수방향 = "하락" Then
                If CurrentIndex_순매수_마지막방향 = "중립" Then
                    '유지
                ElseIf CurrentIndex_순매수_마지막방향 = "상승" Then
                    ret = "상승" '상승 베팅
                End If
            End If
        End If

        Return ret

    End Function

    Public Sub CalcAlgorithm_D() '이동평균선 알고리즘


        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        If 일분옵션데이터_CurrentIndex < 이동평균선_기준일자 Then Return
        If Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) < 1020 Or Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) > 1445 Then Return

        Dim Index As Integer = 일분옵션데이터_CurrentIndex - 1

        For i As Integer = 0 To 1
            If is동일신호가현재살아있나("D", i) Then Return

            If 일분옵션데이터(i).price(Index, 3) < 0.2 Then Return '0.2보다 작으면 신호를 만들지 않는다

            Dim is장대양봉 As Integer = CALC_IS_장대양봉(i, Index)

            Dim flag As Boolean = False


            Select Case is장대양봉
                Case 1
                    If 일분옵션데이터(i).price(Index, 0) < 일분옵션데이터(i).이동평균선(Index) And 일분옵션데이터(i).price(Index, 3) > 일분옵션데이터(i).이동평균선(Index) Then flag = True
                Case 2
                    'If 일분옵션데이터(i).price(Index - 1, 0) < 일분옵션데이터(i).이동평균선(Index) And 일분옵션데이터(i).price(Index, 3) > 일분옵션데이터(i).이동평균선(Index) Then flag = True
                    'Add_Log("신호", "2개를 더한 양봉 신호 발생")
                Case Else

            End Select
            If flag = True Then                'D신호 발생

                Dim str As String = String.Format("D 신호 발생 콜풋 : {0}, 인덱스 : {1}", i, Index)
                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("D", i)
                SoonMesuShinhoList.Add(shinho)

            End If

        Next

    End Sub

    Private Function is동일신호가현재살아있나(ByVal 신호id As String, ByVal callput As Integer) As Boolean

        If SoonMesuShinhoList IsNot Nothing Then
            For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A03_신호ID = 신호id And s.A15_현재상태 = 1 And s.A08_콜풋 = callput Then Return 1
            Next
        End If

        Return False
    End Function

    Private Function CALC_IS_장대양봉(ByVal callput As Integer, ByVal index As Integer) As Integer

        Dim 기준캔들수 As Integer = Math.Round(이동평균선_기준일자 * X_계산기준봉비율)
        Dim max As Single = Single.MinValue
        Dim min As Single = Single.MaxValue

        For i As Integer = 0 To 기준캔들수 - 1
            max = Math.Max(일분옵션데이터(callput).price(index - i, 3), max)
            min = Math.Min(일분옵션데이터(callput).price(index - i, 3), min)
        Next

        Dim 기준캔들내최대크기 As Single = max - min          '최대-최소값을 구한다

        Dim 현재분봉크기 As Single = 일분옵션데이터(callput).price(index, 3) - 일분옵션데이터(callput).price(index, 0)  '종가 - 시가 : 이러면 양봉인지 확인됨
        Dim 이차분봉크기 As Single = 일분옵션데이터(callput).price(index, 3) - 일분옵션데이터(callput).price(index - 1, 0)  '한틱전의 값과 더해서 양봉이 길어진다면  ------------------ 너무 많이 뜨고 결과가 안좋아 일단 폐기 20230404

        If 기준캔들내최대크기 * Y_장대양봉기준비율 < 현재분봉크기 Then
            Return 1 '현재분봉이 기준보다 크면 장대양봉이라고 판단함
            'ElseIf 기준캔들내최대크기 * Y_장대양봉기준비율 < 이차분봉크기 Then
            '   Return 2  '2개의 캔들이 더해져서 장대양봉인걸 알려준다

        End If

        Return 0
    End Function



    Private Function MakeSoonMesuShinho(ByVal 신호ID As String, ByVal callput As Integer) As 순매수신호_탬플릿

        Dim shinho As 순매수신호_탬플릿 = New 순매수신호_탬플릿

        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        shinho.A00_신호차수 = SoonMesuShinhoList.Count + 1
        shinho.A01_발생Index = currentIndex_순매수
        shinho.A02_발생시간 = 순매수리스트(currentIndex_순매수).sTime
        shinho.A03_신호ID = 신호ID

        shinho.A04_신호발생순매수 = Get순매수(currentIndex_순매수, 0)
        shinho.A05_신호해제순매수 = 0
        shinho.A06_신호발생종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수
        shinho.A07_신호해제종합주가지수 = 0
        shinho.A08_콜풋 = callput
        shinho.A09_행사가 = 일분옵션데이터(callput).HangSaGa
        shinho.A10_신호발생가격 = 0 '------------------------------------------------ 이거

        shinho.A15_현재상태 = 1
        shinho.A16_이익률 = 0

        shinho.A21_환산이익율 = 0

        shinho.A50_조건전체 = SoonMesuSimulation_조건
        shinho.A51_순매수기준 = Form2.cmb_F2_순매수기준.Text
        shinho.A52_기울기 = Form2.txt_F2_1차상승판정기울기기준.Text

        shinho.A54_IsReal = 0
        shinho.A56_기준가격 = Form2.txt_F2_매수_기준가.Text
        shinho.A57_월물 = sMonth
        shinho.A58_날짜 = 순매수리스트(currentIndex_순매수).sDate
        shinho.A59_남은날짜 = getRemainDate(sMonth, Val(shinho.A58_날짜))

        If shinho.A03_신호ID = "D" Then
            shinho.A60_손절기준차 = 손절기준차
            shinho.A61_익절기준차 = 익절기준차
            Dim 장대양봉크기 As Single = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3) - 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 0)
            shinho.A53_장대양봉손절가 = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3) - (장대양봉크기 * 장대양봉손절기준비율)
        Else
            shinho.A60_손절기준차 = Form2.txt_F2_손절매차.Text
            shinho.A61_익절기준차 = Form2.txt_F2_익절차.Text
        End If

        shinho.A62_TimeoutTime = Form2.txt_F2_TimeoutTime.Text

        If 일분옵션데이터_CurrentIndex >= 0 Then

            If shinho.A03_신호ID = "D" Then
                shinho.A10_신호발생가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0)
                shinho.A14_현재가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0)
            Else
                shinho.A10_신호발생가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                shinho.A14_현재가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
            End If

            shinho.A16_이익률 = Math.Round((shinho.A14_현재가격 - shinho.A10_신호발생가격) / shinho.A10_신호발생가격, 3)
        End If
        shinho.B00_etc = Form2.txt_F2_실험조건.Text

        Return shinho

    End Function

    Private Sub 반대방향신호죽이는함수(ByVal 신호ID As String, ByVal callput As Integer, ByVal 매도사유 As String)

        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A03_신호ID = 신호ID And s.A08_콜풋 = callput And s.A15_현재상태 = 1 Then  '죽어야하는 신호이고 현재상태가 살아있는 상태라면

                    s.A05_신호해제순매수 = Get순매수(currentIndex_순매수, 0)
                    s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수
                    s.A15_현재상태 = 0

                    s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
                    s.A19_매도Index = currentIndex_순매수
                    s.A20_매도사유 = 매도사유

                    Dim 일분옵션데이터_CurrentIndex As Integer = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
                    If 일분옵션데이터_CurrentIndex >= 0 Then s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                    s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                    s.A22_신호해제가격 = s.A14_현재가격

                    If s.A17_중간매도Flag = 1 Then '중간청산을 했으면 환산이익율을 조정한다
                        Dim 중간청산목표이익율 As Single = Val(Form2.txt_F2_중간청산비율.Text)
                        s.A21_환산이익율 = Math.Round((s.A21_환산이익율 + 중간청산목표이익율) / 2, 3)
                    End If

                    If callput = 0 Then s.A55_메모 = Math.Round(s.A07_신호해제종합주가지수 - s.A06_신호발생종합주가지수, 2)
                    If callput = 1 Then s.A55_메모 = Math.Round(s.A06_신호발생종합주가지수 - s.A07_신호해제종합주가지수, 2)

                    SoonMesuShinhoList(i) = s

                    '이전순매수방향 = "중립"   '이렇게 해 놓으면 기존에 떳다가 해제된 신호 방향으로 다시 신호가 발생된다
                End If
            Next
        End If
    End Sub

    Public Function 현재살아있는신호의종목리턴하는함수(ByVal callput As Integer) As String

        Dim str As String = ""
        If SoonMesuShinhoList IsNot Nothing Then
            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A15_현재상태 = 1 And s.A08_콜풋 = callput Then
                    str = Left(s.A09_행사가, 3)
                End If
            Next
        End If
        Return str
    End Function

    Private Sub 모든살아있는신호확인하기()
        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)

                If s.A15_현재상태 = 1 Then

                    Select Case s.A03_신호ID

                        Case "A"
                            살아있는신호확인하기_A(s)
                        Case "B"
                            살아있는신호확인하기_A(s)
                        Case "C"
                            살아있는신호확인하기_A(s)
                        Case "D"
                            살아있는신호확인하기_D(s)

                    End Select



                End If

                SoonMesuShinhoList(i) = s

            Next
        End If

    End Sub

    Private Sub 살아있는신호확인하기_D(ByRef s As 순매수신호_탬플릿)

        Dim 매도사유 As String = ""
        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        If s.A15_현재상태 = 1 Then

            s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)

            If s.A14_현재가격 > 0 Then
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
            End If


            '옵션가격 기준 손절매, 익절
            Dim 옵션가손절매기준 As Single = Val(손절기준차)
            Dim 옵션익절기준 As Single = Val(익절기준차)
            If s.A21_환산이익율 < 옵션가손절매기준 Then

                매도사유 = "option_son"

                If isRealFlag = False Then
                    s.A14_현재가격 = Math.Round(s.A10_신호발생가격 + (s.A10_신호발생가격 * 옵션가손절매기준), 2)
                    s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                End If

            End If
            If s.A21_환산이익율 > 옵션익절기준 Then
                매도사유 = "ik"

                If isRealFlag = False Then
                    s.A14_현재가격 = Math.Round(s.A10_신호발생가격 + (s.A10_신호발생가격 * 옵션익절기준), 2)
                    s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                End If
            End If

            '타임아웃
            If Val(순매수리스트(currentIndex_순매수).sTime) >= Val(s.A62_TimeoutTime) Then
                매도사유 = "timeout"
                If isRealFlag = False Then
                    s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0)
                    s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                End If
            End If

            '양봉크기비례 손절  - 장대양봉의 하단*비율을 하향 돌파하면 손절함  -- 없는게 결과가 더 좋아서 잠정 보류함 20230402
            If 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 2) < s.A53_장대양봉손절가 Then
                's.A14_현재가격 = s.A53_장대양봉손절가 - 0.01
                's.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                's.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                '매도사유 = "son"
            End If

            '이평선 하양돌파 익절  
            Dim 현재이평선 As Single = 일분옵션데이터(s.A08_콜풋).이동평균선(일분옵션데이터_CurrentIndex)
            Dim 현재이평선기준이익률 As Single = (현재이평선 - s.A10_신호발생가격) / s.A10_신호발생가격
            If 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 2) < 현재이평선 And 현재이평선기준이익률 > 이평선하향돌파익절기준 Then
                s.A14_현재가격 = 현재이평선 - 0.01
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                매도사유 = "ik_ip"
            End If

            '청산할 때 하는 프로세스
            If 매도사유 <> "" Then

                s.A15_현재상태 = 0
                s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
                s.A19_매도Index = currentIndex_순매수
                s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수
                s.A20_매도사유 = 매도사유
                s.A22_신호해제가격 = s.A14_현재가격
                If EBESTisConntected = True Then Add_Log("신호", String.Format("해제신호 발생 AT {0}, 사유 = {1}", s.A18_매도시간, 매도사유))

            Else  '살아 있으면 중간청산 체크하기
                'Dim 중간청산목표이익율 As Single = Val(Form2.txt_F2_중간청산비율.Text)
                'Dim 중간청산할지설정FLAG As Boolean = Form2.chk_중간청산.Checked
                'If 중간청산할지설정FLAG = True And s.A21_환산이익율 > 중간청산목표이익율 And s.A17_중간매도Flag = False Then
                's.A17_중간매도Flag = 1
                'End If

            End If

        End If

    End Sub

    Private Sub 살아있는신호확인하기_A(ByRef s As 순매수신호_탬플릿)

        Dim 매도사유 As String = ""
        Dim 현재매수매도점수 As Integer = PIP_Point_Lists(1).마지막신호_점수 + PIP_Point_Lists(2).마지막신호_점수
        Dim 종합주가지수 As Single = 순매수리스트(currentIndex_순매수).코스피지수

        If s.A03_신호ID = "A" Then
            Dim 마지막순매수index As Integer = Get마지막순매수Index()
            Dim 해제기준점수 As Integer = Val(Form2.txt_F2_신호해제점수기준.Text)
            Dim 해제기준점수_UP, 해제기준점수_DOWN As Integer
            If 해제기준점수 = 0 Then
                해제기준점수_UP = 0
                해제기준점수_DOWN = 0
            Else
                해제기준점수_UP = 해제기준점수
                해제기준점수_DOWN = 해제기준점수 * -1
            End If

            If 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then
                If s.A08_콜풋 = 0 And 현재매수매도점수 < 해제기준점수_UP Then
                    매도사유 = "weak"
                End If
                If s.A08_콜풋 = 1 And 현재매수매도점수 > 해제기준점수_DOWN Then
                    매도사유 = "weak"
                End If
            End If
        End If

        If s.A03_신호ID = "B" Then

            Dim 기울기A As Single = PIP_Point_Lists(1).마지막선기울기
            Dim 기울기B As Single = PIP_Point_Lists(2).마지막선기울기

            Dim 해제기준기울기 As Single = B_기준기울기 - B_해제기울기

            Dim 해제기준인덱스 As Integer = s.A01_발생Index + b_최소유지INDEX

            If s.A08_콜풋 = 0 Then

                If (기울기A < 해제기준기울기 Or 기울기B < 해제기준기울기) And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weak"

            ElseIf s.A08_콜풋 = 1 Then

                Dim 해제기준기울기_음수 As Single = B_해제기울기 * -1
                If (기울기A > 해제기준기울기_음수 Or 기울기B > 해제기준기울기_음수) And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weak"
            End If


        End If

        If s.A03_신호ID = "C" Then

            Dim 현재기울기 As Single = PIP_Point_Lists(0).마지막선기울기

            'C_해제기울기

            Dim 해제기준인덱스 As Integer = s.A01_발생Index + b_최소유지INDEX

            If s.A08_콜풋 = 0 Then

                If 현재기울기 < C_해제기울기 And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weak"

            ElseIf s.A08_콜풋 = 1 Then

                Dim 해제기준기울기_음수 As Single = C_해제기울기 * -1
                If 현재기울기 > 해제기준기울기_음수 And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weak"
            End If


        End If


        '손절조건 확인
        If s.A08_콜풋 = 0 And s.A06_신호발생종합주가지수 - 종합주가지수 > s.A60_손절기준차 Then 매도사유 = "son"
        If s.A08_콜풋 = 1 And 종합주가지수 - s.A06_신호발생종합주가지수 > s.A60_손절기준차 Then 매도사유 = "son"

        '익절조건 확인 - 현재매수매도점수 조건 추가 - 익절을 지연 시킴 - 20230126
        If s.A08_콜풋 = 0 And 종합주가지수 - s.A06_신호발생종합주가지수 > s.A61_익절기준차 And 현재매수매도점수 < 3 Then 매도사유 = "ik"
        If s.A08_콜풋 = 1 And s.A06_신호발생종합주가지수 - 종합주가지수 > s.A61_익절기준차 And 현재매수매도점수 > -3 Then 매도사유 = "ik"

        'timeout 확인
        If Val(순매수리스트(currentIndex_순매수).sTime) >= Val(s.A62_TimeoutTime) Then 매도사유 = "timeout"

        '옵션가격 기준 손절매
        Dim 옵션가손절매기준 As Single = Val(Form2.txt_F2_옵션가기준손절매.Text)
        If s.A21_환산이익율 < 옵션가손절매기준 Then 매도사유 = "option_son"

        '아래는 그냥 정보를 업데이트하는 코드들
        If s.A08_콜풋 = 0 Then s.A55_메모 = Math.Round(종합주가지수 - s.A06_신호발생종합주가지수, 2)                    '종합주가지수 차이를 계산한다
        If s.A08_콜풋 = 1 Then s.A55_메모 = Math.Round(s.A06_신호발생종합주가지수 - 종합주가지수, 2)

        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        If 일분옵션데이터_CurrentIndex >= 0 Then s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)

        If s.A14_현재가격 > 0 Then
            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
        End If


        '청산할 때 하는 프로세스
        If 매도사유 <> "" Then
            s.A05_신호해제순매수 = Get순매수(currentIndex_순매수, 0)
            s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수

            s.A15_현재상태 = 0
            s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
            s.A19_매도Index = currentIndex_순매수
            s.A20_매도사유 = 매도사유
            s.A22_신호해제가격 = s.A14_현재가격

            If s.A17_중간매도Flag = 1 Then '중간청산을 했으면 환산이익율을 조정한다
                Dim 중간청산목표이익율 As Single = Val(Form2.txt_F2_중간청산비율.Text)
                s.A21_환산이익율 = Math.Round((s.A21_환산이익율 + 중간청산목표이익율) / 2, 3)
            End If

            If EBESTisConntected = True Then Add_Log("신호", String.Format("해제신호 발생 AT {0}, 사유 = {1}", s.A18_매도시간, 매도사유))

            If s.A03_신호ID = "B" Then 이전순매수방향 = "중립"   'B알고리즘에 의해 매수 했다면 A도 살수 있게 조치한다

        Else  '살아 있으면 중간청산 체크하기
            Dim 중간청산목표이익율 As Single = Val(Form2.txt_F2_중간청산비율.Text)
            Dim 중간청산할지설정FLAG As Boolean = Form2.chk_중간청산.Checked
            If 중간청산할지설정FLAG = True And s.A21_환산이익율 > 중간청산목표이익율 And s.A17_중간매도Flag = False Then
                s.A17_중간매도Flag = 1
            End If
        End If

    End Sub

    Public Function is중간청산Flag(ByVal callput As Integer) As Boolean

        Dim ret As Boolean = False
        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A08_콜풋 = callput And s.A15_현재상태 = 1 Then   '방향이 같고 현재 살아있는 상태라면
                    If s.A17_중간매도Flag = 1 Then
                        ret = True
                    End If

                End If
            Next
        End If
        Return ret

    End Function

    Private Function 현재신호계산하기() As Integer

        Dim ret As Integer = 0

        If SoonMesuShinhoList IsNot Nothing Then
            For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A15_현재상태 = 1 Then
                    If s.A03_신호ID = "A" Or s.A03_신호ID = "B" Then
                        If s.A08_콜풋 = 0 Then ret = 1        '상승베팅
                        If s.A08_콜풋 = 1 Then ret = -1
                    End If
                End If
            Next
            For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A15_현재상태 = 1 Then
                    If s.A03_신호ID = "D" Then    'A,B보다 D를 우선시 한다
                        If s.A08_콜풋 = 0 Then ret = 2
                        If s.A08_콜풋 = 1 Then ret = -2
                    End If

                End If
            Next
        End If
        Return ret
    End Function

    Public Sub 매매신호처리함수()

        Dim 현재신호 As Integer = 현재신호계산하기()   '신호리스트로부터 현재신호를 계산해낸다 - D를 우선으로 한다
        Form2.lbl_F2_매매신호.Text = 현재신호.ToString()
        If 현재신호 > 0 Then
            Form2.lbl_F2_매매신호.BackColor = Color.Magenta
        ElseIf 현재신호 < 0 Then
            Form2.lbl_F2_매매신호.BackColor = Color.Green
        Else
            Form2.lbl_F2_매매신호.BackColor = Color.White
        End If

        Form2.lbl_F2_매매신호.Refresh()

        Dim is처분중 As Boolean = False

        If EBESTisConntected = True And 당일반복중_flag = False And ReceiveCount > 3 Then     '---------------- 당일반복 돌릴 때 문제 없도록 잘 해야 함, 시작하자마자 팔리거나 사는 걸 방지하기 위해 수신횟수를 추가함

            Dim 매매1회최대수량 As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)

            '현재잔고를 검사해서 방향과 다르면 처분하기 - 이건 시간에 관계없이 한다  
            If List잔고 IsNot Nothing Then
                For i As Integer = 0 To List잔고.Count - 1

                    Dim it As 잔고Type = List잔고(i)

                    If it.A02_구분 = "매도" Then  '무엇인가 매도된 상태라면

                        If Mid(it.A01_종복번호, 1, 1) = "2" Then
                            If 현재신호 >= 0 Then  '콜일 때 -내린다를 산 상태인데 앞으로 오르거나 신호해제가 되면
                                Dim 종목번호 As String = it.A01_종복번호

                                Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                                Dim count As Integer = 0

                                count = Math.Min(잔고와청산가능, 콜최대구매개수 - 콜현재환매개수)
                                count = Math.Min(count, 매매1회최대수량)
                                If count > 0 Then
                                    한종목매수(종목번호, it.A10_현재가, count, "매도를청산", "03") '호가유형 지정가 00, 시장가 03
                                End If

                            End If

                        Else '풋일 때  -- 오른다를 산 상태
                            If 현재신호 <= 0 Then
                                Dim 종목번호 As String = it.A01_종복번호
                                Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                                Dim count As Integer = 0

                                count = Math.Min(잔고와청산가능, 풋최대구매개수 - 풋현재환매개수)
                                count = Math.Min(count, 매매1회최대수량)
                                If count > 0 Then
                                    한종목매수(종목번호, it.A10_현재가, count, "매도를청산", "03") '호가유형 지정가 00, 시장가 03
                                End If
                            End If
                        End If

                    ElseIf it.A02_구분 = "매수" Then  '매수된 항목 처리하는 로직 

                        If Mid(it.A01_종복번호, 1, 1) = "3" Then
                            If 현재신호 >= 0 Then  '풋을 샀는데  -내린다를 산 상태인데 앞으로 오르거나 신호해제가 되면

                                Dim 종목번호 As String = it.A01_종복번호
                                Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)

                                Dim count As Integer = Math.Min(잔고와청산가능, 매매1회최대수량)
                                If count > 0 Then
                                    한종목매도(종목번호, it.A10_현재가, count, "매수를청산", "03") '호가유형 지정가 00, 시장가 03
                                    is처분중 = True
                                End If
                            End If

                        Else '콜을 매수한 상태이면 -  오른다를 산 상태
                            If 현재신호 <= 0 Then
                                Dim 종목번호 As String = it.A01_종복번호
                                Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)

                                Dim count As Integer = Math.Min(잔고와청산가능, 매매1회최대수량)
                                If count > 0 Then
                                    한종목매도(종목번호, it.A10_현재가, count, "매수를청산", "03") '호가유형 지정가 00, 시장가 03)
                                    is처분중 = True
                                End If
                            End If
                        End If

                        '중간청산 처리 로직
                        If is처분중 = False Then '위에서 처분하고 있다면 중간청산로직은 건너뛴다

                            If Mid(it.A01_종복번호, 1, 1) = "2" Then  '콜 매수의 경우

                                If is중간청산Flag(0) = True Then '해당하는 방향의 신호가 있고 그 신호의 중단매도 Flag가 1이면
                                    Dim 종목번호 As String = it.A01_종복번호

                                    Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                    Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                                    Dim count As Integer = 0
                                    count = Math.Min(잔고와청산가능, 콜중간청산개수 - 콜현재환매개수)
                                    count = Math.Min(count, 매매1회최대수량)
                                    If count > 0 Then
                                        한종목매도(종목번호, it.A10_현재가, count, "콜__중간청산", "03")  '호가유형 지정가 00, 시장가 03
                                    End If
                                End If

                            Else '풋일 때  -- 오른다를 산 상태

                                If is중간청산Flag(1) = True Then '해당하는 방향의 신호가 있고 그 신호의 중단매도 Flag가 1이면
                                    Dim 종목번호 As String = it.A01_종복번호
                                    Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                    Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                                    Dim count As Integer = 0
                                    count = Math.Min(잔고와청산가능, 풋중간청산개수 - 풋현재환매개수)  '중간청산개수는 최대구매개수와 동일하게 증가, 초기화하기 때문에 이걸 기준으로 환매개수를 빼서 중간청산개수까지만 매수한다
                                    count = Math.Min(count, 매매1회최대수량)
                                    If count > 0 Then
                                        한종목매도(종목번호, it.A10_현재가, count, "풋__중간청산", "03")  '호가유형 지정가 00, 시장가 03
                                    End If
                                End If
                            End If
                        End If

                    End If
                Next
            End If


            '신규매수를 한다 - 시간체크는 별도로 하지 않고 신호 생성에서 담당한다
            If currentIndex_순매수 >= 0 Then

                If Form2.chk_실거래실행.Checked = True Then

                    If 현재신호 > 0 Then  '상승베팅 

                        If is중간청산Flag(0) = False Then '중간 청산 후 다시 사는 걸 방지하기 위해 현재 중간청산 중인지 확인하는 함수. 현재 신호가 살아있고 중간청산 Flag가 set 되어 있는지 확인하여 중간청산이 아닐 때만 매도 실행

                            '매도 코드 주석처리함 20230221
                            'Dim code As String = 일분옵션데이터(1).Code
                            'Dim 최대허용구매개수 As Integer = Val(Form2.txt_F2_매도허용구매개수.Text)
                            'Dim count As Integer = Math.Min(풋구매가능개수, 최대허용구매개수 - 풋최대구매개수)
                            'count = Math.Min(count, 매매1회최대수량)   '매도했으나 체결이 늦게되어 더 많이 구매하는 문제처리 로직 검토
                            'Dim price As Single = 일분옵션데이터(1).price(currentIndex_1MIn, 3)
                            'If count > 0 Then 한종목매도(code, price, count, "신규매도", "03") '호가유형 지정가 00, 시장가 03

                            '매수를 추가함 - 20230205  - 상승베팅
                            추가매수실행(0, 현재신호) '콜을 매수한다
                        End If

                    ElseIf 현재신호 < 0 Then

                        If is중간청산Flag(1) = False Then '중간 청산 후 다시 사는 걸 방지하기 위해 현재 중간청산 중인지 확인하는 함수. 현재 신호가 살아있고 중간청산 Flag가 set 되어 있는지 확인하여 중간청산이 아닐 때만 매도 실행

                            '매도 코드 주석처리함 20230221
                            'Dim code As String = 일분옵션데이터(0).Code
                            'Dim 최대허용구매개수 As Integer = Val(Form2.txt_F2_매도허용구매개수.Text)
                            'Dim count As Integer = Math.Min(콜구매가능개수, 최대허용구매개수 - 콜최대구매개수)
                            'count = Math.Min(count, 매매1회최대수량)   '매도했으나 체결이 늦게되어 더 많이 구매하는 문제처리 로직 검토
                            'Dim price As Single = 일분옵션데이터(0).price(currentIndex_1MIn, 3)
                            'If count > 0 Then 한종목매도(code, price, count, "신규매도", "03") '호가유형 지정가 00, 시장가 03

                            '매수를 추가함 - 20230205 - 하락베팅
                            추가매수실행(1, 현재신호) '풋을 매수한다
                        End If

                    End If

                End If
            End If
        End If
    End Sub

    Private Sub 추가매수실행(ByVal direction As Integer, ByVal 투자그레이드 As Integer)

        Dim targetIndex As Integer = 매수종목index찾기(direction)
        If targetIndex >= 0 Then
            Dim it As ListTemplate = optionList(targetIndex)
            Dim 종목번호 As String = it.Code(direction)
            Dim price As Single = it.price(direction, 3)


            Dim 진짜최종투자금액 As Long = 최종투자금액
            If Math.Abs(투자그레이드) = 2 Then
                진짜최종투자금액 = 투자금_D
            End If

            Dim count As Integer = 매수수량계산(price, direction, 진짜최종투자금액)

            If count > 0 Then
                한종목매수(종목번호, price, count, "신규매수", "00")  '호가유형 지정가 00, 시장가 03

                If direction = 0 Then
                    콜현재까지매수금액 = 콜현재까지매수금액 + (price * count * 250000)
                Else
                    풋현재까지매수금액 = 풋현재까지매수금액 + (price * count * 250000)
                End If
            End If
        End If
    End Sub

    Private Function 매수수량계산(ByVal price As Single, ByVal direction As Integer, ByVal 진짜최종투자금액 As Long) As Integer
        Dim count As Integer = 0

        Dim adj_price As Single = price + 0.1
        Dim totalAmount As Long = adj_price * 250000

        If 진짜최종투자금액 > 0 Then

            Dim 평가금액기준남은금액 As Long = 진짜최종투자금액 - 평가종합.현재까지주문금액   '평가금액 기준 주문금액

            Dim 현재까지매수금액 As Long
            If direction = 0 Then
                현재까지매수금액 = 콜현재까지매수금액
            Else
                현재까지매수금액 = 풋현재까지매수금액
            End If
            Dim 현재까지매수금액기준남은금액 As Long = 진짜최종투자금액 - 현재까지매수금액      '주문할 때 계산한 주문금액 - 평가금액 반영이 늦어서 더 많이 사지는 걸 방지하는 기능
            Dim 남은금액 As Long = Math.Min(평가금액기준남은금액, 현재까지매수금액기준남은금액)  '평가금액 기준과 주문할 때 기준 중 작은 값으로 수량을 계산한다

            count = Math.Round((남은금액 / totalAmount), 0)

            '매수 최대구매개수와 비교를 한다         현재계좌에서 최대구매개수 가져오기
            Dim max매수카운트 As Integer = 0
            If List잔고 IsNot Nothing Then
                For i As Integer = 0 To List잔고.Count - 1
                    Dim it As 잔고Type = List잔고(i)
                    If it.A02_구분 = "매수" Then
                        max매수카운트 = Math.Max(max매수카운트, it.A03_잔고수량)
                    End If
                Next
            End If

            Dim 남은매매가능개수 As Integer = 0

            Dim tmp = Val(Form2.txt_F2_매수허용구매개수.Text) - max매수카운트  '50개에서 현재까지 산 갯수를 뺀다
            If tmp >= 0 Then 남은매매가능개수 = tmp

            count = Math.Min(count, 남은매매가능개수)
            count = Math.Min(count, Val(Form2.txt_F2_1회최대매매수량.Text))
        End If






        Return count
    End Function

    Private Function 매수종목index찾기(ByVal direction As Integer) As String

        Dim Targetprice As Single = Val(Form2.txt_F2_매수_기준가.Text)
        Dim targetIndex As Integer = -1
        Dim minGap As Single = 1100.0

        For i As Integer = 0 To optionList.Count - 1
            Dim it As ListTemplate = optionList(i)
            Dim gap As Single = Math.Abs(Targetprice - it.price(direction, 3))
            If gap < minGap And it.price(direction, 3) > 0.2 Then
                targetIndex = i
                minGap = gap
            End If
        Next

        Return targetIndex
    End Function

    Private Function Get마지막순매수Index() As Integer
        Dim ret = 0
        If SoonMesuShinhoList IsNot Nothing Then
            For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A15_현재상태 = 1 Then
                    ret = s.A01_발생Index
                End If
            Next
        End If
        Return ret
    End Function

    Public Sub Calc이동평균Data()   '이동평균선을 계산하여 일분데이터에 추가한다

        If currentIndex_1MIn < 이동평균선_기준일자 Then Return         ' 현재 index가 이동평균계산기준일자보다 작으면 패스한다

        For callput As Integer = 0 To 1
            For j As Integer = 이동평균선_기준일자 - 1 To currentIndex_1MIn

                If isRealFlag = False Then
                    If 일분옵션데이터(callput).이동평균선(j) <= 0 Then 일분옵션데이터(callput).이동평균선(j) = 이동평균선값계산(이동평균선_기준일자, callput, j)   '빈것들만 계산하여 속도를 빠르게 한다
                Else
                    일분옵션데이터(callput).이동평균선(j) = 이동평균선값계산(이동평균선_기준일자, callput, j) '0 값이 들어오는 경우가 많아서 real 에서는 전부 다 계산한다
                End If
            Next
        Next

    End Sub
End Module
