using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ExportProductExelModel
    {
        private ITableRepository<Goods, TypeParameterSetTableProduct> _productRepository;
        private FileExel? fileExel;

        public ExportProductExelModel()
        {
            _productRepository = new ProductTableRepository();
        }

        public Goods? GetItem(string itemSearch)
        {
            Goods product = (Goods)_productRepository.GetItem(itemSearch);
            return product;
        }

        public List<Goods> GetItems()
        {
            return (List<Goods>)_productRepository.GetAll();
        }
        
        public bool Export(string path,List<Goods> products)
        {
            try
            {
                fileExel = new FileExel();
                fileExel.Write(path,products);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
    }
}
