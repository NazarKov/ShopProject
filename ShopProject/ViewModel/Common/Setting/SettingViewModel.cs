using ShopProject.Infrastructure.Mvvm;
using ShopProject.Infrastructure.Mvvm.Interface;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.View.Common.Setting;
using ShopProject.ViewModel.SettingPage;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Setting
{
    internal class SettingViewModel : ViewModel<SettingViewModel> ,IViewModelLoadResourse
    {
        private ICommand _openGeneralSettingOpenCommand;  
        private ICommand _openSettingPrintingCheckCommand;
        private ICommand _openSettingPintingStickerCommand;
        private ICommand _openSettingUserCommand;
        private ICommand _openSettingWebServerCommand;
        private ICommand _openSettingScannerCommand;
        private ICommand _openSettingOperationRecorderCommand;
        private ICommand _openSettingStorageCommand;
        private ICommand _openSettingExpansionCommand;
        public SettingViewModel()
        {  
            _openSettingUserCommand = CreateCommand(() =>{ PageSetting = App.Container.GetViewWithViewModel<SettingProfileView, SettingProfileViewModel>(); });
            _openSettingPrintingCheckCommand = CreateCommand(() => { PageSetting = App.Container.GetViewWithViewModel<SettingPrintingCheckView,SettingPrintingCheckViewModel>(); });
            _openSettingPintingStickerCommand = CreateCommand(() => { PageSetting = App.Container.GetViewWithViewModel<SettingPrintingStickerView, SettingPrintingStickerViewModel>(); }); 
            _openSettingOperationRecorderCommand = CreateCommand(() => { PageSetting = App.Container.GetViewWithViewModel<SettingPointOfSaleView, SettingPointOfSaleViewModel>(); });
            _openSettingStorageCommand = CreateCommand(() => { PageSetting = App.Container.GetViewWithViewModel<SettingDirectoriesView, SettingDirectoriesViewModel>(); }); 
            _pageSetting = new Page();

            _generalShadowVisibility = Visibility.Collapsed;
            MediatorService.AddEventAsync("SettingSnadowSetVissible", async () => { GeneralShadowVisibility = Visibility.Visible; });
            MediatorService.AddEventAsync("SettingSnadowSetCollapsed", async () => { GeneralShadowVisibility = Visibility.Collapsed; });
        }

        private Page _pageSetting;
        public Page PageSetting
        {
            get { return _pageSetting; }
            set { _pageSetting = value; OnPropertyChanged(nameof(PageSetting)); }
        } 
        private Visibility _generalShadowVisibility;
        public Visibility GeneralShadowVisibility
        {
            get { return _generalShadowVisibility; }
            set { _generalShadowVisibility = value; OnPropertyChanged(nameof(GeneralShadowVisibility)); }
        }

        public ICommand GeneralSettingOpenCommand => _openGeneralSettingOpenCommand;  
        public ICommand PrintingCheckCommand => _openSettingPrintingCheckCommand;
        public ICommand StickerSettingOpenCommand => _openSettingPintingStickerCommand;
        public ICommand OpenSettingUserCommand => _openSettingUserCommand;
        public ICommand OpenSettingWebServerCommand => _openSettingWebServerCommand; 
        public ICommand OpenSettingScannerCommand=> _openSettingScannerCommand;
        public ICommand OpenSettingOpenrationRecorderCommand => _openSettingOperationRecorderCommand;
        public ICommand OpenSettingStorageCommand => _openSettingStorageCommand; 
        public ICommand OpenSettingExpansionComman => _openSettingExpansionCommand;

        public Task LoadResourse()
        {
            SafeExecute(() => {
                PageSetting = App.Container.GetViewWithViewModel<SettingProfileView,SettingProfileViewModel>();
            });  
            return Task.CompletedTask;
        }
    }
}
