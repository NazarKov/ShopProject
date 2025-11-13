using Azure;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Operation;
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
    public class OperationController : ControllerBase
    {
        private IOperationServise _servise;

        public OperationController(IOperationServise servise)
        {
            _servise = servise;
        } 
        [HttpPost("AddOperation")]
        public  IActionResult AddOperation([FromQuery] string token, CreateOperationDto operation)
        {
            try
            {
                 var id = _servise.Add(token, operation); 
                return Ok(ApiResponse<int>.Ok(id, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetOperations")]
        public async Task<IActionResult> GetOperations(string token)
        {
            try
            {
                var result = _servise.GetAll(token); 
                return Ok(ApiResponse<IEnumerable<OperationDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetLastNumberOperation")]
        public async Task<IActionResult> GetLastNumberOperation(string token,int shiftId)
        {
            try
            {
                var result = _servise.GetLast(token,shiftId); 
                return Ok(ApiResponse<string>.Ok(result.NumberPayment)); 

            }
            catch (Exception ex)
            { 
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }
    }
}
