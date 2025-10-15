using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationRecorderUserTableAccess : IOperationRecorederUserTableAccess 
    {
        private DbContextOptions<ContextDataBase> _option;
        public OperationRecorderUserTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public void Add(OperationsRecorderUserEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.OperationsRecorderUsers.Load();

                    context.OperationsRecorderUsers.Add(item);
                }
                context.SaveChanges();
            }
        }

        public void AddRange(Guid userId, IEnumerable<OperationsRecorderEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.Users.Load();
                    context.OperationsRecorderUsers.Load();

                    if (context.OperationsRecorderUsers != null)
                    {
                        foreach (var item in items)
                        {
                            context.OperationsRecorderUsers.Add(new OperationsRecorderUserEntity()
                            {
                                Users = context.Users.Find(userId),
                                OpertionsRecorders = context.OperationsRecorders.Find(item.ID),
                            });
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Delete(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationsRecorderUserEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.ObjectOwners.Load();
                    context.OperationsRecorders.Load();
                    context.OperationsRecorderUsers.Load();
                    context.UserTokens.Load();
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
