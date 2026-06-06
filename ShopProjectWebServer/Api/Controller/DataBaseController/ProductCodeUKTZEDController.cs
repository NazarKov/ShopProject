using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.Validation.Interface; 
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.ProductCodeUKTZED; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCodeUKTZEDController : ControllerBase
    {
        private IValidator<CreateProductUKTZEDDto> _createValidator;
        private IValidator<UpdateProductCodeUKTZEDDto> _updateValidator;
        private IProductCodeUKTZEDService _service;

        public ProductCodeUKTZEDController(IProductCodeUKTZEDService service , IValidator<CreateProductUKTZEDDto> createValidator ,IValidator<UpdateProductCodeUKTZEDDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateProductUKTZEDDto item)
        {
            try
            {
                var validation = _createValidator.Validation(item);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<ProductCodeUKTZEDDto>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.AddAsync(item.ToProductCodeUKTZED());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<ProductCodeUKTZEDDto>.Ok(result.Data.ToProductCodeUKTZEDDto()));
                }
                else
                {
                    return Ok(ApiResponse<ProductCodeUKTZEDDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            } 
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCodeUKTZEDDto item)
        {
            try
            {
                var validation = _updateValidator.Validation(item);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.UpdateAsync(item.ToProductCodeUKTZED());

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
        public async Task<IActionResult> UpdateParameter([FromQuery] string parameter, [FromQuery] string value,[FromBody] UpdateProductCodeUKTZEDDto item)
        {
            try
            {
                var validation = _updateValidator.Validation(item);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.UpdateParameterAsync(parameter, value, item.ToProductCodeUKTZED());

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
        public IActionResult Delete([FromBody]int id)
        {
            try
            { 
                var result =  _service.Delete(id);

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
        [HttpPost("GetByCode")]
        public IActionResult GetByCode([FromQuery] string code,[FromBody] PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            try
            {
                var result = _service.GetByCode(code, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByNamePageColumn")]
        public IActionResult GetByNamePageColumn([FromQuery] string name,[FromBody] PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            try
            {
                var result = _service.GetByNamePageColumn(name,paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetPageColumn")]
        public IActionResult GetPageColumn([FromBody] PaginatorDto<ProductCodeUKTZEDDto, int> paginator)
        {
            try
            {
                var result = _service.GetPageColumn(paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
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
                    return Ok(ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>.Ok(result.Data.ToProductCodeUKTZEDDto()));
                }
                else
                {
                    return Ok(ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
