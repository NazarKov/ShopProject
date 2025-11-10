using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Api.Interface.Services; 

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
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result ,"Mac створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
=======
                return Ok(ApiResponseDto<bool>.Ok(result ,"Mac створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message)); 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetLastMAC")]
        public async Task<IActionResult> GetLastMAC(string token, Guid operationRecorderId)
        {
            try
            {
                var result = _servise.GetLastMediaAccessControl(token, operationRecorderId);
<<<<<<< HEAD
                return Ok(ApiResponse<MediaAccessControlDto>.Ok(result));
=======
                return Ok(ApiResponseDto<MediaAccessControlDto>.Ok(result));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }
    }
}
