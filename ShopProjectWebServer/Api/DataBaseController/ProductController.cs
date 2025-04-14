using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Reflection.Metadata;
using System.Text.Json;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(string token)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var products = DataBaseMainController.DataBaseAccess.ProductTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(products),
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

        [HttpGet("GetProductsByBarCode")]
        public async Task<IActionResult> GetProductsByBarCode(string token,string barcode)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var product = DataBaseMainController.DataBaseAccess.ProductTable.GetByBarCode(barcode);

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(product),
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

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(string token, ProductEntity product)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {

                        var productItem = DataBaseMainController.DataBaseAccess.ProductTable.GetAll();
                        if (productItem.Count() > 0)
                        {
                            var item = productItem.Where(i => i.Code == product.Code).FirstOrDefault();

                            if (item != null)
                            {
                                throw new Exception("Товар існує");
                            }
                        }
                        DataBaseMainController.DataBaseAccess.ProductTable.Add(product);


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

        [HttpPost("AddProductRange")]
        public async Task<IActionResult> AddProductRange(string token, List<ProductEntity> product)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {

                        DataBaseMainController.DataBaseAccess.ProductTable.AddRange(product);


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

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string token, ProductEntity product)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        DataBaseMainController.DataBaseAccess.ProductTable.Update(product);

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

        [HttpPost("UpdateProductRange")]
        public async Task<IActionResult> UpdateProductRange(string token, List<ProductEntity> product)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        DataBaseMainController.DataBaseAccess.ProductTable.UpdateRange(product);

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

        [HttpPost("UpdateParameterProduct")]
        public async Task<IActionResult> UpdateParameterProduct(string token,[FromQuery]string parameter , [FromQuery]string value ,ProductEntity product)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        DataBaseMainController.DataBaseAccess.ProductTable.UpdateParameter(product, parameter, value);

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
