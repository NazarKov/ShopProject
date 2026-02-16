using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.DataBase.DataBaseException;

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
                return Ok(ApiResponse<ProductInfoDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        } 

        [HttpGet("GetProductsByBarCode")]
        public IActionResult GetProductsByBarCode(string token, string barCode, TypeStatusProduct statusProduct = TypeStatusProduct.Unknown)
        {
            try
            {
                var result = _servise.GetProductsByBarCode(token,barCode , statusProduct); 
                return Ok(ApiResponse<ProductDto>.Ok(result));
            }
            catch(InvalidOperationException invalidOperationException)
            {
                 if(invalidOperationException.Message == "Sequence contains no elements"){
                     return Ok(ApiResponse<ProductDto>.Ok(new ProductDto()));
                 }
                 else
                 {
                     return BadRequest(ApiResponse<string>.Fail(invalidOperationException.Message));
                 }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetProductByNamePageColumn")]
        public IActionResult GetProductByNamePageColumn(string token,string name, int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                var result = _servise.GetProductByNamePageColumn(token, name,page,countColumn,status); 
                return Ok(ApiResponse<PaginatorDto<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetProductsPageColumn")]
        public IActionResult GetProductsPageColumn(string token, int page, int countColumn, TypeStatusProduct status)
        {
            try
            {
                var result = _servise.GetProductsPageColumn(token, page, countColumn, status); 
                return Ok(ApiResponse<PaginatorDto<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(string token)
        {
            try
            {
                var result = _servise.GetProducts(token); 
                return Ok(ApiResponse<IEnumerable<ProductDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(string token, CreateProductDto product)
        {
            try
            {
                var result = _servise.Add(token,product); 
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (ExceptionObjectExists exeption)
            {
                return Ok(ApiResponse<bool>.Fail(exeption.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpPost("AddProductRange")]
        public async Task<IActionResult> AddProductRange(string token, IEnumerable<CreateProductDto> product)
        {
            try
            {
                var result = await _servise.AddProductRangeAsync(token, product); 
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string token, UpdateProductDto product)
        {
            try
            {
                var result = _servise.UpdateProduct(token, product); 
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpPost("UpdateProductRange")]
        public async Task<IActionResult> UpdateProductRange(string token, IEnumerable<UpdateProductDto> product)
        {
            try
            {
                var result = _servise.UpdateProductRange(token, product); 
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }

        [HttpPost("UpdateParameterProduct")]
        public async Task<IActionResult> UpdateParameterProduct(string token,[FromQuery]string parameter , [FromQuery]string value ,UpdateProductDto product)
        {
            try
            {
                var result = _servise.UpdateParameterProduct(token, parameter,value,product); 
                return Ok(ApiResponse<bool>.Ok(result)); 

            }
            catch (Exception ex)
            { 
                return BadRequest(ApiResponse<string>.Fail(ex.Message)); 
            }
        }
    }
}
