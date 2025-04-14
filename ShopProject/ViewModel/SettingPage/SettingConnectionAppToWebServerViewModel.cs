using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingConnectionAppToWebServerViewModel : ViewModel<SettingConnectionAppToWebServerViewModel>
    {
        private readonly SettingConnectionAppToWebServerModel _model;

        private ICommand _connectUserCommand;
        private ICommand _disconnectUserCommand;

        private ICommand _autoFindURLServerCommand;
        private ICommand _saveSettingCommand;
        private ICommand _chekedConnectionToServerCommand;
        private ICommand _reconnectToServerCommand;

        private string _token;

        public SettingConnectionAppToWebServerViewModel()
        {
            _model = new SettingConnectionAppToWebServerModel();

            _connectUserCommand = new DelegateCommand(ConnectUser);
            _disconnectUserCommand = new DelegateCommand(DisconnectUser);

            _saveSettingCommand = new DelegateCommand(SaveSettings);
            _autoFindURLServerCommand = new DelegateCommand(AutoFindURLServer);
            _chekedConnectionToServerCommand = new DelegateCommand(ChekedConnectionToServer);
            _reconnectToServerCommand = new DelegateCommand(ReconnectToServer);

         //   _userList = new List<UserSettingServer>();
 

            _token = string.Empty;
            _loginUser = string.Empty;
            _passwordUser = string.Empty;
            _passwordAdmin = string.Empty;
            _ipRouter = string.Empty;
            _urlServer = string.Empty;

            SetFieldSettingConnect();
            setFielPage();
        }

        private void SetFieldSettingConnect()
        {

            var port = AppSettingsManager.GetParameterFiles("Port");
            if(port != null)
            {
                _port = (int)port;
            }
            else
            {
                //_port = _model.GetPort();
            }
            var ipRouter = AppSettingsManager.GetParameterFiles("IpRouter");
            if (IpRouter != null)
            {
                _ipRouter = ipRouter.ToString();
            }
            else
            {
               // _ipRouter = _model.GetApiRouter();
            }

            var url = AppSettingsManager.GetParameterFiles("URLWebServer");
            if(url != null)
            {
                _urlServer = url.ToString();
            }
        }

        private async void setFielPage()
        {
            DefaultSettingPage();

            _token = (string)AppSettingsManager.GetParameterFiles("TokenSetting");
            if (_token != null && _token != string.Empty)
            {
                //var user = await _model.GetUser(_token);
                //if (user != null)
                //{
                //    LoginUser = user.Login;
                //    UserList = await _model.GetUsers(_token);
                //    VisibilitiCreateServerUser = Visibility.Collapsed;
                //    VisibilitiConnectServerUser = Visibility.Visible;
                 
                //}
            }

        }
        private void DefaultSettingPage()
        {
            VisibilitiCreateServerUser = Visibility.Visible;
            VisibilitiConnectServerUser = Visibility.Collapsed;
        
            LoginUser = string.Empty;
            PasswordUser = string.Empty;
            PasswordAdmin = string.Empty;
        }

        private string _loginUser;
        public string LoginUser
        {
            get { return _loginUser; }
            set { _loginUser = value; OnPropertyChanged(nameof(LoginUser)); }
        }
        private string _passwordUser;
        public string PasswordUser
        {
            get { return _passwordUser; }
            set { _passwordUser = value; OnPropertyChanged(nameof(PasswordUser)); }
        }
        private string _passwordAdmin;
        public string PasswordAdmin
        {
            get { return _passwordAdmin; }
            set { _passwordAdmin = value; OnPropertyChanged(nameof(PasswordAdmin)); }
        }

        private Visibility _visibilitiCreateServerUser;
        public Visibility VisibilitiCreateServerUser
        {
            get { return _visibilitiCreateServerUser; }
            set { _visibilitiCreateServerUser = value; OnPropertyChanged(nameof(VisibilitiCreateServerUser)); }
        }
        private Visibility _visibilitiConnectServerUser;
        public Visibility VisibilitiConnectServerUser
        {
            get { return _visibilitiConnectServerUser; }
            set { _visibilitiConnectServerUser = value; OnPropertyChanged(nameof(VisibilitiConnectServerUser)); }
        }
        //private List<UserSettingServer> _userList;
        //public List<UserSettingServer> UserList
        //{
        //    get { return _userList; }
        //    set { _userList = value;OnPropertyChanged(nameof(UserList)); }
        //}
        #region SettingConnect
        private string _urlServer;
        public string UrlServer
        {
            get { return _urlServer; }
            set { _urlServer = value; OnPropertyChanged(nameof(UrlServer)); }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; OnPropertyChanged(nameof(Port)); }
        }

        private string _ipRouter;
        public string IpRouter
        {
            get { return _ipRouter; }
            set { _ipRouter = value; OnPropertyChanged(nameof(IpRouter));}
        }


        public ICommand AutoFindURLServerCommand => _autoFindURLServerCommand;
        private void AutoFindURLServer()
        {
            Task.Run(async () =>
            {
                //UrlServer = await  _model.FindIpServer(IpRouter, Port);
            });
        }
        public ICommand SaveSettingCommand => _saveSettingCommand;
        public void SaveSettings()
        {
            AppSettingsManager.SetParameterFile("URLWebServer", UrlServer);
            AppSettingsManager.SetParameterFile("Port", Port);
            AppSettingsManager.SetParameterFile("IpRouter", IpRouter);
            MessageBox.Show("Налаштування збережено");
        }

        public ICommand ChekedConnectionToServerCommand => _chekedConnectionToServerCommand;
        private void ChekedConnectionToServer()
        {
            Task.Run(async () => {

               // var result = await _model.Ping();
                //if(result!=string.Empty)
                //{
                //    MessageBox.Show("Підключення успішне час підключення:" + result);
                //}
            });
        }
        public ICommand ReconectToServerCommand => _reconnectToServerCommand;
        private void ReconnectToServer()
        {
            Task task = Task.Run(async () =>
            {

                _model.Reconnect(UrlServer);
               // var result = await _model.Ping();
                //if (result != string.Empty)
                //{
                //    MessageBox.Show("Підключення успішне час підключення:" + result);
                //}
            });
            task.ContinueWith(t =>
            {
                setFielPage();
            });
        }
        #endregion

        public ICommand ConnectUserCommand => _connectUserCommand;
        public void ConnectUser()
        {
            Task task = Task.Run(async () =>
            {
                if (ChekedField())
                {
                    await _model.ConnectUser(LoginUser, PasswordUser, PasswordAdmin);
                }
            });
            task.ContinueWith(t =>
            {
                setFielPage();
            });
         
        }
        public ICommand DisconnectUserCommand => _disconnectUserCommand;
        public void DisconnectUser()
        {
            Task task = Task.Run(async () =>
            {
                 await _model.DisconnectUser(_token);
            });
            task.ContinueWith(t =>
            {
                setFielPage();
            });
        }

        private bool ChekedField()
        {
            if (LoginUser == string.Empty)
            {
                MessageBox.Show("Заповніть поле Логін користувача");
                return false;
            }
            if (PasswordUser == string.Empty)
            {
                MessageBox.Show("Заповніть поле Пароль користувача");
                return false;
            }
            if (PasswordAdmin == string.Empty)
            {
                MessageBox.Show("Заповніть поле Пароль Admin");
                return false;
            }
            return true;
        }
    }
}
