using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRecorderController : ControllerBase
    {
        [HttpGet(nameof(GetOperationRecordersByNumberAndUser))]
        public IActionResult GetOperationRecordersByNumberAndUser(string token, string number, Guid userId)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                if (AuthorizationApi.LoginToken(token))
                {
                    var items = DataBaseMainController.DataBaseAccess.OperationRecorderTable.SearchByNumberAndUser(number, userId);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(items, options),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати товари");

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

        [HttpGet("GetOperationRecordersByNameAndUser")]
        public IActionResult GetOperationRecordersByNameAndUser(string token, string name , Guid userId)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                if (AuthorizationApi.LoginToken(token))
                {
                    var items = DataBaseMainController.DataBaseAccess.OperationRecorderTable.SearchByNameAndUser(name, userId);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(items, options),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати товари");

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

        [HttpPost("DeleteOperationRecorder")]
        public async Task<IActionResult> DeleteOperationRecorder([FromQuery] string token, OperationsRecorderEntity operationsRecorder)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {


                    DataBaseMainController.DataBaseAccess.OperationRecorderTable.Delete(operationsRecorder);

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

        [HttpGet("GetOperationRecordersByNamePageColumn")]
        public IActionResult GetOperationRecordersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                if (AuthorizationApi.LoginToken(token))
                {
                    var items = DataBaseMainController.DataBaseAccess.OperationRecorderTable.GetOperationRecorderByNamePageColumn(name, page, countColumn, status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(items, options),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати товари");

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

        [HttpGet("GetOperationRecordersPageColumn")]
        public IActionResult GetOperationRecordersPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                if (AuthorizationApi.LoginToken(token))
                {
                    var items = DataBaseMainController.DataBaseAccess.OperationRecorderTable.GetAllPageColumn(page, countColumn, status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(items, options),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати товари");

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
    }
}
