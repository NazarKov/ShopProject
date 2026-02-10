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
            _contextDataBase.OperationsRecorders.Load();

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
            _contextDataBase.OperationsRecorders.Load();

            _contextDataBase.OperationsRecorders.AddRange(items);
            _contextDataBase.SaveChanges();
        }

        public void Delete(OperationsRecorderEntity item)
        {
            _contextDataBase.OperationsRecorders.Load();

            if (_contextDataBase.OperationsRecorders != null)
            {
                var operationsRecorders = _contextDataBase.OperationsRecorders.Find(item.ID);
                _contextDataBase.OperationsRecorders.Remove(operationsRecorders);
            }
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<OperationsRecorderEntity> GetAll()
        {
            _contextDataBase.OperationsRecorders.Load();

            if (_contextDataBase.OperationsRecorders.Count() != 0)
            {
                return _contextDataBase.OperationsRecorders.ToList();
            }
            else
            {
                return new List<OperationsRecorderEntity>();
            }
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
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.OperationsRecorderUsers.Load();

            if (_contextDataBase.OperationsRecorders != null)
            {
                if (_contextDataBase.OperationsRecorderUsers != null)
                {
                    var operationsRecorders = _contextDataBase.OperationsRecorderUsers.Where(i => i.OpertionsRecorders.Name.Contains(item)).ToList();


                    var result = new List<OperationsRecorderEntity>();

                    var operationRecorders = operationsRecorders.Where(u => u.Users.ID == userId).ToList();

                    foreach (var operationsRecorder in operationRecorders)
                    {
                        result.Add(operationsRecorder.OpertionsRecorders);
                    }
                    return result;
                }
            }
            return null;
        }

        public IEnumerable<OperationsRecorderEntity> SearchByNumberAndUser(string item, Guid userId)
        {
            _contextDataBase.Users.Load();
            _contextDataBase.OperationsRecorders.Load();
            _contextDataBase.OperationsRecorderUsers.Load();


            if (_contextDataBase.OperationsRecorders != null)
            {
                if (_contextDataBase.OperationsRecorderUsers != null)
                {
                    var operationsRecorders = _contextDataBase.OperationsRecorderUsers.Where(i => i.OpertionsRecorders.FiscalNumber.Contains(item)).ToList();


                    var result = new List<OperationsRecorderEntity>();

                    var operationRecorders = operationsRecorders.Where(u => u.Users.ID == userId).ToList();

                    foreach (var operationsRecorder in operationRecorders)
                    {
                        result.Add(operationsRecorder.OpertionsRecorders);
                    }
                    return result;
                }
            }
            return null;
        }

        public void Update(OperationsRecorderEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
