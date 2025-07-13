using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper.ProductContoller
{
    public class ProductInfo
    {
        public int CountProductAllStatus { get; set; }
        public int CountProductInStockStatus { get; set; }
        public int CountProductOutStockStatus { get; set; }
        public int CountProductArchivedStauts { get; set; }
    
        public ProductInfo() { }
    }
}
