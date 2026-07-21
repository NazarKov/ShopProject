using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.DtoModels.Order; 
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Services.Modules.Domain.Order.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Order;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _servise;

        public OrderController(IOrderService servise)
        {
            _servise = servise;
        }

        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(IEnumerable<CreateOrderDto> orders)
        {
            try
            {
                _servise.AddRange(orders.ToListOrder()); 
                return Ok(ApiResponse<bool>.Ok(true, "Обєкти створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(string token)
        {
            try
            {
                var result = _servise.GetAll(token); 
                return Ok(ApiResponse<IEnumerable<OrderDto>>.Ok(result)); 
            }
            catch (Exception ex)
            { 
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }
    }
}
