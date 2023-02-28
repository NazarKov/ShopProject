using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ExportProductExelModel
    {
        private readonly ShopContext db;
        private List<Product> products;
        private FileExel? fileExel;

        public ExportProductExelModel()
        {
            db = new ShopContext();
            products = new List<Product>();
            
            db.products.Load();
            if(db.products!=null)
            products = db.products.Local.ToList();
        }

        //public Product? GetItem(string itemSearch)
        //{
        //    //return Search.ProductDataBase(itemSearch, products, TypeSearch.Code);
        //}

        public List<Product> GetItems()
        {
            return products;
        }
        
        public bool Export(string path,List<Product> products)
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
