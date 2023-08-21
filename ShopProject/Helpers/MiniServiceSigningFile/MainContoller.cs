using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers.MiniServiceSigningFile
{
    internal class MainContoller
    {
        private string _pathProcess= "..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe";
        private const string HOST = "127.0.0.1";
        private const int PORT = 8888;
        
        private TcpClient tcpClient;
        private NetworkStream networkStream;


        public void StartServise()
        { 
            try
            {
                using (Process myProcess = new Process())
                {
                    if(StopServise())
                    {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = _pathProcess;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private bool StopServise()
        {
            DisconnectServise();
            Process[] processes = Process.GetProcessesByName("MiniServiceSigningFiles");
            if(processes.Length > 0)
            {
                foreach (Process process in processes)
                {
                    process.Kill();
                    return true;
                }
            }
            return true;
        }

        public void ConnectService()
        {
            tcpClient = new TcpClient(HOST, PORT);
            networkStream = tcpClient.GetStream();
        }
        public void SendingCommand(TypeCommand command)
        {
            switch(command)
            {
                case TypeCommand.None:
                    {
                        Send("None",false);
                        break;
                    }
                case TypeCommand.Initialize:
                    {
                        Send("Initialize", true);
                        break;
                    }
                case TypeCommand.IsInitialize:
                    {
                        Send("IsInitialize",true);
                        break;
                    }
                case TypeCommand.SingFile:
                    {
                        Send("SingFile", true);
                        break;
                    }
                case TypeCommand.Disconnect:
                    {
                        Send("Disconnect", false);
                        break;
                    }
            }
        }
        private void Send(string command,bool returnMessege)
        {
            networkStream = tcpClient.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(command);
            networkStream.WriteAsync(data, 0, data.Length);

            Thread.Sleep(500);
            if(returnMessege)
            {
                data = new byte[256];
                int bytesRead = networkStream.Read(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytesRead);

                //MessageBox.Show(response);
            }
        }
        public void DisconnectServise()
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
        }


    }
}
