using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Domain.Operation.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Operation;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private IOperationService _service;

        public OperationController(IOperationService service)
        {
            _service = service;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateOperationDto operation)
        {
            try
            {
                var result = await _service.Add(operation.ToOperation());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<OperationDto>.Ok(result.Data.ToOperationDto(), "Обєкт створено"));
                }
                else
                {
                    return Ok(ApiResponse<string>.Fail(result.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpGet("GetOperationsInfo")]
        public async Task<IActionResult> GetOperationsInfo(int shiftId)
        {
            try
            {
                var result = _service.GetInfo(shiftId);
                return Ok(ApiResponse<OperaiontStatisticsDto>.Ok(result)); 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [HttpGet("GetOperationsІnformation")]
        public async Task<IActionResult> GetOperationsІnformation(string token, int shiftId)
        {
            try
            {
                //var result = _service.GetInformation(token, shiftId);
                //return Ok(ApiResponse<OperationІnformationDto>.Ok(result));
                return Ok();
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
                //var result = _service.GetAll(token); 
                //return Ok(ApiResponse<IEnumerable<OperationDto>>.Ok(result));
                return Ok();
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
                //var result = _service.GetLast(token,shiftId); 
                //return Ok(ApiResponse<string>.Ok(result.NumberPayment)); 
                return Ok();

            }
            catch (Exception ex)
            { 
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }
    }
}
