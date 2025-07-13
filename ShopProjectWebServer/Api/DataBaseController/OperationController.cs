using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpPost("AddOperation")]
        public async Task<IActionResult> AddOperationRecorder(string token, OperationEntity operation)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.OperationTable.Add(operation);

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize<bool>(true),
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
        public async Task<IActionResult> GetLastOperation(string token)
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

                    var operation = DataBaseMainController.DataBaseAccess.OperationTable.GetLastItem();

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
