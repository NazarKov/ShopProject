using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.UserRole;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Domain.UserRole.Interface;
using ShopProject.Services.Modules.Mapping.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input; 

namespace ShopProject.ViewModel.AdminPage.UserPage
{
    internal class CreateUserViewModel : ViewModel<CreateUserViewModel>, IViewModelLoadResourse, IСontrolView
    {

        private ICommand _createUserCommand;  
        private ICommand _openFileKeyCommand;
        private ICommand _clearWindowCommand;
        private ICommand _exitWindowCommand;

        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
          
        private string _nameFile;

        public CreateUserViewModel(IUserRoleService userRoleService, IUserService userService)
        {
            _userRoleService = userRoleService;
            _userService = userService;
            _login = string.Empty;
            _nameFile = string.Empty;
            _fullName = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _pathKey = string.Empty;
            _passwordKey = string.Empty;
            _error = string.Empty;
            _success = string.Empty;
            _userRoles = new List<UserRoleModel>(); 
              
            _createUserCommand = CreateCommandAsync(CreateUser); 


            _openFileKeyCommand = CreateCommand(OpenFileKey);
            _clearWindowCommand = CreateCommand(ClearWindow);
            _exitWindowCommand = CreateCommand(()=> { CloseView?.Invoke(); });

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;

            SetFieldPage();
        }
        public Action? CloseView { get; set; }

        public async Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        
        private string _pathKey;
        public string PathKey
        {
            get { return _pathKey; }
            set { _pathKey = value; OnPropertyChanged(nameof(PathKey)); }
        }
        private string _passwordKey;
        public string PasswordKey
        {
            get { return _passwordKey; }
            set { _passwordKey = value; OnPropertyChanged(nameof(PasswordKey)); }
        }

        private List<UserRoleModel> _userRoles;
        public List<UserRoleModel> UserRoles
        {
            get { return _userRoles; }
            set { _userRoles = value; OnPropertyChanged(nameof(UserRoles)); }
        }
        private int _selectUserRole;
        public int SelectUserRole
        {
            get { return _selectUserRole; }
            set { _selectUserRole = value; OnPropertyChanged(nameof(SelectUserRole)); }
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

        private void SetFieldPage()
        { 
            SetFielComboBoxRole(); 
        } 
        private void SetFielComboBoxRole()
        {
            UserRoles = new List<UserRoleModel>(_userRoleService.GetFromSession().ToUserRoleModel());
            SelectUserRole = 0;
        }

        public ICommand CreateUserCommand => _createUserCommand;

        public async Task CreateUser()
        {  
            var result = await _userService.CreateUser(Login, Email, FullName, Password, PathKey, PasswordKey, UserRoles.ElementAt(SelectUserRole).ToUserRole());
            if (result.IsSuccess)
            {
                SetSuccess(result.Data.FullName);
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Користувач", "Користувач успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadUser.ToString());

            }
            else if (result.IsError)
            {
                SetError(result.ErrorMessage);
            }
            else
            {
                SetError("Невдалося виконати операцію");
            } 

        } 
        public ICommand OpenFiLeKeyCommand => _openFileKeyCommand;
        private void OpenFileKey()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathKey = openFileDialog.FileName;
                _nameFile = openFileDialog.SafeFileName;
            } 
        }
        public ICommand ClearWindowCommadn => _clearWindowCommand;
        private void ClearWindow()
        {
            Login = string.Empty;
            FullName = string.Empty;
            Password = string.Empty;
            PasswordKey = string.Empty;
            PathKey = string.Empty; 
            SelectUserRole = 0;
        }
        public ICommand ExitWindowCommand => _exitWindowCommand;

        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string name)
        {
            Success = $"Користувач {name} створений";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
