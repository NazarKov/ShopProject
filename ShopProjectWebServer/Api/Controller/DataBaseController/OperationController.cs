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
                var result = await _service.GetInfo(shiftId);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<OperaiontStatisticsDto>.Ok(result.Data));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.ErrorMessage));
                } 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpGet("GetLastNumberOperation")]
        public async Task<IActionResult> GetLastNumberOperation(int shiftId)
        {
            try
            {
                var result = await _service.GetInformation(shiftId);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<OperationІnformationDto>.Ok(result.Data));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.ErrorMessage));
                } 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        } 
    }
}
