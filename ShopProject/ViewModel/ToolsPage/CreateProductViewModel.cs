﻿using ShopProject.Model;
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
    internal class CreateProductViewModel : ViewModel<CreateProductViewModel>
    {
        private readonly ICommand saveProduct;
        private readonly ICommand clearWindow;

        CreateProductModel productModel;

        public CreateProductViewModel()
        {
            productModel = new CreateProductModel();

            saveProduct = new DelegateCommand(SaveAndCreateProductDataBase);
            clearWindow = new DelegateCommand(ClearTextWindow);

            _code = string.Empty;
            _name = string.Empty;
            _articule = string.Empty;
            _description = string.Empty;
            _units = null;
            _selectUnits = string.Empty;

            SetFieldComboBox();
        }

        private void SetFieldComboBox()
        {
            Units = new List<string>();
            Units.Add("Шт");
            Units.Add("кг");
            Units.Add("пачка");
            Units.Add("ящик");
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

        private string _description;
        public string Description
        {
            get{ return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price= value; OnPropertyChanged("Price"); } 
        }

        private double _purchase_price;
        public double PurchasePrice
        {
            get { return _purchase_price; }
            set { _purchase_price = value; OnPropertyChanged("PurchasePrice"); }
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
            get { return _selectUnits;}
            set { _selectUnits = value; OnPropertyChanged("SelectUnits"); }
        }

    
        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose,CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

        
        public ICommand ClearWindow => clearWindow;

        private void ClearTextWindow()
        {
            Name = string.Empty;
            Description = string.Empty;
            Code = string.Empty;
            Count = 0;
            Price = 0;
            PurchasePrice = 0;
        }

        public ICommand SaveProduct => saveProduct;

        private void SaveAndCreateProductDataBase()
        {
            if (CreateProduct())
            {
                if (productModel.SaveItemDataBase())
                {
                    MessageBox.Show("товар доблений");
                }
                else
                {
                    MessageBox.Show("помилка добавлення");
                }
            }
        }
            private bool CreateProduct()
        {
            return productModel.CreateNewProduct(_name, _code, _articule, _description, _price, _purchase_price, _count, _selectUnits);
        } 

    }
}
