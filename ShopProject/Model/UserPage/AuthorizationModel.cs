using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing;

namespace ShopProject.Model.UserPage
{
    internal class AuthorizationModel
    {

        public async Task<bool> LogIn(string login, string password)
        {
            try
            {
                var response = await MainWebServerController.MainDataBaseConntroller.UserController.Authorization(login, password);

                if(response != null)
                {

                    AppSettingsManager.SetParameterFile("TokenUser", response.Token);
                    Session.User = response.User;
                    return true;
                }
                else
                {
                    throw new Exception("Не вдалося авторизуватися");
                }

                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


    }
}
