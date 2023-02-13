using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Model;

namespace ShopProject.Model.StoragePage
{
    internal class StorageModel
    {
        private ShopContext db;
        private List<Product> products;

        public StorageModel()
        {
            db = new ShopContext();
            products = new List<Product>();
            db.products.Load();
            if(db.products!=null)
            products = db.products.ToList();
        }
        public List<Product> GetItems()
        {
            return products;
        }

        public List<Product>? SearchProduct(string itemSearch, TypeSearch type)
        {
            try
            {
                return Search.ProductDataBase(itemSearch, type, products);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка",MessageBoxButton.OK);
                return null;
            }
        }

        public bool DeleteProduct(Product productDelete)
        {
            try
            {
                if (db != null)
                {
                    if (db.products != null)
                    {
                        db.products.Remove(productDelete);
                        db.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }

        }
        public void ContertToListProduct(IList list, List<Product> products)
        {
            foreach (var item in list)
            {
                products.Add((Product)item);
            }
        }

    }
}
