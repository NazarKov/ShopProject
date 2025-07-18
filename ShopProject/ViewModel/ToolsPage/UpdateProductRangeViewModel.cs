using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using ShopProjectDataBase.DataBase.Model;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class UpdateProductRangeViewModel : ViewModel<UpdateProductRangeViewModel>
    {
        private UpdateProductRangeModel _model;
        private ICommand _updateProductCommand;

        public UpdateProductRangeViewModel()
        {
            _model = new UpdateProductRangeModel();
            _updateProductCommand = new DelegateCommand(UpdateProduct);
            
            _productList = new List<ProductEntity>();
            _productUnits = new List<ProductUnitEntity>();
            _productCodeUKTZED = new List<ProductCodeUKTZEDEntity>();


            SetField();
        }

        private void SetField()
        {
            if (_model != null)
            {
                ProductUnits = _model.GetUnits();
                ProductCodeUKTZED = _model.GetCodeUKTZED();
            }

            if (Session.ProductList != null)
            {
                ProductList = Session.ProductList;
            }

        }

        private List<ProductUnitEntity> _productUnits;
        public List<ProductUnitEntity> ProductUnits
        {
            get { return _productUnits; }
            set { _productUnits = value; OnPropertyChanged(nameof(ProductUnits)); }
        }

        private List<ProductCodeUKTZEDEntity> _productCodeUKTZED;
        public List<ProductCodeUKTZEDEntity> ProductCodeUKTZED
        {
            get { return _productCodeUKTZED; }
            set { _productCodeUKTZED = value; OnPropertyChanged(nameof(ProductCodeUKTZED)); }
        }

        private List<ProductEntity> _productList;
        public List<ProductEntity> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged(nameof(ProductList)); }
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
            if (_model.UpdateProduct(ProductList))
            {
                MessageBox.Show("Товари редаговано", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
