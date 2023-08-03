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
        private static List<GoodsArchive>? searchResultArhive;
        private static List<GoodsOutOfStock>? searchResultProductsOutOfStock;

        public static List<Goods>? ProductDataBase(string itemSearch, TypeSearch type,List<Goods> products)
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
        public static List<GoodsArchive>? ArhiveDataBase(string itemSearch, TypeSearch type, List<GoodsArchive> archives)
        {
            searchResultArhive = new List<GoodsArchive>();
            switch (type)
            {
                case TypeSearch.Code:
                    {
                        foreach (GoodsArchive archive in archives)
                        {
                            //if (archive.goods != null)
                            //{
                            //    if (archive.goods.code == itemSearch)
                            //    {
                            //        searchResultArhive.Add(archive);
                            //    }
                            //}
                        }
                        return searchResultArhive;
                    }
                case TypeSearch.Name:
                    {
                        foreach (GoodsArchive archive in archives)
                        {
                            //if (archive.goods != null)
                            //{
                            //    if (archive.goods.name != null)
                            //        if (archive.goods.name.ToLower().ToString().Contains(itemSearch.ToLower()))
                            //        {
                            //            searchResultArhive.Add(archive);
                            //        }

                            //}
                        }
                        return searchResultArhive;
                    }
                case TypeSearch.Articule:
                    {
                        foreach (GoodsArchive archive in archives)
                        {
                            //if (archive.goods != null)
                            //    if (archive.goods.articule != null)
                            //        if (archive.goods.articule.ToLower().ToString().Contains(itemSearch.ToLower()))
                            //        {
                            //            searchResultArhive.Add(archive);
                            //        }
                        }

                        return searchResultArhive;
                    }
                default:
                    {
                        throw new Exception("Товар не знайдено");
                    }
            }
        }
        public static List<GoodsOutOfStock>? OutOfStockProductDataBase(string itemSearch, TypeSearch type, List<GoodsOutOfStock> products)
        {
            searchResultProductsOutOfStock = new List<GoodsOutOfStock>();
            switch (type)
            {
                case TypeSearch.Code:
                    {
                        foreach (GoodsOutOfStock product in products)
                        {   
                            //if (product.goods != null)
                            //{
                            //    if (product.goods.code == itemSearch)
                            //    {
                            //        searchResultProductsOutOfStock.Add(product);
                            //    }
                            //}
                        }
                        return searchResultProductsOutOfStock;
                    }
                case TypeSearch.Name:
                    {
                        foreach (GoodsOutOfStock product in products) { 
                        //{
                        //    if (product.goods != null)
                        //        if (product.goods.name != null)
                        //            if (product.goods.name.ToLower().ToString().Contains(itemSearch.ToLower()))
                        //            {
                        //                searchResultProductsOutOfStock.Add(product);
                        //            }
                        }

                        return searchResultProductsOutOfStock;
                    }
                case TypeSearch.Articule:
                    {
                        foreach (GoodsOutOfStock product in products)
                        {
                            //if (product.goods != null)
                            //    if (product.goods.articule != null)
                            //        if (product.goods.articule.ToLower().ToString().Contains(itemSearch.ToLower()))
                            //        {
                            //            searchResultProductsOutOfStock.Add(product);
                            //        }
                        }

                        return searchResultProductsOutOfStock;
                    }
                default:
                    {
                        throw new Exception("Товар не знайдено");
                    }
            }
        }
    }
}
