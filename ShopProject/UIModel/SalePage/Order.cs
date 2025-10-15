using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{
    public class Order
    { 
        public int ID { get; set; }
        /// <summary>
        /// кількість товару
        /// </summary>
        public int Count { get; set; } = 0;
        /// <summary>
        /// товар
        /// </summary>
        public Product? Product { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public Operation? Operation { get; set; }
    }
}
