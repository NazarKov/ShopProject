using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectOwnerController : ControllerBase
    {
        [HttpGet("GetObjectsOwners")]
        public async Task<IActionResult> GetObjectsOwners(string token)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var objectsOwners = DataBaseMainController.DataBaseAccess.ObjectOwnerTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(objectsOwners),
                            Type = TypeMessage.Message
                        }.ToString());
                    }
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
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        DataBaseMainController.DataBaseAccess.ObjectOwnerTable.Add(objectOwner);

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize<bool>(true),
                            Type = TypeMessage.Message
                        }.ToString());
                    }
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
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        DataBaseMainController.DataBaseAccess.ObjectOwnerTable.AddRange(objectOwner);

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize<bool>(true),
                            Type = TypeMessage.Message
                        }.ToString());
                    }
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
