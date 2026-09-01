using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.TaxObject;
using ShopProject.Model.Domain.TaxObjectUser;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.TaxObject;
using ShopProject.Services.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxObjectModel = ShopProject.Model.Domain.TaxObject.TaxObject;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface
{
    internal interface ITaxObjectService
    {
        public Task<OperationResult<Paginator<TaxObjectModel, TypeStatusTaxObject>>> GetPageColumn(int page, int countColumn, TypeStatusTaxObject status);
        public Task<OperationResult<Paginator<TaxObjectModel, TypeStatusTaxObject>>> SearchByName(string item, int page, int countColumn, TypeStatusTaxObject status);
        public Task<OperationResult<TaxObjectModel>> Add(TaxObjectModel taxObject);
        public Task<OperationResult<TaxObjectModel>> Update(TaxObjectModel taxObject);
        public Task<OperationResult<IEnumerable<TaxObjectModel>>> AddRange(IEnumerable<TaxObjectModel> taxObjects);
        public Task<OperationResult<IEnumerable<TaxObjectModel>>> GetTaxServer(string pathFile, string passwordKey);

        public Task<OperationResult<bool>> AddBindingOperationRecorderToTaxObject(Guid idTaxObject, IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> operationRecorders);
        public Task<OperationResult<bool>> AddBindingUserToTaxObject(Guid idTaxObject, IEnumerable<ShopProject.Model.Domain.User.User> users); 
        public Task<OperationResult<bool>> UpdateParameter(string parameter, object value, TaxObjectModel item);


        public void SetBindingTaxObjectTOSession(TaxObjectModel taxObject);
        public TaxObjectModel GetBindingTaxObjectOnSession();

        public Task<OperationResult<IEnumerable<TaxObjectUser>>> GetTaxObjectsAssignedUser();
        public void SetPoinOfSaleOnSession(TaxObjectModel taxObject, ShopProject.Model.Domain.OperationRecorder.OperationRecorder operationRecorder);
         
    }
}
