using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Views.StoragePage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class OutOfStockViewModel : ViewModel<OutOfStockViewModel>
    {
        private OutOfStockModel _model;

        private ICommand _searchButton;
        private ICommand _visibileAllButton;
        private List<ProductsOutOfStock> _productsOutOfStock;

        public OutOfStockViewModel() 
        {
            _model = new OutOfStockModel();
            _products = new List<ProductsOutOfStock>();
            _productsOutOfStock = new List<ProductsOutOfStock>();

            _visibileAllButton = new DelegateCommand(setFieldDataGrid);

            _searchButton = new DelegateCommand(SearchProductInCodeAndName);
            setFieldDataGrid();
            SetFieldTextComboBox();
        }
        private void setFieldDataGrid()
        {
            Products = _model.GetAll();   
        }

        private void SetFieldTextComboBox()
        {
            SearchTemplateName = new List<string>();
            SearchTemplateName.Add("ШтрихКод");
            SearchTemplateName.Add("Назва");
            SearchTemplateName.Add("Артикуль");
            SelectedIndexSearch = 0;
        }

        private List<ProductsOutOfStock> _products;
        public List<ProductsOutOfStock> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
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
            if (_model != null)
                switch (_selectedIndexSearch)
                {
                    case 0:
                        {
                            Products = _model.SearchProducts(_nameSearch, TypeSearch.Code);
                            break;
                        }
                    case 1:
                        {
                            Products = _model.SearchProducts(_nameSearch, TypeSearch.Name);
                            break;
                        }
                    case 2:
                        {
                            Products = _model.SearchProducts(_nameSearch, TypeSearch.Articule);
                            break;
                        }
                }
        }
        public ICommand VisibileAllButton => _visibileAllButton;

        public ICommand ReturnProductInStorageCommand { get => new DelegateParameterCommand(ReturnProductInStorage, (object parameter) => true); }
        private void ReturnProductInStorage(object parameter)
        {
            if (MessageBox.Show("Перенести", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _productsOutOfStock = new List<ProductsOutOfStock>();
                if (_productsOutOfStock != null)
                {
                    _model.ConvertToList((IList)parameter, _productsOutOfStock);
                    if (_productsOutOfStock.Count == 1)
                    {
                        if (_model.ReturnProductInStorage(_productsOutOfStock[0]))
                        {
                            new Thread(new ThreadStart(setFieldDataGrid)).Start();
                        }
                    }
                }
            }
        }

        public ICommand DeleteProductOutOfStockAndProductCommand { get => new DelegateParameterCommand(DeleteArhiveAndProduct, (object parameter) => true); }
        private void DeleteArhiveAndProduct(object parameter)
        {
            if (MessageBox.Show("Ви точно хочете видалити?\nТовар також видаляється.", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _productsOutOfStock = new List<ProductsOutOfStock>();
                if (_productsOutOfStock != null)
                {
                    _model.ConvertToList((IList)parameter, _productsOutOfStock);

                    if (_productsOutOfStock.Count == 1)
                    {
                        if (_model.DeleteRecordArhive(_productsOutOfStock[0], _productsOutOfStock[0].Product))
                        {
                            MessageBox.Show("Aрхівну записку виладено", "in", MessageBoxButton.OK);
                            new Thread(new ThreadStart(setFieldDataGrid)).Start();
                        }

                    }
                }
            }
        }
    }
}
