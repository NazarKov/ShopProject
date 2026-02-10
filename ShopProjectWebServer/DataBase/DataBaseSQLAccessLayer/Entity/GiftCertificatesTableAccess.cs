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
            _contextDataBase.Users.Load();
            _contextDataBase.GiftCertificates.Load();

            if (item != null)
            {
                _contextDataBase.GiftCertificates.Add(item);

            }
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
            _contextDataBase.GiftCertificates.Load();

            if (_contextDataBase.GiftCertificates != null && _contextDataBase.GiftCertificates.Count() != 0)
            {
                GiftCertificatesEntity result = new GiftCertificatesEntity();
                if (status == TypeStatusGiftCertificate.Unknown)
                {
                    result = _contextDataBase.GiftCertificates.First(i => i.Code == barCode);
                }
                else
                {
                    result = _contextDataBase.GiftCertificates.Where(t => t.Status == status).First(i => i.Code == barCode);
                }
                return result;
            }
            else
            {
                throw new Exception("Неможливий пошук оскільки немає товарів");
            }
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
            _contextDataBase.Users.Load();
            _contextDataBase.GiftCertificates.Load();

            if (item != null)
            {
                var giftCertificate = _contextDataBase.GiftCertificates.Find(item.ID);
                if (giftCertificate != null)
                {
                    giftCertificate.Price = item.Price;
                    giftCertificate.Status = item.Status;
                    giftCertificate.Description = item.Description;
                    giftCertificate.Code = item.Code;
                }

            }
            _contextDataBase.SaveChanges();
        }

        public void UpdateParameter(GiftCertificatesEntity item, string parameter, object value)
        {
            _contextDataBase.Products.Load();

            if (_contextDataBase.Products.Count() != 0)
            {
                GiftCertificatesEntity giftCertificates;
                if (item.Code != null && item.Code != string.Empty)
                {
                    giftCertificates = _contextDataBase.GiftCertificates.Where(i => i.Code == item.Code).First();
                }
                else
                {
                    giftCertificates = _contextDataBase.GiftCertificates.Find(item.ID);
                }

                switch (parameter)
                {
                    case nameof(giftCertificates.Status):
                        {
                            var status = Enum.Parse<TypeStatusGiftCertificate>(value.ToString());
                            giftCertificates.Status = status;
                            break;
                        }
                }
                _contextDataBase.SaveChanges();
            }
        }
    }
}
