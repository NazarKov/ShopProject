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
using ShopProject.Model.ModelRepository;
using System.Xml;
using ShopProject.DataBase.Model;

namespace ShopProject.Helpers.DFSAPI
{
    public struct Messe
    {
        public string mac;
        public string id;
    }
     internal class DFSAPI
     {
        private string addres = "https://prro.tax.gov.ua:443 ";//
        private string testadress = "https://cabinet.tax.gov.ua:9443";//text

        private string pathxml = "C:\\Users\\lesak\\Source\\Repos\\NazarKov\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml";
        private string pathxml7ps = "C:\\Users\\lesak\\Source\\Repos\\NazarKov\\ShopProject\\ShopProject\\Resource\\BufferStorage\\Chek.xml.p7s";
        CallOptions callOptions;
        OrderXMLTableRepositories OrderXMLTableRepositories;

        public DFSAPI() 
        {
            OrderXMLTableRepositories = new OrderXMLTableRepositories();
        }

        private ByteString ReadFile(string path)
        {
            byte[] bytes =File.ReadAllBytes(path);
            return ByteString.CopyFrom(bytes);
        }

        public void OpenShift()
        {
            MainContoller mainContoller = new MainContoller();

            mainContoller.StartServise("..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe");
            mainContoller.ConnectService();
            mainContoller.SendingCommand(TypeCommand.Initialize);
            mainContoller.ReceivingResult();

            long date = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            XmlCheck xmlCheck = new XmlCheck();
            xmlCheck.writeOpenСhange(pathxml, date.ToString());

            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();

            CheckResponse check = SendMessages(date, Check.Types.Type.Servicechk, 0);
            if (check.Status == CheckResponse.Types.Status.Ok)
            {
                OrderXMLTableRepositories.Add(new DataBase.Model.OrderXML() { XMLString = DFSAPI.ReadXmlFileToString(pathxml) });
            }
            mainContoller.SendingCommand(TypeCommand.Disconnect);
            MessageBox.Show("зміна відкрита");
        }
        public Messe SendChek(List<Product> products, Order order, long date)
        {
            MainContoller mainContoller = new MainContoller();

            mainContoller.StartServise("..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe");
            mainContoller.ConnectService();
            mainContoller.SendingCommand(TypeCommand.Initialize);
            mainContoller.ReceivingResult();

            XmlCheck xmlCheck = new XmlCheck();
             
            string mac = xmlCheck.writeOpenChek(pathxml, date.ToString(),products,order);

            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();

            CheckResponse check = SendMessages(date, Check.Types.Type.Chk, 0);

            if (check.Status == CheckResponse.Types.Status.Ok)
            {
                OrderXMLTableRepositories.Add(new DataBase.Model.OrderXML() { XMLString = DFSAPI.ReadXmlFileToString(pathxml) });
            }
            mainContoller.SendingCommand(TypeCommand.Disconnect);
            return new Messe() { mac = mac, id = check.Id };
        }
        public void CloseShift(int count)
        {
            MainContoller mainContoller = new MainContoller();

            mainContoller.StartServise("..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe");
            mainContoller.ConnectService();
            mainContoller.SendingCommand(TypeCommand.Initialize);
            mainContoller.ReceivingResult();

            long date = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            XmlCheck xmlCheck = new XmlCheck();

            xmlCheck.writeCloseCase(pathxml, date.ToString(),count);
            mainContoller.SendingCommand(TypeCommand.SingFile);
            mainContoller.ReceivingResult();

            CheckResponse check = SendMessages(date, Check.Types.Type.Zreport, 0);
            if (check.Status == CheckResponse.Types.Status.Ok)
            {
                OrderXMLTableRepositories.Add(new DataBase.Model.OrderXML() { XMLString = DFSAPI.ReadXmlFileToString(pathxml) });
            }
            mainContoller.SendingCommand(TypeCommand.Disconnect);
        }

        private CheckResponse SendMessages(long date, Check.Types.Type type, int localNumber)
        {
            using var channel = GrpcChannel.ForAddress(addres);
            callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(10));

            var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

            ByteString bytes = ReadFile(pathxml7ps);


            var reply = client.sendChkV2(new Check()
            {
                CheckSign = bytes,
                CheckType = type,
                DateTime = date,
                RroFn = "4000512773",
                LocalNumber = localNumber,
            }, callOptions);
            if (reply.Status != CheckResponse.Types.Status.Ok)
            {
                MessageBox.Show(reply.Status.ToString());
            }
            return reply;
        }

        static string ReadXmlFileToString(string filePath)
        {
            // Загрузка XML из файла
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // Сохранение XML в строку
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.Formatting = Formatting.Indented;
            xmlDoc.WriteTo(xmlWriter);

            // Возвращение XML в виде строки
            return stringWriter.ToString();
        }



        public void ping()
        {
            using var channel = GrpcChannel.ForAddress(addres);
            callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(10));

            var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

            var reply = client.ping(new Check()
            {
                CheckSign = ReadFile(pathxml7ps),
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
