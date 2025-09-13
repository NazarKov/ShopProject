using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json.Serialization;
using System.Text.Json; 
using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.Mappings;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost("AddOrderRange")]
        public async Task<IActionResult> AddOrderRange(string token, List<CreateOrderDto> order)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {  
                    DataBaseMainController.DataBaseAccess.OrderTable.AddRange(order.ToListOrderEntity());


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

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(string token)
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

                    var operations = DataBaseMainController.DataBaseAccess.OrderTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize(operations, options),
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
