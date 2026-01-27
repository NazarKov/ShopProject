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
    internal class SettingOperationRecorderViewModel : ViewModel<SettingOperationRecorderViewModel>
    {
        private SettingOperationRecorderModel _model;

        private ICommand _saveSettingCommand;

        public SettingOperationRecorderViewModel()
        {
            _model = new SettingOperationRecorderModel();

            _saveSettingCommand = new DelegateCommand(SaveSetting);
            SetFieldPage();
        }

        private bool _isTestMode;
        public bool IsTestMode
        {
            get { return _isTestMode; }
            set { _isTestMode = value; OnPropertyChanged(nameof(IsTestMode)); }
        }
        private string _deleteBarCode;
        public string DeleteBarCode
        {
            get { return _deleteBarCode; }
            set { _deleteBarCode = value; OnPropertyChanged(nameof(DeleteBarCode)); }
        }

        private void SetFieldPage()
        {
            var setting = _model.GetSetting(); 
            _isTestMode = setting.IsTestMode; 
            _deleteBarCode = setting.DeleteBarCode;
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;
        private void SaveSetting()
        {
            if (_model.SaveSetting(IsTestMode,DeleteBarCode))
            {
                MessageBox.Show("Налаштування збережено");
            }
        }
    }
}
