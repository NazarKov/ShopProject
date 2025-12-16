using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IGiftCertificatesTableAccess 
    {
        void Add(GiftCertificatesEntity item);
        void Update(GiftCertificatesEntity item);
        void UpdateParameter(GiftCertificatesEntity item, string parameter, object value);
        void Delete(GiftCertificatesEntity item);
        IEnumerable<GiftCertificatesEntity> GetAll();
        public GiftCertificatesEntity GetByBarCode(string barCode, TypeStatusGiftCertificate status);
        public IEnumerable<GiftCertificatesEntity> GetByNameAndStatus(string name, TypeStatusGiftCertificate status);
    }
}
