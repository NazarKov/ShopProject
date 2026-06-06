using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.Validation.Interface; 
using ShopProjectWebServer.Services.Modules.Domain.Product.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Product; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IValidator<CreateProductDto> _createValidator;
        private IValidator<UpdateProductDto> _updateValidator;
        private IProductService _service;
        public ProductController (IProductService servise,IValidator<CreateProductDto> createValidator , IValidator<UpdateProductDto> updateValidator)
        {
            _service = servise;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateProductDto product)
        {
            try
            {
                var validation = _createValidator.Validation(product);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<ProductDto>.Fail(validation.Errors,ErrorType.Validation,ErrorSource.Client));
                }

                var result = await _service.AddAsync(product.ToProduct());
                 
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<ProductDto>.Ok(result.Data.ToProductDto()));
                }
                else
                {
                    return Ok(ApiResponse<ProductDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            } 
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message,ErrorType.Server));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("AddRange")]
        public async Task<IActionResult> AddRange(IEnumerable<CreateProductDto> products)
        {
            try
            {
                var result = await _service.AddRangeAsync(products.ToProduct());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<bool>.Ok(true));
                }
                else
                {
                    return Ok(ApiResponse<string>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                } 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateProductDto product)
        {
            try
            {
                var validation = _updateValidator.Validation(product);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.UpdateAsync(product.ToProduct());

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
        public async Task<IActionResult> UpdateParameter([FromQuery] string parameter, [FromQuery] string value, UpdateProductDto product)
        {
            try
            {
                var validation = _updateValidator.Validation(product);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.UpdateParameterAsync(parameter,value, product.ToProduct());

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
        [HttpPost("UpdateRange")]
        public async Task<IActionResult> UpdateRange(IEnumerable<UpdateProductDto> product)
        {
            try
            {  
                var result = await _service.UpdateRangeAsync(product.ToProduct());

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
        public async Task<IActionResult> GetPageColumn(PaginatorDto<ProductDto,int> paginator)
        {
            try
            { 
                var result = _service.GetPageColumn(paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                } 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByNamePageColumn")]
        public IActionResult GetByNamePageColumn([FromQuery]string name, [FromBody]PaginatorDto<ProductDto, int> paginator)
        {
            try
            {
                var result = _service.GetByNamePageColumn(name,paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
          
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpGet("GetInfoProducts")]
        public IActionResult GetInfoProducts()
        {
            try
            {
                var result = _service.GetInfoProducts();
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<ProductInfoDto>.Ok(result.Data.ToProductInfo()));
                }
                else
                {
                    return Ok(ApiResponse<ProductInfoDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString()))); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByBarCodePageColumn")]
        public IActionResult GetByBarCodePageColumn([FromQuery] string barCode, [FromBody] PaginatorDto<ProductDto, int> paginator)
        {
            try
            {
                var result = _service.GetByBarCode(barCode, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<ProductDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<ProductDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                } 
            } 
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("GetByBarCode")]
        public IActionResult GetByBarCode([FromQuery] string barCode , int status)
        {
            try
            {
                var result = _service.GetByBarCode(barCode);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<ProductDto>.Ok(result.Data.ToProductDto()));
                }
                else
                {
                    return Ok(ApiResponse<ProductDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }    
    }
}
