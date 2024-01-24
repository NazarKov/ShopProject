using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopProject.Helpers
{
    internal static class StaticResourse
    {
        public static string nameCompany = "Дім рибалки";
        public static Goods? goods { get; set; }
        public static List<Goods>? goodsList { get; set; }
        public static ObservableCollection<TabItem> tabs = new ObservableCollection<TabItem>();

        public static void Clear()
        {
            goods = null;
            goodsList = null;
        }
    }
}
