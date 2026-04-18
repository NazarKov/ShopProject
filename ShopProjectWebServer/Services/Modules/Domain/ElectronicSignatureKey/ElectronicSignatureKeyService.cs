using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.SignatureKey;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.ElectronicSignatureKey
{
    internal class ElectronicSignatureKeyService : IElectronicSignatureKeyServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;
        public ElectronicSignatureKeyService(DataBaseService controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationService(controller);
        }
        public SignatureKeyDto GetSignatureKeyByUser(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var user = _controller.DataBaseAccess.UserTable.GetUser(token);
            var result = _controller.DataBaseAccess.SignatureKeyTable.GetKeyByUser(user.ID.ToString());
            return result.ToSignatureKeyDto();
        }
    }
}
