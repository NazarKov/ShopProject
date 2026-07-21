
namespace ShopProjectWebServer.Models.Domain.BootStrap
{
    public class StartData
    {
        public IEnumerable<ShopProjectWebServer.Models.Domain.UserRole.UserRole>  Roles {  get; set; } = new List<ShopProjectWebServer.Models.Domain.UserRole.UserRole>();
        public IEnumerable<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED> ProductCodeUKTZED { get; set; } = new List<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED>();
        public IEnumerable<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit> ProductUnit { get; set; } = new List<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit>();
    }
}
