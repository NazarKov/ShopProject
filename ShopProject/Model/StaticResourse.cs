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
       public static Product? product { get; set; } 
       public static List<Product>? products { get; set; }
    }
}
