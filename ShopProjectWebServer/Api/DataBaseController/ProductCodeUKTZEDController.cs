using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCodeUKTZEDController : ControllerBase
    {
        [HttpPost("AddCodeUKTZED")]
        public IActionResult AddCodeUKTZED(string token, ProductCodeUKTZEDEntity codeUKTZED)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.Add(codeUKTZED);


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

        [HttpPost("UpdateCodeUKTZED")]
        public IActionResult UpdateCodeUKTZED(string token, ProductCodeUKTZEDEntity codeUKTZED)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.Update(codeUKTZED);

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

        [HttpPost("UpdateParameterCodeUKTZED")]
        public IActionResult UpdateParameterCodeUKTZED(string token, [FromQuery] string parameter, [FromQuery] string value, ProductCodeUKTZEDEntity codeUKTZEDE)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.UpdateParameter(codeUKTZEDE, parameter, value);

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

        [HttpPost("DeleteCodeUKTZEDE")]
        public IActionResult DeleteCodeUKTZEDE(string token, ProductCodeUKTZEDEntity codeUKTZEDE)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.Delete(codeUKTZEDE);

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

        [HttpGet("GetCodeUKTZEDEByCode")]
        public IActionResult GetCodeUKTZEDEByCode(string token, string code, TypeStatusCodeUKTZED status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetCodeUKTZEDByCode(int.Parse(code), status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(products),
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

        [HttpGet("GetCodeUKTZEDByNamePageColumn")]
        public IActionResult GetCodeUKTZEDByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetCodeUKTZEDByNamePageColumn(name, page, countColumn, status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(products),
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

        [HttpGet("GetCodeUKTZEDPageColumn")]
        public IActionResult GetCodeUKTZEDPageColumn(string token, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetAllPageColumn(page, countColumn, status);

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(products),
                        Type = TypeMessage.Message
                    }.ToString());
                }
                throw new Exception("Невдалося отримати одиниці виміру");

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

        [HttpGet("GetCodeUKTZED")]
        public IActionResult GetCodeUKTZED(string token)
        {
            try
            {   
                if (AuthorizationApi.LoginToken(token))
                {
                    var codeUKTZED = DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetAll();
                
                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize<IEnumerable<ProductCodeUKTZEDEntity>>(codeUKTZED),
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
