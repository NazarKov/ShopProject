using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.StoragePage
{
    public class ProductCodeUKTZED
    {
        public int ID { get; set; } 
        public string NameCode { get; set; } = string.Empty; 
        public string Code { get; set; } = string.Empty; 
        public TypeStatusCodeUKTZED Status { get; set; }
    }
}
