using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage
{
    class CreateUserViewModel :ViewModel<CreateUserViewModel>
    {
        private CreateUserModel _model;

        private ICommand _createUserCommand;
        private ICommand _openFileKeyCommand;
        private ICommand _clearWindowCommand;
        private ICommand _exitWindowCommand;

        private string _nameFile;

        public CreateUserViewModel()
        {
            _model = new CreateUserModel();

            _userRoles = new List<string>();
            _login = string.Empty;
            _fullName = string.Empty;
            _password = string.Empty;
            _checkedIsKey = true;
            _pathKey = string.Empty;
            _passwordKey = string.Empty;
            _nameFile = string.Empty;

            _createUserCommand = new DelegateCommand(CreateUser);
            _openFileKeyCommand = new DelegateCommand(OpenFileKey);
            _clearWindowCommand = new DelegateCommand(ClearWindow);
            _exitWindowCommand = new DelegateCommand(ExitWindow);


            new Thread(new ThreadStart(SetFieldPage)).Start();
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
        }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set{ _fullName = value; OnPropertyChanged("FullName"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password");}
        }
        private bool _checkedIsKey;
        public bool CheckIsKey
        {
            get { return _checkedIsKey; }
            set { _checkedIsKey = value; OnPropertyChanged("CheckIsKey"); }
        }

        private string _pathKey;
        public string PathKey
        {
            get { return _pathKey; }
            set { _pathKey = value; OnPropertyChanged("PathKey"); }
        }
        private string _passwordKey;
        public string PasswordKey
        {
            get { return _passwordKey; }
            set { _passwordKey = value; OnPropertyChanged("PasswordKey"); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }

        private List<string> _userRoles;
        public List<string> UserRoles
        {
            get { return _userRoles; }
            set { _userRoles = value; OnPropertyChanged("UserRoles"); }
        }
        private int _selectUserRole;
        public int SelectUserRole
        {
            get { return _selectUserRole; }
            set { _selectUserRole = value; OnPropertyChanged("SelectUserRole"); }
        }

        private void SetFieldPage()
        {
            UserRoles = _model.GetUserRoles();
            SelectUserRole = 0;
            CheckIsKey = true;
        }
        public ICommand CreateUserCommand => _createUserCommand;
        private async void CreateUser()
        {
            if(CheckIsKey)
            {
                if(await _model.CreateUserKey(PathKey,_nameFile,Login,Email,Password,PasswordKey,UserRoles.ElementAt(SelectUserRole)))
                {
                    MessageBox.Show("Корисувача створено");
                }
            }
            else
            {
                _model.CreateUser(FullName,Login,Email,Password,UserRoles.ElementAt(SelectUserRole));
            }

        }
        public ICommand OpenFiLeKeyCommand => _openFileKeyCommand;
        private void OpenFileKey()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog()== DialogResult.OK)
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
            CheckIsKey = true;
            SelectUserRole = 0;
        }
        public ICommand ExitWindowCommand => _exitWindowCommand;
        private void ExitWindow()
        {
            
        }
    }
}
