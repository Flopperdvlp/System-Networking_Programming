using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static void Main()
    {
        var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            // Подключаемся к серверу (локальный сервер)
            var serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            client.Connect(serverEndPoint);
            Console.WriteLine("Подключение к серверу установлено!");

            // Отправляем сообщение на сервер
            string message = "Привет, сервер!";
            client.Send(Encoding.UTF8.GetBytes(message));

            // Получаем поток данных для чтения
            NetworkStream stream = new NetworkStream(client);

            // Чтение IP-адреса, полученного от сервера
            var buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string receivedIp = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"IP-адрес сервера, полученный от клиента: {receivedIp}");

            // Чтение дополнительного сообщения от сервера
            bytesRead = stream.Read(buffer, 0, buffer.Length);
            string serverMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Сообщение от сервера: {serverMessage}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }
}
