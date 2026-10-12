using ShopProject.Infrastructure.Mvvm;
using ShopProject.Infrastructure.Mvvm.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.User;
using ShopProject.Model.UI.UserRole;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Domain.UserRole.Interface;
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.Services.Modules.Mapping.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Setting
{
    internal class SettingProfileViewModel : ViewModel<SettingProfileViewModel>, IViewModelLoadResourse
    {
        private ICommand _updateUserCommand;
        private ICommand _saveUserCommand;
        private ICommand _changePasswordCommand;
        private ICommand _savePasswordCommand;
        private IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMessageBoxControlService _messageBoxControlService;
        public SettingProfileViewModel(IUserService userService, IUserRoleService userRoleService, IMessageBoxControlService messageBoxControlService)
        {

            _updateUserCommand = CreateCommand(UpdateUser);
            _saveUserCommand = CreateCommandAsync(SaveUser);
            _changePasswordCommand = CreateCommand(ChangePassword);
            _savePasswordCommand = CreateCommandAsync(SavePassword);
            _userService = userService;
            _userRoleService = userRoleService;
            _messageBoxControlService = messageBoxControlService;
            _user = new UserModel();
            _userRoles = new List<UserRoleModel>();
            _isEnableUpdateField = false;
            _saveButtonVisibiliti = Visibility.Collapsed;
            _selectUserRole = 0;
            _buttonChangePasswordVisibiliti = Visibility.Visible;
            _panelChangePasswordVisibiliti = Visibility.Collapsed;
        }

        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            return Task.CompletedTask;
        }

        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(nameof(User)); }
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

        private bool _isEnableUpdateField;
        public bool IsEnabelUpdateField
        {
            get { return _isEnableUpdateField; }
            set { _isEnableUpdateField = value; OnPropertyChanged(nameof(IsEnabelUpdateField)); }
        }

        private Visibility _saveButtonVisibiliti;
        public Visibility SaveButtonVisibiliti
        {
            get { return _saveButtonVisibiliti; }
            set { _saveButtonVisibiliti = value; OnPropertyChanged(nameof(SaveButtonVisibiliti)); }
        }

        private string _userPassword;
        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; OnPropertyChanged(nameof(UserPassword)); }
        }

        private Visibility _panelChangePasswordVisibiliti;
        public Visibility PanelChangePasswordVisibiliti
        {
            get { return _panelChangePasswordVisibiliti; }
            set { _panelChangePasswordVisibiliti = value; OnPropertyChanged(nameof(PanelChangePasswordVisibiliti)); }
        }
        private Visibility _buttonChangePasswordVisibiliti;
        public Visibility ButtonChangePasswordVisibiliti
        {
            get { return _buttonChangePasswordVisibiliti; }
            set { _buttonChangePasswordVisibiliti = value; OnPropertyChanged(nameof(ButtonChangePasswordVisibiliti)); }
        }


        private void SetFieldPage()
        {
            User = _userService.GetUserFromSession().ToUserModel();
            SetFielComboBoxRole();
        }

        private void SetFielComboBoxRole()
        {
            UserRoles = new List<UserRoleModel>(_userRoleService.GetFromSession().ToUserRoleModel());
            SelectUserRole = 0;
        }

        public ICommand UpdateUserCommand => _updateUserCommand;
        private void UpdateUser()
        {
            IsEnabelUpdateField = true;
            SaveButtonVisibiliti = Visibility.Visible;
        }
        public ICommand SaveUserCommand => _saveUserCommand;
        private async Task SaveUser()
        {
            var result = await _userService.UpdateUser(User.ToUser(), string.Empty, string.Empty);

            if (result.IsSuccess)
            {
                await _messageBoxControlService.Show($"Користувач {result.Data.FullName} редаговано", "Success", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Success, "SettingSnadow");
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Користувач", "Користувач успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadUser.ToString());
                await _userService.GetCurrentUserFromServer();
            }
            else if (result.IsError)
            {
                await _messageBoxControlService.Show(result.ErrorMessage, "Error", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Error, "SettingSnadow");

            }
            else
            {
                await _messageBoxControlService.Show("Невдалося виконати операцію", "Error", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Error, "SettingSnadow");
            }
            IsEnabelUpdateField = false;
            SaveButtonVisibiliti = Visibility.Collapsed;
        }

        public ICommand ChangePasswordCommand => _changePasswordCommand;
        private void ChangePassword()
        {
            PanelChangePasswordVisibiliti = Visibility.Visible;
            ButtonChangePasswordVisibiliti = Visibility.Collapsed;
        }

        public ICommand SavePasswordCommand => _savePasswordCommand;
        private async Task SavePassword()
        {
            if(string.IsNullOrEmpty(UserPassword))
            {
                await _messageBoxControlService.Show("Поле пароль пусте", "Error", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Warning, "SettingSnadow");
                return;
            }

            var result = await _userService.UpdateParameter(nameof(User.Password),UserPassword,User.ToUser());


            if (result.IsSuccess)
            {
                await _messageBoxControlService.Show($"Пароль збережено", "Success", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Success, "SettingSnadow");
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Користувач", "Користувач успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadUser.ToString());
                await _userService.GetCurrentUserFromServer();
            }
            else if (result.IsError)
            {
                await _messageBoxControlService.Show(result.ErrorMessage, "Error", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Error, "SettingSnadow");

            }
            else
            {
                await _messageBoxControlService.Show("Невдалося виконати операцію", "Error", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Error, "SettingSnadow");
            }
            UserPassword = string.Empty;
            PanelChangePasswordVisibiliti = Visibility.Collapsed;
            ButtonChangePasswordVisibiliti = Visibility.Visible; 
        }
    }
}
