using ShopProject.Helpers;
using ShopProject.Model.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopProject.ViewModel.AdminPage
{
    internal class UserDataViewModel : ViewModel<UserDataViewModel>
    {
        private UserDataModel _model;

        public UserDataViewModel()
        {
            _model = new UserDataModel();
            
            _name = string.Empty;
            _login = string.Empty;
            _tin = string.Empty;
            _role = string.Empty;
            _status = string.Empty;
            _automaticLogin = false;

            new Thread(new ThreadStart(SetFieldWidnow)).Start();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        
        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
        }
        
        private string _tin;
        public string Tin
        {
            get { return _tin; }
            set { _tin = value; OnPropertyChanged("Tin"); }
        }
       
        private string _role;
        public string Role
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged("Role"); }
        }
       
        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }
        
        private bool _automaticLogin;
        public bool AutomaticLogin
        {
            get { return _automaticLogin; }
            set { _automaticLogin = value; OnPropertyChanged("AutomaticLogin"); }
        }
        
        private DateTime _dateCreate;
        public DateTime DateCreate
        {
            get { return _dateCreate; }
            set { _dateCreate = value; OnPropertyChanged("DateCreate"); }
        }

        private void SetFieldWidnow()
        {
            if (Session.UserItem != null)
            {
                var user = _model.GetUser();

                Name = user.FullName;
                Login = user.Login;
                Tin = user.TIN;
                Role = user.UserRole.NameRole;

                if (user.Status == 0)
                {
                    Status = "Користувач без ключа ЕЦП";
                }
                else
                {
                    Status = "Користувач з ключем ЕЦП";
                }

                AutomaticLogin = user.AutomaticLogin;
                DateCreate = DateTime.Parse(user.CreatedAt.ToString());
            }
        }

    }
}
