using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationTableAccess : IOperationTableAccess 
    {
        private DbContextOptions<ContextDataBase> _option;
        public OperationTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }
        public void Add(OperationEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.WorkingShift.Load();
                    context.MediaAccessControls.Load();
                    context.Operations.Load();
                    if (context.Operations != null)
                    {
                        item.MAC = context.MediaAccessControls.Find(item.MAC.ID);
                        if(item.Shift != null)
                        {
                            item.Shift = context.WorkingShift.Find(item.Shift.ID);
                        }

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
            using (ContextDataBase context = new ContextDataBase(_option))
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

        public OperationEntity GetLastItem(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.First(i=>i.Shift.ID==shiftId);
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
