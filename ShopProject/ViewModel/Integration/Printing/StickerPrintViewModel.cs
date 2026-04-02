using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Modules.ModelService.Product.Interface;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ShopProject.ViewModel.Integration.Printing
{
    internal class StickerPrintViewModel : ViewModel<StickerPrintViewModel> , IViewModelLoadResourse , IСontrolView
    {
        private ICommand _createStikerCommand;
        private ICommand _printStikerCommand;
        private ICommand _clearStikerCommand;
        private ICommand _exitWiendowCommand;

        private IPrintingStikerService _printingStikerService;
        private IProductServiсe _productServiсe;

        public StickerPrintViewModel(IPrintingStikerService printingStikerService,IProductServiсe productServiсe)
        {
            _printingStikerService = printingStikerService;
            _productServiсe = productServiсe;
            _createStikerCommand = CreateCommand(CreateStiker);
            _printStikerCommand = CreateCommand(Print);
            _clearStikerCommand = CreateCommand(ClearField);
            _exitWiendowCommand = CreateCommand(() => { CloseView?.Invoke(); });

            _nameProduct = string.Empty;
            _nameCompany = string.Empty;
            _code = string.Empty;
            _description = string.Empty;
            _barCode = new BitmapImage();

            IsShowNameCompany = true;
            IsShowProductBarCode = true;
            IsShowProductName = true;
            IsShowProductDescription = true; 
        }
        public Task LoadResourse()
        {
            SafeExecute(SetFeildTextBox);
            return Task.CompletedTask;
        }
        private void SetFeildTextBox()
        {
            NameCompany = _printingStikerService.GetNameCompany();

            var item = _productServiсe.GetProductOnSession();
            if (item != null)
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

        public Action? CloseView { get; set; }
        private string _nameProduct;
        public string NameProduct
        {
            get { return _nameProduct; }
            set { _nameProduct = value; OnPropertyChanged(nameof(NameProduct)); }
        }

        private string _nameCompany;
        public string NameCompany
        {
            get { return _nameCompany; }
            set { _nameCompany = value; OnPropertyChanged(nameof(NameCompany)); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private BitmapImage _barCode;
        public BitmapImage BarCode
        {
            get { return _barCode; }
            set { _barCode = value; OnPropertyChanged(nameof(BarCode)); }
        }

        private bool _isShowNameCompany;
        public bool IsShowNameCompany
        {
            get { return _isShowNameCompany; }
            set { _isShowNameCompany = value; OnPropertyChanged(nameof(IsShowNameCompany)); }
        }

        private bool _isShowProductBarCode;
        public bool IsShowProductBarCode
        {
            get { return _isShowProductBarCode; }
            set { _isShowProductBarCode = value; OnPropertyChanged(nameof(IsShowProductBarCode)); }
        }
        private bool _isShowProductName;
        public bool IsShowProductName
        {
            get { return _isShowProductName; }
            set { _isShowProductName = value; OnPropertyChanged(nameof(IsShowProductName)); }
        }
        private bool _isShowProductDescription;
        public bool IsShowProductDescription
        {
            get { return _isShowProductDescription; }
            set { _isShowProductDescription = value; OnPropertyChanged(nameof(IsShowProductDescription)); }
        }

        public ICommand CreateStikerComman => _createStikerCommand;

        private void CreateStiker()
        {
            _printingStikerService.SetShowTextInImage(_isShowNameCompany, _isShowProductBarCode, _isShowProductName, _isShowProductDescription);
            BarCode = _printingStikerService.CreateBarCode(_nameCompany, _nameProduct, _description, _code);
        }

        public ICommand PrintStikerCommand => _printStikerCommand;

        private void Print()
        {
            _printingStikerService.Print();
        }

        public ICommand ClearStikerCommand => _clearStikerCommand;

        private void ClearField()
        {
            Code = string.Empty;
            NameCompany = string.Empty;
            NameProduct = string.Empty;
            Description = string.Empty; 
        }

        public ICommand ExitWindow => _exitWiendowCommand; 
 
    }
}