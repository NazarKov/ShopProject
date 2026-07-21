using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.DtoModels.UserRole;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DtoModels.BootStrap
{
    public class StartDataDto
    {
        [JsonPropertyName("Roles")]
        public IEnumerable<UserRoleDto>? Roles { get; set; }
        [JsonPropertyName("ProductCodeUKTZEDs")]
        public IEnumerable<ProductCodeUKTZEDDto>? ProductCodeUKTZEDs { get; set; } 
        [JsonPropertyName("ProductUnits")]
        public IEnumerable<ProductUnitDto>? ProductUnits { get; set; }
    }
}
