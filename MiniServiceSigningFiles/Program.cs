using System;
using System.Net.Sockets;
using System.Net;
using System.Text;


using MiniServiceSigningFiles.Helpers;
using MiniServiceSigningFiles.Helpers.TcpJsonRCP;

namespace MiniServiceSigningFiles
{
    internal class Program
    {

        private string _pathProcess = "..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe";
        private const string IP = "127.0.0.1";
        private const int PORT = 8888;

        public static void Main(string[] args)
        {
            byte[] response = new byte[25];
            byte[] data;
            int bytesRead;
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
                if (stream == null)
                {
                    client = listener.AcceptTcpClient();
                    Console.WriteLine("Клієнт підключений!");

                    stream = client.GetStream();

                }
                data = new byte[256];
                bytesRead = stream.Read(data, 0, data.Length);
                UserCommand commands = UserCommand.FromJson(Encoding.UTF8.GetString(data, 0, bytesRead));
                Console.WriteLine(commands.ToJson());
                Console.WriteLine("Отримано від клієнта: " + commands.Description);

                switch (commands.TypeCommand)
                {
                    case TypeCommand.Ping:
                        {
                            Send(CommandExecute.Ping(),response,stream);
                            break;
                        }
                    case TypeCommand.Initialize:
                        {
                            Send(CommandExecute.Initialize(false), response, stream);
                            break;
                        }
                    case TypeCommand.IsInitialize:
                        {
                            Send(CommandExecute.IsInitialize(), response, stream);
                            break;
                        }
                    case TypeCommand.SingFile:
                        {
                            Send(CommandExecute.SignFile(commands.PathKey,commands.PasswordKey), response, stream);
                            break;
                        }
                    case TypeCommand.GetDataKey:
                        {
                            Send(CommandExecute.GetDataKey(commands.PathKey, commands.PasswordKey), response, stream);
                            break;
                        }
                    case TypeCommand.DisconnectUser:
                        {

                            Send(new UserCommand() { Status="OK", Description = "UserDisconnect", Time = DateTime.Now}, response, stream);
                            stream.Close();
                            client.Close();
                            stream = null;
                            break;
                        }
                }


            }

        }
        private static void Send(UserCommand messages, byte[] response,NetworkStream stream)
        {

            response = Encoding.UTF8.GetBytes(messages.ToJson());
            stream.WriteAsync(response, 0, response.Length);
            stream.FlushAsync();
            Console.WriteLine("Надіслано клієнту: "+messages.ToJson());
        }
      
    }
}
