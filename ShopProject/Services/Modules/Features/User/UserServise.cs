using ShopProject.Helpers;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Enum;
using ShopProject.Model.Exceptions;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi;
using ShopProject.Services.Integration.Network.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping;
using ShopProject.Services.Modules.ModelService.User.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting.Interface;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.ModelService.User
{
    internal class UserServise : IUserServise
    {
        private static List<ShopProject.Model.Domain.User.User> _users;
        private string? _token;
        private MainElectronicTaxAccountController _mainTaxAccauntController;
        private SigningFileContoller _mainSigningFileController;
        private IMainWebServerService _webServerService;
        private ISessionService _sessionService;
        private ISettingService _settingService;
        public UserServise(IMainWebServerService webServerService, ISessionService sessionService , ISettingService settingService)
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
        public async Task<Paginator<ShopProject.Model.Domain.User.User>> GetUsersPageColumn(int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var result = await _webServerService.DataBase.UserController.GetUsersPageColumn(_token, page, countColumn, status);

                Paginator<ShopProject.Model.Domain.User.User> paginator = new Paginator<ShopProject.Model.Domain.User.User>()
                {
                    Data = result.Data.ToUser(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.User.User>();
            }
        }

        public async Task<Paginator<ShopProject.Model.Domain.User.User>> SearchByName(string item, int page, int countColumn, TypeStatusUser status)
        {
            try
            {
                var result = await _webServerService.DataBase.UserController.GetUserByNamePageColumn(_token, item, page, countColumn, status);

                var paginator = new Paginator<ShopProject.Model.Domain.User.User>()
                {
                    Data = result.Data.ToUser(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return new Paginator<ShopProject.Model.Domain.User.User>();
            }
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
            return await _webServerService.DataBase.UserController.GetUserById(_token, /*Session.UserItem.ID.ToString()*/"");
        }

        public async Task<bool> UpdateUser(Guid id, string fullName, string login, string email, string password, ShopProject.Model.Domain.UserRole.UserRole role)
        {
            try
            {
                var user = new ShopProject.Model.Domain.User.User()
                {
                    ID = id,
                    FullName = fullName,
                    Login = login,
                    Email = email,
                    Password = password,
                    Role = role,
                    Status = TypeStatusUser.NotAvailableElectronicKey,
                    SignatureKey = null,
                };

                return await _webServerService.DataBase.UserController.UpdateUser(_token, user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserKey(Guid id, string pathKey, string namefile, string login, string email, string password, string passwrodKey, ShopProject.Model.Domain.UserRole.UserRole role)
        {
            try
            {
                if (_mainSigningFileController.GetDataToFile(pathKey, passwrodKey))
                {
                    var response = await _mainTaxAccauntController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;
                    if (nameUser != null)
                    {
                        //FileDirectory.Init();
                        //FileDirectory.CreateUserFolder(nameUser);

                        string pathFile = ""; /*FileDirectory.CopyKeyInUserFolder(namefile, pathKey, nameUser);*/

                        var signature = new ShopProject.Model.Domain.SignatureKey.SignatureKey()
                        {
                            Signature = File.ReadAllBytes(pathFile),
                            CreateAt = DateTime.Now,
                            SignaturePassword = passwrodKey,
                        };

                        var user = new ShopProject.Model.Domain.User.User()
                        {
                            ID = id,
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            SignatureKey = signature,
                            Status = TypeStatusUser.AvailableElectronicKey,
                            Role = role,
                        };
                        return await _webServerService.DataBase.UserController.UpdateUser(_token, user);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateUser(string fullName, string login, string email, string password, UserRole role)
        {
            try
            {
                var user = new ShopProject.Model.Domain.User.User()
                {
                    FullName = fullName,
                    Login = login,
                    Email = email,
                    Password = password,
                    Role = role,
                    CreatedAt = DateTime.Now,
                    Status = TypeStatusUser.NotAvailableElectronicKey,
                    SignatureKey = null,
                };

                return await _webServerService.DataBase.UserController.AddUser(_token, user.ToCreateUserDto());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                return false;
            }
        }

        public async Task<bool> CreateUserKey(string pathKey, string namefile, string login, string email, string password, string passwrodKey, UserRole role)
        {
            try
            {
                if (_mainSigningFileController.GetDataToFile(pathKey, passwrodKey))
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainTaxAccauntController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;
                    if (nameUser != null)
                    {
                        //FileDirectory.Init();
                        //FileDirectory.CreateUserFolder(nameUser);

                        string pathFile = ""; /*FileDirectory.CopyKeyInUserFolder(namefile, pathKey, nameUser);*/

                        var signature = new SignatureKey()
                        {
                            Signature = File.ReadAllBytes(pathFile),
                            CreateAt = DateTime.Now,
                            SignaturePassword = passwrodKey,
                        };

                        var user = new ShopProject.Model.Domain.User.User()
                        {
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            SignatureKey = signature,
                            Status = TypeStatusUser.AvailableElectronicKey,
                            CreatedAt = DateTime.Now,
                            Role = role,
                        };
                        return await _webServerService.DataBase.UserController.AddUser(_token, user.ToCreateUserDto());
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
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

                _sessionService.User =response.ToUser();
                _settingService.SetSetting<SessionSetting>(new SessionSetting() { User = response.ToUser() });
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
    }
}
