using ShopProjectDataBase.DataBase.Model;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IGiftCertificatesTableAccess 
    {
        void Add(GiftCertificatesEntity item);
        void Update(GiftCertificatesEntity item);
        void Delete(GiftCertificatesEntity item);
        IEnumerable<GiftCertificatesEntity> GetAll();
    }
}
