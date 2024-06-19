using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.View.ToolsPage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>
    {

        private StorageModel? _model;
        private static Timer? _timer;
        private static string? _itemSearch;

        private ICommand _openCreateProductWindowCommand;
        private ICommand _openFormationProductWindowCommand;
        private ICommand _updateSizeGridCommand;
        private ICommand _updateProductDataGridViewCommand;
        private ICommand _openDeliveriOfProductCommand;
        private ICommand _openExportProductToExelCommand;
        private ICommand _openImportProductWhichExelCommand;

        private List<ProductEntiti> _products;

        public StorageViewModel()
        {
            _openCreateProductWindowCommand = new DelegateCommand(() => { new CreateProductView().Show(); });

            _openFormationProductWindowCommand = new DelegateCommand(() => { new FormationProductView().Show(); });
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);

            _updateProductDataGridViewCommand = new DelegateCommand(() => { SearchProduct(""); });
            _openDeliveriOfProductCommand = new DelegateCommand(() => { new DeliveryProductView().Show(); });

            _openExportProductToExelCommand = new DelegateCommand(() => { new ExportProductExelView().Show(); });
            _openImportProductWhichExelCommand = new DelegateCommand(() => { new ImportProductExelView().Show(); });


            _model = new StorageModel();
            _products = new List<ProductEntiti>();
            
            _itemSearch = string.Empty;
            _statusBarCountProduct = string.Empty;

            _timer = new Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);
            

            ProductList = new List<ProductEntiti>();
            
           
            SearchProduct("");
            SetFiledStatusBar();

        }

        private List<ProductEntiti>? _productslist;
        public List<ProductEntiti>? ProductList
        {
            get {  return _productslist;  }
            set { _productslist = value; OnPropertyChanged("ProductList"); }
        }

        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged("Heigth"); }
        }

        private string _statusBarCountProduct;
        public string StatusBarCountProduct
        {
            get { return _statusBarCountProduct; }
            set { _statusBarCountProduct = value; OnPropertyChanged("StatusBarCountProduct"); }
        }

        private async void SetFiledStatusBar()
        {
            await Task.Run(() =>
            {
                StatusBarCountProduct = "Кілкісь товарі: " + _model.GetCount();
            });

        }
        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 280;
        }

        public ICommand UpdateProductDataGridView => _updateProductDataGridViewCommand;

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchProduct, CanRegister); }

        private void SearchProduct(object parameter)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _itemSearch = parameter.ToString();

            _timer.Change(2000, Timeout.Infinite);// 2000 затримка в дві секунди для продовження ведення тексту
        }

        private void OnInputStopped(object state)
        {
            UpdateDataGrid(_itemSearch);
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private async void UpdateDataGrid(string parameter)
        {
            await Task.Run(() =>
            {
                _itemSearch = parameter.ToString();

                var result = _model.SearchItems(parameter.ToString());
                result.Reverse();

                if (ProductList.Count != 0)
                {
                    ProductList.Clear();
                }

                if (result.Count > 100)
                {
                    ProductList = result.Take(100).ToList();//100 це кількість елементів на екрані 
                }
                else
                {
                    ProductList = result;
                }
            });
            
        }

        public ICommand UpdateProductCommand { get => new DelegateParameterCommand(UpdateProduct, CanRegister); }
        private void UpdateProduct(object parameter)
        {
            _products = new List<ProductEntiti>();
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

        public ICommand DeleteProductCommand { get => new DelegateParameterCommand(DeleteProduct, CanRegister); }
        private void DeleteProduct(object parameter)
        {
            if (MessageBox.Show("видалити?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _products = new List<ProductEntiti>();
                if (_model != null)
                    _model.ContertIListToList((IList)parameter, _products);
                if (_products.Count == 1)
                {
                    if (_products[0] != null)
                        if (_model != null)
                        {
                            if (_model.DeleteItem(_products[0]))
                            {
                                if (_itemSearch != string.Empty)
                                {
                                    SearchProduct(_itemSearch);
                                }
                                else
                                {
                                    SearchProduct(_itemSearch);
                                }
                            }
                        }
                }
            }
        }

        public ICommand AddProductArhiveCommand { get => new DelegateParameterCommand(AddProductArhive, CanRegister); }
        private void AddProductArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _products = new List<ProductEntiti>();
                if (_model != null)
                {
                    _model.ContertIListToList((IList)parameter, _products);
                    if (_products.Count == 1)
                    {
                        if(_model.SetItemInArhive(_products[0]))
                        {
                            if(_itemSearch != string.Empty)
                            {
                                SearchProduct(_itemSearch);
                            }
                            else
                            {
                                SearchProduct(_itemSearch);
                            }
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
                _products = new List<ProductEntiti>();
                if (_model != null)
                {
                    _model.ContertIListToList((IList)parameter, _products);
                    if (_products.Count == 1)
                    {
                        if (_model.SetItemOutOfStock(_products[0]))
                        {
                            if (_itemSearch != string.Empty)
                            {
                                SearchProduct(_itemSearch);
                            }
                            else
                            {
                                SearchProduct(_itemSearch);
                            }
                        }
                    }
                }
            }
        }
        
        public ICommand OpenWindoiwCreateStikerCommand{ get => new DelegateParameterCommand(ShowWindowCreateStikerCommand, CanRegister); }
        private void ShowWindowCreateStikerCommand(object parameter)
        {
            _products = new List<ProductEntiti>();
            if (_model != null)
                _model.ContertIListToList((IList)parameter, _products);

            if (_products.Count == 1)
            {
                Session.Product = _products[0];
                new CreateStickerView().Show();
            }
        }
        
        private bool CanRegister(object parameter) => true;

        public ICommand OpenCreateProductWindowCommand => _openCreateProductWindowCommand;
        public ICommand OpenFormationProductWindowCommand => _openFormationProductWindowCommand;
        public ICommand OpenDeliveriOfProductCommand => _openDeliveriOfProductCommand;
        public ICommand OpenExportProductToExelCommand => _openExportProductToExelCommand;
        public ICommand OpenImportProductWhichExelCommand => _openImportProductWhichExelCommand;

    }
}
