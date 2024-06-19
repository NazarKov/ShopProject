using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.UserPage
{
    internal class AuthorizationModel
    {
        private IEntityAccess<UserEntiti> _userTable;

        private List<UserEntiti> _users;


        public AuthorizationModel() 
        {
            _userTable = new UserTableAccess();
            
            _users = new List<UserEntiti>();
        }

        public bool LogIn(string login , string password)
        {
            try
            {
                _users = (List<UserEntiti>)_userTable.GetAll();

                var user = _users.Where(item => item.Login == login).FirstOrDefault();
                if(user != null)
                {
                    if(user.Password.Equals(password))
                    {
                        Session.Add(user);
                        return true;
                    }
                    else 
                    {
                        throw new Exception("Невірний пароль користувача");
                    }
                }
                else
                {
                    throw new Exception("Не знайдено корисувача");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        

    }
}
