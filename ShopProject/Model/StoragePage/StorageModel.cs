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
using ShopProject.Views.StoragePage;
using Archive = ShopProject.DataBase.Model.Archive;

namespace ShopProject.Model.StoragePage
{
    internal class StorageModel
    {
        private ShopContext db;
        private List<Product> products;
        private List<Archive> archives;

        public StorageModel()
        {
            db = new ShopContext();
            products = new List<Product>();
            archives = new List<Archive>();

            db.products.Load();
            db.archives.Load();

            if (db.products != null && db.archives!=null)
            {
                products = db.products.ToList();//Where(p => p.count != 0).ToList();
                archives = db.archives.ToList();
            }
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

        public void SetProductInArhive(Product item)
        {
            try
            {
                Validation.ChekIsProductInArhive(item, archives);
               
                Archive archive = new Archive();
                SetFieldArchive(archive, item);

                if (db.products!=null)
                  SetProductCountNull(item);
                SaveChangeDataBase(archive);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        private void SetFieldArchive(Archive archive, Product item)
        {
            archive.product = item;
            archive.created_at = DateTime.Now;
        }

        private void SetProductCountNull(Product item)
        {
            if (db.products != null)
            {
                foreach (Product product in db.products)
                {
                    if (product.Equals(item))
                    {
                        product.count = 0;
                    }
                }
            }
        }
        
        private void SaveChangeDataBase(Archive archive)
        {
            if (db.archives != null)
                db.archives.Add(archive);
            db.SaveChangesAsync();
        }

    }
}
