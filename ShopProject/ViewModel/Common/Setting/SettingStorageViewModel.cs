using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Modules.Setting.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Setting
{
    internal class SettingStorageViewModel : ViewModel<SettingStorageViewModel> , IViewModelLoadResourse
    { 
        private ICommand _saveSettingCommand;

        private ISettingService _settingService;

        public SettingStorageViewModel(ISettingService settingService)
        {
            _settingService = settingService;  
            _saveSettingCommand = CreateCommand(SaveSetting); 
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);

            return Task.CompletedTask;
        }

        private int _productBarCodeLength;
        public int ProductBarCodeLength
        {
            get { return _productBarCodeLength; }
            set { _productBarCodeLength = value; OnPropertyChanged(nameof(ProductBarCodeLength)); }
        }

        private int _sertificateBarCodeLength;
        public int SertificateBarCodeLength
        {
            get { return _sertificateBarCodeLength; }
            set { _sertificateBarCodeLength = value; OnPropertyChanged(nameof(SertificateBarCodeLength)); }
        }

        private void SetFieldPage()
        {
            var setting = _settingService.GetSetting<StorageSetting>();
            if(setting == null)
            {
                setting = new StorageSetting();
            }
            ProductBarCodeLength = setting.ProductBarCodeLength;
            SertificateBarCodeLength = setting.SertificateBarCodeLength;
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;
        private void SaveSetting()
        {
            _settingService.SetSetting<StorageSetting>(new StorageSetting() { ProductBarCodeLength = ProductBarCodeLength,SertificateBarCodeLength = SertificateBarCodeLength });
            MessageBox.Show("Налаштування збережено"); 
        } 
    }
}
