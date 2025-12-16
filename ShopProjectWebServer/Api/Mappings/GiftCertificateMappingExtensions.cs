using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.GiftCertificate;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class GiftCertificateMappingExtensions
    {
        public static GiftCertificatesEntity ToGiftCetificate(this UpdateGiftCertificateDto item)
        {
            return new GiftCertificatesEntity()
            {
                ID = item.ID,
                Description = item.Description,
                Code = item.Code,
                Name = item.Name,
                Price = item.Price,
            };
        }
        public static GiftCertificatesEntity ToGiftCetificate(this CreateGiftCertificateDto item)
        {
            var result = new GiftCertificatesEntity()
            {
                Code = item.Code,
                CreateAt = item.CreateAt,
                Description = item.Description,
                Name = item.Name,
                Price = item.Price,
                Status = ShopProjectDataBase.Helper.TypeStatusGiftCertificate.InStock, 
            }; 
            return result;
        }
        public static GiftCertificateDto ToGiftCetificateDto(this GiftCertificatesEntity item)
        {
            var result = new GiftCertificateDto()
            {
                Status = (int)item.Status,
                Code = item.Code,
                CreateAt = item.CreateAt,
                Description = item.Description, 
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
            }; 
            return result;
        }

        public static IEnumerable<GiftCertificateDto> ToGiftCetificateDto(this IEnumerable<GiftCertificatesEntity> items)
        {
            var result = new List<GiftCertificateDto>();
            foreach (var item in items)
            {
                result.Add(ToGiftCetificateDto(item));
            }
            return result;
        }
    }
}
