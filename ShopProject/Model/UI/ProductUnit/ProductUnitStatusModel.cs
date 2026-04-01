using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.ProductUnit
{
    internal class ProductUnitStatusModel
    {
        public static List<string> GetStatus()
        {
            return new List<string>()
            {
                "Не визначений",
                "Обраний",
                "Не обраний", 
            };
        }
        public static List<string> GetStatusForStorage()
        {
            return new List<string>()
            {
                "Статус (Всі)",
                "Статус (Обраний)",
                "Статус (Не обраний)", 
            };
        }
    }
}
