using QRCoder;
using ShopProject.DataBase.Model;
using ShopProject.Resource.template;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ShopProject.Helpers.PrintingServise
{
    internal class PrintingDayReport
    {
        private string _printer;

        private string _nameShop;
        private string _nameSeller;
        private string _nameFop;
        private string region, district, city, street, house;

        public PrintingDayReport() 
        {
            //_nameShop = AppSettingsManager.GetParameterFiles("NameShop").ToString();
            //_nameSeller = AppSettingsManager.GetParameterFiles("NameSeller").ToString();
            //_nameFop = AppSettingsManager.GetParameterFiles("NameFop").ToString();
            //region = AppSettingsManager.GetParameterFiles("Region").ToString();
            //district = AppSettingsManager.GetParameterFiles("District").ToString();
            //city = AppSettingsManager.GetParameterFiles("Citi").ToString();
            //street = AppSettingsManager.GetParameterFiles("Streer").ToString();
            //house = AppSettingsManager.GetParameterFiles("House").ToString();

            //_printer = AppSettingsManager.GetParameterFiles("PrinterCheck").ToString(); ;
        }
        public void PrintCheck(OperationEntiti operation)
        {
            try
            {
                PrintDialog printDlg = new PrintDialog();
                TemplatePrintingDayReport template = new TemplatePrintingDayReport();
                
                template.NameFop.Text = _nameFop;
                template.NameShop.Text = _nameShop;
                template.Seller.Text = _nameSeller;
                template.RegionDistrictCiti.Text = region+", "+district+", "+city;
                template.StreetHouse.Text = street + ", " + house;
                template.Id.Text = "ID "+operation.ID;

                template.TotalSum.Text = (operation.AmountReceivedCard + operation.AmountReceivedCash).ToString("0.00");
                template.TotalSumCash.Text= operation.AmountReceivedCash.ToString("0.00");
                template.TotalSumCard.Text= operation.AmountReceivedCard.ToString("0.00");

                template.TotalSumReturn.Text = (operation.AmountCheckReturnCash + operation.AmountCheckReturnCard).ToString("0.00");
                template.TotalSumCashReturn.Text= operation.AmountCheckReturnCash.ToString("0.00");
                template.TotalSumCardReturn.Text = operation.AmountCheckReturnCard.ToString("0.00");

                template.TotalSumOfficialEntry.Text = operation.AmountOfFundsReceived.ToString("0.00");
                template.TotalSumOfficialIssuance.Text = operation.AmountOfIssuedFunds.ToString("0.00");

                template.CountCheck.Text = operation.NumberOfSalesReceipts.ToString("0");
                template.TotalReturnCheck.Text = operation.NumberOfPendingReturns.ToString("0");

                template.FNCheck.Text ="ФН чека: "+ operation.NumberPayment;
                template.FNRRo.Text = "ФН ПРРО: " + operation.FiscalNumberRRO;

                template.date.Text = DateTime.Now.ToString();

                if (_printer == null || _printer == string.Empty || _printer == " ")
                {
                    throw new Exception("Ви не вказали принтера");
                }

                printDlg.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), _printer);
                printDlg.PrintVisual(template.grid, "DayZRepot");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
