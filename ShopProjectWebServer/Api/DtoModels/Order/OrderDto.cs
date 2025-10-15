using ShopProjectDataBase.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopProjectWebServer.Api.DtoModels.Order
{
    public class OrderDto
    { 
        public int ID { get; set; } 
        public int Count { get; set; } = 0; 
        public Guid? ProductID { get; set; } 
        public int? OperationID { get; set; }
    }
}
