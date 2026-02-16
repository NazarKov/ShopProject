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
            if (item.OperationsRecorder != null)
            {
                item.OperationsRecorder = _contextDataBase.OperationsRecorders
                    .Find(item.OperationsRecorder.ID);
            }

            if (item.Operation != null)
            {
                item.Operation = _contextDataBase.Operations
                    .Find(item.Operation.ID);
            }

            if (item.WorkingShifts != null)
            {
                var shift = _contextDataBase.WorkingShift
                    .Find(item.WorkingShifts.ID);

                item.WorkingShifts = shift;

                if (shift.MACCreateAt == null)
                    shift.MACCreateAt = item;
                else
                    shift.MACEndAt = item;
            }

            item.SequenceNumber = _contextDataBase.MediaAccessControls.Count(i => i.OperationsRecorder.ID == item.OperationsRecorder.ID);

            _contextDataBase.MediaAccessControls.Add(item);

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
            return _contextDataBase.MediaAccessControls
                .Where(i => i.OperationsRecorder.ID == operationRecorderId)
                .OrderByDescending(i => i.ID)
                .FirstOrDefault();    
        }

        public void Update(MediaAccessControlEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
