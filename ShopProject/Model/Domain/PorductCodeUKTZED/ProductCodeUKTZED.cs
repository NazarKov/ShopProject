using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.PorductCodeUKTZED
{
    public class ProductCodeUKTZED
    {
        public int ID { get; set; }
        public string NameCode { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public TypeStatusCodeUKTZED Status { get; set; }
    }
}
