using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using ShopProject.View.AdminPage.Storage.ProductUnit;
using ShopProject.ViewModel.StoragePage.ProductUnitPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.Storage.ProductUnit
{
    internal class ProductUnitsDataViewModel : ViewModel<ProductUnitsDataViewModel> , IViewModelLoadResourse
    {  

        private ICommand _openCreateProductUnitPageCommand;
        private ICommand _updateGridViewCommad; 

        private IProductUnitServiсe _productUnitServiсe; 
        private ICommand _searchItemCommand;

        private bool _isReadyUpdateDataGriedView; 
        private bool _reloadField;
        public ProductUnitsDataViewModel(IProductUnitServiсe productUnitServiсe)
        {
            _productUnitServiсe = productUnitServiсe;

            _openCreateProductUnitPageCommand = CreateCommand(() => {  App.Container.GetNewViewWithViewModel<CreateProductUnitView,CreateProductUnitViewModel>().Show(); });
            _updateGridViewCommad = CreateCommandAsync(async () => { _reloadField = false; SearchItem = string.Empty; SelectedStatusUnit = 0; SelectIndexCountShowList = 0; await SetFieldPage(); });
            _searchItemCommand = CreateCommandAsync(DebounceSearch);

            _units = new List<ProductUnitModel>();
            _statusUnits = new List<string>();
            _paginator = new PaginatorViewModel();
            _countShowList = new List<string>();
            _isReadyUpdateDataGriedView = false; 
            _searchItem = string.Empty;
            _shadowVisibility = Visibility.Collapsed;
            _reloadField = false;

            Paginator.Callback =async(int i)=> {await UpdateDataGridView(i); };


            MediatorService.AddEventAsync("ReloadUnitsGriedView", async () => { await SetFieldPage(); });
        }

        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage); 
        }

        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; OnPropertyChanged(nameof(SearchItem)); if (_reloadField) { SearchCommand.Execute(null); } }
        }

        private List<ProductUnitModel> _units;
        public List<ProductUnitModel> Units
        {
            get { return _units; }
            set
            {
                _units = value; OnPropertyChanged(nameof(Units));
            }
        }

        private List<string> _countShowList;
        public List<string> CountShowList
        {
            get { return _countShowList; }
            set { _countShowList = value; OnPropertyChanged(nameof(CountShowList)); }
        }

        private int _selectIndexCountShowList;
        public int SelectIndexCountShowList
        {
            get { return _selectIndexCountShowList; }
            set
            {
                _selectIndexCountShowList = value; OnPropertyChanged(nameof(SelectIndexCountShowList));
                Task.Run(async () => { await UpdateDataGridView(); });
            }
        }

        private List<string> _statusUnits;
        public List<string> StatusUnits
        {
            get { return _statusUnits; }
            set { _statusUnits = value; OnPropertyChanged(nameof(_statusUnits)); }
        }

        private int _selectedStatusUnit;
        public int SelectedStatusUnit
        {
            get { return _selectedStatusUnit; }
            set
            {
                _selectedStatusUnit = value; OnPropertyChanged(nameof(SelectedStatusUnit));
                Task.Run(async () => { await UpdateDataGridView(); });
            }
        }

        private PaginatorViewModel _paginator;
        public PaginatorViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
        }

        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged(nameof(Heigth)); }
        }

        private Visibility _shadowVisibility;
        public Visibility ShadowVisibility
        {
            get { return _shadowVisibility; }
            set { _shadowVisibility = value; OnPropertyChanged(nameof(ShadowVisibility)); }
        }
        private ICommand? _lostfocusCommand;
        public ICommand? LostFocusCommand
        {
            get { return _lostfocusCommand; }
            set { _lostfocusCommand = value; OnPropertyChanged(nameof(LostFocusCommand)); }
        }

        private void SetFieldComboBox()
        {
            if (CountShowList.Count == 0)
            {
                CountShowList.Add("10");
                CountShowList.Add("25");
                CountShowList.Add("50");
                CountShowList.Add("100");
                CountShowList.Add("250");
                CountShowList.Add("500");
                CountShowList.Add("1000");
            }
            SelectIndexCountShowList = 0;
        }

        private void SetFielComboBoxTypeStatusUnits()
        {
            SelectedStatusUnit = 0;
            if (StatusUnits.Count == 0)
            {
                StatusUnits = new List<string>(ProductUnitStatusModel.GetStatusForStorage());
            }
        }

        private async Task SetFieldPage()
        {
            SetFieldComboBox();
            SetFielComboBoxTypeStatusUnits();
            await SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
            _reloadField = true;
        }

        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        { 
            var result = await _productUnitServiсe.GetUnitsPageColumn(page, countCoulmn, Enum.GetValues<TypeStatusUnit>().ElementAt(SelectedStatusUnit));
            if (reloadbutton)
            {
                if (result.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.ReloadButton = true;
                    Paginator.CountButton = result.Pages;
                }
            }
            if(result.Data == null)
            {
                throw new Exception("Невдалося завантажити одиниці");
            }
            Units = result.Data.ToProductUnitModel().ToList();
            _isReadyUpdateDataGriedView = true;
        }

        private async Task UpdateDataGridView(int page = 1,bool reloadbutton = false)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (Units.Count > 0)
                {
                    Units.Clear();
                }

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_searchItem == string.Empty && _searchItem == "")
                {
                   await SetFieldDataGridView(countColumn, page, reloadbutton);
                }
                else
                {
                    await SearchByNameAndByBarCode(countColumn, page);
                }
            }
        }

        public ICommand SearchCommand => _searchItemCommand;


        private CancellationTokenSource? _searchCts;
        private async Task DebounceSearch()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(1000, _searchCts.Token);
                await SafeExecuteAsync(async () =>
                {
                    await UpdateDataGridView();
                });
            }
            catch (TaskCanceledException) { }
        }  
        private async Task SearchByNameAndByBarCode(int countColumn, int page)
        {
            Paginator<Model.Domain.ProductUnit.ProductUnit> result = new Paginator<Model.Domain.ProductUnit.ProductUnit>(); 

            if (Regex.Matches(_searchItem, "[0-9]").Count == _searchItem.Length)
            {
                result= await _productUnitServiсe.SearchByBarCode(_searchItem,page,countColumn, Enum.GetValues<TypeStatusUnit>().ElementAt(SelectedStatusUnit));
            }
            else
            {
                result = await _productUnitServiсe.SearchByName(_searchItem, page, countColumn, Enum.GetValues<TypeStatusUnit>().ElementAt(SelectedStatusUnit));
            }

            if (result.Data != null)
            {
                if (result.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.CountButton = result.Pages;
                }
                Units = result.Data.ToProductUnitModel().ToList();
            } 
        } 

        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateProductUnitPageCommand => _openCreateProductUnitPageCommand;

        public ICommand OpenUpdateProductUnitPageCommand { get => CreateCommandParameterAsync<object>(UpdateUnit); }

        private async Task UpdateUnit(object parameter)
        {
            var items = (parameter as IList);

            if (items != null && items.Count > 0)
            {
                _productUnitServiсe.SetUnitOnSession(((items[0] as ProductUnitModel)).ToProductUnit());
                App.Container.GetNewViewWithViewModel<UpdateProductUnitView,UpdateProductUnitViewModel>().ShowDialog(); 
                await UpdateDataGridView(Paginator.SelectIndexButton, true);
            }
            else
            {
                throw new Exception("Ви не обрали елемент");
            } 
        }

        public ICommand DeleteProductUnitCommand { get => CreateCommandParameterAsync<object>(DeleteUnit); }

        private async Task DeleteUnit(object parameter)
        {
            var items = parameter as IList;


            if (items != null && items.Count > 0)
            { 
                if (await _productUnitServiсe.Delete(((ProductUnitModel)items[0]).ToProductUnit()))
                {
                    MessageBox.Show("Одиницю видалено");
                }
                await UpdateDataGridView();
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }

        }

        public ICommand UpdateStatusToFavoriteProductUnitCommand { get => CreateCommandParameterAsync<object>(UpdateToFavoriteStatus); }

        private async Task UpdateToFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            { 
                if (await _productUnitServiсe.ChangeStatus(((ProductUnitModel)items[0]).ToProductUnit(), TypeStatusUnit.Favorite))
                {
                    MessageBox.Show("Одиницю оновлено");
                }
                await UpdateDataGridView();
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }

        }

        public ICommand UpdateStatusToUnFavoriteProductUnitCommand { get => CreateCommandParameterAsync<object>(UpdateToUnFavoriteStatus); }

        private async Task UpdateToUnFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            {
                bool result = false;
                result = await _productUnitServiсe.ChangeStatus(((ProductUnitModel)items[0]).ToProductUnit(), TypeStatusUnit.UnFavorite);
                if (result)
                {
                    MessageBox.Show("Одиницю оновлено");
                }
                await UpdateDataGridView();
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }

        } 
    }
}
