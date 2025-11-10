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

<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(true, "Обєкт створено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("UpdateWorkingShift")]
        public async Task<IActionResult> UpdateWorkingShift([FromQuery] string token, UpdateWorkingShiftDto item)
        {
            try
            {
                _servise.Update(token, item);

<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт оновлено"));
=======
                return Ok(ApiResponseDto<bool>.Ok(true, "Обєкт оновлено"));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }
    }
}
