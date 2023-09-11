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

        public static List<Goods>? GoodsDataBase(string itemSearch,List<Goods> goods)
        {
            searchResult = new List<Goods>();

            var item = goods.Where(item => item.code.Contains(itemSearch)).ToList();
            if(item.Count != 0 && item!=null)
            {
                searchResult.AddRange(item);
            }
            item = goods.Where(item => item.name.ToLower().Contains(itemSearch.ToLower())).ToList();
            if(item.Count != 0 && item != null)
            {
                searchResult.AddRange(item);
            }
            item = goods.Where(item => item.articule.ToLower().Contains(itemSearch.ToLower())).ToList();
            if (item.Count != 0 && item != null)
            {
                searchResult.AddRange(item);
            }
            return searchResult;
        }

    }
}
