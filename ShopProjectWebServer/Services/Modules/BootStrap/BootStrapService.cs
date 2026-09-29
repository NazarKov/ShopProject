using ShopProjectWebServer.Models.Domain.BootStrap;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Modules.BootStrap.Interface;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit.Interface;
using ShopProjectWebServer.Services.Modules.Domain.TaxObject.Interface;
using ShopProjectWebServer.Services.Modules.Domain.UserRole.Interface;
using ShopProjectWebServer.Services.Modules.Domain.WorkingShift.Interface;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Services.Modules.BootStrap
{
    public class BootStrapService : IBootStrapService
    {
        private readonly IUserRoleServiсe _userRoleServiсe;
        private readonly IProductUnitService _productUnitService;
        private readonly IProductCodeUKTZEDService _productCodeUKTZEDService;
        private readonly IWorkingShiftService _workingShiftService;
        private readonly IOperationRecorderService _operationRecorderService; 

        public BootStrapService(IUserRoleServiсe userRoleServiсe, IProductUnitService productUnitService,
            IProductCodeUKTZEDService productCodeUKTZEDService, IWorkingShiftService workingShiftService,
            IOperationRecorderService operationRecorderService)
        {
            _userRoleServiсe = userRoleServiсe;
            _productUnitService = productUnitService;
            _productCodeUKTZEDService = productCodeUKTZEDService;
            _workingShiftService = workingShiftService;
            _operationRecorderService = operationRecorderService; 
        }

        public OperationResult<StartData> GetStartData()
        {
            var roles = _userRoleServiсe.GetAll();
            var productunit = _productUnitService.GetAll();
            var productcodeUktzed = _productCodeUKTZEDService.GetAll();

            var result = new OperationResult<StartData>();
            result.Data = new StartData();

            if (roles.IsSuccess)
            {
                result.Data.Roles = roles.Data;
            }

            if (productunit.IsSuccess) 
            {
                result.Data.ProductUnit = productunit.Data;
            }

            if (productcodeUktzed.IsSuccess) 
            {
                result.Data.ProductCodeUKTZED = productcodeUktzed.Data;
            }

            result.Status = Common.Enum.ResultStatus.Success; 
            return result;  
        }

        public async Task<OperationResult<WorkingShiftStatus>> GetWrokingShiftData(Guid userID)
        {
            var workingShift = await _workingShiftService.GetByUserId(userID);
            if (workingShift.IsSuccess)
            {
                var operationRecoreder = _operationRecorderService.GetByFiscalNumber(workingShift.Data.FiscalNumberRRO);
                return OperationResult<WorkingShiftStatus>.Success(new WorkingShiftStatus()
                {
                    WorkingShift = workingShift.Data,
                    OperationRecorder = operationRecoreder.Data,
                    TaxObject = operationRecoreder.Data.TaxObject,
                    OpenShiftTime = DateTime.Parse(workingShift.Data.CreateAt.ToString()),
                    Status = Models.Domain.Enum.TypeStatusShift.Open,
                    UserID = userID,
                    
                });
            }
            else
            {
                return OperationResult<WorkingShiftStatus>.Success(new WorkingShiftStatus());
            }
        }
    }
}
