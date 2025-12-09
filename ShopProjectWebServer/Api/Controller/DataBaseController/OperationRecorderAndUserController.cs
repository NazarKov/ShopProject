using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Api.Services;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRecorderAndUserController : ControllerBase
    {
        private IOperationRecordersAndUserServise _servise;

        public OperationRecorderAndUserController(IOperationRecordersAndUserServise servise)
        {
            _servise = servise;
        }
        [HttpPost("AddOperationRecordersAndUser")]
        public async Task<IActionResult> AddOperationRecordersAndUser(string token,Guid userId, IEnumerable<BindingUserToOperationRecorderDto> operationsRecorderUserEntity)
        {
            try
            {
                _servise.Add(token,userId ,operationsRecorderUserEntity); 
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт збережено")); 
            }
            catch (Exception ex)
            { 
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetOperationRecordersAndUser")]
        public async Task<IActionResult> GetOperationRecordersAndUser(string token)
        {
            try
            { 
                var result = _servise.GetOperationRecorderForUser(token);

                return Ok(ApiResponse<OperationRecorderUserDto>.Ok(result)); 

            }
            catch (Exception ex)
            { 
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

    }
}
