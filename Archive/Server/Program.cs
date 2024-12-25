using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Server
{
    static List<TcpClient> clients = new List<TcpClient>();

    public static void Main()
    {
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            var client = listener.AcceptTcpClient();
            Console.WriteLine("Клиент подключился.");
            clients.Add(client);

            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    public static void HandleClient(TcpClient client)
    {
        using (var stream = client.GetStream())
        {
            var buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Отримано повідомлення: " + message);

                foreach (var otherClient in clients)
                {
                    if (otherClient != client)
                    {
                        var otherStream = otherClient.GetStream();
                        byte[] responseData = Encoding.UTF8.GetBytes(message);
                        otherStream.Write(responseData, 0, responseData.Length);
                        break;
                    }
                }
            }
        }
    }
}