using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED
{
    public class UpdateProductCodeUKTZEDDto
    {
        public int ID { get; set; }
        public string NameCode { get; set; } = string.Empty; 
        public string Code { get; set; } = string.Empty; 
        public int Status { get; set; }
    }
}
