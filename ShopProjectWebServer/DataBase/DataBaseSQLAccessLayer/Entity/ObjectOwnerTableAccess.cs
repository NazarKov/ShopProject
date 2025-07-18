using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ObjectOwnerTableAccess : IObjectOwnerTableAccess 
    {
        private string _connectionString;
        public ObjectOwnerTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }


        public void Add(ObjectOwnerEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();

                    context.ObjectOwners.Add(item);
                }
                context.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<ObjectOwnerEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                    context.ObjectOwners.AddRange(items);
                }
                context.SaveChanges();
            }
        }


        public void Delete(ObjectOwnerEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ObjectOwnerEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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

        public void Update(ObjectOwnerEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
