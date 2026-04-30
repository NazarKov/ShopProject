using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Token;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models.Exceptions; 
using ShopProjectWebServer.Services.Modules.Authorization.Interface;
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;
using ShopProjectWebServer.Services.Modules.Mapping;

namespace ShopProjectWebServer.Services.Modules.Domain.User
{
    internal class UserService : IUserService
    {
        private IDataBaseService _dataBaseService;
        private IAuthorizationService _authorizationServise;

        public UserService(IDataBaseService dataBaseService, IAuthorizationService authorizationService)
        {
            _dataBaseService = dataBaseService;
            _authorizationServise = authorizationService;
        }
        public bool AddUser(string token, CreateUserDto user)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _dataBaseService.DataBaseAccess.UserTable.Add(user.ToUserEntity());
            return true;
        }
         
        public ShopProjectWebServer.Models.Domain.User.User Authorization(string login, string password, string devise) 
        {
            if (login == null||login == string.Empty)
            {
                throw new EmptyFieldException("Ведіть логін");
            }

            if (password == null||password == string.Empty)
            {
                throw new EmptyFieldException("Ведіть пароль");
            }

            var user = _dataBaseService.DataBaseAccess.UserTable.GetUserByLogin(login);
             
            if (user!= null)
            {
                if(!user.Password.Equals(password))
                {
                    throw new AuthorizationException("Невірний пароль");
                }


                var tokenbody = GenerationToken.Generate(16);

                var token = new TokenEntity()
                {
                    Device = devise,
                    Token = tokenbody,
                    User = user,
                    CreateAt = DateTime.Now,
                };
                _dataBaseService.DataBaseAccess.TokenTable.Add(token);
                _authorizationServise.AddToken(tokenbody);

                var result = new ShopProjectWebServer.Models.Domain.User.User()
                {
                    ID= user.ID,
                    AutomaticLogin = user.AutomaticLogin, 
                    Email = user.Email,
                    FullName = user.FullName,
                    Login = user.Login,
                    TIN = user.TIN,
                    Token = token.Token, 
                    Status= user.Status,
                    CreatedAt = DateTime.Now,
                };
                if(user.UserRole != null)
                {
                    result.UserRole = user.UserRole.ToUserRole();
                }

                return result; 
            }
            else
            {
                throw new AuthorizationException("Користувача не знайдено");
            }
            throw new AuthorizationException("Не вдалося авторизуватися");
        }

        public bool DeleteUser(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _dataBaseService.DataBaseAccess.UserTable.Delete(new UserEntity() { ID = Guid.Parse(id)});
            return true;
        }

        public UserDto GetUser(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _dataBaseService.DataBaseAccess.UserTable.GetUser(token).ToUser().ToUserDto(); 
        }

        public ShopProjectWebServer.Models.Domain.User.User GetUserById(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _dataBaseService.DataBaseAccess.UserTable.GetById(Guid.Parse(id)).ToUser();
        }

        public PaginatorDto<UserDto> GetUserByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUser status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var users = _dataBaseService.DataBaseAccess.UserTable.GetByNameAndStatus(name, status);
            var paginator = PaginatorDto<UserEntity>.CreationPaginator(users,page, countColumn);
            return new PaginatorDto<UserDto>(paginator.Page, paginator.Pages, paginator.Data.ToUser().ToUserDto());
        }

        public IEnumerable<UserDto> GetUsers(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _dataBaseService.DataBaseAccess.UserTable.GetAll().ToUser().ToUserDto();
        }

        public PaginatorDto<UserDto> GetUsersPageColumn(string token, int page, int countColumn, TypeStatusUser status)
            =>GetUserByNamePageColumn(token,string.Empty,page,countColumn,status);

        public bool UpdateUser(string token, UpdateUserDto user)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _dataBaseService.DataBaseAccess.UserTable.Update(user.ToUserEntity());
            return true;
        }
        public bool UpdateParameterUser(string token,string id , string nameParameter, object value)
        {


            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _dataBaseService.DataBaseAccess.UserTable.UpdateParameter(Guid.Parse(id),nameParameter,value);
            return true;
        }
    }
}
