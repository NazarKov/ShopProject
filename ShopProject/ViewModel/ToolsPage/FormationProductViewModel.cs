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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class FormationProductViewModel : ViewModel<FormationProductViewModel>
    {
        private ICommand _addGoodsDataBaseCommand;
        private ICommand _searchBarCodeGoodsCommand;

        private FormationProductModel _model;
        private List<Goods> _productsSelectGridView;

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
            _goodsList = new List<Goods>();
            _productsSelectGridView = new List<Goods>();

            _addGoodsDataBaseCommand = new DelegateCommand(AddProductDataBase);
            _searchBarCodeGoodsCommand = new DelegateCommand(SearchGoods);

            _goodsList = new List<Goods>();
            
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

        private List<Goods> _goodsList;
        public List<Goods> GoodsList
        {
            get { return _goodsList; }
            set { _goodsList = value; OnPropertyChanged("GoodsList"); }
        }

        public ICommand SearchBarCodeGoodsCommand => _searchBarCodeGoodsCommand;

        private void SearchGoods()
        {
            List<Goods> temp;
            if (SearchCode.Length > 12)
            {
                if (SearchCode != "0000000000000")//винести в настройки
                {
                    var item = _model.Search(SearchCode);
                    if (item != null)
                    {
                        item.count = 1;
                        temp = new List<Goods>();
                        temp = GoodsList;

                        if (temp.Find(pr => pr.code == item.code) != null)
                        {
                            temp.Find(pr => pr.code == item.code).count += 1;
                        }
                        else
                        {
                            temp.Add(item);
                        }

                        GoodsList = new List<Goods>();
                        GoodsList = temp;
                        SearchCode = string.Empty;
                    }
                }
                else
                {
                    if (GoodsList.Count() != 0)
                    {
                        if (GoodsList.ElementAt(GoodsList.Count - 1).count == 1)
                        {
                            temp = new List<Goods>();
                            temp = GoodsList;

                            temp.Remove(temp.ElementAt(temp.Count - 1));
                            GoodsList = new List<Goods>();
                            GoodsList = temp;
                        }
                        else
                        {
                            temp = new List<Goods>();
                            temp = GoodsList;


                            temp.ElementAt(GoodsList.Count - 1).count -= 1;
                            GoodsList = new List<Goods>();
                            GoodsList = temp;
                        }
                        SearchCode = string.Empty;
                    }
                }
            }
        }

        public ICommand UpdateProductsInFormedProductCommand { get => new DelegateParameterCommand(UpdateProductsInFormedProduct, CanRegister); }
       
        private void UpdateProductsInFormedProduct(object parameter)
        {
            _productsSelectGridView = new List<Goods>();
            if (_model != null)
            {
                _model.ContertToListProduct((IList)parameter, _productsSelectGridView);
                
                var list = _model.UpdateList(_goodsList, _productsSelectGridView);
                
                if (list != null)
                {
                    GoodsList = list;
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

        public ICommand AddGoodsDataBaseCommand => _addGoodsDataBaseCommand;

        private void AddProductDataBase()
        {
            if(_model.AddProduct(Name,Code,Articule,(decimal)Price,Count,Units.ElementAt(SelectUnits),CodeUKTZED.ElementAt(SelectCodeUKTZED),GoodsList))
            {
                MessageBox.Show("Товар добавлено", "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
