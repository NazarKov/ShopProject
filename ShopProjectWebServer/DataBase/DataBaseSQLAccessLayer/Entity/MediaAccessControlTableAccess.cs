using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class MediaAccessControlTableAccess : IMediaAccessControlTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public MediaAccessControlTableAccess(DbContextOptions<ContextDataBase> option) 
        {
            _option = option;
        } 

        public void Add(MediaAccessControlEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.WorkingShift.Load();
                    context.MediaAccessControls.Load();
                    context.Operations.Load();
                    if (context.MediaAccessControls != null && context.OperationsRecorders != null && context.Operations != null && context.WorkingShift != null)
                    {
                        if (item.OperationsRecorder != null)
                        {
                            item.OperationsRecorder = context.OperationsRecorders.FirstOrDefault(i => i.ID == item.OperationsRecorder.ID);
                        }
                        if (item.Operation != null)
                        {
                            var operation = context.Operations.FirstOrDefault(i => i.ID == item.Operation.ID);
                            item.Operation = operation;
                        }

                        if (item.WorkingShifts != null)
                        {
                            var shift = context.WorkingShift.FirstOrDefault(i => i.ID == item.WorkingShifts.ID);
                            item.WorkingShifts = shift;

                            if (shift.MACCreateAt == null)
                            {
                                shift.MACCreateAt = item;
                            }
                            else
                            {
                                shift.MACEndAt = item;
                            }
                        }

                        item.SequenceNumber = context.MediaAccessControls.Where(i => i.OperationsRecorder.ID == item.OperationsRecorder.ID).Count();

                        context.MediaAccessControls.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void AddRange(Guid userID, IEnumerable<MediaAccessControlEntity> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(MediaAccessControlEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MediaAccessControlEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public MediaAccessControlEntity GetLastMAC(Guid operationRecorderId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.MediaAccessControls.Load();
                    if (context.MediaAccessControls != null)
                    {
                        var item = context.MediaAccessControls.Where(i => i.OperationsRecorder.ID == operationRecorderId).ToList().Last();
                        if(item != null)
                        {
                            return item;
                        } 
                    }
                }
                throw new Exception();
            }
        }

        public void Update(MediaAccessControlEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
