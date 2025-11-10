using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ObjectOwner;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Api.Services;
using ShopProjectWebServer.DataBase;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectOwnerController : ControllerBase
    {
        private readonly IObjectOwnerServise _servise;
        public ObjectOwnerController(IObjectOwnerServise objectOwnerServise)
        {
            _servise = objectOwnerServise;
        }

        [HttpPost("DeleteObjectsOwner")]
        public async Task<IActionResult> DeleteObjectsOwner([FromQuery] string token, string id)
        {
            try
            {
                var result = _servise.Delete(token, id);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(result, "Обєкт Власності видалено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(result, "Обєкт Власності видалено"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetObjectsOwnersByNamePageColumn")]
        public IActionResult GetObjectsOwnersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusObjectOwner status)
        {
            try
            {
                var result = _servise.GetPageColumnByName(token, name, page, countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ObjectOwnerListDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<PaginatorDto<ObjectOwnerListDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetObjectsOwnersPageColumn")]
        public IActionResult GetObjectsOwnersPageColumn(string token, int page, int countColumn, TypeStatusObjectOwner status)
        {
            try
            { 
                var result = _servise.GetPageColumn(token, page, countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<ObjectOwnerListDto>>.Ok(result));
=======
                return Ok(ApiResponseDto<PaginatorDto<ObjectOwnerListDto>>.Ok(result));
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


        [HttpGet("GetObjectsOwners")]
        public async Task<IActionResult> GetObjectsOwners(string token)
        {
            try
            { 
                var result = _servise.GetAll(token); 
<<<<<<< HEAD
                return Ok(ApiResponse<List<ObjectOwnerListDto>>.Ok(result.ToList())); 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<List<ObjectOwnerListDto>>.Ok(result.ToList())); 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("AddObjectOwner")]
        public async Task<IActionResult> AddObjectOwner(string token, CreateObjectOwnerDto objectOwner)
        {
            try
            {
                _servise.Add(token, objectOwner);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(true));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(true));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("AddObjectsOwners")]
        public async Task<IActionResult> AddObjectsOwners(string token, IEnumerable<CreateObjectOwnerDto> objectOwner)
        {
            try
            {
                _servise.AddRange(token, objectOwner);
<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(true)); 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return Ok(ApiResponseDto<bool>.Ok(true)); 
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

    }
}
