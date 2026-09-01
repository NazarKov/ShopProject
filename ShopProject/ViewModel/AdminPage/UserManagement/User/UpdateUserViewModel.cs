using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.User;
using ShopProject.Model.UI.UserRole;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Domain.UserRole.Interface;
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.Services.Modules.Mapping.UserRole;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.UserManagement.User
{
    internal class UpdateUserViewModel : ViewModel<UpdateUserViewModel>, IViewModelLoadResourse, IСontrolView
    {  
        private ICommand _updateUserCommand; 

        private ICommand _openFileKeyCommand; 
        private ICommand _exitWindowCommand;
        private ICommand _openAddKeyFieldCommand;
        private ICommand _deleteKeyCommand;

        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;

        private string _nameFile; 

        public UpdateUserViewModel(IUserRoleService userRoleService,IUserService userService)
        {
            _userService = userService;
            _userRoleService = userRoleService;

            _user = new UserModel();
            _nameFile = string.Empty; 
            _pathKey = string.Empty;
            _passwordKey = string.Empty; 
            _userRoles = new List<UserRoleModel>();
            _error = string.Empty;
            _success = string.Empty;

            _updateUserCommand = CreateCommandAsync(UpdateUser);  

            _openFileKeyCommand = CreateCommand(OpenFileKey); 
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
            _openAddKeyFieldCommand = CreateCommand(() => { AddKeyFieldVisibility = Visibility.Visible; UserHasSignatureKeyVisibility = Visibility.Collapsed; UserNoneSignatureKeyVisibility = Visibility.Collapsed; });
            _deleteKeyCommand = CreateCommandAsync(DeleteKey);

            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            _userHasSignatureKeyVisibility = Visibility.Collapsed;
            _userNoneSignatureKeyVisibility = Visibility.Collapsed;
            _addKeyFieldVisibility = Visibility.Collapsed;

        }
        public Action? CloseView { get; set; }

        public async Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
        }

        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(nameof(User)); }
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
        private Visibility _userHasSignatureKeyVisibility;
        public Visibility UserHasSignatureKeyVisibility
        {
            get { return _userHasSignatureKeyVisibility; }
            set { _userHasSignatureKeyVisibility = value; OnPropertyChanged(nameof(UserHasSignatureKeyVisibility)); }
        }
        public Visibility _userNoneSignatureKeyVisibility;
        public Visibility UserNoneSignatureKeyVisibility
        {
            get { return _userNoneSignatureKeyVisibility; }
            set { _userNoneSignatureKeyVisibility = value;OnPropertyChanged(nameof(UserNoneSignatureKeyVisibility)); }
        }
        private Visibility _addKeyFieldVisibility;
        public Visibility AddKeyFieldVisibility
        {
            get { return _addKeyFieldVisibility; }
            set { _addKeyFieldVisibility = value; OnPropertyChanged(nameof(AddKeyFieldVisibility)); }
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



            var user = _userService.GetUpdateUserFromSession();

            if (user != null)
            {
                _user = user.ToUserModel(); 
                switch (user.Status)
                {
                    case ShopProject.Model.Enum.TypeStatusUser.AvailableElectronicKey:
                        {
                            UserHasSignatureKeyVisibility = Visibility.Visible;
                            break;
                        }
                    case ShopProject.Model.Enum.TypeStatusUser.NotAvailableElectronicKey:
                        {
                            UserNoneSignatureKeyVisibility = Visibility.Visible;
                            break;
                        }
                    default:
                        {
                            UserNoneSignatureKeyVisibility = Visibility.Visible;
                            break;
                        }
                } 
            }
        } 

        private void SetFielComboBoxRole()
        {
            UserRoles = new List<UserRoleModel>(_userRoleService.GetFromSession().ToUserRoleModel());
            SelectUserRole = 0;
        }

        public ICommand UpdateUserCommand => _updateUserCommand;

        public async Task UpdateUser()
        {

            var result = await _userService.UpdateUser(User.ToUser(), PathKey, PasswordKey);

            if (result.IsSuccess)
            {
                SetSuccess($"Користувач{result.Data.FullName} редаговано");
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Користувач", "Користувач успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadUser.ToString());

                if (!string.IsNullOrEmpty(PathKey)||!string.IsNullOrEmpty(PasswordKey))
                {
                    UserNoneSignatureKeyVisibility = Visibility.Collapsed;
                    UserHasSignatureKeyVisibility = Visibility.Visible;
                    AddKeyFieldVisibility = Visibility.Collapsed;
                }

            }
            else if (result.IsError)
            {
                SetError(result.ErrorMessage);
            }
            else
            {
                SetError("Невдалося виконати операцію");
            }
            PathKey = string.Empty;
            PasswordKey = string.Empty;
        }
        public ICommand OpenAddKeyFieldCommand => _openAddKeyFieldCommand;
        public ICommand DeleteKeyCommand => _deleteKeyCommand;
        private async Task DeleteKey()
        {  
            var result = await _userService.UpdateUser(User.ToUser(), string.Empty, string.Empty,true);

            if (result.IsSuccess)
            {
                SetSuccess("Ключ ЕЦП видалено");

                UserNoneSignatureKeyVisibility = Visibility.Visible;
                UserHasSignatureKeyVisibility = Visibility.Collapsed;
                AddKeyFieldVisibility = Visibility.Collapsed;
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Користувач", "Користувач успішно редаговано")));
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
            PathKey = string.Empty;
            PasswordKey = string.Empty;
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
        public ICommand ExitWindowCommand => _exitWindowCommand;

        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string message)
        {
            Success = message;
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
