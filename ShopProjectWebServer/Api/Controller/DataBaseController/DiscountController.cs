using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Discount; 
using ShopProjectWebServer.Services.Modules.Domain.Discount.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Discount;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountService _servise;
        public DiscountController(IDiscountService servise)
        {
            _servise = servise;
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public IActionResult Add(CreateDicountDto discount)
        {
            try
            {
                var result =_servise.Add(discount.ToDiscount());
                return Ok(ApiResponse<int>.Ok(result.Data, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

    }
}
