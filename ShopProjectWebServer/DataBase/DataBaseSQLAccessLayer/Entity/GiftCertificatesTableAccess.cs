using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class GiftCertificatesTableAccess : IGiftCertificatesTableAccess 
    {
        private DbContextOptions<ContextDataBase> _option;
        public GiftCertificatesTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }
        public void Add(GiftCertificatesEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.GiftCertificates.Load();

                    if (item != null)
                    { 
                        context.GiftCertificates.Add(item);
                        
                    }
                    context.SaveChanges();
                }
            }
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
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.GiftCertificates.Load(); 

                    if (context.GiftCertificates != null && context.GiftCertificates.Count() != 0)
                    {
                        GiftCertificatesEntity result = new GiftCertificatesEntity();
                        if (status == TypeStatusGiftCertificate.Unknown)
                        {
                            result = context.GiftCertificates.First(i => i.Code == barCode);
                        }
                        else
                        {
                            result = context.GiftCertificates.Where(t => t.Status == status).First(i => i.Code == barCode);
                        }
                        return result;
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new GiftCertificatesEntity();
            }
        }

        public IEnumerable<GiftCertificatesEntity> GetByNameAndStatus(string name, TypeStatusGiftCertificate status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                IQueryable<GiftCertificatesEntity> query = context.GiftCertificates.AsNoTracking();

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
        }

        public void Update(GiftCertificatesEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.GiftCertificates.Load();

                    if (item != null)
                    {
                        var giftCertificate = context.GiftCertificates.Find(item.ID);
                        if (giftCertificate != null)
                        {
                            giftCertificate.Price = item.Price;
                            giftCertificate.Status = item.Status; 
                            giftCertificate.Description = item.Description;
                            giftCertificate.Code = item.Code;
                        }

                    }
                    context.SaveChanges();
                }
            }
        }

        public void UpdateParameter(GiftCertificatesEntity item, string parameter, object value)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Products.Load();

                    if (context.Products.Count() != 0)
                    {
                        GiftCertificatesEntity giftCertificates;
                        if (item.Code != null && item.Code != string.Empty)
                        {
                            giftCertificates = context.GiftCertificates.Where(i => i.Code == item.Code).First();
                        }
                        else
                        {
                            giftCertificates = context.GiftCertificates.Find(item.ID);
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
                    }
                }
                context.SaveChanges();
            }
        }
       }
}
