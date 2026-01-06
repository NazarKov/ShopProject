using ShopProject.Model.Command;
using System.Windows.Controls;
using System.Windows.Input;
using ShopProject.Views.SettingPage;
using ShopProject.View.AdminPage.WebServer;
using ShopProject.View.SettingPage;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingViewModel : ViewModel<SettingViewModel>
    {
        private ICommand _openGeneralSettingOpenCommand;  
        private ICommand _openSettingPrintingCheckCommand;
        private ICommand _openSettingPintingStickerCommand;
        private ICommand _openSettingUserCommand;
        private ICommand _openSettingWebServerCommand;
        private ICommand _openSettingScannerCommand;
        public SettingViewModel()
        { 
            _openGeneralSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingGeneral(); }); 
            _openSettingPrintingCheckCommand = new DelegateCommand(() => { PageSetting = new SettingPrintingCheckView(); });
            _openSettingPintingStickerCommand = new DelegateCommand(() => { PageSetting = new SettingPrintingStickerView(); });
            _openSettingUserCommand = new DelegateCommand(() => { PageSetting = new SettingUser(); });
            _openSettingWebServerCommand = new DelegateCommand(() => { PageSetting = new SettingWebServerView(); });
            _openSettingScannerCommand = new DelegateCommand(() => { PageSetting = new SettingScannerView(); });
            PageSetting = new SettingUser();
        }

        private Page _pageSetting;
        public Page PageSetting
        {
            get { return _pageSetting; }
            set { _pageSetting = value; OnPropertyChanged(nameof(PageSetting)); }
        }


        public ICommand GeneralSettingOpenCommand => _openGeneralSettingOpenCommand;  
        public ICommand PrintingCheckCommand => _openSettingPrintingCheckCommand;
        public ICommand StickerSettingOpenCommand => _openSettingPintingStickerCommand;
        public ICommand OpenSettingUserCommand => _openSettingUserCommand;
        public ICommand OpenSettingWebServerCommand => _openSettingWebServerCommand; 
        public ICommand OpenSettingScannerCommand=> _openSettingScannerCommand;
    }
}
