using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Helpers;
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
            return Ok(new Message() { MessageBody = JsonSerializer.Serialize<string>(DateTime.Now.ToString()), Type = TypeMessage.Message }.ToString());
        }
    }
}
