using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ExportProductExelModel
    {
        private FileExel? fileExel;

        public ExportProductExelModel(){}

        public ExportProductInFileHelper GetItem(string itemSearch)
        {

            try
            {
                ExportProductInFileHelper item = new ExportProductInFileHelper();
                Task task = Task.Run(async () =>
                {
                    //item.Product = await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(Session.Token, itemSearch);
                    item.ProductCount = 1;
                });
                task.Wait();
                return item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ExportProductInFileHelper();
            }
        }

        public List<ProductEntity> GetItems()
        {
            try
            {
                List<ProductEntity> items = new List<ProductEntity>();

                Task task = Task.Run(async ()=>{
                
                    items = (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProducts(Session.Token)).ToList();
                });

                task.Wait();
                return items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public void Save(List<ExportProductInFileHelper> products, string path)
        {
            if (Export(path, products))
            {
                MessageBox.Show("Товар успішно експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Товар не експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void Save(string path)
        {

            List<ExportProductInFileHelper> goods = new List<ExportProductInFileHelper>();

            var list = GetItems();
            if (list != null)
            {
                foreach (var item in list)
                {
                    goods.Add(new ExportProductInFileHelper() { Product = item, ProductCount = item.Count });
                }
            }

            if (Export(path, goods))
            {
                MessageBox.Show("товар успішно експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("товар не експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool Export(string path, List<ExportProductInFileHelper> goods)
        {
            try
            {
                fileExel = new FileExel();
                fileExel.Write(path, goods);
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
