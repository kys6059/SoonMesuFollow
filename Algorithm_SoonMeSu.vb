Option Explicit On

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
        Dim A53_선행포인트수_마진 As Single  '--------------------------------- 이까지 38건
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

    Public Sub SoonMesuCalcAlrotithmAll()

        Dim startTime As Integer = Val(Form2.txt_F2_매수시작시간.Text)
        Dim endTime As Integer = Val(Form2.txt_F2_매수마감시간.Text)
        Dim 최초매매시작시간 As Integer = Val(Form2.txt_F2_최초매매시작시간.Text)
        Dim timeoutTime As Integer = Val(Form2.txt_F2_TimeoutTime.Text)
        'If Val(순매수리스트(currentIndex_순매수).sTime) > startTime Then Return  '매수시작시간전에는 아예 신호가 안뜨게 만든다 ---- 이걸 하니깐 켈리지수가 급락함 제외함

        Dim ret As String = CalcAlgorithm_AB()
        If Val(순매수리스트(currentIndex_순매수).sTime) >= startTime Then  '매수시작시간전에는 아예 신호가 안뜨게 만들고 아래 이전순매수방향만 지정한다  '끝나는 시간도 정하니까 신호가 안떠서 반대방향 신호를 죽이지 않는 문제점 발생해서 시작 시간만 체크함
            If Val(순매수리스트(currentIndex_순매수).sTime) < timeoutTime + 500 Then

                '이전순매수와 지금인덱스를 비교하여 5보다 작으면(2분이하) 무시하는 로직 추가
                Dim 마지막순매수index As Integer = Get마지막순매수Index()
                If ret <> "중립" And 마지막순매수index + 신호최소유지시간index < currentIndex_순매수 Then '중립 --> 상승 or 하강이 뜨거나 반대 방향 신호가 뜨면 여기를 진입한다

                    If ret = "상승" Then '중립에서 상승으로 전환 

                        반대방향신호죽이는함수("A_DOWN", "change")                                 '이전 신호확인해서 죽이기
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_UP")               '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("상승신호 발생 AT {0}", shinho.A02_발생시간))
                        Form2.lbl_F2_매매신호.Text = "1"
                        Form2.lbl_F2_매매신호.BackColor = Color.Magenta
                    ElseIf ret = "하락" Then '중립에서 하락으로 전환

                        반대방향신호죽이는함수("A_UP", "change")                                   '이전 신호확인해서 죽이기
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_DOWN")             '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("하락신호 발생 AT {0}", shinho.A02_발생시간))
                        Form2.lbl_F2_매매신호.Text = "-1"
                        Form2.lbl_F2_매매신호.BackColor = Color.Green
                    End If
                    이전순매수방향 = ret '신호가 뜬걸 의미한다 이걸 바꿔 놓는다
                Else
                    살아있는신호확인하기()
                End If
            End If

        Else  '시작시간 전에 1개만 매매 - 31일 중 2건 발생함 2건 다 성공 
            If currentIndex_순매수 > 4 Then

                Dim 시작전신호 = PIP_Point_Lists(0).마지막신호
                Dim 시작전기울기 = Math.Abs(PIP_Point_Lists(0).마지막선기울기)
                If SoonMesuShinhoList.Count < 1 Then
                    If Val(순매수리스트(currentIndex_순매수).sTime) > 최초매매시작시간 And 시작전기울기 > 시작전허용기울기 Then   '최초매매시작시간보다 클 때 방향이 생기고 신호가 하나도 없는 상태일 때 - 기울기조건 추가

                        If 시작전신호 = "상승" Then
                            Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_UP")               '신규 신호 입력하기
                            SoonMesuShinhoList.Add(shinho)
                            If EBESTisConntected = True Then Add_Log("신호", String.Format("최초 상승신호 발생 AT {0}", shinho.A02_발생시간))
                            Form2.lbl_F2_매매신호.Text = "1"
                            Form2.lbl_F2_매매신호.BackColor = Color.Magenta
                            이전순매수방향 = 시작전신호
                        ElseIf 시작전신호 = "하락" Then
                            Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_DOWN")             '신규 신호 입력하기
                            SoonMesuShinhoList.Add(shinho)
                            If EBESTisConntected = True Then Add_Log("신호", String.Format("최초 하락신호 발생 AT {0}", shinho.A02_발생시간))
                            Form2.lbl_F2_매매신호.Text = "-1"
                            Form2.lbl_F2_매매신호.BackColor = Color.Green
                            이전순매수방향 = 시작전신호
                        End If
                    End If

                Else
                    살아있는신호확인하기()
                End If
            End If

        End If

        If currentIndex_순매수 > 500 Then
            If Val(순매수리스트(currentIndex_순매수).sTime) > 151600 Then 전체잔고정리하기()
        End If

    End Sub



    Public Sub SoonMesuCalcAlrotithmAll_old()

        Dim startTime As Integer = Val(Form2.txt_F2_매수시작시간.Text)
        Dim endTime As Integer = Val(Form2.txt_F2_매수마감시간.Text)
        Dim 최초매매시작시간 As Integer = Val(Form2.txt_F2_최초매매시작시간.Text)
        Dim timeoutTime As Integer = Val(Form2.txt_F2_TimeoutTime.Text)
        'If Val(순매수리스트(currentIndex_순매수).sTime) > startTime Then Return  '매수시작시간전에는 아예 신호가 안뜨게 만든다 ---- 이걸 하니깐 켈리지수가 급락함 제외함

        Dim ret As String = CalcAlgorithm_AB()

        If ret <> "중립" Then '중립 --> 상승 or 하강이 뜨거나 반대 방향 신호가 뜨면 여기를 진입한다
            If Val(순매수리스트(currentIndex_순매수).sTime) >= startTime Then  '매수시작시간전에는 아예 신호가 안뜨게 만들고 아래 이전순매수방향만 지정한다  '끝나는 시간도 정하니까 신호가 안떠서 반대방향 신호를 죽이지 않는 문제점 발생해서 시작 시간만 체크함
                If Val(순매수리스트(currentIndex_순매수).sTime) < timeoutTime Then
                    If ret = "상승" Then '중립에서 상승으로 전환 

                        반대방향신호죽이는함수("A_DOWN", "change")                                 '이전 신호확인해서 죽이기
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_UP")               '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("상승신호 발생 AT {0}", shinho.A02_발생시간))
                        Form2.lbl_F2_매매신호.Text = "1"
                        Form2.lbl_F2_매매신호.BackColor = Color.Magenta
                    ElseIf ret = "하락" Then '중립에서 하락으로 전환

                        반대방향신호죽이는함수("A_UP", "change")                                   '이전 신호확인해서 죽이기
                        Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_DOWN")             '신규 신호 입력하기
                        SoonMesuShinhoList.Add(shinho)

                        If EBESTisConntected = True Then Add_Log("신호", String.Format("하락신호 발생 AT {0}", shinho.A02_발생시간))
                        Form2.lbl_F2_매매신호.Text = "-1"
                        Form2.lbl_F2_매매신호.BackColor = Color.Green
                    End If
                End If

            Else  '시작시간 전에 1개나 2매 매매 검토 221007

                    If currentIndex_순매수 > 4 Then
                    If Val(순매수리스트(currentIndex_순매수).sTime) > 최초매매시작시간 Then   '최초매매시작시간보다 클 때 방향이 생기고 신호가 하나도 없는 상태일 때
                        If SoonMesuShinhoList.Count < 1 Then
                            If ret = "상승" Then
                                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_UP")               '신규 신호 입력하기
                                'SoonMesuShinhoList.Add(shinho)
                                If EBESTisConntected = True Then Add_Log("신호", String.Format("최초 상승신호 발생 AT {0}", shinho.A02_발생시간))
                                Form2.lbl_F2_매매신호.Text = "1"
                                Form2.lbl_F2_매매신호.BackColor = Color.Magenta

                            ElseIf ret = "하락" Then
                                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_DOWN")             '신규 신호 입력하기
                                SoonMesuShinhoList.Add(shinho)
                                If EBESTisConntected = True Then Add_Log("신호", String.Format("최초 하락신호 발생 AT {0}", shinho.A02_발생시간))
                                Form2.lbl_F2_매매신호.Text = "-1"
                                Form2.lbl_F2_매매신호.BackColor = Color.Green
                            End If

                        End If
                    End If

                End If

            End If

            이전순매수방향 = ret '신호가 뜬걸 의미한다 이걸 바꿔 놓는다
        Else
            살아있는신호확인하기()
        End If

    End Sub

    Public Function CalcAlgorithm_AB() As String

        Dim ret As String = "중립"

        Dim CurrentIndex_순매수_마지막방향 As String = PIP_Point_Lists(PIP적합포인트인덱스).마지막신호

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

    Private Function MakeSoonMesuShinho(ByVal 신호ID As String) As 순매수신호_탬플릿

        Dim shinho As 순매수신호_탬플릿 = New 순매수신호_탬플릿

        shinho.A00_신호차수 = SoonMesuShinhoList.Count + 1
        shinho.A01_발생Index = currentIndex_순매수
        shinho.A02_발생시간 = 순매수리스트(currentIndex_순매수).sTime
        shinho.A03_신호ID = 신호ID

        shinho.A04_신호발생순매수 = Get순매수(currentIndex_순매수)
        shinho.A05_신호해제순매수 = 0
        shinho.A06_신호발생종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수
        shinho.A07_신호해제종합주가지수 = 0

        shinho.A09_행사가 = 0
        shinho.A10_신호발생가격 = 0

        shinho.A15_현재상태 = 1
        shinho.A16_이익률 = 0

        shinho.A21_환산이익율 = 0

        shinho.A50_조건전체 = SoonMesuSimulation_조건
        shinho.A51_순매수기준 = Form2.cmb_F2_순매수기준.Text
        shinho.A52_기울기 = Form2.txt_F2_상승하락기울기기준.Text
        shinho.A53_선행포인트수_마진 = Form2.txt_선행_포인트_마진.Text
        shinho.A54_IsReal = 0
        shinho.A56_기준가격 = Form2.txt_JongmokTargetPrice.Text
        shinho.A57_월물 = sMonth
        shinho.A58_날짜 = 순매수리스트(currentIndex_순매수).sDate
        shinho.A59_남은날짜 = getRemainDate(sMonth, Val(shinho.A58_날짜))
        shinho.A60_손절기준차 = Form2.txt_F2_손절매차.Text
        shinho.A61_익절기준차 = Form2.txt_F2_익절차.Text
        shinho.A62_TimeoutTime = Form2.txt_F2_TimeoutTime.Text

        '콜풋종목정보 - 매수의 경우
        '        If 신호ID = "A_UP" Then
        '       shinho.A08_콜풋 = 0
        '      ElseIf 신호ID = "A_DOWN" Then
        '     shinho.A08_콜풋 = 1
        '    End If
        '   shinho.A09_행사가 = 일분옵션데이터(shinho.A08_콜풋).HangSaGa
        '
        'Dim 일분옵션데이터_CurrentIndex As Integer = 순매수시간으로1MIN인덱스찾기(Val(shinho.A02_발생시간))
        'shinho.A10_신호발생가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
        'shinho.A14_현재가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
        'shinho.A16_이익률 = Math.Round(shinho.A14_현재가격 / shinho.A10_신호발생가격, 3)


        '콜풋종목정보 - 매도의 경우
        If 신호ID = "A_UP" Then
            shinho.A08_콜풋 = 1
        ElseIf 신호ID = "A_DOWN" Then
            shinho.A08_콜풋 = 0
        End If
        shinho.A09_행사가 = 일분옵션데이터(shinho.A08_콜풋).HangSaGa

        Dim 일분옵션데이터_CurrentIndex As Integer = 순매수시간으로1MIN인덱스찾기(Val(shinho.A02_발생시간))
        If 일분옵션데이터_CurrentIndex >= 0 Then
            shinho.A10_신호발생가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
            shinho.A14_현재가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
            shinho.A16_이익률 = Math.Round((shinho.A10_신호발생가격 - shinho.A14_현재가격) / shinho.A10_신호발생가격, 3)
        End If

        shinho.B00_etc = Form2.txt_F2_실험조건.Text

        Return shinho

    End Function

    Private Sub 반대방향신호죽이는함수(ByVal str As String, ByVal 매도사유 As String)

        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A03_신호ID = str And s.A15_현재상태 = 1 Then  '죽어야하는 신호이고 현재상태가 살아있는 상태라면

                    s.A05_신호해제순매수 = Get순매수(currentIndex_순매수)
                    s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수

                    s.A15_현재상태 = 0

                    s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
                    s.A19_매도Index = currentIndex_순매수
                    s.A20_매도사유 = 매도사유

                    '매도의 경우
                    Dim 일분옵션데이터_CurrentIndex As Integer = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
                    If 일분옵션데이터_CurrentIndex >= 0 Then s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                    s.A16_이익률 = Math.Round((s.A10_신호발생가격 - s.A14_현재가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)
                    s.A22_신호해제가격 = s.A14_현재가격

                    If str = "A_UP" Then
                        s.A55_메모 = Math.Round(s.A07_신호해제종합주가지수 - s.A06_신호발생종합주가지수, 2)
                    ElseIf str = "A_DOWN" Then
                        s.A55_메모 = Math.Round(s.A06_신호발생종합주가지수 - s.A07_신호해제종합주가지수, 2)
                    End If
                    SoonMesuShinhoList(i) = s
                End If
            Next
        End If
    End Sub

    Private Sub 살아있는신호확인하기()
        If SoonMesuShinhoList IsNot Nothing Then

            For i As Integer = 0 To SoonMesuShinhoList.Count - 1

                Dim s As 순매수신호_탬플릿 = SoonMesuShinhoList(i)
                If s.A15_현재상태 = 1 Then

                    Dim 종합주가지수 As Single = 순매수리스트(currentIndex_순매수).코스피지수
                    Dim 매도사유 As String = ""

                    '손절조건 확인
                    If s.A03_신호ID = "A_UP" And s.A06_신호발생종합주가지수 - 종합주가지수 > s.A60_손절기준차 Then 매도사유 = "son"
                    If s.A03_신호ID = "A_DOWN" And 종합주가지수 - s.A06_신호발생종합주가지수 > s.A60_손절기준차 Then 매도사유 = "son"
                    '익절조건 확인
                    If s.A03_신호ID = "A_UP" And 종합주가지수 - s.A06_신호발생종합주가지수 > s.A61_익절기준차 Then 매도사유 = "ik"
                    If s.A03_신호ID = "A_DOWN" And s.A06_신호발생종합주가지수 - 종합주가지수 > s.A61_익절기준차 Then 매도사유 = "ik"
                    'timeout 확인
                    If Val(순매수리스트(currentIndex_순매수).sTime) >= Val(s.A62_TimeoutTime) Then 매도사유 = "timeout"

                    '반대방향기울기일정시간유지 시 reverse로 매도  '-- 이로직을 적용해도 켈리지수가 30이하라서 불필요함 삭제
                    'Dim 기울기 As Double = PIP_Point_Lists(PIP적합포인트인덱스).마지막선기울기
                    'If PIP_Point_Lists(PIP적합포인트인덱스).마지막선거리합 > Val(Form2.txt_F2_마지막선길이.Text) Then  '마지막선의 길이가 기준 이상이고
                    'If s.A03_신호ID = "A_UP" And Math.Abs(기울기) > Val(Form2.txt_F2_반대방향처리기울기.Text) And 기울기 < 0 Then   '기울기의 절대값이 기준이상이고 반대방향이고
                    '매도사유 = "RERVERSE"
                    'ElseIf s.A03_신호ID = "A_DOWN" And Math.Abs(기울기) > Val(Form2.txt_F2_반대방향처리기울기.Text) And 기울기 > 0 Then   '기울기의 절대값이 기준이상이고 반대방향이고
                    '   매도사유 = "RERVERSE"
                    'End If
                    'End If

                    If s.A03_신호ID = "A_UP" Then
                        s.A55_메모 = Math.Round(종합주가지수 - s.A06_신호발생종합주가지수, 2)
                    ElseIf s.A03_신호ID = "A_DOWN" Then
                        s.A55_메모 = Math.Round(s.A06_신호발생종합주가지수 - 종합주가지수, 2)
                    End If

                    '매수의 경우
                    'Dim 일분옵션데이터_CurrentIndex As Integer = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
                    's.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                    's.A16_이익률 = Math.Round(s.A14_현재가격 / s.A10_신호발생가격, 3)
                    's.A21_환산이익율 = Math.Round(s.A16_이익률 - 1.02, 3)

                    '매도의 경우

                    Dim 일분옵션데이터_CurrentIndex As Integer
                    If EBESTisConntected = True And currentIndex_1MIn >= 0 Then
                        일분옵션데이터_CurrentIndex = currentIndex_1MIn
                    Else
                        일분옵션데이터_CurrentIndex = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
                    End If
                    If 일분옵션데이터_CurrentIndex >= 0 Then s.A14_현재가격 = 일분옵션데이터(s.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
                    s.A16_이익률 = Math.Round((s.A10_신호발생가격 - s.A14_현재가격) / s.A10_신호발생가격, 3)
                    s.A21_환산이익율 = Math.Round(s.A16_이익률 - 0.02, 3)


                    If 매도사유 <> "" Then
                        s.A05_신호해제순매수 = Get순매수(currentIndex_순매수)
                        s.A07_신호해제종합주가지수 = 순매수리스트(currentIndex_순매수).코스피지수

                        s.A15_현재상태 = 0
                        s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
                        s.A19_매도Index = currentIndex_순매수
                        s.A20_매도사유 = 매도사유
                        s.A22_신호해제가격 = s.A14_현재가격
                        Form2.lbl_F2_매매신호.Text = "0"  '해제가 되면 타이머로 돌면서 체크해서 매매신호와 잔고를 비교해서 다르면 청산한다  -- 이건 시간에 관계없이 해야 함
                        Form2.lbl_F2_매매신호.BackColor = Color.White
                        If EBESTisConntected = True Then Add_Log("신호", String.Format("해제신호 발생 AT {0}, 사유 = {1}", s.A18_매도시간, 매도사유))
                    End If

                    SoonMesuShinhoList(i) = s

                End If
            Next
        End If
    End Sub

    Public Sub 매매신호처리함수()

        Dim 현재신호 As Integer = Val(Form2.lbl_F2_매매신호.Text)
        Form2.lbl_F2_매매신호.Refresh()

        If EBESTisConntected = True And 당일반복중_flag = False Then     '---------------- 당일반복 돌릴 때 문제 없도록 잘 해야 함

            Dim 매매1회최대수량 As Integer = Val(Form2.txt_F2_1회최대매매수량.Text)

            '현재잔고를 검사해서 방향과 다르면 처분하기 - 이건 시간에 관계없이 한다  
            If List잔고 IsNot Nothing Then
                For i As Integer = 0 To List잔고.Count - 1

                    Dim it As 잔고Type = List잔고(i)

                    If it.A02_구분 = "매도" Then  '무엇인가 매도된 상태라면

                        If Mid(it.A01_종복번호, 1, 1) = "2" Then
                            If 현재신호 = 0 Or 현재신호 = 1 Then  '콜일 때 -내린다를 산 상태인데 앞으로 오르거나 신호해제가 되면
                                Dim 종목번호 As String = it.A01_종복번호

                                Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                                Dim count As Integer = 0

                                count = Math.Min(잔고와청산가능, 콜최대구매개수 - 콜현재환매개수)
                                count = Math.Min(count, 매매1회최대수량)
                                If count > 0 Then 한종목매수(종목번호, it.A10_현재가, count)

                            End If

                        Else '풋일 때  -- 오른다를 산 상태
                            If 현재신호 = 0 Or 현재신호 = -1 Then
                                Dim 종목번호 As String = it.A01_종복번호
                                Dim callput As String = Mid(it.A01_종복번호, 1, 1)
                                Dim 잔고와청산가능 As Integer = Math.Min(it.A03_잔고수량, it.A04_청산가능수량)
                                Dim count As Integer = 0

                                count = Math.Min(잔고와청산가능, 풋최대구매개수 - 풋현재환매개수)
                                count = Math.Min(count, 매매1회최대수량)
                                If count > 0 Then 한종목매수(종목번호, it.A10_현재가, count)
                            End If
                        End If
                    End If
                Next
            End If


            '매도를 한다 - 이건 시간 체크한다
            If currentIndex_순매수 >= 0 Then
                Dim currentTime As Integer = Val(순매수리스트(currentIndex_순매수).sTime)

                If Form2.chk_실거래실행.Checked = True Then
                    Dim 매수시작시간 As Integer = Val(Form2.txt_F2_매수시작시간.Text)
                    Dim 매수마감시간 As Integer = Val(Form2.txt_F2_매수마감시간.Text)
                    If currentTime >= 매수시작시간 And currentTime <= 매수마감시간 Then

                        '매도실행
                        Dim 매도가능수량 As Integer = 0
                        If 현재신호 = 1 Then  '상승베팅 - put 매도

                            Dim code As String = 일분옵션데이터(1).Code
                            Dim count As Integer = Math.Min(풋구매가능개수, 매매1회최대수량)   '매도했으나 체결이 늦게되어 더 많이 구매하는 문제처리 로직 검토
                            Dim price As Single = 일분옵션데이터(1).price(currentIndex_1MIn, 3)
                            If count > 0 Then 한종목매도(code, price, count)

                        ElseIf 현재신호 = -1 Then
                            Dim code As String = 일분옵션데이터(0).Code
                            Dim count As Integer = Math.Min(콜구매가능개수, 매매1회최대수량)
                            Dim price As Single = 일분옵션데이터(0).price(currentIndex_1MIn, 3)
                            If count > 0 Then 한종목매도(code, price, count)

                        End If

                    End If
                End If
            End If
        End If
    End Sub

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
End Module
