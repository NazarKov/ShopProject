using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using ShopProject.Views.ToolsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class DeliveryOfGoodsViewModel : ViewModel<DeliveryOfGoodsViewModel>
    {
        private DeliveryOfGoodsModel _model;
        private ICommand _saveParametersCommand;

        public DeliveryOfGoodsViewModel() 
        {
            _model = new DeliveryOfGoodsModel();

            _saveParametersCommand = new DelegateCommand(Save);

            Count = 0;
        }
        private string _barCode;
        public string BarCode
        {
            get { return _barCode; }
            set { _barCode = value; OnPropertyChanged("BarCode"); }
        }

        private decimal count;
        public decimal Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged("Count"); }
        }
        public ICommand SaveParametersCommand => _saveParametersCommand;
        private void Save()
        {
            if (_model != null)
            {
                if(_model.SetCount(BarCode, count))
                {
                    Count = 0;
                    BarCode = string.Empty;
                }
            }
        }
        public ICommand ExitWindowCommand { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

    }
}
