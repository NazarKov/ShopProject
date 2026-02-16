using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationRecorderUserTableAccess : IOperationRecorederUserTableAccess 
    {

        private readonly ContextDataBase _contextDataBase;
        public OperationRecorderUserTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public void Add(OperationsRecorderUserEntity item)
        { 
            _contextDataBase.OperationsRecorderUsers.Add(item);
            _contextDataBase.SaveChanges(); 
        }

        public void AddRange(Guid userId, IEnumerable<OperationsRecorderEntity> items)
        {
            foreach (var item in items)
            {
                _contextDataBase.OperationsRecorderUsers.Add(new OperationsRecorderUserEntity()
                {
                    Users = _contextDataBase.Users.Find(userId),
                    OpertionsRecorders = _contextDataBase.OperationsRecorders.Find(item.ID),
                });
            }
            _contextDataBase.SaveChanges();
        }

        public void Delete(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationsRecorderUserEntity> GetAll()
        {
            return _contextDataBase.OperationsRecorderUsers.Include(u => u.Users).Include(o => o.OpertionsRecorders).AsNoTracking().ToList();
        }

        public void Update(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
