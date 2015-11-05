'|@|@|@|@|           |@|@|@|@|
'|@|@|@|@|   _____   |@|@|@|@|
'|@|@|@|@| /\_T_T_/\ |@|@|@|@|
'|@|@|@|@||/\ T T /\||@|@|@|@|
' ~/T~~T~||~\/~T~\/~||~T~~T\~
'  \|__|_| \(-(O)-)/ |_|__|/
'  _| _|    \\8_8//    |_ |_             GamePlayerBot
'|(@)]   /~~[_____]~~\   [(@)|              Version
'  ~    (  |       |  )    ~                  7.5
'      [~` ]       [ '~]            
'      |~~|         |~~|               
'      |  |         |  |                
'     _<\/>_       _<\/>_          
'    /_====_\     /_====_\             
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.IO
Public Class Form1
    Dim Core As String
    Sub getswf()
        Dim sourcecode As String = String.Empty
        Try
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://epicduel.artix.com/play-now/")
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            sourcecode = sr.ReadToEnd
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim Fst As Integer = sourcecode.IndexOf("http://epicduelcdn.artix.com/")
        Dim Snd As String = sourcecode.Substring(Fst, sourcecode.Length - Fst)
        sourcecode = Snd
        Dim Thrd As Integer = sourcecode.IndexOf("flashContent")
        Dim Frth As String = sourcecode.Substring(0, Thrd)
        Dim Final As String = Frth.Replace("""", "")
        Core = Final.Replace(",", "")
        TextBox9.Text = Core
        Flashplayer.LoadMovie(0, TextBox9.Text)
    End Sub
    Private Sub Win95Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button1.Click
        Flashplayer.LoadMovie(0, TextBox9.Text)
    End Sub
    Private Sub Win95Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button2.Click
        Try : Flashplayer.LoadMovie(0, "IdontExist.swf") : Catch : End Try
    End Sub
    Private Sub Win95Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button3.Click
        dl = New OpenFileDialog With {.FileName = "Bot Script.txt", .Filter = "Bot Scripts (*.txt*)|*.txt", .Title = "Open a Bot Script", .InitialDirectory = Application.StartupPath}
        If dl.ShowDialog() = Windows.Forms.DialogResult.OK Then RichTextBox1.LoadFile(dl.FileName, RichTextBoxStreamType.PlainText)
    End Sub
    Private Sub Win95Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button4.Click
        dlg = New SaveFileDialog With {.Filter = "Bot Scripts (*.txt*)|*.txt", .Title = "Save a Bot Script", .FileName = "Bot Script", .InitialDirectory = Application.StartupPath}
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then RichTextBox1.SaveFile(dlg.FileName, RichTextBoxStreamType.PlainText)
    End Sub
    Private Sub ListBox1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If String.IsNullOrEmpty(TextBox1.Text) Then TextBox1.Text = "0"
        If String.IsNullOrEmpty(TextBox2.Text) Then TextBox2.Text = "0"
        If String.IsNullOrEmpty(TextBox3.Text) Then TextBox3.Text = "1500"
        If String.IsNullOrEmpty(TextBox4.Text) Then TextBox4.Text = "250"
        Dim X As String = "'"
        If String.IsNullOrEmpty(TextBox5.Text) Then X = String.Empty
        If String.IsNullOrEmpty(RGBline.Text) Then RGBline.Text = "6699BB"
        Try
            If CheckBox7.Checked Then
                If CheckBox6.Checked = False Then
                    RichTextBox1.Text &= vbNewLine & PixLeftClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & RGBline.Text.ToUpper & Space & TextBox11.Text & Space & TextBox12.Text & Space & X & TextBox5.Text & vbNewLine
                Else
                    RichTextBox1.Text &= vbNewLine & PixRightClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & RGBline.Text.ToUpper & Space & TextBox11.Text & Space & TextBox12.Text & Space & X & TextBox5.Text & vbNewLine
                End If
            Else
                If CheckBox6.Checked = False Then
                    RichTextBox1.Text &= vbNewLine & LeftClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & Space & X & TextBox5.Text & vbNewLine
                Else
                    RichTextBox1.Text &= vbNewLine & RightClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & X & TextBox5.Text & vbNewLine
                End If
            End If
            MsgBox("Script added!", MsgBoxStyle.Exclamation)
        Catch ex As Exception : End Try
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try : Dim wordArr As String() = ListBox1.SelectedItem.ToString.Split(",") : TextBox1.Text = wordArr(0) : TextBox11.Text = wordArr(0) : TextBox12.Text = wordArr(1) : TextBox2.Text = wordArr(1) : RGBline.Text = wordArr(2)
        Catch ex As Exception : End Try
        Dim V As Color = ColorTranslator.FromHtml("#" & RGBline.Text) : Try : PictureBox3.BackColor = V
        Catch ex As Exception : MsgBox(ex) : End Try
    End Sub
    Private Sub Win95Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button5.Click
        PictureBox1.BackColor = Color.Empty
        PictureBox2.BackColor = Color.Empty
        Windows.Forms.Cursor.Position = New Point((Screen.PrimaryScreen.Bounds.Width - DesktopLocation.X) / 2, (Screen.PrimaryScreen.Bounds.Height - DesktopLocation.Y) / 2)
        If CheckBox3.Checked = True Then : Timer.Enabled = False : End If
        Loops = 0
        isRunning = True
        RunBackGround()
    End Sub
    Private Sub Win95Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button6.Click
        CancelWork()
        Timer.Enabled = True
    End Sub
    Private Sub Label11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label11.TextChanged
        If Me.Text = "False" Then : Me.ForeColor = Color.Red
        ElseIf Me.Text = "True" Then : Me.ForeColor = Color.Green
        End If
    End Sub
    Private Sub Win95Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button7.Click
        Try
            If TextBox7.Text = Nothing Then : TextBox7.Text = Me.Width
            Else : Me.Width = TextBox7.Text
            End If
            If TextBox8.Text = Nothing Then : TextBox8.Text = Me.Height
            Else : Me.Height = TextBox8.Text
            End If : Me.Refresh()
        Catch ex As Exception : End Try
    End Sub
    Private Sub Win95Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button8.Click
        Me.Location = New Point(0, 0)
        Windows.Forms.Cursor.Position = New Point((Screen.PrimaryScreen.Bounds.Width - DesktopLocation.X) / 2, (Screen.PrimaryScreen.Bounds.Height - DesktopLocation.Y) / 2)
    End Sub

    Private Sub Win95Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button9.Click
        getswf()
    End Sub

    Private Sub Win95Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button10.Click
        MsgBox("Tutorial for beginners" & vbNewLine & vbNewLine & "1) Hover over where you want to click" & vbNewLine & "2) Press the F1 key to Save the Coordinates" & vbNewLine & "3) Set the delays then double click the coordinates or click the button" & vbNewLine & "4) Once you have your Click positions setup in your Script click Start Bot or F2 to start botting" & vbNewLine & vbNewLine & "HotKeys" & vbNewLine & "F1 - Save Mouse Coordinates Position" & vbNewLine & "F2 - Start Bot" & vbNewLine & "F3 - Stop Bot" & vbNewLine & "F4 - Force Exit Program" & vbNewLine & vbNewLine & "Commands" & vbNewLine & "SENDKEY [KeyToSend] - Sends Specific Key" & vbNewLine & "ENTER - Inputs Enter Key" & vbNewLine & "END [NumberOfLoops] - Stops bot after script loops so many times" & vbNewLine & "CLICK [X] [Y] [MouseDown] [MouseUp]" & vbNewLine & "RCLICK [X] [Y] [MouseDown] [MouseUp] - Right Click" & vbNewLine & "PCLICK [X] [Y] [BeforeMouseMove] [AfterMouseUp] [ColorHexadecimal] [GetPixel X] [GetPixel Y] - Only Clicks if the location's pixel color is the RRGGBB value" & vbNewLine & "PRCLICK [X] [Y] [BeforeMouseMove] [AfterMouseUp] [ColorHexadecimal] [GetPixel X] [GetPixel Y] - Right Clicks if the location's pixel color is the same")
    End Sub

    Private Sub Win95Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button11.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then TextBox1.Text = "0"
        If String.IsNullOrEmpty(TextBox2.Text) Then TextBox2.Text = "0"
        If String.IsNullOrEmpty(TextBox3.Text) Then TextBox3.Text = "1500"
        If String.IsNullOrEmpty(TextBox4.Text) Then TextBox4.Text = "250"
        Dim X As String = "'"
        If String.IsNullOrEmpty(TextBox5.Text) Then X = String.Empty
        If String.IsNullOrEmpty(RGBline.Text) Then RGBline.Text = "6699BB"
        Try
            If CheckBox7.Checked Then
                If CheckBox6.Checked = False Then
                    RichTextBox1.Text &= vbNewLine & PixLeftClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & RGBline.Text.ToUpper & Space & TextBox11.Text & Space & TextBox12.Text & Space & X & TextBox5.Text & vbNewLine
                Else
                    RichTextBox1.Text &= vbNewLine & PixRightClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & RGBline.Text.ToUpper & Space & TextBox11.Text & Space & TextBox12.Text & Space & X & TextBox5.Text & vbNewLine
                End If
            Else
                If CheckBox6.Checked = False Then
                    RichTextBox1.Text &= vbNewLine & LeftClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & Space & X & TextBox5.Text & vbNewLine
                Else
                    RichTextBox1.Text &= vbNewLine & RightClick & Space & TextBox1.Text & Space & TextBox2.Text & Space & TextBox3.Text & Space & TextBox4.Text & Space & X & TextBox5.Text & vbNewLine
                End If
            End If
            MsgBox("Script added!", MsgBoxStyle.Exclamation)
        Catch ex As Exception : End Try
    End Sub
    Private Sub Win95Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Win95Button12.Click
        ListBox1.Items.Clear()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then : CheckBox2.Enabled = False
        Else : CheckBox2.Enabled = True
        End If
    End Sub
    Private Sub TextBox10_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox10.MouseClick
        Static B As Boolean = True
        If B = False Then
        Else
            MsgBox("Warning, changing Loop Delay will cause/fix impoper timing with delays") : B = False
        End If
    End Sub
#Region "Botstuff"
    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Const LEFTDOWN As Int32 = &H2, LEFTUP As Int32 = &H4, RIGHTDOWN As Int32 = &H8, RIGHTUP As Int32 = &H10, ABSOLUTE As Int32 = &H8000, MMOVE As Int32 = &H1
    Const Zero As Byte = 0, One As Byte = 1, Two As Byte = 2, Three As Byte = 3, Four As Byte = 4, Five As Byte = 5, Six As Byte = 6, Seven As Byte = 7, _
          Thou As Int16 = 1000, Space As Char = " ", PixLeftClick As String = "PCLICK", PixRightClick As String = "PRCLICK", LeftClick As String = "CLICK", RightClick As String = "RCLICK", EndCmd As String = "END", SendENT As String = "ENTER", SendKey As String = "SENDKEY", SendUp As String = "UP", SendDown As String = "DOWN", SendLeft As String = "LEFT", SendRight As String = "RIGHT"
    Private Property isRunning As Boolean
    Private arr() As String, RND As Random
    WithEvents BackGround As System.ComponentModel.BackgroundWorker
    Public Const WM_HOTKEY As Integer = &H312
    Public Const NULL As Integer = &H0
    Private Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Private Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
    Dim Loops As Integer = 0
    Dim CheckSave As Boolean = True
    Private Sub HoldKeyDown(ByVal key As Byte, ByVal durationInSeconds As Integer)
        Dim targetTime As DateTime = DateTime.Now().AddSeconds(durationInSeconds)
        Do : keybd_event(key, MapVirtualKey(key, 0), 0, 0)
        Loop While targetTime.Subtract(DateTime.Now()).TotalSeconds > 0
        keybd_event(key, MapVirtualKey(key, 0), 2, 0)
    End Sub
#Region "HotKeys"
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If CheckSave Then
            Dim ans As String
            ans = MsgBox("Are you sure you want to exit without saving?", vbYesNo)
            If ans = vbYes Then : Else
                dlg = New SaveFileDialog With {.Filter = "Bot Scripts (*.txt*)|*.txt", .Title = "Save a Bot Script", .FileName = "Bot Script", .InitialDirectory = Application.StartupPath}
                If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then RichTextBox1.SaveFile(dlg.FileName, RichTextBoxStreamType.PlainText)
            End If
        Else
        End If
    End Sub
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(0, 0)
        Call RegisterHotKey(Me.Handle, 100, NULL, Keys.F1) : Call RegisterHotKey(Me.Handle, 200, NULL, Keys.F2) : Call RegisterHotKey(Me.Handle, 300, NULL, Keys.F3) : Call RegisterHotKey(Me.Handle, 400, NULL, Keys.F4)
        TextBox7.Text = Me.Width
        TextBox8.Text = Me.Height
        Try
            getswf()
        Catch ex As Exception
            MsgBox("Problem fetching SWF :( ")
        End Try
        Windows.Forms.Cursor.Position = New Point((Screen.PrimaryScreen.Bounds.Width - DesktopLocation.X) / 2, (Screen.PrimaryScreen.Bounds.Height - DesktopLocation.Y) / 2)
        Me.Location = New Point(0, 0)
    End Sub
    Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        Call UnregisterHotKey(Me.Handle, 100) : Call UnregisterHotKey(Me.Handle, 200) : Call UnregisterHotKey(Me.Handle, 300) : Call UnregisterHotKey(Me.Handle, 400)
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "100" : Label1.ForeColor = Color.Yellow : Label1.Refresh() : TextBox1.Text = Cursor.Position.X : TextBox2.Text = Cursor.Position.Y : If CheckBox7.Checked = True Then : TextBox11.Text = Cursor.Position.X : TextBox12.Text = Cursor.Position.Y : End If : Windows.Forms.Cursor.Position = New Point(TextBox1.Text, TextBox2.Text) : Threading.Thread.Sleep(150) : Label1.ForeColor = Color.Black : If CheckBox7.Checked Then : GPTemp() : ListBox1.Items.Add(Cursor.Position.X & "," & Cursor.Position.Y & "," & RGBline.Text) : Else : ListBox1.Items.Add(Cursor.Position.X & "," & Cursor.Position.Y) : End If
                Case "200" : Windows.Forms.Cursor.Position = New Point((Screen.PrimaryScreen.Bounds.Width - DesktopLocation.X) / 2, (Screen.PrimaryScreen.Bounds.Height - DesktopLocation.Y) / 2) : isRunning = True : RunBackGround() : If CheckBox3.Checked = True Then : Timer.Enabled = False : End If : Loops = 0 : PictureBox1.BackColor = Color.Empty : PictureBox2.BackColor = Color.Empty
                Case "300" : CancelWork() : If Timer.Enabled = False Then : Timer.Enabled = True : End If : Me.Show() : GP()
                Case "400" : RichTextBox1.Dispose() : CheckSave = False : Application.Exit()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub
#End Region
    Dim dl As OpenFileDialog : Dim dlg As SaveFileDialog
    Dim c As Drawing.Color : Dim S As String
    Private Arrays As String()
    Private b As Boolean
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isRunning = False
        RND = New Random(System.DateTime.Now.Ticks Mod System.Int32.MaxValue)
        BackGround = New System.ComponentModel.BackgroundWorker() With {.WorkerSupportsCancellation = True}
        Me.Location = New Point(0, 0)
        Windows.Forms.Cursor.Position = New Point((Screen.PrimaryScreen.Bounds.Width - DesktopLocation.X) / 2, (Screen.PrimaryScreen.Bounds.Height - DesktopLocation.Y) / 2)
        Timer.Start()
    End Sub
    Sub GP()
        Dim a As New Drawing.Bitmap(1, 1)
        Dim b As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(a)
        b.CopyFromScreen(New Drawing.Point(MousePosition.X, MousePosition.Y), New Drawing.Point(0, 0), a.Size)
        PictureBox1.BackColor = a.GetPixel(0, 0)
        b.Dispose() : a.Dispose()
    End Sub
    Sub GPTemp()
        Dim rec As New Rectangle(MousePosition.X, MousePosition.Y, 1, 1)
        Dim a As New Drawing.Bitmap(1, 1)
        Dim b As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(a)
        b.CopyFromScreen(New Drawing.Point(MousePosition.X, MousePosition.Y), New Drawing.Point(0, 0), a.Size)
        Dim D As Color
        D = a.GetPixel(0, 0)
        Dim S As String = D.Name
        RGBline.Text = S.Substring(2)
        PictureBox3.BackColor = D
        b.Dispose() : a.Dispose()
    End Sub
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Label1.Text = Cursor.Position.X & "," & Cursor.Position.Y : Updater()
    End Sub
    Sub Updater()
        Label8.Text = Loops : Label16.Text = Flashplayer.Quality2
        If BackGround.IsBusy Then : Label11.ForeColor = Color.Green : Label11.Text = "ON!"
        Else : Label11.ForeColor = Color.Red : Label11.Text = "OFF!"
        End If : If CheckBox2.Checked Then : If Label16.Text = "Low" Then : Label16.ForeColor = Color.Green : Else : Label16.ForeColor = Color.OrangeRed : Flashplayer.Quality2 = "Low" : End If
        End If
    End Sub
    Sub Delay(ByVal Seconds As Integer)
        Threading.Thread.Sleep(Seconds)
    End Sub
    Sub CancelWork()
        PictureBox1.BackColor = Color.Empty
        PictureBox2.BackColor = Color.Empty
        If BackGround.IsBusy() Then BackGround.CancelAsync() : isRunning = False Else 
        Try : BackGround.CancelAsync() : isRunning = False
        Catch ex As Exception : isRunning = True
        End Try
    End Sub
    Sub RunBackGround()
        If BackGround.CancellationPending Then BackGround = New System.ComponentModel.BackgroundWorker() With {.WorkerSupportsCancellation = True}
        If isRunning And (Not BackGround.IsBusy) Then
            arr = {}
            arr = RichTextBox1.Lines
            b = CheckBox1.Checked
            BackGround.RunWorkerAsync({arr, b})
        Else : CancelWork()
        End If
    End Sub
    Sub Background_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackGround.DoWork 'Main Code BackgroundWorker, runs the maincode as well as handles it's status. 
        While (Not BackGround.CancellationPending) AndAlso (isRunning)
            RunCodeWith(e.Argument(0), e.Argument(1))
        End While
    End Sub
    Sub RunCodeWith(ByRef Lines As String(), ByVal isChecked As Boolean)
        If Lines.Count > 0 And isRunning Then
            For Each strLine As String In Lines
                arr = {}
                arr = strLine.Split(Space)
                Select Case arr(Zero)
                    Case PixLeftClick
                        Dim a As New Drawing.Bitmap(1, 1)
                        a.SetPixel(0, 0, ColorTranslator.FromHtml("#" & arr(Five)))
                        PictureBox2.BackColor = a.GetPixel(0, 0)
                        a.Dispose()
                        Try : If isChecked Then
                                Select RND.Next(Zero, Five)
                                    Case Zero : Windows.Forms.Cursor.Position = New Point(arr(One) + One, arr(Two)) : Case One : Windows.Forms.Cursor.Position = New Point(arr(One) - One, arr(Two)) : Case Two : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) + One) : Case Three : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) - One) : Case Four : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two))
                                End Select
                            Else : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two)) : End If
                            Dim a2 As New Drawing.Bitmap(1, 1) : Dim b2 As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(a2)
                            Delay(RND.Next(Zero, arr(Three))) : mouse_event(ABSOLUTE, arr(One), arr(Two), 0, 0)
                            Application.DoEvents()
                            b2.CopyFromScreen(New Drawing.Point(arr(Six), arr(Seven)), New Drawing.Point(0, 0), a2.Size)
                            PictureBox1.BackColor = a2.GetPixel(0, 0)
                            If PictureBox1.BackColor = PictureBox2.BackColor Then
                                mouse_event(LEFTDOWN + LEFTUP, Zero, Zero, Zero, One) : Delay(arr(Four))
                            Else
                                Delay(arr(Three)) : Delay(arr(Four))
                            End If
                            b2.Dispose() : a2.Dispose()
                            PictureBox1.Dispose()
                            PictureBox2.Dispose()
                        Catch ex As Exception : End Try
                    Case PixRightClick
                        Dim a As New Drawing.Bitmap(1, 1)
                        a.SetPixel(0, 0, ColorTranslator.FromHtml("#" & arr(Five)))
                        PictureBox2.BackColor = a.GetPixel(0, 0)
                        a.Dispose()
                        Try : If isChecked Then
                                Select Case RND.Next(Zero, Five)
                                    Case Zero : Windows.Forms.Cursor.Position = New Point(arr(One) + One, arr(Two)) : Case One : Windows.Forms.Cursor.Position = New Point(arr(One) - One, arr(Two)) : Case Two : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) + One) : Case Three : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) - One) : Case Four : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two))
                                End Select
                            Else : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two)) : End If
                            Dim a2 As New Drawing.Bitmap(1, 1) : Dim b2 As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(a2)
                            Delay(RND.Next(Zero, arr(Three))) : mouse_event(ABSOLUTE, arr(One), arr(Two), 0, 0)
                            Application.DoEvents()
                            b2.CopyFromScreen(New Drawing.Point(arr(Six), arr(Seven)), New Drawing.Point(0, 0), a2.Size)
                            PictureBox1.BackColor = a2.GetPixel(0, 0)
                            If PictureBox1.BackColor = PictureBox2.BackColor Then
                                mouse_event(RIGHTDOWN + RIGHTUP, Zero, Zero, Zero, One) : Delay(arr(Four))
                            Else
                                Delay(arr(Three)) : Delay(arr(Four))
                            End If
                            b2.Dispose() : a2.Dispose()
                            PictureBox1.Dispose()
                            PictureBox2.Dispose()
                        Catch ex As Exception : End Try
                    Case LeftClick
                        Try : If isChecked Then
                                Select Case RND.Next(Zero, Five)
                                    Case Zero : Windows.Forms.Cursor.Position = New Point(arr(One) + One, arr(Two))
                                    Case One : Windows.Forms.Cursor.Position = New Point(arr(One) - One, arr(Two))
                                    Case Two : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) + One)
                                    Case Three : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) - One)
                                    Case Four : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two))
                                End Select
                            Else
                                Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two))
                            End If
                            Delay(arr(Three)) : mouse_event(LEFTDOWN, Zero, Zero, Zero, One)
                            Delay(arr(Four)) : mouse_event(LEFTUP, Zero, Zero, Zero, One)
                        Catch ex As Exception : End Try
                    Case RightClick
                        Try : If isChecked Then
                                Select Case RND.Next(Zero, Five)
                                    Case Zero : Windows.Forms.Cursor.Position = New Point(arr(One) + One, arr(Two))
                                    Case One : Windows.Forms.Cursor.Position = New Point(arr(One) - One, arr(Two))
                                    Case Two : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) + One)
                                    Case Three : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two) - One)
                                    Case Four : Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two))
                                    Case Else
                                End Select
                            Else
                                Windows.Forms.Cursor.Position = New Point(arr(One), arr(Two))
                            End If
                            Delay(RND.Next(Zero, arr(Three))) : mouse_event(RIGHTDOWN, Zero, Zero, Zero, One)
                            Delay(RND.Next(Zero, arr(Four))) : mouse_event(RIGHTUP, Zero, Zero, Zero, One)
                        Catch ex As Exception
                        End Try
                    Case EndCmd : If Loops > arr(1) Then : isRunning = False : Application.DoEvents() : End If
                    Case SendENT : SendKeys.SendWait("{ENTER}")
                    Case SendUp : If CheckBox4.Checked = True Then : HoldKeyDown(Keys.Up, arr(1))
                        ElseIf CheckBox4.Checked = False Then : SendKeys.SendWait("{UP}") : End If
                    Case SendDown : If CheckBox4.Checked = True Then : HoldKeyDown(Keys.Down, arr(1))
                        ElseIf CheckBox4.Checked = False Then : SendKeys.SendWait("{DOWN}") : End If
                    Case SendLeft : If CheckBox4.Checked = True Then : HoldKeyDown(Keys.Left, arr(1))
                        ElseIf CheckBox4.Checked = False Then : SendKeys.SendWait("{LEFT}") : End If
                    Case SendRight : If CheckBox4.Checked = True Then : HoldKeyDown(Keys.Up, arr(1))
                        ElseIf CheckBox4.Checked = False Then : SendKeys.SendWait("{RIGHT}") : End If
                    Case SendKey : If strLine.StartsWith(SendKey) Then : Dim str As String = strLine.Remove(0, 8) : Dim arr() As String = str.Split(CChar(" "))
                            For L As Integer = 0 To arr.Length - 1
                                If L < arr.Length - 1 Then : SendKeys.SendWait(arr(L) & " ") : Else : SendKeys.SendWait(arr(L)) : End If
                            Next : End If
                    Case Else : End Select
                Delay(15)
                If isRunning = False Then : Exit For : End If : Next
            Loops += 1
            If CheckBox5.Checked = True Then
                If Loops > TextBox6.Text Then
                    CancelWork()
                End If : End If : Else
            CancelWork()
        End If
    End Sub
#End Region
End Class
