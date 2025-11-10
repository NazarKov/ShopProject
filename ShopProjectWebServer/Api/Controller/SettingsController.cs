using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using System.Text.Json;

namespace ShopProjectWebServer.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpGet("Ping")]
        public IActionResult GetSettings()
        {
<<<<<<< HEAD
            return Ok(ApiResponse<string>.Ok(DateTime.Now.ToString())); 
=======
            return Ok(ApiResponseDto<string>.Ok(DateTime.Now.ToString())); 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        }
    }
}
