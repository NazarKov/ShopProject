using ShopProject.Helpers;
using ShopProject.Services.Integration.File.Excel;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZXing.Aztec.Internal;

namespace ShopProject.Model.StoragePage.ExcelPage.ExportExcelPage
{
    internal class ExportProductExelModel
    {
        private FileExcelServise? fileExel;

        public ExportProductExelModel()
        {
            fileExel = new FileExcelServise();
        }

      
        //public bool Save(List<Product> products, string path)
        //{
        //    return Export(path, fileExel.ToDataTable<Product>(products));
        //}
        //public async Task<bool> Save(string path)
        //{ 
        //    List<Product> products = new List<Product>(); 
        //    var list = await GetItems();
        //    return Export(path, fileExel.ToDataTable<Product>(list));
        //}

        public bool Export(string path, DataTable table)
        {
            try
            {
                FileExcel.Write(path, fileExel.CreateExelTable(table));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
