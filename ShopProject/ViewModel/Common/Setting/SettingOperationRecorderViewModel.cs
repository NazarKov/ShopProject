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
    internal class SettingOperationRecorderViewModel : ViewModel<SettingOperationRecorderViewModel> , IViewModelLoadResourse
    { 

        private ICommand _saveSettingCommand;

        private ISettingService _settingService;

        public SettingOperationRecorderViewModel(ISettingService settingService)
        {
            _settingService = settingService;
            _isTestMode = true;
            _deleteBarCode = string.Empty;

            _saveSettingCommand = CreateCommand(SaveSetting); 
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);
            return Task.CompletedTask;
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
            var setting = _settingService.GetSetting<OperationRecorderSetting>();
            if(setting == null)
            { 
                return;
            }

            _isTestMode = setting.IsTestMode;
            _deleteBarCode = setting.DeleteBarCode;
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;
        private void SaveSetting()
        {
            _settingService.SetSetting<OperationRecorderSetting>(new OperationRecorderSetting() 
            {
                DeleteBarCode = _deleteBarCode,
                IsTestMode = _isTestMode 
            });

            MessageBox.Show("Налаштування збережено");
        } 
    }
}
