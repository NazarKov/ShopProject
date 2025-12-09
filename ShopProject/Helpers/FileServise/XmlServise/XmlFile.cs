using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShopProject.Helpers.FileServise.XmlServise
{
    public class XmlFile
    { 
        public XmlFile() { }

        private void WriteHeader(WorkingShift shift, XmlTextWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("RQ");
            writer.WriteAttributeString("V", "1");
            writer.WriteStartElement("DAT");
            writer.WriteAttributeString("DI", shift.DataPacketIdentifier.ToString("0"));
            writer.WriteAttributeString("DT", shift.TypeRRO.ToString("0"));
            writer.WriteAttributeString("FN", shift.FiscalNumberRRO.ToString());

            if (shift.UserOpenShift != null)
            {
                if (shift.UserCloseShift != null)
                {
                    writer.WriteAttributeString("TN", "ПН " + shift.UserCloseShift.TIN);
                }
                else
                {
                    writer.WriteAttributeString("TN", "ПН " + shift.UserOpenShift.TIN);
                }
            }

            writer.WriteAttributeString("V", "1");
            writer.WriteAttributeString("ZN", shift.FactoryNumberRRO.ToString());
        }
        private void WriteFooter(decimal DataPacketIdentifier, DateTimeOffset time, MediaAccessControl MAC, XmlTextWriter writer)
        {

            writer.WriteElementString("TS", time.ToString("yyyyMMddHHmmss"));
            writer.WriteEndElement();

            writer.WriteStartElement("MAC");
            writer.WriteAttributeString("DI", DataPacketIdentifier.ToString("0"));
            if (MAC != null)
            {
                writer.WriteAttributeString("NT", MAC.SequenceNumber.ToString("0"));
                writer.WriteString(MAC.Content);
            }
            else
            {
                writer.WriteAttributeString("NT", "0"); 
                writer.WriteString(string.Empty); 
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private void WriteTagP(XmlTextWriter writer, int serialNumber , Product product)//продажа
        {
            writer.WriteStartElement("P");
            writer.WriteAttributeString("N", serialNumber.ToString());//порядковий номер 
            writer.WriteAttributeString("C", product.Articule);//код товару
            writer.WriteAttributeString("CD", product.Code.ToString());//штрихкод товару 
            if (product.CodeUKTZED != null) 
            {
                writer.WriteAttributeString("CZD", product.CodeUKTZED.Code.ToString());//штрихкод товару
            }
            writer.WriteAttributeString("NM", product.NameProduct);//назва товару або послуги
            writer.WriteAttributeString("SM", product.Price.ToString().Replace(".", "").Replace(",", ""));//Сума операції
            writer.WriteAttributeString("Q", (Convert.ToDecimal(((decimal)product.Count).ToString("0.000"))).ToString().Replace(".", "").Replace(",", ""));//кількість товару
            writer.WriteAttributeString("PRC", product.Price.ToString().Replace(".", "").Replace(",", ""));//Ціна товару
            writer.WriteAttributeString("TX", "0");//податок
            writer.WriteEndElement();
        }
        private void WriteTagD(XmlTextWriter writer, int serialNumber)
        {
            writer.WriteStartElement("D");
            writer.WriteAttributeString("N", serialNumber.ToString());//порядковий номер 
            writer.WriteAttributeString("TR", serialNumber.ToString());//тип застусування
            writer.WriteAttributeString("TY", serialNumber.ToString());//тип
            writer.WriteAttributeString("ST", serialNumber.ToString());// проміжка сума чеку
            writer.WriteAttributeString("NI", serialNumber.ToString());// порядковий номер операції до якох застосовується (присутній якщо стосується однієї операції)
            writer.WriteAttributeString("TX", "0");//податок 
            writer.WriteAttributeString("PR", serialNumber.ToString());//для відсоткових знизок відсоток знижки
            writer.WriteAttributeString("SM", serialNumber.ToString());//загальна сума знижки 
            writer.WriteEndElement();
        }
        private void WriteTagI(XmlTextWriter writer, int serialNumber , Operation operation) 
        {
            writer.WriteStartElement("I");
            writer.WriteAttributeString("N", serialNumber.ToString());//Порядковий номер операції в чеку.
            switch (operation.TypePayment)
            {
                case TypePayment.Cash:
                    {
                        writer.WriteAttributeString("T", 0.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Готівка");//Формат оплати.
                        break;
                    }
                case TypePayment.Card:
                    {
                        writer.WriteAttributeString("T", 1.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Картка");//Формат оплати.
                        break;
                    }
                case TypePayment.GiftCertificate:
                    {
                        writer.WriteAttributeString("T", 1.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Подарунковий сертифікат");//Формат оплати.
                        break;
                    }
            }
            writer.WriteAttributeString("SM", operation.TotalPayment.ToString("0"));//Сума оплати.
            writer.WriteEndElement();
        }
        private void WriteTagO(XmlTextWriter writer, int serialNumber, Operation operation)
        {
            writer.WriteStartElement("O");
            writer.WriteAttributeString("N",serialNumber.ToString());//Порядковий номер операції в чеку.
            switch (operation.TypePayment)
            {
                case TypePayment.Cash:
                    {
                        writer.WriteAttributeString("T", 0.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Готівка");//Формат оплати.
                        break;
                    }
                case TypePayment.Card:
                    {
                        writer.WriteAttributeString("T", 1.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Картка");//Формат оплати.
                        break;
                    }
                case TypePayment.GiftCertificate:
                    {
                        writer.WriteAttributeString("T", 1.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Подарунковий сертифікат");//Формат оплати.
                        break;
                    }
            }
            writer.WriteAttributeString("SM", operation.TotalPayment.ToString("0"));//Сума оплати.
            writer.WriteEndElement();
        }

        private void WriteTagM(XmlTextWriter writer, int serialNumber, Operation operation)
        {

            writer.WriteStartElement("M");//оплата
            writer.WriteAttributeString("N", serialNumber.ToString());//порядковий номер 
            switch (operation.TypePayment)
            {
                case TypePayment.Cash:
                    {
                        writer.WriteAttributeString("T", 0.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Готівка");//Формат оплати.
                        break;
                    }
                case TypePayment.Card:
                    {
                        writer.WriteAttributeString("T", 1.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Картка");//Формат оплати.
                        break;
                    }
                case TypePayment.GiftCertificate:
                    {
                        writer.WriteAttributeString("T", 1.ToString("0"));//Формат оплати.
                        writer.WriteAttributeString("NM", "Подарунковий сертифікат");//Формат оплати.
                        break;
                    }
            }
            writer.WriteAttributeString("SM", operation.BuyersAmount.ToString("0.00").Replace(".", "").Replace(",", ""));//Сума до оплати що вноситьця покупцем
            writer.WriteAttributeString("RM", operation.RestPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//Решта якщо немає то невказується;
            writer.WriteEndElement();
        }
        private void WriteTagL(XmlTextWriter writer, int serialNumber,string comment)
        {
            writer.WriteStartElement("L");
            writer.WriteAttributeString("N", serialNumber.ToString());//порядковий номер 
            writer.WriteString(comment);
            writer.WriteEndElement();
        }
        private void WriteTagE(XmlTextWriter writer, int serialNumber , Operation operation , string fiscalNumberRRO)
        {
            writer.WriteStartElement("E");//закінчення чеку
            writer.WriteAttributeString("N", serialNumber.ToString());//порядковий номер
            writer.WriteAttributeString("NO", operation.NumberPayment.ToString());//номер фіксально чеку
            writer.WriteAttributeString("SM", operation.TotalPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна сума чеку
            writer.WriteAttributeString("FN", fiscalNumberRRO);//фіксальний номер рро
            writer.WriteAttributeString("TS", operation.CreatedAt.ToString("yyyyMMddHHmmss"));//дата та час
            writer.WriteAttributeString("TX", operation.GoodsTax);//податок
            writer.WriteEndElement();
        }

        private static void WriteElementZ(WorkingShift shift, XmlTextWriter writer)
        {
            writer.WriteStartElement("Z");

            writer.WriteStartElement("M");
            writer.WriteAttributeString("T", "0");//Форма оплати
            writer.WriteAttributeString("NM", "ГОТІВКА");//Форма оплати
            writer.WriteAttributeString("SMI", shift.AmountOfFundsReceived.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна сума чеків за зміну
            writer.WriteAttributeString("SMO", shift.AmountOfFundsIssued.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна решта чеків за зміну
            writer.WriteEndElement();


            writer.WriteStartElement("IO");//Підсумок службових внесень та видач за день готівка
            writer.WriteAttributeString("T", "0");//Форма оплати
            writer.WriteAttributeString("NM", "ГОТІВКА");//Форма оплати
            writer.WriteAttributeString("NI", shift.AmountOfOfficialFundsReceivedCash.ToString("0.00").Replace(".", "").Replace(",", ""));//сума внесень за день
            writer.WriteAttributeString("NO", shift.AmountOfOfficialFundsIssuedCash.ToString("0.00").Replace(".", "").Replace(",", ""));//сума видач за день
            writer.WriteEndElement();

            writer.WriteStartElement("IO");//Підсумок службових внесень та видач за день картка
            writer.WriteAttributeString("T", "1");//Форма оплати
            writer.WriteAttributeString("NM", "безготівкові форми оплати");//Форма оплати
            writer.WriteAttributeString("NI", shift.AmountOfOfficialFundsReceivedCard.ToString("0.00").Replace(".", "").Replace(",", ""));//сума внесень за день
            writer.WriteAttributeString("NO", shift.AmountOfOfficialFundsIssuedCard.ToString("0.00").Replace(".", "").Replace(",", ""));//сума видач за день
            writer.WriteEndElement();

            writer.WriteStartElement("NC");//Підсумок чеків за день
            writer.WriteAttributeString("NI", shift.TotalCheckForShift.ToString("0"));//кількість чеків продажу
            writer.WriteAttributeString("NO", shift.TotalReturnCheckForShift.ToString("0"));//кількість чеків повернення
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        public void WriteOpenShift(string path, WorkingShift workingShift)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                WriteHeader(workingShift, writer);
                writer.WriteStartElement("C");
                writer.WriteAttributeString("T", workingShift.TypeShiftCrateAt.ToString("D"));
                writer.WriteEndElement(); 
                WriteFooter(workingShift.DataPacketIdentifier,workingShift.CreateAt, workingShift.MACCreateAt,writer);
                
            }
        }
        public void WriteFiscalCheck(string path , WorkingShift workingShift, Operation operation , IEnumerable<Product> products)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                WriteHeader(workingShift, writer);
                writer.WriteStartElement("C");
                writer.WriteAttributeString("T", operation.TypeOperation.ToString("D"));
                var index = 1;
                foreach (Product product in products) 
                {
                    WriteTagP(writer,index,product);
                    index++;
                }
                WriteTagM(writer, index, operation);
                index++;
                WriteTagE(writer, index, operation,workingShift.FiscalNumberRRO); 
                writer.WriteEndElement();
                WriteFooter(workingShift.DataPacketIdentifier, operation.CreatedAt, operation.MAC, writer); 
            }
        }
        public void WriteCloseShift(string path, WorkingShift workingShift)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                WriteHeader(workingShift, writer);
                WriteElementZ(workingShift, writer);
                WriteFooter(workingShift.DataPacketIdentifier, workingShift.EndAt, workingShift.MACEndAt, writer);
            }
        }

    }
}
