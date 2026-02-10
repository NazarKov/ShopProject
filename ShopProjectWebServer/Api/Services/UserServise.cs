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
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public UserServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public bool AddUser(string token, CreateUserDto user)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.UserTable.Add(user.ToUserEntity());
            return true;
        }
         
        public AuthorizationUserDto Authorization(string login, string password, string devise) 
        {
            var user = _controller.DataBaseAccess.UserTable.Authorization(login, password);

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
                _controller.DataBaseAccess.TokenTable.Add(token);
                AuthorizationServise.AddToken(tokenbody);
                 
                var result = new AuthorizationUserDto()
                {
                    AutomaticLogin = user.AutomaticLogin, 
                    Email = user.Email,
                    FullName = user.FullName,
                    Login = user.Login,
                    TIN = user.TIN,
                    Token = token.Token,
                    UserRoleID = user.UserRole.ID
                };

                return result; 
            }
            throw new Exception("Не вдалося авторизуватися");
        }

        public bool DeleteUser(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.UserTable.Delete(new UserEntity() { ID = Guid.Parse(id)});
            return true;
        }

        public UserDto GetUser(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.UserTable.GetUser(token).ToUserDto(); 
        }

        public UserDto GetUserById(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.UserTable.GetById(Guid.Parse(id)).ToUserDto();
        }

        public PaginatorDto<UserDto> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var users = _controller.DataBaseAccess.UserTable.GetByNameAndStatus(name, status);
            var paginator = PaginatorDto<UserEntity>.CreationPaginator(users,page, countColumn);
            return new PaginatorDto<UserDto>(paginator.Page, paginator.Pages, paginator.Data.ToUserDto());
        }

        public IEnumerable<UserDto> GetUsers(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.UserTable.GetAll().ToUserDto();
        }

        public PaginatorDto<UserDto> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status)
            =>GetUserByNamePageColumn(token,string.Empty,page,countColumn,status);

        public bool UpdateUser(string token, UpdateUserDto user)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.UserTable.Update(user.ToUserEntity());
            return true;
        }
    }
}
