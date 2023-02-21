using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class UpdateProductAllViewModel : ViewModel<UpdateProductAllViewModel>
    {
        private UpdateProductAllModel model;
        private ICommand upateProductDataBaseCommand;

        public UpdateProductAllViewModel()
        {
            model = new UpdateProductAllModel();
            upateProductDataBaseCommand = new DelegateCommand(UpdateProduct);
            
            _products = new List<Product>();
            _textComboBox = new List<string>();

            TextComboBox = new List<string>();
            TextComboBox.Add("Шт");
            TextComboBox.Add("кг");
            TextComboBox.Add("пачка");
            TextComboBox.Add("ящик");

            SetFieldDataGrid();
        }

        private void SetFieldDataGrid()
        {
            if(StaticResourse.products!=null)
            Products = StaticResourse.products;
        }

        private List<string> _textComboBox;
        public List<string> TextComboBox
        {
            get { return _textComboBox; }
            set { _textComboBox = value; OnPropertyChanged("TextComboBox"); }
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

        public ICommand UpateProductDataBaseCommand => upateProductDataBaseCommand;
        private void UpdateProduct()
        {
            if (model.UpdateProduct(Products))
            {
                MessageBox.Show("Товари редаговано");
            }
        }

    }
}
