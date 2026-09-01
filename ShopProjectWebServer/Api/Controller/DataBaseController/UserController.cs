using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Api.Common; 
using ShopProjectWebServer.Api.DtoModels.User;  
using ShopProjectWebServer.Api.Validation.Interface; 
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.User;

namespace ShopProjectWebServer.Api.Controller.DataBaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IValidator<CreateUserDto> _createValidator;
        private IValidator<UpdateUserDto> _updateValidator;
        private IValidator<UserDto> _authorizationValidator;
        private IUserService _service;
        public UserController(IUserService service, IValidator<CreateUserDto> createValidator , IValidator<UpdateUserDto> updateValidator , IValidator<UserDto> authorizationValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _authorizationValidator = authorizationValidator;
        }

        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateUserDto user)
        {
            try
            {
                var validation = _createValidator.Validation(user);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<UserDto>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.Add(user.ToUser());

                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<UserDto>.Ok(result.Data.ToUserDto()));
                }
                else
                {
                    return Ok(ApiResponse<UserDto>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, ErrorType.Server));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateUserDto user)
        {
            try
            {
                var validation = _updateValidator.Validation(user);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.Update(user.ToUser());

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
        public async Task<IActionResult> UpdateParameter([FromQuery] string parameter, [FromQuery] string value, UpdateUserDto userDto)
        {
            try
            {
                var validation = _updateValidator.Validation(userDto);
                if (!validation.isValid)
                {
                    return Ok(ApiResponse<bool>.Fail(validation.Errors, ErrorType.Validation, ErrorSource.Client));
                }

                var result = await _service.UpdateParameter(userDto.ID,parameter, value);

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
        [AllowAnonymous]
        [HttpGet("Authorization")]
        public async Task<IActionResult> Authorization(string login, string password, string devise)
        {
            try
            {
                var result = _service.Authorization(login, password, devise);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<AuthorizationUserDto>.Ok(result.Data.ToAuthoUserDto()));
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
        [HttpGet("GetById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var result = _service.GetById(id);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<UserDto>.Ok(result.Data.ToUserDto()));
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
        [HttpGet("GetByToken")]
        public async Task<IActionResult> GetByToken(string token)
        {
            try
            {
                var result = _service.GetUser(token);
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<UserDto>.Ok(result.Data.ToUserDto()));
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
        [HttpPost("GetByNamePageColumn")]
        public IActionResult GetByNamePageColumn([FromQuery] string name, [FromBody] PaginatorDto<UserDto, int> paginator)
        {
            try
            {
                var result = _service.GetByNamePageColumn(name, paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<UserDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<UserDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
        [Authorize(AuthenticationSchemes = "ApiAuthorization")] 
        [HttpPost("GetPageColumn")]
        public IActionResult GetPageColumn(PaginatorDto<UserDto, int> paginator)
        {
            try
            {
                var result = _service.GetPageColumn(paginator.ToPaginator());
                if (result.IsSuccess)
                {
                    return Ok(ApiResponse<PaginatorDto<UserDto, int>>.Ok(result.Data.ToPaginatorDto()));
                }
                else
                {
                    return Ok(ApiResponse<PaginatorDto<UserDto, int>>.Fail(result.ErrorMessage, Enum.Parse<ErrorType>(result.ErrorType.ToString()), Enum.Parse<ErrorSource>(result.Source.ToString())));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
