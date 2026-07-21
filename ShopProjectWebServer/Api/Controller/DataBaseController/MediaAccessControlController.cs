using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Services.Modules.Domain.MediaAccessControl;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaAccessControlController : ControllerBase
    {
        private readonly IMediaAccessContolServise _servise;
        public MediaAccessControlController(IMediaAccessContolServise mediaAccessContolServise)
        {
            _servise = mediaAccessContolServise;
        }


        [HttpPost("AddMAC")]
        public async Task<IActionResult> AddMAC(string token, CreateMediaAccessControlDto mediaAccessControl)
        {
            try
            {
                var result = _servise.Add(token, mediaAccessControl);  
                return Ok(ApiResponse<bool>.Ok(result ,"Mac створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));  
            } 
        } 
    }
}
