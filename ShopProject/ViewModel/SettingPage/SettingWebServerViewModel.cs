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
    internal class SettingWebServerViewModel : ViewModel<SettingWebServerViewModel>
    {
        private ICommand _entranceWebServerSettingCommand;
        private ICommand _createDataBaseCommand;
        private ICommand _createUserCommand;
        private ICommand _exitDataBaseInfoCommand;
        private ICommand _exitUserInfoCommand;
        private ICommand _updateSizeGridCommand;



        private ICommand _chekedConnectionWebServerCommand;

        private SettingWebServerModel _model;

        public SettingWebServerViewModel()
        {
            _entranceWebServerSettingCommand = new DelegateCommand(EnrenceWebSetting);
            _createDataBaseCommand = new DelegateCommand(CreateDataBase);
            _createUserCommand = new DelegateCommand(CreateUser);
            _exitDataBaseInfoCommand = new DelegateCommand(ExitDataBaseInfo);
            _exitUserInfoCommand = new DelegateCommand(ExitUserInfo);
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);


            _model = new SettingWebServerModel();
            
            _login = string.Empty;
            _password = string.Empty;
            _nameDataBase = string.Empty;
            _passwordDataBase = string.Empty;

            _nameUser = string.Empty;
            _passwordUser = string.Empty;
            _height = 480;
            _width = 840;

            _selectTypeDataBase = 0;
            _selectConnectTypeDataBase = 0;

            _visibilitiEnranceWebServerSetting = Visibility.Visible;
            _visibilitiSettingWebServer = Visibility.Visible;
            _visibilitiConnectionTypeDataBase = Visibility.Hidden;

            _visibilitiInfoDataBase = Visibility.Hidden;
            _visibilitiInforUser = Visibility.Hidden;


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

        private Visibility _visibilitiEnranceWebServerSetting;
        public Visibility VisibilitiEntranceWebServerSetting
        {
            get { return _visibilitiEnranceWebServerSetting; }
            set { _visibilitiEnranceWebServerSetting = value;OnPropertyChanged(nameof(VisibilitiEntranceWebServerSetting)); }
        }
        private Visibility _visibilitiSettingWebServer;
        public Visibility VisibilitiSettingWebServer
        {
            get { return _visibilitiSettingWebServer; }
            set { _visibilitiSettingWebServer = value;OnPropertyChanged(nameof(VisibilitiSettingWebServer)); }
        }

        private string _nameDataBase;
        public string NameDataBase
        {
            get { return _nameDataBase; }
            set { _nameDataBase = value; OnPropertyChanged(nameof(NameDataBase)); }
        }
        private string _passwordDataBase;
        public string PasswordDataBase
        {
            get { return _passwordDataBase; }
            set { _passwordDataBase = value; OnPropertyChanged(nameof(PasswordDataBase)); }
        }

        private Array? _typeDataBase;
        public Array? TypeDataBase
        {
           // get { return Enum.GetValues(typeof(TypeDataBase)); }
            set { _typeDataBase = value; OnPropertyChanged(nameof(TypeDataBase)); }
        }
        private int _selectTypeDataBase;
        public int SelectTypeDataBase
        {
            get { return _selectTypeDataBase; }
            set { _selectTypeDataBase = value;
            //    VisibilitiConnectionTypeDataBase = TypeDataBase.GetValue(value).ToString() == "SQL" ?   Visibility.Visible : Visibility.Hidden;
                OnPropertyChanged(nameof(SelectTypeDataBase)); }
        }

        private Array? _typeConnectDataBase;
        public Array? TypeConnectDataBase
        {
          //  get { return Enum.GetValues(typeof(ConnectionType)); }
            set { _typeConnectDataBase = value; OnPropertyChanged(nameof(TypeConnectDataBase)); }
        }


        private int _selectConnectTypeDataBase;
        public int SelectConnectTypeDataBase
        {
            get { return _selectConnectTypeDataBase; }
            set { _selectConnectTypeDataBase = value; OnPropertyChanged(nameof(SelectConnectTypeDataBase)); }
        }

        private Visibility _visibilitiConnectionTypeDataBase;
        public Visibility VisibilitiConnectionTypeDataBase
        {
            get { return _visibilitiConnectionTypeDataBase; }
            set { _visibilitiConnectionTypeDataBase = value; OnPropertyChanged(nameof(VisibilitiConnectionTypeDataBase)); }
        }


        private string _nameUser;
        public string NameUser
        {
            get { return _nameUser; }
            set { _nameUser = value; OnPropertyChanged(nameof(NameUser)); }
        }
        private string _passwordUser;
        public string PasswordUser
        {
            get { return _passwordUser; }
            set { _passwordUser = value; OnPropertyChanged(nameof(PasswordUser)); }
        }

        private Visibility _visibilitiInfoDataBase;
        public Visibility VisibilitiInfoDataBase
        {
            get { return _visibilitiInfoDataBase; }
            set { _visibilitiInfoDataBase = value; OnPropertyChanged(nameof(VisibilitiInfoDataBase)); }
        }
        private Visibility _visibilitiInforUser;
        public Visibility VisibilityInforUser
        {
            get { return _visibilitiInforUser; }
            set { _visibilitiInforUser = value; OnPropertyChanged(nameof(VisibilityInforUser)); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged(nameof(Height)); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged(nameof(Width)); }
        }

        public ICommand ChekedConnectionWebServerCommand => _chekedConnectionWebServerCommand;
        private void ChekedConnectionWebServer()
        {
            Task.Run(async () =>
            {
              //  var result = await _model.Ping();
                //if (result != string.Empty)
                //{
                //    MessageBox.Show(result);
                //}
            });
        }

        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Height = (double)(Application.Current.MainWindow.ActualHeight - 80);
            Width = (double)(Application.Current.MainWindow.ActualWidth - 20);
        }


        public ICommand EnranceWebServerSetting => _entranceWebServerSettingCommand;
        private void EnrenceWebSetting()
        {
            VisibilitiEntranceWebServerSetting = Visibility.Collapsed;
            VisibilitiSettingWebServer = Visibility.Visible;
        }

        public ICommand CreateDataBaseCommand => _createDataBaseCommand;
        private void CreateDataBase()
        {

        }

        public ICommand CreateUserCommand => _createUserCommand;
        private void CreateUser()
        {

        }
        public ICommand ExitDataBaseInfoCommand => _exitDataBaseInfoCommand;
        private void ExitDataBaseInfo()
        {
            VisibilitiInfoDataBase = Visibility.Collapsed;
        }
        public ICommand ExitUserInfoCommand => _exitUserInfoCommand;
        private void ExitUserInfo()
        {
            VisibilityInforUser = Visibility.Collapsed;
        }


        
    }
}
