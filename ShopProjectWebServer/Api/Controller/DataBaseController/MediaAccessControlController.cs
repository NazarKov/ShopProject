using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using System.Text.Json;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaAccessControlController : ControllerBase
    {
        [HttpPost("AddMAC")]
        public async Task<IActionResult> AddMAC(string token, CreateMediaAccessControlDto mediaAccessControl)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.MediaAccessControlTable.Add(mediaAccessControl.ToMediaAccessEntity());

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(true),
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

        [HttpGet("GetLastMAC")]
        public async Task<IActionResult> GetLastMAC(string token, Guid operationRecorderId)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var mac = DataBaseMainController.DataBaseAccess.MediaAccessControlTable.GetLastMAC(operationRecorderId).ToMediaAccessDto();

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(mac),
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
