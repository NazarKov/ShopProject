
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ObjectOwner;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IObjectOwnerServise
    {
        public bool Add(string token, CreateObjectOwnerDto ObjectOwner);
        public bool AddRange(string token, IEnumerable<CreateObjectOwnerDto> ObjectOwners);
        public bool Delete(string token, string id);

        public IEnumerable<ObjectOwnerListDto> GetAll(string token);
        public PaginatorDto<ObjectOwnerListDto> GetPageColumnByName(string token, string name, int page, int column, TypeStatusObjectOwner typeStatus);
        public PaginatorDto<ObjectOwnerListDto> GetPageColumn(string token, int page, int column, TypeStatusObjectOwner typeStatus);
    }
}
