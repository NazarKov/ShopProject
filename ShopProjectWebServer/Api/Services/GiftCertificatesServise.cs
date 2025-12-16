using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.GiftCertificate;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class GiftCertificatesServise : IGiftCertificatesServise
    {
        public void Add(string token, CreateGiftCertificateDto item)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.GiftCertificatesTable.Add(item.ToGiftCetificate());
        }

        public GiftCertificateDto GetGiftCertificateByBarCode(string token, string barCode, TypeStatusGiftCertificate status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.GiftCertificatesTable.GetByBarCode(barCode, status).ToGiftCetificateDto();
        }

        public PaginatorDto<GiftCertificateDto> GetGiftCertificatesByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var certificate = DataBaseMainController.DataBaseAccess.GiftCertificatesTable.GetByNameAndStatus(name, status);

            var paginator = PaginatorDto<GiftCertificatesEntity>.CreationPaginator(certificate.Reverse(), page, countColumn);
            return new PaginatorDto<GiftCertificateDto>(paginator.Page, paginator.Pages, paginator.Data.ToGiftCetificateDto());
        }

        public PaginatorDto<GiftCertificateDto> GetGiftCertificatesPageColumn(string token, int page, int countColumn, TypeStatusGiftCertificate status)
            => GetGiftCertificatesByNamePageColumn(token, string.Empty, page, countColumn, status);

        public void Update(string token, UpdateGiftCertificateDto item)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.GiftCertificatesTable.Update(item.ToGiftCetificate());
        }

        public void UpdateParameterGiftCertificate(string token, string parameter, string value, UpdateGiftCertificateDto giftCertificates)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.GiftCertificatesTable.UpdateParameter(giftCertificates.ToGiftCetificate(),parameter,value);
        }
    }
}
