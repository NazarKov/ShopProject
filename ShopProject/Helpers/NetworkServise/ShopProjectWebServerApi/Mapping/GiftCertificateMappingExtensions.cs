using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.GiftCertificate;
using ShopProject.UIModel.GiftCertificatesPage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class GiftCertificateMappingExtensions
    {
        public static UpdateGiftCertificateDto ToUpdateGiftCertificateDto(this GiftCertificate item)
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
        public static CreateGiftCertificateDto ToCreateGiftCertificate(this GiftCertificate item)
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
        public static GiftCertificate ToGiftCertificate(this GiftCertificateDto item)
        {
            return new GiftCertificate()
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

        public static IEnumerable<GiftCertificate> ToGiftCertificate(this IEnumerable<GiftCertificateDto> items)
        {
            var result = new List<GiftCertificate>();
            foreach (var item in items)
            {
                result.Add(ToGiftCertificate(item));
            }
            return result;
        }
    }
}
