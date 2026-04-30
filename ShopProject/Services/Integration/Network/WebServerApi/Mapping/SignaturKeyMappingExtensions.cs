using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.SignatureKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Mapping
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
