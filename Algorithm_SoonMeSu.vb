Option Explicit On
Imports System.Reflection
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
        Dim A11_손절기준가격 As String         'A11_주문번호을 A11_손절기준가격으로 변경함 F 알고리즘을 위해 변경
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

    Public 슬리피지 As Single = 0.02

    'A알고리즘용
    Public 합계순매수기준기울기 As Single = 11.0  ' 이걸 해봤으나 29 이상으로 높아지면 켈리지수는 높지만 이익이 2.얼마로 낮아져서 포기함  ----------- A 알고리즘 더이상 사용하지 않음

    'B 알고리즘용  
    Public B_StartTime As Integer = 92500
    Public B_EndTime As Integer = 101900
    Public B_기준기울기 As Single = 50.0
    Public B_해제기울기 As Single = 2.0
    Public B_마지막점과그앞점거리최소INDEX = 2   '추가 - 20230726  - 1틱만으로 50이 넘어서 신호가 생성되는 거를 제외함
    Public B_마지막점과그앞점거리최대INDEX = 50   '추가 - 20230726 - 상한으로는 안걸려서 큰 의미 없음 그냥 큰 숫자로 세팅함

    'C알고리즘 시작 - 시작하자마자 순매수가 몰리면 바로 사는 것

    Public C_StartTime As Integer = 90400
    Public C_EndTime As Integer = 90600
    Public C_개별금액 As Integer = 50
    Public C_합계금액 As Integer = 800
    Public C_해제기울기 As Integer = 10
    'C알고리즘 끝

    'C1 알고리즘 - 외국인만의 시작하자마자 매수를 검증하는 코드
    Public C1_StartTime As Integer = 90400
    Public C1_EndTime As Integer = 90600
    Public C1_개별금액 As Integer = 1000
    Public C1_해제기울기 As Integer = 13

    'C1 알고리즘 끝

    'D 알고리즘용 시작
    'CNT_009_A_50_B_0.59_C_0.6_D_0.33_E_0.4  ------------ B231115_T001 테스트  결과

    Public 이동평균선_기준일자 As Integer = 65       '이동평균선 갯수 기준
    Public X_계산기준봉비율 As Single = 0.59         '장대양봉의 크기를 계산하는 기준으로 X / 이동평균선_기준일자 비율을 의미함     230725 조건 재설정함 켈리지수 54. 승률 67%
    Public Y_장대양봉기준비율 As Single = 0.6      'X_계산기준봉비율내의 캔들들의 최대최소값의 차에 비해 어느정도인지에 대한 비율
    Public 장대양봉손절기준비율 As Single = 2.5    '장대양봉의 크기를 1로 두고 장대양봉 위에서부터 몇%에서 손절할지 결정함  - 추가
    Public 이평선하향돌파익절기준 As Single = 0.1   '이평선위에서 아래로 하향돌파 시 익절 기준으로 적어도 이익이 이 기준 이상일 때 매도함
    Public D_MAX이익율 As Single = 0.0              '상한치를 찍고 내려와서 이평선을 하향 돌파하면 매도하는 현재값
    Public D_MAX이익율상한 As Single = 0.33         '상한치를 찍고 내려와서 이평선을 하향 돌파하면 매도하는 기준 2023.08.09 추가함
    Public D_반대편음봉_양봉대비비율 As Single = 0.4   '한쪽만 올라가는 이상패턴을 걸러내기 위해 반대쪽도 비슷한 비율로 떨어지는지 확인하기 위한 변수임

    '20230725 추가
    'Public D신호_유지_IndexCount As Integer = 16
    'Public D신호_유지_비율 As Single = 1.0


    'D 알고리즘용 끝

    'E 알고리즘
    '12월 27일 시험, 9월 이후 데이터로만 테스트 결과 0,3일 CNT_008_A_4_B_9_C_2_D_120_E_104000_F_150000_G_4_H_1  켈리지수 49. 6배 이상
    '24년 1월 26일 --- CNT_001_A_4_B_7_C_2_D_120_E_105000_F_150000_G_4_H_1_I_65_J_2.5
    Public E_신호발생기준기울기 As Single = 6.0
    Public E_신호해제기준기울기 As Single = 2.0
    Public E_DataSource As Integer = 1 '0 : 외국인 + 기관, 1:외국인, 2 : 기관
    Public 신호최소유지시간index As Integer = 4 '신호가 뜬 후 최소 얼마간 유지할 건지를 판단하는 변수로 만약 4라면 2분 초과 필요하다
    Public E_기관반대순매수_허용크기비율 As Single = 2.5
    Public E2_tick_count_기준 As Integer = 30

    '20240212 E알고리즘 시험 결과 B240212_E202     E2_CNT_001_A_6_B_2_C_105000_D_150000_E_4_F_2.5_G_30 20승 11패 켈리지수 47.94  ------------- E2가 더 좋아서 E를 버리고 이걸 사용함
    'B240212_T011 'E2_CNT_001_A_6_B_2_C_105000_D_150000_E_4_F_2.5_G_30  -- 종합


    'F 알고리즘
    'CNT_000_A_22_B_75_C_1.2_D_1.05_E_1.3_F_0.9_G_1.2_H_0.95_I_50_J_1.18
    Public F_PIP_포인트수 As Integer = 7
    Public F_PoinIndexList(1) As List(Of Integer)   '포인트의 리스트

    Public F_PIP_최소카운트 As Integer = 22   '18  - 선이 6개가 각 3개로 구성됨  --------------------------------------- 변수
    Public F_PIP_최대카운트 As Integer = 75    'PIP 계산하는 최종 길이          --------------------------------------- 변수
    Public F_기본계곡최소깊이 As Single = 1.27  '헤드앤숄더에서 높은점과 낮은점의 기본 높이 차  ------------------------- 변수  ------------ 이걸 줄이면서 3일이나 6일 어찌되는지 봐야 함
    Public F_현재점의최소높이 As Single = 1.06  '헤드앤숄더에서 6번째점에서 7번째점의 최소 높이 차  --------------------- 변수
    Public F_현재점의최대높이 As Single = 1.3  '헤드앤숄더에서 6번째점에서 7번째점의 최대 높이 차  --------------------- 변수
    Public F_손절배율 As Single = 0.95         'option_son 나는걸 방지하기 위해 최저점을 하향 돌파할 때 손절한다 ----- 변수
    Public F_좌우골짜기깊이상한 As Single = 1.25  '중앙골짜기 대비 좌우 골짜기의 깊이 차 상한 --------------------------- 변수
    Public F_좌골짜기깊이하한 As Single = 0.92  '중앙골짜기 대비 좌우 골짜기의 깊이 차 하한 --------------------------- 변수


    Public F_최저점근접기간Index As Integer = 50  '몇가지를 보니 최저점에 가까울 때 트리플바닥으로 상승하는 경우가 많음. 이를 위해 최저점 근처인지를 확인하는데 PIP 시작점으로부터 이전 index count 변수 
    Public F_최저점대비높은비율 As Single = 1.1  '최근 최저점 대비 PIP의 최저점이 높은 허용 정도

    Public F_우골짜기깊이하한 As Single = 0.95  '중앙골짜기 대비 좌우 골짜기의 깊이 차 하한 --------------------------- 변수

    Public F_첫번째시작시간 As Integer = 100000
    Public F_첫번째종료시간 As Integer = 114000
    Public F_두번째시작시간 As Integer = 140000
    Public F_두번째종료시간 As Integer = 151000

    Public 첫번째중간매도이익율 As Single = 0.34
    Public 두번째중간매도이익율 As Single = 0.64
    Public 세번째중간매도이익율 As Single = 0.94

    Public 중간매도후이익율차이 As Single = 0.25



    Public Sub CalcAlgorithmAll()

        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        If 일분옵션데이터_CurrentIndex >= 0 Then

            모든살아있는신호확인하기(일분옵션데이터_CurrentIndex)

            'If Form2.chk_Algorithm_A.Checked = True Then CalcAlgorithm_A()
            If Form2.chk_Algorithm_B.Checked = True Then CalcAlgorithm_B()
            If Form2.chk_Algorithm_C.Checked = True Then CalcAlgorithm_C()
            If Form2.chk_Algorithm_D.Checked = True Then CalcAlgorithm_D(일분옵션데이터_CurrentIndex)
            If Form2.chk_Algorithm_E.Checked = True Then CalcAlgorithm_E()
            If Form2.chk_Algorithm_F.Checked = True Then CalcAlgorithm_F(일분옵션데이터_CurrentIndex)
            If Form2.chk_Algorithm_G.Checked = True Then CalcAlgorithm_G(일분옵션데이터_CurrentIndex)
            If Form2.chk_Algorithm_M.Checked = True Then CalcAlgorithm_M(일분옵션데이터_CurrentIndex)
            If Form2.chk_Algorithm_N.Checked = True Then CalcAlgorithm_N(일분옵션데이터_CurrentIndex)
            If Form2.chk_Algorithm_E2.Checked = True Then CalcAlgorithm_E2()
            If Form2.chk_Algorithm_O.Checked = True Then CalcAlgorithm_O()

        End If

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

                        '반대방향신호죽이는함수("A", 1, "change")                                 '이전 신호확인해서 죽이기

                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A", 0)               '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("A 상승신호 발생 AT {0}", shinho.A02_발생시간))

                    ElseIf ret = "하락" Then '중립에서 하락으로 전환

                        '반대방향신호죽이는함수("A", 0, "change")                                   '이전 신호확인해서 죽이기

                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A", 1)             '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("A 하락신호 발생 AT {0}", shinho.A02_발생시간))
                    End If

                    이전순매수방향 = ret '신호가 뜬걸 의미한다 이걸 바꿔 놓는다
                End If
            End If
        End If


    End Sub

    Public Sub CalcAlgorithm_E()  '외국인만의 순매를 기준으로 매수하는 알고리즘으로 변경 - 기관순매수가 0~0.05일 때 최고가 되어 기관순매수는 무시함

        Dim startTime As Integer = Val(Form2.txt_F2_매수시작시간.Text)
        Dim endTime As Integer = Val(Form2.txt_F2_매수마감시간.Text)


        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        If Val(순매수리스트(currentIndex_순매수).sTime) >= 123000 And Val(순매수리스트(currentIndex_순매수).sTime) <= 143000 Then Return  '12시반부터 14시반까지는 결과가 안좋아서 제외함 231228


        If Val(순매수리스트(currentIndex_순매수).sTime) >= startTime And Val(순매수리스트(currentIndex_순매수).sTime) <= endTime Then

            Dim 현재순매수기울기 As Single = PIP_Point_Lists(E_DataSource).마지막선기울기
            Dim 현재순매수기울기_절대치 As Single = Math.Abs(현재순매수기울기)

            'Dim 마지막점과그앞점Index차 As Integer = PIP_Point_Lists(E_DataSource).마지막점과그앞점간INDEXCOUNT

            If 현재순매수기울기_절대치 > E_신호발생기준기울기 Then


                '기관도 같은 방향이거나 크게 반대방향이 아닌지 확인하는 코드
                Dim 기관순매수기울기 As Single = PIP_Point_Lists(2).마지막선기울기
                Dim 기관순매수기울기_절대치 As Single = Math.Abs(기관순매수기울기)

                If 현재순매수기울기 * 기관순매수기울기 < 0 Then  '외국인과 기관의 순매수 방향이 다르면

                    Dim 기관반대순매수_허용기울기 As Single = E_신호발생기준기울기 * E_기관반대순매수_허용크기비율
                    If 기관순매수기울기_절대치 > 기관반대순매수_허용기울기 Then
                        Return
                    End If


                End If



                If 현재순매수기울기 > 0 Then  '  콜 방향


                    '직전에 동일한 신호가 해제되었다면 같은 방향으로 또 만들지 않는다 ---------------------------------------------------------------------------- 손절되었다가 다시 사는걸 방지 --- 이렇게 하는게 수익률이 좋음 20231230 확인
                    If is동일신호가있나("E", 0) = True Then Return

                    If is동일신호가현재살아있나("B", 0) = True Then Return 'B 알고리즘이 현재 살아있다면 E 신호를 만들지 않는다

                    Dim 현재이평선상태 As Integer = 일분옵션데이터(0).MACD_Result(2, 일분옵션데이터_CurrentIndex)

                    If 현재이평선상태 > 0 Then  '콜이 이평선 위에 있을때만 매수
                        Dim str As String = String.Format("E 신호 발생 콜풋 : {0} 방향", 0)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("E", 0)
                        SoonMesuShinhoList.Add(shinho)
                    End If

                Else ' 풋 방향

                    If is동일신호가있나("E", 1) = True Then Return


                    If is동일신호가현재살아있나("B", 1) = True Then Return   'B 알고리즘이 현재 살아있다면 E 신호를 만들지 않는다

                    Dim 현재이평선상태 As Integer = 일분옵션데이터(1).MACD_Result(2, 일분옵션데이터_CurrentIndex)  '풋의 직전 이평선의 +- 값

                    If 현재이평선상태 > 0 Then '풋이 이평선 위에 있을때만 매수
                        Dim str As String = String.Format("E 신호 발생 콜풋 : {0} 방향", 1)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("E", 1)
                        SoonMesuShinhoList.Add(shinho)
                    End If
                End If

            End If
        End If


    End Sub

    '15분(30틱) 기준 순매수 기울기를 기준으로 매매하는 알고리즘 20240212

    '20240212 E알고리즘 시험 결과 B240212_E202     E2_CNT_001_A_6_B_2_C_105000_D_150000_E_4_F_2.5_G_30 20승 11패 켈리지수 47.94

    Public Sub CalcAlgorithm_E2()

        Dim startTime As Integer = Val(Form2.txt_F2_매수시작시간.Text)
        Dim endTime As Integer = Val(Form2.txt_F2_매수마감시간.Text)


        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        If Val(순매수리스트(currentIndex_순매수).sTime) >= 123000 And Val(순매수리스트(currentIndex_순매수).sTime) <= 143000 Then Return  '12시반부터 14시반까지는 결과가 안좋아서 제외함 231228
        If Val(순매수리스트(currentIndex_순매수).sTime) >= startTime And Val(순매수리스트(currentIndex_순매수).sTime) <= endTime Then

            Dim 현재순매수기울기 As Single = 틱당기울기계산(E_DataSource, E2_tick_count_기준)
            Dim 현재순매수기울기_절대치 As Single = Math.Abs(현재순매수기울기)

            'Dim 마지막점과그앞점Index차 As Integer = PIP_Point_Lists(E_DataSource).마지막점과그앞점간INDEXCOUNT

            If 현재순매수기울기_절대치 > E_신호발생기준기울기 Then


                '기관도 같은 방향이거나 크게 반대방향이 아닌지 확인하는 코드
                Dim 기관순매수기울기 As Single = 틱당기울기계산(2, E2_tick_count_기준)
                Dim 기관순매수기울기_절대치 As Single = Math.Abs(기관순매수기울기)

                If 현재순매수기울기 * 기관순매수기울기 < 0 Then  '외국인과 기관의 순매수 방향이 다르면

                    Dim 기관반대순매수_허용기울기 As Single = E_신호발생기준기울기 * E_기관반대순매수_허용크기비율
                    If 기관순매수기울기_절대치 > 기관반대순매수_허용기울기 Then
                        Return
                    End If


                End If



                If 현재순매수기울기 > 0 Then  '  콜 방향


                    '직전에 동일한 신호가 해제되었다면 같은 방향으로 또 만들지 않는다 ---------------------------------------------------------------------------- 손절되었다가 다시 사는걸 방지 --- 이렇게 하는게 수익률이 좋음 20231230 확인
                    If is동일신호가있나("E2", 0) = True Then Return
                    If is동일신호가현재살아있나("B", 0) = True Then Return 'B 알고리즘이 현재 살아있다면 E 신호를 만들지 않는다

                    Dim 현재이평선상태 As Integer = 일분옵션데이터(0).MACD_Result(2, 일분옵션데이터_CurrentIndex)

                    If 현재이평선상태 > 0 Then  '콜이 이평선 위에 있을때만 매수
                        Dim str As String = String.Format("E___2 신호 발생 콜풋 : {0} 방향", 0)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("E2", 0)
                        SoonMesuShinhoList.Add(shinho)
                    End If

                Else ' 풋 방향

                    If is동일신호가있나("E2", 1) = True Then Return
                    If is동일신호가현재살아있나("B", 1) = True Then Return   'B 알고리즘이 현재 살아있다면 E 신호를 만들지 않는다

                    Dim 현재이평선상태 As Integer = 일분옵션데이터(1).MACD_Result(2, 일분옵션데이터_CurrentIndex)  '풋의 직전 이평선의 +- 값

                    If 현재이평선상태 > 0 Then '풋이 이평선 위에 있을때만 매수
                        Dim str As String = String.Format("E___2 신호 발생 콜풋 : {0} 방향", 1)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("E2", 1)
                        SoonMesuShinhoList.Add(shinho)
                    End If
                End If

            End If
        End If


    End Sub

    '삼위일체 - 외국인선물, 외국인현물, 이평선 위 3개가 맞을때만 매수하는 로직

    Public O_선물발생기준기울기 As Single = 16.0
    Public O_외국인현물발생기준기울기 As Single = 5.0

    Public O_선물해제기준기울기 As Single = 2.0
    Public O_외국인현물해제기준기울기 As Single = 2.0

    Public O_tick_count_기준 As Integer = 40
    Public O_해제tick_count_기준 As Integer = 25


    Public O_시작시간 As Integer = 100000
    Public O_마감시간 As Integer = 150000


    Public Sub CalcAlgorithm_O()

        Dim startTime As Integer = O_시작시간
        Dim endTime As Integer = O_마감시간


        Dim 일분옵션데이터_CurrentIndex As Integer
        If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False Then
            일분옵션데이터_CurrentIndex = currentIndex_1MIn
        Else
            일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
        End If

        'If Val(순매수리스트(currentIndex_순매수).sTime) >= 123000 And Val(순매수리스트(currentIndex_순매수).sTime) <= 143000 Then Return  '일단 전부다 함
        If Val(순매수리스트(currentIndex_순매수).sTime) >= startTime And Val(순매수리스트(currentIndex_순매수).sTime) <= endTime Then

            Dim 선물순매수기울기 As Single = 틱당기울기계산(3, O_tick_count_기준)
            Dim 외국인현물순매수기울기 As Single = 틱당기울기계산(1, O_tick_count_기준)


            Dim 선물순매수기울기_절대치 As Single = Math.Abs(선물순매수기울기)
            Dim 외국인현물순매수기울기_절대치 As Single = Math.Abs(외국인현물순매수기울기)


            If 선물순매수기울기 * 외국인현물순매수기울기 <= 0 Then Return '곱해서 음수이면 빠진다


            If 선물순매수기울기_절대치 > O_선물발생기준기울기 And 외국인현물순매수기울기_절대치 > O_외국인현물발생기준기울기 Then  '선물, 현물 둘다 매도나 매수중이면


                If 선물순매수기울기 > 0 Then  '  콜 방향


                    '직전에 동일한 신호가 해제되었다면 같은 방향으로 또 만들지 않는다 ---------------------------------------------------------------------------- 손절되었다가 다시 사는걸 방지 --- 이렇게 하는게 수익률이 좋음 20231230 확인
                    If is동일신호가있나("O", 0) = True Then Return


                    Dim 현재이평선상태 As Integer = 일분옵션데이터(0).MACD_Result(2, 일분옵션데이터_CurrentIndex)

                    If 현재이평선상태 > 0 Then  '콜이 이평선 위에 있을때만 매수
                        Dim str As String = String.Format("OOO 신호 발생 콜풋 : {0} 방향", 0)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("O", 0)
                        SoonMesuShinhoList.Add(shinho)
                    End If


                Else ' 풋 방향

                        If is동일신호가있나("O", 1) = True Then Return

                    Dim 현재이평선상태 As Integer = 일분옵션데이터(1).MACD_Result(2, 일분옵션데이터_CurrentIndex)  '풋의 직전 이평선의 +- 값

                    If 현재이평선상태 > 0 Then '풋이 이평선 위에 있을때만 매수
                        Dim str As String = String.Format("OOO 신호 발생 콜풋 : {0} 방향", 1)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("O", 1)
                        SoonMesuShinhoList.Add(shinho)
                    End If

                End If

                End If
        End If


    End Sub


    Public Sub CalcAlgorithm_B() '좀더 짧은 시간에 더 급격한 커블 때 매수하는 로직으로 변경함. 외국인, 기관 모두 20230403

        If Val(순매수리스트(currentIndex_순매수).sTime) >= B_StartTime And Val(순매수리스트(currentIndex_순매수).sTime) <= B_EndTime Then

            Dim 현재순매수기울기 As Single = PIP_Point_Lists(1).마지막선기울기  '0 통합, 1 -'외국인기울기, 2 기관
            Dim 현재순매수기울기_절대치 As Single = Math.Abs(현재순매수기울기)

            Dim 마지막점과그앞점Index차 As Integer = PIP_Point_Lists(1).마지막점과그앞점간INDEXCOUNT

            If 현재순매수기울기_절대치 > B_기준기울기 And 마지막점과그앞점Index차 > B_마지막점과그앞점거리최소INDEX And 마지막점과그앞점Index차 < B_마지막점과그앞점거리최대INDEX Then

                If 현재순매수기울기 > 0 Then  '  콜 방향


                    '직전에 동일한 신호가 해제되었다면 같은 방향으로 또 만들지 않는다
                    If is동일신호가있나("B", 0) = True Then Return

                    If is동일신호가현재살아있나("B", 0) = False Then
                        Dim str As String = String.Format("B 신호 발생 콜풋 : {0} 방향", 0)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("B", 0)
                        SoonMesuShinhoList.Add(shinho)
                    End If

                Else ' 풋 방향

                    If is동일신호가있나("B", 1) = True Then Return

                    If is동일신호가현재살아있나("B", 1) = False Then
                        Dim str As String = String.Format("B 신호 발생 콜풋 : {0} 방향", 1)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("B", 1)
                        SoonMesuShinhoList.Add(shinho)
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

    Public Sub CalcAlgorithm_C1() '외국인만의 --- 9시10분전 급격한 기울기

        Dim currentTime As Integer = Val(순매수리스트(currentIndex_순매수).sTime)

        If currentTime >= C1_StartTime And currentTime <= C1_EndTime Then  '변수 2건

            If SoonMesuShinhoList.Count <= 0 Then  '신호가 현재까지 한번도 안떳을 때만 수행한다

                If Math.Abs(순매수리스트(currentIndex_순매수).외국인순매수) > C1_개별금액 And 순매수리스트(currentIndex_순매수).외국인순매수 * 순매수리스트(currentIndex_순매수).기관순매수 < 0 Then  '개별 항목들이 기준 금액을 넘었고 기관과 외국인의 방향이 다를 때


                    If 순매수리스트(currentIndex_순매수).외국인순매수 > 0 Then

                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("C1", 0)               '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)
                        If EBESTisConntected = True Then Add_Log("C1 신호", String.Format("C1 - 최초 상승신호 발생 AT {0}", shinho.A02_발생시간))

                    Else

                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("C1", 1)             '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)
                        If EBESTisConntected = True Then Add_Log("C1 신호", String.Format("C1 - 최초 하락신호 발생 AT {0}", shinho.A02_발생시간))

                    End If

                End If

            End If

        End If

    End Sub


    Public Function CalcAlgorithm_AB() As String

        Dim ret As String = "중립"

        Dim CurrentIndex_순매수_마지막방향 As String = 신규합계_마지막신호

        If 이전순매수방향 <> CurrentIndex_순매수_마지막방향 Then

            'Dim 현재순매수기울기 As Single = PIP_Point_Lists(0).마지막선기울기


            If 이전순매수방향 = "중립" Then
                If CurrentIndex_순매수_마지막방향 = "상승" Then 'And 합계순매수기준기울기 <= 현재순매수기울기
                    ret = "상승" '상승 베팅
                ElseIf CurrentIndex_순매수_마지막방향 = "하락" Then ' And 합계순매수기준기울기 * -1 >= 현재순매수기울기
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

    Public Sub CalcAlgorithm_D(ByVal 일분옵션데이터_CurrentIndex As Integer) '이동평균선 알고리즘

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
                D_MAX이익율 = 0.0

            End If

        Next

    End Sub

    Private Function is동일신호가있나(ByVal 신호id As String, ByVal callput As Integer) As Boolean

        If SoonMesuShinhoList IsNot Nothing Then
            For i As Integer = SoonMesuShinhoList.Count - 1 To 0 Step -1
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A03_신호ID = 신호id And s.A08_콜풋 = callput Then

                    Return True  '신호와 방향이 같은게 있었다면 true를 리턴해서 다시 뜨지 않도록 함

                ElseIf s.A03_신호ID = 신호id And s.A08_콜풋 <> callput Then

                    Return False 'E 신호는 같으나 방향이 다른게 있었다면 false를 리턴

                End If

            Next
        End If

        Return False
    End Function



    Public Function is동일신호가현재살아있나(ByVal 신호id As String, ByVal callput As Integer) As Boolean

        If SoonMesuShinhoList IsNot Nothing Then

            If 신호id = "ALL" Then
                For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                    Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                    If s.A15_현재상태 = 1 And s.A08_콜풋 = callput Then Return 1
                Next

            Else
                For i As Integer = 0 To SoonMesuShinhoList.Count - 1
                    Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                    If s.A03_신호ID = 신호id And s.A15_현재상태 = 1 And s.A08_콜풋 = callput Then Return 1
                Next

            End If
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

        Dim 현재분봉크기_비율 As Single = 일분옵션데이터(callput).price(index, 3) / 일분옵션데이터(callput).price(index, 0) - 1

        If 기준캔들내최대크기 * Y_장대양봉기준비율 < 현재분봉크기 Then


            '반대쪽도 충분한 장대음봉인지 확인해본다 20230820 추가
            Dim callput_revese As Integer = 0
            If callput = 0 Then callput_revese = 1

            Dim 반대편현재분봉크기 As Single = 일분옵션데이터(callput_revese).price(index, 0) / 일분옵션데이터(callput_revese).price(index, 3) - 1  '시가 - 종가 : 이러면 음봉인지 확인됨

            If 현재분봉크기_비율 * D_반대편음봉_양봉대비비율 < 반대편현재분봉크기 Then
                Return 1 '반대편 분봉이 충분히 크면 인정
            End If

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
        shinho.A60_손절기준차 = Form2.txt_F2_손절매차.Text
        shinho.A61_익절기준차 = Form2.txt_F2_익절차.Text
        shinho.A62_TimeoutTime = Form2.txt_F2_TimeoutTime.Text

        If shinho.A03_신호ID = "D" Then
            Dim 장대양봉크기 As Single = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3) - 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 0)
            shinho.A53_장대양봉손절가 = 일분옵션데이터(callput).price(currentIndex_1MIn - 1, 3) - (장대양봉크기 * 장대양봉손절기준비율)
        End If



        If 일분옵션데이터_CurrentIndex >= 0 Then

            If shinho.A03_신호ID = "D" Or shinho.A03_신호ID = "M" Or shinho.A03_신호ID = "N" Then

                Dim buyingPrice As Single
                If 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0) > 0 Then
                    buyingPrice = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0)
                Else
                    buyingPrice = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex - 1, 3)
                End If

                shinho.A10_신호발생가격 = buyingPrice
                shinho.A14_현재가격 = buyingPrice

            Else
                shinho.A10_신호발생가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                shinho.A14_현재가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
            End If

            shinho.A16_이익률 = Math.Round((shinho.A14_현재가격 - shinho.A10_신호발생가격) / shinho.A10_신호발생가격, 3)
        End If
        shinho.B00_etc = Form2.txt_F2_실험조건.Text

        Return shinho

    End Function


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

    Private Sub 모든살아있는신호확인하기(ByVal 일분옵션데이터_CurrentIndex As Integer)

        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)

                If s.A15_현재상태 = 1 Then

                    Dim ret As String = 살아있는신호확인하기_통합(s, 일분옵션데이터_CurrentIndex)

                    If ret = "" And s.A14_현재가격 > 0 Then

                        Select Case s.A03_신호ID

                            Case "C"
                                ret = 살아있는신호확인하기_C(s, 일분옵션데이터_CurrentIndex)
                            Case "C1"
                                ret = 살아있는신호확인하기_C(s, 일분옵션데이터_CurrentIndex)
                            Case "D"
                                ret = 살아있는신호확인하기_D(s, 일분옵션데이터_CurrentIndex)
                            Case "B"
                                ret = 살아있는신호확인하기_E(s, 일분옵션데이터_CurrentIndex)
                            Case "E"
                                ret = 살아있는신호확인하기_E(s, 일분옵션데이터_CurrentIndex)
                            Case "F"
                                ret = 살아있는신호확인하기_F(s, 일분옵션데이터_CurrentIndex)
                            Case "G"
                                ret = 살아있는신호확인하기_G(s, 일분옵션데이터_CurrentIndex)
                            Case "M"
                                ret = 살아있는신호확인하기_M(s, 일분옵션데이터_CurrentIndex)
                            Case "N"
                                ret = 살아있는신호확인하기_N(s, 일분옵션데이터_CurrentIndex)
                            Case "E2"
                                ret = 살아있는신호확인하기_E2(s, 일분옵션데이터_CurrentIndex)
                            Case "O"
                                ret = 살아있는신호확인하기_O(s, 일분옵션데이터_CurrentIndex)
                        End Select
                    End If

                    '마지막 신호가 아니라면 continue  시킨다
                    If i <> SoonMesuShinhoList.Count - 1 And ret = "" Then
                        ret = "continue"
                    End If


                    If ret <> "" Then
                        죽은신호처리하기(s, ret)
                    End If

                End If

                SoonMesuShinhoList(i) = s

            Next
        End If

    End Sub

    Private Sub 죽은신호처리하기(ByRef s As 순매수신호_탬플릿, ByVal 매도사유 As String)

        '청산할 때 하는 프로세스
        If 매도사유 <> "" Then

            s.A05_신호해제순매수 = Get순매수(currentIndex_순매수, 0)
            s.A15_현재상태 = 0
            s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
            s.A19_매도Index = currentIndex_순매수
            s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수
            s.A20_매도사유 = 매도사유
            s.A22_신호해제가격 = s.A14_현재가격

            Dim 나머지, 첫번째, 두번째, 세번째, 최종 As Single

            If s.A17_중간매도Flag = 0 Then

                최종 = s.A21_환산이익율 * 1.0

            ElseIf s.A17_중간매도Flag = 1 Then

                나머지 = s.A21_환산이익율 * 0.7
                첫번째 = 첫번째중간매도이익율 * 0.3
                최종 = 나머지 + 첫번째

            ElseIf s.A17_중간매도Flag = 2 Then

                나머지 = s.A21_환산이익율 * 0.4
                첫번째 = 첫번째중간매도이익율 * 0.3
                두번째 = 두번째중간매도이익율 * 0.3
                최종 = 나머지 + 첫번째 + 두번째

            ElseIf s.A17_중간매도Flag = 3 Then

                나머지 = s.A21_환산이익율 * 0.2
                첫번째 = 첫번째중간매도이익율 * 0.3
                두번째 = 두번째중간매도이익율 * 0.3
                세번째 = 세번째중간매도이익율 * 0.2
                최종 = 나머지 + 첫번째 + 두번째 + 세번째

            End If


            s.A21_환산이익율 = Math.Round(최종, 3)

            If EBESTisConntected = True Then Add_Log("신호", String.Format("해제신호 발생 AT {0}, 사유 = {1}", s.A18_매도시간, 매도사유))

        End If

    End Sub

    Private Function 살아있는신호확인하기_통합(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String


        Dim 매도사유 As String = ""

        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
        If s.A14_현재가격 > 0 Then
            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
        End If

        '신호리스트의 최신을 찾아서 만약 현재와 방향이 다르면 reverse한다   -- 20230806 추가
        Dim 최종신호 As Integer = 현재신호계산하기()
        If (s.A08_콜풋 = 0 And 최종신호 < 0) Or (s.A08_콜풋 = 1 And 최종신호 > 0) Then
            매도사유 = "reverse"
        End If


        '옵션가격 기준 손절매, 익절
        Dim 옵션가손절매기준 As Single = Val(Form2.txt_F2_옵션가기준손절매.Text)
        If s.A17_중간매도Flag = 1 Then
            옵션가손절매기준 = 첫번째중간매도이익율 - 중간매도후이익율차이   ' 중간매도가 되면 손절매 기준을 올려서 수익을 좋게 만든다
        ElseIf s.A17_중간매도Flag = 2 Then
            옵션가손절매기준 = 두번째중간매도이익율 - 중간매도후이익율차이
        ElseIf s.A17_중간매도Flag = 3 Then
            옵션가손절매기준 = 세번째중간매도이익율 - 중간매도후이익율차이
        End If
        'Form2.txt_F2_옵션가기준손절매.Text = 옵션가손절매기준.ToString()  -- 이거 지워야 함

        Dim 옵션익절기준 As Single = Val(Form2.txt_F2_익절차.Text)
        If s.A21_환산이익율 < 옵션가손절매기준 Then

            매도사유 = "option_son"

            If isRealFlag = False Then
                s.A14_현재가격 = Math.Round(s.A10_신호발생가격 + (s.A10_신호발생가격 * 옵션가손절매기준), 2)
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
            End If

        End If

        '익절조건 확인 - 현재매수매도점수 조건 추가 - 익절을 지연 시킴 - 20230126  - 익절지연코드는 삭제 20230526 
        Dim 종합주가지수 As Single = 순매수리스트(currentIndex_순매수).코스피지수
        If s.A08_콜풋 = 0 And 종합주가지수 - s.A06_신호발생종합주가지수 > s.A61_익절기준차 Then 매도사유 = "ik"
        If s.A08_콜풋 = 1 And s.A06_신호발생종합주가지수 - 종합주가지수 > s.A61_익절기준차 Then 매도사유 = "ik"


        'RSI에 의한 익절 확인
        If s.A21_환산이익율 > RSI_익절기준 Then  'RSI 익절기준을 넘었고

            If 일분옵션데이터(s.A08_콜풋).RSI(일분옵션데이터_CurrentIndex) > RSI_과열기준 Then

                매도사유 = "RSI_IK"

            End If
        End If


        '타임아웃
        If Val(순매수리스트(currentIndex_순매수).sTime) >= Val(s.A62_TimeoutTime) Then
            매도사유 = "timeout"
            If isRealFlag = False Then
                s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0)
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
            End If
        End If


        '중간청산 후 50일선 하향돌파 시 청산
        Dim 현재이평선 As Single = 일분옵션데이터(s.A08_콜풋).이동평균선(일분옵션데이터_CurrentIndex)
        If s.A17_중간매도Flag > 0 And 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0) > 현재이평선 And 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3) < 현재이평선 Then
            매도사유 = "below50"
            s.A14_현재가격 = Math.Round(현재이평선 - 0.01, 2)
            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
        End If


        Dim 중간청산할지설정FLAG As Boolean = Form2.chk_중간청산.Checked
        If 중간청산할지설정FLAG = True Then

            If s.A17_중간매도Flag = 0 And s.A21_환산이익율 > 첫번째중간매도이익율 Then
                s.A17_중간매도Flag = 1
            ElseIf s.A17_중간매도Flag = 1 And s.A21_환산이익율 > 두번째중간매도이익율 Then
                s.A17_중간매도Flag = 2
            ElseIf s.A17_중간매도Flag = 2 And s.A21_환산이익율 > 세번째중간매도이익율 Then
                s.A17_중간매도Flag = 3

            End If

        End If

        '아래는 그냥 정보를 업데이트하는 코드들
        If s.A08_콜풋 = 0 Then s.A55_메모 = Math.Round(종합주가지수 - s.A06_신호발생종합주가지수, 2)                    '종합주가지수 차이를 계산한다
        If s.A08_콜풋 = 1 Then s.A55_메모 = Math.Round(s.A06_신호발생종합주가지수 - 종합주가지수, 2)

        Return 매도사유
    End Function



    Private Function 살아있는신호확인하기_D(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then

            '양봉크기비례 손절  - 장대양봉의 하단*비율을 하향 돌파하면 손절함  -- 없는게 결과가 더 좋아서 잠정 보류함 20230402
            If 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 2) < s.A53_장대양봉손절가 Then
                's.A14_현재가격 = s.A53_장대양봉손절가 - 0.01
                's.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                's.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                '매도사유 = "son"
            End If

            '이평선 하양돌파 익절  
            Dim 현재이평선 As Single = 일분옵션데이터(s.A08_콜풋).이동평균선(일분옵션데이터_CurrentIndex)
            Dim 현재이평선기준이익률 As Single = (현재이평선 - s.A10_신호발생가격) / s.A10_신호발생가격
            If 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 2) < 현재이평선 And 현재이평선기준이익률 > 이평선하향돌파익절기준 Then
                s.A14_현재가격 = 현재이평선 - 0.01
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                매도사유 = "ik_ip"
            End If

            If s.A21_환산이익율 > D_MAX이익율 Then
                D_MAX이익율 = s.A21_환산이익율
            End If

            If D_MAX이익율 > D_MAX이익율상한 And 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 2) < 현재이평선 Then
                매도사유 = "D_weak"
                s.A14_현재가격 = 현재이평선 - 0.01
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
            End If

            '이전 tick의 고가가 낮을 때 비교
            'If D_MAX이익율 > D_MAX이익율상한 And 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex - 1, 1) < 현재이평선 Then
            '매도사유 = "D_weak2"
            's.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0) ' 현재틱의 시가
            's.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
            's.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
            'End If


            'D 신호 발생 후 일정 시간이 지났는데도 올라가지 않으면 손절  ---- 없는게 제일 나아서 제외함
            'If 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3) < s.A10_신호발생가격 * D신호_유지_비율 And currentIndex_순매수 > s.A01_발생Index + D신호_유지_IndexCount Then
            's.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
            's.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
            's.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
            '매도사유 = "Weak_D"
            'End If
        End If

        Return 매도사유

    End Function

    Private Function 살아있는신호확인하기_F(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then
            'F알고리즘 특화
            Dim 손절기준가 As Single = s.A11_손절기준가격 * F_손절배율
            If s.A14_현재가격 < 손절기준가 And s.A14_현재가격 > 0.01 Then  '현재가격이 순간적으로 0일 때 팔려버리는 문제가 있어서 fix함

                매도사유 = "F_son"

                If isRealFlag = False Then
                    s.A14_현재가격 = Math.Round(손절기준가 - 0.01, 2)
                    s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                End If

            End If

            'G알고리즘 특화 - Index limit  추가하니 더 안좋아져서 삭제함
        End If

        Return 매도사유

    End Function

    Private Function 살아있는신호확인하기_E(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then

            '순매수가 기준이하로 작아질 때 weak_E로 매도함
            Dim 현재순매수기울기 As Single = PIP_Point_Lists(1).마지막선기울기
            Dim 마지막순매수index As Integer = Get마지막순매수Index()
            If 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then

                If s.A08_콜풋 = 0 Then

                    If s.A03_신호ID = "B" And is동일신호가현재살아있나("E", 1) = True Then  '반대방향 E 신호 발생
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "E_Occur"
                    End If
                    If s.A03_신호ID = "E" Then
                        If E_신호해제기준기울기 > 현재순매수기울기 Then    '해제기준보다 현재순매수기울기가 작다면 매도
                            s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                            매도사유 = "weak_E"
                        End If
                    ElseIf s.A03_신호ID = "B" Then
                        If B_해제기울기 > 현재순매수기울기 Then    '해제기준보다 현재순매수기울기가 작다면 매도
                            s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                            매도사유 = "weak_B"
                        End If
                    End If

                Else
                    If s.A03_신호ID = "B" And is동일신호가현재살아있나("E", 0) = True Then  '반대방향 E 신호 발생
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "E_Occur"
                    End If
                    If s.A03_신호ID = "E" Then
                        If (E_신호해제기준기울기 * -1) < 현재순매수기울기 Then    '해제기준값보다  현재순매수기울기가 크다면 매도
                            s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                            매도사유 = "weak_E"
                        End If
                    ElseIf s.A03_신호ID = "B" Then
                        If (B_해제기울기 * -1) < 현재순매수기울기 Then    '해제기준값보다  현재순매수기울기가 크다면 매도
                            s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                            매도사유 = "weak_B"
                        End If
                    End If
                End If
            End If

        End If

        Return 매도사유
    End Function


    Private Function 살아있는신호확인하기_E2(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then


            Dim 현재순매수기울기 As Single = 틱당기울기계산(E_DataSource, E2_tick_count_기준)

            Dim 마지막순매수index As Integer = Get마지막순매수Index()
            If 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then

                If s.A08_콜풋 = 0 Then


                    If E_신호해제기준기울기 > 현재순매수기울기 Then    '해제기준보다 현재순매수기울기가 작다면 매도
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "weak_E2"
                    End If

                Else

                    If (E_신호해제기준기울기 * -1) < 현재순매수기울기 Then    '해제기준값보다  현재순매수기울기가 크다면 매도
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "weak_E2"
                    End If

                End If
            End If

        End If





        Return 매도사유
    End Function

    Private Function 살아있는신호확인하기_O(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then


            Dim 선물현재순매수기울기 As Single = 틱당기울기계산(3, O_해제tick_count_기준)
            Dim 외국인현물현재순매수기울기 As Single = 틱당기울기계산(1, O_해제tick_count_기준)


            Dim 마지막순매수index As Integer = Get마지막순매수Index()
            If 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then

                If s.A08_콜풋 = 0 Then

                    '선물이 낮아지면

                    If O_선물해제기준기울기 > 선물현재순매수기울기 Then    '해제기준보다 현재순매수기울기가 작다면 매도
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "weak_O_1"
                    End If

                    '현물이 낮아지면
                    If O_외국인현물해제기준기울기 > 외국인현물현재순매수기울기 Then    '해제기준보다 현재순매수기울기가 작다면 매도
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "weak_O_2"
                    End If


                Else

                    '선물이 낮아지면
                    If (O_선물해제기준기울기 * -1) < 선물현재순매수기울기 Then    '해제기준값보다  현재순매수기울기가 크다면 매도
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "weak_O_1"
                    End If

                    '현물이 낮아지면
                    If (O_외국인현물해제기준기울기 * -1) < 외국인현물현재순매수기울기 Then    '해제기준값보다  현재순매수기울기가 크다면 매도
                        s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                        s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                        s.A21_환산이익율 = Math.Round(s.A16_이익률 - 슬리피지, 3)
                        매도사유 = "weak_O_2"
                    End If

                End If
            End If

        End If





        Return 매도사유
    End Function

    'C 9시 10분 이전 발생하는 신호에서 사용함
    Private Function 살아있는신호확인하기_C(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A03_신호ID = "C" Then

            Dim 현재기울기 As Single = PIP_Point_Lists(0).마지막선기울기
            Dim 해제기준인덱스 As Integer = s.A01_발생Index + 신호최소유지시간index

            If s.A08_콜풋 = 0 Then

                If 현재기울기 < C_해제기울기 And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weakC"

            ElseIf s.A08_콜풋 = 1 Then

                Dim 해제기준기울기_음수 As Single = C_해제기울기 * -1
                If 현재기울기 > 해제기준기울기_음수 And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weakC"
            End If
        ElseIf s.A03_신호ID = "C1" Then

            Dim 현재기울기 As Single = PIP_Point_Lists(1).마지막선기울기  '외국인만의 기울기
            Dim 해제기준인덱스 As Integer = s.A01_발생Index + 신호최소유지시간index

            If s.A08_콜풋 = 0 Then

                If 현재기울기 < C1_해제기울기 And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weakC1"

            ElseIf s.A08_콜풋 = 1 Then

                Dim 해제기준기울기_음수 As Single = C1_해제기울기 * -1
                If 현재기울기 > 해제기준기울기_음수 And currentIndex_순매수 > 해제기준인덱스 Then 매도사유 = "weakC1"
            End If
        End If

        '청산할 때 하는 프로세스  - C에만 이걸 넣는다
        If 매도사유 <> "" Then
            s.A05_신호해제순매수 = Get순매수(currentIndex_순매수, 0)
            s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수
        End If

        Return 매도사유

    End Function

    Public Function is중간청산Flag(ByVal callput As Integer) As Integer

        Dim ret As Integer = 0
        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A08_콜풋 = callput And s.A15_현재상태 = 1 Then   '방향이 같고 현재 살아있는 상태라면
                    ret = s.A17_중간매도Flag
                End If
            Next
        End If
        Return ret

    End Function

    Private Function 현재신호계산하기() As Integer

        Dim ret As Integer = 0

        If SoonMesuShinhoList IsNot Nothing Then
            Dim lastCount = SoonMesuShinhoList.Count - 1
            If lastCount >= 0 Then
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(lastCount)
                If s.A15_현재상태 = 1 Then
                    If s.A03_신호ID = "E" Then   'E는 90프로를 투자하고 나머지는 100% 투자한다

                        If s.A08_콜풋 = 0 Then ret = 1        '상승베팅
                        If s.A08_콜풋 = 1 Then ret = -1
                    Else
                        If s.A08_콜풋 = 0 Then ret = 2        '상승베팅
                        If s.A08_콜풋 = 1 Then ret = -2
                    End If
                End If
            End If
        End If
        Return ret
    End Function

    Private Function 마지막신호Index계산하기() As Integer

        Dim ret As Integer = 0

        If SoonMesuShinhoList IsNot Nothing Then
            Dim lastCount = SoonMesuShinhoList.Count - 1
            If lastCount >= 0 Then
                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(lastCount)
                If s.A15_현재상태 = 1 Then
                    ret = s.A01_발생Index
                End If
            End If
        End If
        Return ret
    End Function

    Public Sub 매매신호처리함수()

        Dim 현재신호 As Integer = 현재신호계산하기()   '신호리스트로부터 현재신호를 계산해낸다 - D를 우선으로 한다

        'Add_Log("일반", "현재신호 = " + 현재신호.ToString())

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



        'If EBESTisConntected = True Then     '---------------- 테스트할 때 쓰기 위해서 다 삭제한 버전
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

                                '매매 수량을 가격에 따라 가변적으로 변하게 설정한다
                                Dim price As Single = it.A10_현재가
                                Dim 계산count As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)
                                If price < 0.8 Then 계산count = Math.Max(CInt(-250 * price + 250), 계산count)
                                매매1회최대수량 = Math.Max(매매1회최대수량, 계산count)

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

                                '매매 수량을 가격에 따라 가변적으로 변하게 설정한다
                                Dim price As Single = it.A10_현재가
                                Dim 계산count As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)
                                If price < 0.8 Then 계산count = Math.Max(CInt(-250 * price + 250), 계산count)
                                매매1회최대수량 = Math.Max(매매1회최대수량, 계산count)

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

                                Dim 중간청산상태값 As Integer = is중간청산Flag(0)

                                If 중간청산상태값 > 0 Then '해당하는 방향의 신호가 있고 그 신호의 중단매도 Flag가 1보다 같거나 크다면 이면

                                    Dim 종목번호 As String = it.A01_종복번호

                                    Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                    Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)

                                    '매매 수량을 가격에 따라 가변적으로 변하게 설정한다
                                    Dim price As Single = it.A10_현재가
                                    Dim 계산count As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)
                                    If price < 0.8 Then 계산count = Math.Max(CInt(-250 * price + 250), 계산count)
                                    매매1회최대수량 = Math.Max(매매1회최대수량, 계산count)


                                    '3회에 걸쳐 중간청산을 한다
                                    '1회 = 0.34% 조건, 0.17% 손절가 변경, 30%, 
                                    '2회 = 0.64% 조건, 0.40% 손절가 변경, 60%
                                    '3회 = 0.94% 조건, 0.60% 손절가 변경, 80%
                                    '나머지는 마지막

                                    If 중간청산상태값 = 1 Then
                                        콜중간청산가능개수 = Math.Round(콜최대구매개수 * 0.3, 0)

                                    ElseIf 중간청산상태값 = 2 Then
                                        콜중간청산가능개수 = Math.Round(콜최대구매개수 * 0.6, 0)
                                    ElseIf 중간청산상태값 = 3 Then
                                        콜중간청산가능개수 = Math.Round(콜최대구매개수 * 0.8, 0)
                                    Else
                                        Add_Log("오류", "콜 중간청산상태값 오류")
                                    End If

                                    Dim count As Integer = 0
                                    count = Math.Min(잔고와청산가능, 콜중간청산가능개수 - 콜현재환매개수)
                                    count = Math.Min(count, 매매1회최대수량)
                                    If count > 0 Then
                                        한종목매도(종목번호, it.A10_현재가, count, "콜__중간청산" + 중간청산상태값.ToString() + "번째", "03")  '호가유형 지정가 00, 시장가 03
                                    End If
                                End If

                            Else '풋일 때  -- 오른다를 산 상태

                                Dim 중간청산상태값 As Integer = is중간청산Flag(1)

                                If 중간청산상태값 > 0 Then '해당하는 방향의 신호가 있고 그 신호의 중단매도 Flag가 1이면
                                    Dim 종목번호 As String = it.A01_종복번호
                                    Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                    Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)

                                    '매매 수량을 가격에 따라 가변적으로 변하게 설정한다
                                    Dim price As Single = it.A10_현재가
                                    Dim 계산count As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)
                                    If price < 0.8 Then 계산count = Math.Max(CInt(-250 * price + 250), 계산count)
                                    매매1회최대수량 = Math.Max(매매1회최대수량, 계산count)

                                    '3회에 걸쳐 중간청산을 한다
                                    '1회 = 0.34% 조건, 0.17% 손절가 변경, 30%, 
                                    '2회 = 0.64% 조건, 0.40% 손절가 변경, 60%
                                    '3회 = 0.94% 조건, 0.60% 손절가 변경, 80%
                                    '나머지는 마지막

                                    If 중간청산상태값 = 1 Then
                                        풋중간청산가능개수 = Math.Round(풋최대구매개수 * 0.3, 0)
                                    ElseIf 중간청산상태값 = 2 Then
                                        풋중간청산가능개수 = Math.Round(풋최대구매개수 * 0.6, 0)
                                    ElseIf 중간청산상태값 = 3 Then
                                        풋중간청산가능개수 = Math.Round(풋최대구매개수 * 0.8, 0)
                                    Else
                                        Add_Log("오류", "풋 중간청산상태값 오류")
                                    End If

                                    Dim count As Integer = 0
                                    count = Math.Min(잔고와청산가능, 풋중간청산가능개수 - 풋현재환매개수)  '중간청산개수는 최대구매개수와 동일하게 증가, 초기화하기 때문에 이걸 기준으로 환매개수를 빼서 중간청산개수까지만 매수한다
                                    count = Math.Min(count, 매매1회최대수량)
                                    If count > 0 Then
                                        한종목매도(종목번호, it.A10_현재가, count, "풋__중간청산 " + 중간청산상태값.ToString() + "번째", "03")  '호가유형 지정가 00, 시장가 03
                                    End If
                                End If
                            End If
                        End If

                    End If
                Next


                '신규매수를 한다 - 시간체크는 별도로 하지 않고 신호 생성에서 담당한다
                If currentIndex_순매수 >= 0 Then

                    If Form2.chk_실거래실행.Checked = True Then

                        If 현재신호 > 0 Then  '상승베팅 

                            If is중간청산Flag(0) = False Then '중간 청산 후 다시 사는 걸 방지하기 위해 현재 중간청산 중인지 확인하는 함수. 현재 신호가 살아있고 중간청산 Flag가 set 되어 있는지 확인하여 중간청산이 아닐 때만 매도 실행



                                Dim 발생인덱스 As Integer = 마지막신호Index계산하기()
                                If 발생인덱스 > 0 And currentIndex_순매수 - 발생인덱스 < 10 Then
                                    추가매수실행(0, 현재신호) '콜을 매수한다
                                End If

                            End If

                        ElseIf 현재신호 < 0 Then

                            If is중간청산Flag(1) = False Then '중간 청산 후 다시 사는 걸 방지하기 위해 현재 중간청산 중인지 확인하는 함수. 현재 신호가 살아있고 중간청산 Flag가 set 되어 있는지 확인하여 중간청산이 아닐 때만 매도 실행

                                Dim 발생인덱스 As Integer = 마지막신호Index계산하기()

                                If 발생인덱스 > 0 And currentIndex_순매수 - 발생인덱스 < 10 Then
                                    추가매수실행(1, 현재신호) '풋을 매수한다
                                End If

                            End If

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
            If Math.Abs(투자그레이드) = 1 Then 진짜최종투자금액 = 진짜최종투자금액 * 0.9

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

            Dim 계산count As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)
            If price < 0.8 Then
                계산count = Math.Max(CInt(-250 * price + 250), 계산count)
            End If

            count = Math.Min(count, 계산count)
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

                일분옵션데이터(callput).이동평균선(j) = 이동평균선값계산(이동평균선_기준일자, callput, j) '0 값이 들어오는 경우가 많아서 real 에서는 전부 다 계산한다
            Next
        Next

    End Sub


    ''' <summary>
    ''' M 알고리즘용
    ''' </summary>

    Public Sub CalcMACD이동평균Data()   '이동평균선을 계산하여 일분데이터에 추가한다

        For callput As Integer = 0 To 1

            For i As Integer = 0 To MA_Interval.Length - 1

                Dim 기준일자 As Integer = MA_Interval(i)

                For j As Integer = 기준일자 - 1 To currentIndex_1MIn

                    If currentIndex_1MIn < 기준일자 Then Continue For

                    일분옵션데이터(callput).MA(i, j) = 이동평균선값계산(기준일자, callput, j)


                Next
            Next
        Next

    End Sub

    Public Sub CalcMACD계산치Data()   'MACD 이동평균선을 이용하셔 MACD 값 등을 계산한다


        Dim 기본장기_26 As Integer = MA_Interval(1)
        Dim 팔때장기_39 As Integer = MA_Interval(4)
        Dim 추세이평선_50 As Integer = MA_Interval(2)


        For callput As Integer = 0 To 1


            'MACD선 12일선 - 26일선

            For j As Integer = 0 To currentIndex_1MIn

                If 일분옵션데이터(callput).MA(1, j) > 0 And j >= 기본장기_26 Then
                    일분옵션데이터(callput).CA_기본(0, j) = 일분옵션데이터(callput).MA(0, j) - 일분옵션데이터(callput).MA(1, j)  '12일선 - 26일선

                End If


                If 일분옵션데이터(callput).MA(4, j) > 0 And j >= 팔때장기_39 Then
                    일분옵션데이터(callput).CA_팔때(0, j) = 일분옵션데이터(callput).MA(3, j) - 일분옵션데이터(callput).MA(4, j)  '19일선 - 39일선
                End If
            Next


            '시그널선 : CA_기본이나 CA_팔때 값의 9일이동평균선

            For j As Integer = 0 To currentIndex_1MIn

                If j >= 기본장기_26 + 9 Then   '9일치 이동평균선이기 때문에 CA_기본이 9개가 쌓일 때부터 계산한다
                    일분옵션데이터(callput).CA_기본(1, j) = MACD_신호선_이동평균선값계산(9, callput, j, True)
                End If


                If j >= 팔때장기_39 + 9 Then   '9일치 이동평균선이기 때문에 CA_기본이 9개가 쌓일 때부터 계산한다
                    일분옵션데이터(callput).CA_팔때(1, j) = MACD_신호선_이동평균선값계산(9, callput, j, False)
                End If
            Next

            '오실레이터선 : MACD - Signal선

            For j As Integer = 0 To currentIndex_1MIn

                If j >= 기본장기_26 + 9 Then   '9일치 이동평균선이기 때문에 CA_기본이 9개가 쌓일 때부터 계산한다
                    일분옵션데이터(callput).CA_기본(2, j) = 일분옵션데이터(callput).CA_기본(0, j) - 일분옵션데이터(callput).CA_기본(1, j)
                End If


                If j >= 팔때장기_39 + 9 Then   '9일치 이동평균선이기 때문에 CA_기본이 9개가 쌓일 때부터 계산한다
                    일분옵션데이터(callput).CA_팔때(2, j) = 일분옵션데이터(callput).CA_팔때(0, j) - 일분옵션데이터(callput).CA_팔때(1, j)
                End If
            Next

            'MACD_Result 계산
            '0: 단순히 MACD값이 0보다 크다 / 작다   - 콜_풋 / 시간대별
            '1: MACD > 시그널 
            '2: 장기이평선 위/아래
            '3: 팔때 MACD > 시그널

            For j As Integer = 0 To currentIndex_1MIn

                If 일분옵션데이터(callput).MA(1, j) > 0 And j >= 추세이평선_50 Then


                    If 일분옵션데이터(callput).CA_기본(0, j) > 0 Then  '0: 단순히 MACD값이 0보다 크다 / 작다   - 콜_풋 / 시간대별
                        일분옵션데이터(callput).MACD_Result(0, j) = 1
                    ElseIf 일분옵션데이터(callput).CA_기본(0, j) < 0 Then
                        일분옵션데이터(callput).MACD_Result(0, j) = -1
                    End If

                    If 일분옵션데이터(callput).CA_기본(0, j) > 일분옵션데이터(callput).CA_기본(1, j) Then '1: MACD > 시그널 
                        일분옵션데이터(callput).MACD_Result(1, j) = 1
                    ElseIf 일분옵션데이터(callput).CA_기본(0, j) < 일분옵션데이터(callput).CA_기본(1, j) Then
                        일분옵션데이터(callput).MACD_Result(1, j) = -1
                    End If


                    If isRealFlag = True And 당일반복중_flag = False Then
                        If 일분옵션데이터(callput).price(j, 3) > 일분옵션데이터(callput).MA(2, j) Then '2: 장기이평선 위/아래
                            일분옵션데이터(callput).MACD_Result(2, j) = 1
                        ElseIf 일분옵션데이터(callput).price(j, 3) < 일분옵션데이터(callput).MA(2, j) Then
                            일분옵션데이터(callput).MACD_Result(2, j) = -1
                        End If
                    Else    ' 분석중일때는 고가만이라도 넘으면 넘은걸로 간주함
                        If 일분옵션데이터(callput).price(j, 1) > 일분옵션데이터(callput).MA(2, j) Then '2: 장기이평선 위/아래
                            일분옵션데이터(callput).MACD_Result(2, j) = 1
                        ElseIf 일분옵션데이터(callput).price(j, 3) < 일분옵션데이터(callput).MA(2, j) Then
                            일분옵션데이터(callput).MACD_Result(2, j) = -1
                        End If
                    End If




                    If 일분옵션데이터(callput).CA_팔때(0, j) > 일분옵션데이터(callput).CA_팔때(1, j) Then '3: 팔때 MACD > 시그널
                        일분옵션데이터(callput).MACD_Result(3, j) = 1
                    ElseIf 일분옵션데이터(callput).CA_팔때(0, j) < 일분옵션데이터(callput).CA_팔때(1, j) Then
                        일분옵션데이터(callput).MACD_Result(3, j) = -1
                    End If

                End If
            Next
        Next


    End Sub


    ' F알고리즘 시작

    Public Sub CalcAlgorithm_F(ByVal 일분옵션데이터_CurrentIndex As Integer) '역헤드엔숄더발생 

        ReDim F_PoinIndexList(1)

        'If Val(일분옵션데이터(0).ctime(currentIndex_1MIn)) < 930 Or Val(일분옵션데이터(0).ctime(currentIndex_1MIn)) > 1510 Then Return
        Dim curTime As Integer = Val(순매수리스트(currentIndex_순매수).sTime)

        If (curTime >= F_첫번째시작시간 And curTime <= F_첫번째종료시간) Or (curTime >= F_두번째시작시간 And curTime <= F_두번째종료시간) Then

            'Add_Log("test", curTime.ToString())
            For callput As Integer = 0 To 1

                Dim 손절기준점 As Single = CalcPIPData_F(callput, 일분옵션데이터_CurrentIndex)
                If 손절기준점 > 0 Then

                    If is동일신호가현재살아있나("F", callput) = False Then

                        Dim str As String = String.Format("F 신호 발생 콜풋 : {0} 방향", callput)
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("F", callput)
                        shinho.A11_손절기준가격 = 손절기준점
                        SoonMesuShinhoList.Add(shinho)

                    End If

                End If

            Next
        End If

    End Sub


    Public Function CalcPIPData_F(ByVal callput As Integer, ByVal 일분옵션데이터_CurrentIndex As Integer) As Single         '대표선 계산

        If 일분옵션데이터_CurrentIndex > F_PIP_최소카운트 Then


            For PIP길이 As Integer = F_PIP_최소카운트 To F_PIP_최대카운트 Step 2

                F_PoinIndexList(callput) = PIP_PD(일분옵션데이터_CurrentIndex, F_PIP_포인트수, callput, PIP길이)

                If F_PoinIndexList(callput).Count = F_PIP_포인트수 Then '같을 때만 체크함

                    Dim bUp_and_down As Boolean = Check_UP_AND_DOWN(F_PoinIndexList(callput), callput)

                    If bUp_and_down = True Then

                        Dim 최고낮은점 As Single = Check역헤드엔숄더(F_PoinIndexList(callput), callput)  '중간골짜기가 제일 깊은 골짜기이고 좌우 골짜기가 중간골짜기 대비 같거나 살짝 높은지 확인한다

                        If 최고낮은점 > 0 Then
                            'Str = String.Format("일분옵션데이터_CurrentIndex_{0}__Callput_{1}___PIP길이_{2} 발생", 일분옵션데이터_CurrentIndex, callput, PIP길이)
                            'Add_Log("UP_DOWN", Str)
                            Return 최고낮은점
                        End If


                    End If
                End If

            Next
        End If

        Return -1.0

    End Function



    '하상하상하상 확인하는 함수 - 만약 그렇다면 true를 리턴한다
    Private Function Check_UP_AND_DOWN(ByVal list As List(Of Integer), ByVal callput As Integer) As Boolean

        Dim ret As Boolean = True

        If 일분옵션데이터(callput).price(list(0), 3) < 0.2 Then Return False
        If 일분옵션데이터(callput).price(list(0), 3) < 일분옵션데이터(callput).price(list(1), 3) * F_기본계곡최소깊이 Then ret = False
        If 일분옵션데이터(callput).price(list(2), 3) < 일분옵션데이터(callput).price(list(1), 3) * F_기본계곡최소깊이 Then ret = False
        If 일분옵션데이터(callput).price(list(2), 3) < 일분옵션데이터(callput).price(list(3), 3) * F_기본계곡최소깊이 Then ret = False   '3번이 제일 낮아야 하는 점임
        If 일분옵션데이터(callput).price(list(4), 3) < 일분옵션데이터(callput).price(list(3), 3) * F_기본계곡최소깊이 Then ret = False
        If 일분옵션데이터(callput).price(list(4), 3) < 일분옵션데이터(callput).price(list(5), 3) * F_기본계곡최소깊이 Then ret = False
        If 일분옵션데이터(callput).price(list(6), 3) < 일분옵션데이터(callput).price(list(5), 3) * F_현재점의최소높이 Then ret = False
        If 일분옵션데이터(callput).price(list(6), 3) > 일분옵션데이터(callput).price(list(5), 3) * F_현재점의최대높이 Then ret = False

        'If 일분옵션데이터(callput).price(list(1), 3) > 일분옵션데이터(callput).price(list(5), 3) Then ret = False  '고점 2개 중 뒤의 점이 더 높아야 한다  -- 큰 의미 없음 승률이 비슷
        'If 일분옵션데이터(callput).price(list(2), 3) > 일분옵션데이터(callput).price(list(4), 3) Then ret = False  '저점 2개 중 뒤의 점이 더 높아야 한다  -- 더 안좋음

        Return ret

    End Function

    '중간골짜기가 제일 깊은 골짜기이고 좌우 골짜기가 중간골짜기 대비 같거나 살짝 높은지 확인한다
    Private Function Check역헤드엔숄더(ByVal list As List(Of Integer), ByVal callput As Integer) As Single

        Dim ret As Single = -1.0

        Dim 중간골짜기값 As Single = 일분옵션데이터(callput).price(list(3), 3)
        Dim 왼쪽골짜기값 As Single = 일분옵션데이터(callput).price(list(1), 3)
        Dim 오른쪽골짜기값 As Single = 일분옵션데이터(callput).price(list(5), 3)

        Dim 좌측높이 As Single = 왼쪽골짜기값 / 중간골짜기값
        Dim 우측높이 As Single = 오른쪽골짜기값 / 중간골짜기값


        If 좌측높이 >= F_좌골짜기깊이하한 And 좌측높이 <= F_좌우골짜기깊이상한 And 우측높이 >= F_우골짜기깊이하한 And 우측높이 <= F_좌우골짜기깊이상한 Then

            Dim min As Single = Math.Min(왼쪽골짜기값, 오른쪽골짜기값)
            min = Math.Min(min, 중간골짜기값)
            ret = min      '제일 낮은 값을 리턴함

        End If


        '최근 X틱 중 최저점보다 Y배 이내인지 검사해서 그럴 때만 신호를 발생시킨다  -- 이러면 반대쪽 중복 이슈가 많이 줄어들 듯

        Dim startIndex As Integer = Math.Max(list(0) - F_최저점근접기간Index, 0)
        Dim endIndex As Integer = Math.Max(list(0) - 1, 0)
        Dim 이전최저값 As Single = Single.MaxValue
        For i As Integer = startIndex To endIndex
            If 이전최저값 > 일분옵션데이터(callput).price(i, 3) Then
                이전최저값 = 일분옵션데이터(callput).price(i, 3)
            End If
        Next

        If ret < 이전최저값 * F_최저점대비높은비율 Then
            Return ret
        Else
            Return -1
        End If



    End Function

    Public Function PIP_PD(ByVal LastIndex As Integer, ByVal n As Integer, ByVal callput As Integer, ByVal PIP길이 As Integer) As List(Of Integer)

        LastIndex = Math.Min(LastIndex, 380) '380번째 인덱스가 1520분이다 항상 이때까지만 계산한다

        Dim pipData(n) As Double
        Dim pipIndexList As List(Of Integer) = New List(Of Integer)
        Dim startIndex As Integer = F_PIP_최소카운트
        If PIP길이 > 0 Then startIndex = Math.Max(LastIndex - PIP길이, F_PIP_최소카운트)

        pipIndexList.Add(startIndex)  '첫점
        pipIndexList.Add(LastIndex) 'PIP 2 '마지막점

        For pipCount As Integer = 2 To n - 1

            Dim RightPipPoint = 1
            Dim leftPipIndex = pipIndexList(RightPipPoint - 1)
            Dim RightPipIndex = pipIndexList(RightPipPoint)

            Dim maxDistance As Double = Double.MinValue
            Dim maxDistanceIndex As Integer = 0

            For i As Integer = startIndex To LastIndex

                If RightPipIndex = i And i < (LastIndex) Then

                    RightPipPoint += 1
                    leftPipIndex = RightPipIndex
                    RightPipIndex = pipIndexList(RightPipPoint)

                Else '거리측정

                    Dim distance As Double = PerpendichalrDistance(leftPipIndex, 일분옵션데이터(callput).price(leftPipIndex, 3), RightPipIndex, 일분옵션데이터(callput).price(RightPipIndex, 3), i, 일분옵션데이터(callput).price(i, 3))

                    If distance > maxDistance Then
                        maxDistance = distance
                        maxDistanceIndex = i
                    End If
                End If
            Next

            For i As Integer = 0 To pipIndexList.Count - 1

                If pipIndexList(i) > maxDistanceIndex Then
                    pipIndexList.Insert(i, maxDistanceIndex)
                    Exit For
                End If

            Next
        Next

        Return pipIndexList
    End Function

    Private Function PerpendichalrDistance(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal x3 As Double, ByVal y3 As Double) As Double

        Dim s As Double = (y2 - y1) / (x2 - x1)
        Return Math.Abs(s * x3 - y3 + y1 - s * x1) / Math.Sqrt(s * s + 1)
    End Function


    'G 알고리즘 시작
    'G 알고리즘 변경 - RSI로 변경함

    'G 알고리즘 - 연속 음봉 후 양봉 출현
    Public G_첫번째시작시간 As Integer = 1430   '시작시간
    Public G_첫번째종료시간 As Integer = 1500   '종료시간
    Public G_최대유지기간Index As Integer = 60    '20분이 지나면 무조건 매도  - 순매수 인덱스가 기준이라서 2배가 된다
    Public G_RSI최저기준 As Single = 0.25           'RSI가 해당 기준 이하이면
    Public G_양봉막대크기 As Single = 1.06      '직전 틱의 양봉 크기

    '202402 test 결과 :  B240211_T002 --------- C_TEST_CNT_019_A_1430_B_1500_C_60_D_0.25_E_1.06     5승2패로 아직 데이터가 작음


    Public Sub CalcAlgorithm_G(ByVal 일분옵션데이터_CurrentIndex As Integer) '음봉연속 발생


        Dim curTime As Integer = Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex))

        If (curTime >= G_첫번째시작시간 And curTime <= G_첫번째종료시간) Then

            For callput As Integer = 0 To 1

                If 일분옵션데이터(callput).price(일분옵션데이터_CurrentIndex - 1, 3) < 0.2 Then Continue For

                Dim 양봉크기 As Single = 일분옵션데이터(callput).price(일분옵션데이터_CurrentIndex - 1, 3) / 일분옵션데이터(callput).price(일분옵션데이터_CurrentIndex - 1, 0)
                If 일분옵션데이터(callput).price(일분옵션데이터_CurrentIndex - 1, 0) < 일분옵션데이터(callput).price(일분옵션데이터_CurrentIndex - 1, 3) And 양봉크기 > G_양봉막대크기 Then  '직전이 양봉이고 양봉의 크기 비율이 일정 기준 이상이면 

                    '여기다 이동평균선 위에서만 매수하는 거 추가

                    '여기다 RSI 기준 추가
                    If 일분옵션데이터(callput).RSI(일분옵션데이터_CurrentIndex - 1) <= G_RSI최저기준 Then

                        If is동일신호가현재살아있나("G", callput) = False Then
                            Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("G", callput)
                            SoonMesuShinhoList.Add(shinho)
                        End If

                    End If

                End If

            Next

        End If

    End Sub

    Private Function 살아있는신호확인하기_G(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then             'G알고리즘 특화


            'G알고리즘 특화 - Index limit  추가
            Dim 경과Index As Integer = currentIndex_순매수 - s.A01_발생Index
            If 경과Index > G_최대유지기간Index And s.A03_신호ID = "G" Then

                매도사유 = "indexlimit"

                If isRealFlag = False Then
                    s.A14_현재가격 = Math.Round(일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0), 2)  '해당 틱의 시가로 조정
                    s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                End If
            End If


        End If

        Return 매도사유

    End Function

    ' MovingAverage : Call, put, 종합주가지수 3가지를 다 하나의 배열에 저장한다
    '0: 단기 (12일)
    '1: 장기 (26일)
    '2: 추세이평선(50일)
    '3: 팔때 단기 19일
    '4: 팔 때 장기 39일

    '계산치 : Call, put, 종합주가지수 3가지를 다 하나의 배열에 저장한다
    '0: MACD선 
    '1: 시그널선 (MACD의 9일)
    '2: Oscillator
    '3: 팔때 MACD
    '4: 팔때 MACD 시그널
    '5: 팔때 MACD Oscillator

    'MACD_Result
    '0: 단순히 MACD값이 0보다 크다 / 작다   - 콜_풋 / 시간대별
    '1: MACD > 시그널 
    '2: 장기이평선 위/아래
    '3: 팔때 MACD > 시그널


    '20240211 테스트 결과 'C_TEST_CNT_020_A_0.005_B_58_C_0.013_D_1215
    Public 기울기최저기준 As Single = 0.005  '기울기가 일정 기준 이상일때만 사도록 하는 기능임. 참고로 2023년 9월부터 12월까지 평균은 0.01, 최대값은 0.059 였음   - 최소 확인 23.09.03 ! 12.22   - 0,3일 대상
    Public 기울기최고기준 As Single = 0.013  '기울기가 일정 기준 이상일때만 사도록 하는 기능임. 참고로 2023년 9월부터 12월까지 평균은 0.01, 최대값은 0.059 였음   - 최대 확인 23.09.03 ! 12.22   - 0,3일 대상
    Public M_마감시간 As Integer = 1500



    Public Sub CalcAlgorithm_M(ByVal 일분옵션데이터_CurrentIndex As Integer) 'MACD 활용 1  - 단순히 MACD값이 0보다 클  때 사고 0보다 작을때 판다

        max_interval = MA_Interval(2) '전역변수 max_interval에 값을 넣어 놓는다

        If 일분옵션데이터_CurrentIndex < max_interval Then Return  '추세선이 아직  안 만들어졌으면 빠진다

        'If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False And Val(순매수리스트(currentIndex_순매수).sTime) >= 121500 Then Return  '아래의 마감시간이 자꾸 오동작하여 추가함

        If Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) > M_마감시간 Or Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) < N_시작시간 Then Return

        Dim Index As Integer = 일분옵션데이터_CurrentIndex - 1

        For i As Integer = 0 To 1
            If is동일신호가현재살아있나("M", i) Then Continue For
            If is동일신호가현재살아있나("N", i) Then Continue For

            If 일분옵션데이터(i).price(Index, 3) < 0.2 Then Continue For '0.2보다 작으면 신호를 만들지 않는다


            'If 일분옵션데이터(i).MACD_Result(0, Index - 1) < 0 And 일분옵션데이터(i).MACD_Result(0, Index) > 0 Then   '단순히 0보다 작았다가 커질때 신호가 발생하는 거 
            If 일분옵션데이터(i).MACD_Result(0, Index - 1) < 0 And 일분옵션데이터(i).MACD_Result(0, Index) > 0 And 일분옵션데이터((i + 1) Mod 2).MACD_Result(0, Index) < 0 Then   '단순히 0보다 작았다가 커질때 신호가 발생할 때 반대쪽은 이미 0보다 작아야 함

                '기울기 계산
                Dim 기울기 As Single = Math.Round(일분옵션데이터(i).CA_기본(0, Index) - 일분옵션데이터(i).CA_기본(0, Index - 1), 4)

                If 기울기 > 기울기최저기준 And 기울기 < 기울기최고기준 Then

                    If 일분옵션데이터(i).MACD_Result(2, Index) > 0 Then  ' 장기 이동평균선 위에 있을 때만 신호

                        Dim 선물기울기 As Integer = 틱당기울기계산(3, O_tick_count_기준)
                        Dim 현물기울기 As Integer = 틱당기울기계산(1, O_tick_count_기준)
                        Dim 동일방향 As Integer = 선물기울기 * 현물기울기
                        Dim 선물기울기_절대치 As Integer = Math.Abs(선물기울기)

                        'If 동일방향 <= 0 Then Continue For

                        If i = 0 And (선물기울기 < 0 Or 선물기울기_절대치 < M_선물기울기_기준) Then Continue For
                        If i = 1 And (선물기울기 > 0 Or 선물기울기_절대치 < M_선물기울기_기준) Then Continue For

                        Dim 남은날짜 As Integer = getRemainDate(sMonth, Val(순매수리스트(currentIndex_순매수).sDate)) Mod 7
                        Dim log_str As String = String.Format("콜풋:{0}:인덱스:{1}:남은날짜:{2}:기울기:{3}:발생일자:{4}", i, Index, 남은날짜, 기울기, 순매수리스트(currentIndex_순매수).sDate)
                        'Add_Log("M신호:", log_str)


                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("M", i)
                        SoonMesuShinhoList.Add(shinho)

                    End If

                End If

            End If

        Next

    End Sub




    Private Function 살아있는신호확인하기_M(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then

            'MACD 값이 0 밑으로 떨어지면 매도
            Dim Index As Integer = 일분옵션데이터_CurrentIndex - 1
            Dim callput = s.A08_콜풋

            If 일분옵션데이터(callput).MACD_Result(0, Index - 1) > 0 And 일분옵션데이터(callput).MACD_Result(0, Index) < 0 Then

                매도사유 = "M_below_0"
                If isRealFlag = False Then
                    s.A14_현재가격 = Math.Round(일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0), 2)  '해당 틱의 시가로 조정

                End If
                s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
                s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)

            End If

        End If

        Return 매도사유

    End Function


    Public N_기울기최저기준 As Single = 0.003  '기울기가 일정 기준 이상일때만 사도록 하는 기능임. 참고로 2023년 9월부터 12월까지 평균은 0.01, 최대값은 0.059 였음   - 최소 확인 23.09.03 ! 12.22   - 0,3일 대상
    Public N_기울기최고기준 As Single = 0.015  '240312 확인해 보니 007로 하니 한달동안 한번도 안사져서 0.009로 변경함

    Public N_마감시간 As Integer = 1230
    Public N_시작시간 As Integer = 1000

    Public M_선물기울기_기준 As Integer = 13


    '20240211 테스트 결과 C_TEST_CNT_012_A_0.005_B_58_C_0.007_D_1230

    Public Sub CalcAlgorithm_N(ByVal 일분옵션데이터_CurrentIndex As Integer) 'MACD 활용 2  - 상향돌파

        max_interval = MA_Interval(2) '전역변수 max_interval에 값을 넣어 놓는다

        If 일분옵션데이터_CurrentIndex < max_interval Then Return  '추세선이 아직  안 만들어졌으면 빠진다

        'If EBESTisConntected = True And currentIndex_1MIn >= 0 And 당일반복중_flag = False And Val(순매수리스트(currentIndex_순매수).sTime) >= 123000 Then Return  '아래의 마감시간이 자꾸 오동작하여 추가함
        'If Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) > N_마감시간 Or Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) < N_시작시간 Then Return


        If Val(순매수리스트(currentIndex_순매수).sTime) >= 123000 And Val(순매수리스트(currentIndex_순매수).sTime) <= 143000 Then Return  '12시반부터 14시반까지는 결과가 안좋아서 제외함 231228
        If Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) > N_마감시간 Or Val(일분옵션데이터(0).ctime(일분옵션데이터_CurrentIndex)) < N_시작시간 Then Return



        Dim Index As Integer = 일분옵션데이터_CurrentIndex - 1

        For i As Integer = 0 To 1
            If is동일신호가현재살아있나("N", i) Then Continue For
            If is동일신호가현재살아있나("M", i) Then Continue For

            If 일분옵션데이터(i).price(Index, 3) < 0.2 Then Continue For '0.2보다 작으면 신호를 만들지 않는다


            'If 일분옵션데이터(i).MACD_Result(1, Index - 1) < 0 And 일분옵션데이터(i).MACD_Result(1, Index) > 0 And 일분옵션데이터((i + 1) Mod 2).MACD_Result(1, Index) < 0 Then   '단순히 0보다 작았다가 커질때 신호가 발생할 때 반대쪽은 이미 0보다 작아야 함
            If 일분옵션데이터(i).MACD_Result(1, Index - 1) < 0 And 일분옵션데이터(i).MACD_Result(1, Index) > 0 Then

                '기울기 계산
                Dim 기울기 As Single = Math.Round(일분옵션데이터(i).CA_기본(0, Index) - 일분옵션데이터(i).CA_기본(0, Index - 1), 4)

                If 기울기 > N_기울기최저기준 And 기울기 < N_기울기최고기준 Then

                    If 일분옵션데이터(i).MACD_Result(2, Index) > 0 Then  ' 장기 이동평균선 위에 있을 때만 신호

                        '여기 선물케이스 정리해서 넣을 것

                        Dim 선물기울기 As Integer = 틱당기울기계산(3, O_tick_count_기준)
                        Dim 현물기울기 As Integer = 틱당기울기계산(1, O_tick_count_기준)
                        Dim 동일방향 As Integer = 선물기울기 * 현물기울기
                        Dim 선물기울기_절대치 As Integer = Math.Abs(선물기울기)


                        If 동일방향 <= 0 Then Continue For

                        If i = 0 And (선물기울기 < 0 Or 선물기울기_절대치 < M_선물기울기_기준) Then Continue For
                        If i = 1 And (선물기울기 > 0 Or 선물기울기_절대치 < M_선물기울기_기준) Then Continue For


                        Dim 남은날짜 As Integer = getRemainDate(sMonth, Val(순매수리스트(currentIndex_순매수).sDate)) Mod 7
                        Dim log_str As String = String.Format("콜풋:{0}:인덱스:{1}:남은날짜:{2}:기울기:{3}:발생일자:{4}", i, Index, 남은날짜, 기울기, 순매수리스트(currentIndex_순매수).sDate)
                        'Add_Log("N신호:", log_str)


                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("N", i)

                        SoonMesuShinhoList.Add(shinho)


                        'End If



                    End If

                End If

            End If


        Next

    End Sub



    Private Function 살아있는신호확인하기_N(ByRef s As 순매수신호_탬플릿, ByVal 일분옵션데이터_CurrentIndex As Integer) As String

        Dim 매도사유 As String = ""

        If s.A15_현재상태 = 1 Then

            'MACD 값이 신호선 밑으로 떨어지면 매도
            Dim Index As Integer = 일분옵션데이터_CurrentIndex - 1
            Dim callput = s.A08_콜풋

            Dim 마지막순매수index As Integer = Get마지막순매수Index()
            'If 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then



            If 일분옵션데이터(callput).MACD_Result(3, Index - 1) > 0 And 일분옵션데이터(callput).MACD_Result(3, Index) < 0 Then   ' 팔때 신호로 할 때 가장 결과가 좋았음 이걸로 함 20240115

                Dim 발생조건상태 As Boolean = False
                If 일분옵션데이터(callput).MACD_Result(1, Index - 1) < 0 And 일분옵션데이터(callput).MACD_Result(1, Index) > 0 Then 발생조건상태 = True

                If 발생조건상태 = False Then 매도사유 = "N_below_1"


            End If
            'End If

            If 일분옵션데이터(callput).MACD_Result(0, Index - 1) > 0 And 일분옵션데이터(callput).MACD_Result(0, Index) < 0 Then  '0보다 작아지면 바로 매도하기 추가 실험
                매도사유 = "N_below_0"
            End If


        End If

        If 매도사유 <> "" Then
            If isRealFlag = False Then
                s.A14_현재가격 = Math.Round(일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 0), 2)  '해당 틱의 시가로 조정

            End If
            s.A16_이익률 = Math.Round((s.A14_현재가격 - s.A10_신호발생가격) / s.A10_신호발생가격, 3)
            s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
        End If

        Return 매도사유

    End Function




    Public Sub CalcRSIData()   'RSI를 계산해서 익절할 때 사용한다

        For callput As Integer = 0 To 1



            For j As Integer = RSI_기준일 To currentIndex_1MIn


                일분옵션데이터(callput).RSI(j) = RSI값계산(callput, j) '0 값이 들어오는 경우가 많아서 real 에서는 전부 다 계산한다



            Next
        Next

    End Sub

    Private Function RSI값계산(ByVal callput As Integer, ByVal index As Integer) As Single


        Dim AU As Single = 0
        Dim AD As Single = 0
        Dim RSI As Single = -1

        For i As Integer = 0 To RSI_기준일 - 1  '자기를 포함한 이동평균선기준일자까지 더한다

            If 일분옵션데이터(callput).price(index - i, 3) > 0 Then

                If 일분옵션데이터(callput).price(index - i, 0) <= 일분옵션데이터(callput).price(index - i, 3) Then   '종가가 큰 양봉이면

                    AU = AU + Math.Abs(일분옵션데이터(callput).price(index - i, 3) - 일분옵션데이터(callput).price(index - i, 0))

                Else

                    AD = AD + Math.Abs(일분옵션데이터(callput).price(index - i, 3) - 일분옵션데이터(callput).price(index - i, 0))
                End If


            End If

        Next

        If AU > 0 Then
            RSI = AU / (AU + AD)
        End If

        Return RSI

    End Function

End Module
