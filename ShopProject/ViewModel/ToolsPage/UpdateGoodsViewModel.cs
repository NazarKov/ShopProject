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
using ShopProject.Helpers;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class UpdateGoodsViewModel : ViewModel<UpdateGoodsViewModel>
    {
        private ICommand _saveProductCommand;

        private UpdateGoodsModel _model;
        private Goods? _goods;

        public UpdateGoodsViewModel()
        {
            _model = new UpdateGoodsModel();
            _goods = new Goods();

            _saveProductCommand = new DelegateCommand(UpdateGoodsDataBase);

            if (StaticResourse.goods != null)
                _goods = StaticResourse.goods;

            _code = string.Empty;
            _name = string.Empty;
            _articule = string.Empty;
            _units = null;
            _selectUnits = string.Empty;

            Units = new List<string>();
            CodeUKTZED = new List<string>();
            ClearResourses();
            SetFieldText();
            new Thread(new ThreadStart(setFiledWindow)).Start();
        }

        private void setFiledWindow()
        {
            Units = _model.GetUnitList();
            CodeUKTZED = _model.GetCodeUKTZEDList();
            
        }

        private Guid _id;

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

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        private decimal _count;
        public decimal Count
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
            set { _selectUnits = value;  }
        }

        private List<string> _codeUKTZED;
        public List<string> CodeUKTZED
        {
            get { return _codeUKTZED; }
            set { _codeUKTZED = value; OnPropertyChanged("CodeUKTZED"); }
        }
        private string? _selectCodeUKTZED;
        public string? SelcetCodeUKTZED
        {
            get { return _selectCodeUKTZED; }
            set { _selectCodeUKTZED = value;}
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
            if (_goods != null)
            {
                if (_goods.id != null)
                    _id = _goods.id;
                if (_goods.code != null)
                    Code = _goods.code;
                if (_goods.name != null)
                    Name = _goods.name;
                if (_goods.articule != null)
                    Articule = _goods.articule;
                if (_goods.price != null)
                    Price = (decimal)_goods.price;
                if (_goods.count != null)
                    Count = (decimal)_goods.count;
                if (_goods.unit.shortName != null)
                    SelectUnits = _goods.unit.shortName;
                if (_goods.codeUKTZED != null)
                    SelcetCodeUKTZED = _goods.codeUKTZED.name;
            }
        }
        private void ClearResourses()
        {
            StaticResourse.goods = null;
        }

        public ICommand SaveProductCommand => _saveProductCommand;

        private void UpdateGoodsDataBase()
        {
            new Thread(new ThreadStart(() => {
                if (_model.UpdateItemDataBase(_id,_name, _code, _articule, _price, _count, _selectUnits,_selectCodeUKTZED))
                {
                    MessageBox.Show("Товар редаговано","Інформація",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            })).Start();
        }
    }
}
