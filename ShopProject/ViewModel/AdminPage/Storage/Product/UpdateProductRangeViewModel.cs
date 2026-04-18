using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.UI.Product;
using ShopProject.Model.UI.ProductCodeUKTZED;
using ShopProject.Model.UI.ProductUnit;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.Product.Interface;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.Storage.Product
{
    internal class UpdateProductRangeViewModel : ViewModel<UpdateProductRangeViewModel>, IViewModelLoadResourse , IСontrolView
    { 
        private ICommand _updateProductCommand;
        private ICommand _updateProductRangeCommand;
        private ICommand _clearItemForListCommand;
        private ICommand _exitWindowCommand;
        
        private IProductServiсe _productServiсe;
        private IProductUnitServiсe _productUnitServiсe;
        private IProductCodeUKTZEDServiсe _productCodeUKTZEDServiсe;

        public UpdateProductRangeViewModel(IProductServiсe productServiсe,IProductUnitServiсe productUnitServiсe , IProductCodeUKTZEDServiсe productCodeUKTZEDServiсe)
        {
            _productServiсe = productServiсe;
            _productUnitServiсe = productUnitServiсe;
            _productCodeUKTZEDServiсe = productCodeUKTZEDServiсe;
             
            _updateProductCommand = CreateCommandParameterAsync<ProductModel>(UpdateProduct);
            _updateProductRangeCommand = CreateCommandAsync(UpdateProductRange);
            _clearItemForListCommand = CreateCommandParameterAsync<ProductModel>(ClearItemForList);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); });


            _product = new ObservableCollection<ProductModel>();
            _productCodeUKTZED = new ObservableCollection<ProductCodeUKTZEDModel>();
            _productUnits = new ObservableCollection<ProductUnitModel>();
            _statusProducts = new ObservableCollection<string>();

            _error = string.Empty;
            _success = string.Empty;
            _isEnableSaveButton = true;
            _price = 0;
            _count = 0;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed; 
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
            _count = 0;
            _price = 0;

            ProductUnits = new ObservableCollection<ProductUnitModel>((await _productUnitServiсe.GetFromSession()).ToProductUnitModel());
            ProductCodeUKTZED = new ObservableCollection<ProductCodeUKTZEDModel>((await _productCodeUKTZEDServiсe.GetFromSession()).ToProductCodeUKTZEDModel());


            var products = _productServiсe.GetProductsOnSession().ToProductModel();
            Product = new ObservableCollection<ProductModel>(products);
             

            if (StatusProducts.Count == 0)
            {
                StatusProducts = new ObservableCollection<string>(ProductStatusModel.GetProductStatus()); 
            }

        }
        private ObservableCollection<ProductModel> _product;
        public ObservableCollection<ProductModel> Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        private ObservableCollection<ProductUnitModel> _productUnits;
        public ObservableCollection<ProductUnitModel> ProductUnits
        {
            get { return _productUnits; }
            set { _productUnits = value; OnPropertyChanged(nameof(ProductUnits)); }
        }
        private ObservableCollection<ProductCodeUKTZEDModel> _productCodeUKTZED;
        public ObservableCollection<ProductCodeUKTZEDModel> ProductCodeUKTZED
        {
            get { return _productCodeUKTZED; }
            set { _productCodeUKTZED = value; OnPropertyChanged(nameof(ProductCodeUKTZED)); }
        }
        public ObservableCollection<string> _statusProducts;
        public ObservableCollection<string> StatusProducts
        {
            get { return _statusProducts; }
            set { _statusProducts = value; OnPropertyChanged(nameof(StatusProducts)); }
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

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }            
        }
        public double _count;
        public double Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(nameof(Count));  } 
        }

        public ICommand ExitWindowCommand => _exitWindowCommand; 
        public ICommand UpdateProductCommand => _updateProductCommand;

        public Action? CloseView { get; set; }

        private async Task UpdateProduct(ProductModel item)
        {
            IsEnableSaveButton = false;

            if (await _productServiсe.Update(item.ToProduct()))
            {
                Success = "Товар : " + item.NameProduct+" редаговано ";
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                SuccessTextBlockVisibiliti = Visibility.Visible; 
            }
            else
            {
                Error = "Невдалося редагувати товар";
                ErrorTextBlockVisibiliti = Visibility.Visible;
                SuccessTextBlockVisibiliti = Visibility.Collapsed; 
            }

            Product.Remove(item);

            if(Product.Count == 0)
            {

                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Товар", "Товари успішно редаговано")));
                CloseView?.Invoke(); 
            }
            IsEnableSaveButton = true;
        }
        public ICommand ClearItem => _clearItemForListCommand;
        private async Task ClearItemForList(ProductModel item)
        {
            Product.Remove(item);

            Success = "Товар : " + item.NameProduct + " видалено зі списку";
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;

            if (Product.Count == 0)
            {
                CloseView?.Invoke();
            }
        }
        public ICommand UpdateProductRangeCommand => _updateProductRangeCommand;
        public async Task UpdateProductRange()
        {
            IsEnableSaveButton = false;

            if(Count != 0)
            {
                Product = new ObservableCollection<ProductModel>((await _productServiсe.UpdateProductParatemer(Product.ToProduct(), nameof(Count), Count)).ToProductModel());
            }
            if(Price != 0)
            {
               Product = new ObservableCollection<ProductModel>((await _productServiсe.UpdateProductParatemer(Product.ToProduct(), nameof(Price), Price)).ToProductModel());
            }

            if (await _productServiсe.UpdateRange(Product.ToProduct().ToList()))
            {
                Success = "Товари редаговано ";
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                SuccessTextBlockVisibiliti = Visibility.Visible;
            }
            else
            {
                Error = "Невдалося редагувати товар";
                ErrorTextBlockVisibiliti = Visibility.Visible;
                SuccessTextBlockVisibiliti = Visibility.Collapsed;
            } 
            IsEnableSaveButton = true; 
        }

    }
}
