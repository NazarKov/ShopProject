using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationRecorderTableAccess : IOperationRecorderTableAccess 
    {
        private DbContextOptions<ContextDataBase> _option;
        public OperationRecorderTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public void Add(OperationsRecorderEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
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
            using (ContextDataBase context = new ContextDataBase(_option))
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
            using (ContextDataBase context = new ContextDataBase(_option))
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
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load(); 

                    if (context.OperationsRecorders != null)
                    {
                        var operationsRecorders = context.OperationsRecorders.Find(item.ID);
                        context.OperationsRecorders.Remove(operationsRecorders);
                    }
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<OperationsRecorderEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
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
        public IEnumerable<OperationsRecorderEntity> GetByNameAndStatus(string name, TypeStatusOperationRecorder status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                IQueryable<OperationsRecorderEntity> query = context.OperationsRecorders.AsNoTracking();

                if (status != TypeStatusOperationRecorder.Unknown)
                {
                    query = query.Where(o => o.TypeStatus == status);
                }

                if (!(name == string.Empty))
                {
                    query = query.Where(o => o.Name.Contains(name));
                }

                var result = query.ToList();
                return result;
            }
        } 
        public IEnumerable<OperationsRecorderEntity> SearchByNameAndUser(string item, Guid userId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.OperationsRecorderUsers.Load();

                    if (context.OperationsRecorders != null)
                    {
                        if (context.OperationsRecorderUsers != null)
                        {
                            var operationsRecorders = context.OperationsRecorderUsers.Where(i => i.OpertionsRecorders.Name.Contains(item)).ToList();


                            var result = new List<OperationsRecorderEntity>();

                            var operationRecorders = operationsRecorders.Where(u => u.Users.ID == userId).ToList();
                             
                            foreach (var operationsRecorder in operationRecorders)
                            {
                                result.Add(operationsRecorder.OpertionsRecorders);
                            }
                            return result;
                        }
                    }
                }
                return Enumerable.Empty<OperationsRecorderEntity>();
            }
        }

        public IEnumerable<OperationsRecorderEntity> SearchByNumberAndUser(string item, Guid userId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.OperationsRecorders.Load();
                    context.OperationsRecorderUsers.Load();
                    

                    if (context.OperationsRecorders != null)
                    {
                        if (context.OperationsRecorderUsers != null)
                        {
                            var operationsRecorders = context.OperationsRecorderUsers.Where(i => i.OpertionsRecorders.FiscalNumber.Contains(item)).ToList();
                            
                            
                            var result = new List<OperationsRecorderEntity>();
                            
                            var operationRecorders = operationsRecorders.Where(u => u.Users.ID == userId).ToList();

                            foreach (var operationsRecorder in operationRecorders)
                            {
                                result.Add(operationsRecorder.OpertionsRecorders);
                            }
                            return result;
                        }  
                    }
                }

                return Enumerable.Empty<OperationsRecorderEntity>();
            }
        }

        public void Update(OperationsRecorderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
