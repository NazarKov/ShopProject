using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.MediaAccessControl
{
    internal class MediaAccessControlService : IMediaAccessContolServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;

        public MediaAccessControlService(DataBaseService controller)
        {
            this._controller = controller;
            _authorizationServise = new AuthorizationService(controller);
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
            var result = _controller.DataBaseAccess.MediaAccessControlTable.GetLastMAC(id);
            if (result == null)
            {
                throw new Exception("Невдалося отримати MAC");
            }
            return result.ToMediaAccessDto();
        }
    }
}
