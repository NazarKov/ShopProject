using ShopProject.Model.AdminPage.UserPage;
using ShopProject.Helpers.Command;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;   
using ShopProject.UIModel.UserPage;
using ShopProject.Helpers;

namespace ShopProject.ViewModel.AdminPage.UserPage
{
    internal class CreateUserViewModel : ViewModel<CreateUserViewModel>
    {

        private ICommand _createUserCommand;
        private ICommand _openPanelWithKeyCommand;
        private ICommand _openPanelWithoutKeyCommand;

        private ICommand _openFileKeyCommand;
        private ICommand _clearWindowCommand;
        private ICommand _exitWindowCommand;

        private CreateUserModel _model;
        private bool _isUserHaveKey;
        private string _nameFile;

        public CreateUserViewModel()
        {
            _login = string.Empty;
            _nameFile = string.Empty;
            _fullName = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _pathKey = string.Empty;
            _passwordKey = string.Empty;
            _userRoles = new List<UserRole>();
            _backgroundButtonWihtKey = Brushes.LightGray;
            _backgroundButtonWithoutKey = Brushes.LightGray;

            _model = new CreateUserModel();

            _createUserCommand = new DelegateCommand(CreateUser);
            _openPanelWithKeyCommand = new DelegateCommand(OpenPanelWithKey);
            _openPanelWithoutKeyCommand = new DelegateCommand(OpenPanelWithoutKey);


            _openFileKeyCommand = new DelegateCommand(OpenFileKey);
            _clearWindowCommand = new DelegateCommand(ClearWindow);
            _exitWindowCommand = new DelegateCommand(ExitWindow);

            _isUserHaveKey = false;

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

        private List<UserRole> _userRoles;
        public List<UserRole> UserRoles
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

        private Brush _backgroundButtonWihtKey;
        public Brush BackgroundButtonWihtKey
        {
            get { return _backgroundButtonWihtKey; }
            set { _backgroundButtonWihtKey = value; OnPropertyChanged(nameof(BackgroundButtonWihtKey)); }
        }

        private Brush _backgroundButtonWithoutKey;
        public Brush BackgroundButtonWithoutKey
        {
            get { return _backgroundButtonWithoutKey; }
            set { _backgroundButtonWithoutKey = value; OnPropertyChanged(nameof(BackgroundButtonWithoutKey)); }
        }

        private void SetFieldPage()
        {
            SetButton();
            SetFielComboBoxRole();

            _visibilityFielKey = Visibility.Hidden;
            _sizeWindow = 400;
        }

        private void SetButton()
        {
            _backgroundButtonWithoutKey = Brushes.CadetBlue;
            _backgroundButtonWihtKey = Brushes.LightGray;

            _buttonRowIndex = 6;
        }

        private void SetFielComboBoxRole()
        {
            if (Session.Roles != null)
            {
                UserRoles = Session.Roles.ToList();
            }
        }

        public ICommand CreateUserCommand => _createUserCommand;

        public void CreateUser()
        {
            Task t = Task.Run(async () => {
                if (_isUserHaveKey)
                {
                    if (await _model.CreateUserKey(PathKey, _nameFile, Login, Email, Password, PasswordKey, UserRoles.ElementAt(SelectUserRole)))
                    {
                        MessageBox.Show("Корисувача створено");
                    }
                }
                else
                {
                    if (await _model.CreateUser(FullName, Login, Email, Password, UserRoles.ElementAt(SelectUserRole)))
                    {
                        MessageBox.Show("Корисувача створено");
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
            BackgroundButtonWithoutKey = Brushes.LightGray;
            BackgroundButtonWihtKey = Brushes.CadetBlue;
            _isUserHaveKey = true;
        }

        public ICommand OpenPanelWithoutKeyCommand => _openPanelWithoutKeyCommand;
        private void OpenPanelWithoutKey()
        {
            ButtonRowIndex = 6;
            SizeWindow = 400;
            VisibilityFielKey = Visibility.Hidden;
            BackgroundButtonWithoutKey = Brushes.CadetBlue;
            BackgroundButtonWihtKey = Brushes.LightGray;
            _isUserHaveKey = false;
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
