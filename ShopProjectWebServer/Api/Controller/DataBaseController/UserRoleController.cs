using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.DtoModels.UserRole;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Domain.UserRole.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.UserRole;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUserRoleServiсe _service;

        public UserRoleController(IUserRoleServiсe service)
        {
            _service = service;
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles(string token)
        {
            try
            {
                var result = _service.GetAll();
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<IEnumerable<UserRoleDto>>.Ok(result.Data.ToUserRoleDto()));
                }
                else
                {
                    return Ok(ApiResponse<IEnumerable<UserRoleDto>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
