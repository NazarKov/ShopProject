using ShopProjectWebServer.Api.DtoModels.SignatureKey;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IElectronicSignatureKeyServise
    {
        public SignatureKeyDto GetSignatureKeyByUser(string token);
    }
}
