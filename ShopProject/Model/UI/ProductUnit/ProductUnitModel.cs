using ShopProject.Model.Enum;
using ShopProject.Model.UI.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.ProductUnit
{
    public class ProductUnitModel
    {
        public int ID { get; set; }
        public string NameUnit { get; set; } = string.Empty;
        public string ShortNameUnit { get; set; } = string.Empty;
        public int Number { get; set; } = 0;
        public TypeStatusUnit Status { get; set; }

        public string StatusString
        {
            get { return ProductUnitStatusModel.GetStatus().ElementAt(System.Enum.GetValues<TypeStatusUnit>().ToList().IndexOf(Status)); }
            set { Status = System.Enum.GetValues<TypeStatusUnit>().ToList().ElementAt(ProductUnitStatusModel.GetStatus().IndexOf(value)); }
        }

    }
}
