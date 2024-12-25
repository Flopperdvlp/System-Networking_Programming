using System;
using System.Net.Sockets;
using System.Text;

public class Client2
{
    public static void Main()
    {
        using (var client = new TcpClient("127.0.0.1", 5000))
        using (var stream = client.GetStream())
        {
            System.Threading.Thread receiveThread = new System.Threading.Thread(() => ReceiveMessages(stream));
            receiveThread.Start();
            receiveThread.Join();
        }
    }

    public static void ReceiveMessages(NetworkStream stream)
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0) break;

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Отримано повідомлення: " + message);
        }
    }
}