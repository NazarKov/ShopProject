using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Interface;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.TaxObject;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Infrastructure.Mediator.Notifications;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Mapping.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject
{
    internal class CreateTaxObjectFromKeyViewModel : ViewModel<CreateTaxObjectFromKeyViewModel>, IСontrolView
    {
        private ICommand _createTaxObjectCommand;
        private ICommand _exitWindowCommand;
        private ICommand _openFileKeyCommand;

        private ITaxObjectService _taxObjectService; 

        public CreateTaxObjectFromKeyViewModel(ITaxObjectService taxObjectService)
        {
            _taxObjectService = taxObjectService;
            _error = string.Empty;
            _success = string.Empty;
            _taxObjects = new List<TaxObjectSelectItemModel>();
            _passwordKey = string.Empty;
            _pathKey = string.Empty;

            _createTaxObjectCommand = CreateCommandAsync(CreateTaxObject);
            _exitWindowCommand = CreateCommand(() => { CloseView?.Invoke(); }); 
            _openFileKeyCommand = CreateCommandAsync(OpenFileKey);

            _successTextBlockVisibiliti = Visibility.Collapsed;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _taxObjectVisibiliti = Visibility.Collapsed;
        }

        public Action? CloseView { get; set; }

        private List<TaxObjectSelectItemModel> _taxObjects;
        public List<TaxObjectSelectItemModel> TaxObjects
        {
            get { return _taxObjects; }
            set { _taxObjects = value; OnPropertyChanged(nameof(TaxObjects)); }
        }

        private string _pathKey;
        public string PathKey
        {
            get { return _pathKey; }
            set { _pathKey = value; OnPropertyChanged(nameof(PathKey)); }
        }
        private string _passwordKey;
        public string PasswordKey
        {
            get { return _passwordKey; }
            set { _passwordKey = value; OnPropertyChanged(nameof(PasswordKey)); }
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
        private Visibility _taxObjectVisibiliti;
        public Visibility TaxObjectVisibiliti
        {
            get { return _taxObjectVisibiliti; }
            set { _taxObjectVisibiliti = value; OnPropertyChanged(nameof(TaxObjectVisibiliti)); }
        }
        public ICommand CreateTaxObjectCommand => _createTaxObjectCommand;

        public async Task CreateTaxObject()
        {
            var result = await _taxObjectService.AddRange(TaxObjects.Where(i=>i.IsActive == true).ToTaxObjectModel());
            if (result.IsSuccess)
            {
                SetSuccess("Обєкти збережено");
                await MediatorService.PublishNotificationsAsync<ShowNotificationEvent>(new ShowNotificationEvent(Notification.Succes("Обєкт власноті", "Обєкт власноті збережено в базі даних")));
                await MediatorService.ExecuteEventAsync(NavigationButton.ReloadTaxObject.ToString());

            }
            else if (result.IsError)
            {
                SetError(result.ErrorMessage);
            }
            else
            {
                SetError("Невдалося виконати операцію");
            } 
        }

        public ICommand OpenFiLeKeyCommand => _openFileKeyCommand;
        private async Task OpenFileKey()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathKey = openFileDialog.FileName;



                var result = await _taxObjectService.GetTaxServer(PathKey, PasswordKey);
                if (result.IsSuccess)
                {
                    TaxObjects = new List<TaxObjectSelectItemModel>(result.Data.ToTaxObjectSelctedItemModel());
                    TaxObjectVisibiliti = Visibility.Visible;
                    SetSuccess("Обєкти завантажено з сервера"); 
                }
                else if (result.IsError) 
                {
                    SetError(result.ErrorMessage); 
                }
                else
                {
                    SetError("Невдалося виконати операцію"); 
                }

            }
        } 
        public ICommand ExitWindowCommand => _exitWindowCommand;
        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
        }
        private void SetSuccess(string messege)
        {
            Success = messege;
            ErrorTextBlockVisibiliti = Visibility.Collapsed;
            SuccessTextBlockVisibiliti = Visibility.Visible;
        }
    }
}
