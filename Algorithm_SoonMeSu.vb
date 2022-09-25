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

        Dim ret As String = CalcAlgorithm_AB()

        If ret <> "중립" Then '중립 --> 상승 or 하강이 뜨거나 반대 방향 신호가 뜨면 여기를 진입한다

            If ret = "상승" Then '중립에서 상승으로 전환 


                반대방향신호죽이는함수("A_DOWN", "change")                '이전 신호확인해서 죽이기
                '매수잔고 중에서 반대 신호 매수로 해결하기  ---- 꼭 해야 함 20220925

                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_UP")             '신규 신호 입력하기
                SoonMesuShinhoList.Add(shinho)

                If EBESTisConntected = True And Form2.chk_F2_매수실행.Checked = True Then
                    Dim 매수시작시간 As Integer = Val(Form2.txt_F2_매수시작시간.Text)
                    Dim 매수마감시간 As Integer = Val(Form2.txt_F2_매수마감시간.Text)
                    If shinho.A02_발생시간 >= 매수시작시간 And shinho.A02_발생시간 <= 매수마감시간 Then
                        Add_Log("신호", String.Format("콜매수로 청산, 풋 매도 AT {0}", shinho.A02_발생시간))
                    End If
                End If
            ElseIf ret = "하락" Then '중립에서 하락으로 전환

                반대방향신호죽이는함수("A_UP", "change") '이전 신호확인해서 죽이기
                '매수잔고 중에서 반대 신호 매수로 해결하기  ---- 꼭 해야 함 20220925

                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A_DOWN")              '신규 신호 입력하기
                SoonMesuShinhoList.Add(shinho)

                If EBESTisConntected = True And Form2.chk_F2_매수실행.Checked = True Then
                    Dim 매수시작시간 As Integer = Val(Form2.txt_F2_매수시작시간.Text)
                    Dim 매수마감시간 As Integer = Val(Form2.txt_F2_매수마감시간.Text)
                    If shinho.A02_발생시간 >= 매수시작시간 And shinho.A02_발생시간 <= 매수마감시간 Then
                        Add_Log("신호", String.Format("풋매수로 청산, 콜 매도 AT {0}", shinho.A02_발생시간))
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

        Dim 매수마감시간 As Integer = Val(Form2.txt_F2_매수마감시간.Text)
        Dim curTime = Val(순매수리스트(currentIndex_순매수).sTime)

        If curTime >= 매수마감시간 Then Return ret

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

        shinho.A04_신호발생순매수 = 순매수리스트(currentIndex_순매수).외국인순매수 + 순매수리스트(currentIndex_순매수).기관순매수
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
        shinho.A56_기준가격 = Form2.txt_F2_기준가격.Text
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
        shinho.A10_신호발생가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
        shinho.A14_현재가격 = 일분옵션데이터(shinho.A08_콜풋).price(일분옵션데이터_CurrentIndex, 3)
        shinho.A16_이익률 = Math.Round((shinho.A10_신호발생가격 - shinho.A14_현재가격) / shinho.A10_신호발생가격, 3)


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
                    's.A14_현재가격 --- 이건 나중에 옵션 가격을 적는다
                    's.A16_이익률     '이것도 나중에 옵션 가격 기준으로 적는다
                    's.A21_환산이익율
                    s.A18_매도시간 = 순매수리스트(currentIndex_순매수).sTime
                    s.A19_매도Index = currentIndex_순매수
                    s.A20_매도사유 = 매도사유

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
                    Dim 일분옵션데이터_CurrentIndex As Integer = 순매수시간으로1MIN인덱스찾기(Val(순매수리스트(currentIndex_순매수).sTime))
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
                    End If

                    SoonMesuShinhoList(i) = s

                End If
            Next
        End If
    End Sub



End Module
