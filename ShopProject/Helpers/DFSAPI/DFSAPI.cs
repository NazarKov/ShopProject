using Google.Protobuf;
using Grpc.Net;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject;
using Grpc.Core;
using GreetClient;
using System.Windows;
using ShopProject.Helpers.MiniServiceSigningFile;

namespace ShopProject.Helpers.DFSAPI
{
    public class DFSAPI
    { 
        CallOptions callOptions;

        public DFSAPI() { }

        private ByteString ReadFile(string path)
        {
            byte[] bytes =File.ReadAllBytes(path);
            return ByteString.CopyFrom(bytes);
        }

        public void OpenShift()
        {
            long date = long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            XmlCheck xmlCheck = new XmlCheck();
            xmlCheck.writeOpenСhange("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml", date.ToString(), "");

            MainContoller mainContoller = new MainContoller();
            mainContoller.StartServise("..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe");
            mainContoller.ConnectService();
            mainContoller.SendingCommand(TypeCommand.Initialize);
            mainContoller.ReceivingResult();
            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();

            CheckResponse check = SendMessages(date, Check.Types.Type.Chk,0);
    

            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();
            mainContoller.SendingCommand(TypeCommand.Disconnect);
            SendMessages(date, Check.Types.Type.Servicechk,0);
        }
        public void SendChek()
        {
            long date = long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            XmlCheck xmlCheck = new XmlCheck();
            xmlCheck.writeOpenСhange("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml", date.ToString(), "");

            MainContoller mainContoller = new MainContoller();
            mainContoller.StartServise("..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe");
            mainContoller.ConnectService();
            mainContoller.SendingCommand(TypeCommand.Initialize);
            mainContoller.ReceivingResult();
            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();

            CheckResponse check = SendMessages(date, Check.Types.Type.Chk,0);


            date = long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            xmlCheck.writeOpenChek("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml", date.ToString(), check.ErrorMessage.Split(" ")[3].ToString());



            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();
            mainContoller.SendingCommand(TypeCommand.Disconnect);
            SendMessages(date, Check.Types.Type.Chk,0);
        }
        public void CloseShift()
        {
            long date = long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            XmlCheck xmlCheck = new XmlCheck();
            xmlCheck.writeOpenСhange("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml", date.ToString(),"");

            MainContoller mainContoller = new MainContoller();
            mainContoller.StartServise("..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe");
            mainContoller.ConnectService();
            mainContoller.SendingCommand(TypeCommand.Initialize);
            mainContoller.ReceivingResult();
            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();
      
            CheckResponse check = SendMessages(date,Check.Types.Type.Chk,0);


            date = long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"));
            xmlCheck.writeCloseCase("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml", date.ToString(), check.ErrorMessage.Split(" ")[3].ToString());

        

            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();
            mainContoller.SendingCommand(TypeCommand.Disconnect);
            SendMessages(date,Check.Types.Type.Zreport,0);
        }

        private CheckResponse SendMessages(long date,Check.Types.Type type , int localNumber)
        {
            using var channel = GrpcChannel.ForAddress("https://cabinet.tax.gov.ua:9443");
            callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(10));

            var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

            ByteString bytes = ReadFile("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml.p7s");

            var reply = client.sendChkV2(new Check()
            {
                CheckSign = bytes,
                CheckType = type,
                DateTime = date,
                RroFn = "4000512773",
                LocalNumber = localNumber
            }, callOptions);
            MessageBox.Show(reply.Status.ToString());
            return reply;
        }



        public void ping()
        {
            using var channel = GrpcChannel.ForAddress("https://cabinet.tax.gov.ua:9443");
            callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(10));

            var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

            var reply = client.ping(new Check()
            {
                CheckSign = ReadFile("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml.p7s"),
                CheckType = Check.Types.Type.Servicechk,
                DateTime = long.Parse("2023070314812"),
                RroFn = "4000512773",
                LocalNumber = 0
            }, callOptions);
            MessageBox.Show(reply.Status.ToString());
        }
        internal long СurrentCompDate()
        {
            DateTime now = DateTime.Now;
            return Convert.ToInt64(now.Year.ToString() + TwoSigns(now.Month.ToString()) + TwoSigns(now.Day.ToString()) + (now.Hour.ToString()) + (now.Minute.ToString()) + TwoSigns(now.Second.ToString()));
        }

        private string TwoSigns(string OneSigns)
        {
            if (OneSigns.Length < 2)
                OneSigns = "0" + OneSigns;
            return OneSigns;
        }
    }
}
