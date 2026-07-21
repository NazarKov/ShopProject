using ShopProjectWebServer.Api.DtoModels.BootStrap;
using ShopProjectWebServer.Services.Modules.Mapping.ProductCodeUKTZED;
using ShopProjectWebServer.Services.Modules.Mapping.ProductUnit;
using ShopProjectWebServer.Services.Modules.Mapping.UserRole;

namespace ShopProjectWebServer.Services.Modules.Mapping.BootStrap
{
    public static class StartDataApiMappingExtensions
    {
        public static StartDataDto ToStartDataDto (this ShopProjectWebServer.Models.Domain.BootStrap.StartData item)
        {
            return new StartDataDto()
            {
                ProductCodeUKTZEDs = item.ProductCodeUKTZED.ToProductCodeUKTZEDDto(),
                ProductUnits = item.ProductUnit.ToProductUnit(),
                Roles = item.Roles.ToUserRoleDto(),
            };
        }
    }
}
