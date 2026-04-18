using ShopProjectWebServer.Api.DtoModels.SignatureKey;

namespace ShopProjectWebServer.Services.Modules.Domain.ElectronicSignatureKey
{
    public interface IElectronicSignatureKeyServise
    {
        public SignatureKeyDto GetSignatureKeyByUser(string token);
    }
}
