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

        private DbContextOptions<ContextDataBase> _option;
        public ObjectOwnerTableAccess(DbContextOptions<ContextDataBase> option)
        { 
            _option = option;
        } 

        public void Add(ObjectOwnerEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    context.ObjectOwners.Add(item);
                    
                    context.SaveChanges();
                }
            }
        }

        public void AddRange(IEnumerable<ObjectOwnerEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                    context.ObjectOwners.AddRange(items);
                    context.SaveChanges();
                }
            }
        }


        public void Delete(ObjectOwnerEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    if (context.ObjectOwners != null)
                    {
                        var operationsRecorders = context.ObjectOwners.Find(item.ID);
                        context.ObjectOwners.Remove(operationsRecorders);
                    }
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<ObjectOwnerEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                  
                    if (context.ObjectOwners.Count() != 0)
                    {
                        return context.ObjectOwners.ToList();
                    }
                    else
                    {
                        return new List<ObjectOwnerEntity>();
                    }
                }
                return null;
            }
        } 
        public IEnumerable<ObjectOwnerEntity> GetByNameAndStatus(string name, TypeStatusObjectOwner status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                IQueryable<ObjectOwnerEntity> query = context.ObjectOwners.AsNoTracking();

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
        } 
      
        public void Update(ObjectOwnerEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
