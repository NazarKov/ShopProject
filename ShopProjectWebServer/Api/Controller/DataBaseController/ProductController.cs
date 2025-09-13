using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("GetInfoProducts")]
        public IActionResult GetInfoProducts(string token)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var informations = DataBaseMainController.DataBaseAccess.ProductTable.GetProductInfo();

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(informations),
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

        //[HttpGet("GetProductsByBarCode")]
        //public IActionResult GetProductsByBarCode(string token, string barCode, TypeStatusProduct status)
        //{
        //    try
        //    {
        //        if (AuthorizationApi.LoginToken(token))
        //        {
        //            var products = DataBaseMainController.DataBaseAccess.ProductTable.GetByBarCode(barCode, status);

        //            return Ok(new Message()
        //            {
        //                MessageBody = JsonSerializer.Serialize(products),
        //                Type = TypeMessage.Message
        //            }.ToString());
        //        }
        //        throw new Exception("Невдалося отримати товари");

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new Message()
        //        {
        //            MessageBody = ex.ToString(),
        //            Type = TypeMessage.Error,

        //        }.ToString());
        //    }
        //}

        [HttpGet("GetProductsByBarCode")]
        public IActionResult GetProductsByBarCode(string token, string barCode)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductTable.GetByBarCode(barCode);

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

        [HttpGet("GetProductByNamePageColumn")]
        public IActionResult GetProductByNamePageColumn(string token,string name, int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductTable.GetProductByNamePageColumn(name,page,countColumn ,status);

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

        [HttpGet("GetProductsPageColumn")]
        public IActionResult GetProductsPageColumn(string token, int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductTable.GetAllPageColumn(page,countColumn, status);

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

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(string token)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {
                    var products = DataBaseMainController.DataBaseAccess.ProductTable.GetAll();

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

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(string token, ProductEntity product)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 

                    var productItem = DataBaseMainController.DataBaseAccess.ProductTable.GetAll();
                        if (productItem.Count() > 0)
                        {
                            var item = productItem.FirstOrDefault(i => i.Code == product.Code);

                            if (item != null)
                            {
                                throw new Exception("Товар існує");
                            }
                        }
                        DataBaseMainController.DataBaseAccess.ProductTable.Add(product);


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

        [HttpPost("AddProductRange")]
        public async Task<IActionResult> AddProductRange(string token, List<ProductEntity> product)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 

                    DataBaseMainController.DataBaseAccess.ProductTable.AddRange(product);


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

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string token, ProductEntity product)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 

                    DataBaseMainController.DataBaseAccess.ProductTable.Update(product);

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

        [HttpPost("UpdateProductRange")]
        public async Task<IActionResult> UpdateProductRange(string token, List<ProductEntity> product)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 
                    DataBaseMainController.DataBaseAccess.ProductTable.UpdateRange(product);

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

        [HttpPost("UpdateParameterProduct")]
        public async Task<IActionResult> UpdateParameterProduct(string token,[FromQuery]string parameter , [FromQuery]string value ,ProductEntity product)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                { 
                    DataBaseMainController.DataBaseAccess.ProductTable.UpdateParameter(product, parameter, value);

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
