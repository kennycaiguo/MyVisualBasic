﻿Namespace My
    
    ''' <summary>
    ''' 屏幕截图相关函数
    ''' </summary>
    ''' <remarks></remarks>
    Partial Public NotInheritable Class Screen

        ''' <summary>
        ''' 获取屏幕截图（全屏区域的截图）
        ''' </summary>
        ''' <returns>结果图片（失败返回1*1个像素，#00000000透明色的图片）</returns>
        ''' <remarks></remarks>
        Public Shared Function Image() As Bitmap
            Try
                Dim ScreenArea As Rectangle = My.Computer.Screen.Bounds
                Dim Temp As New Bitmap(ScreenArea.Width, ScreenArea.Height)
                Graphics.FromImage(Temp).CopyFromScreen(0, 0, 0, 0, ScreenArea.Size)
                Return Temp
            Catch ex As Exception
                Return New Bitmap(1, 1)
            End Try
        End Function

        ''' <summary>
        ''' 获取屏幕截图（指定区域的截图）
        ''' </summary>
        ''' <param name="Area">指定区域</param>
        ''' <returns>结果图片（失败返回1*1个像素，#00000000透明色的图片）</returns>
        ''' <remarks></remarks>
        Public Shared Function Image(ByVal Area As Rectangle) As Bitmap
            Try
                Dim Temp As New Bitmap(Area.Width, Area.Height)
                Graphics.FromImage(Temp).CopyFromScreen(Area.Left, Area.Top, 0, 0, Area.Size)
                Return Temp
            Catch ex As Exception
                Return New Bitmap(1, 1)
            End Try
        End Function

        ''' <summary>
        ''' 获取屏幕截图（全屏区域的缩略图）
        ''' </summary>
        ''' <param name="Scale">缩略比例（应大于0，且小于等于1）</param>
        ''' <returns>结果图片（失败返回1*1个像素，#00000000透明色的图片）</returns>
        ''' <remarks></remarks>
        Public Shared Function ImageThumbnail(ByVal Scale As Double) As Bitmap
            If Scale <= 0 Or Scale > 1 Then
                Return New Bitmap(0, 0)
            End If
            Try
                Dim ScreenArea As Rectangle = My.Computer.Screen.Bounds
                Dim Temp As New Bitmap(ScreenArea.Width, ScreenArea.Height)
                Graphics.FromImage(Temp).CopyFromScreen(0, 0, 0, 0, ScreenArea.Size)
                Return Temp.GetThumbnailImage(ScreenArea.Width * Scale, ScreenArea.Height * Scale, Nothing, New System.IntPtr(0))
            Catch ex As Exception
                Return New Bitmap(1, 1)
            End Try
        End Function

        ''' <summary>
        ''' 获取屏幕截图（指定区域的缩略图）
        ''' </summary>
        ''' <param name="Area">指定区域</param>
        ''' <param name="Scale">缩略比例（应大于0，且小于等于1）</param>
        ''' <returns>结果图片（失败返回1*1个像素，#00000000透明色的图片）</returns>
        ''' <remarks></remarks>
        Public Shared Function ImageThumbnail(ByVal Area As Rectangle, ByVal Scale As Double) As Bitmap
            If Scale <= 0 Or Scale > 1 Then
                Return New Bitmap(0, 0)
            End If
            Try
                Dim Temp As New Bitmap(Area.Width, Area.Height)
                Graphics.FromImage(Temp).CopyFromScreen(0, 0, 0, 0, Area.Size)
                Return Temp.GetThumbnailImage(Area.Width * Scale, Area.Height * Scale, Nothing, New System.IntPtr(0))
            Catch ex As Exception
                Return New Bitmap(1, 1)
            End Try
        End Function

    End Class

End Namespace