using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ExportProductExelModel
    {
        private IEntityGet<ProductEntiti> _productRepository;
        private FileExel? fileExel;

        public ExportProductExelModel()
        {
            _productRepository = new ProductTableAccess();
        }

        public ExportProductInFileHelper GetItem(string itemSearch)
        {

            try
            {
                return  new ExportProductInFileHelper()
                {
                    Product = _productRepository.GetByBarCode(itemSearch),
                    ProductCount = 1
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ExportProductInFileHelper();
            }
        }

        public List<ProductEntiti> GetItems()
        {
            try
            {
                return (List<ProductEntiti>)_productRepository.GetAll("in_stock");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public void Save(List<ExportProductInFileHelper> products ,string path)
        {
            if (Export(path, products))
            {
                MessageBox.Show("Товар успішно експортовано","informations",MessageBoxButton.OK,MessageBoxImage.Information);
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
                    goods.Add(new ExportProductInFileHelper() { Product = item,ProductCount = item.Count });
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

        public bool Export(string path,List<ExportProductInFileHelper> goods)
        {
            try
            {
                fileExel = new FileExel();
                fileExel.Write(path,goods);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
    }
}
