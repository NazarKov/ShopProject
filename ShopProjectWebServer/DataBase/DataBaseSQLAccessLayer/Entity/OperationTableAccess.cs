using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationTableAccess : IOperationTableAccess 
    {
        private string _connectionString;
        public OperationTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        public void Add(OperationEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    if (context.Products != null)
                    {
                        context.Operations.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Delete(OperationEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Operations.Load(); 

                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.ToList();
                    }
                    else
                    {
                        return new List<OperationEntity>();
                    }
                }
                return null;
            }
        }

        public OperationEntity GetLastItem()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.ElementAt(context.Operations.Count() - 1);
                    }
                    else
                    {
                        return new OperationEntity();
                    }
                }
                return null;
            }
        }

        public void Update(OperationEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
