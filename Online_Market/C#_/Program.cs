using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    static Dictionary<string, decimal> products = new Dictionary<string, decimal>
    {
        { "Борщ", 150m },
        { "Цезарь", 200m },
        { "Плов", 180m },
        { "Суп грибной", 160m },
        { "Куриное филе", 300m },
        { "Наполеон", 120m },
        { "Компот", 50m }
    };

    public static void Main()
    {
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Сервер запущен...");

        while (true)
        {
            var client = listener.AcceptTcpClient();
            Console.WriteLine("Клиент подключился.");

            using (var stream = client.GetStream())
            {
                string menu = "Меню магазина:\n" + string.Join("\n", products.Keys);
                byte[] menuData = Encoding.UTF8.GetBytes(menu);
                stream.Write(menuData, 0, menuData.Length);

                var buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string[] parts = request.Split(';');
                if (parts.Length != 2 || !int.TryParse(parts[1], out int quantity)) { continue; }

                string productName = parts[0];
                if (!products.ContainsKey(productName))
                {
                    string notFoundResponse = "Товар не найден.";
                    byte[] notFoundData = Encoding.UTF8.GetBytes(notFoundResponse);
                    stream.Write(notFoundData, 0, notFoundData.Length);
                }
                else
                {
                    decimal price = products[productName];
                    decimal total = price * quantity;
                    string response = $"Ваш заказ: {productName} x{quantity}, Сумма: {total}$";
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);
                }
            }

            client.Close();
        }
    }
}
