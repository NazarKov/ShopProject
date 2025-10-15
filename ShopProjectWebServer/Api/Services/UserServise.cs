using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Token;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Helpers;

namespace ShopProjectWebServer.Api.Services
{
    public class UserServise : IUserServise
    {
        public bool AddUser(string token, CreateUserDto user)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.UserTable.Add(user.ToUserEntity());
            return true;
        }

        public TokenDto Authorization(string login, string password, string devise)
        {
            var user = DataBaseMainController.DataBaseAccess.UserTable.Authorization(login, password);

            if (user!= null)
            {
                var tokenbody = GenerationToken.Generate(16);

                var token = new TokenEntity()
                {
                    Device = devise,
                    Token = tokenbody,
                    User = user,
                    CreateAt = DateTime.Now,
                };
                DataBaseMainController.DataBaseAccess.TokenTable.Add(token);
                AuthorizationApi.AddToken(tokenbody);

                return token.ToTokenDto();
            }
            throw new Exception("Не вдалося авторизуватися");
        }

        public bool DeleteUser(string token, string id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.UserTable.Delete(new UserEntity() { ID = Guid.Parse(id)});
            return true;
        }

        public UserDto GetUser(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.UserTable.GetUser(token).ToUserDto(); 
        }

        public UserDto GetUserById(string token, string id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.UserTable.GetById(Guid.Parse(id)).ToUserDto();
        }

        public PaginatorDto<UserDto> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var users = DataBaseMainController.DataBaseAccess.UserTable.GetByNameAndStatus(name, status);
            var paginator = PaginatorDto<UserEntity>.CreationPaginator(users,page, countColumn);
            return new PaginatorDto<UserDto>(paginator.Page, paginator.Pages, paginator.Data.ToUserDto());
        }

        public IEnumerable<UserDto> GetUsers(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.UserTable.GetAll().ToUserDto();
        }

        public PaginatorDto<UserDto> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status)
            =>GetUserByNamePageColumn(token,string.Empty,page,countColumn,status);

        public bool UpdateUser(string token, UpdateUserDto user)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.UserTable.Update(user.ToUserEntity());
            return true;
        }
    }
}
