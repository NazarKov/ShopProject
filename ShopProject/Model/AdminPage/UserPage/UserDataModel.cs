 using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.AdminPage.UserPage
{
    internal class UserDataModel
    { 
        private User _user;

        public UserDataModel()
        {
            _user = new User();
        }

        public async Task<User>? GetUser()
        { 
            return await MainWebServerController.MainDataBaseConntroller.UserController.GetUserById(Session.User.Token, Session.UserItem.ID.ToString());
        }
    }
}
