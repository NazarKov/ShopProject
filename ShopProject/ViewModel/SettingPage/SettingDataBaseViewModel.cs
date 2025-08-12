 
using Org.BouncyCastle.Asn1.Cmp;
using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZXing;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingDataBaseViewModel : ViewModel<SettingDataBaseViewModel>
    {
        private ICommand _connectDataBaseCommand;
        private ICommand _disconnectDataBaseCommand;

        private SettingDataBaseModel _model;

        private string tokenUser;
        private string tokenDataBase;

        public SettingDataBaseViewModel()
        {
            _model = new SettingDataBaseModel();
            _disconnectDataBaseCommand = new DelegateCommand(DiscoonectDataBase);
            _connectDataBaseCommand = new DelegateCommand(ConnectDataBase);

            _nameDataBase = string.Empty;
            _authorizationVisibiliti = Visibility.Visible;
            _settingConnectAndCreateDataBaseVisisbiliti = Visibility.Visible;
            _settingDataBaseVisibiliti = Visibility.Visible;
            _visibilitiConnectionTypeDataBase = Visibility.Collapsed;


            _selectTypeConnect = 0;
            _selectTypeDataBase = 0;
            _heightConnectButtonRow = 0;
            _rowSpanGrid = 7;
            SetSettingPage();
        }

        private async void SetSettingPage()
        {
            tokenUser = AppSettingsManager.GetParameterFiles("TokenSetting").ToString();

            SettingConncectAndCreateDataBaseVisibiliti = Visibility.Visible;
            SettingDataBaseVisibiliti = Visibility.Collapsed;

            RowSpanGrid = 6;
            ConnectDataBaseButtonRow = 5;
            AuthorizationVisibiliti = Visibility.Visible;

            if (tokenUser != null && tokenUser != string.Empty)
            {
                AuthorizationVisibiliti = Visibility.Collapsed;
                RowSpanGrid = 5;
                ConnectDataBaseButtonRow = 4;

            }

            tokenDataBase = AppSettingsManager.GetParameterFiles("TokenDataBase").ToString();
            if (tokenDataBase != null && tokenDataBase != string.Empty)
            {
                //var result = await _model.GetSetting(tokenDataBase);
                //if(result != null)
                //{
                //    NameDataBase = result.ConnectionString.InitialCatalog;
                //    SettingConncectAndCreateDataBaseVisibiliti = Visibility.Collapsed;
                //    SettingDataBaseVisibiliti = Visibility.Visible;
                //    RowSpanGrid = 3;
                //}
            }
           
        }

        private string _nameDataBase;
        public string NameDataBase
        {
            get { return _nameDataBase; }
            set { _nameDataBase = value; OnPropertyChanged(nameof(NameDataBase)); }
        }
        private int _selectTypeConnect;
        public int SelectTypeConnect
        {
            get { return _selectTypeConnect; }
            set { _selectTypeConnect = value; OnPropertyChanged(nameof(SelectTypeConnect)); }
        }

        private Array? _typeConnectDataBase;
        public Array? TypeConnectDataBase
        {
       //     get { return Enum.GetValues(typeof(ConnectionType)); }
            set { _typeConnectDataBase = value; OnPropertyChanged(nameof(TypeConnectDataBase)); }
        }

        private int _selectTypeDataBase;
        public int SelectTypeDataBase
        {
            get { return _selectTypeDataBase; }
            set { _selectTypeDataBase = value;
            ////    if (TypeDataBase.GetValue(value).ToString()=="SQL")
            //    {
            //        VisibilitiConnectionTypeDataBase = Visibility.Visible;
            //        HeightConnectButtonRow = 30;
            //        RowSpanGrid++;
            //    }
            //    else
            //    {
            //        VisibilitiConnectionTypeDataBase = Visibility.Hidden;
            //        HeightConnectButtonRow = 0;
            //        RowSpanGrid--;
            //    }
                OnPropertyChanged(nameof(SelectTypeDataBase)); }
        }

        private Visibility _visibilitiConnectionTypeDataBase;
        public Visibility VisibilitiConnectionTypeDataBase
        {
            get { return _visibilitiConnectionTypeDataBase; }
            set { _visibilitiConnectionTypeDataBase = value; OnPropertyChanged(nameof(VisibilitiConnectionTypeDataBase)); }
        }

        private Array? _typeDataBase;
        public Array? TypeDataBase
        {
         //   get { return Enum.GetValues(typeof(TypeDataBase)); }
            set { _typeDataBase = value; OnPropertyChanged(nameof(TypeDataBase)); }

        }
        private string _passwordDataBase;
        public string PasswordDataBase
        {
            get { return _passwordDataBase; }
            set { _passwordDataBase = value; OnPropertyChanged(nameof(PasswordDataBase)); }
        }
        private string _passwordAdmin;
        public string PasswordAdmin
        {
            get { return _passwordAdmin; }
            set { _passwordAdmin = value; OnPropertyChanged(nameof(PasswordAdmin)); }
        }

        private Visibility _settingDataBaseVisibiliti;
        public Visibility SettingDataBaseVisibiliti
        {
            get { return _settingDataBaseVisibiliti; }
            set { _settingDataBaseVisibiliti = value; OnPropertyChanged(nameof(SettingDataBaseVisibiliti)); }
        }
        private Visibility _settingConnectAndCreateDataBaseVisisbiliti;
        public Visibility SettingConncectAndCreateDataBaseVisibiliti
        {
            get { return _settingConnectAndCreateDataBaseVisisbiliti; }
            set { _settingConnectAndCreateDataBaseVisisbiliti = value; OnPropertyChanged(nameof(SettingConncectAndCreateDataBaseVisibiliti));}
        }

        private Visibility _authorizationVisibiliti;
        public Visibility AuthorizationVisibiliti
        {
            get { return _authorizationVisibiliti; }
            set { _authorizationVisibiliti = value; OnPropertyChanged(nameof(AuthorizationVisibiliti));}
        }
        private int _rowSpanGrid;
        public int RowSpanGrid
        {
            get { return _rowSpanGrid; }
            set { _rowSpanGrid = value; OnPropertyChanged(nameof(RowSpanGrid)); }
        }
        private int _connectDataBaseButtonRow;
        public int ConnectDataBaseButtonRow
        {
            get { return _connectDataBaseButtonRow; }
            set { _connectDataBaseButtonRow = value; OnPropertyChanged(nameof(ConnectDataBaseButtonRow)); }
        }


        private int _heightConnectButtonRow;
        public int HeightConnectButtonRow
        {
            get { return _heightConnectButtonRow; }
            set { _heightConnectButtonRow = value; OnPropertyChanged(nameof(HeightConnectButtonRow)); }
        }


        public ICommand ConnectDataBaseCommand => _connectDataBaseCommand;
        private void ConnectDataBase()
        {
            Task task = Task.Run(async () =>
            {
                if (ChekedField())
                {
                    if (tokenUser != null && tokenUser != string.Empty)
                    {
                   //     await _model.ConnectDataBase(NameDataBase, PasswordDataBase, tokenUser, (ConnectionType)TypeConnectDataBase.GetValue(SelectTypeConnect), (TypeDataBase)TypeDataBase.GetValue(SelectTypeDataBase));
                    }
                    else
                    {
                //        await _model.ConnectDataBase(NameDataBase, PasswordDataBase, PasswordAdmin, (ConnectionType)TypeConnectDataBase.GetValue(SelectTypeConnect),(TypeDataBase)TypeDataBase.GetValue(SelectTypeDataBase));
                    }
                }
            });
            task.ContinueWith(t =>
            {
                SetSettingPage();
            });
        }
        public ICommand DisconnectDataBaseCommand => _disconnectDataBaseCommand;
        private void DiscoonectDataBase()
        {
            Task task = Task.Run(async () =>
            {
             //   await _model.DisconnectDataBase(tokenDataBase);
            });
            task.ContinueWith(t =>
            {
                SetSettingPage();
            });
        }

        private bool ChekedField()
        {
            if (NameDataBase == string.Empty)
            {
                MessageBox.Show("Заповніть поле Назва бази даних");
                return false;
            }
            if (PasswordDataBase == string.Empty)
            {
                MessageBox.Show("Заповніть поле Пароль бази даних");
                return false;
            }
            return true;
        }

    }
}
