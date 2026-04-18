using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.WorkingShift
{
    internal class WorkingShiftService : IWorkingShiftServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;

        public WorkingShiftService(DataBaseService controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationService(controller);
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

            var result = _controller.DataBaseAccess.WorkingShiftTable.GetById(int.Parse(id));

            if (result == null) 
            {
                throw new Exception("Невдалося завантажити зміну");
            }

            return result.ToWorkingShiftDto(); 

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
