using LocateWindow;
using ShopProject.Model.Command;
using ShopProject.Model.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage
{
    internal class AuthorizationViewModel : ViewModel<AuthorizationViewModel>
    {
        private AuthorizationModel _model;

        private ICommand _logInCommnad;
        private ICommand _openChangePassowordCommand;

        public AuthorizationViewModel()
        {
            _model = new AuthorizationModel();

            _logInCommnad = new DelegateCommand(LogIn);
            _openChangePassowordCommand = new DelegateCommand(OpenChangePassword);

            _login = string.Empty;
            _password = string.Empty;
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(nameof(Login)); } 
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand LogInCommnad => _logInCommnad;
        private void LogIn()
        {
            if (_model.LogIn(Login, Password))
            {
                Mediator.Notify("VisibleMenu", "");
                MessageBox.Show("Вхід успішно виконано");
            }
        }
        public ICommand OpenChangePasswordCommand => _openChangePassowordCommand;
        private void OpenChangePassword()
        {
            Mediator.Notify("OpenChangePassword", "");
        }

    }
}
