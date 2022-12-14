using ShopProject.DataBase.Model;
using ShopProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ShopProject.Model.ToolsPage;
using System.Threading;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class UpdateProductViewModel : ViewModel<UpdateProductViewModel>
    {
        private ICommand saveProduct;

        UpdateProductModel update;
        Product product;

        public UpdateProductViewModel()
        {
            product = ResourseProductModel.product;
            update = new UpdateProductModel();
            if(product.code!=null)
            Code = product.code;
            if (product.name != null)
                Name = product.name;
            if (product.description != null)
                Description = product.description;
            if (product.price != null)
                Price = (double)product.price;
            if (product.purchase_prise != null)
                PurchasePrice = (double)product.purchase_prise;
            if (product.count != null)
                Count = (int)product.count;

            ResourseProductModel.product = null;

            saveProduct = new DelegateCommand(saveProductDb);

            Units = new List<string>();
            Units.Add("Шт");
            Units.Add("кг");
            Units.Add("пачка");
            Units.Add("ящик");

            SelectUnits = product.units;

         
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
            get { return _description; }
        }

        private double _price;
        public double Price
        {
            set { _price = value; OnPropertyChanged("Price"); }
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
            get { return _selectUnits; }
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }

        private bool CanRegister(object parameter) => true;

        public ICommand SaveProduct => saveProduct;

        private void saveProductDb()
        {
            if (update.updateproduct(_name, _code, _description, _price, _purchase_price, _count, _selectUnits))
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
