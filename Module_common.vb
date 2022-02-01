﻿Option Explicit On

Structure DataSet

    Dim HangSaGa As String '콜풋
    Dim Code() As String '콜풋
    Dim ctime() As String  '시간 100개 콜풋 구분 없음

    Dim price(,,) As Single '콜풋, 시간, 시고저종

    Dim Big(,) As Single '콜풋 시고저종
    Dim Small(,) As Single '콜풋 시고저종
    Dim 거래량(,) As Long '콜풋 100개

    Dim 증거금() As Long

    Public Sub Initialize()

        ReDim Code(1)
        ReDim 증거금(1)
        ReDim ctime(100) '
        ReDim price(1, 100, 3)
        ReDim Big(1, 3) '시고저종을 기록해야해서 4개
        ReDim Small(1, 3) '시고저종을 기록해야해서 4개
        ReDim 거래량(1, 100) '시간대별 거래량을 기록해야 해서 100개가 필요함


    End Sub

End Structure




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





    Public Sub InitDataStructure()

        ReDim Data(100) '최대 100개지만 실제로는 28개까지만 지원한다 - 콜풋 각각
        For i As Integer = 0 To 100
            Data(i).Initialize()
        Next

        TargetDate = 0
        sMonth = "0"
        Interval = 5
        TotalJongMokCount = 0
        UpperLimit = 4.0
        LowerLimt = 0.4
        timeIndex = 0

        JongmokTargetPrice = 2.0
        selectedJongmokIndex(0) = 0
        selectedJongmokIndex(1) = 0

    End Sub


    Public Function CalcTargetJonhmokIndex(ByVal callput As Integer) As Integer

        Dim temptarget, i As Integer
        Dim mingap, gap As Single

        mingap = 1000.0

        For i = 0 To TotalCount

            If Data(i).price(callput, currentIndex, 3) > 0 Then
                gap = Math.Abs(Data(i).price(callput, currentIndex, 3) - JongmokTargetPrice)
                If gap < mingap Then
                    mingap = gap
                    temptarget = i
                End If
            End If

        Next

        Return temptarget

    End Function

End Module