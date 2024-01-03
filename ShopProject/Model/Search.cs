using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    internal static class Search
    {
        private static List<Goods>? searchResult;

        public static List<Goods>? GoodsDataBase(string itemSearch, List<Goods> goods)
        {
            searchResult = new List<Goods>();

            for (int i = 0; i < goods.Count; i++)
            {
                if (goods[i].code.Contains(itemSearch))
                {
                    searchResult.Add(goods[i]);
                }
                else if (goods[i].name.ToLower().Contains(itemSearch.ToLower()))
                {
                    searchResult.Add(goods[i]);
                }
                else if (goods[i].articule.ToLower().ToLower().Contains(itemSearch.ToLower())) 
                {
                    searchResult.Add(goods[i]);
                }
            }
            return searchResult;
        }
    }
}
