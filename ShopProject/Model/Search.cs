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
        private static Goods? item;
        private static List<Goods>? searchResult;

        public static List<Goods>? GoodsDataBase(string itemSearch, TypeSearch type,List<Goods> products)
        {
            searchResult = new List<Goods>();
            switch (type)
            {
                case TypeSearch.Code:
                    {
                        foreach (Goods product in products)
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
                        foreach (Goods product in products)
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
                        foreach (Goods product in products)
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

    }
}
