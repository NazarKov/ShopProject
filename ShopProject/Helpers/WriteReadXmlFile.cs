using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ShopProject.Helpers
{
    public static class WriteReadXmlFile
    {


        private static void WriteHeader(Operation operation , XmlTextWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("RQ");
            writer.WriteAttributeString("V", "1");

            writer.WriteStartElement("DAT");
            writer.WriteAttributeString("DI", operation.dataPacketIdentifier.ToString("0"));
            writer.WriteAttributeString("DT", operation.typeRRO.ToString("0"));
            writer.WriteAttributeString("FN", operation.fiscalNumberRRO.ToString());
            writer.WriteAttributeString("TN", "ПН " + operation.taxNumber);
            writer.WriteAttributeString("V", "1");
            writer.WriteAttributeString("ZN", operation.factoryNumberRRO.ToString());
        }
        private static void WriteFooter(Operation operation, XmlTextWriter writer)
        {

            writer.WriteElementString("TS", operation.createdAt.ToString("yyyyMMddHHmmss"));
            writer.WriteEndElement();
            writer.WriteElementString("MAC", operation.mac);

            writer.WriteEndDocument();

            // Закриття XmlTextWriter
            writer.Close();
        }
        private static void WriteElementCT0(Operation operation,List<GoodsOperation> goods, XmlTextWriter writer)
        {
            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.typeOperation.ToString("0"));

            if (goods.Count() != 0)
            {

                for (int i = 0; i < goods.Count; i++)
                {
                    writer.WriteStartElement("P");//продажа
                    writer.WriteAttributeString("N", (i + 1).ToString());//порядковий номер 
                    writer.WriteAttributeString("C", goods[i].goods.articule);//код товару
                    writer.WriteAttributeString("CD", goods[i].goods.code.ToString());//штрихкод товару
                    writer.WriteAttributeString("NM", goods[i].goods.name);//назва товару або послуги
                    writer.WriteAttributeString("SM", goods[i].goods.price.ToString().Replace(".", "").Replace(",", ""));//Сума операції
                    writer.WriteAttributeString("Q", (Convert.ToDecimal(((decimal)goods[i].count).ToString("0.000"))).ToString().Replace(".", "").Replace(",", ""));//кількість товару
                    writer.WriteAttributeString("PRC", goods[i].goods.price.ToString().Replace(".", "").Replace(",", ""));//Ціна товару
                    writer.WriteAttributeString("TX", "0");//податок
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("M");//оплата
                writer.WriteAttributeString("N", (goods.Count + 1).ToString());//порядковий номер 
                writer.WriteAttributeString("T", "0");//тип опалати
                writer.WriteAttributeString("SM", operation.buyersAmount.ToString("0.00").Replace(".", "").Replace(",", ""));//Сума до оплати що вноситьця покупцем
                writer.WriteAttributeString("RM", operation.restPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//Решта якщо немає то невказується;
                writer.WriteEndElement();


                writer.WriteStartElement("E");//закінчення чеку
                writer.WriteAttributeString("N", (goods.Count + 2).ToString());//порядковий номер
                writer.WriteAttributeString("NO", operation.numberPayment.ToString());//номер фіксально чеку
                writer.WriteAttributeString("SM", operation.totalPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна сума чеку
                writer.WriteAttributeString("FN", operation.fiscalNumberRRO);//фіксальний номер рро
                writer.WriteAttributeString("TS", operation.createdAt.ToString("yyyyMMddHHmmss"));//дата та час
                writer.WriteAttributeString("TX", operation.goodsTax);//податок
                writer.WriteEndElement();

            }

            writer.WriteEndElement();
        }
        private static void WriteElementCT0_1(Operation operation,List<Goods> goods, XmlTextWriter writer)
        {
            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.typeOperation.ToString("0"));

            if (goods.Count() != 0)
            {

                for (int i = 0; i < goods.Count; i++)
                {
                    writer.WriteStartElement("P");//продажа
                    writer.WriteAttributeString("N", (i + 1).ToString());//порядковий номер 
                    writer.WriteAttributeString("C", goods[i].articule);//код товару
                    writer.WriteAttributeString("CD", goods[i].code.ToString());//штрихкод товару
                    writer.WriteAttributeString("NM", goods[i].name);//назва товару або послуги
                    writer.WriteAttributeString("SM", goods[i].price.ToString().Replace(".", "").Replace(",", ""));//Сума операції
                    writer.WriteAttributeString("Q", (Convert.ToDecimal(((decimal)goods[i].count).ToString("0.000"))).ToString().Replace(".", "").Replace(",", ""));//кількість товару
                    writer.WriteAttributeString("PRC", goods[i].price.ToString().Replace(".", "").Replace(",", ""));//Ціна товару
                    writer.WriteAttributeString("TX", "0");//податок
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("M");//оплата
                writer.WriteAttributeString("N", (goods.Count + 1).ToString());//порядковий номер 
                writer.WriteAttributeString("T", "0");//тип опалати
                writer.WriteAttributeString("SM", operation.buyersAmount.ToString("0.00").Replace(".", "").Replace(",", ""));//Сума до оплати що вноситьця покупцем
                writer.WriteAttributeString("RM", operation.restPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//Решта якщо немає то невказується;
                writer.WriteEndElement();


                writer.WriteStartElement("E");//закінчення чеку
                writer.WriteAttributeString("N", (goods.Count + 2).ToString());//порядковий номер
                writer.WriteAttributeString("NO", operation.numberPayment.ToString());//номер фіксально чеку
                writer.WriteAttributeString("SM", operation.totalPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//загальна сума чеку
                writer.WriteAttributeString("FN", operation.fiscalNumberRRO);//фіксальний номер рро
                writer.WriteAttributeString("TS", operation.createdAt.ToString("yyyyMMddHHmmss"));//дата та час
                writer.WriteAttributeString("TX", operation.goodsTax);//податок
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        private static void WriteElementCT2(Operation operation , XmlTextWriter writer, bool type)
        {
            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.typeOperation.ToString("0"));

            if (type)
            {
                writer.WriteStartElement("I");
                writer.WriteAttributeString("N", "1");//Порядковий номер операції в чеку.
                writer.WriteAttributeString("T", operation.formOfPayment.ToString("0"));//Формат оплати.
                writer.WriteAttributeString("SM", operation.totalPayment.ToString("0"));//Сума оплати.
                writer.WriteEndElement();

            }
            else
            {
                writer.WriteStartElement("O");
                writer.WriteAttributeString("N", "1");//Порядковий номер операції в чеку.
                writer.WriteAttributeString("T", operation.formOfPayment.ToString("0"));//Формат оплати.
                writer.WriteAttributeString("SM", operation.totalPayment.ToString("0"));//Сума оплати.
                writer.WriteEndElement();
            }

            writer.WriteStartElement("E");//закінчення чеку
            writer.WriteAttributeString("N", "2");//порядковий номер
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static void WriteElementCT108(Operation operation, XmlTextWriter writer)
        {

            writer.WriteStartElement("C");
            writer.WriteAttributeString("T", operation.typeOperation.ToString("0"));
            writer.WriteEndElement();
        }
        
        private static void WriteElementZ(Operation operation, XmlTextWriter writer)
        {
            writer.WriteStartElement("Z");

            writer.WriteStartElement("M");
            writer.WriteAttributeString("T", "0");//Форма оплати
            writer.WriteAttributeString("NM", "ГОТІВКА");//Форма оплати
            writer.WriteAttributeString("SMI", operation.buyersAmount.ToString("0.00").Replace(".", "").Replace(",", ""));//Сума до оплати що вноситьця покупцем
            writer.WriteAttributeString("SMO", operation.restPayment.ToString("0.00").Replace(".", "").Replace(",", ""));//Решта якщо немає то невказується;
            writer.WriteEndElement();


            writer.WriteStartElement("IO");//Підсумок службових внесень та видач за день готівка
            writer.WriteAttributeString("T", "0");//Форма оплати
            writer.WriteAttributeString("NM", "ГОТІВКА");//Форма оплати
            writer.WriteAttributeString("NI", operation.numberOfSalesReceipts.ToString("0"));//сума внесень за день
            writer.WriteAttributeString("NO", operation.numberOfPendingReturns.ToString("0"));//сума видач за день
            writer.WriteEndElement();

            //writer.WriteStartElement("IO");//Підсумок службових внесень та видач за день картка
            //writer.WriteAttributeString("T", "1");//Форма оплати
            //writer.WriteAttributeString("NM", "безготівкові форми оплати");//Форма оплати
            //writer.WriteAttributeString("NI", operation.numberOfSalesReceipts.ToString("0"));//сума внесень за день
            //writer.WriteAttributeString("NO", operation.numberOfPendingReturns.ToString("0"));//сума видач за день
            //writer.WriteEndElement();

            writer.WriteStartElement("NC");//Підсумок чеків за день
            writer.WriteAttributeString("NI", operation.numberOfSalesReceipts.ToString("0"));//кількість чеків продажу
            writer.WriteAttributeString("NO", operation.numberOfPendingReturns.ToString("0"));//кількість чеків повернення
            //WriteAttributeString("NO", "0");
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
        
       
        public static void WriteXmlFile(Operation operation, List<GoodsOperation> goodsOperation,List<Goods> goods, string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                writer.Formatting = Formatting.Indented;

                // Початок документа XML
                WriteHeader(operation, writer);

                switch (operation.typeOperation.ToString("0"))
                {
                    case "0":
                        {
                            //запис чеку продаж
                            if (goodsOperation.Count != 0)
                            {
                                WriteElementCT0(operation, goodsOperation, writer);
                            }
                            else if (goods.Count != 0)
                            {
                                WriteElementCT0_1(operation, goods, writer);
                            }
                            break;
                        }
                    case "1":
                        {
                            //запис чеку повернення
                            if (goodsOperation.Count != 0)
                            {
                                WriteElementCT0(operation, goodsOperation, writer);
                            }
                            else if (goods.Count != 0)
                            {
                                WriteElementCT0_1(operation, goods, writer);
                            }
                            break;
                        }
                    case "2":
                        {
                            if (operation.typeOperation.ToString("0.00")=="2,00")
                            {
                                WriteElementCT2(operation, writer,true);
                            }
                            else
                            {
                                WriteElementCT2(operation, writer, false);
                            }
                            //запис чеку внесення
                            break;
                        }
                    case "108":
                        {
                            //запис відкриття змінни
                            WriteElementCT108(operation, writer);
                            break;
                        }
                    case "113":
                    {
                        //запис елемена <Z> в файл
                        WriteElementZ(operation, writer);
                        break;
                    }
                }
                // Закриття XmlTextWriter
                WriteFooter(operation,writer);
                writer.Close();

            }
        }
    }
}
