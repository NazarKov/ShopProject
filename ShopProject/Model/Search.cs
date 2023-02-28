using NPOI.SS.Formula.Functions;
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
        Articule = 2,
    };

    internal static class Search
    {
        private static Product? item;
        private static List<Product>? searchResult;
        private static List<ProductArchive>? searchResultArhive;

        public static List<Product>? ProductDataBase(string itemSearch, TypeSearch type,List<Product> products)
        {
            searchResult = new List<Product>();
            switch (type)
            {
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
                case TypeSearch.Name:
                    {
                        foreach (Product product in products)
                        {
                            if(product.name!=null)
                                if (product.name.ToLower().ToString().Contains(itemSearch.ToLower()))
                                {
                                    searchResult.Add(product);
                                }
                        }

                        return searchResult;
                    }
                case TypeSearch.Articule:
                    {
                        foreach (Product product in products)
                        {
                            if(product.articule !=null)
                                if (product.articule.ToLower().ToString().Contains(itemSearch.ToLower()))
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
        public static List<ProductArchive>? ArhiveDataBase(string itemSearch, TypeSearch type, List<ProductArchive> archives)
        {
            searchResultArhive = new List<ProductArchive>();
            switch (type)
            {
                case TypeSearch.Code:
                    {
                        foreach (ProductArchive archive in archives)
                        {
                            if (archive.Product != null)
                            {
                                if (archive.Product.code == itemSearch)
                                {
                                    searchResultArhive.Add(archive);
                                }
                            }
                        }
                        return searchResultArhive;
                    }
                case TypeSearch.Name:
                    {
                        foreach (ProductArchive archive in archives)
                        {
                            if (archive.Product != null)
                            {
                                if (archive.Product.name != null)
                                    if (archive.Product.name.ToLower().ToString().Contains(itemSearch.ToLower()))
                                    {
                                        searchResultArhive.Add(archive);
                                    }

                            }
                        }
                        return searchResultArhive;
                    }
                case TypeSearch.Articule:
                    {
                        foreach (ProductArchive archive in archives)
                        {
                            if (archive.Product != null)
                                if (archive.Product.articule != null)
                                    if (archive.Product.articule.ToLower().ToString().Contains(itemSearch.ToLower()))
                                    {
                                        searchResultArhive.Add(archive);
                                    }
                        }

                        return searchResultArhive;
                    }
                default:
                    {
                        throw new Exception("Товар не знайдено");
                    }
            }
        }
       
    }
}
