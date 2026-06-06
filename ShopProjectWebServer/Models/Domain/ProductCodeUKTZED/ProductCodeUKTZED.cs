
using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Models.Domain.ProductCodeUKTZED
{
    public class ProductCodeUKTZED
    {
        public int ID { get; set; } 
        public string NameCode { get; set; } = string.Empty; 
        public string Code { get; set; } = string.Empty; 
        public TypeStatusCodeUKTZED Status { get; set; }
    }
}
