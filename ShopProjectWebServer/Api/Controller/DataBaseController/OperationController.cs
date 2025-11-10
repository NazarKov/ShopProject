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
        public  IActionResult AddOperationRecorder([FromQuery] string token, CreateOperationDto operation)
        {
            try
            {
                _servise.Add(token, operation);

<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(true, "Обєкт збережено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetOperations")]
        public async Task<IActionResult> GetOperations(string token)
        {
            try
            {
                var result = _servise.GetAll(token);

<<<<<<< HEAD
                return Ok(ApiResponse<IEnumerable<OperationDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<IEnumerable<OperationDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetLastOperation")]
        public async Task<IActionResult> GetLastOperation(string token,int shiftId)
        {
            try
            {
                var result = _servise.GetLast(token,shiftId);

<<<<<<< HEAD
                return Ok(ApiResponse<OperationDto>.Ok(result));
=======
                return Ok(ApiResponseDto<OperationDto>.Ok(result));
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
