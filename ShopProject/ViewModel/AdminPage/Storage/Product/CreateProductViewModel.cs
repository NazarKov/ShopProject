using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Discount;
using ShopProject.Model.UI.Product;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Mapping.Product;
using ShopProject.Services.Modules.Mapping.ProductCodeUKTZED;
using ShopProject.Services.Modules.Mapping.ProductUnit; 
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ShopProject.Services.Modules.Domain.ProductUnit.Interface;
using ShopProject.Services.Modules.Domain.Product.Interface;

namespace ShopProject.ViewModel.AdminPage.Storage.Product
{
    internal class CreateProductViewModel : ViewModel<CreateProductViewModel>, IViewModelLoadResourse , IСontrolView
    {
        private readonly ICommand _saveProductCommand;
        private readonly ICommand _clearWindowCommand;
        private readonly ICommand _exitWindowCommand;

        private readonly IProductServiсe _productServiсe;
        private readonly IProductUnitServiсe _productUnitServiсe;
        private readonly IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;
        public CreateProductViewModel(IProductServiсe productService , IProductUnitServiсe productUnitService , IProductCodeUKTZEDServiсe productCodeUKTZEDService )
        {
            _productServiсe = productService;
            _productUnitServiсe = productUnitService;
            _productCodeUKTZEDServiсe = productCodeUKTZEDService;

            _saveProductCommand = CreateCommandAsync(SaveAndCreateProductDataBase,SetError);
            _clearWindowCommand = CreateCommand(ClearTextWindow);
            _exitWindowCommand = CreateCommand(()=> { CloseView?.Invoke(); }); 
 
            _product = new ProductModel();
            _units = new List<ProductUnitModel>();
            _codeUKTZED = new List<ProductCodeUKTZEDModel>();
            _selectUnitsIndex = 0;
            _selectCodeUKTZEDIndex = 0; 
            _error = string.Empty;
            _success = string.Empty;
            _isEnableSaveButton = true;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed; 
        }  
        public Action? CloseView { get; set; }

        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFiledWindow); 
        }

        private async Task SetFiledWindow()
        { 
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;


            var units = (await _productUnitServiсe.GetFromSession()).ToProductUnitModel();
            var favoriteUnit = units.Where(u => u.Status == TypeStatusUnit.Favorite);
            if (favoriteUnit.Any())
            {
                Units = new List<ProductUnitModel>(favoriteUnit);
            }
            else
            {
                Units = new List<ProductUnitModel>(units); 
            }

            var codeUKTZED = (await _productCodeUKTZEDServiсe.GetFromSession()).ToProductCodeUKTZEDModel();
            var favoriteCodeUKTZED = codeUKTZED.Where(c => c.Status == TypeStatusCodeUKTZED.Favorite);
            if (favoriteCodeUKTZED.Any()) 
            {
                CodeUKTZED = new List<ProductCodeUKTZEDModel>(favoriteCodeUKTZED);
            }
            else
            {
                CodeUKTZED = new List<ProductCodeUKTZEDModel>(codeUKTZED);
            } 
        }
        private ProductModel _product;
        public ProductModel Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(nameof(Product));}
        } 

        private List<ProductUnitModel> _units;
        public List<ProductUnitModel> Units
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

        private List<ProductCodeUKTZEDModel> _codeUKTZED;
        public List<ProductCodeUKTZEDModel> CodeUKTZED
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
        public ICommand ClearWindowCommand => _clearWindowCommand;

        private void ClearTextWindow()
        {
            Product = new ProductModel();
        }
        public ICommand SaveProductCommand => _saveProductCommand;

        private async Task SaveAndCreateProductDataBase()
        {
            if (!_units.Any() || !_codeUKTZED.Any())
            {
                throw new Exception("Невдалося завантажити ресурси");
            }

            IsEnableSaveButton = false;

            var result = await _productServiсe.Add(new ProductModel()
            {
                NameProduct = _product.NameProduct,
                Code = _product.Code,
                Articule = _product.Articule,
                Price = _product.Price,
                Count = _product.Count,
                Unit = _units[_selectUnitsIndex],
                CodeUKTZED = _codeUKTZED[_selectCodeUKTZEDIndex],
                Status = TypeStatusProduct.InStock,
                Discount = new DiscountModel(),
            }.ToProduct()); 

            if (result.IsSuccess)
            {
                SetSuccess(result.Data.NameProduct);
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Товар", "Товар успішно створений в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadProduct.ToString());

            }
            else if (result.IsError)
            {
                SetError(result.ErrorMessage);
            }
            else
            {
                SetError("Невдалося виконати операцію");
            }

            IsEnableSaveButton = true;
        }
        private void SetError(string error)
        { 
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string name)
        {
            Success = $"Товар {name} добавлений";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible; 
        }

    } 
}
