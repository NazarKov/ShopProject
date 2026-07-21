using ShopProjectWebServer.Models.Domain.BootStrap;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Modules.BootStrap.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit.Interface;
using ShopProjectWebServer.Services.Modules.Domain.UserRole.Interface;

namespace ShopProjectWebServer.Services.Modules.BootStrap
{
    public class BootStrapService : IBootStrapService
    {
        private readonly IUserRoleServiсe _userRoleServiсe;
        private readonly IProductUnitService _productUnitService;
        private readonly IProductCodeUKTZEDService _productCodeUKTZEDService;

        public BootStrapService(IUserRoleServiсe userRoleServiсe, IProductUnitService productUnitService, IProductCodeUKTZEDService productCodeUKTZEDService)
        {
            _userRoleServiсe = userRoleServiсe;
            _productUnitService = productUnitService;
            _productCodeUKTZEDService = productCodeUKTZEDService;
        }

        public OperationResult<StartData> GetStartData()
        {
            var roles = _userRoleServiсe.GetAll();
            var productunit = _productUnitService.GetAll();
            var productcodeUktzed = _productCodeUKTZEDService.GetAll();

            var result = new OperationResult<StartData>();
            result.Data = new StartData();

            if (roles.IsSuccess)
            {
                result.Data.Roles = roles.Data;
            }

            if (productunit.IsSuccess) 
            {
                result.Data.ProductUnit = productunit.Data;
            }

            if (productcodeUktzed.IsSuccess) 
            {
                result.Data.ProductCodeUKTZED = productcodeUktzed.Data;
            }

            result.Status = Common.Enum.ResultStatus.Success; 
            return result;  
        }

    }
}
