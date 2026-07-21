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

        private readonly ContextDataBase _contextDataBase;
        public OperationRecorderTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public async Task<OperationsRecorderEntity> AddAsync(OperationsRecorderEntity item)
        { 
            await _contextDataBase.OperationsRecorders.AddAsync(item);
            await _contextDataBase.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<OperationsRecorderEntity>> AddRangeAsync(IEnumerable<OperationsRecorderEntity> items)
        {
            await _contextDataBase.OperationsRecorders.AddRangeAsync(items);
            await _contextDataBase.SaveChangesAsync();
            return items;
        }

        public void AddBinding(Guid idoperationrecoreder, Guid idobjectowner)
        {
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.TaxObject.Load();

            var item = _contextDataBase.OperationsRecorders.Where(i => i.ID == idoperationrecoreder).FirstOrDefault();
            if (item != null)
            {
                item.TaxObject = _contextDataBase.TaxObject.Where(i => i.ID == idobjectowner).FirstOrDefault();
            }
            _contextDataBase.SaveChanges();
        }
         
        public void Delete(OperationsRecorderEntity item)
        {

            var entity = _contextDataBase.OperationsRecorders.Find(item);

            if (entity == null) return;
             
            _contextDataBase.OperationsRecorders.Remove(entity);
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<OperationsRecorderEntity> GetAll()
        {
            return _contextDataBase.OperationsRecorders.AsNoTracking().ToList();
        } 
        public IEnumerable<OperationsRecorderEntity> GetByNameAndStatus(string name, TypeStatusOperationRecorder status)
        {
            IQueryable<OperationsRecorderEntity> query = _contextDataBase.OperationsRecorders.AsNoTracking();

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
        public void Update(OperationsRecorderEntity item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _contextDataBase.OperationsRecorders.AnyAsync(p => p.Name == name);
        }
    }
}
