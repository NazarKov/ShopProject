using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers.SMTPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.UserPage
{
    internal class ChangePasswordModel
    {
        private string _code;
        private SMTPController _SMTPController;

        IEntityAccess<UserEntiti> _userTable;
        IEntityUpdate<UserEntiti> _userTableUpdate;

        public ChangePasswordModel()
        {
            _code = string.Empty;

            _userTable = new UserTableAccess();
            _userTableUpdate = new UserTableAccess();
            _SMTPController = new SMTPController();
        }


        public void SendMessageEmail(string email)
        {
            _code = new Random().Next(000000, 999999).ToString();
            _SMTPController.SendMessage(email, _code, TypeSMPTServer.Gmail);
        }

        public bool ConfirmCode(string code)
        {
            if (code.Equals(_code))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ChangePassword(string email, string password)
        {
            try
            {
                var user = _userTable.GetAll().Where(item => item.Email == email).FirstOrDefault();
                if (user != null)
                {
                    _userTableUpdate.UpdateParameter(user.ID, nameof(user.Password), password);
                    throw new Exception("Пароль змінено");
                }
                else
                {
                    throw new Exception("Користувач відсутній");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
