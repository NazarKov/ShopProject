using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.GiftCertificatesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.GiftCertificatesPage
{
    internal class UpdateGiftCertificateViewModel :ViewModel<UpdateGiftCertificateViewModel>
    {
        private UpdateGiftCertificateModel _model;
        
        private ICommand _saveGiftCertificateCommand;
        private ICommand _clearFieldCommand;

        private int _id;
        public UpdateGiftCertificateViewModel()
        {
            _model = new UpdateGiftCertificateModel();

            _clearFieldCommand = new DelegateCommand(ClearFiled);
            _saveGiftCertificateCommand = new DelegateCommand(SaveGiftCertificate);
            _code = string.Empty;
            _name = string.Empty;
            _description = string.Empty;
            _price = decimal.Zero;

            SetFieldPage();
        }

        private void SetFieldPage()
        {
            var giftCertificate = Session.GiftCertificate;
            if (giftCertificate != null) 
            {
                _name = giftCertificate.Name;
                _price = giftCertificate.Price.Value;
                _description = giftCertificate.Description;
                _code = giftCertificate.Code;
                _id = giftCertificate.ID;
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        public ICommand SaveGiftCertificateCommand => _saveGiftCertificateCommand;
        private void SaveGiftCertificate()
        {
            Task t = Task.Run(async () =>
            {
                if (await _model.SaveItemDataBase(new UIModel.GiftCertificatesPage.GiftCertificate()
                {
                    ID = _id,
                    Name = _name,
                    Code = _code,
                    Description = _description,
                    Price = _price, 
                }))
                {
                    MessageBox.Show("Сертифікат оновлено", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                    MediatorService.ExecuteEvent(NavigationButton.ReloadGiftCertificates.ToString());
                }
            });

        }
        public ICommand ClearFieldCommand => _clearFieldCommand;
        private void ClearFiled()
        {
            _name = string.Empty;
            _description = string.Empty;
            _price = decimal.Zero;
            _code = string.Empty;
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
