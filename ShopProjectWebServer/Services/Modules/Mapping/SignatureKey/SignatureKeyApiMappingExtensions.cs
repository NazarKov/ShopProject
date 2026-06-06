using ShopProjectWebServer.Api.DtoModels.SignatureKey;

namespace ShopProjectWebServer.Services.Modules.Mapping.SignatureKey
{
    public static class SignatureKeyApiMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.SignatureKey.SignatureKey ToSignatureKey(this SignatureKeyDto item)
        {
            return new Models.Domain.SignatureKey.SignatureKey()
            {
                Signature = item.Signature,
                SignaturePassword = item.SignaturePassword,
                CreateAt = item.CreateAt,
                EndAt = item.EndAt,
                ID = item.ID
            };
        }
        public static SignatureKeyDto ToSignatureKey(this ShopProjectWebServer.Models.Domain.SignatureKey.SignatureKey item)
        {
            return new SignatureKeyDto()
            {
                Signature = item.Signature,
                SignaturePassword = item.SignaturePassword,
                CreateAt = item.CreateAt,
                EndAt = item.EndAt,
                ID = item.ID
            };
        }
    }
}
