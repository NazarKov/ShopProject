using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.Model.ToolsPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class FormationProductViewModel : ViewModel<FormationProductViewModel>
    {
        private ICommand _addProductDataBaseCommand;
        private ICommand _searchBarCodeProductCommand;

        private FormationProductModel _model;
        private List<ProductEntiti> _productsSelectGridView;

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
            _selectUnits = 0;
            _ProductList = new List<ProductEntiti>();
            _productsSelectGridView = new List<ProductEntiti>();

            _addProductDataBaseCommand = new DelegateCommand(AddProductDataBase);
            _searchBarCodeProductCommand = new DelegateCommand(SearchProduct);

            _ProductList = new List<ProductEntiti>();
            
            new Thread(new ThreadStart(SetFieldComboBox)).Start();
        }
        private void SetFieldComboBox()
        {
            Units = _model.GetUnitList();
            CodeUKTZED = _model.GetCodeUKTZEDList();
            
            SelectUnits = 0;
            SelectCodeUKTZED = 0;
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

        private int _selectUnits;
        public int SelectUnits
        {
            get { return _selectUnits; }
            set { _selectUnits = value;OnPropertyChanged("SelectUnits"); }
        }

        private List<string> _codeUKTZED;
        public List<string> CodeUKTZED
        {
            get { return _codeUKTZED; }
            set { _codeUKTZED = value; OnPropertyChanged("CodeUKTZED"); }
        }
        private int _selectCodeUKTZED;
        public int SelectCodeUKTZED
        {
            get { return _selectCodeUKTZED; }
            set { _selectCodeUKTZED = value; OnPropertyChanged("SelectCodeUKTZED"); }
        }

        private List<ProductEntiti> _ProductList;
        public List<ProductEntiti> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged("ProductList"); }
        }

        public ICommand SearchBarCodeProductCommand => _searchBarCodeProductCommand;

        private void SearchProduct()
        {
            List<ProductEntiti> temp;
            if (SearchCode.Length > 12)
            {
                if (SearchCode != "0000000000000")//винести в настройки
                {
                    var item = _model.Search(SearchCode);
                    if (item != null)
                    {
                        item.Count = 1;
                        temp = new List<ProductEntiti>();
                        temp = ProductList;

                        if (temp.Find(pr => pr.Code == item.Code) != null)
                        {
                            temp.Find(pr => pr.Code == item.Code).Count += 1;
                        }
                        else
                        {
                            temp.Add(item);
                        }

                        ProductList = new List<ProductEntiti>();
                        ProductList = temp;
                        SearchCode = string.Empty;
                    }
                }
                else
                {
                    if (ProductList.Count() != 0)
                    {
                        if (ProductList.ElementAt(ProductList.Count - 1).Count == 1)
                        {
                            temp = new List<ProductEntiti>();
                            temp = ProductList;

                            temp.Remove(temp.ElementAt(temp.Count - 1));
                            ProductList = new List<ProductEntiti>();
                            ProductList = temp;
                        }
                        else
                        {
                            temp = new List<ProductEntiti>();
                            temp = ProductList;


                            temp.ElementAt(ProductList.Count - 1).Count -= 1;
                            ProductList = new List<ProductEntiti>();
                            ProductList = temp;
                        }
                        SearchCode = string.Empty;
                    }
                }
            }
        }

        public ICommand UpdateProductsInFormedProductCommand { get => new DelegateParameterCommand(UpdateProductsInFormedProduct, CanRegister); }
       
        private void UpdateProductsInFormedProduct(object parameter)
        {
            _productsSelectGridView = new List<ProductEntiti>();
            if (_model != null)
            {
                _model.ContertToListProduct((IList)parameter, _productsSelectGridView);
                
                var list = _model.UpdateList(_ProductList, _productsSelectGridView);
                
                if (list != null)
                {
                    ProductList = list;
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

        public ICommand AddProductDataBaseCommand => _addProductDataBaseCommand;

        private void AddProductDataBase()
        {
            if(_model.AddProduct(Name,Code,Articule,(decimal)Price,Count,Units.ElementAt(SelectUnits),CodeUKTZED.ElementAt(SelectCodeUKTZED),ProductList))
            {
                MessageBox.Show("Товар добавлено", "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
