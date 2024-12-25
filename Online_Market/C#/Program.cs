using System;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main()
    {
        Console.WriteLine("Подключение к магазину...");
        using (var client = new TcpClient("127.0.0.1", 5000))
        {
            using (var stream = client.GetStream())
            {
                var buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string menu = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine(menu);
                Console.Write("Введите название товара: ");
                string productName = Console.ReadLine();

                Console.Write("Введите количество: ");
                string quantity = Console.ReadLine();

                string request = $"{productName};{quantity}";
                byte[] requestData = Encoding.UTF8.GetBytes(request);
                stream.Write(requestData, 0, requestData.Length);
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine(response);
            }
        }
    }
}
