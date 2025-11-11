using ShopProjectWebServer.Api.DtoModels.WorkingShift;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IWorkingShiftServise
    {
        public void Add(string token, CreateWorkingShiftDto item);
        public void Update(string token, UpdateWorkingShiftDto item);

        public WorkingShiftDto GetById(string token, string id);
    }
}
