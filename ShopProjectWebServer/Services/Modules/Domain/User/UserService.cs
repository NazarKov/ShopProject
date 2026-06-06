using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Helpers; 
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum; 
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.User;
using ShopProjectWebServer.Services.Modules.Mapping.UserRole; 
using UserModel = ShopProjectWebServer.Models.Domain.User.User;

namespace ShopProjectWebServer.Services.Modules.Domain.User
{
    internal class UserService : IUserService
    {
        private IDataBaseService _dataBaseService; 

        public UserService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService; 
        }
        public async Task<OperationResult<UserModel>> Add(UserModel user)
        {
            try
            {
                var valid = await CreateValidation(user);
                if (valid.IsError)
                {
                    return valid;
                }

                var result = await _dataBaseService.DataBaseAccess.UserTable.AddAsync(user.ToUserEntity());
                return OperationResult<UserModel>.Success(result.ToUser());
            }
            catch (Exception ex)
            {
                return OperationResult<UserModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        private async Task<OperationResult<UserModel>> CreateValidation(UserModel user)
        {
            if (await _dataBaseService.DataBaseAccess.UserTable.ExistsByLogin(user.Login))
            {
                return new OperationResult<UserModel>()
                {
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.ObjectExists,
                    Source = ErrorSource.Database,
                    ErrorMessage = "Користувач існує"
                };
            }
            return new OperationResult<UserModel>()
            {
                Status = ResultStatus.Success,
            };
        }

        public async Task<OperationResult<bool>> Update(UserModel user)
        {
            try
            {
                await _dataBaseService.DataBaseAccess.UserTable.UpdateAsync(user.ToUserEntity());
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
        public async Task<OperationResult<bool>> UpdateParameter(string id, string nameParameter, object value)
        {
            try
            {
                await _dataBaseService.DataBaseAccess.UserTable.UpdateParameterAsync(new Guid(id), nameParameter, value);
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<bool> Delete(string id)
        {
            try
            {
                _dataBaseService.DataBaseAccess.UserTable.DeleteAsync(Guid.Parse(id));
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
            
        }



        public OperationResult<UserModel> Authorization(string login, string password, string devise) 
        { 
            var user = _dataBaseService.DataBaseAccess.UserTable.GetByLogin(login);
             
            if (user!= null)
            {
                if(!user.Password.Equals(password))
                {
                    return OperationResult<UserModel>.Fail("Невірний пароль", ErrorType.Authorized, ErrorSource.Client); 
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

                var result = new UserModel()
                {
                    ID= user.ID,
                    AutomaticLogin = user.AutomaticLogin, 
                    Email = user.Email,
                    FullName = user.FullName,
                    Login = user.Login,
                    TIN = user.TIN,
                    Token = token.Token, 
                    Status = (Models.Domain.Enum.TypeStatusUser)user.Status,
                    CreatedAt = DateTime.Now,
                };
                if(user.UserRole != null)
                {
                    result.UserRole = user.UserRole.ToUserRole();
                }

                return OperationResult<UserModel>.Success(result);
            }
            else
            {
                return OperationResult<UserModel>.Fail("Користувача не знайдено", ErrorType.Authorized, ErrorSource.Client); 
            } 
        } 

        public OperationResult<UserModel> GetUser(string token)
        {
            try
            {
                var result = _dataBaseService.DataBaseAccess.UserTable.GetUser(token);
                if (result != null)
                {
                    return OperationResult<UserModel>.Success(result.ToUser()); 
                }
                else
                {
                    return OperationResult<UserModel>.Fail("Користувача не знайдено", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch(Exception ex)
            {
                return OperationResult<UserModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
            
        }

        public OperationResult<UserModel> GetById(string id)
        {
            try
            {
                var result = _dataBaseService.DataBaseAccess.UserTable.GetById(Guid.Parse(id));
                if (result != null)
                {
                    return OperationResult<UserModel>.Success(result.ToUser());
                }
                else
                {
                    return OperationResult<UserModel>.Fail("Користувача не знайдено", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UserModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<IEnumerable<UserModel>> GetUsers()
        {
            try
            {
                var result = _dataBaseService.DataBaseAccess.UserTable.GetAll();
                return OperationResult<IEnumerable<UserModel>>.Success(result.ToUser());
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<UserModel>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int>> GetByNamePageColumn(string name, ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int> paginator)
        {
            try
            {
                var users = _dataBaseService.DataBaseAccess.UserTable.GetByNameAndStatus(name, (TypeStatusUser)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserEntity, TypeStatusUser>.CreationPaginator(users.Reverse(), paginator.Page, paginator.CountItemPage, (TypeStatusUser)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int>>.Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToUser(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }



        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int>> GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int> paginator)
            => GetByNamePageColumn(string.Empty,paginator); 
    }
}
