using ShopProject.Helpers.Command;
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
    internal class SettingStorageViewModel : ViewModel<SettingStorageViewModel>
    {
        private SettingStorageModel _model;
        private ICommand _saveSettingCommand;

        public SettingStorageViewModel()
        {
            _model = new SettingStorageModel();

            _saveSettingCommand = new DelegateCommand(SaveSetting);
            SetFieldPage();
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
            var setting = _model.GetSetting();
            ProductBarCodeLength = setting.ProductBarCodeLength;
            SertificateBarCodeLength = setting.SertificateBarCodeLength; 
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;
        private void SaveSetting()
        {
            if (_model.SaveSetting(ProductBarCodeLength, SertificateBarCodeLength))
            {
                MessageBox.Show("Налаштування збережено");
            }
        }

    }
}
