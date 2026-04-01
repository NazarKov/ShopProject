using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.UI.SignatureKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping
{
    internal static class SignatureKeyMappingExtensions
    {
        public static ShopProject.Model.Domain.SignatureKey.SignatureKey ToSignatureKey(this SignatureKeyModel item)
        {
            return new SignatureKey()
            {
                Signature = item.Signature,
                SignaturePassword = item.SignaturePassword,
                CreateAt = item.CreateAt,
                EndAt = item.EndAt,
                ID = item.ID,
            };
        }
        public static SignatureKeyModel ToSignatureKeyModel(this ShopProject.Model.Domain.SignatureKey.SignatureKey item)
        {
            return new SignatureKeyModel()
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
