using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.Interface.Services; 
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRecorderController : ControllerBase
    {

        private IOperationRecorderServise _servise;
        
        public OperationRecorderController(IOperationRecorderServise servise)
        {
            _servise = servise;
        }

        [HttpGet(nameof(GetOperationRecordersByNumberAndUser))]
        public IActionResult GetOperationRecordersByNumberAndUser(string token, string number, Guid userId)
        {
            try
            { 
               var result = _servise.GetOperationRecordersByNumberAndUser(token, number, userId);
               return Ok(ApiResponseDto<IEnumerable<OperationRecorderDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetOperationRecordersByNameAndUser")]
        public IActionResult GetOperationRecordersByNameAndUser(string token, string name , Guid userId)
        {
            try
            {
                var result = _servise.GetOperationRecordersByNameAndUser(token, name, userId);
                return Ok(ApiResponseDto<IEnumerable<OperationRecorderDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpPost("DeleteOperationRecorder")]
        public async Task<IActionResult> DeleteOperationRecorder([FromQuery] string token, string id)
        {
            try
            {
                var result = _servise.Delete(token, id);
                return Ok(ApiResponseDto<bool>.Ok(result, "Обєкт Власності видалено"));

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetOperationRecordersByNamePageColumn")]
        public IActionResult GetOperationRecordersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var result = _servise.GetOperationRecordersByNamePageColumn(token, name, page,countColumn , status);
                return Ok(ApiResponseDto<PaginatorDto<OperationRecorderDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetOperationRecordersPageColumn")]
        public IActionResult GetOperationRecordersPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            try
            {
                var result = _servise.GetOperationRecordersPageColumn(token, page, countColumn, status);
                return Ok(ApiResponseDto<PaginatorDto<OperationRecorderDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetOperationRecorders")]
        public async Task<IActionResult> GetOperationRecorders(string token)
        {
            try
            {
                var result = _servise.GetOperationRecorders(token);
                return Ok(ApiResponseDto<IEnumerable<OperationRecorderDto>>.Ok(result));

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpPost("AddOperationRecorder")]
        public async Task<IActionResult> AddOperationRecorder(string token, CreateOperationRecorderDto operationsRecorder)
        {
            try
            {
                var result = _servise.Add(token , operationsRecorder);
                return Ok(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpPost("AddOperationRecorders")]
        public async Task<IActionResult> AddOperationRecorders(string token, IEnumerable<CreateOperationRecorderDto> operationRecorders)
        {
            try
            {
                var result = _servise.AddRange(token, operationRecorders);
                return Ok(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }
        [HttpPost("AddBindingOperationRecorder")]
        public async Task<IActionResult> AddBindingOperationRecorder(string token, string idoperationrecoreder , string idobjectowner)
        {
            try
            {
                var result = _servise.AddBindingOperationRecorder(token, idoperationrecoreder, idobjectowner);
                return Ok(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }
    }
}
