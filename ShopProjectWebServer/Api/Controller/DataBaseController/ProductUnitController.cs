using Microsoft.AspNetCore.Mvc; 
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.Interface.Services; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {
        private IProductUnitServise _servise;
        public ProductUnitController(IProductUnitServise servise)
        {
            _servise = servise;
        }

        [HttpPost("AddUnit")]
        public IActionResult AddUnit(string token, CreateProductUnitDto unit)
        {
            try
            {
                var result = _servise.AddUnit(token,unit);
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

        [HttpPost("UpdateUnit")]
        public IActionResult UpdateUnit(string token, UpdateProductUnitDto unit)
        {
            try
            {
                var result = _servise.UpdateUnit(token, unit);
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

        [HttpPost("UpdateParameterUnit")]
        public IActionResult UpdateParameterUnit(string token, [FromQuery] string parameter, [FromQuery] string value, UpdateProductUnitDto unit)
        {
            try
            {
                var result = _servise.UpdateParameterUnit(token,parameter,value, unit);
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

        [HttpPost("DeleteUnit")]
        public IActionResult DeleteUnit(string token, int id)
        {
            try
            {
                var result = _servise.DeleteUnit(token, id);
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

        [HttpGet("GetUnitByCode")]
        public IActionResult GetUnitByCode(string token, string code, TypeStatusUnit status)
        {
            try
            {
                var result = _servise.GetUnitByCode(token, code,status);
<<<<<<< HEAD
                return Ok(ApiResponse<ProductUnitDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<ProductUnitDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetUnitsByNamePageColumn")]
        public IActionResult GetUnitsByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUnit status)
        {
            try
            {
                var result = _servise.GetUnitsByNamePageColumn(token, name,page,countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ProductUnitDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<PaginatorDto<ProductUnitDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetUnitsPageColumn")]
        public IActionResult GetUnitsPageColumn(string token, int page, int countColumn, TypeStatusUnit status)
        {
            try
            {
                var result = _servise.GetUnitsPageColumn(token, page, countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ProductUnitDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<PaginatorDto<ProductUnitDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetUnits")]
        public IActionResult GetUnits(string token) 
        {
            try
            {
                var result = _servise.GetUnits(token);
<<<<<<< HEAD
                return Ok(ApiResponse<IEnumerable<ProductUnitDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<IEnumerable<ProductUnitDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }
    }
}
