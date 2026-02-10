using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.SignatureKey;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class ElectronicSignatureKeyServise : IElectronicSignatureKeyServise
    {
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;
        public ElectronicSignatureKeyServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
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
