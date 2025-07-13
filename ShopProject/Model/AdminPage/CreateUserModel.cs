using NPOI.SS.Formula.Functions;
using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Views.SettingPage;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using SigningFileLib;
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
        private SigningFileContoller _mainSigningFileController;

        private List<UserRoleEntity> _userRoles;
        private List<ObjectOwnerEntity> _userObjects;

        public CreateUserModel()
        { 

            _userRoles = new List<UserRoleEntity>();
            _userObjects = new List<ObjectOwnerEntity>();

            _mainTaxAccauntController = new MainElectronicTaxAccountController();
            _mainSigningFileController = new SigningFileContoller();
            _mainSigningFileController.Initialize(false);
           
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

        public async Task<bool> CreateUserKey(string pathKey, string NameFile, string login, string email, string password, string passwrodKey, UserRoleEntity role)
        {
            try
            {
                string namefile = Path.GetFileName("C:\\ProgramData\\ShopProject\\Temp\\Key.txt.p7s");

                if (_mainSigningFileController.GetDataKey(pathKey, passwrodKey))
                {
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _mainTaxAccauntController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;
                    if (nameUser != null)
                    {
                        FileDirectory.Init();
                        FileDirectory.CreateUserFolder(nameUser);
                        string pathFile = FileDirectory.CopyKeyInUserFolder(NameFile, pathKey, nameUser);

                        var user = new UserEntity()
                        {
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            KeyPassword = passwrodKey,
                            KeyPath = pathFile,
                            Status = 1,
                            CreatedAt = DateTime.Now,
                            UserRole = role,
                        };
                         
                        var resultCreateUser = await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(Session.Token, user); 

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
