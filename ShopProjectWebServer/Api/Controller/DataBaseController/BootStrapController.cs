using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.BootStrap;
using ShopProjectWebServer.Services.Modules.BootStrap.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.BootStrap;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootStrapController : ControllerBase
    {
        private IBootStrapService _bootStrapService;

        public BootStrapController(IBootStrapService bootStrapService)
        {
            _bootStrapService = bootStrapService;
        }

        [AllowAnonymous]
        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                var result = _bootStrapService.GetStartData();
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<StartDataDto>.Ok(result.Data.ToStartDataDto()));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
