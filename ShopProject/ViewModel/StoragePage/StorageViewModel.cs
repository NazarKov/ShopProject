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
        private ICommand _openFormationProductWindow;

        private List<Goods> _goods;
        
        public StorageViewModel()
        {
            GoodsList = new List<Goods>();
            SearchTemplateName = new List<string>();
          

            _searchButton = new DelegateCommand(SearchProductInCodeAndName);
            _visibileAllButton = new DelegateCommand(() => { new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start(); });
            _openCreateProductWindow = new DelegateCommand(() => { new CreateGoodsPage().Show(); });
            _openFormationProductWindow = new DelegateCommand(() => { new FormationProduct().Show(); });

            SizeDataGrid = (double)SystemParameters.PrimaryScreenWidth;
            _goods = new List<Goods>();

            _nameSearch = string.Empty;
            _searchTemplateName = new List<string>();

            SetFieldTextComboBox();

            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
        }

      

        private List<Goods>? _goodslist;
        public List<Goods>? GoodsList
        {
            get { return _goodslist; }
            set{ _goodslist = value; OnPropertyChanged("GoodsList"); }
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

        private void SetFieldTextComboBox()
        {
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        public ICommand SearchButton => _searchButton;

        void SearchProductInCodeAndName()
        {
            if (_storageModel != null)
                switch (_selectedIndexSearch)
                {
                    case 0:
                        {
                            GoodsList = _storageModel.SearchProduct(_nameSearch, TypeSearch.Code);
                            break;
                        }
                    case 1:
                        {
                            GoodsList = _storageModel.SearchProduct(_nameSearch, TypeSearch.Name);
                            break;
                        }
                    case 2:
                        {
                            GoodsList = _storageModel.SearchProduct(_nameSearch, TypeSearch.Articule);
                            break;
                        }
                }
        }
        public ICommand VisibileAllButton => _visibileAllButton;

        void SetFieldItemDataGridThread()
        {
            GoodsList = null;
            _storageModel = new StorageModel();
            GoodsList = _storageModel.GetItems();
        }

        public ICommand UpdateProductCommand { get => new DelegateParameterCommand(EditingProduct, CanRegister); }
        private void EditingProduct(object parameter)
        {
            _goods = new List<Goods>();
            if (_storageModel != null)
                _storageModel.ContertToListProduct((IList)parameter, _goods);

            if (_goods.Count == 1)
            {
                StaticResourse.product = _goods[0];
                new UpdateProduct().ShowDialog();
            }
            else
            {
                StaticResourse.products = _goods;
                new UpdateProductAll().ShowDialog();
            }
            
        }

        public ICommand DeleteProductCommand { get => new DelegateParameterCommand(DeleteProduct, CanRegister); }
        private void DeleteProduct(object parameter)
        {
            if (MessageBox.Show("видалити?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goods = new List<Goods>();
                if (_storageModel != null)
                    _storageModel.ContertToListProduct((IList)parameter, _goods);
                if (_goods.Count == 1)
                {
                    if (_goods[0] != null)
                        if (_storageModel != null)
                        {
                            if (_storageModel.DeleteProduct(_goods[0]))
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
                _goods = new List<Goods>();
                if (_storageModel != null)
                {
                    _storageModel.ContertToListProduct((IList)parameter, _goods);
                    if (_goods.Count == 1)
                    {
                        if(_storageModel.SetProductInArhive(_goods[0]))
                            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
                    }
                }
            }
        }
        public ICommand AddOutOfStockProductCommand { get => new DelegateParameterCommand(AddOutOfStockProduct, CanRegister); }
        private void AddOutOfStockProduct(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _goods = new List<Goods>();
                if (_storageModel != null)
                {
                    _storageModel.ContertToListProduct((IList)parameter, _goods);
                    if (_goods.Count == 1)
                    {
                        if (_storageModel.SetProductinOutOfStok(_goods[0]))
                            new Thread(new ThreadStart(SetFieldItemDataGridThread)).Start();
                    }
                }
            }
        }
        public ICommand OpenWindoiwCreateStiker{ get => new DelegateParameterCommand(ShowWindowCreateStiker, CanRegister); }
        private void ShowWindowCreateStiker(object parameter)
        {
            _goods = new List<Goods>();
            if (_storageModel != null)
                _storageModel.ContertToListProduct((IList)parameter, _goods);

            if (_goods.Count == 1)
            {
                StaticResourse.product = _goods[0];
                new CreateStiker().Show();
            }
        }
        private bool CanRegister(object parameter) => true;

        public ICommand OpenCreateProductWindow => _openCreateProductWindow;
        public ICommand OpenFormationProductWindow => _openFormationProductWindow;

    }
}
