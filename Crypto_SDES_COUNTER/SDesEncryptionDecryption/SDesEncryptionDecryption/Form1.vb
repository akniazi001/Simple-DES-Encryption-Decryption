Imports System.IO
Imports System.String
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Convert
Imports System.Globalization

Public Class Form1
    Dim N As Integer ' Number of rounds
    Dim NKeysInList As Integer = 0 ' Number of rounds.
    Dim NPlanInList As Integer = 0 ' Number of rounds
    Dim Counter As Integer
    Dim CounterVal As String
    Dim CounterKeyVal As String
    Dim listItem As String
    Dim NewRoundKey As String
    Dim CountKey As Integer
    Dim BitBox As String
    Dim BitDistrbuted As String
    Dim ConvertedBitHolder As String
    Dim KeyToEncrypt As String
    Dim FirstPermutExtention As String
    Dim KeyPreperedToEncrypt As String
    Dim FirstXorHolder As String
    Dim SBoxHolder As String
    Dim LeftSideHolder As String
    Dim LeftCipherHolder As String
    Dim DecryptionKey As Integer = 0
    Dim FirstRoundXorHolder As String
    Dim LastFinalOutput As String
    Dim LimitLoop As Integer = 0
    Dim LimitLoopManul As Integer = 0
    Dim LimitLoopManul1 As Integer = 0
    Dim KeysInDecryptList As Integer = 0
    Dim RoundNumberDecryption As Integer = 0
    Dim ValCipher As String = Nothing
    Dim KeyToDecrypt As String
    Dim KeyPreperedToDecrypt As String
    Dim KeyNumber As Integer = 0
    Dim RoundNumber As Integer = 0
    Dim GeneratingLoopas As Integer = 1
    Dim CounterHolder As String
    Dim PlanXorCounterHolder As String
    Dim LimitLoop1 As Integer
    Dim FinalDecriptionLoop As Integer = 0
    Dim FinalReturnLoop As Integer = 0
    Dim DecryptionLoop As Integer
    Dim t As Integer
    Dim zoher As New DataSet
    Dim WithEvents BS As New BindingSource
    Dim WithEvents BS1 As New BindingSource
    Dim RandomClass As New Random()
    Dim stat As Boolean
    Private Shared prng As New Random

#Region "aupdate"
    Public Sub aupdate()
        Static n As Integer
        n = Me.BS1.Position
        Dim Str3 As String = "update dtb set Statuse ='" & 1 & "'" & "where  Id=" & Me.TextBox26.Text
        cmd.Connection = cnn
        cmd.CommandText = Str3
        If cnn.State = ConnectionState.Open Then cnn.Close()
        cnn.Open()
        cmd.ExecuteNonQuery()
        cnn.Close()
        Me.BS.Position = n
    End Sub
#End Region
#Region "fill Table2"
    Private Sub filltable2()
        On Error Resume Next
        Dim DataAdapter As New OleDbDataAdapter("SELECT * FROM dtb where Statuse = False ", cnn)
        cnn.Open()
        ds1.Clear()
        DataAdapter.Fill(ds1, "dtb")
        cnn.Close()
        DataGridView2.DataSource = ds1
        DataGridView2.DataMember = "dtb"
        DataGridView2.Refresh()
        DataGridView2.RowHeadersWidth = 25
       DataGridView2.Columns(0).Width = 22
        DataGridView2.Columns(1).Width = 100
        DataGridView2.Columns(0).HeaderText = "ID"
        DataGridView2.Columns(1).HeaderText = "User Name"
        DataGridView2.Columns(2).HeaderText = "Statuse"
        DataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        TextBox27.DataBindings.Add("Text", ds1, "dtb.Key_Char")
        TextBox41.DataBindings.Add("Text", ds1, "dtb.Key_Char")
        TextBox26.DataBindings.Add("Text", ds1, "dtb.ID")
        TextBox28.DataBindings.Add("Text", ds1, "dtb.Knonce")
        TextBox29.DataBindings.Add("Text", ds1, "dtb.Encrypted_msg")
    End Sub
#End Region
#Region "fill Table"
    Private Sub filltable()
        On Error Resume Next
        Dim DataAdapter As New OleDbDataAdapter("SELECT * FROM dtb where Statuse = True ", cnn)
        cnn.Open()
        ds.Clear()
        DataAdapter.Fill(ds, "dtb")
        cnn.Close()
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "dtb"
        DataGridView1.Refresh()
        DataGridView1.RowHeadersWidth = 25
        DataGridView1.Columns(0).Width = 22
        DataGridView1.Columns(1).Width = 100
        DataGridView1.Columns(0).HeaderText = "ID"
        DataGridView1.Columns(1).HeaderText = "Key Charachter"
        DataGridView1.Columns(2).HeaderText = "Knonce"
        DataGridView1.Columns(3).HeaderText = "Encrypted Message"
        DataGridView1.Columns(4).HeaderText = "Statuse"

        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
#End Region

#Region "wait list"
    Private Sub waitlist()
        On Error Resume Next
        cnn.ConnectionString = constring3
        ds.EnforceConstraints = False
        cnn.Open()
        Dim str1 As String = "SELECT * FROM dtb where Statuse = FALSE"
        adapt1 = New OleDbDataAdapter(str1, cnn)
        ds1.Clear()
        adapt1.Fill(ds1, "dtb")
        BS1.DataSource = ds1
        BS1.DataMember = "dtb"
        ds1.EnforceConstraints = True
        adapt1.Dispose()
        cnn.Close()
        Dim n As Double
        n = BS1.Count
        Label22.Text = " (" & n & ")  Unread Message "

    End Sub
#End Region

    Sub KeyREQUIRED()
        On Error Resume Next
        DecryptionLoop = N
        TextBox25.Text = N
        LimitLoopManul = N * 2 - 1
        LimitLoopManul1 = N * 2
        DecryptionKey = LimitLoopManul
        LimitLoop1 = N
        DecryptionKey -= 1
        FinalDecriptionLoop = N
        FinalReturnLoop = N
        ' LimitLoopManul
    End Sub

    Private Sub pictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        End
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox135.Text = ConvertHexToBin(TextBox29.Text)
        SplitHix()
        KeyREQUIRED()
        aupdate()
    End Sub
    Sub SplitHix()
        On Error Resume Next
        Dim j As Integer = 0
        Dim bitbox As String
        Dim oneyte As String = Nothing
        Dim tex As String = TextBox135.Text
        For h = 0 To TextBox135.Text.Length
            bitbox = tex.Chars(h)
            oneyte += bitbox
            j += 1
            If j = 8 Then
                j = 0
                ListBox5.Items.Add(oneyte)
                oneyte = Nothing
                N += 1
            End If
        Next
    End Sub
    Private Function ConvertHexToBin(hex As String) As String
        Return String.Concat(hex.Select(Function(c) System.Convert.ToString(System.Convert.ToInt32(c, 16), 2).PadLeft(4, "0"c)))
    End Function
    Sub DistrbutionOneByOne()
        On Error Resume Next
        BitDistrbuted = TextBox29.Text
        For i = 0 To N - 1
            BitBox = BitDistrbuted.Chars(i)
            PlanToBinary()
            ListBox5.Items.Add(ConvertedBitHolder.ToString())
        Next i
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If RadioButton1.Checked = True Then
            RadioButton2.Enabled = False
        Else
            RadioButton2.Checked = True
            RadioButton1.Enabled = False
        End If
        StringToBinaryKey()
        button3.Enabled = True
        Button6.Enabled = False
    End Sub
    Private Sub button3_Click(sender As Object, e As EventArgs) Handles button3.Click
        If RadioButton1.Checked = True Then
            DistrbutingBitsKey()
            InitialPermutation()
            DivitionBits()
            ShiftLeftBit()
            FirstKeyPermute()
            ShiftTwoLeftBit()
            SecondKeyPermute()
            Button9.Enabled = True
        Else
            DistrbutingBitsKey()
            button4.Enabled = True
            button3.Enabled = False
        End If
    End Sub
    Sub DistrbutingBitsKey()
        On Error Resume Next
        Dim textBoxes() As TextBox = {TextBox61, TextBox50, TextBox49, TextBox48, TextBox47, TextBox46, TextBox45, TextBox44, TextBox43, TextBox42}
        Dim FirstName As String = TextBox41.Text
        For i = 0 To 7
            Dim TextB As String = FirstName.Chars(i)
            textBoxes(i).Text = TextB
            If i = 7 Then
                Do Until i = 9
                    i += 1
                    textBoxes(i).Text = 0
                Loop
            End If
        Next i
    End Sub
    Sub StringToBinaryKey()
        On Error Resume Next
        Dim Val As String = Nothing
        Dim Result As New System.Text.StringBuilder
        For Each Character As Byte In System.Text.ASCIIEncoding.ASCII.GetBytes(TextBox41.Text)
            Result.Append(System.Convert.ToString(Character, 2).PadLeft(8, "0"))
            Result.Append(" ")
        Next
        Val = Result.ToString.Substring(0, Result.ToString.Length - 1)
        TextBox41.Text = Val
    End Sub

    Private Sub button4_Click(sender As Object, e As EventArgs) Handles button4.Click
        InitialPermutation()
    End Sub
    Sub InitialPermutation()
        Dim z0, z1, z2, z3, z4, z5, z6, z7, z8, z9 As String
        z0 = TextBox61.Text
        z1 = TextBox50.Text
        z2 = TextBox49.Text
        z3 = TextBox48.Text
        z4 = TextBox47.Text
        z5 = TextBox46.Text
        z6 = TextBox45.Text
        z7 = TextBox44.Text
        z8 = TextBox43.Text
        z9 = TextBox42.Text
        textBox5.Text = z2
        textBox6.Text = z4
        textBox7.Text = z1
        textBox8.Text = z6
        textBox9.Text = z3
        textBox10.Text = z9
        textBox11.Text = z0
        textBox12.Text = z8
        TextBox76.Text = z7
        TextBox77.Text = z5
        button5.Enabled = True
        button4.Enabled = False
    End Sub
    Private Sub button5_Click(sender As Object, e As EventArgs) Handles button5.Click
        DivitionBits()
    End Sub
    Sub DivitionBits()
        TextBox36.Text = textBox5.Text
        TextBox37.Text = textBox6.Text
        TextBox38.Text = textBox7.Text
        TextBox39.Text = textBox8.Text
        TextBox40.Text = textBox9.Text
        TextBox21.Text = textBox10.Text
        TextBox22.Text = textBox11.Text
        TextBox23.Text = textBox12.Text
        TextBox24.Text = TextBox76.Text
        TextBox35.Text = TextBox77.Text
        ' for next Round 
        TextBox13.Text = TextBox36.Text
        TextBox14.Text = TextBox37.Text
        TextBox62.Text = TextBox38.Text
        TextBox63.Text = TextBox39.Text
        TextBox64.Text = TextBox40.Text
        textBox30.Text = TextBox21.Text
        textBox31.Text = TextBox22.Text
        textBox32.Text = TextBox23.Text
        textBox33.Text = TextBox24.Text
        textBox34.Text = TextBox35.Text
        button8.Enabled = True
        button5.Enabled = False
    End Sub
    Private Sub button8_Click(sender As Object, e As EventArgs) Handles button8.Click
        ShiftLeftBit()
    End Sub
    Sub ShiftLeftBit()
        Dim t0, t1, t2, t3, t4, t5, t6, t7, t8, t9 As String
        t0 = TextBox36.Text
        t1 = TextBox37.Text
        t2 = TextBox38.Text
        t3 = TextBox39.Text
        t4 = TextBox40.Text
        t5 = TextBox21.Text
        t6 = TextBox22.Text
        t7 = TextBox23.Text
        t8 = TextBox24.Text
        t9 = TextBox35.Text
        ' for left side shift
        TextBox36.Text = t1
        TextBox37.Text = t2
        TextBox38.Text = t3
        TextBox39.Text = t4
        TextBox40.Text = t0
        ' for right side shift
        TextBox21.Text = t6
        TextBox22.Text = t7
        TextBox23.Text = t8
        TextBox24.Text = t9
        TextBox35.Text = t5
        Button2.Enabled = True
        button8.Enabled = False
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FirstKeyPermute()
    End Sub
    Sub FirstKeyPermute()
        Dim d0, d1, d2, d3, d4, d5, d6, d7, d8, d9 As String
        d0 = TextBox36.Text
        d1 = TextBox37.Text
        d2 = TextBox38.Text
        d3 = TextBox39.Text
        d4 = TextBox40.Text
        d5 = TextBox21.Text
        d6 = TextBox22.Text
        d7 = TextBox23.Text
        d8 = TextBox24.Text
        d9 = TextBox35.Text
        TextBox3.Text = d5
        TextBox4.Text = d2
        TextBox15.Text = d6
        TextBox16.Text = d3
        TextBox17.Text = d7
        TextBox18.Text = d4
        TextBox19.Text = d9
        TextBox20.Text = d8
        Button10.Enabled = True
        Button2.Enabled = False
        CountKey += 1
        listItem = TextBox3.Text + TextBox4.Text + TextBox15.Text + TextBox16.Text + TextBox17.Text + TextBox18.Text + TextBox19.Text + TextBox20.Text
        ListBox1.Items.Add("KEY " & CountKey & "   " & listItem.ToString())
        ListBox2.Items.Add(listItem.ToString())

        LimitLoop += 1
    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ShiftTwoLeftBit()
    End Sub
    Sub ShiftTwoLeftBit()
        Dim f0, f1, f2, f3, f4, f5, f6, f7, f8, f9 As String
        f0 = TextBox13.Text
        f1 = TextBox14.Text
        f2 = TextBox62.Text
        f3 = TextBox63.Text
        f4 = TextBox64.Text
        f5 = textBox30.Text
        f6 = textBox31.Text
        f7 = textBox32.Text
        f8 = textBox33.Text
        f9 = textBox34.Text
        ' for left side shift
        TextBox13.Text = f3
        TextBox14.Text = f4
        TextBox62.Text = f0
        TextBox63.Text = f1
        TextBox64.Text = f2
        ' for right side shift
        textBox30.Text = f8
        textBox31.Text = f9
        textBox32.Text = f5
        textBox33.Text = f6
        textBox34.Text = f7
        Button11.Enabled = True
        Button10.Enabled = False
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        SecondKeyPermute()
    End Sub
    Sub SecondKeyPermute()
        Dim g0, g1, g2, g3, g4, g5, g6, g7, g8, g9 As String
        g0 = TextBox13.Text
        g1 = TextBox14.Text
        g2 = TextBox62.Text
        g3 = TextBox63.Text
        g4 = TextBox64.Text
        g5 = textBox30.Text
        g6 = textBox31.Text
        g7 = textBox32.Text
        g8 = textBox33.Text
        g9 = textBox34.Text
        TextBox67.Text = g5
        TextBox68.Text = g2
        TextBox69.Text = g6
        TextBox70.Text = g3
        TextBox71.Text = g7
        TextBox72.Text = g4
        TextBox73.Text = g9
        TextBox74.Text = g8
        CountKey += 1
        listItem = TextBox67.Text + TextBox68.Text + TextBox69.Text + TextBox70.Text + TextBox71.Text + TextBox72.Text + TextBox73.Text + TextBox74.Text
        ListBox1.Items.Add("KEY " & CountKey & "   " & listItem.ToString())
        ListBox2.Items.Add(listItem.ToString())
        Button11.Enabled = False
        Button9.Enabled = True
        MsgBox("Your Keys Has Been Generated")

    End Sub
    Private Function listItemXorCounter(ByVal bin1 As String, ByVal bin2 As String) As String
        Dim count As Integer
        Dim XORStr As String = ""
        For count = 0 To bin1.Length() - 1
            If bin1.Chars(count) = bin2.Chars(count) Then
                XORStr = XORStr + "0"
            ElseIf bin1.Chars(count) <> bin2.Chars(count) Then
                XORStr = XORStr + "1"
            End If
        Next count
        TextBox41.Text = XORStr
    End Function
    Sub InitialToEncrypt()
        On Error Resume Next
        If RoundNumber = 0 Then
            Dim PlanTextToEncrypt As String
            Dim PlanBoxes() As TextBox = {TextBox1, TextBox2, TextBox60, TextBox58, TextBox79, TextBox78, TextBox66, TextBox65}
            PlanTextToEncrypt = ListBox3.Items(NKeysInList)
            NKeysInList += 1
            For i = 0 To 7
                Dim PlanBitDistrbuted As String = PlanTextToEncrypt.Chars(i)
                PlanBoxes(i).Text = PlanBitDistrbuted
            Next i
            For i = 0 To 3
                Dim PlanBitDistrbuted As String = PlanTextToEncrypt.Chars(i)
                LeftSideHolder += PlanBitDistrbuted
            Next i
            TextBox112.Text = TextBox79.Text
            TextBox113.Text = TextBox78.Text
            TextBox114.Text = TextBox66.Text
            TextBox115.Text = TextBox65.Text
            RoundNumber += 1
        Else
            RoundNumber -= 1
        End If
    End Sub
    Sub SendKey()
        On Error Resume Next
        If KeyNumber = 0 Then
            Dim KeyBoxes() As TextBox = {TextBox88, TextBox89, TextBox90, TextBox91, TextBox92, TextBox93, TextBox94, TextBox95}
            KeyToEncrypt = ListBox2.Items(KeyNumber)
            KeyNumber += 1
            For y = 0 To 7
                Dim KeyBitDistrbuted As String = KeyToEncrypt.Chars(y)
                KeyBoxes(y).Text = KeyBitDistrbuted
            Next y
            KeyPreperedToEncrypt = TextBox88.Text + TextBox89.Text + TextBox90.Text + TextBox91.Text + TextBox92.Text + TextBox93.Text + TextBox94.Text + TextBox95.Text
            Button9.Enabled = False
            Button17.Enabled = True
        Else
            Dim KeyBoxes() As TextBox = {TextBox88, TextBox89, TextBox90, TextBox91, TextBox92, TextBox93, TextBox94, TextBox95}
            KeyToEncrypt = ListBox2.Items(KeyNumber)
            KeyNumber += 1
            For y = 0 To 7
                Dim KeyBitDistrbuted As String = KeyToEncrypt.Chars(y)
                KeyBoxes(y).Text = KeyBitDistrbuted
            Next y
            KeyPreperedToEncrypt = TextBox88.Text + TextBox89.Text + TextBox90.Text + TextBox91.Text + TextBox92.Text + TextBox93.Text + TextBox94.Text + TextBox95.Text
            Button9.Enabled = False
            Button17.Enabled = True
            KeyNumber = 0
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If RadioButton3.Checked = True Then
            Dim k As Integer = LimitLoopManul1
            Do Until k = 0
                k = k - 1
                GenerateCounter()
                InitialToEncrypt()
                SendKey()
                FirstPermutToEncript()
                PlanXorKey(KeyPreperedToEncrypt, FirstPermutExtention)
                DistrbutionPlanXorKey()
                SBox0Propability()
                SBox1Propability()
                PermuteSBox()
                LeftXorRight(SBoxHolder, LeftSideHolder)
                DistrbutingFinalOutput()
                SecondRoundEcryption()
            Loop
        Else
            GenerateCounter()
            InitialToEncrypt()
            SendKey()
        End If
    End Sub

    Sub GenerateCounter()
        Dim crt As String
        If GeneratingLoopas = 1 Then
            GeneratingLoopas -= 1
            crt = TextBox55.Text
            For j = 0 To N - 1

                Dim temp As Integer
                Dim Str As String = ""
                Dim iv As Integer
                Dim result As Integer = 1
                For i = crt.Length - 1 To 0 Step -1
                    iv = Int32.Parse(crt(i)) + result
                    temp = Int32.Parse(iv) Mod 2
                    Str += temp.ToString()
                    result = iv \ 2
                Next i
                CounterHolder = StrReverse(Str)
                crt = CounterHolder
                ListBox3.Items.Add(CounterHolder.ToString())

            Next j
        Else

        End If
    End Sub
    Sub PlanToBinary()
        On Error Resume Next
        Dim Val As String = Nothing
        Dim Result As New System.Text.StringBuilder
        For Each Character As Byte In System.Text.ASCIIEncoding.ASCII.GetBytes(BitBox)
            Result.Append(System.Convert.ToString(Character, 2).PadLeft(8, "0"))
            Result.Append(" ")
        Next
        Val = Result.ToString.Substring(0, Result.ToString.Length - 1)
        ConvertedBitHolder = Val
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        FirstPermutToEncript()
    End Sub

    Sub FirstPermutToEncript()
        Dim h0, h1, h2, h3 As String
        h0 = TextBox79.Text
        h1 = TextBox78.Text
        h2 = TextBox66.Text
        h3 = TextBox65.Text
        TextBox80.Text = h3
        TextBox81.Text = h0
        TextBox82.Text = h1
        TextBox83.Text = h2
        TextBox84.Text = h1
        TextBox85.Text = h2
        TextBox86.Text = h3
        TextBox87.Text = h0
        FirstPermutExtention = h3 + h0 + h1 + h2 + h1 + h2 + h3 + h0
        Button12.Enabled = True
        Button17.Enabled = False

    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        PlanXorKey(KeyPreperedToEncrypt, FirstPermutExtention)
        DistrbutionPlanXorKey()
    End Sub

    Private Function PlanXorKey(ByVal bin1 As String, ByVal bin2 As String) As String
        Dim count As Integer
        Dim XORStr As String = ""
        For count = 0 To bin1.Length() - 1
            If bin1.Chars(count) = bin2.Chars(count) Then
                XORStr = XORStr + "0"
            ElseIf bin1.Chars(count) <> bin2.Chars(count) Then
                XORStr = XORStr + "1"
            End If
        Next count
        FirstXorHolder = XORStr
    End Function
    Sub DistrbutionPlanXorKey()
        On Error Resume Next
        Dim XorBitstBoxes() As TextBox = {TextBox96, TextBox97, TextBox98, TextBox99, TextBox100, TextBox101, TextBox102, TextBox103}
        Dim FirstName As String = FirstXorHolder
        For i = 0 To 7
            Dim TextB As String = FirstName.Chars(i)
            XorBitstBoxes(i).Text = TextB
        Next i
        Button12.Enabled = False
        Button14.Enabled = True
    End Sub
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        SBox0Propability()
        SBox1Propability()
    End Sub
    Sub SBox0Propability()
        Dim o0, o1, o2, o3 As String
        o0 = TextBox96.Text
        o1 = TextBox97.Text
        o2 = TextBox98.Text
        o3 = TextBox99.Text
        If o0 = 0 And o3 = 0 And o1 = 0 And o2 = 0 Then
            TextBox106.Text = 0
            TextBox107.Text = 1
        End If
        If o0 = 0 And o3 = 0 And o1 = 0 And o2 = 1 Then
            TextBox106.Text = 0
            TextBox107.Text = 0
        End If
        If o0 = 0 And o3 = 0 And o1 = 1 And o2 = 0 Then
            TextBox106.Text = 1
            TextBox107.Text = 1
        End If
        If o0 = 0 And o3 = 0 And o1 = 1 And o2 = 1 Then
            TextBox106.Text = 1
            TextBox107.Text = 0
        End If
        If o0 = 0 And o3 = 1 And o1 = 0 And o2 = 0 Then
            TextBox106.Text = 1
            TextBox107.Text = 1
        End If
        If o0 = 0 And o3 = 1 And o1 = 0 And o2 = 1 Then
            TextBox106.Text = 1
            TextBox107.Text = 0
        End If
        If o0 = 0 And o3 = 1 And o1 = 1 And o2 = 0 Then
            TextBox106.Text = 0
            TextBox107.Text = 1
        End If
        If o0 = 0 And o3 = 1 And o1 = 1 And o2 = 1 Then
            TextBox106.Text = 0
            TextBox107.Text = 0
        End If
        If o0 = 1 And o3 = 0 And o1 = 0 And o2 = 0 Then
            TextBox106.Text = 0
            TextBox107.Text = 0
        End If
        If o0 = 1 And o3 = 0 And o1 = 0 And o2 = 1 Then
            TextBox106.Text = 1
            TextBox107.Text = 0
        End If
        If o0 = 1 And o3 = 0 And o1 = 1 And o2 = 0 Then
            TextBox106.Text = 0
            TextBox107.Text = 1
        End If
        If o0 = 1 And o3 = 0 And o1 = 1 And o2 = 1 Then
            TextBox106.Text = 1
            TextBox107.Text = 1
        End If
        If o0 = 1 And o3 = 1 And o1 = 0 And o2 = 0 Then
            TextBox106.Text = 1
            TextBox107.Text = 1
        End If
        If o0 = 1 And o3 = 1 And o1 = 0 And o2 = 1 Then
            TextBox106.Text = 0
            TextBox107.Text = 1
        End If
        If o0 = 1 And o3 = 1 And o1 = 1 And o2 = 0 Then
            TextBox106.Text = 1
            TextBox107.Text = 1
        End If
        If o0 = 1 And o3 = 1 And o1 = 1 And o2 = 1 Then
            TextBox106.Text = 1
            TextBox107.Text = 0
        End If
    End Sub
    Sub SBox1Propability()
        Dim o4, o5, o6, o7 As String
        o4 = TextBox100.Text
        o5 = TextBox101.Text
        o6 = TextBox102.Text
        o7 = TextBox103.Text
        If o4 = 0 And o7 = 0 And o5 = 0 And o6 = 0 Then
            TextBox104.Text = 0
            TextBox105.Text = 0
        End If
        If o4 = 0 And o7 = 0 And o5 = 0 And o6 = 1 Then
            TextBox104.Text = 0
            TextBox105.Text = 1
        End If
        If o4 = 0 And o7 = 0 And o5 = 1 And o6 = 0 Then
            TextBox104.Text = 1
            TextBox105.Text = 0
        End If
        If o4 = 0 And o7 = 0 And o5 = 1 And o6 = 1 Then
            TextBox104.Text = 1
            TextBox105.Text = 1
        End If
        If o4 = 0 And o7 = 1 And o5 = 0 And o6 = 0 Then
            TextBox104.Text = 1
            TextBox105.Text = 0
        End If
        If o4 = 0 And o7 = 1 And o5 = 0 And o6 = 1 Then
            TextBox104.Text = 0
            TextBox105.Text = 0
        End If
        If o4 = 0 And o7 = 1 And o5 = 1 And o6 = 0 Then
            TextBox104.Text = 0
            TextBox105.Text = 1
        End If
        If o4 = 0 And o7 = 1 And o5 = 1 And o6 = 1 Then
            TextBox104.Text = 1
            TextBox105.Text = 1
        End If
        If o4 = 1 And o7 = 0 And o5 = 0 And o6 = 0 Then
            TextBox104.Text = 1
            TextBox105.Text = 1
        End If
        If o4 = 1 And o7 = 0 And o5 = 0 And o6 = 1 Then
            TextBox104.Text = 0
            TextBox105.Text = 0
        End If
        If o4 = 1 And o7 = 0 And o5 = 1 And o6 = 0 Then
            TextBox104.Text = 0
            TextBox105.Text = 1
        End If
        If o4 = 1 And o7 = 0 And o5 = 1 And o6 = 1 Then
            TextBox104.Text = 0
            TextBox105.Text = 0
        End If
        If o4 = 1 And o7 = 1 And o5 = 0 And o6 = 0 Then
            TextBox104.Text = 1
            TextBox105.Text = 0
        End If

        If o4 = 1 And o7 = 1 And o5 = 0 And o6 = 1 Then
            TextBox104.Text = 0
            TextBox105.Text = 1
        End If
        If o4 = 1 And o7 = 1 And o5 = 1 And o6 = 0 Then
            TextBox104.Text = 0
            TextBox105.Text = 0
        End If
        If o4 = 1 And o7 = 1 And o5 = 1 And o6 = 1 Then
            TextBox104.Text = 1
            TextBox105.Text = 1
        End If
        Button14.Enabled = False
        Button13.Enabled = True
    End Sub
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        PermuteSBox()
    End Sub
    Sub PermuteSBox()
        TextBox108.Text = TextBox107.Text
        TextBox109.Text = TextBox105.Text
        TextBox110.Text = TextBox104.Text
        TextBox111.Text = TextBox106.Text
        SBoxHolder = TextBox108.Text + TextBox109.Text + TextBox110.Text + TextBox111.Text
        Button13.Enabled = False
        Button15.Enabled = True
    End Sub
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        LeftXorRight(SBoxHolder, LeftSideHolder)
        DistrbutingFinalOutput()
    End Sub
    Private Function LeftXorRight(ByVal bin1 As String, ByVal bin2 As String) As String
        Dim count As Integer
        Dim XORStr As String = ""
        For count = 0 To bin1.Length() - 1
            If bin1.Chars(count) = bin2.Chars(count) Then
                XORStr = XORStr + "0"
            ElseIf bin1.Chars(count) <> bin2.Chars(count) Then
                XORStr = XORStr + "1"
            End If
        Next count
        FirstRoundXorHolder = XORStr
    End Function
    Sub DistrbutingFinalOutput()
        On Error Resume Next
        Dim textBoxes() As TextBox = {TextBox116, TextBox117, TextBox118, TextBox119}
        Dim textBoxes1() As TextBox = {TextBox120, TextBox121, TextBox122, TextBox123}
        Dim FirstName As String = FirstRoundXorHolder
        For i = 0 To 3
            Dim TextB As String = FirstName.Chars(i)
            textBoxes(i).Text = TextB
            textBoxes1(i).Text = TextB
        Next i
        Button15.Enabled = False
        Button16.Enabled = True
    End Sub
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        SecondRoundEcryption()
    End Sub
    Sub SecondRoundEcryption()

        TextBox1.Text = TextBox112.Text
        TextBox2.Text = TextBox113.Text
        TextBox60.Text = TextBox114.Text
        TextBox58.Text = TextBox115.Text
        TextBox79.Text = TextBox116.Text
        TextBox78.Text = TextBox117.Text
        TextBox66.Text = TextBox118.Text
        TextBox65.Text = TextBox119.Text
        LeftSideHolder = TextBox1.Text + TextBox2.Text + TextBox60.Text + TextBox58.Text
        Button16.Enabled = False
        Button9.Enabled = True
        SendFinalOutput()
        If LimitLoopManul = 0 Then
            Button16.Enabled = False
            MsgBox("Encryption Process Has Done")
            Button9.Enabled = False
        End If
        LimitLoopManul -= 1
    End Sub
    Sub SendFinalOutput()
        If RoundNumber = 0 Then
            '  TextBox29.Text += TextBox112.Text + TextBox113.Text + TextBox114.Text + TextBox115.Text + TextBox116.Text + TextBox117.Text + TextBox118.Text + TextBox119.Text + " "
            LastFinalOutput = TextBox112.Text + TextBox113.Text + TextBox114.Text + TextBox115.Text + TextBox116.Text + TextBox117.Text + TextBox118.Text + TextBox119.Text
            ListBox6.Items.Add(LastFinalOutput.ToString())

            ListBox4.Items.Add(LastFinalOutput.ToString())
        End If
    End Sub
    Private Sub TextBox41_TextChanged(sender As Object, e As EventArgs) Handles TextBox41.TextChanged
        Button6.Enabled = True
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        If RadioButton7.Checked = True Then

            TextBox51.Text = ""
            TextBox52.Text = ""

            For i = 0 To FinalDecriptionLoop - 1
                Dim KeyXorCounterEncrypt As String
                Dim CounterXorKeyEncrypt As String

                KeyXorCounterEncrypt = ListBox4.Items(i)
                CounterXorKeyEncrypt = ListBox5.Items(i)
                Dim textBoxes() As TextBox = {TextBox75, TextBox124, TextBox126, TextBox54, TextBox125, TextBox56, TextBox57, TextBox59}
                Dim textBoxes1() As TextBox = {TextBox130, TextBox132, TextBox133, TextBox127, TextBox134, TextBox128, TextBox129, TextBox131}
                Dim FirstName As String = ListBox4.Items(i)
                Dim SecondName As String = ListBox5.Items(i)

                For l = 0 To 7
                    Dim TextA As String = FirstName.Chars(l)
                    Dim TextB As String = SecondName.Chars(l)
                    textBoxes(l).Text = TextA
                    textBoxes1(l).Text = TextB
                Next l
                CipherXorCounter(KeyXorCounterEncrypt, CounterXorKeyEncrypt)
            Next

            ConvertCipherText()

            MsgBox("Your Decryption Process Done")
        Else

            If t = DecryptionLoop Then
                MsgBox("Your Decryption Process Done")

            Else
                Dim KeyXorCounterEncrypt As String
                Dim CounterXorKeyEncrypt As String
                KeyXorCounterEncrypt = ListBox4.Items(t)
                CounterXorKeyEncrypt = ListBox5.Items(t)
                Dim textBoxes() As TextBox = {TextBox75, TextBox124, TextBox126, TextBox54, TextBox125, TextBox56, TextBox57, TextBox59}
                Dim textBoxes1() As TextBox = {TextBox130, TextBox132, TextBox133, TextBox127, TextBox134, TextBox128, TextBox129, TextBox131}
                Dim FirstName As String = ListBox4.Items(t)
                Dim SecondName As String = ListBox5.Items(t)
                For l = 0 To 7
                    Dim TextA As String = FirstName.Chars(l)
                    Dim TextB As String = SecondName.Chars(l)
                    textBoxes(l).Text = TextA
                    textBoxes1(l).Text = TextB
                Next l
                CipherXorCounter(KeyXorCounterEncrypt, CounterXorKeyEncrypt)
                ConvertCipherText()
                t += 1
            End If
        End If

    End Sub
    Sub CipherTextConvert()

        Dim Val As String = Nothing
        Dim Characters As String = System.Text.RegularExpressions.Regex.Replace(TextBox29.Text, "[^01]", "")
        Dim ByteArray((Characters.Length / 8) - 1) As Byte
        For Index As Integer = 0 To ByteArray.Length - 1
            ByteArray(Index) = System.Convert.ToByte(Characters.Substring(Index * 8, 8), 2)
        Next
        Val = System.Text.ASCIIEncoding.ASCII.GetString(ByteArray)
        TextBox53.Text = Val
    End Sub
    Private Function PlanXorCounter(ByVal bin1 As String, ByVal bin2 As String) As String
        Dim count As Integer
        Dim XORStr As String = ""
        For count = 0 To bin1.Length() - 1
            If bin1.Chars(count) = bin2.Chars(count) Then
                XORStr = XORStr + "0"
            ElseIf bin1.Chars(count) <> bin2.Chars(count) Then
                XORStr = XORStr + "1"
            End If
        Next count
        ListBox5.Items.Add(XORStr.ToString())
        TextBox29.Text += XORStr + " "
    End Function
    Private Function CipherXorCounter(ByVal bin1 As String, ByVal bin2 As String) As String
        Dim count As Integer
        Dim XORStr As String = ""
        For count = 0 To bin1.Length() - 1
            If bin1.Chars(count) = bin2.Chars(count) Then
                XORStr = XORStr + "0"
            ElseIf bin1.Chars(count) <> bin2.Chars(count) Then
                XORStr = XORStr + "1"
            End If
        Next count
        TextBox51.Text += XORStr + " "
    End Function
    Sub ConvertCipherText()
        Dim Val As String = Nothing
        Dim Characters As String = System.Text.RegularExpressions.Regex.Replace(TextBox51.Text, "[^01]", "")
        Dim ByteArray((Characters.Length / 8) - 1) As Byte
        For Index As Integer = 0 To ByteArray.Length - 1
            ByteArray(Index) = System.Convert.ToByte(Characters.Substring(Index * 8, 8), 2)
        Next
        Val = System.Text.ASCIIEncoding.ASCII.GetString(ByteArray)
        TextBox52.Text = Val
        TextBox53.Text = Val
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        cnn.ConnectionString = constring3
        ds.EnforceConstraints = False
        cnn.Open()
        Dim str As String = "SELECT * FROM dtb ORDER BY ID"
        adapt = New OleDbDataAdapter(str, cnn)
        ds.Clear()
        adapt1.Fill(ds1, "dtb")
        BS.DataSource = ds
        BS1.DataMember = "dtb"
        ds.EnforceConstraints = True
        adapt.Dispose()
        adapt1.Dispose()
        cnn.Close()
        waitlist()
        filltable()
        filltable2()
        Me.Timer1.Interval = 1 * 20000
        Me.Timer1.Enabled = True
        cnn.Close()
    End Sub
    Private Sub TextBox28_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox28.TextChanged
        KnonceToBinary()
    End Sub

    Sub KnonceToBinary()
        On Error Resume Next
        Dim Val As String = Nothing
        Dim Result As New System.Text.StringBuilder
        For Each Character As Byte In System.Text.ASCIIEncoding.ASCII.GetBytes(TextBox28.Text)
            Result.Append(System.Convert.ToString(Character, 2).PadLeft(8, "0"))
            Result.Append(" ")
        Next
        Val = Result.ToString.Substring(0, Result.ToString.Length - 1)
        TextBox55.Text = Val
    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        waitlist()
        filltable()
        filltable2()
    End Sub
End Class
