using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Services.Common;
using OperationModel = ShopProjectWebServer.Models.Domain.Operation.Operation;

namespace ShopProjectWebServer.Services.Modules.Domain.Operation.Interface
{
    public interface IOperationService
    {
        public Task<OperationResult<OperationModel>> Add(OperationModel item);
        //public IEnumerable<OperationDto> GetAll(string token);
        //public OperationDto GetLast(string token , int shiftId); 
        public OperaiontStatisticsDto GetInfo(int shiftId);
        //public OperationІnformationDto GetInformation(string token , int shiftId);
    }
}
