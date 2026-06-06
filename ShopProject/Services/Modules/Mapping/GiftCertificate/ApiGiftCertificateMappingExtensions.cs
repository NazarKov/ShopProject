using ShopProject.Model.Domain.GiftCertificate;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.GiftCertificate; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.GiftCertificate
{
    public static class ApiGiftCertificateMappingExtensions
    {
        public static UpdateGiftCertificateDto ToUpdateGiftCertificateDto(this ShopProject.Model.Domain.GiftCertificate.GiftCertificate item)
        {
            return new UpdateGiftCertificateDto()
            {
                Code = item.Code,
                Description = item.Description,
                ID = item.ID,
                Price = item.Price,
                Name = item.Name,
            };
        }
        public static CreateGiftCertificateDto ToCreateGiftCertificate(this ShopProject.Model.Domain.GiftCertificate.GiftCertificate item)
        {
            var result = new CreateGiftCertificateDto()
            {
                Code = item.Code, 
                Description = item.Description,
                Name = item.Name,
                Price = item.Price, 
            }; 
            return result;
        }
        public static ShopProject.Model.Domain.GiftCertificate.GiftCertificate ToGiftCertificate(this GiftCertificateDto item)
        {
            return new ShopProject.Model.Domain.GiftCertificate.GiftCertificate()
            {
                Code = item.Code,
                CreateAt = item.CreateAt,
                Description = item.Description, 
                ID = item.ID,
                Name = item.Name,
                Price = item.Price, 
                Status = (TypeStatusGiftCertificate)item.Status
            };
        }

        public static IEnumerable<ShopProject.Model.Domain.GiftCertificate.GiftCertificate> ToGiftCertificate(this IEnumerable<GiftCertificateDto> items)
        {
            var result = new List<ShopProject.Model.Domain.GiftCertificate.GiftCertificate>();
            foreach (var item in items)
            {
                result.Add(ToGiftCertificate(item));
            }
            return result;
        }
    }
}
