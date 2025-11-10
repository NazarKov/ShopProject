using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Api.DtoModels.UserRole;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Services;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUserRoleServise _servise;

        public UserRoleController(IUserRoleServise servise)
        {
            _servise = servise;
        }
    
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles(string token)
        {
            try
            {
                var result = _servise.GetAll(token);

<<<<<<< HEAD
                return Ok(ApiResponse<IEnumerable<UserRoleDto>>.Ok(result));
=======
                return Ok(ApiResponseDto<IEnumerable<UserRoleDto>>.Ok(result));
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
