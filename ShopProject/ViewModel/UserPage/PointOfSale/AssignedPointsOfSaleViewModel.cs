using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Model.UI.PointOfSale;
using ShopProject.Services.Infrastructure.Mediator;
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

        public AssignedPointsOfSaleViewModel(ITaxObjectService taxObjectService)
        {
            _taxObjectService = taxObjectService;
            _pointsOfSale = new List<TaxObjectAndOperationRecorderModel>();
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
                    var taxObject = PointsOfSale.Where(p=>p.OperationRecorders.Where(o => o.FiscalNumber.Equals(operationRecorder.FiscalNumber)).Any()).First().TaxObject;


                    _taxObjectService.SetPoinOfSaleOnSession(taxObject.ToTaxObject(), operationRecorder.ToOperationRecorder());
                    MediatorService.ExecuteNavigation(NavigationButton.RedirectToWorkShiftMenuPage); 
                }
                else
                {
                    MessageBox.Show("Невдалося відкрити зміну");
                }
            }
        }
    }
}
