﻿using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class CreateGoodsViewModel : ViewModel<CreateGoodsViewModel>
    {
        private readonly ICommand _saveGoodsCommand;
        private readonly ICommand _clearWindowCommand;

        CreateGoodsModel _model;

        public CreateGoodsViewModel()
        {
            _model = new CreateGoodsModel();
            Units = new List<string>();

            _saveGoodsCommand = new DelegateCommand(SaveAndCreateGoodsDataBase);
            _clearWindowCommand = new DelegateCommand(ClearTextWindow);

            _code = string.Empty;
            _name = string.Empty;
            _articule = string.Empty;
            _units = null;
            _selectUnits = string.Empty;
            _selectCodeUKTZED = string.Empty;

            _price = 0m;
            _count = 0m;

            Units = _model.GetUnitList();
            CodeUKTZED = _model.GetCodeUKTZEDList();
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
            set { _articule = value; OnPropertyChanged("Articule"); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price= value; OnPropertyChanged("Price"); } 
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
            set { _units = value; }
        }

        private string _selectUnits;
        public string SelectUnits
        {
            get { return _selectUnits; }
            set { _selectUnits = value; }
        }

        private List<string>? _codeUKTZED;
        public List<string>? CodeUKTZED
        {
            get { return _codeUKTZED; }
            set { _codeUKTZED = value; }
        }
        private string _selectCodeUKTZED;
        public string SelcetCodeUKTZED
        {
            get { return _selectCodeUKTZED; }
            set { _selectCodeUKTZED = value; }
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

        public ICommand SaveGoodsCommand => _saveGoodsCommand;

        private void SaveAndCreateGoodsDataBase()
        {
           if (_model.SaveItemDataBase(_name, _code, _articule, _price, _count, _selectUnits,_selectCodeUKTZED))
           {
                MessageBox.Show("Товар добавлений", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
           }
        }

    }
}
