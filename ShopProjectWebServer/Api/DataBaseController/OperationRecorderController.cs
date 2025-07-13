using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRecorderController : ControllerBase
    {
        [HttpGet("GetOperationRecorders")]
        public async Task<IActionResult> GetOperationRecorders(string token)
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

                    var operationRecorders = DataBaseMainController.DataBaseAccess.OperationRecorderTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(operationRecorders,options),
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

        [HttpPost("AddOperationRecorder")]
        public async Task<IActionResult> AddOperationRecorder(string token, OperationsRecorderEntity objectOwner)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 
                    DataBaseMainController.DataBaseAccess.OperationRecorderTable.Add(objectOwner);

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

        [HttpPost("AddOperationRecorders")]
        public async Task<IActionResult> AddOperationRecorders(string token, IEnumerable<OperationsRecorderEntity> objectOwner)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 

                    DataBaseMainController.DataBaseAccess.OperationRecorderTable.AddRange(objectOwner);

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
        [HttpPost("AddBindingOperationRecorder")]
        public async Task<IActionResult> AddBindingOperationRecorder(string token, string idoperationrecoreder , string idobjectowner)
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

                    DataBaseMainController.DataBaseAccess.OperationRecorderTable.AddBinding(Guid.Parse(idoperationrecoreder),Guid.Parse(idobjectowner));

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
    }
}
