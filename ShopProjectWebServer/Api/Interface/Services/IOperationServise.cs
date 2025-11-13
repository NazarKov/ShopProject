using ShopProjectWebServer.Api.DtoModels.Operation;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IOperationServise
    {
        public int Add(string token, CreateOperationDto item);
        public IEnumerable<OperationDto> GetAll(string token);
        public OperationDto GetLast(string token , int shiftId);
    }
}
