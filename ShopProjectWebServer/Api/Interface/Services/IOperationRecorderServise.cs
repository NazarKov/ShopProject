using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IOperationRecorderServise 
    {
        public bool AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner);
        public bool Add(string token, CreateOperationRecorderDto operationsRecorder);
        public bool AddRange(string token, IEnumerable<CreateOperationRecorderDto> operationsRecorder);
        public bool Delete(string token, string id); 
        public IEnumerable<OperationRecorderDto> GetOperationRecordersByNumberAndUser(string token, string number, Guid userId); 
        public IEnumerable<OperationRecorderDto> GetOperationRecordersByNameAndUser(string token, string name, Guid userId);
        public PaginatorDto<OperationRecorderDto> GetOperationRecordersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status);
        public PaginatorDto<OperationRecorderDto> GetOperationRecordersPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status);
        public IEnumerable<OperationRecorderDto> GetOperationRecorders(string token);
       
    }
}
