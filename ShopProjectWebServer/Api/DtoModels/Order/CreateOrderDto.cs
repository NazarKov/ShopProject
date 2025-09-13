using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopProjectWebServer.Api.DtoModels.Order
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
