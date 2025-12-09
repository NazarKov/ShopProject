
using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.UserPage;
using ShopProject.UIModel.UserPage;
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
        private ICommand _exitCommand;

        public AuthorizationViewModel()
        {
            _model = new AuthorizationModel();

            _logInCommnad = new DelegateCommand(LogIn);
            _openChangePassowordCommand = new DelegateCommand(OpenChangePassword);
            _exitCommand = new DelegateCommand(Exit);
            _login = string.Empty;
            _password = string.Empty;

            SetFieldPage(); 
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
        private bool _autoLogin;
        public bool AutoLogin
        {
            get { return _autoLogin; }
            set { _autoLogin = value; OnPropertyChanged(nameof(AutoLogin));}
        } 
        private void SetFieldPage()
        {
            var user = Session.User;

            if(user != null)
            {
                AutoLogin = user.AutomaticLogin;
                Login = user.Login;
            }
        }
        public ICommand LogInCommnad => _logInCommnad;
        private void LogIn()
        {
            bool entranse = false;
            Task t = Task.Run(async () =>
            {
                if (await _model.LogIn(Login, Password))
                {
                    _model.Init();
                    entranse = true;
                }
            });
            t.ContinueWith(t => { 
                if(entranse) 
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Вхід успішно виконано");
                        MediatorService.ExecuteEvent(NavigationButton.RedirectToTitleView.ToString()); 
                    });
                }
            });

        }
        public ICommand OpenChangePasswordCommand => _openChangePassowordCommand;
        private void OpenChangePassword()
        {
            MediatorService.ExecuteEvent(NavigationButton.RedirectToChangePassword.ToString()); 
        }
        public ICommand ExitCommand=> _exitCommand;
        private void Exit()
        {
            MediatorService.ExecuteEvent(NavigationButton.ExitApp.ToString());
        }
    }
}
