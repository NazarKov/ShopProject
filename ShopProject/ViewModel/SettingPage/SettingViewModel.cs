using ShopProject.Model.Command;
using System.Windows.Controls;
using System.Windows.Input;
using ShopProject.Views.SettingPage;
using ShopProject.View.SettingPage;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingViewModel : ViewModel<SettingViewModel>
    {
        private ICommand _generalSettingOpenCommand;
        private ICommand _serviseSingFileOpenCommand;
        private ICommand _deviceSettlementOperationsCommand;
        private ICommand _printingCheckCommand;
        private ICommand _stickerSettingOpenCommand;
        private ICommand _openSettingUserCommand;
        private ICommand _openSettingWebServerCommand;
        private ICommand _opendataBaseSettingCommand;

        public SettingViewModel()
        {
            _opendataBaseSettingCommand = new DelegateCommand(() => { PageSetting = new SettingDataBaseView(); });
            _generalSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingGeneral(); });
            _serviseSingFileOpenCommand = new DelegateCommand(() => {PageSetting = new SettingServiseSingingFiles(); });
            _deviceSettlementOperationsCommand = new DelegateCommand(() => {PageSetting = new SettingDeviceSettlementOperations(); });
            _printingCheckCommand = new DelegateCommand(() => { PageSetting = new SettingPrintingCheck(); });
            _stickerSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingSticker(); });
            _openSettingUserCommand = new DelegateCommand(() => { PageSetting = new SettingUser(); });
            _openSettingWebServerCommand = new DelegateCommand(() => { PageSetting = new SettingConnectionAppToWebServerView(); });

            PageSetting = new SettingUser();
        }

        private Page _pageSetting;
        public Page PageSetting
        {
            get { return _pageSetting; }
            set { _pageSetting = value; OnPropertyChanged("PageSetting"); }
        }


        public ICommand GeneralSettingOpenCommand => _generalSettingOpenCommand;
        public ICommand ServiseSingFileOpenCommand => _serviseSingFileOpenCommand;
        public ICommand DeviceSettlementOperationsCommand => _deviceSettlementOperationsCommand;
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        public ICommand StickerSettingOpenCommand => _stickerSettingOpenCommand;
        public ICommand OpenSettingUserCommand => _openSettingUserCommand;
        public ICommand OpenSettingWebServerCommand => _openSettingWebServerCommand;
        public ICommand OpenDataBaseSettingCommand => _opendataBaseSettingCommand;
    }
}
