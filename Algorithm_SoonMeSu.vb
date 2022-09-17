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

    End Structure

    Public SoonMesuShinhoList As List(Of 순매수신호_탬플릿)
    Public SoonMesuSimulationTotalShinhoList As List(Of 순매수신호_탬플릿)
    Public SoonMesuSimulation_조건 As String

    Public Sub SoonMesuCalcAlrotithmAll()

        Dim ret As String = CalcAlgorithm_AB()

        If ret <> "중립" Then

            이전순매수방향 = ret '신호가 뜬걸 의미한다 이걸 바꿔 놓는다

            If ret = "상승" Then

                '이전 신호확인해서 죽이기
                '매수잔고 중에서 반대 신호 매도하기

                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("A")             '신규 신호 입력하기
                SoonMesuShinhoList.Add(shinho)

                If EBESTisConntected = True And Form2.chk_F2_매수실행.Checked = True Then
                End If
            ElseIf ret = "하락" Then

                '이전 신호확인해서 죽이기
                '매수잔고 중에서 반대 신호 매도하기

                Dim shinho As 순매수신호_탬플릿 = MakeSoonMesuShinho("B")              '신규 신호 입력하기
                SoonMesuShinhoList.Add(shinho)

                If EBESTisConntected = True And Form2.chk_F2_매수실행.Checked = True Then
                End If
            End If
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
        If 신호ID = "A" Then
            shinho.A08_콜풋 = 0
        Else
            shinho.A08_콜풋 = 1
        End If

        shinho.A09_행사가 = 0
        shinho.A10_신호발생가격 = 0

        shinho.A15_현재상태 = 1
        shinho.A16_이익률 = 0

        shinho.A21_환산이익율 = 0

        shinho.A50_조건전체 = ""
        shinho.A51_순매수기준 = "3.외국인_연기금"  '1.외국인, 2.외국인_기관, 3.외국인_연기금
        shinho.A52_기울기 = Form2.txt_상승하락기울기기준.Text
        shinho.A53_선행포인트수_마진 = Form2.txt_선행_포인트_마진.Text
        shinho.A54_IsReal = 0
        shinho.A56_기준가격 = Form2.txt_F2_기준가격.Text
        shinho.A57_월물 = sMonth
        shinho.A58_날짜 = 순매수리스트(currentIndex_순매수).sDate
        shinho.A59_남은날짜 = getRemainDate(sMonth, Val(shinho.A58_날짜))
        shinho.A60_손절기준차 = Form2.txt_F2_손절매차.Text
        shinho.A61_익절기준차 = Form2.txt_F2_익절차.Text
        shinho.A62_TimeoutTime = Form2.txt_F2_TimeoutTime.Text

        Return shinho

    End Function


End Module
