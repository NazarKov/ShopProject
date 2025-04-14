using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.DataBase;
using System.Text.Json;

namespace ShopProjectWebServer.Api.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeUKTZEDController : ControllerBase
    { 

        [HttpGet("GetCodeUKTZED")]
        public async Task<IActionResult> GetCodeUKTZED(string token)
        {
            try
            {
                var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

                if (tokens != null)
                {
                    var userToken = tokens.Where(t => t.Token == token).FirstOrDefault();
                    if (userToken != null)
                    {
                        var codeUKTZED = DataBaseMainController.DataBaseAccess.CodeUKTZEDTable.GetAll();

                        return Ok(new Message()
                        {
                            MessageBody = JsonSerializer.Serialize<IEnumerable<CodeUKTZEDEntity>>(codeUKTZED),
                            Type = TypeMessage.Message
                        }.ToString());
                    }
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
