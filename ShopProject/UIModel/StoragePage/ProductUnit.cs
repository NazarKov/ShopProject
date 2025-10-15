using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.StoragePage
{
    public class ProductUnit
    {
        public int ID { get; set; } 
        public string NameUnit { get; set; } = string.Empty; 
        public string ShortNameUnit { get; set; } = string.Empty; 
        public int Number { get; set; } = 0; 
        public TypeStatusUnit Status { get; set; }
    }
}
