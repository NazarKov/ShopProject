using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;
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
    public class OperationRecorderAndUserController : ControllerBase
    {
        private IOperationRecordersAndUserServise _servise;

        public OperationRecorderAndUserController(IOperationRecordersAndUserServise servise)
        {
            _servise = servise;
        }
        [HttpPost("AddOperationRecordersAndUser")]
        public async Task<IActionResult> AddOperationRecordersAndUser(string token,Guid userId, IEnumerable<BindingUserToOperationRecorderDto> operationsRecorderUserEntity)
        {
            try
            {
                _servise.Add(token,userId ,operationsRecorderUserEntity);

<<<<<<< HEAD
                return Ok(ApiResponse<bool>.Ok(true, "Обєкт збережено"));
=======
                return Ok(ApiResponseDto<bool>.Ok(true, "Обєкт збережено"));
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

        [HttpGet("GetOperationRecordersAndUser")]
        public async Task<IActionResult> GetOperationRecordersAndUser(string token)
        {
            try
            {
<<<<<<< HEAD
                var result = _servise.GetOperationRecorderForUser(token);

                return Ok(ApiResponse<OperationRecorderUserDto>.Ok(result));
=======
                var result = _servise.GetAll(token);

                return Ok(ApiResponseDto<IEnumerable<OperationRecorderUserDto>>.Ok(result));
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
