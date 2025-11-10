using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.Interface.Services; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCodeUKTZEDController : ControllerBase
    {
        private IProductCodeUKTZEDServise _servise;

        public ProductCodeUKTZEDController(IProductCodeUKTZEDServise servise)
        {
            _servise = servise;
        }

        [HttpPost("AddCodeUKTZED")]
        public IActionResult AddCodeUKTZED(string token, CreateProductUKTZEDDto codeUKTZED)
        {
            try
            {
                var result = _servise.Add(token, codeUKTZED);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("UpdateCodeUKTZED")]
        public IActionResult UpdateCodeUKTZED(string token, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            try
            {
                var result = _servise.Update(token, codeUKTZED);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
=======
                return Ok(ApiResponseDto<bool>.Ok(result));
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

        [HttpPost("UpdateParameterCodeUKTZED")]
        public IActionResult UpdateParameterCodeUKTZED(string token, [FromQuery] string parameter, [FromQuery] string value, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            try
            {
                var result = _servise.UpdateParameter(token, parameter,value,codeUKTZED);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("DeleteCodeUKTZEDE")]
        public IActionResult DeleteCodeUKTZEDE(string token, int id)
        {
            try
            {
                var result = _servise.DeleteCodeUKTZEDE(token, id);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetCodeUKTZEDEByCode")]
        public IActionResult GetCodeUKTZEDEByCode(string token, string code, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = _servise.GetCodeUKTZEDEByCode(token, code, status);
<<<<<<< HEAD
                return Ok(ApiResponse<ProductCodeUKTZEDDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<ProductCodeUKTZEDDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetCodeUKTZEDByNamePageColumn")]
        public IActionResult GetCodeUKTZEDByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = _servise.GetCodeUKTZEDByNamePageColumn(token, name,page,countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto>>.Ok(result));
=======
                return Ok(ApiResponseDto<PaginatorDto<ProductCodeUKTZEDDto>>.Ok(result));
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

        [HttpGet("GetCodeUKTZEDPageColumn")]
        public IActionResult GetCodeUKTZEDPageColumn(string token, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            try
            {
                var result = _servise.GetCodeUKTZEDPageColumn(token, page, countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ProductCodeUKTZEDDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<PaginatorDto<ProductCodeUKTZEDDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetCodeUKTZED")]
        public IActionResult GetCodeUKTZED(string token)
        {
            try
            {
                var result = _servise.GetAll(token);
<<<<<<< HEAD
                return Ok(ApiResponse<IEnumerable<ProductCodeUKTZEDDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<IEnumerable<ProductCodeUKTZEDDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }
    }
}
