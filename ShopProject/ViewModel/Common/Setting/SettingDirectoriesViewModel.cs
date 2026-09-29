using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Core.Mvvm; 
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Setting;
using ShopProject.Services.Modules.Control;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Setting.Interface; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Setting
{
    internal class SettingDirectoriesViewModel : ViewModel<SettingDirectoriesViewModel> , IViewModelLoadResourse
    { 
        private ICommand _saveSettingCommand;

        private ISettingService _settingService;
        private readonly IMessageBoxControlService _messageBoxControlService;

        public SettingDirectoriesViewModel(ISettingService settingService, IMessageBoxControlService messageBoxControlService)
        {
            _settingService = settingService;  
            _messageBoxControlService = messageBoxControlService;
            _saveSettingCommand = CreateCommand(SaveSetting); 
            
            _storageSetting = new StorageSetting();
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);

            return Task.CompletedTask;
        }

        private StorageSetting _storageSetting;
        public StorageSetting StorageSetting { get { return _storageSetting; }  set { _storageSetting = value; OnPropertyChanged(nameof(StorageSetting)); } }

        private void SetFieldPage()
        {
            var setting = _settingService.GetSetting<StorageSetting>();
            if(setting == null)
            {
                setting = new StorageSetting();
            }
            StorageSetting = setting; 
        }

        public ICommand SaveSettingCommand => _saveSettingCommand;
        private void SaveSetting()
        {
            _settingService.SetSetting<StorageSetting>(StorageSetting);
            _messageBoxControlService.Show("Налаштування збережено","Успішно",MessageBoxType.Success, "SettingSnadow"); 
        } 
    }
}
