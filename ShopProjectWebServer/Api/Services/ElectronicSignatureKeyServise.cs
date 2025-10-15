using ShopProjectWebServer.Api.DtoModels.SignatureKey;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class ElectronicSignatureKeyServise : IElectronicSignatureKeyServise
    {
        public SignatureKeyDto GetSignatureKeyByUser(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var user = DataBaseMainController.DataBaseAccess.UserTable.GetUser(token);
            var result = DataBaseMainController.DataBaseAccess.SignatureKeyTable.GetKeyByUser(user.ID.ToString());
            return result.ToSignatureKeyDto();
        }
    }
}
