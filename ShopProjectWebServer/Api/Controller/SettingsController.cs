using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ControlWebServer;
using ShopProjectWebServer.Services.Infrastructure.ContolWebServer.Interface;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private IControlWebServerService _controlWebServerService;

        public SettingsController(IControlWebServerService controlWebServerService)
        {
            _controlWebServerService = controlWebServerService;
        }

        [HttpGet("Ping")]
        public IActionResult GetSettings()
        { 
            return Ok(ApiResponse<string>.Ok(DateTime.Now.ToString()));  
        }

        [HttpGet("Health")]
        public async Task<IActionResult> GetHealth()
        {
            var result = await _controlWebServerService.CheckDataBaseHealth();

            return Ok(ApiResponse<ControlWebServerDto>.Ok(new ControlWebServerDto()
            {
                IsEnabled = true,
                IsEnableDataBase = result,
            }));
        }
    }
}
