using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Api.Services;
using ShopProjectWebServer.DataBase;
using System.Text.Json;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingShiftController : ControllerBase
    {
        private IWorkingShiftServise _servise;

        public WorkingShiftController(IWorkingShiftServise servise)
        {
            _servise = servise;
        }

        [HttpPost("AddWorkingShift")]
        public async Task<IActionResult> AddWorkingShift([FromQuery] string token, CreateWorkingShiftDto item)
        {
            try
            {
                _servise.Add(token, item);
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPost("UpdateWorkingShift")]
        public async Task<IActionResult> UpdateWorkingShift([FromQuery] string token, UpdateWorkingShiftDto item)
        {
            try
            {
                _servise.Update(token, item);

                return Ok(ApiResponse<bool>.Ok(true, "Обєкт оновлено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetWorkingShift")]
        public async Task<IActionResult> GetWorkingShift([FromQuery] string token, [FromQuery] string id)
        {
            try
            {
                var result = _servise.GetById(token, id);

                return Ok(ApiResponse<WorkingShiftDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
