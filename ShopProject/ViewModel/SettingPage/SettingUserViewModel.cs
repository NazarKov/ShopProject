using ShopProject.Helpers;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingUserViewModel : ViewModel<SettingUserViewModel>
    {
        private SettingUserModel _model;

        private ICommand _changePasswordCommand;

        public SettingUserViewModel() 
        {
            _model = new SettingUserModel();

            _login = string.Empty;
            _fullName = string.Empty;
            _tin = string.Empty;
            _nameKey = string.Empty;
            _status = string.Empty;
            _create_at = new DateTime();
            _autoLogin = false;

           // SetFieldPage();
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
        }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged("Full_Name"); }
        }
        private string _tin;
        public string TIN
        {
            get { return _tin; }
            set { _tin = value; OnPropertyChanged("TIN"); }
        }
        private string _nameKey;
        public string NameKey
        {
            get { return _nameKey; }
            set { _nameKey = value; OnPropertyChanged("NameKey"); }
        }
        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }
        private DateTime _create_at;
        public DateTime Create_At
        {
            get { return _create_at; }
            set { _create_at = value; OnPropertyChanged("Create_At"); }
        }
        private bool _autoLogin;
        public bool AutoLogin
        {
            get { return _autoLogin; }
            set { _autoLogin = value; OnPropertyChanged("AutoLogin"); }
        }

        //private void SetFieldPage()
        //{
            ////var item = Session.User;
            //if (item != null)
            //{
            //    if (item.Login != string.Empty)
            //    {
            //        Login = item.Login;
            //    }
            //    if (item.FullName != string.Empty)
            //    {
            //        FullName = item.FullName;
            //    }
            //    else
            //    {
            //        FullName = "Користувач не вказав";
            //    }
            //    if (item.TIN != string.Empty)
            //    {
            //        TIN = item.TIN;
            //    }
            //    else
            //    {
            //        TIN = "Користувач не вказав";
            //    }
            //    if (item.KeyPath != string.Empty)
            //    {
            //        var temp = item.KeyPath.Split("\\");
            //        NameKey = temp.ElementAt(temp.Length - 1);
            //    }
            //    else
            //    {
            //        NameKey = "Ключ відсутній";
            //    }
            //    if (Session.User.Status == 0)
            //    {
            //        Status = "Користувач без ключа ЕЦП";
            //    }
            //    else
            //    {
            //        Status = "ОК";
            //    }
            //    if (Session.User.CreatedAt != null)
            //    {
            //        Create_At = Session.User.CreatedAt.Value.Date;
            //    }
            //    AutoLogin = Session.User.AutomaticLogin;
        //    }
        //}

        //public ICommand ChangePasswordCommand => _changePasswordCommand;
        //private void ChangePassword()
        //{

        //}
    }
}
