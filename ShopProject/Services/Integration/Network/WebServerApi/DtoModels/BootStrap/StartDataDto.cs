using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductCodeUKTZED;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ProductUnit;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.UserRole; 
using System.Collections.Generic; 
using System.Text.Json.Serialization; 

namespace ShopProject.Services.Integration.Network.WebServerApi.DtoModels.BootStrap
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
