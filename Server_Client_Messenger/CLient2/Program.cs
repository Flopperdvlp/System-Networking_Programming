using System;
using System.Net.Sockets;
using System.Text;

public class Client1
{
    public static void Main()
    {
        using (var client = new TcpClient("127.0.0.1", 5000))
        using (var stream = client.GetStream())
        {
            Console.WriteLine("Введіть ваше повідомлення:");

            while (true)
            {
                string message = Console.ReadLine();
                if (string.IsNullOrEmpty(message)) continue;

                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Повідомлення відправлено.");
            }
        }
    }
}