using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ShopProject.Model.StoragePage.StorageModel;

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
        

    }
}
