using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.View.AdminPage.UserManagement.User;
using ShopProject.ViewModel.AdminPage.UserManagement.User;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject.ViewModel.AdminPage.UserManagement
{
    internal class UserManagementViewModel : ViewModel<UserManagementViewModel>, IViewModelLoadResourse
    {
        public UserManagementViewModel()
        {
            _tabs = new ObservableCollection<TabItem>(); 
            _generalShadowVisibility = Visibility.Collapsed;
            MediatorService.AddEventAsync("UserManagementSnadowSetVissible", async () => { GeneralShadowVisibility = Visibility.Visible; });
            MediatorService.AddEventAsync("UserManagementSnadowSetCollapsed", async () => { GeneralShadowVisibility = Visibility.Collapsed; });
        }
        private Visibility _generalShadowVisibility;
        public Visibility GeneralShadowVisibility
        {
            get { return _generalShadowVisibility; }
            set { _generalShadowVisibility = value; OnPropertyChanged(nameof(GeneralShadowVisibility)); }
        }

        private ObservableCollection<TabItem> _tabs;
        public ObservableCollection<TabItem> Tabs
        {
            get { return _tabs; }
            set { _tabs = value; OnPropertyChanged(nameof(Tabs)); }
        }
        private int _selectedTabItem;
        public int SelectedTabItem
        {
            get { return _selectedTabItem; }
            set { _selectedTabItem = value; OnPropertyChanged(nameof(SelectedTabItem)); }
        }

        public Task LoadResourse()
        {
            if (Tabs.Count == 0)
            {
                Tabs.Add(new TabItem() { Header = "Користувачі", Content = new Frame() { Content = App.Container.GetViewWithViewModel<UsersDataView, UsersDataViewModel>() }, }); 
            }
            SelectedTabItem = 0;
            return Task.CompletedTask;
        }
    }
}
