using ShopProject.DataBase.Model;
using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ShopProject.Model.ToolsPage;
using System.Threading;
using ShopProject.Model;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class UpdateProductViewModel : ViewModel<UpdateProductViewModel>
    {
        private ICommand saveProduct;

        private UpdateProductModel update;
        private Product? product;

        public UpdateProductViewModel()
        {
            update = new UpdateProductModel();
            saveProduct = new DelegateCommand(UpdateProductDataBase);

            if (StaticResourse.product != null)
                product = StaticResourse.product;

            _code = string.Empty;
            _name = string.Empty;
            _articule = string.Empty;
            _units = null;
            _selectUnits = string.Empty;


            SetFieldComboBox();
            SetFieldText();
            ClearResourses();
        }
        private void SetFieldText()
        {
            if (product != null)
            {
                if (product.code != null)
                    Code = product.code;
                if (product.name != null)
                    Name = product.name;
                if (product.articule != null)
                    Articule = product.articule;
                 if (product.price != null)
                    Price = (double)product.price;
                if (product.startingPrise != null)
                    StartingPrise = (double)product.startingPrise;
                if (product.count != null)
                    Count = (int)product.count;
                if (product.units != null)
                    SelectUnits = product.units;
            }
          
        }
        private void SetFieldComboBox()
        {
            Units = new List<string>();
            Units.Add("Шт");
            Units.Add("кг");
            Units.Add("пачка");
            Units.Add("ящик");
        }
        private void ClearResourses()
        {
            StaticResourse.product = null;
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
        private string _articule;
        public string Articule
        {
            get { return _articule; }
            set { _articule=value; OnPropertyChanged("Articule"); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        private double _startingPrise;
        public double StartingPrise
        {
            get { return _startingPrise; }
            set { _startingPrise = value; OnPropertyChanged("StartingPrise"); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged("Count"); }
        }

        private List<string>? _units;
        public List<string>? Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged("Units"); }
        }

        private string _selectUnits;
        public string SelectUnits
        {
            get { return _selectUnits; }
            set { _selectUnits = value; OnPropertyChanged("SelectUnits"); }
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;


        public ICommand SaveProduct => saveProduct;

        private void UpdateProductDataBase()
        {
            if (update.UpdateProduct(_name, _code, _articule, _price, _startingPrise, _count, _selectUnits))
            {
                if (update.SaveProduct())
                {
                    MessageBox.Show("товар редаговано");
                }
                else
                {
                    MessageBox.Show("помилка редагування");
                }
            }
        }
    }
}
