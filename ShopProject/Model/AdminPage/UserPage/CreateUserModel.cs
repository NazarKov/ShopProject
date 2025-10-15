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
using ShopProjectDataBase.Helper;
using ShopProject.UIModel.UserPage;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;

namespace ShopProject.Model.AdminPage.UserPage
{
    internal class CreateUserModel
    {
        private MainElectronicTaxAccountController _mainTaxAccauntController;
        private SigningFileContoller _mainSigningFileController;
        private readonly string _token;

        public CreateUserModel() 
        {
            _mainTaxAccauntController = new MainElectronicTaxAccountController();
            _mainSigningFileController = new SigningFileContoller();
            _mainSigningFileController.Initialize(false);
            _token = Session.User.Token;
        }

        public async Task<bool> CreateUser(string fullName , string login , string email ,string password , UserRole role)
        {
            try
            {
                var user = new User()
                {
                    FullName = fullName,
                    Login = login,
                    Email = email,
                    Password = password,
                    Role = role, 
                    CreatedAt = DateTime.Now,
                    Status =  TypeStatusUser.NotAvailableElectronicKey,
                    SignatureKey =  null,
                };

                return await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(_token, user.ToCreateUserDto()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

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
                        FileDirectory.Init();
                        FileDirectory.CreateUserFolder(nameUser);
                       
                        string pathFile = FileDirectory.CopyKeyInUserFolder(namefile, pathKey, nameUser);

                        var signature = new SignatureKey()
                        {
                            Signature = File.ReadAllBytes(pathFile),
                            CreateAt = DateTime.Now,
                            SignaturePassword = passwrodKey,
                        };

                        var user = new User()
                        {
                            FullName = nameUser,
                            TIN = infoUser.ElementAt(0).values.TIN,
                            Login = login,
                            Email = email,
                            Password = password,
                            SignatureKey = signature,
                            Status =  TypeStatusUser.AvailableElectronicKey,
                            CreatedAt = DateTime.Now,
                            Role = role,
                        };
                        return await MainWebServerController.MainDataBaseConntroller.UserController.AddUser(_token, user.ToCreateUserDto());
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
    }
}
