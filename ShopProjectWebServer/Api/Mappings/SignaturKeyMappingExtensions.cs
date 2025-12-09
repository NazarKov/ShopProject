using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.SignatureKey;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class SignaturKeyMappingExtensions
    {
        public static SignatureKeyDto ToSignatureKeyDto(this ElectronicSignatureKey item)
        {
            if (item != null)
            {
                return new SignatureKeyDto()
                {
                    Signature = item.Signature,
                    SignaturePassword = item.SignaturePassword,
                    CreateAt = item.CreateAt,
                    EndAt = item.EndAt,
                };
            }
            else
            {
                return new SignatureKeyDto();
            }
        }
    }
}
