using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {

        [HttpPost("AddUnit")]
        public IActionResult AddUnit(string token, CreateProductUnitDto unit)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.ProductUnitTable.Add(unit.ToProductUnitEntity());


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

        [HttpPost("UpdateUnit")]
        public IActionResult UpdateUnit(string token, UpdateProductUnitDto unit)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    DataBaseMainController.DataBaseAccess.ProductUnitTable.Update(unit.ToProductUnitEntity());

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

        [HttpPost("UpdateParameterUnit")]
        public IActionResult UpdateParameterUnit(string token, [FromQuery] string parameter, [FromQuery] string value, UpdateProductUnitDto unit)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    DataBaseMainController.DataBaseAccess.ProductUnitTable.UpdateParameter(unit.ToProductUnitEntity(), parameter, value);

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

        [HttpPost("DeleteUnit")]
        public IActionResult DeleteUnit(string token, ProductUnitEntity unit)// переробити на int ID
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {

                    DataBaseMainController.DataBaseAccess.ProductUnitTable.Delete(unit);

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

        [HttpGet("GetUnitByCode")]
        public IActionResult GetUnitByCode(string token, string code, TypeStatusUnit status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductUnitTable.GetUnitByCode(int.Parse(code), status);

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

        [HttpGet("GetUnitsByNamePageColumn")]
        public IActionResult GetUnitsByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUnit status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductUnitTable.GetUnitByNamePageColumn(name, page, countColumn, status);

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

        [HttpGet("GetUnitsPageColumn")]
        public IActionResult GetUnitsPageColumn(string token, int page, int countColumn, TypeStatusUnit status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductUnitTable.GetAllPageColumn(page, countColumn, status);

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

        [HttpGet("GetUnits")]
        public IActionResult GetUnits(string token) 
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

                    var units = DataBaseMainController.DataBaseAccess.ProductUnitTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(units),
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
