using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.View.AdminPage.Storage.Product;
using ShopProject.View.AdminPage.Storage.ProductCodeUKTZED;
using ShopProject.View.AdminPage.Storage.ProductUnit;
using ShopProject.ViewModel.AdminPage.Storage.Product;
using ShopProject.ViewModel.AdminPage.Storage.ProductUnit;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject.ViewModel.StoragePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>, IViewModelLoadResourse
    {
        public StorageViewModel()
        {
            _tabs = new ObservableCollection<TabItem>();

            _generalShadowVisibility = Visibility.Collapsed;
            MediatorService.AddEventAsync("StorageSnadowSetVissible", async () => { GeneralShadowVisibility = Visibility.Visible; });
            MediatorService.AddEventAsync("StorageSnadowSetCollapsed", async () => { GeneralShadowVisibility = Visibility.Collapsed; });
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
            if(Tabs.Count == 0)
            {
                Tabs.Add(new TabItem() { Header = "Товари", Content = new Frame() { Content = App.Container.GetViewWithViewModel<ProductsDataView, ProductsDataViewModel>() }, });
                Tabs.Add(new TabItem() { Header = "Одиниці", Content = new Frame() { Content = App.Container.GetViewWithViewModel<ProductUnitsDataView, ProductUnitsDataViewModel>() }, });
                Tabs.Add(new TabItem() { Header = "Коди УКТЗЕД", Content = new Frame() { Content = App.Container.GetViewWithViewModel<ProductCodesUKTZEDDataView, ProductCodeUKTZEDDataViewModel>() }, });
            }
            SelectedTabItem = 0;
            return Task.CompletedTask;
        }
    }
}
