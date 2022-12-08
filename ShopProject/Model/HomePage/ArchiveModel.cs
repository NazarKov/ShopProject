using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static ShopProject.Model.HomePage.StorageModel;

namespace ShopProject.Model.HomePage
{
    internal class ArchiveModel
    {
        public enum TypeSearch
        {
            Name = 0,
            Code = 1,
        };

        ShopContext db;
        List<Archive>  archives;

        public ArchiveModel()
        {
            db = new ShopContext();
            archives = new List<Archive>();


        }

        public List<Archive> GetItemsLoadDb()
        {
            db.products.Load();
            db.archives.Load();
            archives = db.archives.ToList();
            return db.archives.ToList();
        }
        public List<Archive> GetItems()
        {
            return db.archives.ToList();
        }

        public List<Archive>? Search(string itemSearch, TypeSearch type)
        {
            archives = new List<Archive>();
            archives = db.archives.ToList();
            List<Archive> searchResult = new List<Archive>();

            switch (type)
            {
                case TypeSearch.Name:
                    {
                        foreach (Archive product in archives)
                        {
                            if (product.product.name == itemSearch)
                            {
                                searchResult.Add(product);
                            }
                        }

                        return searchResult;
                    }
                case TypeSearch.Code:
                    {
                        foreach (Archive product in archives)
                        {
                            if (product.product.code == itemSearch)
                            {
                                searchResult.Add(product);
                            }
                        }
                        return searchResult;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

    }
}
