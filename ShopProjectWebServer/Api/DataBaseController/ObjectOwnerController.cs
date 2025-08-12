using Microsoft.AspNetCore.Mvc; 
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectOwnerController : ControllerBase
    {
        [HttpPost("DeleteObjectsOwner")]
        public async Task<IActionResult> DeleteObjectsOwner([FromQuery] string token, ObjectOwnerEntity item)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {


                    DataBaseMainController.DataBaseAccess.ObjectOwnerTable.Delete(item);

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

        [HttpGet("GetObjectsOwnersByNamePageColumn")]
        public IActionResult GetObjectsOwnersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusObjectOwner status)
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
                    var items = DataBaseMainController.DataBaseAccess.ObjectOwnerTable.GetObjectOwnerByNamePageColumn(name, page, countColumn, status);

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

        [HttpGet("GetObjectsOwnersPageColumn")]
        public IActionResult GetObjectsOwnersPageColumn(string token, int page, int countColumn, TypeStatusObjectOwner status)
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
                    var items = DataBaseMainController.DataBaseAccess.ObjectOwnerTable.GetAllPageColumn(page, countColumn, status);

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


        [HttpGet("GetObjectsOwners")]
        public async Task<IActionResult> GetObjectsOwners(string token)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var objectsOwners = DataBaseMainController.DataBaseAccess.ObjectOwnerTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(objectsOwners),
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

        [HttpPost("AddObjectOwner")]
        public async Task<IActionResult> AddObjectOwner(string token, ObjectOwnerEntity objectOwner)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.ObjectOwnerTable.Add(objectOwner);

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

        [HttpPost("AddObjectsOwners")]
        public async Task<IActionResult> AddObjectsOwners(string token, IEnumerable<ObjectOwnerEntity> objectOwner)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.ObjectOwnerTable.AddRange(objectOwner);

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
