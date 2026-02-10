using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class MediaAccessControlServise : IMediaAccessContolServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public MediaAccessControlServise(DataBaseMainController controller)
        {
            this._controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }

        public bool Add(string token, CreateMediaAccessControlDto createMediaAccessControlDto)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.MediaAccessControlTable.Add(createMediaAccessControlDto.ToMediaAccessEntity());
            return true;
        }

        public MediaAccessControlDto GetLastMediaAccessControl(string token , Guid id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            return _controller.DataBaseAccess.MediaAccessControlTable.GetLastMAC(id).ToMediaAccessDto();
        }
    }
}
