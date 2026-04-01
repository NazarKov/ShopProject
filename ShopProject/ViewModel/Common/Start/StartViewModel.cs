using ShopProject.Helpers; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ShopProject.Core.Mvvm;
using ShopProject.Model.Navigation;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.Service;

namespace ShopProject.ViewModel.Common.Start
{
    internal class StartViewModel : ViewModel<StartViewModel>
    {
        private ICommand _openServerSelectionPageCommand; 
        private ICommand _closeWindowCommand;
        public StartViewModel()
        {
            _openServerSelectionPageCommand = CreateCommand(() => { MediatorService.ExecuteNavigation(NavigationButton.RedirectServerSelectionPage); });
            _closeWindowCommand = CreateCommand(() => { MediatorService.ExecuteNavigation(NavigationButton.ExitApp); });
        }

        public ICommand OpenServerSelectionPageCommand => _openServerSelectionPageCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand;


    }
}
