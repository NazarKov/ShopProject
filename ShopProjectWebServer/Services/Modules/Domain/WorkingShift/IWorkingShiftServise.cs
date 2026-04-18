using ShopProjectWebServer.Api.DtoModels.WorkingShift;

namespace ShopProjectWebServer.Services.Modules.Domain.WorkingShift
{
    public interface IWorkingShiftServise
    {
        public int Add(string token, CreateWorkingShiftDto item);
        public void Update(string token, UpdateWorkingShiftDto item);

        public WorkingShiftDto GetById(string token, string id);
    }
}
