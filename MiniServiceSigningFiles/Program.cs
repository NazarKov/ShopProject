using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;
using EUSignCP;
using MiniServiceSigningFiles.Helpers.Command;
using System.Threading.Tasks;
using System.IO;

namespace MiniServiceSigningFiles
{
    internal class Program
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 8888;

        static async Task Main(string[] args)
        {
            byte[] response = new byte[25];
            byte[] data;
            int bytesRead;
            string command;
            // Встановлення IP-адреси та порту сервера
            IPAddress ipAddress = IPAddress.Parse(IP);

            // Створення TCP слухача
            TcpListener listener = new TcpListener(ipAddress, PORT);
            listener.Start();
            Console.WriteLine("Сервер запущений. Очікування підключень...");

 
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Клієнт підключений!");

            NetworkStream stream = client.GetStream();
          
            while (true)
            {
                data = new byte[256];
                bytesRead = stream.Read(data, 0, data.Length);
                command = Encoding.UTF8.GetString(data, 0, bytesRead);
                Console.WriteLine("Отримано від клієнта: " + command);

                switch (command.ToString())
                {
                    case "None":
                    {
                        break;
                    }
                    case "Initialize":
                    {
                        string error = Command.Initialize(false);
                        if (error == "OK")
                        {
                            send(error, response, stream);
                        }
                        else
                        {
                                send(error, response, stream);
                            }
                        break;
                    }
                    case "IsInitialize":
                    {
                        bool error = Command.IsInitialize();
                        if (error)
                        {
                            send(error.ToString(), response, stream);
                        }
                        else
                        {
                            send("Криптографічну бібліотеку не ініціалізовано.", response, stream);
                        }
                        break;
                    }
                    case "SingFile":
                    {
                        if (Command.SignFile())
                        {
                            send("Файл Успішно підписано", response, stream);
                        }
                        else
                        {
                            send("Помилка підпису", response, stream);
                        }
                        break;
                    }
                    case "Disconnect":
                    {
                        Command.Finalize();
                        send("Сервіс закінчив роботу", response, stream);
                        Environment.Exit(0);
                        break;
                    }
                }
                

            }

        }
        public static void send(string message, byte[] response,NetworkStream stream)
        {
            response = Encoding.UTF8.GetBytes(message);
            stream.WriteAsync(response, 0, response.Length);
            stream.FlushAsync();
            Console.WriteLine("Надіслано клієнту: "+message);
        }
    }
}
