using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.GiftCertificate;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Services.Modules.Authorization;

namespace ShopProjectWebServer.Services.Modules.Domain.GiftCertificates
{
    internal class GiftCertificatesService : IGiftCertificatesServise
    {
        private DataBaseService _controller;
        private AuthorizationService _authorizationServise;

        public GiftCertificatesService(DataBaseService controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationService(controller);
        }
        public void Add(string token, CreateGiftCertificateDto item)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.GiftCertificatesTable.Add(item.ToGiftCetificate());
        }

        public GiftCertificateDto GetGiftCertificateByBarCode(string token, string barCode, TypeStatusGiftCertificate status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.GiftCertificatesTable.GetByBarCode(barCode, status).ToGiftCetificateDto();
        }

        public PaginatorDto<GiftCertificateDto> GetGiftCertificatesByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusGiftCertificate status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }

            var certificate = _controller.DataBaseAccess.GiftCertificatesTable.GetByNameAndStatus(name, status);

            var paginator = PaginatorDto<GiftCertificatesEntity>.CreationPaginator(certificate.Reverse(), page, countColumn);
            return new PaginatorDto<GiftCertificateDto>(paginator.Page, paginator.Pages, paginator.Data.ToGiftCetificateDto());
        }

        public PaginatorDto<GiftCertificateDto> GetGiftCertificatesPageColumn(string token, int page, int countColumn, TypeStatusGiftCertificate status)
            => GetGiftCertificatesByNamePageColumn(token, string.Empty, page, countColumn, status);

        public void Update(string token, UpdateGiftCertificateDto item)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.GiftCertificatesTable.Update(item.ToGiftCetificate());
        }

        public void UpdateParameterGiftCertificate(string token, string parameter, string value, UpdateGiftCertificateDto giftCertificates)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.GiftCertificatesTable.UpdateParameter(giftCertificates.ToGiftCetificate(),parameter,value);
        }
    }
}
