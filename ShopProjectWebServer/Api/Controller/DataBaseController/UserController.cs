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
                return BadRequest(ApiResponseDto<PaginatorDto<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetUsersPageColumn")]
        public IActionResult GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var result = _servise.GetUsersPageColumn(token, page, countColumn, status);
                return BadRequest(ApiResponseDto<PaginatorDto<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string token, string id)  
        {
            try
            {
                var result = _servise.DeleteUser(token,id);
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }


        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] string token, UpdateUserDto user)
        {
            try
            {
                var result = _servise.UpdateUser(token, user);
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromQuery]string token, CreateUserDto user)
        {
            try
            {
                var result = _servise.AddUser(token, user);
                return BadRequest(ApiResponseDto<bool>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }


        [HttpGet("Authorization")]
        public async Task<IActionResult> Authorization(string login, string password, string devise)
        {
            try
            {
                var result = _servise.Authorization(login,password,devise);
                return BadRequest(ApiResponseDto<TokenDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string token)
        {
            try
            {
                var result = _servise.GetUsers(token);
                return BadRequest(ApiResponseDto<IEnumerable<UserDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string token,string id)
        {
            try
            {
                var result = _servise.GetUserById(token,id);
                return BadRequest(ApiResponseDto<UserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        } 

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string token)
        {
            try
            {
                var result = _servise.GetUser(token);
                return BadRequest(ApiResponseDto<UserDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<string>.Fail(ex.Message));
            }
        } 
    }
}
