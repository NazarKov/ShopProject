using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Entities; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationRecorderTableAccess : IOperationRecorderTableAccess 
    {
        private string _connectionString;
        public OperationRecorderTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(OperationsRecorderEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();

                    context.OperationsRecorders.Add(item);
                }
                context.SaveChanges();
            }
        }

        public void AddBinding(Guid idoperationrecoreder, Guid idobjectowner)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.ObjectOwners.Load();
                    
                    var item = context.OperationsRecorders.Where( i=> i.ID == idoperationrecoreder).FirstOrDefault();
                    if(item != null)
                    {
                        item.ObjectOwner= context.ObjectOwners.Where(i=>i.ID == idobjectowner).FirstOrDefault();
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<OperationsRecorderEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();

                    context.OperationsRecorders.AddRange(items);
                }
                context.SaveChanges();
            }
        }

        public void Delete(OperationsRecorderEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationsRecorderEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();

                    if (context.OperationsRecorders.Count() != 0)
                    {
                        return context.OperationsRecorders.ToList();
                    }
                    else
                    {
                        return new List<OperationsRecorderEntity>();
                    }
                }
                return null;
            }
        }

        public void Update(OperationsRecorderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
