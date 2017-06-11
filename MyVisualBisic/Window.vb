﻿Namespace My

    ''' <summary>
    ''' 窗口管理、控制相关函数
    ''' </summary>
    ''' <remarks></remarks>
    Partial Public NotInheritable Class Window

        Private Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        Private Declare Function GetForegroundWindow Lib "user32.dll" Alias "GetForegroundWindow" () As IntPtr

        ''' <summary>
        ''' 获取系统焦点窗口的窗口句柄
        ''' </summary>
        ''' <returns>结果窗口句柄（IntPtr）</returns>
        ''' <remarks></remarks>
        Public Shared Function FindFocus() As IntPtr
            Return GetForegroundWindow()
        End Function
        ''' <summary>
        ''' 根据窗口标题，获取窗口句柄（当有多个标题相同的窗体存在时，默认获取上一个活动的窗体）
        ''' </summary>
        ''' <param name="Title">窗口标题（字符串必须完全相同）</param>
        ''' <returns>结果窗口句柄（IntPtr）</returns>
        ''' <remarks></remarks>
        Public Shared Function FindByTitle(ByVal Title As String) As IntPtr
            Return FindWindow(Nothing, Title)
        End Function

        Private Declare Function IsIconic Lib "user32.dll" Alias "IsIconic" (ByVal hWnd As IntPtr) As Boolean
        Private Declare Function IsZoomed Lib "user32.dll" Alias "IsZoomed" (ByVal hWnd As IntPtr) As Boolean

        ''' <summary>
        ''' 判断窗口是否获得了系统焦点
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否获得焦点</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckFocus(ByVal hWnd As IntPtr) As Boolean
            Return hWnd = GetForegroundWindow()
        End Function
        ''' <summary>
        ''' 判断窗口是否处于最小化状态
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否最小化</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckMinimized(ByVal hWnd As IntPtr) As Boolean
            Return IsIconic(hWnd)
        End Function
        ''' <summary>
        ''' 判断窗口是否处于最大化状态
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否最大化</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckMaximized(ByVal hWnd As IntPtr) As Boolean
            Return IsZoomed(hWnd)
        End Function

        Private Declare Function GetWindowRect Lib "user32.dll" Alias "GetWindowRect" (ByVal hWnd As IntPtr, ByRef lpRect As Rect) As Boolean
        Private Declare Function GetWindowPlacement Lib "user32.dll" Alias "GetWindowPlacement" (ByVal hWnd As IntPtr, ByRef lpwndpl As Placement) As Boolean
        Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As UInt32, ByVal wParam As Int32, ByVal lParam As System.Text.StringBuilder) As Int32
        Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As UInt32, ByVal wParam As Int32, Optional ByVal lParam As Object = Nothing) As Int32
        Private Structure Rect
            Dim Left As Int32
            Dim Top As Int32
            Dim Right As Int32
            Dim Bottom As Int32
        End Structure
        Private Structure Placement
            Dim length As UInt32
            Dim flags As UInt32
            Dim showCmd As UInt32
            Dim ptMinPosition As Point
            Dim ptMaxPosition As Point
            Dim rcNormalPosition As Rect
        End Structure
        <Flags()> _
        Private Enum WindowsMessage As UInt32
            Destory = 2
            Close = 8
            Quit = 10
            SetRedraw = 11
            SetText = 12
            GetText = 13
            GetTextLength = 14
        End Enum

        ''' <summary>
        ''' 获取窗口区域（大小和位置，即使窗口处于隐藏、最小化、最大化状态也能获取到）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>结果区域值（System.Drawing.Rectangle）</returns>
        ''' <remarks></remarks>
        Public Shared Function GetRectangle(ByVal hWnd As IntPtr) As System.Drawing.Rectangle
            Dim WindowRect As Rect
            If IsIconic(hWnd) Or IsZoomed(hWnd) Then
                Dim WindowPlacement As Placement
                GetWindowPlacement(hWnd, WindowPlacement)
                WindowRect = WindowPlacement.rcNormalPosition
            Else
                GetWindowRect(hWnd, WindowRect)
            End If
            Return New System.Drawing.Rectangle(WindowRect.Left, WindowRect.Top, WindowRect.Right - WindowRect.Left, WindowRect.Bottom - WindowRect.Top)
        End Function
        ''' <summary>
        ''' 获取窗口位置（即使窗口处于隐藏、最小化、最大化状态也能获取到）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>结果窗口位置值（System.Drawing.Point）</returns>
        ''' <remarks></remarks>
        Public Shared Function GetLocation(ByVal hWnd As IntPtr) As Point
            Dim WindowRect As Rect
            If IsIconic(hWnd) Or IsZoomed(hWnd) Then
                Dim WindowPlacement As Placement
                GetWindowPlacement(hWnd, WindowPlacement)
                WindowRect = WindowPlacement.rcNormalPosition
            Else
                GetWindowRect(hWnd, WindowRect)
            End If
            Return New System.Drawing.Point(WindowRect.Left, WindowRect.Top)
        End Function
        ''' <summary>
        ''' 获取窗口大小（即使窗口处于隐藏、最小化、最大化状态也能获取到）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>结果窗口大小值（System.Drawing.Size）</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSize(ByVal hWnd As IntPtr) As Size
            Dim WindowRect As Rect
            If IsIconic(hWnd) Or IsZoomed(hWnd) Then
                Dim WindowPlacement As Placement
                GetWindowPlacement(hWnd, WindowPlacement)
                WindowRect = WindowPlacement.rcNormalPosition
            Else
                GetWindowRect(hWnd, WindowRect)
            End If
            Return New System.Drawing.Size(WindowRect.Right - WindowRect.Left, WindowRect.Bottom - WindowRect.Top)
        End Function
        ''' <summary>
        ''' 获取窗口标题
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>结果字符串（失败返回空字符串）</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTitle(ByVal hWnd As IntPtr) As String
            Dim Length As Int32
            Length = SendMessage(hWnd, WindowsMessage.GetTextLength, 0) + 1
            Dim StringBuilder As New System.Text.StringBuilder(Length)
            SendMessage(hWnd, WindowsMessage.GetText, Length, StringBuilder)
            Return StringBuilder.ToString()
        End Function

        Private Declare Function SetWindowPos Lib "user32.dll" Alias "SetWindowPos" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Int32, ByVal Y As Int32, ByVal cx As Int32, ByVal cy As Int32, ByVal uFlags As UInt32) As Boolean
        Private Declare Function SetForegroundWindow Lib "user32.dll" Alias "SetForegroundWindow" (ByVal hWnd As IntPtr) As Boolean
        <Flags()> _
        Private Enum WindowPos As Int32
            Top = 0
            Bottom = 1
            TopMost = -1
            NoTopMost = -2
        End Enum
        <Flags()> _
        Private Enum SetPos As UInt32
            NoSize = 1 '忽略 cx、cy, 保持大小
            NoMove = 2 '忽略 X、Y, 保持位置
            NoZOrder = 4 '忽略 hWndInsertAfter, 保持窗口排列Z顺序
            NoRedraw = 8 '不重绘
            NoActivate = 16 '不激活，不改变窗口排列Z顺序
            FrameChanged = 32 '给窗口发送WM_NCCALCSIZE消息，即使窗口尺寸没有改变也会发送该消息。如果未指定这个标志，只有在改变了窗口尺寸时才发送WM_NCCALCSIZE。
            ShowWindow = 64 '显示窗口
            HideWindow = 128 '隐藏窗口
            AsyncWindowPos = 16384 '异步请求，不阻塞调用线程
        End Enum

        ''' <summary>
        ''' 设置窗口区域（即使窗口处于隐藏、最小化、最大化状态也能设置）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <param name="Rectangle">窗口区域（System.Drawing.Rectangle）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetRectangle(ByVal hWnd As IntPtr, ByVal Rectangle As System.Drawing.Rectangle) As Boolean
            Dim Result As Boolean
            If IsIconic(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height, SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMinimized)
            ElseIf IsZoomed(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height, SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMaximized)
            Else
                Result = SetWindowPos(hWnd, Nothing, Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height, SetPos.NoActivate)
            End If
            Return Result
        End Function
        ''' <summary>
        ''' 设置窗口位置（即使窗口处于隐藏、最小化、最大化状态也能设置）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <param name="Point">窗口相对于屏幕左上角的位置，Left和Top（System.Drawing.Point）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetLocation(ByVal hWnd As IntPtr, ByVal Point As Point) As Boolean
            Dim Result As Boolean
            If IsIconic(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, Point.X, Point.Y, 0, 0, SetPos.NoSize Or SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMinimized)
            ElseIf IsZoomed(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, Point.X, Point.Y, 0, 0, SetPos.NoSize Or SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMaximized)
            Else
                Result = SetWindowPos(hWnd, Nothing, Point.X, Point.Y, 0, 0, SetPos.NoSize Or SetPos.NoActivate)
            End If
            Return Result
        End Function
        ''' <summary>
        ''' 设置窗口居中（即使窗口处于隐藏、最小化、最大化状态也能设置，效果类似于StartPosition = FormStartPosition.CenterScreen）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetCenterScreen(ByVal hWnd As IntPtr) As Boolean
            Dim Screen As Rectangle = My.Computer.Screen.Bounds
            Dim WindowPlacement As Placement
            GetWindowPlacement(hWnd, WindowPlacement)
            Dim WindowRect As Rect = WindowPlacement.rcNormalPosition
            Dim Result As Boolean
            If IsIconic(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, (Screen.Width - (WindowRect.Right - WindowRect.Left)) / 2, (Screen.Height - (WindowRect.Bottom - WindowRect.Top)) / 2, 0, 0, SetPos.NoSize Or SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMinimized)
            ElseIf IsZoomed(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, (Screen.Width - (WindowRect.Right - WindowRect.Left)) / 2, (Screen.Height - (WindowRect.Bottom - WindowRect.Top)) / 2, 0, 0, SetPos.NoSize Or SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMaximized)
            Else
                Result = SetWindowPos(hWnd, Nothing, (Screen.Width - (WindowRect.Right - WindowRect.Left)) / 2, (Screen.Height - (WindowRect.Bottom - WindowRect.Top)) / 2, 0, 0, SetPos.NoSize Or SetPos.NoActivate)
            End If
            Return Result
        End Function
        ''' <summary>
        ''' 设置窗口大小（即使窗口处于隐藏、最小化、最大化状态也能设置）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <param name="Size">窗口大小，Width和Height（System.Drawing.Size）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetSize(ByVal hWnd As IntPtr, ByVal Size As Size) As Boolean
            Dim Result As Boolean
            If IsIconic(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, 0, 0, Size.Width, Size.Height, SetPos.NoMove Or SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMinimized)
            ElseIf IsZoomed(hWnd) Then
                ShowWindow(hWnd, ShowState.Hide)
                ShowWindow(hWnd, ShowState.ShowNormal)
                Result = SetWindowPos(hWnd, Nothing, 0, 0, Size.Width, Size.Height, SetPos.NoMove Or SetPos.NoActivate)
                ShowWindow(hWnd, ShowState.ShowMaximized)
            Else
                Result = SetWindowPos(hWnd, Nothing, 0, 0, Size.Width, Size.Height, SetPos.NoMove Or SetPos.NoActivate)
            End If
            Return Result
        End Function
        ''' <summary>
        ''' 显示窗口（不获得系统焦点）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal hWnd As IntPtr) As Boolean
            Return SetWindowPos(hWnd, Nothing, 0, 0, 0, 0, SetPos.ShowWindow Or SetPos.NoMove Or SetPos.NoSize Or SetPos.NoActivate)
        End Function
        ''' <summary>
        ''' 隐藏窗口（不获得系统焦点）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Hide(ByVal hWnd As IntPtr) As Boolean
            Return SetWindowPos(hWnd, Nothing, 0, 0, 0, 0, SetPos.HideWindow Or SetPos.NoMove Or SetPos.NoSize Or SetPos.NoActivate)
        End Function
        ''' <summary>
        ''' 设置窗口是否置顶（置顶窗口，显示在其它所有非置顶窗口之上）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <param name="TopMost">是否置顶</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetTopMost(ByVal hWnd As IntPtr, Optional ByVal TopMost As Boolean = True) As Boolean
            If TopMost Then
                Return SetWindowPos(hWnd, WindowPos.TopMost, 0, 0, 0, 0, SetPos.NoMove Or SetPos.NoSize Or SetPos.NoActivate)
            Else
                Return SetWindowPos(hWnd, WindowPos.NoTopMost, 0, 0, 0, 0, SetPos.NoMove Or SetPos.NoSize Or SetPos.NoActivate)
            End If
        End Function
        ''' <summary>
        ''' 设置窗口标题
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <param name="Title">窗口标题</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetTitle(ByVal hWnd As IntPtr, ByVal Title As String) As Boolean
            Dim StringBuilder As New System.Text.StringBuilder(Title)
            Return SendMessage(hWnd, WindowsMessage.SetText, StringBuilder.Length + 1, StringBuilder)
        End Function

        Private Declare Function ShowWindow Lib "user32.dll" Alias "ShowWindow" (ByVal hWnd As IntPtr, ByVal nCmdShow As UInt32) As Boolean
        <Flags()> _
        Private Enum ShowState As UInt32
            Hide = 0
            ShowNormal = 1
            ShowMinimized = 2
            ShowMaximized = 3
            ShowNoActivate = 4
            Restore = 9
        End Enum

        ''' <summary>
        ''' 还原显示窗口（效果类似于WindowState = FormWindowState.Normal，隐藏的窗口会显示出来，不获得系统焦点）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function ShowNormal(ByVal hWnd As IntPtr) As Boolean
            ShowWindow(hWnd, ShowState.Hide)
            Return ShowWindow(hWnd, ShowState.ShowNormal)
        End Function
        ''' <summary>
        ''' 最小化显示窗口（效果类似于WindowState = FormWindowState.Minimized，隐藏的窗口会显示出来，不获得系统焦点）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function ShowMinimized(ByVal hWnd As IntPtr) As Boolean
            ShowWindow(hWnd, ShowState.Hide)
            Return ShowWindow(hWnd, ShowState.ShowMinimized)
        End Function
        ''' <summary>
        ''' 最大化显示窗口（效果类似于WindowState = FormWindowState.Maximized，隐藏的窗口会显示出来，不获得系统焦点）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function ShowMaximized(ByVal hWnd As IntPtr) As Boolean
            ShowWindow(hWnd, ShowState.Hide)
            Return ShowWindow(hWnd, ShowState.ShowMaximized)
        End Function

        Private Declare Function GetWindowThreadProcessId Lib "user32.dll" Alias "GetWindowThreadProcessId" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As Int32) As Int32
        Private Declare Function AttachThreadInput Lib "user32.dll" Alias "AttachThreadInput" (ByVal idAttach As Int32, ByVal idAttachTo As Int32, ByVal fAttach As Boolean) As Boolean

        ''' <summary>
        ''' 使窗口获得系统焦点（隐藏的窗口会显示出来，最小化/最大化的窗口会还原，禁止重绘的窗口会允许重绘）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetFocus(ByVal hWnd As IntPtr) As Boolean
            Dim ForegroundThreadId As Int32
            Dim HandleThreadId As Int32
            Dim Result As Int32
            GetWindowThreadProcessId(GetForegroundWindow(), ForegroundThreadId)
            GetWindowThreadProcessId(hWnd, HandleThreadId)
            AttachThreadInput(HandleThreadId, ForegroundThreadId, True)
            ShowWindow(hWnd, ShowState.ShowNormal)
            SetWindowPos(hWnd, WindowPos.TopMost, 0, 0, 0, 0, SetPos.NoMove Or SetPos.NoSize)
            SetWindowPos(hWnd, WindowPos.NoTopMost, 0, 0, 0, 0, SetPos.NoMove Or SetPos.NoSize)
            Result = SetForegroundWindow(hWnd)
            AttachThreadInput(HandleThreadId, ForegroundThreadId, False)
            Return Result
        End Function

        ''' <summary>
        ''' 设置窗口是否允许重绘（禁止重绘后，窗口画面静止不变，可以减轻负荷，但是无法接收用户输入，此时用鼠标点击窗口区域，会点击到后面的窗口）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <param name="CanRedraw">是否允许重绘</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function SetCanRedraw(ByVal hWnd As IntPtr, Optional ByVal CanRedraw As Boolean = True) As Boolean
            If CanRedraw Then
                Return SendMessage(hWnd, WindowsMessage.SetRedraw, 1)
            Else
                Return SendMessage(hWnd, WindowsMessage.SetRedraw, 0)
            End If
        End Function

        Private Declare Function RedrawWindow Lib "user32.dll" Alias "RedrawWindow" (ByVal hWnd As IntPtr, lprcUpdate As Rect, ByVal hrgnUpdate As IntPtr, ByVal fuRedraw As UInt32) As Boolean
        <Flags()> _
        Private Enum Redraw As UInt32
            Invalidate = 1
            InternalPaint = 2
            DoErase = 4
            Validate = 8
            NoInternalPaint = 16
            NoErase = 32
            NoChildren = 64
            AllChildren = 128
            UpdateNow = 256
            EraseNow = 512
            Frame = 1024
            NoFrame = 2048
        End Enum

        ''' <summary>
        ''' 刷新窗口（更新一帧画面，禁止重绘的窗口仍然禁止重绘）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Refresh(ByVal hWnd As IntPtr) As Boolean
            Return RedrawWindow(hWnd, Nothing, Nothing, Redraw.Invalidate Or Redraw.DoErase Or Redraw.UpdateNow)
        End Function

        Private Declare Function GetWindowDC Lib "user32.dll" Alias "GetWindowDC" (ByVal hWnd As IntPtr) As IntPtr
        Private Declare Function ReleaseDC Lib "user32.dll" Alias "ReleaseDC" (ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Boolean
        Private Declare Function CreateCompatibleDC Lib "gdi32.dll" Alias "CreateCompatibleDC" (ByVal hDC As IntPtr) As IntPtr
        Private Declare Function DeleteDC Lib "gdi32.dll" Alias "DeleteDC" (ByVal hDC As IntPtr) As Boolean
        Private Declare Function CreateCompatibleBitmap Lib "gdi32.dll" Alias "CreateCompatibleBitmap" (ByVal hDC As IntPtr, ByVal nWidth As Int32, ByVal nHeight As Int32) As IntPtr
        Private Declare Function DeleteObject Lib "gdi32.dll" Alias "DeleteObject" (ByVal hObject As IntPtr) As Boolean
        Private Declare Function SelectObject Lib "gdi32.dll" Alias "SelectObject" (ByVal hDC As IntPtr, ByVal hgdiobj As IntPtr) As IntPtr
        Private Declare Function PrintWindow Lib "user32.dll" Alias "PrintWindow" (ByVal hWnd As IntPtr, ByVal hDCBlt As IntPtr, ByVal nFlags As UInt32) As Boolean

        ''' <summary>
        ''' 获取窗口截图（即使窗口处于屏幕外、被遮挡、最小化等状态，也能获取到）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>结果图片（失败返回1*1个像素，#00000000透明色的图片）</returns>
        ''' <remarks></remarks>
        Public Shared Function Image(ByVal hWnd As IntPtr) As Bitmap
            Try
                Dim WindowPlacement As Placement
                GetWindowPlacement(hWnd, WindowPlacement)
                Dim WindowRect As Rect = WindowPlacement.rcNormalPosition
                Dim SourceDC As IntPtr = GetWindowDC(hWnd)
                Dim SourceMemoryDC As IntPtr = CreateCompatibleDC(SourceDC)
                Dim TempBitmap As IntPtr = CreateCompatibleBitmap(SourceDC, WindowRect.Right - WindowRect.Left, WindowRect.Bottom - WindowRect.Top)
                SelectObject(SourceMemoryDC, TempBitmap)
                PrintWindow(hWnd, SourceMemoryDC, 0)
                Dim Result As Bitmap = Bitmap.FromHbitmap(TempBitmap)
                DeleteObject(TempBitmap)
                DeleteDC(SourceMemoryDC)
                ReleaseDC(hWnd, SourceDC)
                Return Result
            Catch ex As Exception
                Return New Bitmap(1, 1)
            End Try
        End Function

        Private Declare Function PostMessage Lib "user32.dll" Alias "PostMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As UInt32, Optional ByVal wParam As Object = Nothing, Optional ByVal lParam As Object = Nothing) As Boolean
        Private Declare Function DestroyWindow Lib "user32.dll" Alias "DestroyWindow" (ByVal hWnd As IntPtr) As Boolean

        ''' <summary>
        ''' 销毁窗口（可能会出现“5拒绝访问”错误而无效果，实测：可关闭计算器、记事本、GitHub等）
        ''' </summary>
        ''' <param name="hWnd">窗口句柄（IntPtr）</param>
        ''' <returns>是否执行成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Close(ByVal hWnd As IntPtr) As Boolean
            PostMessage(hWnd, WindowsMessage.Close)
            Dim Result As Boolean = DestroyWindow(hWnd)
            PostMessage(hWnd, WindowsMessage.Destory)
            Return Result
        End Function

    End Class

End Namespace