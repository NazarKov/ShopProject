using ShopProject.Helpers; 
using ShopProject.Helpers.FileServise.ExcelServise;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.StoragePage;
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

        public async Task<Product> GetItem(string itemSearch)
        { 
            try
            {
                var item = (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(Session.User.Token, itemSearch)).ToProduct(); 
                return item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Product();
            }
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            try
            {
                var items = (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProducts(Session.User.Token)).ToProduct();  
                return items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public bool Save(List<Product> products, string path)
        {
            return Export(path, fileExel.ToDataTable<Product>(products));
        }
        public async Task<bool> Save(string path)
        { 
            List<Product> products = new List<Product>(); 
            var list = await GetItems();
            return Export(path, fileExel.ToDataTable<Product>(list));
        }

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
