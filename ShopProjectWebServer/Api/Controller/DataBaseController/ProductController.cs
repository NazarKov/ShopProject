using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.Interface.Services; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductServise _servise;
        public ProductController (IProductServise servise)
        {
            _servise = servise;
        }

        [HttpGet("GetInfoProducts")]
        public IActionResult GetInfoProducts(string token)
        {
            try
            {
                var result = _servise.GetInfoProducts(token);
<<<<<<< HEAD
                return Ok(ApiResponse<ProductInfoDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<ProductInfoDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        } 

        [HttpGet("GetProductsByBarCode")]
        public IActionResult GetProductsByBarCode(string token, string barCode)
        {
            try
            {
                var result = _servise.GetProductsByBarCode(token,barCode);
<<<<<<< HEAD
                return Ok(ApiResponse<ProductDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<ProductDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetProductByNamePageColumn")]
        public IActionResult GetProductByNamePageColumn(string token,string name, int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                var result = _servise.GetProductByNamePageColumn(token, name,page,countColumn,status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<PaginatorDto<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetProductsPageColumn")]
        public IActionResult GetProductsPageColumn(string token, int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                var result = _servise.GetProductsPageColumn(token, page, countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<PaginatorDto<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(string token)
        {
            try
            {
                var result = _servise.GetProducts(token);
<<<<<<< HEAD
                return Ok(ApiResponse<IEnumerable<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<IEnumerable<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(string token, CreateProductDto product)
        {
            try
            {
                var result = _servise.Add(token,product);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("AddProductRange")]
        public async Task<IActionResult> AddProductRange(string token, IEnumerable<CreateProductDto> product)
        {
            try
            {
                var result = _servise.AddProductRange(token, product);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string token, UpdateProductDto product)
        {
            try
            {
                var result = _servise.UpdateProduct(token, product);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("UpdateProductRange")]
        public async Task<IActionResult> UpdateProductRange(string token, IEnumerable<UpdateProductDto> product)
        {
            try
            {
                var result = _servise.UpdateProductRange(token, product);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("UpdateParameterProduct")]
        public async Task<IActionResult> UpdateParameterProduct(string token,[FromQuery]string parameter , [FromQuery]string value ,UpdateProductDto product)
        {
            try
            {
                var result = _servise.UpdateParameterProduct(token, parameter,value,product);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
=======
                return BadRequest(ApiResponseDto<bool>.Ok(result));
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
