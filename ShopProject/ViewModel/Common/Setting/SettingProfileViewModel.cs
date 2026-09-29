using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
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
        //private ICommand _changePasswordCommand; 
        private ICommand _updateUserCommand;
        private ICommand _saveUserCommand;
        private IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMessageBoxControlService _messageBoxControlService;
        public SettingProfileViewModel(IUserService userService, IUserRoleService userRoleService, IMessageBoxControlService messageBoxControlService)
        {

            _updateUserCommand = CreateCommand(UpdateUser);
            _saveUserCommand = CreateCommandAsync(SaveUser);
            _userService = userService;
            _userRoleService = userRoleService;
            _messageBoxControlService = messageBoxControlService;
            _user = new UserModel();
            _userRoles = new List<UserRoleModel>();
            _isEnableUpdateField = false;
            _saveButtonVisibiliti = Visibility.Collapsed;
            _selectUserRole = 0;
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
            set { _isEnableUpdateField = value;OnPropertyChanged(nameof(IsEnabelUpdateField)); }
        }

        private Visibility _saveButtonVisibiliti;
        public Visibility SaveButtonVisibiliti
        {
            get { return _saveButtonVisibiliti; }
            set { _saveButtonVisibiliti = value;OnPropertyChanged(nameof(SaveButtonVisibiliti)); }
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
            var result = await _userService.UpdateUser(User.ToUser(), string.Empty,string.Empty);

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

        //public ICommand ChangePasswordCommand => _changePasswordCommand;
        //private void ChangePassword()
        //{

        //}
    }
}
