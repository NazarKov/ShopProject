using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Model.UI.PointOfSale;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using ShopProject.Services.Modules.Mapping.TaxObjectUser; 
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage.PointOfSale
{
    internal class AssignedPointsOfSaleViewModel : ViewModel<AssignedPointsOfSaleViewModel>, IViewModelLoadResourse
    {
        private ITaxObjectService _taxObjectService;
        private readonly IMessageBoxControlService _messageBoxControlService;
        public AssignedPointsOfSaleViewModel(ITaxObjectService taxObjectService,IMessageBoxControlService messageBoxControlService)
        {
            _taxObjectService = taxObjectService;
            _messageBoxControlService = messageBoxControlService;
            _pointsOfSale = new List<TaxObjectAndOperationRecorderModel>();
            _generalShadowVisibility = Visibility.Collapsed;
            MediatorService.AddEventAsync("PointOfSaleSnadowUserSetVissible", async () => { GeneralShadowVisibility = Visibility.Visible; });
            MediatorService.AddEventAsync("PointOfSaleSnadowUserSetCollapsed", async () => { GeneralShadowVisibility = Visibility.Collapsed; });
        }
        private Visibility _generalShadowVisibility;
        public Visibility GeneralShadowVisibility
        {
            get { return _generalShadowVisibility; }
            set { _generalShadowVisibility = value; OnPropertyChanged(nameof(GeneralShadowVisibility)); }
        }

        private List<TaxObjectAndOperationRecorderModel> _pointsOfSale;
        public List<TaxObjectAndOperationRecorderModel> PointsOfSale
        {
            get { return _pointsOfSale; }
            set { _pointsOfSale = value; OnPropertyChanged(nameof(PointsOfSale)); }
        }


        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage); 
        }
        public async Task SetFieldPage()
        {
            var result = await _taxObjectService.GetTaxObjectsAssignedUser();
            if (result.IsSuccess)
            { 
                PointsOfSale = new List<TaxObjectAndOperationRecorderModel>(result.Data.ToTaxObjectAndOperationRecorderModel()); 
            }
        }
        public ICommand OpenWorkShifMenuCommand { get => CreateCommandParameter<object>(OpenWorkShiftMenu); }
        private void OpenWorkShiftMenu(object parameter)
        {
            if (parameter != null)
            {
                var operationRecorder = parameter as OperationRecorderModel;
                if (operationRecorder != null) 
                { 
                    if (operationRecorder.TypeStatus != TypeStatusOperationRecorder.Open)
                    {
                        _messageBoxControlService.Show("Касовий апарат недоступний для використання", "Warning", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Warning, "PointOfSaleSnadowUser");
                        return;
                    }
                    else
                    {
                        var taxObject = PointsOfSale.Where(p => p.OperationRecorders.Where(o => o.FiscalNumber.Equals(operationRecorder.FiscalNumber)).Any()).First().TaxObject;
                        if(taxObject.TypeStatus != TypeStatusTaxObject.Open)
                        {
                            _messageBoxControlService.Show("Обєкт з касами недоступний для використання", "Warning", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Warning, "PointOfSaleSnadowUser");
                            return;
                        }

                        _taxObjectService.SetPoinOfSaleOnSession(taxObject.ToTaxObject(), operationRecorder.ToOperationRecorder());
                        MediatorService.ExecuteNavigation(NavigationButton.RedirectToWorkShiftMenuPage);
                    } 
                }
                else
                {
                    _messageBoxControlService.Show("Невдалося відкрити касовий апарат", "Warning", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Warning, "PointOfSaleSnadowUser");
                }
            }
        }
    }
}
