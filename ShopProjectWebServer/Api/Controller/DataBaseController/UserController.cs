using Microsoft.AspNetCore.Mvc; 
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.Token;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Api.Interface.Services; 

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServise _servise;
        public UserController(IUserServise servise)
        {
            _servise = servise;
        }


        [HttpGet("GetUserByNamePageColumn")]
        public IActionResult GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var result = _servise.GetUserByNamePageColumn(token,name,page,countColumn,status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<PaginatorDto<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetUsersPageColumn")]
        public IActionResult GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var result = _servise.GetUsersPageColumn(token, page, countColumn, status);
<<<<<<< HEAD
                return Ok(ApiResponse<PaginatorDto<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<PaginatorDto<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string token, string id)  
        {
            try
            {
                var result = _servise.DeleteUser(token,id);
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


        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] string token, UpdateUserDto user)
        {
            try
            {
                var result = _servise.UpdateUser(token, user);
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


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromQuery]string token, CreateUserDto user)
        {
            try
            {
                var result = _servise.AddUser(token, user);
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


        [HttpGet("Authorization")]
        public async Task<IActionResult> Authorization(string login, string password, string devise)
        {
            try
            {
                var result = _servise.Authorization(login,password,devise);
<<<<<<< HEAD
                return Ok(ApiResponse<AuthorizationUserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<TokenDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string token)
        {
            try
            {
                var result = _servise.GetUsers(token);
<<<<<<< HEAD
                return Ok(ApiResponse<IEnumerable<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<IEnumerable<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string token,string id)
        {
            try
            {
                var result = _servise.GetUserById(token,id);
<<<<<<< HEAD
                return Ok(ApiResponse<UserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<UserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        } 

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string token)
        {
            try
            {
                var result = _servise.GetUser(token);
<<<<<<< HEAD
                return Ok(ApiResponse<UserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
=======
                return BadRequest(ApiResponseDto<UserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            }
        } 
    }
}
