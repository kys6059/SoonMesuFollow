Option Explicit On

Structure DataSet

    Dim HangSaGa() As String '콜풋
    Dim index() As Integer '콜풋
    Dim Code() As String '콜풋
    Dim ctime() As String  '시간 100개 콜풋 구분 없음

    Dim price(,,) As Single '콜풋, 시간, 시고저종

    Dim Big(,) As Single '콜풋 시고저종
    Dim Small(,) As Single '콜풋 시고저종
    Dim 거래량(,) As Long '콜풋 100개

    Dim 증거금() As Long


    Public Sub Initialize()

        ReDim HangSaGa(2)
        ReDim index(2)
        ReDim Code(2)
        ReDim 증거금(2)
        ReDim ctime(100) '
        ReDim price(2, 100, 4)
        ReDim Big(2, 4) '시고저종을 기록해야해서 4개
        ReDim Small(2, 4) '시고저종을 기록해야해서 4개
        ReDim 거래량(2, 100) '시간대별 거래량을 기록해야 해서 100개가 필요함


    End Sub

End Structure




Module Module_common


    Public Data() As DataSet
    Public TargetDate As Integer
    Public sMonth As String
    Public Interval As Integer
    Public TotalJongMokCount As Integer




    Public Sub InitDataStructure()

        ReDim Data(50) '최대 50개 종목만 지원한다 - 콜풋 각각
        TargetDate = 0
        sMonth = "0"
        Interval = 5
        TotalJongMokCount = 0

    End Sub

End Module
