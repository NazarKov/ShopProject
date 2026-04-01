using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.CompositionRoot.Interface;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Core.Mvvm.Mediator.Notifications;
using ShopProject.Core.Mvvm.Service;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.Discount;
using ShopProject.Model.UI.Product;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.Product.Interface;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.Storage.Product
{
    internal class UpdateProductViewModel : ViewModel<UpdateProductViewModel>, IViewModelLoadResourse , IСontrolView
    {
        private ICommand _saveProductCommand;
        private ICommand _exitWindowCommand;

        private IProductServiсe _productServiсe;
        private IProductUnitServiсe _productUnitServiсe;
        private IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;

        public UpdateProductViewModel(IProductServiсe productServiсe , IProductUnitServiсe productUnitServiсe , IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe)
        {
            _productServiсe = productServiсe;
            _productUnitServiсe = productUnitServiсe;
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
            _units = new List<ProductUnitModel>();
            _codeUKTZED = new List<ProductCodeUKTZEDModel>();

            _saveProductCommand = CreateCommandAsync(UpdateProductDataBase);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });
             
            _selectUnits = 0;
            _statusProduct = new List<string>();
            _selectStatusProduct = 0;
            _error = string.Empty;
            _success = string.Empty;
            _product = new ProductModel(); 

            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            _isEnableSaveButton = true;
        }


        public async Task LoadResourse()
        {
            await SafeExecuteAsync(async () => { 
                await SetField();
            });  
        }

        private async Task SetField()
        {

            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;

            var item = _productServiсe.GetProductOnSession();

            Units = (await _productUnitServiсe.GetFromSession()).ToProductUnitModel().ToList();
            CodeUKTZED = (await _productCodeUKTZEDServiсe.GetFromSession()).ToProductCodeUKTZEDModel().ToList();
            if (StatusProduct.Count == 0)
            {
                StatusProduct = new List<string>(ProductStatusModel.GetProductStatus()); 
            }

            if(item == null)
            {
                throw new Exception("Невдалося завантажити товар");
            }
            if (item.ID != new Guid())
                Product.ID = item.ID;
            if (item.Code != null)
                Product.Code = item.Code;
            if (item.NameProduct != null)
                Product.NameProduct = item.NameProduct;
            if (item.Articule != null)
                Product.Articule = item.Articule;
            if (item.Price != 0)
                Product.Price = item.Price;
            if (item.Count != 0)
                Product.Count = item.Count;
            if (item.Unit != null && item.Unit.ShortNameUnit != null)
                SelectUnits = Units.IndexOf(Units.Where(i => i.NameUnit == item.Unit.NameUnit).First());
            if (item.CodeUKTZED != null)
                SelectCodeUKTZED = CodeUKTZED.IndexOf(CodeUKTZED.Where(i => i.NameCode == item.CodeUKTZED.NameCode).First());

            if (Enum.GetValues<TypeStatusProduct>().Where(s => s == item.Status).Any())
            {
                _selectStatusProduct = Enum.GetValues<TypeStatusProduct>().ToList()
                    .IndexOf(Enum.GetValues<TypeStatusProduct>().Where(s => s == item.Status).First());
            }
        }
        private ProductModel _product;
        public ProductModel Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        } 

        private List<ProductUnitModel> _units;
        public List<ProductUnitModel> Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(Units)); }
        }

        private int _selectUnits;
        public int SelectUnits
        {
            get { return _selectUnits; }
            set { _selectUnits = value; OnPropertyChanged(nameof(SelectUnits)); }
        }

        private List<ProductCodeUKTZEDModel> _codeUKTZED;
        public List<ProductCodeUKTZEDModel> CodeUKTZED
        {
            get { return _codeUKTZED; }
            set { _codeUKTZED = value; OnPropertyChanged(nameof(CodeUKTZED)); }
        }
        private int _selectCodeUKTZED;
        public int SelectCodeUKTZED
        {
            get { return _selectCodeUKTZED; }
            set { _selectCodeUKTZED = value; OnPropertyChanged(nameof(SelectCodeUKTZED)); }
        }

        private List<string> _statusProduct;
        public List<string> StatusProduct
        {
            get { return _statusProduct; }
            set { _statusProduct = value; OnPropertyChanged(nameof(StatusProduct)); }
        }
        private int _selectStatusProduct;
        public int SelectStatusProduct
        {
            get { return _selectStatusProduct; }
            set { _selectStatusProduct = value; OnPropertyChanged(nameof(SelectStatusProduct)); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }

        private string _success;
        public string Success
        {
            get { return _success; }
            set { _success = value; OnPropertyChanged(nameof(Success)); }
        }

        private Visibility _successTextBlockVisibiliti;
        public Visibility SuccessTextBlockVisibiliti
        {
            get { return _successTextBlockVisibiliti; }
            set { _successTextBlockVisibiliti = value; OnPropertyChanged(nameof(SuccessTextBlockVisibiliti)); }
        }

        private Visibility _errorTextBlockVisibiliti;
        public Visibility ErrorTextBlockVisibiliti
        {
            get { return _errorTextBlockVisibiliti; }
            set { _errorTextBlockVisibiliti = value; OnPropertyChanged(nameof(ErrorTextBlockVisibiliti)); }
        }

        private bool _isEnableSaveButton;
        public bool IsEnableSaveButton
        {
            get { return _isEnableSaveButton; }
            set { _isEnableSaveButton = value; OnPropertyChanged(nameof(IsEnableSaveButton)); } 
        } 
        public ICommand ExitWindowCommand => _exitWindowCommand; 

        public ICommand SaveProductCommand => _saveProductCommand;

        public Action? CloseView { get; set; }

        private async Task UpdateProductDataBase()
        {
            _isEnableSaveButton = false;

            if (await _productServiсe.Update(new ProductModel()
            {
                ID = Product.ID,
                NameProduct = Product.NameProduct,
                Code = Product.Code,
                Articule = Product.Articule,
                Price = Product.Price,
                Count = Product.Count,
                Unit = Units.ElementAt(_selectUnits),
                CodeUKTZED = CodeUKTZED.ElementAt(_selectCodeUKTZED),
                Discount = new DiscountModel(),
                Status = Enum.GetValues<TypeStatusProduct>().ElementAt(_selectStatusProduct)
            }.ToProduct()))
            {

                Success = "Товар редаговано";
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                SuccessTextBlockVisibiliti = Visibility.Visible;
                _isEnableSaveButton = true;
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Товар", "Товар редаговано")));
            }
        }
    }
}
