using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    public static void Main()
    {
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Сервер запущен, ожидаю подключение...");

        // Ожидаем подключения клиента
        var client = listener.AcceptTcpClient();
        Console.WriteLine("Клиент подключился!");

        // Получаем IP-адрес клиента
        var clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        string clientIp = clientEndPoint.Address.ToString();
        Console.WriteLine($"IP-адрес клиента: {clientIp}");

        // Отправляем IP-адрес клиенту
        var stream = client.GetStream();
        byte[] ipBytes = Encoding.UTF8.GetBytes(clientIp);
        stream.Write(ipBytes, 0, ipBytes.Length);
        Console.WriteLine($"Отправлен IP-адрес: {clientIp}");

        // Закрытие соединения
        client.Close();
        listener.Stop();
    }
}
