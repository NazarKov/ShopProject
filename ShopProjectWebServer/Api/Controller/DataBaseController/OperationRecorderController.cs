using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.OperationRecorder; 
using ShopProjectWebServer.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRecorderController : ControllerBase
    {

        private IOperationRecorderService _service;
        
        public OperationRecorderController(IOperationRecorderService service)
        {
            _service = service;
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateOperationRecorderDto operationsRecorder)
        {
            try
            {

                var result = await _service.Add(operationsRecorder.ToOperationRecorder());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<OperationRecorderDto>.Ok(result.Data.ToOpeartionRecorderDto()));
                }
                else
                {
                    return Ok(ApiResponse<OperationRecorderDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(IEnumerable<CreateOperationRecorderDto> operationRecorders)
        {
            try
            {

                var result = await _service.AddRange(operationRecorders.ToOperationRecordersEntity());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<IEnumerable<OperationRecorderDto>>.Ok(result.Data.ToOperationRecorderDto()));
                }
                else
                {
                    return Ok(ApiResponse<IEnumerable<OperationRecorderDto>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateOperationRecorderDto operationsRecorder)
        {
            try
            {

                var result = await _service.Update(operationsRecorder.ToOperationRecorder());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<OperationRecorderDto>.Ok(result.Data.ToOpeartionRecorderDto()));
                }
                else
                {
                    return Ok(ApiResponse<OperationRecorderDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("UpdateParameter")]
        public async Task<IActionResult> UpdateParameter([FromQuery] string parameter, [FromQuery] string value, [FromBody] string id)
        {
            try
            {
                //var validation = _updateValidator.Validation(userDto);
                //if (!validation.isValid)
                //{
                //    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                //}

                var result = await _service.UpdateParameter(id, parameter, value);

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<bool>.Ok(true));
                }
                else
                {
                    return Ok(ApiResponse<bool>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }


        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetPageColumn")]
        public IActionResult GetPageColumn([FromBody] PaginatorDto<OperationRecorderDto, int> paginator)
        {
            try
            {
                var result = _service.GetPageColumn(paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<OperationRecorderDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<OperationRecorderDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByNamePageColumn")]
        public IActionResult GetByNamePageColumn([FromQuery] string name, [FromBody] PaginatorDto<OperationRecorderDto, int> paginator)
        {
            try
            {
                var result = _service.GetByNamePageColumn(name, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<OperationRecorderDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<OperationRecorderDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        } 
    }
}
