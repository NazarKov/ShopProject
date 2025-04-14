
using NPOI.SS.Formula.Functions;
using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.AdminPage
{
    internal class UserDataModel
    { 
        private UserEntity _user;

        public UserDataModel()
        {
            _user = new UserEntity();
        }

        public UserEntity? GetUser()
        {
            Task t = Task.Run(async () =>
            {
                _user = (await MainWebServerController.MainDataBaseConntroller.UserController.GetUserById(Session.Token, Session.UserItem.ID.ToString()));
            });
            t.Wait();

            return _user;
        }
    }
}
