using ShopProject.Model.Enum;
using ShopProject.Model.UI.ProductUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.ProductCodeUKTZED
{
    public class ProductCodeUKTZEDModel
    {
        public int ID { get; set; }
        public string NameCode { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public TypeStatusCodeUKTZED Status { get; set; }

        public string StatusString
        {
            get { return ProductCodeUKTZEDStatusModel.GetStatus().ElementAt(System.Enum.GetValues<TypeStatusCodeUKTZED>().ToList().IndexOf(Status)); }
            set { Status = System.Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(ProductCodeUKTZEDStatusModel.GetStatus().IndexOf(value)); }
        }
    }
}
