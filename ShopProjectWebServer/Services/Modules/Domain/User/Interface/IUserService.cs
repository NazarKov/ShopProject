using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Token;
using ShopProjectWebServer.Api.DtoModels.User;

namespace ShopProjectWebServer.Services.Modules.Domain.User.Interface
{
    public interface IUserService
    {
        public PaginatorDto<UserDto> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status);
        public PaginatorDto<UserDto> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status); 
        public bool AddUser(string token, CreateUserDto user);
        public bool UpdateUser(string token, UpdateUserDto user);
        public bool UpdateParameterUser(string token, string id, string nameParameter, object value);
        public bool DeleteUser(string token, string id);
       
        public Models.Domain.User.User Authorization(string login, string password, string devise);
        public IEnumerable<UserDto> GetUsers(string token);
        public Models.Domain.User.User GetUserById(string token, string id);
        public UserDto GetUser(string token);
    }
}
