using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Token;
using ShopProjectWebServer.Api.DtoModels.User;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IUserServise
    {
        public PaginatorDto<UserDto> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status);
        public PaginatorDto<UserDto> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status); 
        public bool DeleteUser(string token, string id);
        public bool UpdateUser(string token, UpdateUserDto user);
        public bool AddUser(string token, CreateUserDto user); 
        public TokenDto Authorization(string login, string password, string devise); 
        public IEnumerable<UserDto> GetUsers(string token);
        public UserDto GetUserById(string token, string id);
        public UserDto GetUser(string token);
    }
}
