using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Ping")]
        public IActionResult GetSettings()
        {
            return Ok("DateTime:" + DateTime.Now.ToString());
        }
    }
}
