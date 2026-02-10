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
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public GiftCertificatesServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
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
