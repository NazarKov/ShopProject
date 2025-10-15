using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;  
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using SigningFileLib;
using System.IO;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProject.Model.AdminPage.UserPage
{
    internal class UpdateUserModel
    {
        private MainElectronicTaxAccountController _mainTaxAccauntController;
        private SigningFileContoller _mainSigningFileController;

        public UpdateUserModel()
        {
            _mainTaxAccauntController = new MainElectronicTaxAccountController();
            _mainSigningFileController = new SigningFileContoller();
            _mainSigningFileController.Initialize(false);
        }

        public async Task<bool> UpdateUser(Guid id,string fullName, string login, string email, string password, UserRoleEntity role)
        {
            try
            {
                var user = new UserEntity()
                {
                    ID = id,
                    FullName = fullName,
                    Login = login,
                    Email = email,
                    Password = password,
                    UserRole = role, 
                    Status =  TypeStatusUser.NotAvailableElectronicKey,
                    SignatureKey = null,
                };

                return await MainWebServerController.MainDataBaseConntroller.UserController.UpdateUser(Session.Token, user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public async Task<bool> UpdateUserKey(Guid id, string pathKey, string namefile, string login, string email, string password, string passwrodKey, UserRoleEntity role)
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
                            ID = id,
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            SignatureKey = signature,
                            Status =  TypeStatusUser.AvailableElectronicKey, 
                            UserRole = role,
                        };
                        return await MainWebServerController.MainDataBaseConntroller.UserController.UpdateUser(Session.Token, user);
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
                return null;// (await MainWebServerController.MainDataBaseConntroller.UserRoleController.GetRoles(Session.Token)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<UserRoleEntity>();
            }
        }
    }
}
