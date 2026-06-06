
using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Models.Domain.ProductUnit
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
