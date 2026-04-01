using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Core.Mvvm.Service;
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Product;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.Product.Interface;
using ShopProject.View.AdminPage.Storage.Product;
using ShopProject.View.StoragePage.ExcelPage.ExportExcelPage;
using ShopProject.View.StoragePage.ExcelPage.ImportExcelPage; 
using ShopProject.View.ToolsPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.Storage.Product
{
    internal class ProductsDataViewModel:ViewModel<ProductsDataViewModel> , IViewModelLoadResourse
    {
        private ICommand _openCreateProductWindowCommand;
        private ICommand _openFormationProductWindowCommand;
        private ICommand _updateSizeGridCommand;
        private ICommand _updateProductDataGridViewCommand;
        private ICommand _openDeliveriOfProductCommand;
        private ICommand _openExportProductToExelCommand;
        private ICommand _openImportProductWhichExelCommand;
        private ICommand _searchCommand;

        private readonly IProductServiсe _productService;
        private bool _reloadField;
         
        private bool _isReadyUpdateDataGriedView;  
        public ProductsDataViewModel(IProductServiсe productServiсe)
        {
            _productService = productServiсe;
            _productslist = new List<ProductModel>(); 
            _statusProducts = new List<string>();
            _countShowList = new List<string>();
            _paginator = new PaginatorViewModel();
            _statusBarCountProduct = string.Empty;
            _isReadyUpdateDataGriedView = false; 
            _searchItem = string.Empty;
            _shadowVisibility = Visibility.Collapsed;
            _reloadField = false;

            _openCreateProductWindowCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateProductView, CreateProductViewModel>().Show(); });
            _openFormationProductWindowCommand = CreateCommand(() => { new FormationProductView().Show(); });
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
            Paginator<Model.Domain.Product.Product> result = await _productService.GetPageColumn(page, countCoulmn, Enum.Parse<TypeStatusProduct>(Enum.GetNames(typeof(TypeStatusProduct)).ToList().ElementAt(SelectedStatusProduct)));
            if (reloadbutton)
            {
                
                if (result.Pages == 0)
                {
                    Paginator.CountButton = 1; 
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Paginator.ReloadButton = true;
                        Paginator.CountButton = result.Pages;
                    }); 
                }
            }
            if (result.Data == null)
            {
                throw new Exception("Невдалося завантажити товари");
            }

            ProductList = result.Data.ToProductModel().ToList();
            _isReadyUpdateDataGriedView = true;
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
            var result = new Paginator<Model.Domain.Product.Product>();

            _searchItem = _productService.RemoveSeparatorBarCode(_searchItem);

            if (_productService.ValidationSearchitem(_searchItem))
            {
                result = (await _productService.SearchByBarCode(_searchItem,page, countColumn,
                    Enum.Parse<TypeStatusProduct>(Enum.GetNames(typeof(TypeStatusProduct)).ToList().ElementAt(SelectedStatusProduct))));
            }
            else
            {
                result = await _productService.SearchByName(_searchItem, page, countColumn,
                    Enum.Parse<TypeStatusProduct>(Enum.GetNames(typeof(TypeStatusProduct)).ToList().ElementAt(SelectedStatusProduct)));
            }
            if (result.Data == null) 
            {
                ProductList = new List<ProductModel>();
                throw new Exception("Невдалося завантажити товари");
            }
            if (result.Data.Count() > 0 & result.Pages == 0)
            {
                Paginator.CountButton = 1;
            }
            else
            {
                Paginator.CountButton = result.Pages;
            }
            ProductList = result.Data.ToProductModel().ToList();
        }


        public ICommand UpdateProductCommand { get => CreateCommandParameterAsync<object>(UpdateProduct); }
        private async Task UpdateProduct(object parameter)
        {

            var products = new List<ProductModel>();
            if (parameter != null)
                products = _productService.ContertIListToList((IList)parameter);

            if (products.Count == 1)
            {
                    _productService.SetProductOnSession(products[0].ToProduct());
                var windwow = App.Container.GetNewViewWithViewModel<UpdateProductView, UpdateProductViewModel>();
                windwow.ShowDialog();
            }
            else
            {
                _productService.SetProductsOnSession(products.ToProduct().ToList());
                var windwow = App.Container.GetNewViewWithViewModel<UpdateProductRangeView, UpdateProductRangeViewModel>();
                windwow.ShowDialog();
            }
            Paginator.IsUseSelectIndextButton = true;
            await UpdateDataGridView(Paginator.SelectIndexButton,true);
        }

        public ICommand AddProductArhiveCommand { get => CreateCommandParameterAsync<object>(AddProductArhive); }
        private async Task AddProductArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var products = new List<ProductModel>();
                if (parameter != null)
                {
                    products = _productService.ContertIListToList((IList)parameter);
                    if (products.Count == 1)
                    {
                        if (await _productService.SetTypeInArhive(products[0].ToProduct()))
                        {
                            await SetFieldPage();
                            MessageBox.Show("Товар перенесено в архів");
                        }
                    }
                }
            }
        }

        public ICommand AddOutOfStockProductCommand { get => new DelegateParameterCommandAsync<object>(AddOutOfStockProduct); }
        private async Task AddOutOfStockProduct(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var products = new List<ProductModel>();
                if (parameter != null)
                {
                    products = _productService.ContertIListToList((IList)parameter);
                    if (products.Count == 1)
                    {
                        if (await _productService.SetTypeOutOfStock(products[0].ToProduct()))
                        {
                            await SetFieldPage();
                            MessageBox.Show("Товар перенесено в архів");
                        }
                    }
                }
            }
        }

        public ICommand OpenWindoiwCreateStikerCommand { get => CreateCommandParameter<object>(ShowWindowCreateStikerCommand); }
        private void ShowWindowCreateStikerCommand(object parameter)
        {
            var products = new List<ProductModel>();
            if (parameter != null)
                products = _productService.ContertIListToList((IList)parameter);

            if (products.Count == 1)
            {
                _productService.SetProductOnSession(products[0].ToProduct());
                new CreateStickerView().Show();
            }
        }

        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 280;
        }  
        public ICommand UpdateProductDataGridView => _updateProductDataGridViewCommand;
        public ICommand OpenCreateProductWindowCommand => _openCreateProductWindowCommand;
        public ICommand OpenFormationProductWindowCommand => _openFormationProductWindowCommand;
        public ICommand OpenDeliveriOfProductCommand => _openDeliveriOfProductCommand;
        public ICommand OpenExportProductToExelCommand => _openExportProductToExelCommand;
        public ICommand OpenImportProductWhichExelCommand => _openImportProductWhichExelCommand;

    }
} 
