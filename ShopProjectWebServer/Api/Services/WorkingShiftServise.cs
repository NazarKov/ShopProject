using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class WorkingShiftServise : IWorkingShiftServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public WorkingShiftServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public int Add(string token, CreateWorkingShiftDto item)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            return _controller.DataBaseAccess.WorkingShiftTable.Add(item.ToWorkingShiftEntity());
        }

        public WorkingShiftDto GetById(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            return _controller.DataBaseAccess.WorkingShiftTable.GetById(int.Parse(id)).ToWorkingShiftDto();
        }

        public void Update(string token, UpdateWorkingShiftDto item)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.WorkingShiftTable.Update(item.ToWorkingShiftEntity());
        }
    }
}
