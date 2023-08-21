using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingDeviceSettlementOperationsViewModel : ViewModel<SettingDeviceSettlementOperationsViewModel>
    {
        private SettingDeviceSettlementOperationsModel _model;

        private ICommand _chekConnectionServerCommand;
        private ICommand _saveFiscalNumberCommand;
        private ICommand _saveTaxNumberCommand;

        public SettingDeviceSettlementOperationsViewModel()
        {
            _model = new SettingDeviceSettlementOperationsModel();
            _chekConnectionServerCommand = new DelegateCommand(() => { ChekConnectionServer(); });

            _saveFiscalNumberCommand = new DelegateCommand(() => {  SaveFiscalNumber(); });
            _saveTaxNumberCommand = new DelegateCommand(() => { SaveTaxNumber(); });

            _fiscalNumber = string.Empty;
            _taxNumber = string.Empty;

            setFieldTextBox();
        }
        private void setFieldTextBox()
        {
            FiscalNumber = _model.GetSettingSetting("FiscalNumberRRO");
            TaxNumber = _model.GetSettingSetting("TaxNumber");
        }
        private string _fiscalNumber;
        public string FiscalNumber
        {
            get { return _fiscalNumber; }
            set { _fiscalNumber = value; OnPropertyChanged("FiscalNumber"); }
        }

        private string _taxNumber;
        public string TaxNumber
        {
            get { return _taxNumber; }
            set { _taxNumber = value; OnPropertyChanged("TaxNumber"); }
        }

        public ICommand ChekConnectionServerCommand=> _chekConnectionServerCommand;
        private void ChekConnectionServer()
        {
            _model.ChekConnection();
        }

        public ICommand SaveFiscalNumberCommand => _saveFiscalNumberCommand;
        private void SaveFiscalNumber()
        {
            _model.SaveFieldSetting("FiscalNumberRRO", FiscalNumber);
            MessageBox.Show("Збережено", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public ICommand SaveTaxNumberCommand => _saveTaxNumberCommand;
        private void SaveTaxNumber()
        {
            _model.SaveFieldSetting("TaxNumber", TaxNumber);
            MessageBox.Show("Збережено","Information",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
