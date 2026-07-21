using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Mappings; 
using ShopProjectWebServer.Services.Modules.Domain.WorkingShift.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.WorkingShift; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingShiftController : ControllerBase
    {
        private IWorkingShiftService _service;

        public WorkingShiftController(IWorkingShiftService service)
        {
            _service = service;
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateWorkingShiftDto item )
        {
            try
            {
                var result = await _service.Add(item.ToWorkingShift());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<WorkingShiftDto>.Ok(result.Data.ToWorkingShiftDto(), "Обєкт створено"));
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
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateWorkingShiftDto item)
        {
            try
            {
                var result = await _service.Update(item.ToWorkingShift());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<WorkingShiftDto>.Ok(result.Data.ToWorkingShiftDto(), "Обєкт оновлено"));
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
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _service.GetById(id);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<WorkingShiftDto>.Ok(result.Data.ToWorkingShiftDto()));
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
        [HttpGet("GetResourseByNumberRRO")]
        public async Task<IActionResult> GetResourseByNumberRRO([FromQuery] string fiscalNumberRRo)
        {
            try
            {
                var result = await _service.GetResourseByWorkingShift(fiscalNumberRRo);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<WorkingShiftResourseDto>.Ok(result.Data.ToWorkingShiftDto()));
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
    }
}
