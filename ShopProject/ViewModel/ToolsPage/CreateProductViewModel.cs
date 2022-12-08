using ShopProject.Model;
using ShopProject.Model.ToolsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class CreateProductViewModel : ViewModel<CreateProductViewModel>
    {
        private ICommand saveProduct;
        private ICommand clearWindow;

        CreateProductModel createProductModel;

        public CreateProductViewModel()
        {
            createProductModel = new CreateProductModel();

            saveProduct = new DelegateCommand(addProductDb);
            clearWindow = new DelegateCommand(ClearTextWindow);

            Units = new List<string>();
            Units.Add("Шт");
            Units.Add("кг");
            Units.Add("пачка");
            Units.Add("ящик");


        }

        private string _code;
        public string Code
        {
            set { _code = value; OnPropertyChanged("Code"); }
            get { return _code; }
        }

        private string _name;
        public string Name
        {
            set { _name = value; OnPropertyChanged("Name"); }
            get { return _name; }
        }

        private string _description;
        public string Description
        {
            set { _description = value; OnPropertyChanged("Description"); }
            get{ return _description; }
        }

        private double _price;
        public double Price
        {
            set { _price= value; OnPropertyChanged("Price"); } 
            get { return _price; }
        }

        private double _purchase_price;
        public double PurchasePrice
        {
            set { _purchase_price = value; OnPropertyChanged("PurchasePrice"); }
            get { return _purchase_price; }
        }

        private int _count;
        public int Count 
        {
            set { _count = value; OnPropertyChanged("Count"); }
            get { return _count; }
        }

        private List<string> _units;
        public List<string> Units
        {
            set { _units = value; OnPropertyChanged("Units"); }
            get { return _units; }
        }

        private string _selectUnits;
        public string SelectUnits
        {
            set { _selectUnits = value; OnPropertyChanged("SelectUnits"); }
            get { return _selectUnits;}
        }

    
        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose,CanRegister); }
        private void WindowClose(object parameter)
        {
            Window window = parameter as Window;
            window?.Close();
        }

        private bool CanRegister(object parameter) => true;

        public ICommand ClearWindow => clearWindow;

        void ClearTextWindow()
        {
            Name = null;
            Description = null;
            Code = null;
            Count = 0;
            Price = 0;
            PurchasePrice = 0;

        }

        public ICommand SaveProduct => saveProduct;

        void addProductDb()
        {
            if (createProductModel.addProduct(_name, _code, _description, _price, _purchase_price, _count, _selectUnits))
            {
                MessageBox.Show("товар доблений");
            }
            else
            {
                MessageBox.Show("помилка добавлення");
            }
        }

    }
}
