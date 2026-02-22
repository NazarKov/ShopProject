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

        public void Add(OperationsRecorderEntity item)
        { 
            _contextDataBase.OperationsRecorders.Add(item);
            _contextDataBase.SaveChanges();
        }

        public void AddBinding(Guid idoperationrecoreder, Guid idobjectowner)
        {
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.ObjectOwners.Load();

            var item = _contextDataBase.OperationsRecorders.Where(i => i.ID == idoperationrecoreder).FirstOrDefault();
            if (item != null)
            {
                item.ObjectOwner = _contextDataBase.ObjectOwners.Where(i => i.ID == idobjectowner).FirstOrDefault();
            }
            _contextDataBase.SaveChanges();
        }

        public void AddRange(IEnumerable<OperationsRecorderEntity> items)
        { 
            _contextDataBase.OperationsRecorders.AddRange(items);
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
        public IEnumerable<OperationsRecorderEntity> SearchByNameAndUser(string item, Guid userId)
        {

            var result = _contextDataBase.OperationsRecorderUsers
                            .Where(u => u.Users.ID == userId
                                        && u.OpertionsRecorders.Name.Contains(item)).Include(o=>o.OpertionsRecorders.ObjectOwner)
                            .Select(u => u.OpertionsRecorders)
                            .Distinct()
                            .ToList();
            return result;
        }

        public IEnumerable<OperationsRecorderEntity> SearchByNumberAndUser(string item, Guid userId)
        {

            var result = _contextDataBase.OperationsRecorderUsers
                            .Where(u => u.Users.ID == userId
                                        && u.OpertionsRecorders.FiscalNumber.Contains(item))
                            .Select(u => u.OpertionsRecorders)
                            .Distinct()
                            .ToList();
            return result; 
        }

        public void Update(OperationsRecorderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
