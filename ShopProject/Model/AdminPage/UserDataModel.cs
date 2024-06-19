using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.AdminPage
{
    internal class UserDataModel
    {
        private IEntityAccess<UserEntiti> _userTabel;
        private List<UserEntiti> _users;

        public UserDataModel() 
        {
            _userTabel = new UserTableAccess();
            _users = new List<UserEntiti>();
        }

        public UserEntiti? GetUser(string login)
        {
            _users = (List<UserEntiti>)_userTabel.GetAll();

            if(_users.Count > 0)
            {
                return _users.Where(item => item.Login == login).FirstOrDefault();
            }
            return null;
        }

    }
}
