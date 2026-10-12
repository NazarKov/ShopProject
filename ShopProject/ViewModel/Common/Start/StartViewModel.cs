using System.Windows.Input;
using ShopProject.Model.Navigation; 
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Infrastructure.Mvvm;

namespace ShopProject.ViewModel.Common.Start
{
    internal class StartViewModel : ViewModel<StartViewModel>
    {
        private ICommand _openServerSelectionPageCommand;
        private ICommand _redirectToRegisterWindowServiceCommand;
        private ICommand _closeWindowCommand;
        public StartViewModel()
        {
            _openServerSelectionPageCommand = CreateCommand(() => { MediatorService.ExecuteNavigation(NavigationButton.RedirectServerSelectionPage); });
            _closeWindowCommand = CreateCommand(() => { MediatorService.ExecuteNavigation(NavigationButton.ExitApp); });
            _redirectToRegisterWindowServiceCommand = CreateCommand(()=>{ MediatorService.ExecuteNavigation(NavigationButton.RedirectToRegisterWindwoServicePage); });
        }

        public ICommand OpenServerSelectionPageCommand => _openServerSelectionPageCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;

        public ICommand RedirectToRegisterWindwoServiceComman => _redirectToRegisterWindowServiceCommand;
    }
}
