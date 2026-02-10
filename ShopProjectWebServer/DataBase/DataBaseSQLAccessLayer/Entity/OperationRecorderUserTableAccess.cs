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
            _contextDataBase.OperationsRecorderUsers.Load();

            _contextDataBase.OperationsRecorderUsers.Add(item);
            _contextDataBase.SaveChanges(); 
        }

        public void AddRange(Guid userId, IEnumerable<OperationsRecorderEntity> items)
        {
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.Users.Load();
            _contextDataBase.OperationsRecorderUsers.Load();

            if (_contextDataBase.OperationsRecorderUsers != null)
            {
                foreach (var item in items)
                {
                    _contextDataBase.OperationsRecorderUsers.Add(new OperationsRecorderUserEntity()
                    {
                        Users = _contextDataBase.Users.Find(userId),
                        OpertionsRecorders = _contextDataBase.OperationsRecorders.Find(item.ID),
                    });
                }
            }
            _contextDataBase.SaveChanges();
        }

        public void Delete(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationsRecorderUserEntity> GetAll()
        {
            _contextDataBase.Users.Load();
            _contextDataBase.ObjectOwners.Load();
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.OperationsRecorderUsers.Load();
            _contextDataBase.UserTokens.Load();
            if (_contextDataBase.OperationsRecorderUsers.Count() != 0)
            {
                return _contextDataBase.OperationsRecorderUsers.ToList();
            }
            else
            {
                return new List<OperationsRecorderUserEntity>();
            }
        }

        public void Update(OperationsRecorderUserEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
