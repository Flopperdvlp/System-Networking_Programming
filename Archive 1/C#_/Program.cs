using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    static List<string> products = new List<string>
    {
        "Борщ", "Цезарь", "Плов", "Суп грибной", "Куриное филе", "Наполеон", "Компот"
    };

    public static void Main()
    {
        var listener = new TcpListener(IPAddress.Any, 5001);  // Это будет слушать все доступные интерфейсы
        listener.Start();
        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            var client = listener.AcceptTcpClient();
            Console.WriteLine("Клиент подключился.");
            using (var stream = client.GetStream())
            {
                string menu = "Меню магазина:\n" + string.Join("\n", products);
                byte[] menuData = Encoding.UTF8.GetBytes(menu);
                stream.Write(menuData, 0, menuData.Length);

                var buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string[] parts = request.Split(';');
                if (parts.Length != 2 || !int.TryParse(parts[1], out int quantity)) { continue; }
                string productName = parts[0];
                string response = $"Ваш заказ: {productName} x{quantity}";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);
            }
            client.Close();
        }
    }
}