using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    internal static class StaticResourse
    {
       public static string nameCompany = "Дім рибалки";
       public static Goods? product { get; set; } 
       public static List<Goods>? products { get; set; }

       public static void Clear()
       {
            product = null;
            products = null;
       }
    }
}
