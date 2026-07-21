using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Services.Modules.Mapping.SignatureKey
{
    public static class SignatureKeyEntityMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.SignatureKey.SignatureKey ToSignatureKey(this ElectronicSignatureKey item)
        {
            return new Models.Domain.SignatureKey.SignatureKey()
            {
                Signature = item.Signature,
                SignaturePassword = item.SignaturePassword,
                CreateAt = item.CreateAt,
                EndAt = item.EndAt,
                ID = item.ID,
            };
        }
    }
}
