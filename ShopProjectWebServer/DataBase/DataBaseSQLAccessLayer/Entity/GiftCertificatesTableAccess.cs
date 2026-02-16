using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class GiftCertificatesTableAccess : IGiftCertificatesTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public GiftCertificatesTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public void Add(GiftCertificatesEntity item)
        {
            _contextDataBase.GiftCertificates.Add(item);
            _contextDataBase.SaveChanges();
        }

        public void Delete(GiftCertificatesEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GiftCertificatesEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public GiftCertificatesEntity GetByBarCode(string barCode, TypeStatusGiftCertificate status)
        {
            GiftCertificatesEntity result;

            if (status == TypeStatusGiftCertificate.Unknown)
            {
                result = _contextDataBase.GiftCertificates.FirstOrDefault(i => i.Code == barCode);
            }
            else
            {
                result = _contextDataBase.GiftCertificates.FirstOrDefault(i => i.Code == barCode && i.Status == status);
            }

            if (result == null)
                throw new Exception("Сертифікат не знайдено");

            return result;
        }

        public IEnumerable<GiftCertificatesEntity> GetByNameAndStatus(string name, TypeStatusGiftCertificate status)
        {
            IQueryable<GiftCertificatesEntity> query = _contextDataBase.GiftCertificates.AsNoTracking();

            if (status != TypeStatusGiftCertificate.Unknown)
            {
                query = query.Where(o => o.Status == status);
            }

            if (!(name == string.Empty))
            {
                query = query.Where(o => o.Name.Contains(name));
            }

            var result = query.ToList();
            return result;
        }

        public void Update(GiftCertificatesEntity item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var giftCertificate = _contextDataBase.GiftCertificates
                .FirstOrDefault(g => g.ID == item.ID);

            if (giftCertificate == null)
                throw new Exception("Сертифікат не знайдено");

            giftCertificate.Price = item.Price;
            giftCertificate.Status = item.Status;
            giftCertificate.Description = item.Description;
            giftCertificate.Code = item.Code;

            _contextDataBase.SaveChanges();
        }

        public void UpdateParameter(GiftCertificatesEntity item, string parameter, object value)
        {
            GiftCertificatesEntity giftCertificate;

            if (!string.IsNullOrWhiteSpace(item.Code))
            {
                giftCertificate = _contextDataBase.GiftCertificates .FirstOrDefault(i => i.Code == item.Code);
            }
            else
            {
                giftCertificate = _contextDataBase.GiftCertificates.Find(item.ID);
            }

            if (giftCertificate == null)
                throw new Exception("Сертифікат не знайдено");

            switch (parameter)
            {
                case nameof(giftCertificate.Status):
                    {
                        var status = Enum.Parse<TypeStatusGiftCertificate>(value.ToString());
                        giftCertificate.Status = status;
                        break;
                    }
            } 
            _contextDataBase.SaveChanges();
        }
    }
}
