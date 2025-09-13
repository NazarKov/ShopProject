using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Helpers;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using System.Text.Json; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingShiftController : ControllerBase
    {
        [HttpPost("AddWorkingShift")]
        public async Task<IActionResult> AddWorkingShift([FromQuery] string token, CreateWorkingShiftDto item)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {  
                    int id = DataBaseMainController.DataBaseAccess.WorkingShiftTable.Add(item.ToWorkingShiftEntity());

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(id),
                        Type = TypeMessage.Message
                    }.ToString());
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

        [HttpPost("UpdateWorkingShift")]
        public async Task<IActionResult> UpdateWorkingShift([FromQuery] string token, UpdateWorkingShiftDto item)
        {
            try
            {
                if (AuthorizationApi.LoginToken(token))
                {


                    DataBaseMainController.DataBaseAccess.WorkingShiftTable.Update(item.ToWorkingShiftEntity());

                    return Ok(new Message()
                    {
                        MessageBody = JsonSerializer.Serialize(true),
                        Type = TypeMessage.Message
                    }.ToString());
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
