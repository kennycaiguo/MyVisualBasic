﻿Namespace My
    
    ''' <summary>
    ''' 时间管理、转换相关函数
    ''' </summary>
    ''' <remarks></remarks>
    Partial Public NotInheritable Class Time

        ''' <summary>
        ''' 将当前线程挂起指定的时间（System.Threading.Thread.Sleep）
        ''' </summary>
        ''' <param name="Second">等待时间（单位秒，必须为非负值）</param>
        ''' <remarks></remarks>
        Public Shared Sub Wait(ByVal Second As Double)
            If Second <= 0 Then
                Return
            End If
            System.Threading.Thread.Sleep(Second * 1000)
        End Sub



        ''' <summary>
        ''' 获取UNIX时间戳
        ''' </summary>
        ''' <returns>UNIX时间戳整数</returns>
        ''' <remarks></remarks>
        Public Shared Function Stamp() As Long
            Return (Now.ToUniversalTime() - New DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds()
        End Function

    End Class

End Namespace