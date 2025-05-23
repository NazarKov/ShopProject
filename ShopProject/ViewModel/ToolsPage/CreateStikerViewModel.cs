﻿using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class CreateStickerViewModel : ViewModel<CreateStickerViewModel>
    {
        private ICommand createStikerCommand;
        private ICommand printStikerCommand;
        private ICommand clearStikerCommand;
        private CreateStickerModel model;

        public CreateStickerViewModel()
        {
            model = new CreateStickerModel();

            createStikerCommand = new DelegateCommand(CreateStiker);
            printStikerCommand = new DelegateCommand(Print);
            clearStikerCommand = new DelegateCommand(ClearField);

            _nameProduct = string.Empty;
            _nameCompany = string.Empty;
            _code = string.Empty;
            _description = string.Empty;
            _barCode = new BitmapImage();

            IsShowNameCompany = true;
            IsShowProductBarCode = true;
            IsShowProductName = true;
            IsShowProductDescription = true;

            SetFeildTextBox();
        }
        private void SetFeildTextBox()
        {

            var item = Session.Product;
            //NameCompany = Session.NameCompany;
            if (item == null)
            {

                if (AppSettingsManager.GetParameterFiles("LastBarCode") != string.Empty)
                {
                    Code = AppSettingsManager.GetParameterFiles("LastBarCode").ToString();
                }
            }
            else
            {
                if (item.Code != null)
                    Code = item.Code.ToString();
                if (item.NameProduct != null)
                {
                    string[] splitName = item.NameProduct.ToString().Split(' ');
                    if (splitName.Length < 2)
                    {
                        NameProduct = splitName[0];
                        IsShowProductDescription = false;
                    }
                    else if (splitName.Length == 2)
                    {
                        NameProduct = splitName[0] + "  " + splitName[1];
                        IsShowProductDescription = false;
                    }
                    else
                    {
                        NameProduct = splitName[0] + splitName[1];
                        for (int i = 2; i < splitName.Length; i++)
                            Description += splitName[i] + " ";
                    }
                }

            }
        }

        private string _nameProduct;
        public string NameProduct
        {
            get { return _nameProduct; }
            set { _nameProduct = value; OnPropertyChanged("NameProduct"); }
        }

        private string _nameCompany;
        public string NameCompany
        {
            get { return _nameCompany; }
            set { _nameCompany = value; OnPropertyChanged("NameCompany"); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged("Code"); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        private BitmapImage _barCode;
        public BitmapImage BarCode
        {
            get { return _barCode; }
            set { _barCode = value; OnPropertyChanged("BarCode"); }
        }

        private bool _isShowNameCompany;
        public bool IsShowNameCompany
        {
            get { return _isShowNameCompany; }
            set { _isShowNameCompany = value; OnPropertyChanged("IsShowNameCompany"); }
        }

        private bool _isShowProductBarCode;
        public bool IsShowProductBarCode
        {
            get { return _isShowProductBarCode; }
            set { _isShowProductBarCode = value; OnPropertyChanged("IsShowProductBarCode"); }
        }
        private bool _isShowProductName;
        public bool IsShowProductName
        {
            get { return _isShowProductName; }
            set { _isShowProductName = value; OnPropertyChanged("IsShowProductName"); }
        }
        private bool _isShowProductDescription;
        public bool IsShowProductDescription
        {
            get { return _isShowProductDescription; }
            set { _isShowProductDescription = value; OnPropertyChanged("IsShowProductDescription"); }
        }

        public ICommand CreateStikerComman => createStikerCommand;

        private void CreateStiker()
        {
            model.SetShowTextInImage(_isShowNameCompany, _isShowProductBarCode, _isShowProductName, _isShowProductDescription);
            BarCode = model.CreateBarCode(_nameCompany, _nameProduct, _description, _code);
        }

        public ICommand PrintStikerCommand => printStikerCommand;

        private void Print()
        {
            model.Print();
        }

        public ICommand ClearStikerCommand => clearStikerCommand;

        private void ClearField()
        {
            Code = string.Empty;
            NameCompany = string.Empty;
            NameProduct = string.Empty;
            Description = string.Empty;
            BarCode = model.Clear();
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

    }
}