using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.Validation.Interface; 
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.ProductUnit; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {
        private IValidator<CreateProductUnitDto> _createValidator;
        private IValidator<UpdateProductUnitDto> _updateValidator;
        private IProductUnitService _service;
        public ProductUnitController(IProductUnitService servise, IValidator<CreateProductUnitDto> createValidator , IValidator<UpdateProductUnitDto> updateValidator)
        {
            _service = servise;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateProductUnitDto unit)
        {
            try
            {
                var validation = _createValidator.Validation(unit);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<ProductUnitDto>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.Add(unit.ToProductUnit());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<ProductUnitDto>.Ok(result.Data.ToProductUnit()));
                }
                else
                {
                    return Ok(ApiResponse<ProductUnitDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));  
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductUnitDto unit)
        {
            try
            {
                var validation = _updateValidator.Validation(unit);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.Update(unit.ToProductUnit());

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
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("UpdateParameter")]
        public async Task<IActionResult> UpdateParameter([FromQuery] string parameter, [FromQuery] string value, [FromBody] UpdateProductUnitDto unit)
        {
            try
            {
                var validation = _updateValidator.Validation(unit);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.UpdateParameter(parameter, value, unit.ToProductUnit());

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
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var result = await _service.Delete(id); 
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
        [HttpPost("GetByCodePageColumn")]
        public async Task<IActionResult> GetByCodePageColumn([FromQuery] int code, [FromBody] PaginatorDto<ProductUnitDto, int> paginator)
        {
            try
            {
                var result = _service.GetByCodePageColumn(code, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductUnitDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductUnitDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByNamePageColumn")]
        public IActionResult GetByNamePageColumn([FromQuery] string name, [FromBody] PaginatorDto<ProductUnitDto, int> paginator)
        {
            try
            {
                var result = _service.GetByNamePageColumn(name, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductUnitDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductUnitDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetPageColumn")]
        public IActionResult GetPageColumn([FromBody] PaginatorDto<ProductUnitDto, int> paginator)
        {
            try
            {
                var result = _service.GetPageColumn(paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductUnitDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductUnitDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            try
            {
                var result = _service.GetAll();
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<IEnumerable<ProductUnitDto>>.Ok(result.Data.ToProductUnit()));
                }
                else
                {
                    return Ok(ApiResponse<IEnumerable<ProductUnitDto>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
