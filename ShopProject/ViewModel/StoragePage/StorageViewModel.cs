using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper.ProductContoller;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage; 
using ShopProject.View.ToolsPage;
using ShopProject.ViewModel.TemplatePage;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
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
    internal class StorageViewModel : ViewModel<StorageViewModel>
    { 
        private StorageModel _model; 

        private ICommand _openCreateProductWindowCommand;
        private ICommand _openFormationProductWindowCommand;
        private ICommand _updateSizeGridCommand;
        private ICommand _updateProductDataGridViewCommand;
        private ICommand _openDeliveriOfProductCommand;
        private ICommand _openExportProductToExelCommand;
        private ICommand _openImportProductWhichExelCommand;

        private List<ProductEntity> _products;
        private bool _isReadyUpdateDataGriedView;
        private static Timer _timer;
        private static string _itemSearch;

        public StorageViewModel()
        {
            _model = new StorageModel();
            _productslist = new List<ProductEntity>();
            _products = new List<ProductEntity>();
            _statusProducts = new List<string>();
            _countShowList = new List<string>();
            _paginator = new TemplatePaginatorButtonViewModel();
            _statusBarCountProduct = string.Empty;
            _isReadyUpdateDataGriedView = false;
            _itemSearch = string.Empty;

            _openCreateProductWindowCommand = new DelegateCommand(() => { new CreateProductView().Show(); });
            _openFormationProductWindowCommand = new DelegateCommand(() => { new FormationProductView().Show(); });
            _openDeliveriOfProductCommand = new DelegateCommand(() => { new DeliveryProductView().Show(); });
            _openExportProductToExelCommand = new DelegateCommand(() => { new ExportProductExelView().Show(); });
            _openImportProductWhichExelCommand = new DelegateCommand(() => { new ImportProductExelView().Show(); });
            _updateProductDataGridViewCommand = new DelegateCommand(() => { SetFieldPage(); });      
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);
            
            Paginator.Callback = UpdateDataGridView;

            _timer = new Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite); 

            SetFieldPage();
            Mediator.Subscribe("ReloadProduct", (object obg) => { SetFieldPage(); });
        }

        private TemplatePaginatorButtonViewModel _paginator;
        public TemplatePaginatorButtonViewModel Paginator
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
            get { return _selectIndexCountShowList;}
            set { _selectIndexCountShowList = value; OnPropertyChanged(nameof(SelectIndexCountShowList));
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            }
        }

        private List<ProductEntity>? _productslist;
        public List<ProductEntity>? ProductList
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
            set { _selectedStatusProduct = value; OnPropertyChanged(nameof(SelectedStatusProduct)); 
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
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

        public void SetFieldPage()
        {  
            SetComboBox();
            SetFiledStatusBar();
            SetFielComboBoxTypeStatusProduct();
            SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1 , true);
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
            SelectedStatusProduct = 0;
            if (StatusProducts.Count == 0)
            {
                StatusProducts.Add(TypeStatusProduct.Unknown.ToString());
                StatusProducts.Add(TypeStatusProduct.InStock.ToString());
                StatusProducts.Add(TypeStatusProduct.OutStock.ToString());
                StatusProducts.Add(TypeStatusProduct.Archived.ToString());
            }     
        }

        private void SetFiledStatusBar()
        {
            var result = new ProductInfo();
            Task task = Task.Run(async () => 
            {
                result = await _model.GetProductInfo();
            });
            task.ContinueWith(t =>
            {
                StatusBarCountProduct = $"Кількість товарів: {result.CountProductAllStatus}   " +
                                        $"Кількість товарів в наявності: {result.CountProductInStockStatus}  " +
                                        $"Кількість товарів не в наявносіть: {result.CountProductOutStockStatus}  " +
                                        $"Кількксть товарів в архіві: {result.CountProductArchivedStauts}  ";
            });
        }

        private void SetFieldDataGridView(int countCoulmn, int page = 1 , bool reloadbutton = false)
        {
            PaginatorData<ProductEntity> result = new PaginatorData<ProductEntity>();
            Task t = Task.Run(async () => {

                result = await _model.GetProductsPageColumn(page, countCoulmn,Enum.Parse<TypeStatusProduct>(StatusProducts.ElementAt(SelectedStatusProduct)));
            });
            t.ContinueWith(t => {
                if (reloadbutton) 
                { 
                    Paginator.CountButton = result.Pages;
                }
                Paginator.CountColumn = countCoulmn;
                ProductList = result.Data;
                _isReadyUpdateDataGriedView = true;
            });
        }  

        private void UpdateDataGridView(int countCoulmn, int page = 1)
        {
            if(_isReadyUpdateDataGriedView)
            {
                if (ProductList!=null && ProductList.Count > 0)
                {
                    ProductList.Clear();
                }
                PaginatorData<ProductEntity> result = new PaginatorData<ProductEntity>();

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_itemSearch == string.Empty && _itemSearch == "")
                {
                    SetFieldDataGridView(countCoulmn,page , false);
                }
                else
                {
                    SearchByNameAndByBarCode(countCoulmn, page);
                } 
            }
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchProduct, CanRegister); }

        private void SearchProduct(object parameter)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _itemSearch = parameter.ToString();

            _timer.Change(2000, Timeout.Infinite);// 2000 затримка в дві секунди для продовження ведення тексту
        }

        private void OnInputStopped(object state)
        {
            UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void SearchByNameAndByBarCode(int countColumn, int page)
        { 
            PaginatorData<ProductEntity> result = new PaginatorData<ProductEntity>();

            Task t = Task.Run(async () =>
            {
                if (Regex.Matches(_itemSearch, "[1-9]").Count == 12) // 12 - довжина штрихкоду
                {
                    result.Data = new List<ProductEntity>() { (await _model.SearchByBarCode(_itemSearch, Enum.Parse<TypeStatusProduct>(StatusProducts.ElementAt(SelectedStatusProduct)))) };
                }
                else
                {
                    result = await _model.SearchByName(_itemSearch, page, countColumn, Enum.Parse<TypeStatusProduct>(StatusProducts.ElementAt(SelectedStatusProduct)));
                }

            });
            t.ContinueWith(t =>
            {
                if (!(Paginator.CountButton == result.Pages))
                {
                    Paginator.CountButton = result.Pages;
                }
                Paginator.CountColumn = countColumn; 
                ProductList = result.Data; 
            });
        }
        

        public ICommand UpdateProductCommand { get => new DelegateParameterCommand(UpdateProduct, CanRegister); }
        private void UpdateProduct(object parameter)
        {
            _products = new List<ProductEntity>();
            if (_model != null)
                _model.ContertIListToList((IList)parameter, _products);

            if (_products.Count == 1)
            {
                Session.Product = _products[0];
                new UpdateProductView().ShowDialog();
            }
            else
            {
                Session.ProductList = _products;
                new UpdateProductRangeView().ShowDialog();
            }
        }

        public ICommand AddProductArhiveCommand { get => new DelegateParameterCommand(AddProductArhive, CanRegister); }
        private void AddProductArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _products = new List<ProductEntity>();
                if (_model != null)
                {
                    _model.ContertIListToList((IList)parameter, _products);
                    if (_products.Count == 1)
                    {
                        if (_model.SetItemInArhive(_products[0]))
                        {
                            SetFieldPage();
                            MessageBox.Show("Товар перенесено в архів");
                        }
                    }
                }
            }
        }

        public ICommand AddOutOfStockProductCommand { get => new DelegateParameterCommand(AddOutOfStockProduct, CanRegister); }
        private void AddOutOfStockProduct(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _products = new List<ProductEntity>();
                if (_model != null)
                {
                    _model.ContertIListToList((IList)parameter, _products);
                    if (_products.Count == 1)
                    {
                        if (_model.SetItemOutOfStock(_products[0]))
                        {
                            SetFieldPage();
                        }
                    }
                }
            }
        }

        public ICommand OpenWindoiwCreateStikerCommand { get => new DelegateParameterCommand(ShowWindowCreateStikerCommand, CanRegister); }
        private void ShowWindowCreateStikerCommand(object parameter)
        {
            _products = new List<ProductEntity>();
            if (_model != null)
                _model.ContertIListToList((IList)parameter, _products);

            if (_products.Count == 1)
            {
                Session.Product = _products[0];
                new CreateStickerView().Show();
            }
        } 

        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 380;
        }
        private bool CanRegister(object parameter) => true;


        public ICommand UpdateProductDataGridView => _updateProductDataGridViewCommand;
        public ICommand OpenCreateProductWindowCommand => _openCreateProductWindowCommand;
        public ICommand OpenFormationProductWindowCommand => _openFormationProductWindowCommand;
        public ICommand OpenDeliveriOfProductCommand => _openDeliveriOfProductCommand;
        public ICommand OpenExportProductToExelCommand => _openExportProductToExelCommand;
        public ICommand OpenImportProductWhichExelCommand => _openImportProductWhichExelCommand;

    }
}
