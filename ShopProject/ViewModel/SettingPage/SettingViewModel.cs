using ShopProject.Model.Command;
using System.Windows.Controls;
using System.Windows.Input;
using ShopProject.Views.SettingPage;


namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingViewModel : ViewModel<SettingViewModel>
    {
        private ICommand _dataBaseSettingOpenCommand;
        private ICommand _generalSettingOpenCommand;
        private ICommand _serviseSingFileOpenCommand;
        private ICommand _deviceSettlementOperationsCommand;
        private ICommand _printingCheckCommand;
        private ICommand _stickerSettingOpenCommand;


        public SettingViewModel()
        {
            _dataBaseSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingDataBase(); });
            _generalSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingGeneral(); });
            _serviseSingFileOpenCommand = new DelegateCommand(() => {PageSetting = new SettingServiseSingingFiles(); });
            _deviceSettlementOperationsCommand = new DelegateCommand(() => {PageSetting = new SettingDeviceSettlementOperations(); });
            _printingCheckCommand = new DelegateCommand(() => { PageSetting = new SettingPrintingCheck(); });
            _stickerSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingSticker(); });
        }

        private Page _pageSetting;
        public Page PageSetting
        {
            get { return _pageSetting; }
            set { _pageSetting = value; OnPropertyChanged("PageSetting"); }
        }


        public ICommand DataBaseSettingOpenCommand => _dataBaseSettingOpenCommand;
        public ICommand GeneralSettingOpenCommand => _generalSettingOpenCommand;
        public ICommand ServiseSingFileOpenCommand => _serviseSingFileOpenCommand;
        public ICommand DeviceSettlementOperationsCommand => _deviceSettlementOperationsCommand;
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        public ICommand StickerSettingOpenCommand => _stickerSettingOpenCommand;
    }
}
