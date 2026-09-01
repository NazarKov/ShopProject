using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Product; 
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Common; 
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Domain.Product.Interface;
using ShopProject.Services.Modules.Mapping.Product; 
using ShopProject.View.AdminPage.Storage.Product;
using ShopProject.View.AdminPage.Storage.Tools;
using ShopProject.View.Integration.Excel.Export;
using ShopProject.View.Integration.Excel.Import;
using ShopProject.View.Integration.Printing; 
using ShopProject.ViewModel.Integration.Printing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.Storage.Product
{
    internal class ProductsDataViewModel:ViewModel<ProductsDataViewModel> , IViewModelLoadResourse
    {
        private ICommand _openCreateProductWindowCommand; 
        private ICommand _updateSizeGridCommand;
        private ICommand _updateProductDataGridViewCommand;
        private ICommand _openDeliveriOfProductCommand;
        private ICommand _openExportProductToExelCommand;
        private ICommand _openImportProductWhichExelCommand;
        private ICommand _searchCommand;

        private readonly IProductServiсe _productService;
        private readonly IMessageBoxControlService _messageBoxControlService;
        private bool _reloadField;
         
        private bool _isReadyUpdateDataGriedView;  
        public ProductsDataViewModel(IProductServiсe productServiсe, IMessageBoxControlService messageBoxControlService)
        {
            _productService = productServiсe;
            _productslist = new List<ProductModel>(); 
            _statusProducts = new List<string>();
            _countShowList = new List<string>();
            _paginator = new PaginatorViewModel();
            _messageBoxControlService = messageBoxControlService;
            _statusBarCountProduct = string.Empty;
            _isReadyUpdateDataGriedView = false; 
            _searchItem = string.Empty;
            _shadowVisibility = Visibility.Collapsed; 
            _reloadField = false;

            _openCreateProductWindowCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateProductView, CreateProductViewModel>().Show(); }); 
            _openDeliveriOfProductCommand = CreateCommand(() => { new DeliveryProductView().Show(); });
            _openExportProductToExelCommand = CreateCommand(() => { new ExportExcelProductView().Show(); });
            _openImportProductWhichExelCommand = CreateCommand(() => { new ImportProductExcelView().Show(); });
            _updateProductDataGridViewCommand = CreateCommandAsync(async () => { _reloadField = false; SearchItem = string.Empty; SelectedStatusProduct = 0; SelectIndexCountShowList = 0; await SetFieldPage(); });
            _updateSizeGridCommand = CreateCommand(UpdateSizes);
            _searchCommand = CreateCommandAsync(DebounceSearch);

            Paginator.Callback = async (int i) => { await UpdateDataGridView(i); };

            MediatorService.AddEventAsync(NavigationButton.ReloadProduct.ToString(), async () =>{ await SafeExecuteAsync(SetFieldPage); });
           
        }

        public async Task LoadResourse()
        {
            await SafeExecuteAsync(async () =>
            {
                await SetFieldPage();
            });
        }
        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; OnPropertyChanged(nameof(SearchItem)); if (_reloadField) { SearchCommand.Execute(null); } }
        }

        private PaginatorViewModel _paginator;
        public PaginatorViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
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
                if (_reloadField)
                { 
                    Task.Run(async () => { await UpdateDataGridView(); });
                }
            }
        }

        private List<ProductModel>? _productslist;
        public List<ProductModel>? ProductList
        {
            get { return _productslist; }
            set { _productslist = value; OnPropertyChanged(nameof(ProductList)); }
        }

        private List<string> _statusProducts;
        public List<string> StatusProducts
        {
            get { return _statusProducts; }
            set { _statusProducts = value; OnPropertyChanged(nameof(StatusProducts)); }
        }

        private int _selectedStatusProduct;
        public int SelectedStatusProduct
        {
            get { return _selectedStatusProduct; }
            set
            {
                _selectedStatusProduct = value; OnPropertyChanged(nameof(SelectedStatusProduct));
                if (_reloadField)
                {
                    Task.Run(async () => { await UpdateDataGridView(); });
                }
            }
        }
        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged(nameof(Heigth)); }
        }

        private string _statusBarCountProduct;
        public string StatusBarCountProduct
        {
            get { return _statusBarCountProduct; }
            set { _statusBarCountProduct = value; OnPropertyChanged(nameof(StatusBarCountProduct)); }
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

        public async Task SetFieldPage()
        { 
            SetComboBox();
            SetFielComboBoxTypeStatusProduct();
            await SetFiledStatusBar();
            await SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true); 
            _reloadField = true;
        } 

        private void SetComboBox()
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

        private void SetFielComboBoxTypeStatusProduct()
        {
            if (StatusProducts.Count == 0)
            {
                StatusProducts = new List<string>(ProductStatusModel.GetProductStatusForStorage()); 
            }
            SelectedStatusProduct = 0;
        }

        private async Task SetFiledStatusBar()
        {
            var result = await _productService.GetProductStatistics();
            StatusBarCountProduct = $"Кількість товарів: {result.CountProductAllStatus}   " +
                                    $"Кількість товарів в наявності: {result.CountProductInStockStatus}  " +
                                    $"Кількість товарів не в наявносіть: {result.CountProductOutStockStatus}  " +
                                    $"Кількксть товарів в архіві: {result.CountProductArchivedStauts}  ";
        }

        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = true)
        {
            var result = await _productService.GetPageColumn(page, countCoulmn, Enum.Parse<TypeStatusProduct>(Enum.GetNames(typeof(TypeStatusProduct)).ToList().ElementAt(SelectedStatusProduct)));
            if (result.IsSuccess)
            {
                if (reloadbutton)
                {
                    
                    if (result.Data.Pages == 0)
                    {
                        Paginator.CountButton = 1; 
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Paginator.ReloadButton = true;
                            Paginator.CountButton = result.Data.Pages;
                        }); 
                    }
                }
                if (result.Data == null)
                {
                    throw new Exception("Невдалося завантажити товари");
                }

                ProductList = result.Data.Data.ToProductModel().ToList();
                _isReadyUpdateDataGriedView = true;
            }
        }

        private async Task UpdateDataGridView(int page = 1,bool reloadbutton = false)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (ProductList != null && ProductList.Count > 0)
                {
                    ProductList.Clear();
                }
                int countCoulmn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_searchItem == string.Empty && _searchItem == "")
                {
                    if (page == 1)
                    {
                        await SetFieldDataGridView(countCoulmn, page);
                    }
                    else
                    {
                        await SetFieldDataGridView(countCoulmn, page, reloadbutton);
                    }
                }
                else
                {
                    await SearchByNameAndByBarCode(countCoulmn, page);
                }
            }
        }

        public ICommand SearchCommand => _searchCommand;

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
            var result = new OperationResult<Paginator<ShopProject.Model.Domain.Product.Product,TypeStatusProduct>>();

            _searchItem = _productService.RemoveSeparatorBarCode(_searchItem);

            if (Regex.Matches(_searchItem, "[0-9]").Count == _searchItem.Length)
            {
                result = (await _productService.SearchByBarCode(_searchItem,page, countColumn,
                    Enum.Parse<TypeStatusProduct>(Enum.GetNames(typeof(TypeStatusProduct)).ToList().ElementAt(SelectedStatusProduct))));
            }
            else
            {
                result = await _productService.SearchByName(_searchItem, page, countColumn,
                    Enum.Parse<TypeStatusProduct>(Enum.GetNames(typeof(TypeStatusProduct)).ToList().ElementAt(SelectedStatusProduct)));
            }
            if (result.IsSuccess)
            { 
                if (result.Data.Data.Count() > 0 & result.Data.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.CountButton = result.Data.Pages;
                }
                ProductList = result.Data.Data.ToProductModel().ToList();
            }
            else if (result.IsError)
            {
                ProductList = new List<ProductModel>();
                Paginator.CountButton = 0;
            }
            else
            {
                ProductList = new List<ProductModel>();
                throw new Exception("Невдалося завантажити товари");
            }
        }


        public ICommand UpdateProductCommand { get => CreateCommandParameterAsync<object>(UpdateProduct); }
        private async Task UpdateProduct(object parameter)
        {

            var products = new List<ShopProject.Model.Domain.Product.Product>();
            if (parameter != null) 
                products = _productService.ContertIListToList((IList)parameter);

            if (products.Count == 1)
            {
                    _productService.SetProductOnSession(products[0]);
                var windwow = App.Container.GetNewViewWithViewModel<UpdateProductView, UpdateProductViewModel>();
                windwow.ShowDialog();
            } 
            else if (products.Count > 0)
            {
                _productService.SetProductsOnSession(products.ToList());
                var windwow = App.Container.GetNewViewWithViewModel<UpdateProductRangeView, UpdateProductRangeViewModel>();
                windwow.ShowDialog();
            }

            if (products.Count == 0)
            {
                await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }
            else
            {
                Paginator.IsUseSelectIndextButton = true;
                await UpdateDataGridView(Paginator.SelectIndexButton, true);
            } 
        }

        public ICommand AddProductArhiveCommand { get => CreateCommandParameterAsync<object>(AddProductArhive); }
        private async Task AddProductArhive(object parameter)
        {  
            var products = new List<ShopProject.Model.Domain.Product.Product>();
            if (parameter != null)
            {
                products = _productService.ContertIListToList((IList)parameter);
                if (products.Count == 1)
                {
                    if (await _messageBoxControlService.Show("Перенести?.", "Informations", MessageBoxType.Question, "StorageSnadow"))
                    {
                        var item = products[0];
                        var result = await _productService.UpdateParameter(nameof(item.Status), TypeStatusProduct.Archived, item);
                        if (result.IsSuccess)
                        {
                            await SetFieldPage();
                            await _messageBoxControlService.Show("Товар перенесено в архів.", "Informations", MessageBoxType.Success, "StorageSnadow"); 
                        }
                        else
                        {
                            await _messageBoxControlService.Show("Невдалося виконати операцію.", "Error", MessageBoxType.Error, "StorageSnadow"); 
                        } 
                    }
                }
                else if(products.Count > 1)
                {
                    // зробити можливість міняти статус на декількох елементах
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
                }
            }
        }

        public ICommand AddOutOfStockProductCommand { get => new DelegateParameterCommandAsync<object>(AddOutOfStockProduct); }
        private async Task AddOutOfStockProduct(object parameter)
        { 
            var products = new List<ShopProject.Model.Domain.Product.Product>();
            if (parameter != null)
            {
                products = _productService.ContertIListToList((IList)parameter);
                if (products.Count == 1)
                {
                    if (await _messageBoxControlService.Show("Перенести?.", "Informations", MessageBoxType.Question, "StorageSnadow"))
                    {
                        var item = products[0];
                        var result = await _productService.UpdateParameter(nameof(item.Status), TypeStatusProduct.OutStock, item);
                        if (result.IsSuccess)
                        {
                            await SetFieldPage();
                            await _messageBoxControlService.Show("Товар перенесено.", "Informations", MessageBoxType.Success, "StorageSnadow");
                        }
                        else
                        {
                            await _messageBoxControlService.Show("Невдалося виконати операцію.", "Error", MessageBoxType.Error, "StorageSnadow");
                        }
                    }
                }
                else if (products.Count > 1)
                {
                    // зробити можливість міняти статус на декількох елементах
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
                }
            }
        }

        public ICommand OpenWindoiwCreateStikerCommand { get => CreateCommandParameter<object>(ShowWindowCreateStikerCommand); }
        private void ShowWindowCreateStikerCommand(object parameter)
        {
            var products = new List<ShopProject.Model.Domain.Product.Product>();
            if (parameter != null)
                products = _productService.ContertIListToList((IList)parameter);

            if (products.Count == 1)
            {
                _productService.SetProductOnSession(products[0]);
                App.Container.GetNewViewWithViewModel<StickerPrintView,StickerPrintViewModel>().Show();
            }
            else
            {
                _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "StorageSnadow");
            }
        }

        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 280;
        }  
        public ICommand UpdateProductDataGridView => _updateProductDataGridViewCommand;
        public ICommand OpenCreateProductWindowCommand => _openCreateProductWindowCommand; 
        public ICommand OpenDeliveriOfProductCommand => _openDeliveriOfProductCommand;
        public ICommand OpenExportProductToExelCommand => _openExportProductToExelCommand;
        public ICommand OpenImportProductWhichExelCommand => _openImportProductWhichExelCommand;

    }
} 
