using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Services.Common; 
using OperationRecorderModel = ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder;

namespace ShopProjectWebServer.Services.Modules.Domain.OperationRecorder.Interface
{
    public interface IOperationRecorderService 
    {
        public Task<OperationResult<OperationRecorderModel>> Add(OperationRecorderModel operationsRecorder);
        public Task<OperationResult<IEnumerable<OperationRecorderModel>>> AddRange(IEnumerable<OperationRecorderModel> operationsRecorder);

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>> GetByNamePageColumn(string name,
                  ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int> paginator);
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>>
            GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int> paginator);

        public bool AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner);  
        public bool Delete(string token, string id); 
        //public IEnumerable<OperationRecorderDto> GetOperationRecordersByNumberAndUser(string token, string number, Guid userId); 
        //public IEnumerable<OperationRecorderDto> GetOperationRecordersByNameAndUser(string token, string name, Guid userId);
        ////public PaginatorDto<OperationRecorderDto> GetOperationRecordersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status);
        //public PaginatorDto<OperationRecorderDto> GetOperationRecordersPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status);
        //public IEnumerable<OperationRecorderDto> GetOperationRecorders(string token);
       
    }
}
