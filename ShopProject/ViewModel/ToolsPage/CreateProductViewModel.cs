
using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class CreateProductViewModel : ViewModel<CreateProductViewModel>
    {
        private readonly ICommand _saveProductCommand;
        private readonly ICommand _clearWindowCommand;

        CreateProductModel _model;

        public CreateProductViewModel()
        {
            _model = new CreateProductModel();

            Units = new List<ProductUnitEntity>();
            CodeUKTZED = new List<CodeUKTZEDEntity>();



            _saveProductCommand = new DelegateCommand(SaveAndCreateProductDataBase);
            _clearWindowCommand = new DelegateCommand(ClearTextWindow);

            _code = string.Empty;
            _name = string.Empty;
            _article = string.Empty;
            _units = null;
            _selectUnitsIndex = 0;
            _selectCodeUKTZEDIndex = 0;
            _price = 0m;
            _count = 0m;
            setFiledWindow();


           
        }
        private void setFiledWindow()
        {
            Task t = Task.Run(async () => {

                var units = await _model.GetUnits();
                Units = units.ToList();
                var codeUKTZED = await _model.GetCodeUKTZED();
                CodeUKTZED = codeUKTZED.ToList();
            });
        }


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

        private string _article;
        public string Article
        {
            get { return _article; }
            set { _article = value; OnPropertyChanged(nameof(Article)); }
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

        private List<ProductUnitEntity>? _units;
        public List<ProductUnitEntity>? Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(Units)); }
        }

        private int _selectUnitsIndex;
        public int SelectUnitIndex
        {
            get { return _selectUnitsIndex; }
            set { _selectUnitsIndex = value; }
        }

        private List<CodeUKTZEDEntity>? _codeUKTZED;
        public List<CodeUKTZEDEntity>? CodeUKTZED
        {
            get { return _codeUKTZED; }
            set { _codeUKTZED = value; OnPropertyChanged(nameof(CodeUKTZED)); }
        }
        private int _selectCodeUKTZEDIndex;
        public int SelectCodeUKTZEDIndex
        {
            get { return _selectCodeUKTZEDIndex; }
            set { _selectCodeUKTZEDIndex = value; }
        }


        public ICommand ExitWindowCommand { get => new DelegateParameterCommand(WindowClose,CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

        
        public ICommand ClearWindowCommand => _clearWindowCommand;

        private void ClearTextWindow()
        {
            Name = string.Empty;
            Code = string.Empty;
            Count = 0m;
            Price = 0m;
        }

        public ICommand SaveProductCommand => _saveProductCommand;

        private void SaveAndCreateProductDataBase()
        {
            Task t = Task.Run(async () =>
            {
                if (await _model.SaveItemDataBase(new ProductEntity()
                {
                    NameProduct = _name,
                    Code = _code,
                    Articule = _article,
                    Price = _price,
                    Count = _count,
                    Unit = _units.ElementAt(_selectUnitsIndex),
                    CodeUKTZED = _codeUKTZED.ElementAt(_selectCodeUKTZEDIndex),
                    CreatedAt = DateTime.Now,
                    Status = TypeStatusProduct.InStock,
                })) ;
                {
                    MessageBox.Show("Товар добавлений", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                    Mediator.Notify("ReloadProduct", "");
                }
            });
        }

    }
}
