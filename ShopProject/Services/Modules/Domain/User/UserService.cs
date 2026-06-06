using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.SignatureKey; 
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Model.UI.OperationRecorder; 
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi; 
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Common.Enum;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.OperationRecorderAndUser; 
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.Services.Modules.Mapping.UserRole;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http; 
using System.Threading.Tasks; 
using UserModel = ShopProject.Model.Domain.User.User;

namespace ShopProject.Services.Modules.Domain.User
{
    internal class UserService : IUserService
    {
        private static List<ShopProject.Model.Domain.User.User> _users;
        private string? _token;
        private MainElectronicTaxAccountController _mainTaxAccauntController;
        private SigningFileContoller _mainSigningFileController;
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;
        private ISettingService _settingService;
        public UserService(IMainWebServerService webServerService, ISessionService sessionService , ISettingService settingService)
        {
            _webServerService = webServerService;
            _sessionService = sessionService;
            _settingService = settingService;
            _users = new List<ShopProject.Model.Domain.User.User>();
            //_token = Session.User.Token;

            _mainTaxAccauntController = new MainElectronicTaxAccountController();
            _mainSigningFileController = new SigningFileContoller();
            _mainSigningFileController.Initialize(false);
        }
        public async Task<OperationResult<Paginator<UserModel, TypeStatusUser>>> GetPageColumn(int page, int countColumn, TypeStatusUser status)
        {
            var result = new OperationResult<Paginator<UserModel, TypeStatusUser>>();

            var response = await _webServerService.DataBase.UserController.GetPageColumn(new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<UserModel, TypeStatusUser>()
                    {
                        Data = paginator.Data.ToUser(_sessionService.Roles.ToUserRoleDto()),
                        DataType = (TypeStatusUser)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<Paginator<UserModel,TypeStatusUser>>> SearchByName(string item, int page, int countColumn, TypeStatusUser status)
        {
            var result = new OperationResult<Paginator<UserModel, TypeStatusUser>>();

            var response = await _webServerService.DataBase.UserController.GetByNamePageColumn(item, new() { Page = page, CountItemPage = countColumn, DataType = (int)status });

            if (response.Data != null)
            {
                var paginator = response.Data;
                if (paginator.Data != null)
                {
                    result.Data = new Paginator<UserModel, TypeStatusUser>()
                    {
                        Data = paginator.Data.ToUser(_sessionService.Roles.ToUserRoleDto()),
                        DataType = (TypeStatusUser)paginator.DataType,
                        Page = page,
                        Pages = paginator.Pages,
                    };


                }
            }
            result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
            result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
            result.ErrorMessage = response.Error;
            result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
            result.ValidationErrors = response.Errors;

            return result;
        }

        public async Task<OperationResult<UserModel>> CreateUser(string login, string email, string name, string password, string pathKey, string passwordKey, ShopProject.Model.Domain.UserRole.UserRole role)
        {
            var result = SetFielUserModel(login, email, name, password, pathKey, passwordKey, role);

            if (result.IsError)
            {
                return result;
            }
            Validation(result);
            if (result.IsError)
            {
                return result;
            }
            if (result.IsSuccess)
            {
                var response = await _webServerService.DataBase.UserController.Add(result.Data.ToCreateUserDto());

                if (response.Data != null)
                {
                    result.Data = response.Data.ToUser(_sessionService.Roles.ToUserRoleDto());
                } 
                result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                result.ErrorMessage = response.Error;
                result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                result.ValidationErrors = response.Errors;

                return result;
            }

            return new OperationResult<UserModel>()
            {
                ErrorMessage = "Невдалося викоанти операцію",
                Status = ResultStatus.Error,
            };
        }
        private OperationResult<UserModel> SetFielUserModel(string login, string email, string name, string password, string pathKey, string passwordKey, ShopProject.Model.Domain.UserRole.UserRole role)
        {
            try
            {
                string nameOwnerWithKey = string.Empty;
                string tinOwnerWithKey = string.Empty;

                 
                var signature = new SignatureKey();

                if (!string.IsNullOrEmpty(pathKey) || !string.IsNullOrEmpty(passwordKey))
                {
                    signature = new SignatureKey()
                    {
                        Signature = File.ReadAllBytes(pathKey),
                        CreateAt = DateTime.Now,
                        SignaturePassword = passwordKey,
                    };
                    var infoOwner = _mainSigningFileController.GetDataOwner(pathKey, passwordKey);
                    nameOwnerWithKey = infoOwner.subjFullName;
                    tinOwnerWithKey = infoOwner.subjDRFOCode; 

                }
                else
                {
                    signature = null;
                }
                  
                if (string.IsNullOrEmpty(name))
                {
                    if (string.IsNullOrEmpty(nameOwnerWithKey))
                    {
                        name = login;
                    }
                    else
                    {
                        name = nameOwnerWithKey;
                    }
                }

                var user = new UserModel()
                {
                    FullName = name, 
                    Login = login,
                    Email = email,
                    Password = password,
                    CreatedAt = DateTime.Now,
                    Role = role,
                };
                if (signature == null)
                {
                    user.SignatureKey = null;
                    user.Status = TypeStatusUser.NotAvailableElectronicKey;
                }
                else
                {
                    user.SignatureKey = signature;
                    user.Status = TypeStatusUser.AvailableElectronicKey;
                    user.TIN = tinOwnerWithKey;
                }
                return new OperationResult<UserModel>()
                {
                    Data = user,
                    Status = ResultStatus.Success,
                };
            }
            catch (Exception ex) 
            {
                return new OperationResult<UserModel>()
                {
                    ErrorMessage = ex.Message,
                    Status = ResultStatus.Error,
                };
            }
        }
        private OperationResult<UserModel> Validation(OperationResult<UserModel> item)
        {
            if (item.Data == null)
            {
                item.ErrorMessage = "Заповніть всі поля";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

           
            if (item.Data.Login == string.Empty)
            {
                item.ErrorMessage = "Ведіть логін";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            }

            if (item.Data.Password == string.Empty)
            {
                item.ErrorMessage = "Ведіть пароль";
                item.Status = ResultStatus.Error;
                item.ErrorType = ErrorType.Validation;
                return item;
            } 

            item.Status = ResultStatus.Success;
            return item;
        }

        public async Task<OperationResult<UserModel>> UpdateUser(UserModel user, string pathKey, string passwordKey)
        {
            var result = new OperationResult<UserModel>();
            result.Data = user;
            Validation(result);
            if (result.IsError)
            {
                return result;
            }
            else if (result.IsSuccess)
            {
                var signature = new SignatureKey();

                if (!string.IsNullOrEmpty(pathKey) || !string.IsNullOrEmpty(passwordKey))
                {
                    user.SignatureKey = new SignatureKey()
                    {
                        Signature = File.ReadAllBytes(pathKey),
                        CreateAt = DateTime.Now,
                        SignaturePassword = passwordKey,
                    };
                    user.Status = TypeStatusUser.AvailableElectronicKey;

                }
                else
                {
                    user.SignatureKey = null;
                    user.Status = TypeStatusUser.NotAvailableElectronicKey;
                } 

                var response = await _webServerService.DataBase.UserController.UpdateUser(user.ToUpdateUserDto());
                result.Source = Enum.Parse<ErrorSource>(response.Source.ToString());
                result.Status = Enum.Parse<ResultStatus>(response.Status.ToString());
                result.ErrorMessage = response.Error;
                result.ErrorType = Enum.Parse<ErrorType>(response.ErrorType.ToString());
                result.ValidationErrors = response.Errors;

                return result;
            }

            return new OperationResult<UserModel>()
            {
                ErrorMessage = "Невдалося викоанти операцію",
                Status = ResultStatus.Error,
            };

        }


        public async Task<bool> DeleteUser(ShopProject.Model.Domain.User.User user)
        {
            try
            {
                return await _webServerService.DataBase.UserController.DeleteUser(_token, user.ID.ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }

        }

        public async Task<List<OperationRecorderDialogWindowModel>> GetAllObject()
        {
            try
            {
                List<OperationRecorderDialogWindowModel> result = new List<OperationRecorderDialogWindowModel>();
                var response = (await _webServerService.DataBase.OperationRecorederController.GetOperationRecorders(_token)).ToOperationRecorder().ToList();

                foreach (var item in response)
                {
                    result.Add(new OperationRecorderDialogWindowModel(item));
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<bool> SaveBinding(ShopProject.Model.Domain.User.User user, List<OperationRecorderDialogWindowModel> objectOwnerHelpers)
        {
            try
            {
                List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> result = new List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
                foreach (var item in objectOwnerHelpers)
                {
                    if (item.isActive)
                    {
                        result.Add(item.OperationRecorder);
                    }
                }
                return await _webServerService.DataBase.OperationRecorderAndUserController.AddOperationRecordersAndUser(_token, user.ID, result.ToOperationRecordersEntity());

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ShopProject.Model.Domain.User.User>? GetUser()
        {
            // return await _webServerService.DataBase.UserController.GetUserById(_token, /*Session.UserItem.ID.ToString()*/"");
            return new UserModel();
        } 
       
         

       

        public async Task<bool> LogIn(string login, string password)
        {
            if (login == string.Empty)
            {
                throw new ExceptionStringEmpty("Заповніть поле Логін");
            }

            if (password == string.Empty)
            {
                throw new ExceptionStringEmpty("Заповніть поле Пароль");
            }

            try
            { 
                var response = await _webServerService.DataBase.UserController.Authorization(login, password);

                _sessionService.User = response.ToUser(_sessionService.Roles.ToUserRoleDto());
                _settingService.SetSetting<SessionSetting>(new SessionSetting() { User = response.ToUser(_sessionService.Roles.ToUserRoleDto()) });
                _webServerService.SetToken(response.Token);
                return true;

            }
            catch (HttpRequestException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new AuthorizationException("Не вдалося авторизуватися");
                }
                return false;
            }
            catch (AuthorizationException ex)
            {
                throw;
            }
            catch (Exception  ex)
            {
                throw;
            }
        }

        //public async Task<List<UserRoleEntity>> GetUserRoles()
        //{
        //    try
        //    {
        //        return null;// (await MainWebServerController.MainDataBaseConntroller.UserRoleController.GetRoles(Session.Token)).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        return new List<UserRoleEntity>();
        //    }
        //}

        public ShopProject.Model.Domain.User.User GetUserFromSession()
        {
            return _sessionService.User;
        }
        public void SetUpdateUserInSession(UserModel user)
        {
            _sessionService.UpdateUser = user;
        }
        public UserModel GetUpdateUserFromSession()
        {
            return _sessionService.UpdateUser;
        }
    }
}
