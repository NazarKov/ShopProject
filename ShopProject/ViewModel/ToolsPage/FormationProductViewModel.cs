using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Model.ToolsPage;
using ShopProject.Views.ToolsPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class FormationProductViewModel : ViewModel<FormationProductViewModel>
    {
        private ICommand _addProductInFormationCommand;
        private ICommand _addProudctDataBaseCommand;

        private FormationProductModel _model;
        private List<Goods> _productsSelectGridView;
        private List<Goods> _products;

        public FormationProductViewModel() 
        {
            _model = new FormationProductModel();

            _name = string.Empty;
            _code = string.Empty;
            _articule = string.Empty;
            _searchCode = string.Empty;
            _count = 0;
            _price = 0;
            _units = new List<string>();
            _selcetUnits = 0;
            _productsInFormedProduct = new List<Goods>();
            _productsSelectGridView = new List<Goods>();
            _products = new List<Goods>();

            _addProductInFormationCommand = new DelegateCommand(AddProductInFormation);
            _addProudctDataBaseCommand = new DelegateCommand(AddProductDataBase);

            ProductsInFormedProduct = new List<Goods>();
            
            SetFieldComboBox();
        }
        private void SetFieldComboBox()
        {
            Units = new List<string>();
            Units.Add("Шт");
            Units.Add("кг");
            Units.Add("пачка");
            Units.Add("ящик");

            SelcetUnits = 0;
        }

        private string _searchCode;
        public string SearchCode
        {
            get { return _searchCode; }
            set { _searchCode = value; OnPropertyChanged("SearchCode"); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged("Code"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged("Count"); }
        }

        private string _articule;
        public string Articule
        {
            get { return _articule; } 
            set {  _articule = value; OnPropertyChanged("Articule"); }
        }

        private List<string> _units;
        public List<string> Units 
        {
            get { return _units; }
            set { _units = value;  OnPropertyChanged("Units"); }
        }

        private int _selcetUnits;
        public int SelcetUnits
        {
            get { return _selcetUnits; }
            set { _selcetUnits = value;OnPropertyChanged("SelcetUnits"); }
        }

        private List<Goods> _productsInFormedProduct;
        public List<Goods> ProductsInFormedProduct
        {
            get { return _productsInFormedProduct; }
            set { _productsInFormedProduct = value; OnPropertyChanged("ProductsInFormedProduct"); }
        }

        public ICommand AddProductInFormationCommand => _addProductInFormationCommand;

        private void AddProductInFormation()
        {
            var item= _model.GetProduct(SearchCode);
            if (item != null)
            {
                _products = new List<Goods>();
                _products.AddRange(_productsInFormedProduct);
                _products.Add(item);
                ProductsInFormedProduct = _products;
                SearchCode = string.Empty;
            }
            else
            {
                MessageBox.Show("Товар не знайдено");
            }
        }

        public ICommand UpdateProductsInFormedProductCommand { get => new DelegateParameterCommand(UpdateProductsInFormedProduct, CanRegister); }
       
        private void UpdateProductsInFormedProduct(object parameter)
        {
            _productsSelectGridView = new List<Goods>();
            if (_model != null)
            {
                _model.ContertToListProduct((IList)parameter, _productsSelectGridView);
                
                var list = _model.UpdateList(ProductsInFormedProduct, _productsSelectGridView);
                
                if (list != null)
                {
                    ProductsInFormedProduct = list;
                }
            } 
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }

        private bool CanRegister(object parameter) => true;

        public ICommand AddProductDataBaseCommand => _addProudctDataBaseCommand;

        private void AddProductDataBase()
        {
            if(_model.AddProduct(Name,Code,Articule,Price,Count,Units.ElementAt(SelcetUnits),ProductsInFormedProduct))
            {
                MessageBox.Show("товар добавлено");
            }
        }

    }
}
