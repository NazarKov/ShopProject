using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ObjectOwnerTableAccess : IObjectOwnerTableAccess 
    {

        private readonly ContextDataBase _contextDataBase;
        public ObjectOwnerTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        } 

        public void Add(ObjectOwnerEntity item)
        {
            _contextDataBase.ObjectOwners.Add(item); 
            _contextDataBase.SaveChanges();
        }

        public void AddRange(IEnumerable<ObjectOwnerEntity> items)
        { 
            _contextDataBase.ObjectOwners.AddRange(items);
            _contextDataBase.SaveChanges();
        }


        public void Delete(ObjectOwnerEntity item)
        {
            var entity = _contextDataBase.ObjectOwners.Find(item.ID);

            if (entity == null)
                return;

            _contextDataBase.ObjectOwners.Remove(entity);
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<ObjectOwnerEntity> GetAll()
        {
            return _contextDataBase.ObjectOwners.AsNoTracking().ToList();
        } 
        public IEnumerable<ObjectOwnerEntity> GetByNameAndStatus(string name, TypeStatusObjectOwner status)
        {
            IQueryable<ObjectOwnerEntity> query = _contextDataBase.ObjectOwners.AsNoTracking();

            if (status != TypeStatusObjectOwner.Unknown)
            {
                query = query.Where(o => o.TypeStatus == status);
            }

            if (!(name == string.Empty))
            {
                query = query.Where(o => o.NameObject.Contains(name));
            }

            var result = query.ToList();
            return result;
        } 
      
        public void Update(ObjectOwnerEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
