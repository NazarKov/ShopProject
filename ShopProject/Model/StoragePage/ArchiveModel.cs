using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Views.StoragePage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ShopProject.Model.StoragePage.StorageModel;
using ProductArchive = ShopProject.DataBase.Model.ProductArchive;

namespace ShopProject.Model.StoragePage
{
    internal class ArchiveModel
    {
        private ShopContext db;
        private List<ProductArchive> archives;

        public ArchiveModel()
        {
            db = new ShopContext();
            archives = new List<ProductArchive>();

            db.products.Load();
            db.productArchives.Load();
            if(db.productArchives != null)
            archives = db.productArchives.ToList();
        }

        public List<ProductArchive> GetItems()
        {
            return archives;
        }

        public List<ProductArchive>? SearchArhive(string itemSearch, TypeSearch type)
        {
            try
            {
                return Search.ArhiveDataBase(itemSearch, type,archives);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public bool DeleteRecordArhive(ProductArchive archive,Product product)
        {
            try
            {
                DeleteArhive(archive);
                DeleteProduct(product);
                db.SaveChangesAsync();
                return true;
                throw new Exception("Помилка видалення");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        private void DeleteArhive(ProductArchive archive)
        {
            if (db.productArchives != null)
            {
                db.productArchives.Remove(archive);
            }
        }
        private void DeleteProduct(Product product)
        {
            if (db.products != null)
            {
                if (product != null)
                {
                    db.products.Remove(product);
                }
            }
        }
        public void ConvertToList(IList collection, List<ProductArchive> itemConver)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                itemConver.Add((ProductArchive)collection[i]);
                itemConver[i].product = ((ProductArchive)collection[i]).product;
            }
        }
        
        public bool ReturnProductInStorage(ProductArchive archive)
        {
            try
            {
                SetProductStatus(archive);
                DeleteArhive(archive);
                db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK);
                return false;
            }
        }
        private void SetProductStatus(ProductArchive archive)
        {
            if(db.products!=null)
                foreach(Product product in db.products)
                {
                    if (product == archive.product)
                    {
                        product.status = "in_stock";
                    }
                }
        }

    }
}
