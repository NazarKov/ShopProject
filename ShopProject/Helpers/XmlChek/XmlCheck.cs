using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace ConsoleApp7
{
    internal class XmlCheck
    {

        Guid Uid { get; set; } // <!--Унікальний ідентифікатор документа (GUID)-->
        public string Tin { get; set; } //ЄДРПОУ/ДРФО/№ паспорта продавця (10 символів)-->
        public string Ipn { get; set; } // Податковий номер або Індивідуальний номер платника ПДВ (12 символів
        public string Orgnm { get; set; }//<!--Найменування продавця (256 символів)-->
        public string PointNM { get; set; }//<!--Найменування точки продажу (256 символів)-->
        public string PointAddR { get; set; }//<!--Адреса точки продажу (256 символів)-->
        public DateTime OrderDate { get; set; }// дата 
        public string OrderNum { get; set; }//<!--Локальний номер документа (128 символів)-->
        public int CashDeskNum { get; set; }//<!--Локальний номер реєстратора розрахункових операцій (64 символи)-->
        public string CashRegisterName { get; set; } //<!--Фіскальний номер реєстратора розрахункових операцій(128 символів)-->
        public string Cashier { get; set; }//<!--ПІБ касира (128 символів)-->
        public int Ver { get; set; }//<!--Версія формату документа (числовий)-->
        public string OrderTaxNum { get; set; }//<!--Фіскальний номер документа (128 символів)-->

   

        public XmlCheck()
        {
            DateTime dateTime = DateTime.Now;
            Uid = Guid.NewGuid();
            Tin = "2908905359";
            Ipn = "2908905359";
            Orgnm = "КОРНІЙЧУК СЕРГІЙ ВОЛОДИМИРОВИЧ";
            PointNM = "Магазин Дім рибалки";
            PointAddR = "Волинська область, Луцький район, м. Рожище, вул.Героїв Упа, 2а, підвал";
            OrderDate = DateTime.Now;
            OrderNum = "0";
            CashDeskNum = 2;
            CashRegisterName = "4000512773";
            Cashier = "КОРНІЙЧУК СЕРГІЙ ВОЛОДИМИРОВИЧ";
            Ver = 1;
            OrderTaxNum = "";
           
        }
        static string ComputeHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }

        public void writeOpenCase(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Створення XmlTextWriter з файловим потоком
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                // Встановлення форматування XML
                writer.Formatting = Formatting.Indented;

                // Початок документа XML
                writer.WriteStartDocument();

                // Встановлення кореневого елемента
                writer.WriteStartElement("RQ");
                writer.WriteAttributeString("V", "1");

                // Додавання елементів чеку
                writer.WriteStartElement("DAT");
                writer.WriteAttributeString("DI", "0");
                writer.WriteAttributeString("DT", "0");
                writer.WriteAttributeString("FN", CashRegisterName.ToString());
                writer.WriteAttributeString("TN", "ПН "+Tin);
                writer.WriteAttributeString("V", "1");
                writer.WriteAttributeString("ZN", "LV00000113");

                writer.WriteStartElement("C");
                writer.WriteAttributeString("T", "108");

                writer.WriteEndElement();
                long s = this.СurrentCompDate();
                writer.WriteElementString("TS", s.ToString());
                // Закриття всіх відкритих елементів
                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();
            }

            Console.WriteLine("Чек успішно записано в файл check.xml.");
        }

        public void writeOpenChek(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Створення XmlTextWriter з файловим потоком
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                // Встановлення форматування XML
                writer.Formatting = Formatting.Indented;

                // Початок документа XML
                writer.WriteStartDocument();

                writer.WriteStartElement("RQ");
                writer.WriteAttributeString("V", "1");

                // Додавання елементів чеку
                writer.WriteStartElement("DAT");
                writer.WriteAttributeString("DI", "0");
                writer.WriteAttributeString("DT", "0");
                writer.WriteAttributeString("FN", CashRegisterName.ToString());
                writer.WriteAttributeString("TN", "ПН " + Tin);
                writer.WriteAttributeString("V", "1");
                writer.WriteAttributeString("ZN", "LV00000113");
                writer.WriteStartElement("C");
                writer.WriteAttributeString("T", "0");

                //writer.WriteStartElement("L");
                //writer.WriteAttributeString("N", "1");
                //writer.WriteString("Comment");
                //writer.WriteEndElement();


                //writer.WriteStartElement("P");
                //writer.WriteAttributeString("N", "3");
                //writer.WriteAttributeString("C", "33");
                //writer.WriteAttributeString("NM", "3");
                //writer.WriteAttributeString("SM", "3");
                //writer.WriteAttributeString("TX", "3");
                //writer.WriteEndElement();

                //writer.WriteStartElement("D");
                //writer.WriteAttributeString("N", "4");
                //writer.WriteAttributeString("TR", "33");
                //writer.WriteAttributeString("TY", "3");
                //writer.WriteAttributeString("NI", "3");
                //writer.WriteAttributeString("TX", "3");
                //writer.WriteAttributeString("SM", "3");
                //writer.WriteEndElement();

                //writer.WriteStartElement("M");
                //writer.WriteAttributeString("N", "5");
                //writer.WriteAttributeString("T", "33");
                //writer.WriteAttributeString("NM", "3");
                //writer.WriteAttributeString("SM", "3");
                //writer.WriteAttributeString("M", "3");
                //writer.WriteAttributeString("RM", "3");
                //writer.WriteEndElement();

                //writer.WriteStartElement("E");
                //writer.WriteAttributeString("N", "6");
                //writer.WriteAttributeString("NO", "33");
                //writer.WriteAttributeString("SM", "3");
                //writer.WriteAttributeString("SE", "3");
                //writer.WriteAttributeString("FN", "3");
                //writer.WriteAttributeString("TS", "3");
                //writer.WriteAttributeString("TX", "3");
                //writer.WriteAttributeString("TXPR", "3");
                //writer.WriteAttributeString("TXSM", "3");
                //writer.WriteAttributeString("DTPR", "3");
                //writer.WriteAttributeString("DTSM", "3");
                //writer.WriteAttributeString("TXTY", "3");
                //writer.WriteAttributeString("TXAL", "3");
                //writer.WriteAttributeString("CS", "3");

                ////writer.WriteEndElement();





                writer.WriteEndElement();
                long s = this.СurrentCompDate();
                writer.WriteElementString("TS", s.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("MAC");
                writer.WriteAttributeString("DI", "0");
                writer.WriteAttributeString("NT", "1");
                string str = $"<DAT DI=\"0\" DT=\"0\" FN=\"{CashRegisterName}\" TN=\"ПН {Tin}\" V=\"1\" ZN=\"LV00000113\"><C T=\"0\" /><TS>{s}</TS></DAT>";
       
                byte[] mac = CalculateMAC(str);
                string macBase64 = Convert.ToBase64String(mac);

                Console.WriteLine("MAC (Base64): " + macBase64);

                writer.WriteBase64(mac,0,mac.Length);

                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();
            }

            Console.WriteLine("Чек успішно записано в файл check.xml.");
        }

        static byte[] CalculateMAC(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] hash = sha256.ComputeHash(dataBytes);

                return hash;
            }
        }


      

        public void writeCloseCase(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Створення XmlTextWriter з файловим потоком
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                // Встановлення форматування XML
                writer.Formatting = Formatting.Indented;

                // Початок документа XML
                writer.WriteStartDocument();

                // Встановлення кореневого елемента
                writer.WriteStartElement("RQ");
                writer.WriteAttributeString("V", "1");

                // Додавання елементів чеку
                writer.WriteStartElement("DAT");
                writer.WriteAttributeString("DI", "0");
                writer.WriteAttributeString("DT", "0");
                writer.WriteAttributeString("FN", CashRegisterName.ToString());
                writer.WriteAttributeString("TN", "ПН " + Tin);
                writer.WriteAttributeString("V", "1");
                writer.WriteAttributeString("ZN", "LV00000113");

                writer.WriteStartElement("Z");
                writer.WriteAttributeString("NO", "0");

                writer.WriteStartElement("TXS");
                writer.WriteAttributeString("TX", "0");
                writer.WriteAttributeString("SMI", "0");
                writer.WriteEndElement();

                writer.WriteStartElement("TXS");
                writer.WriteAttributeString("TS", "0");
                writer.WriteAttributeString("TSPR", "0");
                writer.WriteAttributeString("TXI", "0");
                writer.WriteAttributeString("TXO", "0");
                writer.WriteAttributeString("DTPR", "0");

                writer.WriteAttributeString("DTI", "0");
                writer.WriteAttributeString("DTO", "0");
                writer.WriteAttributeString("TXTY", "0");
                writer.WriteAttributeString("TXAL", "0");
                writer.WriteAttributeString("SMI", "0");
                writer.WriteAttributeString("SMO", "0");
                writer.WriteEndElement();

                writer.WriteStartElement("M");
                writer.WriteAttributeString("NM", "0");
                writer.WriteAttributeString("SMI", "0");
                writer.WriteAttributeString("SMO", "0");
                writer.WriteEndElement();

                writer.WriteStartElement("IO");
                writer.WriteAttributeString("NM", "0");
                writer.WriteAttributeString("SMI", "0");
                writer.WriteAttributeString("SMO", "0");
                writer.WriteEndElement();

                writer.WriteStartElement("NC");
                writer.WriteAttributeString("NI", "0");
                writer.WriteAttributeString("NO", "0");
                writer.WriteEndElement();

                writer.WriteEndElement();
                long s = this.СurrentCompDate();
                writer.WriteElementString("TS", s.ToString());
                writer.WriteElementString("MAC", "0");
                // Закриття всіх відкритих елементів
                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();
            }

            Console.WriteLine("Чек успішно записано в файл check.xml.");
        }
        internal  long СurrentCompDate()
        {
            DateTime now = DateTime.Now;
            return Convert.ToInt64(now.Year.ToString() + TwoSigns(now.Month.ToString()) + TwoSigns(now.Day.ToString()) + (now.Hour.ToString()) + (now.Minute.ToString()) + TwoSigns(now.Second.ToString()));
        }

        private  string TwoSigns(string OneSigns)
        {
            if (OneSigns.Length < 2)
                OneSigns = "0" + OneSigns;
            return OneSigns;
        }
    }
}
    
    

