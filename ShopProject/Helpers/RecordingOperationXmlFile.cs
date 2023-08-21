using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using NPOI.SS.Formula.Functions;

namespace ShopProject.Helpers
{
    public static class RecordingOperationXmlFile
    {
        public static void WriteXmlFile(Operation operation, List<Goods> goods, bool type,string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                writer.Formatting = Formatting.Indented;

                // Початок документа XML
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

                if (type)
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
                else
                {

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
                    writer.WriteAttributeString("NI", operation.numberOfSalesReceipts.ToString("0"));//кількість чеків продажу
                    //WriteAttributeString("NO", "0");
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }



                writer.WriteElementString("TS", operation.createdAt.ToString("yyyyMMddHHmmss"));
                writer.WriteEndElement();
                writer.WriteElementString("MAC", operation.mac);

                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();

            }
        }
        public static void WriteXmlFile(Operation operation, List<GoodsOperation> goods, bool type, string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.GetEncoding("windows-1251")))
            {
                writer.Formatting = Formatting.Indented;

                // Початок документа XML
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

                if (type)
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
                            writer.WriteAttributeString("Q", (Convert.ToDecimal(goods[i].count.ToString("0.000"))).ToString().Replace(".", "").Replace(",", ""));//кількість товару
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
                else
                {

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
                    writer.WriteAttributeString("NI", operation.numberOfSalesReceipts.ToString("0"));//кількість чеків продажу
                    //WriteAttributeString("NO", "0");
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }



                writer.WriteElementString("TS", operation.createdAt.ToString("yyyyMMddHHmmss"));
                writer.WriteEndElement();
                writer.WriteElementString("MAC", operation.mac);

                writer.WriteEndDocument();

                // Закриття XmlTextWriter
                writer.Close();

            }
        }
    }
}
