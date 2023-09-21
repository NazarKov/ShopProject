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

            //var item = goods.Where(item => item.code.Contains(itemSearch)).ToList();
            //if (item.Count != 0 && item != null)
            //{
            //    searchResult.AddRange(item);
            //}
            //item = goods.Where(item => item.name.ToLower().Contains(itemSearch.ToLower())).ToList();
            //if (item.Count != 0 && item != null)
            //{
            //    searchResult.AddRange(item);
            //}
            //item = goods.Where(item => item.articule.ToLower().Contains(itemSearch.ToLower())).ToList();
            //if (item.Count != 0 && item != null)
            //{
            //    searchResult.AddRange(item);
            //}
            ////return SearchUsers(goods, itemSearch);
            //return searchResult;

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
    //    static List<Goods> SearchUsers(List<Goods> users, string searchQuery) // модифікований пошук доробити шукає тільки по першому слові а не по рядку
    //    {
    //        List<Goods> results = new List<Goods>();

    //        foreach (Goods user in users)
    //        {
    //            if (IsMatch(user, searchQuery))
    //            {
    //                results.Add(user);
    //            }
    //        }

    //        return results;
    //    }

    //    static bool IsMatch(Goods user, string searchQuery)
    //    {
    //        int queryLength = searchQuery.Length;

    //        if (queryLength == 0)
    //        {
    //            return true; // Пустий пошуковий запит відповідає всім користувачам
    //        }

    //        string[] searchParts = searchQuery.Split(' ');

    //        foreach (string searchPart in searchParts)
    //        {
    //            bool isPartMatched = false;

    //            if (user.code.Length >= queryLength && user.code.Substring(0, queryLength).Equals(searchPart, StringComparison.OrdinalIgnoreCase))
    //            {
    //                isPartMatched = true;
    //            }
    //            else if (user.name.Length >= queryLength && user.name.Substring(0, queryLength).Equals(searchPart, StringComparison.OrdinalIgnoreCase))
    //            {
    //                isPartMatched = true;
    //            }
    //            else if (user.articule.Length >= queryLength && user.articule.Substring(0, queryLength).Equals(searchPart, StringComparison.OrdinalIgnoreCase))
    //            {
    //                isPartMatched = true;
    //            }

    //            if (!isPartMatched)
    //            {
    //                return false; // Якщо хоча б одна частина не знайдена в імені, прізвищі або по батькові, користувач не підходить
    //            }
    //        }

    //        return true; // Всі частини пошукового запиту знайдені в імені, прізвищі або по батькові користувача
    //    }

    //}
}
