using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Entities; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationRecorderUserTableAccess : IOperationRecorederUserTableAccess<OperationsRecorderUserEntity>
    {
        private string _connectionString;
        public OperationRecorderUserTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(OperationsRecorderUserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorderUsers.Load();

                    context.OperationsRecorderUsers.Add(item);
                }
                context.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<OperationsRecorderUserEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.Users.Load();
                    context.OperationsRecorderUsers.Load();

                    foreach (var item in items)
                    {
                        item.Users = context.Users.Find(item.Users.ID);
                        item.OpertionsRecorders = context.OperationsRecorders.Find(item.OpertionsRecorders.ID);

                        context.OperationsRecorderUsers.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationsRecorderUserEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.ObjectOwners.Load();
                    context.OperationsRecorders.Load();
                    context.OperationsRecorderUsers.Load();
                  
                    if (context.OperationsRecorderUsers.Count() != 0)
                    {
                        return context.OperationsRecorderUsers.ToList();
                    }
                    else
                    {
                        return new List<OperationsRecorderUserEntity>();
                    }
                }
                return null;
            }
        }

        public void Update(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
