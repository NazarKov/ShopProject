using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Setting
{
    internal class SettingProfileViewModel : ViewModel<SettingProfileViewModel>, IViewModelLoadResourse
    {
        private ICommand _changePasswordCommand;
        private ISettingService _settingService;
        public SettingProfileViewModel(ISettingService settingService)
        {

            _settingService = settingService;

            _login = string.Empty;
            _fullName = string.Empty;
            _tin = string.Empty;
            _nameKey = string.Empty;
            _status = string.Empty;
            _create_at = new DateTime();
            _autoLogin = false;
             
        }

        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            return Task.CompletedTask;
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
        private string _tin;
        public string TIN
        {
            get { return _tin; }
            set { _tin = value; OnPropertyChanged(nameof(TIN)); }
        }
        private string _nameKey;
        public string NameKey
        {
            get { return _nameKey; }
            set { _nameKey = value; OnPropertyChanged(nameof(NameKey)); }
        }
        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private DateTime _create_at;
        public DateTime Create_At
        {
            get { return _create_at; }
            set { _create_at = value; OnPropertyChanged(nameof(Create_At)); }
        }
        private bool _autoLogin;
        public bool AutoLogin
        {
            get { return _autoLogin; }
            set { _autoLogin = value; OnPropertyChanged(nameof(AutoLogin)); }
        }


        private void SetFieldPage()
        {
            var item = _settingService.GetSetting<SessionSetting>().User;
            if (item != null)
            {
                if (item.Login != string.Empty)
                {
                    Login = item.Login;
                }
                if (item.FullName != string.Empty)
                {
                    FullName = item.FullName;
                }
                else
                {
                    FullName = "Користувач не вказав";
                }
                if (item.TIN != string.Empty)
                {
                    TIN = item.TIN;
                }
                else
                {
                    TIN = "Користувач не вказав";
                }
                if (false)
                {
                    //var temp = item.KeyPath.Split("\\");
                    //NameKey = temp.ElementAt(temp.Length - 1);
                }
                else
                {
                    NameKey = "Ключ відсутній";
                }
                if (item.Status == 0)
                {
                    Status = "Користувач без ключа ЕЦП";
                }
                else
                {
                    Status = "ОК";
                }
                if (item.CreatedAt != null)
                {
                    Create_At = item.CreatedAt.Value.Date;
                }
                AutoLogin = item.AutomaticLogin;
            }
        }

        //public ICommand ChangePasswordCommand => _changePasswordCommand;
        //private void ChangePassword()
        //{

        //}
    }
}
