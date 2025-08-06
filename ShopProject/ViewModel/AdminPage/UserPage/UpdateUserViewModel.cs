using ShopProject.Helpers;
using ShopProject.Model.AdminPage.UserPage;
using ShopProject.Model.Command;
using ShopProjectDataBase.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.UserPage
{
    internal class UpdateUserViewModel : ViewModel<UpdateUserViewModel>
    {
        private UpdateUserModel _model;


        private ICommand _updateUserCommand;
        private ICommand _openPanelWithKeyCommand; 

        private ICommand _openFileKeyCommand;
        private ICommand _clearWindowCommand;
        private ICommand _exitWindowCommand;
         
        private bool _isUserHaveKey;
        private string _nameFile;
        private Guid _id;

        public UpdateUserViewModel()
        {
            _model = new UpdateUserModel();
            
            _login = string.Empty;
            _nameFile = string.Empty;
            _fullName = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _pathKey = string.Empty;
            _passwordKey = string.Empty;
            _userRoles = new List<UserRoleEntity>();
            _messageByKey = string.Empty;
            _contentUpdateKeyButton = string.Empty;

            _updateUserCommand = new DelegateCommand(UpdateUser);
            _openPanelWithKeyCommand = new DelegateCommand(OpenPanelWithKey); 

            _openFileKeyCommand = new DelegateCommand(OpenFileKey);
            _clearWindowCommand = new DelegateCommand(ClearWindow);
            _exitWindowCommand = new DelegateCommand(ExitWindow);
             

            SetFieldPage();
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

        private List<UserRoleEntity> _userRoles;
        public List<UserRoleEntity> UserRoles
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

        private double _sizeWindow;
        public double SizeWindow
        {
            get { return _sizeWindow; }
            set { _sizeWindow = value; OnPropertyChanged(nameof(SizeWindow)); }
        }

        private int _buttonRowIndex;
        public int ButtonRowIndex
        {
            get { return _buttonRowIndex; }
            set { _buttonRowIndex = value; OnPropertyChanged(nameof(ButtonRowIndex)); }
        }

        private Visibility _visibilityFielKey;
        public Visibility VisibilityFielKey
        {
            get { return _visibilityFielKey; }
            set { _visibilityFielKey = value; OnPropertyChanged(nameof(VisibilityFielKey)); }
        }

        private Visibility _visibilityUpdateButtonKey;
        public Visibility VisibilityUpdateButtonKey
        {
            get { return _visibilityUpdateButtonKey; }
            set { _visibilityUpdateButtonKey = value; OnPropertyChanged(nameof(VisibilityUpdateButtonKey)); }
        }

        private string _messageByKey;
        public string MessageByKey
        {
            get { return _messageByKey; }
            set { _messageByKey = value; OnPropertyChanged(nameof(MessageByKey)); }
        }

        private string _contentUpdateKeyButton;
        public string ContentUpdateKeyButton
        {
            get { return _contentUpdateKeyButton; }
            set { _contentUpdateKeyButton = value; OnPropertyChanged(nameof(ContentUpdateKeyButton)); }
        }

        private void SetFieldPage()
        {
            SetButton();
            SetFielComboBoxRole();

            _visibilityFielKey = Visibility.Hidden;
            _sizeWindow = 400;

            var user = Session.UserEntity;

            if (user != null)
            {
                _login = user.Login;
                _fullName = user.FullName;
                _email = user.Email;
                _password = user.Password;
                _id = user.ID;
                if (user.SignatureKey != null)
                {
                    MessageByKey = "";
                    ContentUpdateKeyButton = "Оновити ключ";
                    _isUserHaveKey = true;
                }
                else
                {
                    MessageByKey = "Ключ вісутній";
                    ContentUpdateKeyButton = "Добавити ключ";
                    _isUserHaveKey= false;
                }
            }
        }

        private void SetButton()
        {
            _buttonRowIndex = 7;
        }

        private void SetFielComboBoxRole()
        {
            var item = new List<UserRoleEntity>();
            Task t = Task.Run(async () =>
            {
                item = await _model.GetUserRoles();
            });
            t.ContinueWith(t =>
            {
                if (item.Count > 0)
                {
                    UserRoles = item;
                }
            });
        }

        public ICommand UpdateUserCommand => _updateUserCommand;

        public void UpdateUser()
        {
            Task t = Task.Run(async () => {
                if (_isUserHaveKey)
                {
                    if (await _model.UpdateUserKey(_id,PathKey, _nameFile, Login, Email, Password, PasswordKey, UserRoles.ElementAt(SelectUserRole)))
                    {
                        MessageBox.Show("Корисувача створено");
                        Mediator.Notify("ReloadUser");
                    }
                }
                else
                {
                    if (await _model.UpdateUser(_id,FullName, Login, Email, Password, UserRoles.ElementAt(SelectUserRole)))
                    {
                        MessageBox.Show("Корисувача створено");
                        Mediator.Notify("ReloadUser");
                    }
                }
            });

        }

        public ICommand OpenPanelWithKeyCommand => _openPanelWithKeyCommand;
        private void OpenPanelWithKey()
        {
            ButtonRowIndex = 8;
            SizeWindow = 520;
            VisibilityFielKey = Visibility.Visible;
            VisibilityUpdateButtonKey = Visibility.Hidden;
            _isUserHaveKey = true;
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
        private void ExitWindow()
        {

        }
    }
}
