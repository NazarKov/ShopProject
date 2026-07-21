using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.TaxObject
{
    internal class TaxObjectStatusModel
    {
        public static List<string> GetTaxObjectStatus()
        {
            return new List<string>()
            {
                "Не визначений",
                "Доступний для використання",
                "Не доступний для використання",
            };
        }
        public static List<string> GetTaxObjectStatusForStorage()
        {
            return new List<string>()
            {
                "Статус (Всі)",
                "Статус (Доступний для використання)",
                "Статус (Не доступний для використання)",
            };
        }
    }
}
