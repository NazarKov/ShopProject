using ShopProject.Helpers;
using ShopProject.Helpers.Command;
using ShopProject.Model.StoragePage.ProductsPage;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ProductPage
{
    internal class UpdateProductRangeViewModel : ViewModel<UpdateProductRangeViewModel>
    {
        private UpdateProductRangeModel _model;
        private ICommand _updateProductCommand;

        public UpdateProductRangeViewModel()
        {
            _model = new UpdateProductRangeModel();
            _updateProductCommand = new DelegateCommand(UpdateProduct);

            _product = new List<Product>();
            _productCodeUKTZED = new List<ProductCodeUKTZED>();
            _productUnits = new List<ProductUnit>();
            _statusProducts = new List<TypeStatusProduct>();
            SetField();
        }

        private void SetField()
        {
            var units = Session.ProductUnits;
            if (units != null)
            {
                ProductUnits.AddRange(units);
            }
            var codes = Session.ProductCodesUKTZED;
            if (codes != null)
            {
                ProductCodeUKTZED.AddRange(codes);
            }

             
            var products = Session.UpdateProductRange;
            if (products != null)
            {
                foreach(Product product in products)
                {
                    Product.Add(product);
                } 
            } 

            StatusProducts = Enum.GetValues<TypeStatusProduct>().ToList();

        } 
        private List<Product> _product;
        public List<Product> Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        private List<ProductUnit> _productUnits;
        public List<ProductUnit> ProductUnits
        {
            get { return _productUnits; }
            set { _productUnits = value; OnPropertyChanged(nameof(ProductUnits)); }
        }
        private List<ProductCodeUKTZED> _productCodeUKTZED;
        public List<ProductCodeUKTZED> ProductCodeUKTZED
        {
            get { return _productCodeUKTZED; }
            set { _productCodeUKTZED = value; OnPropertyChanged(nameof(ProductCodeUKTZED)); }
        }
        public List<TypeStatusProduct> _statusProducts;
        public List<TypeStatusProduct> StatusProducts
        {
            get { return _statusProducts; }
            set { _statusProducts = value; OnPropertyChanged(nameof(StatusProducts)); }
        }

        public ICommand ExitWindowCommand { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

        public ICommand UpdateProductCommand => _updateProductCommand;
        private void UpdateProduct()
        {
            
            Task.Run(async () => {
                if (await _model.UpdateProduct(Product))
                {
                    MessageBox.Show("Товари редаговано", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Невдалося редаговати товар", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }

    }
}
