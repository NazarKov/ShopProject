using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ShopProject.Model
{
    internal class OrderCheck
    {
        private string _numberChek;
        private string _stringCheck;
        private string _nameShop;
        private string _nameSeller;
        private string _title;
        private string _description;
        private string _price;
        private List<Product> _products;

        public OrderCheck()
        {
            _stringCheck = string.Empty;
            _nameShop = string.Empty;
            _nameSeller = string.Empty;
            _title = string.Empty;
            _description = string.Empty;
            _price = string.Empty;
            _products = new List<Product>();
        }

        public void drawingChek()
        {
            try
            {
                FlowDocument document = new FlowDocument();
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Section setTextChek(string text)
        {
            Section sec = new Section();
            Paragraph p1 = new Paragraph();

            Run run = new Run(text);
            run.FontFamily = new FontFamily("Times New Roman");
            run.FontSize = 10;

            p1.Inlines.Add(run);

            sec.Blocks.Add(p1);
            return sec;
        }

        public void PrintChek(List<Product> products)
        {
            try
            {
                // Create a PrintDialog  
                PrintDialog printDlg = new PrintDialog();
                // Create a FlowDocument dynamically.  
                FlowDocument doc = new FlowDocument();

                doc.Name = "FlowDoc";

                // Add Section to FlowDocument  
                doc.Blocks.Add(setTextChek("ФОП КОРНІЙЧУК Н.С."));

                doc.Blocks.Add(setTextChek("Волинська ОБЛ.,М.РОЖИЩЕ,ВУЛ.НАЗВА ВУЛИЦІ"));

                doc.Blocks.Add(setTextChek("ІД ЧЕКУ"));

                doc.Blocks.Add(setTextChek("Касир: Корнійчук Н.С."));
                doc.Blocks.Add(setTextChek("------------------------------------------"));

                foreach (Product product in products)
                {
                    doc.Blocks.Add(setTextChek(""+product.name+"  "+product.count+"x"+product.price+".00"));
                }
                doc.Blocks.Add(setTextChek("------------------------------------------"));
                doc.Blocks.Add(setTextChek("Продаж"));

                doc.Blocks.Add(setTextChek(@"Сума                   {}.00"));



                //doc.Blocks.Add(sec);

                // Create IDocumentPaginatorSource from FlowDocument  
                IDocumentPaginatorSource idpSource = doc;
                // Call PrintDocument method to send document to printer  
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
