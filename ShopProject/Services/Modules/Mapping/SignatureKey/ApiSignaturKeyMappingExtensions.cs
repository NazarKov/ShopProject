using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.SignatureKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.SignatureKey
{
    public static class ApiSignaturKeyMappingExtensions
    {
        public static ShopProject.Model.Domain.SignatureKey.SignatureKey ToSignatureKey(this SignatureKeyDto signatureKey)
        {
            return new ShopProject.Model.Domain.SignatureKey.SignatureKey()
            {
                Signature = signatureKey.Signature,
                SignaturePassword = signatureKey.SignaturePassword,
                CreateAt = signatureKey.CreateAt,
                EndAt = signatureKey.EndAt,
            };
        }
    }
}
