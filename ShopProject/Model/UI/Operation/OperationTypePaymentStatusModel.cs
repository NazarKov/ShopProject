using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.Operation
{
    internal class OperationTypePaymentStatusModel
    {
        public static List<string> GetStatus()
        {
            return new List<string>()
            {
                "Готівка",
                "Карта", 
            };
        }
    }
}
