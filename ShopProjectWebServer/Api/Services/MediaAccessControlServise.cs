using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class MediaAccessControlServise : IMediaAccessContolServise
    {
        public bool Add(string token, CreateMediaAccessControlDto createMediaAccessControlDto)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }  
            DataBaseMainController.DataBaseAccess.MediaAccessControlTable.Add(createMediaAccessControlDto.ToMediaAccessEntity());
            return true;
        }

        public MediaAccessControlDto GetLastMediaAccessControl(string token , Guid id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            return DataBaseMainController.DataBaseAccess.MediaAccessControlTable.GetLastMAC(id).ToMediaAccessDto();
        }
    }
}
