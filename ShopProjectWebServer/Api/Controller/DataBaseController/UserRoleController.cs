using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
    
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles(string token)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 
                    var roles = DataBaseMainController.DataBaseAccess.UserRoleTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(roles),
                            Type = TypeMessage.Message
                        }.ToString()); 
                }
                throw new Exception("Невірний токен авторизації");

            }
            catch (Exception ex)
            {
                return BadRequest(new Message()
                {
                    MessageBody = ex.ToString(),
                    Type = TypeMessage.Error,

                }.ToString());
            }
        }
    }
}
