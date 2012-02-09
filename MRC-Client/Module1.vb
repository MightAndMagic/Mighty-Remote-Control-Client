Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Module Module1
    Dim client As TcpClient = Nothing
    Dim stream As NetworkStream = Nothing
    Sub Main()
        Dim ip As IPAddress = Nothing
        Dim port As Integer = 27590
        Console.Title = "Mighty Remote Control Client"
        Try
            'ip = IPAddress.Parse(Console.ReadLine())
            ip = IPAddress.Parse("127.0.0.1")
        Catch e As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("IP Adresse konnte nicht gelesen werden. Fehlercode: {0}", e)
        End Try
        While True
            connect(ip, port)
        End While
    End Sub
    Sub connect(ip, port)
        Try
            client = New TcpClient()
            client.Connect(ip, port)
            Console.ForegroundColor = ConsoleColor.Gray
            stream = client.GetStream()
            Console.WriteLine("Mit Server verbunden auf: {0}.", client.Client.RemoteEndPoint)
            Send(stream)
        Catch e As SocketException
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("Verbindung konnte nicht hergestellt werden. Erneut versuchen...")
            connect(ip, port)
        End Try
    End Sub
    Sub Send(ByVal stream As NetworkStream)
        Dim bytes(128) As Byte
        Console.Write("Befehl eingeben:" & vbNewLine & "1 Open CD Tray" & vbNewLine & "2 Close CD Tray" & vbNewLine & "3 Remoteserver beenden" + vbNewLine + "Deine Auswahl: ")
        Try
            While client.Connected
                Dim input As String = Console.ReadLine()
                input = getFunction(input)
                If input <> Nothing Then
                    bytes = Encoding.Default.GetBytes(input)
                    stream.Write(bytes, 0, bytes.Length)
                End If
            End While
        Catch e As Exception
            Console.WriteLine("Verbindung geschlossen.")
        End Try
    End Sub
    Function getFunction(ByVal input As String)
        If input = "?" Then
            Console.WriteLine("Befehl eingeben:" & vbNewLine & "1 Open CD Tray" & vbNewLine & "2 Close CD Tray" & vbNewLine & "Deine Auswahl: ")
            Return Nothing
        ElseIf input = "1" Then
            Return "opC"
        ElseIf input = "2" Then
            Return "clC"
        ElseIf input = "3" Then
            Return "xt"
        Else
            Return input
        End If
    End Function
End Module
