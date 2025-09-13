using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpPost("AddOperation")]
        public  IActionResult AddOperationRecorder([FromQuery] string token, CreateOperationDto operation)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.OperationTable.Add(operation.ToOperationEntiti());

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

        [HttpGet("GetOperations")]
        public async Task<IActionResult> GetOperations(string token)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };
 
                    var operations = DataBaseMainController.DataBaseAccess.OperationTable.GetAll();

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(operations,options),
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

        [HttpGet("GetLastOperation")]
        public async Task<IActionResult> GetLastOperation(string token,int shiftId)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };

                    var operation = DataBaseMainController.DataBaseAccess.OperationTable.GetLastItem(shiftId);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(operation, options),
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
