Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
MustInherit Class Theme
    Inherits ContainerControl
#Region " Initialization "
    Protected G As Graphics
    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)
    End Sub
    Private ParentIsForm As Boolean
    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        Dock = DockStyle.Fill
        ParentIsForm = TypeOf Parent Is Form
        If ParentIsForm Then
            If Not _TransparencyKey = Color.Empty Then ParentForm.TransparencyKey = _TransparencyKey
            ParentForm.FormBorderStyle = FormBorderStyle.None
        End If
        MyBase.OnHandleCreated(e)
    End Sub
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal v As String)
            MyBase.Text = v
            Invalidate()
        End Set
    End Property
#End Region
#Region " Sizing and Movement "
    Private _Resizable As Boolean = True
    Property Resizable() As Boolean
        Get
            Return _Resizable
        End Get
        Set(ByVal value As Boolean)
            _Resizable = value
        End Set
    End Property

    Private _MoveHeight As Integer = 24
    Property MoveHeight() As Integer
        Get
            Return _MoveHeight
        End Get
        Set(ByVal v As Integer)
            _MoveHeight = v
            Header = New Rectangle(7, 7, Width - 14, _MoveHeight - 7)
        End Set
    End Property

    Private Flag As IntPtr
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If Not e.Button = MouseButtons.Left Then Return
        If ParentIsForm Then If ParentForm.WindowState = FormWindowState.Maximized Then Return

        If Header.Contains(e.Location) Then
            Flag = New IntPtr(2)
        ElseIf Current.Position = 0 Or Not _Resizable Then
            Return
        Else
            Flag = New IntPtr(Current.Position)
        End If

        Capture = False
        DefWndProc(Message.Create(Parent.Handle, 161, Flag, Nothing))

        MyBase.OnMouseDown(e)
    End Sub

    Private Structure Pointer
        ReadOnly Cursor As Cursor, Position As Byte
        Sub New(ByVal c As Cursor, ByVal p As Byte)
            Cursor = c
            Position = p
        End Sub
    End Structure

    Private F1, F2, F3, F4 As Boolean, PTC As Point
    Private Function GetPointer() As Pointer
        PTC = PointToClient(MousePosition)
        F1 = PTC.X < 7
        F2 = PTC.X > Width - 7
        F3 = PTC.Y < 7
        F4 = PTC.Y > Height - 7

        If F1 And F3 Then Return New Pointer(Cursors.SizeNWSE, 13)
        If F1 And F4 Then Return New Pointer(Cursors.SizeNESW, 16)
        If F2 And F3 Then Return New Pointer(Cursors.SizeNESW, 14)
        If F2 And F4 Then Return New Pointer(Cursors.SizeNWSE, 17)
        If F1 Then Return New Pointer(Cursors.SizeWE, 10)
        If F2 Then Return New Pointer(Cursors.SizeWE, 11)
        If F3 Then Return New Pointer(Cursors.SizeNS, 12)
        If F4 Then Return New Pointer(Cursors.SizeNS, 15)
        Return New Pointer(Cursors.Default, 0)
    End Function

    Private Current, Pending As Pointer
    Private Sub SetCurrent()
        Pending = GetPointer()
        If Current.Position = Pending.Position Then Return
        Current = GetPointer()
        Cursor = Current.Cursor
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If _Resizable Then SetCurrent()
        MyBase.OnMouseMove(e)
    End Sub

    Protected Header As Rectangle
    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        Header = New Rectangle(7, 7, Width - 14, _MoveHeight - 7)
        Invalidate()
        MyBase.OnSizeChanged(e)
    End Sub

#End Region

#Region " Convienence "

    MustOverride Sub PaintHook()
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        G = e.Graphics
        PaintHook()
    End Sub

    Private _TransparencyKey As Color
    Property TransparencyKey() As Color
        Get
            Return _TransparencyKey
        End Get
        Set(ByVal v As Color)
            _TransparencyKey = v
            Invalidate()
        End Set
    End Property
    Private _imgsize As Size
    Property Imagesize As Size
        Get
            Return _imgsize
        End Get
        Set(ByVal value As Size)
            _imgsize = value
        End Set
    End Property
    Private _Image As Image
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            Invalidate()
        End Set
    End Property
    ReadOnly Property ImageWidth() As Integer
        Get
            If _Image Is Nothing Then Return 0
            Return _Image.Width
        End Get
    End Property

    Private _Rectangle As Rectangle
    Private _Gradient As LinearGradientBrush
    Private _Brush As SolidBrush
    Private textSize As SizeF
    Private textX, textY As Integer
    Private textBrush As SolidBrush
    Private imageX, imageY As Integer

    Protected Sub DrawCorners(ByVal c As Color, ByVal rect As Rectangle)
        _Brush = New SolidBrush(c)
        G.FillRectangle(_Brush, rect.X, rect.Y, 1, 1)
        G.FillRectangle(_Brush, rect.X + (rect.Width - 1), rect.Y, 1, 1)
        G.FillRectangle(_Brush, rect.X, rect.Y + (rect.Height - 1), 1, 1)
        G.FillRectangle(_Brush, rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), 1, 1)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal P3 As Pen, ByVal p4 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, 1 - rect.X, 1 - rect.Y, 1 + rect.Width, 1 + rect.Height)
        G.DrawRectangle(p2, rect.X - 10, rect.Y - 10, rect.Width + 10, rect.Height + 10)
        G.DrawRectangle(P3, 2 - rect.X, 2 - rect.Y, 2 + rect.Width, 2 + rect.Height)
        G.DrawRectangle(p4, rect.X - 10, rect.Y - 10, rect.Width + 9, rect.Height + 9)
    End Sub
    Protected Sub DrawOutline(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub
    Protected Sub DrawText(ByVal c As Color, ByVal x As Integer, ByVal y As Integer)
        DrawText(HorizontalAlignment.Left, c, x, y)
    End Sub
    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer, ByVal y As Integer)
        If String.IsNullOrEmpty(Text) Then Return
        textSize = G.MeasureString(Text, Font).ToSize
        _Brush = New SolidBrush(c)
        x = textX
        G.DrawString(Text, Font, _Brush, x, y)
    End Sub
    Protected Sub DrawIcon()
        DrawIcon(HorizontalAlignment.Left)
    End Sub
    Protected Sub DrawIcon(ByVal a As HorizontalAlignment)
        textSize = New Size(140, 52)
        imageSize = New Size(28, 16)
        If Image IsNot Nothing Then
            imageX = 6
            imageY = 5
            textX = imageX + 32
            G.DrawImage(_Image, imageX, imageY, imageSize.Width, imageSize.Height)
        Else
            textX = 5
        End If
    End Sub

    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        _Rectangle = New Rectangle(x, y, width, height)
        _Gradient = New LinearGradientBrush(_Rectangle, c1, c2, angle)
        G.FillRectangle(_Gradient, _Rectangle)
    End Sub

#End Region

End Class
MustInherit Class ThemeControl
    Inherits Control

#Region " Initialization "

    Protected G As Graphics, B As Bitmap
    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)
        B = New Bitmap(1, 1)
        G = Graphics.FromImage(B)
    End Sub

    Sub AllowTransparent()
        SetStyle(ControlStyles.Opaque, False)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal v As String)
            MyBase.Text = v
            Invalidate()
        End Set
    End Property
#End Region

#Region " Mouse Handling "

    Protected Enum State As Byte
        MouseNone = 0
        MouseOver = 1
        MouseDown = 2
    End Enum

    Protected MouseState As State
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        ChangeMouseState(State.MouseNone)
        MyBase.OnMouseLeave(e)
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        ChangeMouseState(State.MouseOver)
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        ChangeMouseState(State.MouseOver)
        MyBase.OnMouseUp(e)
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then ChangeMouseState(State.MouseDown)
        MyBase.OnMouseDown(e)
    End Sub

    Private Sub ChangeMouseState(ByVal e As State)
        MouseState = e
        Invalidate()
    End Sub

#End Region

#Region " Convienence "

    MustOverride Sub PaintHook()
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        PaintHook()
        e.Graphics.DrawImage(B, 0, 0)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Not Width = 0 AndAlso Not Height = 0 Then
            B = New Bitmap(Width, Height)
            G = Graphics.FromImage(B)
            Invalidate()
        End If
        MyBase.OnSizeChanged(e)
    End Sub

    Private _NoRounding As Boolean
    Property NoRounding() As Boolean
        Get
            Return _NoRounding
        End Get
        Set(ByVal v As Boolean)
            _NoRounding = v
            Invalidate()
        End Set
    End Property
    Private _Image As Image
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            Invalidate()
        End Set
    End Property
    Private _ImgSize As Size
    Public Property ImageSize As size
        Get
            Return _ImgSize
        End Get
        Set(ByVal value As Size)
            _ImgSize = value
            Invalidate()
        End Set
    End Property
    ReadOnly Property ImageWidth() As Integer
        Get
            If _Image Is Nothing Then Return 0
            Return _Image.Width
        End Get
    End Property
    ReadOnly Property ImageTop() As Integer
        Get
            If _Image Is Nothing Then Return 0
            Return Height \ 2 - _Image.Height \ 2
        End Get
    End Property

    Private _Rectangle As Rectangle
    Private _Gradient As LinearGradientBrush
    Private _Brush As SolidBrush
    Private textSize As SizeF
    Private textX, textY As Integer
    Private textBrush As SolidBrush
    Private imageX, imageY As Integer

    Protected Sub DrawCorners(ByVal c As Color, ByVal rect As Rectangle)
        If _NoRounding Then Return
        B.SetPixel(rect.X, rect.Y, c)
        B.SetPixel(rect.X + (rect.Width - 1), rect.Y, c)
        B.SetPixel(rect.X, rect.Y + (rect.Height - 1), c)
        B.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c)
    End Sub

    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal P3 As Pen, ByVal p4 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, 1 - rect.X, 1 - rect.Y, 1 + rect.Width, 1 + rect.Height)
        G.DrawRectangle(p2, rect.X - 10, rect.Y - 10, rect.Width + 10, rect.Height + 10)
        G.DrawRectangle(P3, 2 - rect.X, 2 - rect.Y, 2 + rect.Width, 2 + rect.Height)
        G.DrawRectangle(p4, rect.X - 10, rect.Y - 10, rect.Width + 9, rect.Height + 9)
    End Sub
    Protected Sub DrawOutline(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub

    Protected Sub DrawText(ByVal c As Color, ByVal x As Integer, ByVal y As Integer)
        DrawText(HorizontalAlignment.Left, c, x, y)
    End Sub
    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer, ByVal y As Integer)
        If String.IsNullOrEmpty(Text) Then Return
        textSize = G.MeasureString(Text, Font).ToSize
        _Brush = New SolidBrush(c)
        x = textX + 1
        G.DrawString(Text, Font, _Brush, x, y)
    End Sub
    Protected Sub DrawIcon()
        DrawIcon(HorizontalAlignment.Left)
    End Sub
    Protected Sub DrawIcon(ByVal a As HorizontalAlignment)
        textSize = New Size(140, 52)
        imageSize = New Size(28, 16)
        If Image IsNot Nothing Then
            imageX = 6
            imageY = 5
            textX = imageX + 32
            G.DrawImage(_Image, imageX, imageY, imageSize.Width, imageSize.Height)
        Else
            textX = 5
        End If
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        _Rectangle = New Rectangle(x, y, width, height)
        _Gradient = New LinearGradientBrush(_Rectangle, c1, c2, angle)
        G.FillRectangle(_Gradient, _Rectangle)
    End Sub
#End Region
End Class
MustInherit Class ThemeContainerControl
    Inherits ContainerControl

#Region " Initialization "

    Protected G As Graphics, B As Bitmap
    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)
        B = New Bitmap(1, 1)
        G = Graphics.FromImage(B)
    End Sub

    Sub AllowTransparent()
        SetStyle(ControlStyles.Opaque, False)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

#End Region

#Region " Convienence "

    MustOverride Sub PaintHook()
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        PaintHook()
        e.Graphics.DrawImage(B, 0, 0)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Not Width = 0 AndAlso Not Height = 0 Then
            B = New Bitmap(Width, Height)
            G = Graphics.FromImage(B)
            Invalidate()
        End If
        MyBase.OnSizeChanged(e)
    End Sub
    Private _NoRounding As Boolean
    Property NoRounding() As Boolean
        Get
            Return _NoRounding
        End Get
        Set(ByVal v As Boolean)
            _NoRounding = v
            Invalidate()
        End Set
    End Property

    Private _Rectangle As Rectangle
    Private _Gradient As LinearGradientBrush

    Protected Sub DrawCorners(ByVal c As Color, ByVal rect As Rectangle)
        If _NoRounding Then Return
        B.SetPixel(rect.X, rect.Y, c)
        B.SetPixel(rect.X + (rect.Width - 1), rect.Y, c)
        B.SetPixel(rect.X, rect.Y + (rect.Height - 1), c)
        B.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c)
    End Sub

    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal P3 As Pen, ByVal p4 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, 1 - rect.X, 1 - rect.Y, 1 + rect.Width, 1 + rect.Height)
        G.DrawRectangle(p2, rect.X - 10, rect.Y - 10, rect.Width + 10, rect.Height + 10)
        G.DrawRectangle(P3, 2 - rect.X, 2 - rect.Y, 2 + rect.Width, 2 + rect.Height)
        G.DrawRectangle(p4, rect.X - 10, rect.Y - 10, rect.Width + 9, rect.Height + 9)
    End Sub
    Protected Sub DrawOutline(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub

    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        _Rectangle = New Rectangle(x, y, width, height)
        _Gradient = New LinearGradientBrush(_Rectangle, c1, c2, angle)
        G.FillRectangle(_Gradient, _Rectangle)
    End Sub
#End Region

End Class
Class Windows95Theme 'Coloring
    Inherits Theme
#Region "Properties"


#End Region
    Public WithEvents ExtBtn As New ControlButtons()
    Public WithEvents MaxBtn As New ControlButtons()
    Public WithEvents MiniBtn As New ControlButtons()
    Sub New()
        Me.Font = New Font("MS Sans Serif", 8, FontStyle.Bold)
        MyBase.Controls.Add(ExtBtn)
        MyBase.Controls.Add(MaxBtn)
        MyBase.Controls.Add(MiniBtn)
    End Sub
    Dim C1 As Color
    Dim C2 As Color
    Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
        MyBase.OnHandleCreated(e)
        AddHandler Me.ParentForm.Activated, AddressOf Parent_Activated
        AddHandler Me.ParentForm.Deactivate, AddressOf Parent_Deactivate
    End Sub

    Private Sub Parent_Activated(ByVal sender As Object, ByVal e As System.EventArgs)
        C1 = Color.FromArgb(10, 36, 106)
        C2 = Color.FromArgb(166, 202, 240)
        Me.Refresh()
    End Sub

    Private Sub Parent_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs)
        C1 = Color.FromArgb(128, 128, 128)
        C2 = Color.FromArgb(192, 192, 192)
        Me.Refresh()
    End Sub

    Dim MN As Integer = 1
    Public Overrides Sub PaintHook()

        Dim OL As New Pen(Color.FromArgb(255, 212, 208, 200), 1)
        Dim Marlett As New Font("marlett", 8, FontStyle.Regular)
        G.Clear(Color.FromArgb(212, 208, 200))


        DrawGradient(C1, C2, 0, 0, Width, 23, 30S)
        DrawIcon()
        DrawOutline(OL, OL, ClientRectangle)
        DrawBorders(Pens.White, Pens.DarkGray, Pens.Gainsboro, Pens.DimGray, ClientRectangle)

        DrawText(Color.White, 15 + ImageWidth, 6)
        ExtBtn.Left = MyBase.Width - 20 : ExtBtn.Top = 5
        ' ExtBtn.Text = "x" 'Regular Font
        ExtBtn.Text = "r" 'Marlett Font
        ExtBtn.Font = Marlett

        MaxBtn.Left = ExtBtn.Left - 18 : MaxBtn.Top = 5
        'MaxBtn.Text = "□" 'Regular Font
        MaxBtn.Text = MN 'Marlett Font
        MaxBtn.Font = Marlett

        MiniBtn.Left = MaxBtn.Left - 17 : MiniBtn.Top = 5
        ' MiniBtn.Text = "−" 'Use
        MiniBtn.Text = "0"
        MiniBtn.Font = Marlett
    End Sub
    Public Sub ExtBt_Click() Handles ExtBtn.Click
        MyBase.ParentForm.Close()
    End Sub
    Public Sub MiniBtn_Click() Handles MiniBtn.Click
        MyBase.ParentForm.WindowState = FormWindowState.Minimized
    End Sub
    Dim Max As Boolean = False
    Public Sub MaxBtn_Click() Handles MaxBtn.Click
        If Max = False Then
            MN = "2" 'Marlett Font
            MyBase.ParentForm.WindowState = FormWindowState.Maximized : Max = True
        Else
            MN = "1" 'Marlett Font
            MyBase.ParentForm.WindowState = FormWindowState.Normal : Max = False
        End If
    End Sub

End Class
Class ControlButtons
    Inherits ThemeControl
    Sub New()
        Me.Height = 15
        Me.Width = 16
    End Sub

    Public Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(0, 0, 0))
        Dim TL As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim BR As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim LTL As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim LBR As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim CR As Color
        Dim RS As New Pen(Color.FromArgb(255, 75, 75, 75))
        Select Case MouseState
            Case State.MouseNone
                TL = Pens.White
                LTL = Pens.Gainsboro
                LBR = Pens.DimGray
                BR = RS
                DrawGradient(Color.FromArgb(212, 208, 200), Color.FromArgb(212, 208, 200), 0, 0, Me.Width + 1, Me.Height + 1, 30)
            Case State.MouseDown
                TL = RS
                LTL = Pens.DimGray
                LBR = Pens.Gainsboro
                BR = Pens.White
                DrawGradient(Color.FromArgb(208, 204, 196), Color.FromArgb(208, 204, 196), 0, 0, Me.Width + 1, Me.Height + 1, 30)
            Case State.MouseOver
                Cursor = Cursors.Arrow
                TL = Pens.White
                LTL = Pens.Gainsboro
                LBR = Pens.DimGray
                BR = RS
                DrawGradient(Color.FromArgb(212, 208, 200), Color.FromArgb(212, 208, 200), 0, 0, Me.Width + 1, Me.Height + 1, 30)
        End Select
        DrawBorders(TL, BR, LTL, LBR, ClientRectangle) : CR = TL.Color
        DrawText(Color.Black, 15, 3)
    End Sub
End Class
Public Class Win95Button
    Inherits Control
    Public G As Graphics
    Private Rect As Rectangle
    Private Brush As SolidBrush
    Private Border As Pen
    Private textSize As SizeF
    Private textX, textY As Integer
    Private textBrush As SolidBrush
    Private imageSize As Size
    Private imageX, imageY As Integer
#Region " Mouse Handling "

    Protected Enum State As Byte
        MouseNone = 0
        MouseOver = 1
        MouseDown = 2
    End Enum

    Protected MouseState As State
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        ChangeMouseState(State.MouseNone)
        MyBase.OnMouseLeave(e)
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        ChangeMouseState(State.MouseOver)
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        ChangeMouseState(State.MouseOver)
        MyBase.OnMouseUp(e)
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then ChangeMouseState(State.MouseDown)
        MyBase.OnMouseDown(e)
    End Sub

    Private Sub ChangeMouseState(ByVal e As State)
        MouseState = e
        Invalidate()
    End Sub

#End Region
    Private _image As Image
    Public Property Image As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            _image = value
            Invalidate()
        End Set
    End Property
    Private _imageAlignment As HorizontalAlignment
    Public Property ImageAlignment As HorizontalAlignment
        Get
            Return _imageAlignment
        End Get
        Set(ByVal value As HorizontalAlignment)
            _imageAlignment = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.Selectable, False)
        BackColor = Color.FromArgb(212, 208, 200)
        Cursor = Cursors.Hand
        ForeColor = Color.FromArgb(5, 5, 5)
        Font = New Font("Arial", 10)
        Size = New Size(140, 52)
        imageSize = New Size(32, 32)
        Border = New Pen(Color.FromArgb(255, 212, 208, 200))
    End Sub
    Sub Borders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal P3 As Pen, ByVal p4 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, 1 - rect.X, 1 - rect.Y, 1 + rect.Width, 1 + rect.Height)
        G.DrawRectangle(p2, rect.X - 10, rect.Y - 10, rect.Width + 10, rect.Height + 10)
        G.DrawRectangle(P3, 2 - rect.X, 2 - rect.Y, 2 + rect.Width, 2 + rect.Height)
        G.DrawRectangle(p4, rect.X - 10, rect.Y - 10, rect.Width + 9, rect.Height + 9)
    End Sub
    Sub Outline(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

        G = e.Graphics
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim TL As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim BR As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim LTL As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim LBR As New Pen(Color.FromArgb(255, 0, 0, 0), 1)
        Dim RS As New Pen(Color.FromArgb(255, 75, 75, 75))
        Dim OL As New Pen(Color.FromArgb(255, 212, 208, 200), 1)
        Select Case MouseState
            Case State.MouseNone
                TL = Pens.White
                LTL = Pens.Gainsboro
                LBR = Pens.DimGray
                BR = RS
            Case State.MouseDown
                TL = RS
                LTL = Pens.DimGray
                LBR = Pens.Gainsboro
                BR = Pens.White
            Case State.MouseOver
                TL = Pens.White
                LTL = Pens.Gainsboro
                LBR = Pens.DimGray
                BR = RS
        End Select
        Outline(OL, OL, ClientRectangle)
        Borders(TL, BR, LTL, LBR, ClientRectangle)
        textSize = G.MeasureString(Text, Font)
        If Image IsNot Nothing Then
            Select Case _imageAlignment
                Case HorizontalAlignment.Left
                    imageX = 6
                    imageY = (Height / 2) - 16
                    textX = imageX + 32 + 4
                    textY = (Height / 2) - (textSize.Height / 2)
                Case HorizontalAlignment.Center
                    imageX = (Width / 2) - 16
                    imageY = (Height / 2) - (imageSize.Height / 2) - 2 - (textSize.Height / 2)
                    textX = (Width / 2) - (textSize.Width / 2)
                    textY = imageY + imageSize.Height + (textSize.Height / 2) - 4
                Case HorizontalAlignment.Right
                    imageX = Width - 6 - imageSize.Width - 1
                    imageY = (Height / 2) - (imageSize.Height / 2)
                    textX = imageX - 4 - (textSize.Width)
                    textY = (Height / 2) - (textSize.Height / 2)
            End Select
            G.DrawImage(Image, imageX, imageY, imageSize.Width, imageSize.Height)
        Else
            textX = (Width / 2) - (textSize.Width / 2)
            textY = (Height / 2) - (textSize.Height / 2)
        End If
        G.DrawString(Text, Font, textBrush, textX, textY)
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        textBrush = New SolidBrush(ForeColor)
        MyBase.OnForeColorChanged(e)
    End Sub
    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Size.Width < 36 Then Size = New Size(36, Size.Height) : If Size.Height < 36 Then Size = New Size(Size.Width, 36)
        Rect = New Rectangle(0, 0, Width - 1, Height - 1)
        MyBase.OnSizeChanged(e)
    End Sub
End Class