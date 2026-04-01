using System.Threading.Tasks; 
using ShopProject.Core.Mvvm; 
using System.Windows.Controls;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.ViewModel.AdminPage.Storage.Product;
using ShopProject.View.AdminPage.Storage.Product;
using System.Collections.ObjectModel;
using ShopProject.View.AdminPage.Storage.ProductUnit;
using ShopProject.ViewModel.AdminPage.Storage.ProductUnit;
using ShopProject.View.AdminPage.Storage.ProductCodeUKTZED;

namespace ShopProject.ViewModel.StoragePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>, IViewModelLoadResourse
    {
        public StorageViewModel()
        {
            _tabs = new ObservableCollection<TabItem>();
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
