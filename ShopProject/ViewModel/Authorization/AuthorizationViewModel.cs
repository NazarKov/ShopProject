using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Core.Mvvm.Service;
using ShopProject.Model.Navigation;
using ShopProject.Services.Modules.ModelService.User.Interface;
using ShopProject.Services.Modules.Session;
using ShopProject.Services.Modules.Session.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.Authorization
{
    internal class AuthorizationViewModel : ViewModel<AuthorizationViewModel>, IViewModelLoadResourse
    {
        private ICommand _logInCommnad;
        private ICommand _openChangePassowordCommand;
        private ICommand _exitCommand;

        private IUserServise _userServise;
        private ISessionService _sessionService;

        public AuthorizationViewModel(IUserServise userServise , ISessionService sessionService)
        {
            _userServise = userServise;
            _sessionService = sessionService;
            
            _logInCommnad = CreateCommandAsync(LogIn,SetError);
            _openChangePassowordCommand = CreateCommand(OpenChangePassword);
            _exitCommand = CreateCommand(Exit);
            _login = string.Empty;
            _password = string.Empty;

            _error = string.Empty;
            _success = string.Empty;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            _messegeBlockVisibiliti = Visibility.Collapsed;
        } 

        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);

            return Task.CompletedTask;
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
            set { _autoLogin = value; OnPropertyChanged(nameof(AutoLogin)); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }

        private string _success;
        public string Success
        {
            get { return _success; }
            set { _success = value; OnPropertyChanged(nameof(Success)); }
        }

        private Visibility _successTextBlockVisibiliti;
        public Visibility SuccessTextBlockVisibiliti
        {
            get { return _successTextBlockVisibiliti; }
            set { _successTextBlockVisibiliti = value; OnPropertyChanged(nameof(SuccessTextBlockVisibiliti)); }
        }

        private Visibility _errorTextBlockVisibiliti;
        public Visibility ErrorTextBlockVisibiliti
        {
            get { return _errorTextBlockVisibiliti; }
            set { _errorTextBlockVisibiliti = value; OnPropertyChanged(nameof(ErrorTextBlockVisibiliti)); }
        }

        private Visibility _messegeBlockVisibiliti;
        public Visibility MessegeBlockVisibiliti
        {
            get { return _messegeBlockVisibiliti; }
            set { _messegeBlockVisibiliti = value; OnPropertyChanged(nameof(MessegeBlockVisibiliti)); }
        }

        private void SetFieldPage()
        {
            MessegeBlockVisibiliti = Visibility.Collapsed;
            if (_sessionService.CheckingSession())
            {
                MediatorService.ExecuteNavigation(NavigationButton.RedirectToTitleView);
            } 
        }
        public ICommand LogInCommnad => _logInCommnad;
        private async Task LogIn()
        {
            if (await _userServise.LogIn(Login, Password))
            {
                Success = "Авторизація успішно";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible;
                MediatorService.ExecuteNavigation(NavigationButton.RedirectToDashBoadPage); 
            }
            else
            {
                SetError("Невдалося авторизуватися");
            }
        }
        public ICommand OpenChangePasswordCommand => _openChangePassowordCommand;
        private void OpenChangePassword()
        {
            MediatorService.ExecuteNavigation(NavigationButton.RedirectToChangePassword);
        }
        public ICommand ExitCommand => _exitCommand;
        private void Exit()
        {
            MediatorService.ExecuteNavigation(NavigationButton.ExitApp);
        }

        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
            MessegeBlockVisibiliti = Visibility.Visible;
        }
    }
}
