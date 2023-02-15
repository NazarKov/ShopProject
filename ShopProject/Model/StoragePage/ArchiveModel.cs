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
using Archive = ShopProject.DataBase.Model.Archive;

namespace ShopProject.Model.StoragePage
{
    internal class ArchiveModel
    {
        private ShopContext db;
        private List<Archive> archives;

        public ArchiveModel()
        {
            db = new ShopContext();
            archives = new List<Archive>();

            db.products.Load();
            db.archives.Load();
            if(db.archives!=null)
            archives = db.archives.ToList();
        }

        public List<Archive> GetItems()
        {
            return archives;
        }

        public List<Archive>? SearchArhive(string itemSearch, TypeSearch type)
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
        public bool DeleteRecordArhive(Archive archive,Product product)
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
        private void DeleteArhive(Archive archive)
        {
            if (db.archives != null)
            {
                db.archives.Remove(archive);
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
        public void ConvertToList(IList collection, List<Archive> itemConver)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                itemConver.Add((Archive)collection[i]);
                itemConver[i].product = ((Archive)collection[i]).product;
            }
        }
        
        public bool ReturnProductInStorage(Archive archive)
        {
            try
            {
                SetProductCount(archive);
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
        private void SetProductCount(Archive archive)
        {
            if(db.products!=null)
                foreach(Product product in db.products)
                {
                    if (product == archive.product)
                    {
                        product.count = 1;
                    }
                }
        }

    }
}
