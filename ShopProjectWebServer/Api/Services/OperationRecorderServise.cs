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
        public bool Add(string token, CreateOperationRecorderDto operationsRecorder)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.OperationRecorderTable.Add(operationsRecorder.ToOperationRecorderEntity());

            return true;
        }

        public bool AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.OperationRecorderTable.AddBinding(Guid.Parse(idoperationrecoreder),Guid.Parse(idobjectowner));
            return true;
        }

        public bool AddRange(string token, IEnumerable<CreateOperationRecorderDto> operationsRecorder)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.OperationRecorderTable.AddRange(operationsRecorder.ToOperationRecordersEntity());
            return true;
        }

        public bool Delete(string token, string id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.OperationRecorderTable.Delete(new ShopProjectDataBase.Entities.OperationsRecorderEntity() { ID = Guid.Parse(id)});
            return true;
        }

        public IEnumerable<OperationRecorderDto> GetOperationRecorders(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.OperationRecorderTable.GetAll().ToOperationRecorderDto();
        }

        public IEnumerable<OperationRecorderDto> GetOperationRecordersByNameAndUser(string token, string name, Guid userId)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.OperationRecorderTable.SearchByNameAndUser(name, userId).ToOperationRecorderDto();
        }

        public PaginatorDto<OperationRecorderDto> GetOperationRecordersByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusOperationRecorder status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var operationRecorders = DataBaseMainController.DataBaseAccess.OperationRecorderTable.GetByNameAndStatus(name, status);

            var paginator = PaginatorDto<OperationsRecorderEntity>.CreationPaginator(operationRecorders, page, countColumn);
            return new PaginatorDto<OperationRecorderDto>(paginator.Page, paginator.Pages, operationRecorders.ToOperationRecorderDto());
        }

        public IEnumerable<OperationRecorderDto> GetOperationRecordersByNumberAndUser(string token, string number, Guid userId)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.OperationRecorderTable.SearchByNumberAndUser(number, userId).ToOperationRecorderDto();
        }

        public PaginatorDto<OperationRecorderDto> GetOperationRecordersPageColumn(string token, int page, int countColumn, TypeStatusOperationRecorder status)
            => GetOperationRecordersByNamePageColumn(token, string.Empty, page, countColumn, status);
    }
}
