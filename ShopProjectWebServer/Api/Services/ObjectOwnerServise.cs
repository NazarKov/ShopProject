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
        public bool Delete(string token, string id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ObjectOwnerTable.Delete(new ObjectOwnerEntity() { ID = Guid.Parse(id) });

            return true;
        }

        public PaginatorDto<ObjectOwnerListDto> GetPageColumnByName(string token, string name, int page, int column, TypeStatusObjectOwner typeStatus)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var ObjectOwners = DataBaseMainController.DataBaseAccess.ObjectOwnerTable.GetByNameAndStatus(name, typeStatus);
 
            var paginator = PaginatorDto<ObjectOwnerEntity>.CreationPaginator(ObjectOwners, page, column);
            var result = new PaginatorDto<ObjectOwnerListDto>(paginator.Page, paginator.Pages, paginator.Data.ToObjectOwnerListDto().ToList());

            return result;
        }

        public PaginatorDto<ObjectOwnerListDto> GetPageColumn(string token, int page, int column, TypeStatusObjectOwner typeStatus) => GetPageColumnByName(token, string.Empty, page, column, typeStatus);

        public IEnumerable<ObjectOwnerListDto> GetAll(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            return DataBaseMainController.DataBaseAccess.ObjectOwnerTable.GetAll().ToObjectOwnerListDto();
        }

        public bool Add(string token, CreateObjectOwnerDto ObjectOwner)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            DataBaseMainController.DataBaseAccess.ObjectOwnerTable.Add(ObjectOwner.ToObjectOwnerEntity());

            return true;
        }

        public bool AddRange(string token, IEnumerable<CreateObjectOwnerDto> ObjectOwners)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            DataBaseMainController.DataBaseAccess.ObjectOwnerTable.AddRange(ObjectOwners.ToObjectOwnerEntity());

            return true;
        }
    }
}
