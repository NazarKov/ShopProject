using ShopProjectWebServer.Api.DtoModels.Operation;
using ShopProjectWebServer.Services.Common;
using OperationModel = ShopProjectWebServer.Models.Domain.Operation.Operation;

namespace ShopProjectWebServer.Services.Modules.Domain.Operation.Interface
{
    public interface IOperationService
    {
        public Task<OperationResult<OperationModel>> Add(OperationModel item);
        public Task<OperationResult<OperaiontStatisticsDto>> GetInfo(int shiftId);
        public Task<OperationResult<OperationІnformationDto>> GetInformation(int shiftId);
    }
}
