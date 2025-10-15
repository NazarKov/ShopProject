using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.DataBase;
using System.Text.Json.Serialization;
using System.Text.Json;
using ShopProjectWebServer.Api.DtoModels.Order;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.Services;
using ShopProjectWebServer.Api.Interface.Services;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderServise _servise;

        public OrderController(IOrderServise servise)
        {
            _servise = servise;
        }

        [HttpPost("AddOrderRange")]
        public async Task<IActionResult> AddOrderRange(string token, IEnumerable<CreateOrderDto> orders)
        {
            try
            {
                _servise.AddRange(token, orders);

                return Ok(ApiResponseDto<bool>.Ok(true, "Обєкти створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(string token)
        {
            try
            {
                var result = _servise.GetAll(token);

                return Ok(ApiResponseDto<IEnumerable<OrderDto>>.Ok(result));

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }
    }
}
