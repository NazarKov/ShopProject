using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.ProductCodeUKTZED
{
    internal class ProductCodeUKTZEDStatusModel
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
