using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.ToolsPage;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class StorageViewModel : ViewModel<StorageViewModel>
    {
        private StorageModel? _storageModel;

        private ICommand _searchButton;
        private ICommand _visibileAllButton;
        private ICommand _openCreateProductWindow;
        private List<Product> _products;
        
        public StorageViewModel()
        {
            Products = new List<Product>();
            SearchTemplateName = new List<string>();
          

            _searchButton = new DelegateCommand(SearchProductInCodeAndName);
            _visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start(); });
            _openCreateProductWindow = new DelegateCommand(CreateProductDatabase);
       
            SizeDataGrid = (double)SystemParameters.PrimaryScreenWidth;
            _products = new List<Product>();

            _nameSearch = string.Empty;
            _searchTemplateName = new List<string>();

            SetFieldTextComboBox();

            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
        }

        private void SetFieldTextComboBox()
        {
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        private List<Product>? _product;
        public List<Product>? Products
        {
            get { return _product; }
            set{ _product = value; OnPropertyChanged("Products"); }
        }

        private double _sizeDataGrid;
        public double SizeDataGrid
        {
            set{ _sizeDataGrid = value; OnPropertyChanged("SizeDataGrid"); }
        }

        private List<string> _searchTemplateName;
        public List<string> SearchTemplateName
        {
            get { return _searchTemplateName; }
            set { _searchTemplateName = value; OnPropertyChanged("SearchTemplateName"); }
        }

        private int _selectedIndexSearch;
        public int SelectedIndexSearch
        {
            get { return _selectedIndexSearch; }
            set { _selectedIndexSearch = value; OnPropertyChanged("SelectedIndexSearch"); }
        }

        private string _nameSearch;
        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged("NameSearch"); }
        }

        public ICommand SearchButton => _searchButton;

        void SearchProductInCodeAndName()
        {
            if (_storageModel != null)
                switch (_selectedIndexSearch)
                {
                    case 0:
                        {
                            Products = _storageModel.SearchProduct(_nameSearch, TypeSearch.Code);
                            break;
                        }
                    case 1:
                        {
                            Products = _storageModel.SearchProduct(_nameSearch, TypeSearch.Name);
                            break;
                        }
                    case 2:
                        {
                            Products = _storageModel.SearchProduct(_nameSearch, TypeSearch.Articule);
                            break;
                        }
                }
        }
        public ICommand VisibileAllButton => _visibileAllButton;

        void SetFieldItemDataGridThread()
        {
            Products = null;
            _storageModel = new StorageModel();
            Products = _storageModel.GetItems();
        }

        public ICommand OpenCreateProductWindow => _openCreateProductWindow;

        private void CreateProductDatabase()
        {
            if (_storageModel != null)
            {
                new CreateProductPage().Show();
            }
        }

        public ICommand UpdateProductCommand { get => new DelegateParameterCommand(EditingProduct, CanRegister); }
        private void EditingProduct(object parameter)
        {
            _products = new List<Product>();
            if (_storageModel != null)
                _storageModel.ContertToListProduct((IList)parameter, _products);

            if (_products.Count == 1)
            {
                StaticResourse.product = _products[0];
                new UpdateProduct().ShowDialog();
            }
            else
            {
                StaticResourse.products = _products;
                new UpdateProductAll().ShowDialog();
            }
            
        }

        public ICommand DeleteProductCommand { get => new DelegateParameterCommand(DeleteProduct, CanRegister); }
        private void DeleteProduct(object parameter)
        {
            if (MessageBox.Show("видалити?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _products = new List<Product>();
                if (_storageModel != null)
                    _storageModel.ContertToListProduct((IList)parameter, _products);
                if (_products.Count == 1)
                {
                    if (_products[0] != null)
                        if (_storageModel != null)
                        {
                            if (_storageModel.DeleteProduct(_products[0]))
                                new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
                        }
                }
            }
        }

        public ICommand AddProductArhiveCommand { get => new DelegateParameterCommand(AddProductArhive, CanRegister); }
        private void AddProductArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _products = new List<Product>();
                if (_storageModel != null)
                {
                    _storageModel.ContertToListProduct((IList)parameter, _products);
                    if (_products.Count == 1)
                    {
                        if(_storageModel.SetProductInArhive(_products[0]))
                            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
                    }
                }
            }
        }
        public ICommand OpenWindoiwCreateStiker{ get => new DelegateParameterCommand(ShowWindowCreateStiker, CanRegister); }
        private void ShowWindowCreateStiker(object parameter)
        {
            _products = new List<Product>();
            if (_storageModel != null)
                _storageModel.ContertToListProduct((IList)parameter, _products);

            if (_products.Count == 1)
            {
                StaticResourse.product = _products[0];
                new CreateStiker().Show();
            }
        }
        private bool CanRegister(object parameter) => true;


    }
}
