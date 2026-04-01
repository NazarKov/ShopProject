using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.Product
{
    internal class ProductStatusModel
    { 
        public static List<string> GetProductStatus()
        {
            return new List<string>()
            {
                "Не визначений",
                "Готовий до продажі",
                "Товар закінчився",
                "Товар aрхівовано",
                "Товар потрібно редагувати",
            };
        }
        public static List<string> GetProductStatusForStorage()
        {
            return new List<string>()
            {
                "Статус (Всі)",
                "Статус (Готові для продажі)",
                "Статус (Не в наявності)",
                "Статус (Архівовані)",
                "Статус (Потрібно редагувати)",
            };
        }
    }
}
