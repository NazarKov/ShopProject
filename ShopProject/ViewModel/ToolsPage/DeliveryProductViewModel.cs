using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class DeliveryProductViewModel : ViewModel<DeliveryProductViewModel>
    { 
        private ICommand _saveParametersCommand;

        public DeliveryProductViewModel() 
        { 

            _saveParametersCommand = new DelegateCommand(Save);
            _barCode = string.Empty;
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
            //if (_model != null)
            //{
            //    if (_model.SetCount(BarCode, count))
            //    {
            //        BarCode = string.Empty;
            //    }
            //}
        }
        public ICommand ExitWindowCommand { get => CreateCommandParameter<object>(WindowClose); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

    }
}
