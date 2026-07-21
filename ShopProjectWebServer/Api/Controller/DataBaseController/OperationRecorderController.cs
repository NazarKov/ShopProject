using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Models.Domain.OperationRecorder;
using ShopProjectWebServer.Models.Domain.TaxObject;
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

        //[HttpGet("GetOperationRecorders")]
        //public async Task<IActionResult> GetOperationRecorders(string token)
        //{
        //    try
        //    {
        //        var result = _servise.GetOperationRecorders(token); 
        //        return Ok(ApiResponse<IEnumerable<OperationRecorderDto>>.Ok(result));  
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ApiResponse<string>.Fail(ex.Message));
        //    }
        //}

         

        
        [HttpPost("AddBindingOperationRecorder")]
        public async Task<IActionResult> AddBindingOperationRecorder(string token, string idoperationrecoreder , string idobjectowner)
        {
            try
            {
                //var result = _servise.AddBindingOperationRecorder(token, idoperationrecoreder, idobjectowner); 
                //return Ok(ApiResponse<bool>.Ok(result));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }
    }
}
