using System.Windows.Controls;
using System.Windows.Input; 
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using System.Threading.Tasks;
using ShopProject.View.Common.Setting;
using ShopProject.ViewModel.SettingPage;

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
            _openSettingOperationRecorderCommand = CreateCommand(() => { PageSetting = App.Container.GetViewWithViewModel<SettingOperationRecorderView, SettingOperationRecorderViewModel>(); });
            _openSettingStorageCommand = CreateCommand(() => { PageSetting = App.Container.GetViewWithViewModel<SettingStorageView, SettingStorageViewModel>(); }); 
            _pageSetting = new Page(); 
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
