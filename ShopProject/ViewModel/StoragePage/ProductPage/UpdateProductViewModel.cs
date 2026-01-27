using ShopProject.Helpers.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Threading;
using ShopProject.Helpers;
using ShopProjectDataBase.Entities;
using ShopProject.Model.StoragePage.ProductsPage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Helper;

namespace ShopProject.ViewModel.StoragePage.ProductPage
{
    internal class UpdateProductViewModel : ViewModel<UpdateProductViewModel>
    {
        private ICommand _saveProductCommand;

        private UpdateProductModel _model;
        private Product? _products;

        public UpdateProductViewModel()
        {
            _model = new UpdateProductModel();
            _products = new Product();
            _units = new List<ProductUnit>();
            _codeUKTZED = new List<ProductCodeUKTZED>();

            _saveProductCommand = new DelegateCommand(UpdateProductDataBase);

            _code = string.Empty;
            _name = string.Empty;
            _articule = string.Empty;
            _units = null;
            _selectUnits = 0;
            _statusProduct = new List<string>();
            _selectStatusProduct = 0;
            SetFiledWindow();
        }

        private void SetFiledWindow()
        {
            var item = Session.Product;
            if (item != null)
                _products = Session.Product;

            Units = Session.ProductUnits.ToList();
            CodeUKTZED = Session.ProductCodesUKTZED.ToList();
           
            StatusProduct.Add("Весь товар");
            StatusProduct.Add("Готовий до продажі");
            StatusProduct.Add("Товар закінчився");
            StatusProduct.Add("Товар aрхівовано");
            StatusProduct.Add("Завантажено з Excel порібно редагування");

            SetFieldText();
        }

        private Guid _id;

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        private string _articule;
        public string Articule
        {
            get { return _articule; }
            set { _articule = value; OnPropertyChanged(nameof(Articule)); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        private decimal _count;
        public decimal Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(nameof(Count)); }
        }

        private List<ProductUnit>? _units;
        public List<ProductUnit>? Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(Units)); }
        }

        private int _selectUnits;
        public int SelectUnits
        {
            get { return _selectUnits; }
            set { _selectUnits = value; OnPropertyChanged(nameof(SelectUnits)); }
        }

        private List<ProductCodeUKTZED> _codeUKTZED;
        public List<ProductCodeUKTZED> CodeUKTZED
        {
            get { return _codeUKTZED; }
            set { _codeUKTZED = value; OnPropertyChanged(nameof(CodeUKTZED)); }
        }
        private int _selectCodeUKTZED;
        public int SelectCodeUKTZED
        {
            get { return _selectCodeUKTZED; }
            set { _selectCodeUKTZED = value; OnPropertyChanged(nameof(SelectCodeUKTZED)); }
        }

        private List<string> _statusProduct;
        public List<string> StatusProduct
        {
            get { return _statusProduct; }
            set { _statusProduct = value; OnPropertyChanged(nameof(StatusProduct)); }
        }
        private int _selectStatusProduct;
        public int SelectStatusProduct
        {
            get { return _selectStatusProduct; }
            set { _selectStatusProduct = value; OnPropertyChanged(nameof(SelectStatusProduct)); }
        }

        public ICommand ExitWindowCommand { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

        private void SetFieldText()
        {
            if (_products != null)
            {
                if (_products.ID != null)
                    _id = _products.ID;
                if (_products.Code != null)
                    Code = _products.Code;
                if (_products.NameProduct != null)
                    Name = _products.NameProduct;
                if (_products.Articule != null)
                    Articule = _products.Articule;
                if (_products.Price != null)
                    Price = _products.Price;
                if (_products.Count != null)
                    Count = _products.Count;
                if (_products.Unit !=null && _products.Unit.ShortNameUnit != null)
                    SelectUnits = Units.IndexOf(Units.Where(i=>i.NameUnit ==_products.Unit.NameUnit).First());
                if (_products.CodeUKTZED != null)
                    SelectCodeUKTZED = CodeUKTZED.IndexOf(CodeUKTZED.Where(i => i.NameCode == _products.CodeUKTZED.NameCode).First());

                if (Enum.GetValues<TypeStatusProduct>().Where(s => s == _products.Status).Any())
                {
                    _selectStatusProduct = Enum.GetValues<TypeStatusProduct>().ToList()
                        .IndexOf(Enum.GetValues<TypeStatusProduct>().Where(s => s == _products.Status).First());
                }
            }
        } 

        public ICommand SaveProductCommand => _saveProductCommand;

        private void UpdateProductDataBase()
        {
            Task.Run(async () =>
            {

                if (await _model.UpdateItemDataBase(new Product() {
                    ID = _id,
                    NameProduct = _name,
                    Code = _code,
                    Articule = _articule,
                    Price = _price,
                    Count = _count,
                    Unit = Units.ElementAt(_selectUnits),
                    CodeUKTZED = CodeUKTZED.ElementAt(_selectCodeUKTZED),
                    Discount = new Discount(),
                    Status = Enum.GetValues<TypeStatusProduct>().ElementAt(_selectStatusProduct)
                }))
                {
                    MessageBox.Show("Товар редаговано", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
    }
}
