using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.TaxObject;
using ShopProjectWebServer.Api.DtoModels.TaxObjectUser;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Services.Modules.Domain.TaxObject.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObject;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObjectUser;
using ShopProjectWebServer.Services.Modules.Mapping.User;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxObjectController : ControllerBase
    {
        private readonly ITaxObjectService _service;
        public TaxObjectController(ITaxObjectService objectOwnerServise)
        {
            _service = objectOwnerServise;
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetPageColumn")]
        public IActionResult GetPageColumn([FromBody] PaginatorDto<TaxObjectDto, int> paginator)
        {
            try
            {
                var result = _service.GetPageColumn(paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<TaxObjectDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<TaxObjectDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByNamePageColumn")]
        public IActionResult GetByNamePageColumn([FromQuery] string name, [FromBody] PaginatorDto<TaxObjectDto, int> paginator)
        {
            try
            {
                var result = _service.GetByNamePageColumn(name, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<TaxObjectDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<TaxObjectDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateTaxObjectDto taxObject)
        {
            try
            {
                 
                var result = await _service.Add(taxObject.ToTaxObject());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<TaxObjectDto>.Ok(result.Data.ToTaxObjectDto()));
                }
                else
                {
                    return Ok(ApiResponse<TaxObjectDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(IEnumerable<CreateTaxObjectDto> taxObjects)
        {
            try
            {

                var result = await _service.AddRange(taxObjects.ToTaxObject());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<IEnumerable<TaxObjectDto?>>.Ok(result.Data.ToTaxObjectDto()));
                }
                else
                {
                    return Ok(ApiResponse<IEnumerable<TaxObjectDto?>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("AddBindingOperationRecorder")]
        public async Task<IActionResult> AddBindingOperationRecorder(string idTaxObject, [FromBody] IEnumerable<OperationRecorderDto> operationRecorderDto)
        {
            try
            {

                var result = await _service.AddBindingOpearationRecorderToTaxObject(Guid.Parse(idTaxObject), operationRecorderDto.ToOpeartionRecorder());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<bool>.Ok(result.Data));
                }
                else
                {
                    return Ok(ApiResponse<bool>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("AddBindingUser")]
        public async Task<IActionResult> AddBindingUser(string idTaxObject, [FromBody] IEnumerable<UserDto> usersDto)
        {
            try
            {

                var result = await _service.AddBindingUserToTaxObject(Guid.Parse(idTaxObject), usersDto.ToUser());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<bool>.Ok(result.Data));
                }
                else
                {
                    return Ok(ApiResponse<bool>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpGet("GetTaxObjectsAssignedUser")]
        public async Task<IActionResult> GetTaxObjectsAssignedUser(string iduser)
        {
            try
            {

                var result = _service.GetTaxObjectsAssignedUser(Guid.Parse(iduser));

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<IEnumerable<TaxObjectUserDto>>.Ok(result.Data.ToTaxObjectUserDto()));
                }
                else
                {
                    return Ok(ApiResponse<bool>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }

        [HttpPost("DeleteObjectsOwner")]
        public async Task<IActionResult> DeleteObjectsOwner([FromQuery] string token, string id)
        {
            try
            {
                // var result = _servise.Delete(token, id); 
                //  return Ok(ApiResponse<bool>.Ok(result, "Обєкт Власності видалено"));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }
 
 


        [HttpGet("GetObjectsOwners")]
        public async Task<IActionResult> GetObjectsOwners(string token)
        {
            try
            {
                //var result = _servise.GetAll(token);  
                //return Ok(ApiResponse<List<ObjectOwnerListDto>>.Ok(result.ToList())); 
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        } 
    }
}
