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
            return Ok(ApiResponse<string>.Ok(DateTime.Now.ToString())); 
        }
    }
}
