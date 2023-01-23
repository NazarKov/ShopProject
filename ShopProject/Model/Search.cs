using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    public enum TypeSearch
    {
        Name = 0,
        Code = 1,
    };


    internal static class Search
    {
        private static List<Product>? searchResult;

        public static List<Product>? ProductDataBase(string itemSearch, TypeSearch type,List<Product> products)
        {
            searchResult = new List<Product>();
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
                        throw new Exception("Товар не знайдено");
                    }
            }
        }
    }
}
