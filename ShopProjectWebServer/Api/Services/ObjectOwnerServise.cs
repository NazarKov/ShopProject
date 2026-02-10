using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ObjectOwner;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class ObjectOwnerServise : IObjectOwnerServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public ObjectOwnerServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public bool Delete(string token, string id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.ObjectOwnerTable.Delete(new ObjectOwnerEntity() { ID = Guid.Parse(id) });

            return true;
        }

        public PaginatorDto<ObjectOwnerListDto> GetPageColumnByName(string token, string name, int page, int column, TypeStatusObjectOwner typeStatus)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var ObjectOwners = _controller.DataBaseAccess.ObjectOwnerTable.GetByNameAndStatus(name, typeStatus);
 
            var paginator = PaginatorDto<ObjectOwnerEntity>.CreationPaginator(ObjectOwners, page, column);
            var result = new PaginatorDto<ObjectOwnerListDto>(paginator.Page, paginator.Pages, paginator.Data.ToObjectOwnerListDto().ToList());

            return result;
        }

        public PaginatorDto<ObjectOwnerListDto> GetPageColumn(string token, int page, int column, TypeStatusObjectOwner typeStatus) => GetPageColumnByName(token, string.Empty, page, column, typeStatus);

        public IEnumerable<ObjectOwnerListDto> GetAll(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            return _controller.DataBaseAccess.ObjectOwnerTable.GetAll().ToObjectOwnerListDto();
        }

        public bool Add(string token, CreateObjectOwnerDto ObjectOwner)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            _controller.DataBaseAccess.ObjectOwnerTable.Add(ObjectOwner.ToObjectOwnerEntity());

            return true;
        }

        public bool AddRange(string token, IEnumerable<CreateObjectOwnerDto> ObjectOwners)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.ObjectOwnerTable.AddRange(ObjectOwners.ToObjectOwnerEntity());

            return true;
        }
    }
}
