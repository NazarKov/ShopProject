using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.View.AdminPage.PointOfSale.OperationRecorder;
using ShopProject.View.AdminPage.PointOfSale.TaxObject;
using ShopProject.ViewModel.AdminPage.PointOfSale.OperationRecorder;
using ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject; 
using System.Collections.ObjectModel; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject.ViewModel.AdminPage.PointOfSale
{
    internal class PointOfSaleViewModel : ViewModel<PointOfSaleViewModel> , IViewModelLoadResourse
    {
        public PointOfSaleViewModel()
        {
            _tabs = new ObservableCollection<TabItem>();
            _generalShadowVisibility = Visibility.Collapsed;
            MediatorService.AddEventAsync("PointOfSaleSnadowSetVissible", async () => { GeneralShadowVisibility = Visibility.Visible; });
            MediatorService.AddEventAsync("PointOfSaleSnadowSetCollapsed", async () => { GeneralShadowVisibility = Visibility.Collapsed; });
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
                Tabs.Add(new TabItem() { Header = "Обєкт оподаткування", Content = new Frame() { Content = App.Container.GetViewWithViewModel<TaxObjectsDataView, TaxObjectsDataViewModel>() }, });
                Tabs.Add(new TabItem() { Header = "Касові апарати", Content = new Frame() { Content = App.Container.GetViewWithViewModel<OperationRecordersDataView, OperationRecordersDataViewModel>() }, }); 
            }
            SelectedTabItem = 0;
            return Task.CompletedTask;
        }
    }
}
