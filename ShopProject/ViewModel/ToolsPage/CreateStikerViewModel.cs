using ShopProject.Helpers;
using ShopProject.Helpers.Command;
using ShopProject.Model.ToolsPage;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class CreateStickerViewModel : ViewModel<CreateStickerViewModel>
    {
        private ICommand _createStikerCommand;
        private ICommand _printStikerCommand;
        private ICommand _clearStikerCommand;
        private CreateStickerModel model;

        public CreateStickerViewModel()
        {
            model = new CreateStickerModel();

            _createStikerCommand = new DelegateCommand(CreateStiker);
            _printStikerCommand = new DelegateCommand(Print);
            _clearStikerCommand = new DelegateCommand(ClearField);

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
            var namecompany = AppSettingsManager.GetParameterFiles("NameCompany").ToString();
            if (namecompany != null && namecompany != string.Empty) 
            {
                NameCompany = namecompany;
            }

            var item = Session.Product;
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
            else
            {
                var cert = Session.GiftCertificate;
                if (cert != null)
                {
                    Code = cert.Code;
                    NameProduct = cert.Name;
                    Description = cert.Description;
                }
            } 

        }

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
            model.SetShowTextInImage(_isShowNameCompany, _isShowProductBarCode, _isShowProductName, _isShowProductDescription);
            BarCode = model.CreateBarCode(_nameCompany, _nameProduct, _description, _code);
            
            var namecompany = AppSettingsManager.GetParameterFiles("NameCompany").ToString();
            if (namecompany != null && namecompany == string.Empty)
            {
                AppSettingsManager.SetParameterFile("NameCompany", NameCompany);
            }
        }

        public ICommand PrintStikerCommand => _printStikerCommand;

        private void Print()
        {
            model.Print();
        }

        public ICommand ClearStikerCommand => _clearStikerCommand;

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