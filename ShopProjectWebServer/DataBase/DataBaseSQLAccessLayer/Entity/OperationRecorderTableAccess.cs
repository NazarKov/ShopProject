using ShopProjectSQLDataBase.Context;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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

        public PaginatorData<OperationsRecorderEntity> GetAllPageColumn(double page, double countColumn, TypeStatusOperationRecorder status)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                { 
                    context.OperationsRecorders.Load();

                    if (context.OperationsRecorders != null && context.OperationsRecorders.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<OperationsRecorderEntity> result = new PaginatorData<OperationsRecorderEntity>();

                        if (status == TypeStatusOperationRecorder.Unknown)
                        {
                            result.Page = (int)page;
                            result.Data = context.OperationsRecorders.OrderBy(i => i.ID)
                                                          .Skip(countStart)
                                                          .Take((int)countColumn).ToList();

                            pages = context.OperationsRecorders.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var operationsRecorders = context.OperationsRecorders.Where(item => item.TypeStatus == status).ToList();
                            result.Data = operationsRecorders.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = operationsRecorders.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;
                    }
                    else
                    {
                        return new PaginatorData<OperationsRecorderEntity>();
                    }
                }
                return new PaginatorData<OperationsRecorderEntity>();
            }
        }

        public PaginatorData<OperationsRecorderEntity> GetOperationRecorderByNamePageColumn(string name, double page, double countColumn, TypeStatusOperationRecorder status)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load(); 

                    if (context.OperationsRecorders != null && context.OperationsRecorders.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<OperationsRecorderEntity> result = new PaginatorData<OperationsRecorderEntity>();

                        if (status == TypeStatusOperationRecorder.Unknown)
                        {
                            result.Page = (int)page;
                            var operationsRecorder = context.OperationsRecorders.Where(i => i.Name.Contains(name)).ToList();
                            result.Data = operationsRecorder.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = operationsRecorder.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var operationsRecorder = context.OperationsRecorders.Where(t => t.TypeStatus == status)
                                                           .Where(i => i.Name.Contains(name)).ToList();
                            result.Data = operationsRecorder.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = operationsRecorder.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;

                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new PaginatorData<OperationsRecorderEntity>();
            }
        }
         

        public IEnumerable<OperationsRecorderEntity> SearchByNameAndUser(string item, Guid userId)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
