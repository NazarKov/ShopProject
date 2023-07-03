using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers.MiniServiceSigningFile
{
    internal class MainContoller
    {
        private const string HOST = "127.0.0.1";
        private const int PORT = 8888;
        
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public void StartServise(string pathServise)
        { 
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = pathServise;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ConnectService()
        {
            tcpClient = new TcpClient(HOST, PORT);
            MessageBox.Show("Підключено");
            networkStream = tcpClient.GetStream();
        }
        public void SendingCommand(TypeCommand command)
        {
            switch(command)
            {
                case TypeCommand.None:
                    {
                        Send("None");
                        break;
                    }
                case TypeCommand.Initialize:
                    {
                        Send("Initialize");
                        break;
                    }
                case TypeCommand.IsInitialize:
                    {
                        Send("IsInitialize");
                        break;
                    }
                case TypeCommand.SingFile:
                    {
                        Send("SingFile");
                        break;
                    }
                case TypeCommand.Disconnect:
                    {
                        Send("Disconnect");
                        break;
                    }
            }
        }
        private void Send(string command)
        {
            networkStream = tcpClient.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(command);
            networkStream.WriteAsync(data, 0, data.Length);
            MessageBox.Show("Відправлено");
        }
        public async void ReceivingResult()
        {
            byte[] data = new byte[256];
            int bytesRead = networkStream.Read(data, 0, data.Length);
            string response = Encoding.UTF8.GetString(data, 0, bytesRead);
            MessageBox.Show("Повідомлення від сервера: " + response);
        }
        public void DisconnectServise()
        {
            tcpClient.Close();
            MessageBox.Show("Закрито");
        }


    }
}
