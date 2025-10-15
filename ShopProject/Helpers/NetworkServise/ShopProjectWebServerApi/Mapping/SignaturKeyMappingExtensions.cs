using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.SignatureKey;
using ShopProject.UIModel.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class SignaturKeyMappingExtensions
    {
        public static SignatureKey ToSignatureKey(this SignatureKeyDto signatureKey)
        {
            return new SignatureKey()
            {
                Signature = signatureKey.Signature,
                SignaturePassword = signatureKey.SignaturePassword,
                CreateAt = signatureKey.CreateAt,
                EndAt = signatureKey.EndAt,
            };
        }
    }
}
