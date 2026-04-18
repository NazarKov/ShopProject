using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProject.View.AdminPage.Storage.ProductCodeUKTZED;
using ShopProject.ViewModel.StoragePage.ProductCodeUKTZEDPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class ProductCodeUKTZEDDataViewModel : ViewModel<ProductCodeUKTZEDDataViewModel> , IViewModelLoadResourse
    { 

        private bool _isReadyUpdateDataGriedView; 

        private ICommand _openCreateProductCodeUKTZEDPageCommand;
        private ICommand _updateGridViewCommad; 
        private ICommand _searchItemCommand;
        private IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;


        private bool _reloadField;
        public ProductCodeUKTZEDDataViewModel(IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe)
        {
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe; 
            _openCreateProductCodeUKTZEDPageCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateProductCodeUKTZEDView,CreateProductCodeUKTZEDViewModel>().Show(); });
             
            _updateGridViewCommad = CreateCommandAsync(async () => { _reloadField = false; SearchItem = string.Empty; SelectedStatusCodeUKTZED = 0; SelectIndexCountShowList = 0; await SetFieldPage(); });
            _searchItemCommand = CreateCommandAsync(DebounceSearch);

            _codeUKTZED = new List<ProductCodeUKTZEDModel>();
            _statusCodeUKTZED = new List<string>();
            _paginator = new PaginatorViewModel();
            _countShowList = new List<string>();
            _isReadyUpdateDataGriedView = false;
            _searchItem = string.Empty;
            _shadowVisibility = Visibility.Collapsed;
            _reloadField = false;

            Paginator.Callback = async (int i) => { await UpdateDataGridView(i); };

            MediatorService.AddEventAsync("ReloadCodeUKTEDGriedView", async () => { await SetFieldPage(); });
        }

        public async Task LoadResourse()
        {
            await SetFieldPage();
        }

        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; OnPropertyChanged(nameof(SearchItem)); if (_reloadField) { SearchCommand.Execute(null); } }
        }

        private List<ProductCodeUKTZEDModel> _codeUKTZED;
        public List<ProductCodeUKTZEDModel> CodeUKTZED
        {
            get { return _codeUKTZED; }
            set
            {
                _codeUKTZED = value; OnPropertyChanged(nameof(CodeUKTZED));
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

        private List<string> _statusCodeUKTZED;
        public List<string> StatusCodeUKTZED
        {
            get { return _statusCodeUKTZED; }
            set { _statusCodeUKTZED = value; OnPropertyChanged(nameof(StatusCodeUKTZED)); }
        }

        private int _selectedStatusCodeUKTZED;
        public int SelectedStatusCodeUKTZED
        {
            get { return _selectedStatusCodeUKTZED; }
            set
            {
                _selectedStatusCodeUKTZED = value; OnPropertyChanged(nameof(SelectedStatusCodeUKTZED));
                Task.Run(async () => { await UpdateDataGridView(); });
            }
        }

        private PaginatorViewModel _paginator;
        public PaginatorViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
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

        private void SetFielComboBoxTypeStatusCodeUKTZED()
        {
            SelectedStatusCodeUKTZED = 0;
            StatusCodeUKTZED = new List<string>(ProductCodeUKTZEDStatusModel.GetStatusForStorage()); 
        }

        private async Task SetFieldPage()
        {
            SetFieldComboBox();
            SetFielComboBoxTypeStatusCodeUKTZED();
            await SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
            _reloadField = true;
        }

        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            var result = await _productCodeUKTZEDServiсe.GetCodeUKTZEDPageColumn(page, countCoulmn, Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(SelectedStatusCodeUKTZED));
            
            if (reloadbutton)
            {
                if (result.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.CountButton = result.Pages;
                }
            }
            if (result.Data != null)
            {
                CodeUKTZED = result.Data.ToProductCodeUKTZEDModel().ToList();
                _isReadyUpdateDataGriedView = true; 
            }
        }

        private async Task UpdateDataGridView(int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (CodeUKTZED.Count > 0)
                {
                    CodeUKTZED.Clear();
                }

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_searchItem == string.Empty && _searchItem == "")
                {
                    await SetFieldDataGridView(countColumn, page, true);
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
            Paginator<ProductCodeUKTZED> result = new Paginator<ProductCodeUKTZED>();

            if (Regex.Matches(_searchItem, "[0-9]").Count == _searchItem.Length)
            {
                result = await _productCodeUKTZEDServiсe.SearchByBarCode(_searchItem,page,countColumn , Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(SelectedStatusCodeUKTZED));
            }
            else
            {
                result = await _productCodeUKTZEDServiсe.SearchByName(_searchItem, page, countColumn, Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(SelectedStatusCodeUKTZED));
            }
            if (result.Pages == 0)
            {
                Paginator.CountButton = 1;
            }
            else
            {
                Paginator.CountButton = result.Pages;
            }

            if (result.Data != null) 
            {
                CodeUKTZED = result.Data.ToProductCodeUKTZEDModel().ToList(); 
            }
        } 

        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateProductCodeUKTZEDPageCommand => _openCreateProductCodeUKTZEDPageCommand;

        public ICommand OpenUpdateProductCodeUKTZEDPageCommand { get => CreateCommandParameter<object>(UpdateCodeUKTZED); }

        private void UpdateCodeUKTZED(object parameter)
        {
            var items = (parameter as IList);
            if (items != null && items.Count > 0)
            {
               _productCodeUKTZEDServiсe.SetOnSession(((ProductCodeUKTZEDModel)items[0]).ToProductCodeUKTZED());
                App.Container.GetNewViewWithViewModel<UpdateProductCodeUKTZEDView,UpdateProductCodeUKTZEDViewModel>().ShowDialog();
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }
        }

        public ICommand DeleteProductCodeUKTZEDCommand { get => CreateCommandParameterAsync<object>(DeleteCodeUKTZED); }

        private async Task DeleteCodeUKTZED(object parameter)
        {
            var items = parameter as IList;


            if (items != null && items.Count > 0)
            {
                if (await _productCodeUKTZEDServiсe.Delete(((ProductCodeUKTZEDModel)items[0]).ToProductCodeUKTZED()))
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

        public ICommand UpdateStatusToFavoriteProductCodeUKTZEDCommand { get => CreateCommandParameterAsync<object>(UpdateToFavoriteStatus); }

        private async Task UpdateToFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            {
                if (await _productCodeUKTZEDServiсe.ChangeStatus(((ProductCodeUKTZEDModel)items[0]).ToProductCodeUKTZED(), TypeStatusCodeUKTZED.Favorite))
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

        public ICommand UpdateStatusToUnFavoriteProductCodeUKTZEDCommand { get => CreateCommandParameterAsync<object>(UpdateToUnFavoriteStatus); }

        private async Task UpdateToUnFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            { 
                if (await _productCodeUKTZEDServiсe.ChangeStatus(((ProductCodeUKTZEDModel)items[0]).ToProductCodeUKTZED(), TypeStatusCodeUKTZED.UnFavorite))
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
