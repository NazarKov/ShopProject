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
using ICSharpCode.SharpZipLib.Core;
using ShopProject.Helpers.SigningFileService.Model;

namespace ShopProject.Helpers.SigningFileService
{
    internal class SigningFileContoller
    {
        private readonly string _pathProcess= "..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe";
        private const string HOST = "127.0.0.1";
        private const int PORT = 8888;
        
        private static TcpClient tcpClient;
        private static NetworkStream networkStream;


        public void StartServise()
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
        private bool StopServise()
        {
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
        public bool IsStartServise()
        {
            Process[] processes = Process.GetProcessesByName("MiniServiceSigningFiles");
            if (processes.Length > 0)
            {
               return true;
            }
            return false;
        }

        public void ConnectService()
        {
            tcpClient = new TcpClient(HOST, PORT);
            networkStream = tcpClient.GetStream();
        }
        public bool IsConnectingServise()
        {
            if(tcpClient != null && tcpClient.Connected !=null)
            {

                if (networkStream != null)
                {
                    return true;
                }
            }
            return false;
        }

        public UserCommand SendingCommand(UserCommand command)
        {
            return Send(command.ToJson());
        }
        private UserCommand Send(string command)
        {
            networkStream = tcpClient.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(command);
            networkStream.WriteAsync(data, 0, data.Length);
            networkStream.FlushAsync();

            data = new byte[256];
            int bytesRead = networkStream.Read(data, 0, data.Length);

            return UserCommand.FromJson(Encoding.UTF8.GetString(data, 0, bytesRead));
        }
        public bool SignFiles(string pathFile, string passwordKey)
        {

            if (!IsConnectingServise())
            {
                if (!IsStartServise())
                {
                    StartServise();
                }
                ConnectService();
            }

            var result = SendingCommand(new UserCommand()
            {
                TypeCommand = TypeCommand.IsInitialize,
                Time = DateTime.Now,
            });

            if (result.Status == "404")
            {
                SendingCommand(new UserCommand()
                {
                    TypeCommand = TypeCommand.Initialize,
                    Time = DateTime.Now,
                });
            }
            result = SendingCommand(new UserCommand()
            {
                TypeCommand = TypeCommand.SingFile,
                PathKey = pathFile.ToString(),
                PasswordKey = passwordKey.ToString(),
                Time = DateTime.Now,
            });
            if (result.Status == "100")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
