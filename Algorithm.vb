Option Explicit On

Structure ShinhoType

    Dim A00_월물 As String
    Dim A01_날짜 As String
    Dim A02_interval As Integer
    Dim A03_남은날짜 As Integer
    Dim A04_발생Index As Integer
    Dim A05_발생시간 As String
    Dim A06_신호ID As String
    Dim A07신호차수 As Single
    Dim A11_콜인덱스 As Integer
    Dim A12_콜행사가 As String
    Dim A13_콜신호발생가격 As Single
    Dim A14_콜매수가격 As Single
    Dim A15_콜주문번호 As String
    Dim A16_콜종목코드 As String
    Dim A17_콜체결상태 As Integer
    Dim A21_풋인덱스 As Integer
    Dim A22_풋행사가 As String
    Dim A23_풋신호발생가격 As Single
    Dim A24_풋매수가격 As Single
    Dim A25_풋주문번호 As String
    Dim A26_풋종목코드 As String
    Dim A27_풋체결상태 As Integer
    Dim A31_신호합계가격 As Single
    Dim A32_현재합계가격 As Single
    Dim A33_현재상태 As String
    Dim A34_이익률 As Single
    Dim A35_손절기준가격 As Single
    Dim A36_익절기준가격 As Single
    Dim A37_손절기준비율 As String
    Dim A38_익절기준비율 As String
    Dim A39_중간매도Flag As Integer
    Dim A40_TimeoutTime As String '장 중 매도할 최종 시간
    Dim A41_매도시간 As String
    Dim A42_매도Index As Integer
    Dim A43_매도사유 As String  '익절, 손절, TimeOver 구분
    Dim A44_메모 As String
    Dim A45_기준가격 As String '매수할 때 기준가격
    Dim A46_환산이익율 As Single '매수 관점에서 이익률
    Dim A47_IsReal As Integer
    Dim A48_조건전체 As String
End Structure

Enum occurType
    oneShot
    muitl
End Enum

Module Algorithm

    Public 양매도TargetIndex As Integer
    Public ShinhoList As List(Of ShinhoType)
    Public SimulationTotalShinhoList As List(Of ShinhoType)
    Public Simulation_조건 As String

    Public Sub 신호현재상태확인하기()

        For i As Integer = 0 To ShinhoList.Count - 1
            Dim str As String = ""

            Dim shinho As ShinhoType = ShinhoList(i)

            If shinho.A33_현재상태 = 1 Then

                If shinho.A06_신호ID = "S" Then

                    IsContinueShinho_S(shinho)
                    ShinhoList(i) = shinho

                    If shinho.A33_현재상태 = 0 Then   '1이었다가 다시 0이 되면 환매를 수행함
                        청산실행(1.0) '청산100%
                    Else '1이 유지될 때 
                        If Form1.chk_중간청산.Checked = True Then
                            If shinho.A39_중간매도Flag = 1 Then 청산실행(0.5)
                        End If
                    End If

                End If
            Else    '신호가 죽고 난 후 잔고가 아직 남아있을 때 처리루틴
                If shinho.A06_신호ID = "S" Then
                    청산실행(1.0) '청산100%
                End If
            End If
        Next
    End Sub

    Private Sub IsContinueShinho_S(ByRef shinho As ShinhoType)

        Dim 콜현재가, 풋현재가, 합계가격 As Single

        콜현재가 = Data(0).price(currentIndex, 3)
        풋현재가 = Data(1).price(currentIndex, 3)

        If 콜현재가 <= 0 Then Return
        If 풋현재가 <= 0 Then Return

        합계가격 = 콜현재가 + 풋현재가

        shinho.A32_현재합계가격 = 합계가격
        shinho.A34_이익률 = (합계가격 / shinho.A31_신호합계가격)
        Dim 중간청산비율 As Single = Val(Form1.txt_중간청산비율)


        If shinho.A35_손절기준가격 < 합계가격 Then '손절

            shinho.A32_현재합계가격 = shinho.A35_손절기준가격 + 0.01
            shinho.A34_이익률 = (shinho.A32_현재합계가격 / shinho.A31_신호합계가격)

            shinho.A33_현재상태 = 0
            shinho.A41_매도시간 = Data(0).ctime(currentIndex)
            shinho.A42_매도Index = currentIndex
            shinho.A43_매도사유 = "son"

        ElseIf shinho.A36_익절기준가격 > 합계가격 Then '익절

            shinho.A32_현재합계가격 = shinho.A36_익절기준가격 + 0.01
            shinho.A34_이익률 = (shinho.A32_현재합계가격 / shinho.A31_신호합계가격)

            shinho.A33_현재상태 = 0
            shinho.A41_매도시간 = Data(0).ctime(currentIndex)
            shinho.A42_매도Index = currentIndex
            shinho.A43_매도사유 = "ik"
        ElseIf Form1.chk_중간청산.Checked = True Then
            If shinho.A34_이익률 < 중간청산비율 Then
                shinho.A39_중간매도Flag = 1
            End If
        End If

        'time limit 체크
        If Val(Data(0).ctime(currentIndex)) >= Val(shinho.A40_TimeoutTime) + 5 Then
            shinho.A33_현재상태 = 0
            shinho.A41_매도시간 = Data(0).ctime(currentIndex)
            shinho.A42_매도Index = currentIndex
            shinho.A43_매도사유 = "timeout"
        End If

        shinho.A46_환산이익율 = 1 - (shinho.A32_현재합계가격 / shinho.A31_신호합계가격)

    End Sub

    Public Function CalcAlrotithmAll() As Boolean '전체 알고리즘을 계산한다 - 일단 양매도 S 알고리즘만 먼저 적용함

        양매도TargetIndex = Val(Form1.txt_양매도Target시간Index.Text)

        Dim ret As Boolean = CalcAlgorithm_S() '양매도 조건 검지  ------------------------------------ 양매도 조건 검지 시 매도 실행
        If ret = True Then
            If EBESTisConntected = True Then
                If Form1.chk_양매도실행.Checked = True Then
                    If 콜현재환매개수 = 0 And 풋현재환매개수 = 0 Then     '신호와 같은 라인에서 사고 팔리면 그 후 자동으로 또 사지는 걸 방지하는 코드
                        매도실행호출(0)
                        매도실행호출(1)
                    End If
                End If
            End If
        End If

        Return ret

    End Function

    Private Function AlreadyOccured(ByVal AlgorithmID As String, ByVal 구분 As Integer) As Boolean

        For i As Integer = 0 To ShinhoList.Count - 1
            If 구분 = occurType.oneShot Then '신호리스트에 같은 코드가 한번이라도 있으면 존재한다라고 응답 (양매도는 이 타입임)
                If ShinhoList(i).A06_신호ID = AlgorithmID Then Return True
            Else '신호리스트에 같은 코드가 있고 그 코드현재상태가 1인 경우만 존재한다고 응답 ----------- 이건 나중에 구현 예정 
                If ShinhoList(i).A06_신호ID = AlgorithmID And ShinhoList(i).A33_현재상태 = 1 Then Return True
            End If
        Next
        Return False

    End Function

    '양매도 조건 검지 - 검지하면 True를 리턴함
    Private Function CalcAlgorithm_S() As Boolean

        Dim ShinhoID As String = "S"
        Dim tempIndex = GetMaxIndex() 'curreuntIndex가 79일 때 0이어서 이상동작하는 거 방지하는 코드
        Dim ret As Boolean = False

        If tempIndex <> 양매도TargetIndex Then Return False '양매도는 정해진 시간에 수행하고 시간이 아니라면 아래를 수행하지 않는다

        '타겟시간 시가가 0보다 크면 무조건 양매도를 친다
        If Data(0).price(tempIndex, 0) > 0 And Data(1).price(tempIndex, 0) > 0 Then

            If isRealFlag = True Then

                If ReceiveCount > 2 Then

                    If AlreadyOccured("S", occurType.oneShot) = False Then   '위에서 이미발생 조건을 확인하지 않으니 신호가 여러개 등록되는 문제가 발생한 거 같음.(나중에 오후에 신호리스트 루프를 돌면서 매수함) True를 리턴해서 매도는 여러번 하더라도 신호는 1개만 등록되도록 변경함. 20220629

                        '"S신호 발생"
                        Dim shinho As ShinhoType = MakeShinho(tempIndex, Data(0).ctime(tempIndex), ShinhoID, 1, selectedJongmokIndex(0), selectedJongmokIndex(1))
                        ShinhoList.Add(shinho)
                        Add_Log("일반", "양매도 신호 isRealFlag = " & isRealFlag.ToString() & ", ReceiveCount = " & ReceiveCount.ToString())

                    End If

                    ret = True

                End If

            Else
                If AlreadyOccured("S", occurType.oneShot) = False Then   '위에서 이미발생 조건을 확인하지 않으니 신호가 여러개 등록되는 문제가 발생한 거 같음.(나중에 오후에 신호리스트 루프를 돌면서 매수함) True를 리턴해서 매도는 여러번 하더라도 신호는 1개만 등록되도록 변경함. 20220629

                    '"S신호 발생"
                    Dim shinho As ShinhoType = MakeShinho(tempIndex, Data(0).ctime(tempIndex), ShinhoID, 1, selectedJongmokIndex(0), selectedJongmokIndex(1))
                    ShinhoList.Add(shinho)
                    Add_Log("일반", "양매도 신호 isRealFlag = " & isRealFlag.ToString() & ", ReceiveCount = " & ReceiveCount.ToString())

                End If

                ret = True

            End If

        End If

        Return ret

    End Function

    Private Function MakeShinho(ByVal index As Integer, ByVal ctime As String, ByVal ShinhoID As String, ByVal 신호차수 As Integer, ByVal callSelectedIndex As Integer, ByVal putSelectedIndex As Integer) As ShinhoType

        Dim shinho As ShinhoType = New ShinhoType

        shinho.A00_월물 = sMonth
        shinho.A01_날짜 = TargetDate
        shinho.A02_interval = Interval
        shinho.A03_남은날짜 = getRemainDate(sMonth, Val(TargetDate))

        shinho.A04_발생Index = index
        shinho.A05_발생시간 = ctime

        shinho.A06_신호ID = ShinhoID
        shinho.A07신호차수 = 신호차수

        shinho.A11_콜인덱스 = callSelectedIndex

        Dim calloption As ListTemplate = optionList(callSelectedIndex)
        Dim putoption As ListTemplate = optionList(putSelectedIndex)

        shinho.A12_콜행사가 = calloption.HangSaGa
        shinho.A16_콜종목코드 = calloption.Code(0)

        shinho.A21_풋인덱스 = putSelectedIndex
        shinho.A22_풋행사가 = putoption.HangSaGa
        shinho.A26_풋종목코드 = putoption.Code(1)

        If isRealFlag = True Then  '실시간에서는 해당 라인의 종가를 가져오고 시뮬레이션에서는 해당라인의 시가 - 0.01 틱으로 처리한다 (매도 시)
            shinho.A13_콜신호발생가격 = Data(0).price(index, 3)
            shinho.A14_콜매수가격 = Data(0).price(index, 3) - 0.01 '한틱정도 슬리피지
            shinho.A23_풋신호발생가격 = Data(1).price(index, 3)
            shinho.A24_풋매수가격 = Data(1).price(index, 3) - 0.01
        Else
            shinho.A13_콜신호발생가격 = Data(0).price(index, 0)
            shinho.A14_콜매수가격 = Data(0).price(index, 0) - 0.01 '한틱정도 슬리피지
            shinho.A23_풋신호발생가격 = Data(1).price(index, 0)
            shinho.A24_풋매수가격 = Data(1).price(index, 0) - 0.01
        End If

        shinho.A31_신호합계가격 = shinho.A13_콜신호발생가격 + shinho.A23_풋신호발생가격
        shinho.A32_현재합계가격 = shinho.A14_콜매수가격 + shinho.A24_풋매수가격 '이건 실시간으로 변경되는 가격인데 안넣어도 되지만 슬리피지 감안해서 넣은게 나을 듯

        shinho.A33_현재상태 = 1
        shinho.A34_이익률 = 1

        Dim 손절기준비율 = Val(Form1.txt_손절매비율.Text)
        Dim 익절기준비율 = Val(Form1.txt_익절목표.Text)

        shinho.A35_손절기준가격 = 손절기준비율 * shinho.A31_신호합계가격
        shinho.A36_익절기준가격 = 익절기준비율 * shinho.A31_신호합계가격
        shinho.A37_손절기준비율 = 손절기준비율
        shinho.A38_익절기준비율 = 익절기준비율
        'shinho.A39_중간매도Flag
        shinho.A40_TimeoutTime = Form1.txt_신호TimeOut시간.Text

        shinho.A45_기준가격 = Form1.txt_JongmokTargetPrice.Text
        If isRealFlag = True Then
            shinho.A47_IsReal = 1
        Else
            shinho.A47_IsReal = 0
        End If

        shinho.A44_메모 = Form1.txt_실험조건.Text
        shinho.A48_조건전체 = Simulation_조건
        Return shinho

    End Function

    Private Function Check중간청산() As Boolean
        Dim ret As Boolean = False

        Dim 중간청산시간 As Integer = 1230

        If Val(Data(0).ctime(currentIndex)) >= 중간청산시간 + 5 Then

            Dim shinho As ShinhoType = ShinhoList(0)
            shinho.A39_중간매도Flag = 1
            ShinhoList(0) = shinho
            ret = True
        End If

        Return ret

    End Function

    Public Sub 청산실행(청산비율 As Single)

        If EBESTisConntected = False Then Return
        If 당일반복중_flag = True Then Return

        Dim 콜여기서의청산갯수 As Integer
        Dim 풋여기서의청산갯수 As Integer
        If 청산비율 >= 0.95 Then
            콜여기서의청산갯수 = 콜최대구매개수 - 콜현재환매개수
            풋여기서의청산갯수 = 풋최대구매개수 - 풋현재환매개수
        Else
            콜여기서의청산갯수 = Math.Max(Math.Truncate(콜최대구매개수 * 0.5) - 콜현재환매개수, 0)
            풋여기서의청산갯수 = Math.Max(Math.Truncate(풋최대구매개수 * 0.5) - 풋현재환매개수, 0)
        End If

        'If 콜여기서의청산갯수 <= 0 And 풋구매가능개수 <= 0 Then Return 이게 있으면 혹시 계산이 잘못되어 매도가 남아있는 경우에 청산하지 못하는 문제 발생

        Dim str As String

        If List잔고 IsNot Nothing Then
            For j As Integer = 0 To List잔고.Count - 1

                Dim it As 잔고Type = List잔고(j)
                Dim count As Integer = 0

                If it.A02_구분 = "매도" Then

                    If Mid(it.A01_종복번호, 1, 1) = "2" Then  '콜일 때
                        count = Math.Min(콜여기서의청산갯수, it.A03_잔고수량)
                        count = Math.Min(count, 풋구매가능개수) '잔고부족으로 안 팔릴 수 있어서 잔고를 감안하여 구매 가능개수만큼만 매수한다
                        str = String.Format("청산 콜 최대 ={0},현재환매={1},청산비율={2},실제청산개수={3},종목={4}", 콜최대구매개수, 콜현재환매개수, 청산비율, count, it.A01_종복번호)
                    Else
                        count = Math.Min(풋여기서의청산갯수, it.A03_잔고수량)
                        count = Math.Min(count, 콜구매가능개수) '잔고부족으로 안 팔릴 수 있어서 잔고를 감안하여 구매 가능개수만큼만 매수한다
                        str = String.Format("청산 풋 최대 ={0},현재환매={1},청산비율={2},실제청산개수={3},종목={4}", 풋최대구매개수, 풋현재환매개수, 청산비율, count, it.A01_종복번호)
                    End If

                    If count > 8 Then
                        count = Math.Min(count, 8) '한번에 최대 8개만 청산한다
                        str = str & "-8개로 조정"
                    End If
                    If count > 0 Then
                        한종목매수(it.A01_종복번호, it.A10_현재가, count)
                        Add_Log("", str)
                    End If

                End If
            Next
        End If

    End Sub

    Public Sub 청산실행_매수를(청산비율 As Single)

        If EBESTisConntected = False Then Return
        If 당일반복중_flag = True Then Return

        Dim str As String

        If List잔고 IsNot Nothing Then
            For j As Integer = 0 To List잔고.Count - 1

                Dim it As 잔고Type = List잔고(j)
                Dim count As Integer = 0

                If it.A02_구분 = "매수" Then

                    If Mid(it.A01_종복번호, 1, 1) = "2" Then  '콜일 때
                        count = it.A03_잔고수량

                        str = String.Format("매수청산 콜 최대 ={0},현재환매={1},청산비율={2},실제청산개수={3},종목={4}", 콜최대구매개수, 콜현재환매개수, 청산비율, count, it.A01_종복번호)
                    Else
                        count = it.A03_잔고수량

                        str = String.Format("매수청산 풋 최대 ={0},현재환매={1},청산비율={2},실제청산개수={3},종목={4}", 풋최대구매개수, 풋현재환매개수, 청산비율, count, it.A01_종복번호)
                    End If

                    If count > 8 Then
                        count = Math.Min(count, 8) '한번에 최대 8개만 청산한다
                        str = str & "-8개로 조정"
                    End If
                    If count > 0 Then 한종목매도(it.A01_종복번호, it.A10_현재가, count)
                    Add_Log("", str)
                End If
            Next
        End If

    End Sub

End Module
