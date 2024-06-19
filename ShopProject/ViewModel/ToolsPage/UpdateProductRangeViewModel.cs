using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
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
            
            _productList = new List<ProductEntiti>();
            _productUnits = new List<string>();
            _productCodeUKTZED = new List<string>();

            ProductUnits = new List<string>();
            
            new Thread(new ThreadStart(SetField)).Start();
        }

        private void SetField()
        {
            if(_model!= null)
            {
                ProductUnits = _model.GetUnitList();
                ProductCodeUKTZED = _model.GetCodeUKTZEDList();
            }

            if (Session.ProductList != null)
            {
                ProductList = _model.GetItem(Session.ProductList);
            }

        }

        private List<string> _productUnits;
        public List<string> ProductUnits
        {
            get { return _productUnits; }
            set { _productUnits = value; OnPropertyChanged("ProductUnits"); }
        }

        private List<string> _productCodeUKTZED;
        public List<string> ProductCodeUKTZED
        {
            get { return _productCodeUKTZED; }
            set { _productCodeUKTZED = value; OnPropertyChanged("ProductCodeUKTZED"); }
        }

        private List<ProductEntiti> _productList;
        public List<ProductEntiti> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged("ProductList"); }
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
                MessageBox.Show("Товари редаговано","Інформація",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

    }
}
