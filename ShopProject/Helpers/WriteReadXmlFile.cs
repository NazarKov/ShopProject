
using ShopProject.UIModel;
using ShopProject.UIModel.SalePage;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml; 

namespace ShopProject.Helpers
{
    public static class WriteReadXmlFile
    { 
        private static readonly string pathxml = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml";
 
        private static void WriteHeader(UIWorkingShiftModel shift , XmlTextWriter writer)
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
                if(shift.UserCloseShift != null)
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
        private static void WriteFooter(UIWorkingShiftModel shift, DateTimeOffset time,UIMediaAccessControlModel MAC, XmlTextWriter writer)
        {

            writer.WriteElementString("TS", time.ToString("yyyyMMddHHmmss"));
            writer.WriteEndElement();

            writer.WriteStartElement("MAC");
            writer.WriteAttributeString("DI", shift.DataPacketIdentifier.ToString("0"));
            if (MAC != null)
            {
                writer.WriteAttributeString("NT", MAC.SequenceNumber.ToString("0")); 
                writer.WriteString(MAC.Content);
            }
            else
            {
                writer.WriteAttributeString("NT", "0"); 
                writer.WriteString(MAC.Content);
            } 
            writer.WriteEndElement();
            writer.WriteEndDocument(); 
        }
        private static void WriteElementCheckByOrder(string fiscalNumberRRO, UIOperationModel operation, List<OrderEntity> orders, XmlTextWriter writer)
        {
            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.TypeOperation.ToString("D"));

            if (orders !=null && orders.Count() != 0)
            {

                for (int i = 0; i < orders.Count; i++)
                {
                    if (orders[i]!= null && orders[i].Product != null)
                    { 
                        writer.WriteStartElement("P");//продажа
                        writer.WriteAttributeString("N", (i + 1).ToString());//порядковий номер 
                        writer.WriteAttributeString("C", orders[i].Product.Articule);//код товару
                        writer.WriteAttributeString("CD", orders[i].Product.Code.ToString());//штрихкод товару
                        writer.WriteAttributeString("NM", orders[i].Product.NameProduct);//назва товару або послуги
                        writer.WriteAttributeString("SM", orders[i].Product.Price.ToString().Replace(".", "").Replace(",", ""));//Сума операції
                        writer.WriteAttributeString("Q", (Convert.ToDecimal(((decimal)orders[i].Count).ToString("0.000"))).ToString().Replace(".", "").Replace(",", ""));//кількість товару
                        writer.WriteAttributeString("PRC", orders[i].Product.Price.ToString().Replace(".", "").Replace(",", ""));//Ціна товару
                        writer.WriteAttributeString("TX", "0");//податок
                        writer.WriteEndElement();
                    }
                }

                writer.WriteStartElement("M");//оплата
                writer.WriteAttributeString("N", (orders.Count + 1).ToString());//порядковий номер 
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


                writer.WriteStartElement("E");//закінчення чеку
                writer.WriteAttributeString("N", (orders.Count + 2).ToString());//порядковий номер
                writer.WriteAttributeString("NO", operation.NumberPayment.ToString());//номер фіксально чеку
                writer.WriteAttributeString("SM", operation.TotalPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна сума чеку
                writer.WriteAttributeString("FN", fiscalNumberRRO);//фіксальний номер рро
                writer.WriteAttributeString("TS", operation.CreatedAt.ToString("yyyyMMddHHmmss"));//дата та час
                writer.WriteAttributeString("TX", operation.GoodsTax);//податок
                writer.WriteEndElement(); 
            }

            writer.WriteEndElement();
        }
       
        private static void WriteElementChekByProduct(string fiscalNumberRRO , UIOperationModel operation, List<ProductEntity> products, XmlTextWriter writer)
        {
            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.TypeOperation.ToString("D"));

            if (products.Count() != 0)
            {

                for (int i = 0; i < products.Count; i++)
                {
                    writer.WriteStartElement("P");//продажа
                    writer.WriteAttributeString("N", (i + 1).ToString());//порядковий номер 
                    writer.WriteAttributeString("C", products[i].Articule);//код товару
                    writer.WriteAttributeString("CD", products[i].Code.ToString());//штрихкод товару
                    writer.WriteAttributeString("NM", products[i].NameProduct);//назва товару або послуги
                    writer.WriteAttributeString("SM", products[i].Price.ToString().Replace(".", "").Replace(",", ""));//Сума операції
                    writer.WriteAttributeString("Q",  (Convert.ToDecimal(((decimal)products[i].Count).ToString("0.000"))).ToString().Replace(".", "").Replace(",", ""));//кількість товару
                    writer.WriteAttributeString("PRC",products[i].Price.ToString().Replace(".", "").Replace(",", ""));//Ціна товару
                    writer.WriteAttributeString("TX", "0");//податок
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("M");//оплата
                writer.WriteAttributeString("N", (products.Count + 1).ToString());//порядковий номер 
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


                writer.WriteStartElement("E");//закінчення чеку
                writer.WriteAttributeString("N", (products.Count + 2).ToString());//порядковий номер
                writer.WriteAttributeString("NO", operation.NumberPayment.ToString());//номер фіксально чеку
                writer.WriteAttributeString("SM", operation.TotalPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна сума чеку
                writer.WriteAttributeString("FN", fiscalNumberRRO);//фіксальний номер рро
                writer.WriteAttributeString("TS", operation.CreatedAt.ToString("yyyyMMddHHmmss"));//дата та час
                writer.WriteAttributeString("TX", operation.GoodsTax);//податок
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        private static void WriteElementDepositWithdrawalMoney(UIOperationModel operation, XmlTextWriter writer, bool type)
        {
            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.TypeOperation.ToString("0"));

            if (type)
            {
                writer.WriteStartElement("I");
                writer.WriteAttributeString("N", "1");//Порядковий номер операції в чеку.
                switch(operation.TypePayment)
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
            else
            {
                writer.WriteStartElement("O");
                writer.WriteAttributeString("N", "1");//Порядковий номер операції в чеку.
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

            writer.WriteStartElement("E");//закінчення чеку
            writer.WriteAttributeString("N", "2");//порядковий номер
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static void WriteElementOpenShift(UIWorkingShiftModel shift, XmlTextWriter writer)
        {

            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", shift.TypeShiftCrateAt.ToString("D"));
            writer.WriteEndElement();
        }

        private static void WriteElementCloseShift(UIWorkingShiftModel shift, XmlTextWriter writer)
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

        public static void WriteXMLFile(UIWorkingShiftModel workingShift, UIOperationModel? operation, List<OrderEntity>? orders, List<ProductEntity>? products)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(pathxml, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                DateTimeOffset time = DateTime.Now;
                UIMediaAccessControlModel mac = new UIMediaAccessControlModel();
                WriteHeader(workingShift, writer);

                if (operation != null && operation.TypeOperation != TypeOperation.None)
                {
                    time = operation.CreatedAt;
                    mac = operation.MAC;
                    switch (operation.TypeOperation)
                    {
                        case TypeOperation.FiscalCheck:
                            {
                                if (orders != null && orders.Count != 0)
                                {
                                    WriteElementCheckByOrder(workingShift.FiscalNumberRRO, operation, orders, writer);
                                }
                                else if (products != null &&  products.Count != 0)
                                {
                                    WriteElementChekByProduct(workingShift.FiscalNumberRRO, operation, products, writer);
                                }
                                break;
                            }
                        case TypeOperation.ReturnCheck:
                            {
                                if (orders != null &&  orders.Count != 0)
                                {
                                    WriteElementCheckByOrder(workingShift.FiscalNumberRRO, operation, orders, writer);
                                }
                                else if (products != null && products.Count != 0)
                                {
                                    WriteElementChekByProduct(workingShift.FiscalNumberRRO, operation, products, writer);
                                }
                                break;
                            }
                        case TypeOperation.DepositMoney:
                            {
                                WriteElementDepositWithdrawalMoney(operation, writer, true);
                                break;
                            }
                        case TypeOperation.WithdrawalMoney:
                            {
                                WriteElementDepositWithdrawalMoney(operation, writer, false);
                                break;
                            }
                    }
                }
                else
                {
                    if (workingShift.TypeShiftEndAt == TypeWorkingShift.CloseShift)
                    {
                        time = workingShift.EndAt;
                        mac = workingShift.MACEndAt; 
                        WriteElementCloseShift(workingShift, writer);
                    }
                    else
                    {
                        if (workingShift.TypeShiftCrateAt == TypeWorkingShift.OpenShift)
                        {
                            time = workingShift.CreateAt;
                            mac = workingShift.MACCreateAt;
                            WriteElementOpenShift(workingShift, writer);
                        }

                    }
                }   
                WriteFooter(workingShift, time, mac, writer);
                writer.Close();
            }


        } 

        public static string GenerationMACForXML()
        {
            return SHA.GenerateSHA256File(pathxml);
        }
    }
}
