using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.Order
{
    public class CreateOrderDto
    { 
        /// <summary>
        /// кількість товару
        /// </summary>
        public int Count { get; set; } = 0;
        /// <summary>
        /// товар
        /// </summary>
        public Guid ProductID { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public int OperationID { get; set; }
    }
}
