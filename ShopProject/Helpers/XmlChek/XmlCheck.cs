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
using ShopProject.Helpers;
using ShopProject.Model.ModelRepository;
using ShopProject.DataBase.Model;
using NPOI.SS.Formula.Functions;

namespace ShopProject
{
    internal class XmlCheck
    {
        OrderXMLTableRepositories OrderXMLTableRepositories { get; set; }

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
            OrderXMLTableRepositories = new OrderXMLTableRepositories();
        }

        public void writeOpenСhange(string path,string time)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            XDocument doc = XDocument.Parse(OrderXMLTableRepositories.LastXML().XMLString.ToString());
            doc.Save("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\buffers.xml");//при пустій базі даних потрібно не вмикати викликає помилку

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
                writer.WriteElementString("TS", time);
                writer.WriteElementString("MAC",SHA.GenerateSHA256File("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\buffers.xml"));
                // Закриття всіх відкритих елементів
                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();
            }
        }

        public string writeOpenChek(string path,string time,List<Product> products , Order order)
        {
            XDocument doc = XDocument.Parse(OrderXMLTableRepositories.LastXML().XMLString.ToString());
            doc.Save("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\buffers.xml");

            string mac = SHA.GenerateSHA256File("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\buffers.xml");

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


                for(int i = 0; i<products.Count;i++)
                {
                      writer.WriteStartElement("P");//продажа
                        writer.WriteAttributeString("N", (i + 1).ToString());//порядковий номер 
                        writer.WriteAttributeString("C", "9507");//код товару
                        writer.WriteAttributeString("CD", products[i].code.ToString());//штрихкод товару
                        writer.WriteAttributeString("NM", products[i].name);//назва товару або послуги
                        writer.WriteAttributeString("SM", Convert.ToDecimal(products[i].price).ToString());//Сума операції
                        writer.WriteAttributeString("Q",  Convert.ToDecimal(products[i].count).ToString());//кількість товару
                        writer.WriteAttributeString("PRC",Convert.ToDecimal(products[i].price).ToString());//Ціна товару
                        writer.WriteAttributeString("TX", "0");//податок
                        writer.WriteEndElement();
                }       

                writer.WriteStartElement("M");//оплата
                writer.WriteAttributeString("N", (products.Count+1).ToString());//порядковий номер 
                writer.WriteAttributeString("T", "0");//тип опалати
                writer.WriteAttributeString("SM", order.userSuma.ToString());//Сума до оплати що вноситьця покупцем
                writer.WriteAttributeString("RM", order.rest.ToString());//Решта якщо немає то невказується;
                writer.WriteEndElement();


                writer.WriteStartElement("E");//закінчення чеку
                writer.WriteAttributeString("N", (products.Count + 2).ToString());//порядковий номер
                writer.WriteAttributeString("NO", order.LocalNumber.ToString());//номер фіксально чеку
                writer.WriteAttributeString("SM", Convert.ToDecimal(order.suma).ToString());//загальна сума чеку
                writer.WriteAttributeString("FN", CashRegisterName);//фіксальний номер рро
                writer.WriteAttributeString("TS", time);//дата та час
                writer.WriteAttributeString("TX", "0");//податок
                writer.WriteEndElement();





                writer.WriteEndElement();
                writer.WriteElementString("TS", time);
                writer.WriteEndElement();

                writer.WriteElementString("MAC",mac);

                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();
            }
            return mac;
        }

        public void writeCloseCase(string path,string time,int count)
        {


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            XDocument doc = XDocument.Parse(OrderXMLTableRepositories.LastXML().XMLString.ToString());
            doc.Save("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\buffers.xml");


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
                //writer.WriteAttributeString("NO", "0");

                //writer.WriteStartElement("TXS");
                //writer.WriteAttributeString("TX", "0");
                //writer.WriteAttributeString("SMI", "0");
                //writer.WriteEndElement();

                //writer.WriteStartElement("TXS");
                //writer.WriteAttributeString("TS", "0");
                //writer.WriteAttributeString("TSPR", "0");
                //writer.WriteAttributeString("TXI", "0");
                //writer.WriteAttributeString("TXO", "0");
                //writer.WriteAttributeString("DTPR", "0");

                //writer.WriteAttributeString("DTI", "0");
                //writer.WriteAttributeString("DTO", "0");
                //writer.WriteAttributeString("TXTY", "0");
                //writer.WriteAttributeString("TXAL", "0");
                //writer.WriteAttributeString("SMI", "0");
                //writer.WriteAttributeString("SMO", "0");
                //writer.WriteEndElement();

                //writer.WriteStartElement("M");
                //writer.WriteAttributeString("NM", "0");
                //writer.WriteAttributeString("SMI", "0");
                //writer.WriteAttributeString("SMO", "0");
                //writer.WriteEndElement();

                //writer.WriteStartElement("IO");
                //writer.WriteAttributeString("NM", "0");
                //writer.WriteAttributeString("SMI", "0");
                //writer.WriteAttributeString("SMO", "0");
                //writer.WriteEndElement();

                writer.WriteStartElement("NC");
                writer.WriteAttributeString("NI", count.ToString());//кількість чеків продажу
                //WriteAttributeString("NO", "0");
                writer.WriteEndElement();

                writer.WriteEndElement();
                
                writer.WriteElementString("TS", time);
                writer.WriteElementString("MAC", SHA.GenerateSHA256File("D:\\Проекти\\Visual Studio\\Project\\ShopProject\\ShopProject\\Resource\\BufferStorage\\buffers.xml"));

                // Закриття всіх відкритих елементів
                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();
            }

        }
       
    }
}
    
    

