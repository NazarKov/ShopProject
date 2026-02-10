using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class MediaAccessControlTableAccess : IMediaAccessControlTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public MediaAccessControlTableAccess(ContextDataBase contextDataBase) 
        {
            _contextDataBase = contextDataBase;
        } 

        public void Add(MediaAccessControlEntity item)
        {
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.WorkingShift.Load();
            _contextDataBase.MediaAccessControls.Load();
            _contextDataBase.Operations.Load();
            if (_contextDataBase.MediaAccessControls != null && _contextDataBase.OperationsRecorders != null && _contextDataBase.Operations != null && _contextDataBase.WorkingShift != null)
            {
                if (item.OperationsRecorder != null)
                {
                    item.OperationsRecorder = _contextDataBase.OperationsRecorders.FirstOrDefault(i => i.ID == item.OperationsRecorder.ID);
                }
                if (item.Operation != null)
                {
                    var operation = _contextDataBase.Operations.FirstOrDefault(i => i.ID == item.Operation.ID);
                    item.Operation = operation;
                }

                if (item.WorkingShifts != null)
                {
                    var shift = _contextDataBase.WorkingShift.FirstOrDefault(i => i.ID == item.WorkingShifts.ID);
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

                item.SequenceNumber = _contextDataBase.MediaAccessControls.Where(i => i.OperationsRecorder.ID == item.OperationsRecorder.ID).Count();

                _contextDataBase.MediaAccessControls.Add(item);
            }
            _contextDataBase.SaveChanges();
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

        public MediaAccessControlEntity GetByOperationId(int operationId)
        {
            IQueryable<MediaAccessControlEntity> query = _contextDataBase.MediaAccessControls.Include(o => o.Operation).AsNoTracking();

            query = query.Where(o => o.Operation.ID == operationId);

            var result = query.First();
            return result;
        }

        public MediaAccessControlEntity GetLastMAC(Guid operationRecorderId)
        {
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.MediaAccessControls.Load();
            if (_contextDataBase.MediaAccessControls != null)
            {
                var item = _contextDataBase.MediaAccessControls.Where(i => i.OperationsRecorder.ID == operationRecorderId).ToList().Last();
                if (item != null)
                {
                    return item;
                }
            }
            return null;
        }

        public void Update(MediaAccessControlEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
