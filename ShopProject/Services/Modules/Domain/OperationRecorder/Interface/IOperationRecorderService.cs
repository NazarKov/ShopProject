using ShopProject.Model.Domain.Paginator; 
using ShopProject.Model.Enum; 
using ShopProject.Services.Modules.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using OperationRecorderModel = ShopProject.Model.Domain.OperationRecorder.OperationRecorder;

namespace ShopProject.Services.Modules.Domain.OperationRecorder.Interface
{
    internal interface IOperationRecorderService
    {
        public Task<OperationResult<OperationRecorderModel>> Add(OperationRecorderModel item);
        public Task<OperationResult<IEnumerable<OperationRecorderModel>>> AddRange(IEnumerable<OperationRecorderModel> items);
        public Task<OperationResult<OperationRecorderModel>> Update(OperationRecorderModel item); 
        public Task<OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>> GetPageColumn(int page, int countColumn, TypeStatusOperationRecorder status);
        public Task<OperationResult<Paginator<OperationRecorderModel, TypeStatusOperationRecorder>>> SearchByName(string item, int page, int countColumn, TypeStatusOperationRecorder status); 
        public Task<OperationResult<IEnumerable<OperationRecorderModel>>> GetTaxServer(string pathFile, string passwordKey); 
        public Task<OperationResult<bool>> UpdateParameter(string parameter, object value, OperationRecorderModel item);

        public void SetOperationRecordeOnSession(OperationRecorderModel item);
        public OperationRecorderModel GetOperationrecorderInSession();
        
    }
}
