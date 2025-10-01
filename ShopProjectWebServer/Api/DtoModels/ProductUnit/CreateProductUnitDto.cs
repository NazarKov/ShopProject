using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.DtoModels.ProductUnit
{
    public class CreateProductUnitDto
    {
        public string NameUnit { get; set; } = string.Empty; 
        public string ShortNameUnit { get; set; } = string.Empty; 
        public int Number { get; set; } = 0; 
        public int Status { get; set; }
    }
}
