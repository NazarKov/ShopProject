using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class OperationRecorderServise : IOperationRecorderServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public OperationRecorderServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public bool Add(string token, CreateOperationRecorderDto operationsRecorder)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.OperationRecorderTable.Add(operationsRecorder.ToOperationRecorderEntity());

            return true;
        }

        public bool AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.OperationRecorderTable.AddBinding(Guid.Parse(idoperationrecoreder),Guid.Parse(idobjectowner));
            return true;
        }

        public bool AddRange(string token, IEnumerable<CreateOperationRecorderDto> operationsRecorder)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.OperationRecorderTable.AddRange(operationsRecorder.ToOperationRecordersEntity());
            return true;
        }

        public bool Delete(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.OperationRecorderTable.Delete(new ShopProjectDataBase.Entities.OperationsRecorderEntity() { ID = Guid.Parse(id)});
            return true;
        }

        public IEnumerable<OperationRecorderDto> GetOperationRecorders(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.OperationRecorderTable.GetAll().ToOperationRecorderDto();
        }

        public IEnumerable<OperationRecorderDto> GetOperationRecordersByNameAndUser(string token, string name, Guid userId)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.OperationRecorderTable.SearchByNameAndUser(name, userId).ToOperationRecorderDto();
        }

        public PaginatorDto<OperationRecorderDto> GetOperationRecordersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var operationRecorders = _controller.DataBaseAccess.OperationRecorderTable.GetByNameAndStatus(name, status);

            var paginator = PaginatorDto<OperationsRecorderEntity>.CreationPaginator(operationRecorders, page, countColumn);
            return new PaginatorDto<OperationRecorderDto>(paginator.Page, paginator.Pages, operationRecorders.ToOperationRecorderDto());
        }

        public IEnumerable<OperationRecorderDto> GetOperationRecordersByNumberAndUser(string token, string number, Guid userId)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.OperationRecorderTable.SearchByNumberAndUser(number, userId).ToOperationRecorderDto();
        }

        public PaginatorDto<OperationRecorderDto> GetOperationRecordersPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status)
            => GetOperationRecordersByNamePageColumn(token, string.Empty, page, countColumn, status);
    }
}
