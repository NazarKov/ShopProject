using NPOI.SS.Formula.Functions;
using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.SigningFileService;
using ShopProject.Helpers.SigningFileService.Model;
using ShopProject.Views.SettingPage;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.AdminPage
{
    class CreateUserModel
    {  
        private MainElectronicTaxAccountController _mainTaxAccauntController;
        private SigningFileContoller _mainControllerTcp;

        private List<UserRoleEntity> _userRoles;
        private List<ObjectOwnerEntity> _userObjects;

        public CreateUserModel()
        { 

            _userRoles = new List<UserRoleEntity>();
            _userObjects = new List<ObjectOwnerEntity>();

            _mainTaxAccauntController = new MainElectronicTaxAccountController();
            _mainControllerTcp = new SigningFileContoller();

        }
        public void CreateUser(string fulleName, string login, string email, string password, UserRoleEntity role)
        {
            try
            {
                var user = new UserEntity()
                {
                    FullName = fulleName,
                    Login = login,
                    Email = email,
                    Password = password,
                    UserRole = role,
                    Status = 0,
                    CreatedAt = DateTime.Now,

                };

                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(Session.Token, user);
                });
                t.Wait();


                MessageBox.Show("Користувача створено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<bool> CreateUserKey(string pathFile, string NameFile, string login, string email, string password, string passwrodKey, UserRoleEntity role)
        {
            UserCommand result = new UserCommand() { Status = "100" };
            try
            {
                string namefile = Path.GetFileName("C:\\ProgramData\\ShopProject\\Temp\\Key.txt.p7s");


                //if (namefile == null && namefile ==string.Empty && namefile == "" )
                //{
                if (!_mainControllerTcp.IsConnectingServise())
                {
                    if (!_mainControllerTcp.IsStartServise())
                    {
                        _mainControllerTcp.StartServise();
                    }
                    _mainControllerTcp.ConnectService();
                }

                result = _mainControllerTcp.SendingCommand(new UserCommand()
                {
                    TypeCommand = TypeCommand.IsInitialize,
                    Time = DateTime.Now,
                });

                if (result.Status == "404")
                {
                    _mainControllerTcp.SendingCommand(new UserCommand()
                    {
                        TypeCommand = TypeCommand.Initialize,
                        Time = DateTime.Now,
                    });
                }


                result = _mainControllerTcp.SendingCommand(new UserCommand()
                {
                    TypeCommand = TypeCommand.GetDataKey,
                    PathKey = pathFile,
                    PasswordKey = passwrodKey,
                    Time = DateTime.Now,
                });
                //}

                if (result.Status == "100")
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainTaxAccauntController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;
                    if (nameUser != null)
                    {
                        FileDirectory.Init();
                        FileDirectory.CreateUserFolder(nameUser);
                        string pathKey = FileDirectory.CopyKeyInUserFolder(NameFile, pathFile, nameUser);

                        var user = new UserEntity()
                        {
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            KeyPassword = passwrodKey,
                            KeyPath = pathKey,
                            Status = 1,
                            CreatedAt = DateTime.Now,
                            UserRole = role,
                        };
                         
                        var resultCreateUser = await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(Session.Token, user); 
                  
                        _mainControllerTcp.SendingCommand(new UserCommand()
                        {
                            TypeCommand = TypeCommand.DisconnectUser,
                            Time = DateTime.Now,
                        });
                        return resultCreateUser;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<UserRoleEntity> GetUserRoles()
        {
            try
            {
                Task t = Task.Run(async () =>
                {
                    _userRoles = (await MainWebServerController.MainDataBaseConntroller.UserRoleController.GetRoles(Session.Token)).ToList();
                });
                t.Wait();
                return _userRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<UserRoleEntity>();
            }
        }
    }
}
