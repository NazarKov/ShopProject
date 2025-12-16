using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.GiftCertificate;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IGiftCertificatesServise
    {
        void Add(string token, CreateGiftCertificateDto item);
        void Update(string token, UpdateGiftCertificateDto item);
        public GiftCertificateDto GetGiftCertificateByBarCode(string token, string barCode, TypeStatusGiftCertificate status);
        public PaginatorDto<GiftCertificateDto> GetGiftCertificatesByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusGiftCertificate status);
        public PaginatorDto<GiftCertificateDto> GetGiftCertificatesPageColumn(string token, int page, int countColumn, TypeStatusGiftCertificate status);
        public void UpdateParameterGiftCertificate(string token, string parameter, string value, UpdateGiftCertificateDto giftCertificates);
    }
}
