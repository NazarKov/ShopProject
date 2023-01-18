using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;

namespace ShopProject.Model.StoragePage
{
    internal class StorageModel
    {
        public enum TypeSearch
        {
            Name = 0,
            Code = 1,
        };


        ShopContext db;
        List<Product> products;

        public StorageModel()
        {
            db = new ShopContext();
            products = new List<Product>();

        }

        public List<Product> GetItemsLoadDb()
        {
            db = new ShopContext();
            db.products.Load();
            products = db.products.ToList();
            return db.products.ToList();
        }
        public List<Product> GetItems()
        {
            return db.products.ToList();
        }

        public List<Product>? Search(string itemSearch, TypeSearch type)
        {
            products = new List<Product>();
            products = db.products.ToList();
            List<Product> searchResult = new List<Product>();

            switch (type)
            {
                case TypeSearch.Name:
                    {
                        foreach (Product product in products)
                        {
                            if (product.name == itemSearch)
                            {
                                searchResult.Add(product);
                            }
                        }

                        return searchResult;
                    }
                case TypeSearch.Code:
                    {
                        foreach (Product product in products)
                        {
                            if (product.code == itemSearch)
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

        public void DeleteProduct(Product productDelete)
        {

            db.products.Remove(productDelete);
            db.SaveChangesAsync();
        }


    }
}
