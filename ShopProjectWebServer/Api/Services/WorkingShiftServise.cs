using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class WorkingShiftServise : IWorkingShiftServise
    {
        public void Add(string token, CreateWorkingShiftDto item)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            DataBaseMainController.DataBaseAccess.WorkingShiftTable.Add(item.ToWorkingShiftEntity());
        }

        public WorkingShiftDto GetById(string token, string id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            return DataBaseMainController.DataBaseAccess.WorkingShiftTable.GetById(int.Parse(id)).ToWorkingShiftDto();
        }

        public void Update(string token, UpdateWorkingShiftDto item)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.WorkingShiftTable.Update(item.ToWorkingShiftEntity());
        }
    }
}
