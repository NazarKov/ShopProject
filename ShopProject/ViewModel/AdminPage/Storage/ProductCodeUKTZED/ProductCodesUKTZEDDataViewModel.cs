using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.ProductCodeUKTZED;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Common;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.Mapping.ProductCodeUKTZED;
using ShopProject.View.AdminPage.Storage.ProductCodeUKTZED;
using ShopProject.ViewModel.StoragePage.ProductCodeUKTZEDPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMessageBoxControlService _messageBoxControlService;

        private bool _reloadField;
        public ProductCodeUKTZEDDataViewModel(IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe, IMessageBoxControlService messageBoxControlService)
        {
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
            _messageBoxControlService = messageBoxControlService;
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
            var result = await _productCodeUKTZEDServiсe.GetPageColumn(page, countCoulmn, Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(SelectedStatusCodeUKTZED));
            if (result.IsSuccess)
            {
                var paginator = result.Data;

                if (reloadbutton)
                {
                    if (paginator.Pages == 0)
                    {
                        Paginator.CountButton = 1;
                    }
                    else
                    {
                        Paginator.CountButton = paginator.Pages;
                    }
                }
                if (result.Data != null)
                {
                    CodeUKTZED = paginator.Data.ToProductCodeUKTZEDModel().ToList();
                    _isReadyUpdateDataGriedView = true;
                }
            } 
            else if (result.IsError)
            {
                CodeUKTZED = new List<ProductCodeUKTZEDModel>();
                Paginator.CountButton = 0;
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
            OperationResult<Paginator<ProductCodeUKTZED, TypeStatusCodeUKTZED>> result = new OperationResult<Paginator<ProductCodeUKTZED, TypeStatusCodeUKTZED>>();

            if (Regex.Matches(_searchItem, "[0-9]").Count == _searchItem.Length)
            {
                result = await _productCodeUKTZEDServiсe.SearchByBarCode(_searchItem,page,countColumn , Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(SelectedStatusCodeUKTZED));
            }
            else
            {
                result = await _productCodeUKTZEDServiсe.SearchByName(_searchItem, page, countColumn, Enum.GetValues<TypeStatusCodeUKTZED>().ToList().ElementAt(SelectedStatusCodeUKTZED));
            }

            if (result.IsSuccess)
            {
                var paginator = result.Data;
                if (paginator.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.CountButton = paginator.Pages;
                }

                if (result.Data != null)
                {
                    CodeUKTZED = paginator.Data.ToProductCodeUKTZEDModel().ToList();
                }
            }
            else if (result.IsError)
            {
                CodeUKTZED = new List<ProductCodeUKTZEDModel>();
                Paginator.CountButton = 0;
            }
        } 

        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateProductCodeUKTZEDPageCommand => _openCreateProductCodeUKTZEDPageCommand;

        public ICommand OpenUpdateProductCodeUKTZEDPageCommand { get => CreateCommandParameter<object>(UpdateCodeUKTZED); }

        private void UpdateCodeUKTZED(object parameter)
        {
            var items = (parameter as IList);
            if (items != null && items.Count == 1)
            {
               _productCodeUKTZEDServiсe.SetOnSession(((ProductCodeUKTZEDModel)items[0]).ToProductCodeUKTZED());
                App.Container.GetNewViewWithViewModel<UpdateProductCodeUKTZEDView,UpdateProductCodeUKTZEDViewModel>().ShowDialog();
            }
            else if (items.Count > 1)
            {
                _messageBoxControlService.Show("Ви вибрали забагато елементів.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }
            else
            {
                _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow"); 
            }
        }

        public ICommand DeleteProductCodeUKTZEDCommand { get => CreateCommandParameterAsync<object>(DeleteCodeUKTZED); }

        private async Task DeleteCodeUKTZED(object parameter)
        {
            var items = parameter as IList;


            if (items != null && items.Count == 1)
            {
                var result = await _productCodeUKTZEDServiсe.Delete(((ProductCodeUKTZEDModel)items[0]).ToProductCodeUKTZED());
                if (result.IsSuccess)
                {
                    _messageBoxControlService.Show("Код видалено", "Informations", MessageBoxType.Success, "StorageSnadow"); 
                    await UpdateDataGridView();
                }
                else if (result.IsError) 
                {
                    _messageBoxControlService.Show(result.ErrorMessage, "Error", MessageBoxType.Error, "StorageSnadow");
                }
                else
                {
                    _messageBoxControlService.Show("Невдалося виконати операцію", "Error", MessageBoxType.Error, "StorageSnadow"); 
                } 
            }
            else if (items.Count > 1)
            {
                _messageBoxControlService.Show("Ви вибрали забагато елементів.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }
            else
            {
                _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }

        }

        public ICommand UpdateStatusToFavoriteProductCodeUKTZEDCommand { get => CreateCommandParameterAsync<object>(UpdateToFavoriteStatus); }

        private async Task UpdateToFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count == 1)
            {
                var item = (ProductCodeUKTZEDModel)items[0];

                var result = await _productCodeUKTZEDServiсe.UpdateParameter(nameof(item.Status), TypeStatusCodeUKTZED.Favorite, item.ToProductCodeUKTZED());

                if (result.IsSuccess)
                {
                    _messageBoxControlService.Show("Код оновлено", "Informations", MessageBoxType.Success, "StorageSnadow"); 
                    await UpdateDataGridView();
                }
                else if (result.IsError)
                {
                    _messageBoxControlService.Show(result.ErrorMessage, "Error", MessageBoxType.Error, "StorageSnadow"); 
                }
                else
                {
                    _messageBoxControlService.Show("Невдалося виконати операцію", "Error", MessageBoxType.Error, "StorageSnadow");
                }  
            }
            else if (items.Count > 1)
            {
                _messageBoxControlService.Show("Ви вибрали забагато елементів.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }
            else
            {
                _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }

        }

        public ICommand UpdateStatusToUnFavoriteProductCodeUKTZEDCommand { get => CreateCommandParameterAsync<object>(UpdateToUnFavoriteStatus); }

        private async Task UpdateToUnFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count == 1)
            {
                var item = (ProductCodeUKTZEDModel)items[0];

                var result = await _productCodeUKTZEDServiсe.UpdateParameter(nameof(item.Status), TypeStatusCodeUKTZED.UnFavorite, item.ToProductCodeUKTZED());

                if (result.IsSuccess)
                {
                    _messageBoxControlService.Show("Код оновлено", "Informations", MessageBoxType.Success, "StorageSnadow");
                    await UpdateDataGridView();
                }
                else if (result.IsError)
                {
                    _messageBoxControlService.Show(result.ErrorMessage, "Error", MessageBoxType.Error, "StorageSnadow");
                }
                else
                {
                    _messageBoxControlService.Show("Невдалося виконати операцію", "Error", MessageBoxType.Error, "StorageSnadow");
                }
            }
            else if (items.Count > 1)
            {
                _messageBoxControlService.Show("Ви вибрали забагато елементів.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }
            else
            {
                _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }

        }
         
    }
}
