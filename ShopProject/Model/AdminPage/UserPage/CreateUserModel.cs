using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Threading.Tasks;
using System.Windows;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using SigningFileLib; 
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProjectSQLDataBase.Entities;

namespace ShopProject.Model.AdminPage.UserPage
{
    internal class CreateUserModel
    {
        private MainElectronicTaxAccountController _mainTaxAccauntController;
        private SigningFileContoller _mainSigningFileController;

        public CreateUserModel() 
        {
            _mainTaxAccauntController = new MainElectronicTaxAccountController();
            _mainSigningFileController = new SigningFileContoller();
            _mainSigningFileController.Initialize(false);
        }

        public async Task<bool> CreateUser(string fullName , string login , string email ,string password , UserRoleEntity role)
        {
            try
            {
                var user = new UserEntity()
                {
                    FullName = fullName,
                    Login = login,
                    Email = email,
                    Password = password,
                    UserRole = role, 
                    CreatedAt = DateTime.Now,
                    Status = ShopProjectSQLDataBase.Helper.TypeStatusUser.NotAvailableElectronicKey,
                    SignatureKey =  null,
                };

                return await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(Session.Token, user); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public async Task<bool> CreateUserKey(string pathKey, string namefile, string login, string email, string password, string passwrodKey, UserRoleEntity role)
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
                        FileDirectory.Init();
                        FileDirectory.CreateUserFolder(nameUser);
                       
                        string pathFile = FileDirectory.CopyKeyInUserFolder(namefile, pathKey, nameUser);

                        var signature = new ElectronicSignatureKey()
                        {
                            Signature = File.ReadAllBytes(pathFile),
                            CreateAt = DateTime.Now,
                            SignaturePassword = passwrodKey,
                        };

                        var user = new UserEntity()
                        {
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            SignatureKey = signature,
                            Status = ShopProjectSQLDataBase.Helper.TypeStatusUser.AvailableElectronicKey,
                            CreatedAt = DateTime.Now,
                            UserRole = role,
                        };
                        return await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(Session.Token, user);
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

        public async Task<List<UserRoleEntity>> GetUserRoles()
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.UserRoleController.GetRoles(Session.Token)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<UserRoleEntity>();
            }
        }
    }
}
