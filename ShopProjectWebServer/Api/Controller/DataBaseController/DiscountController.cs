using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.Interface.Services;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountServise _servise;
        public DiscountController(IDiscountServise servise)
        {
            _servise = servise;
        }

        [HttpPost("AddDiscount")]
        public IActionResult AddDiscount([FromQuery] string token, CreateDicountDto discount)
        {
            try
            {
                 var result =_servise.Add(token, discount);
                return Ok(ApiResponse<int>.Ok(result, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

    }
}
