using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Threading.Tasks;
namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class TaxObjectTableAccess : ITaxObjectTableAccess
    {

        private readonly ContextDataBase _contextDataBase;
        public TaxObjectTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public async Task<TaxObjectEntity> AddAsync(TaxObjectEntity item)
        {
            await _contextDataBase.TaxObject.AddAsync(item);
            await _contextDataBase.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<TaxObjectEntity>> AddRangeAsync(IEnumerable<TaxObjectEntity> items)
        {
            await _contextDataBase.TaxObject.AddRangeAsync(items);
            await _contextDataBase.SaveChangesAsync();
            return items;
        }


        public void Delete(TaxObjectEntity item)
        {
            var entity = _contextDataBase.TaxObject.Find(item.ID);

            if (entity == null)
                return;

            _contextDataBase.TaxObject.Remove(entity);
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<TaxObjectEntity> GetAll()
        {
            return _contextDataBase.TaxObject.AsNoTracking().ToList();
        }
        public IEnumerable<TaxObjectEntity> GetByNameAndStatus(string name, TypeStatusTaxObject status)
        {
            IQueryable<TaxObjectEntity> query = _contextDataBase.TaxObject.AsNoTracking();

            if (status != TypeStatusTaxObject.Unknown)
            {
                query = query.Where(o => o.TypeStatus == status);
            }

            if (!(name == string.Empty))
            {
                query = query.Where(o => o.NameObject.Contains(name));
            }

            var result = query.ToList();
            return result;
        }

        public async Task UpdateAsync(TaxObjectEntity item)
        {
            var taxObject = _contextDataBase.TaxObject.Find(item.ID);
            if (taxObject != null)
            {
                taxObject.NameOwner = item.NameOwner;
                taxObject.NameObject = item.NameObject;
                taxObject.CodeObject = item.CodeObject;
                taxObject.Address = item.Address;

                await _contextDataBase.SaveChangesAsync();
            }


        }
        public async Task<bool> ExistsByName(string name)
        {
            return await _contextDataBase.TaxObject.AnyAsync(p => p.NameObject == name);
        }
        public async Task AddBindingOperationRecorderToTaxObject(Guid idTaxObject, IEnumerable<OperationsRecorderEntity> operationsRecorders)
        {
            var taxObject = _contextDataBase.TaxObject.Find(idTaxObject);

            if (taxObject.OperationsRecorder == null)
            {
                taxObject.OperationsRecorder = new List<OperationsRecorderEntity>();
            }

            foreach (var operationRecorder in operationsRecorders)
            {
                taxObject.OperationsRecorder.Add(_contextDataBase.OperationsRecorders.Find(operationRecorder.ID));
            }
            await _contextDataBase.SaveChangesAsync();
        }

        public async Task AddBindingUserToTaxObject(Guid idTaxObject, IEnumerable<UserEntity> users)
        {
            var taxObject = _contextDataBase.TaxObject.Find(idTaxObject);
             
            var items = new List<TaxObjectUserEnitity>();

            foreach(var user in users)
            {
                var temp = _contextDataBase.Users.Find(user.ID);
                if (temp != null)
                {
                    items.Add(new TaxObjectUserEnitity() { TaxObject = taxObject, User = temp }); 
                }
            } 
            await _contextDataBase.TaxObjectsUsers.AddRangeAsync(items); 
            await _contextDataBase.SaveChangesAsync();
        }

        public IEnumerable<TaxObjectUserEnitity> GetTaxObjectsAssignedUser(Guid userID)
        { 
            return _contextDataBase.TaxObjectsUsers.Include(t=>t.TaxObject).Include(o=>o.TaxObject.OperationsRecorder).Where(i => i.User.ID == userID).ToList();
        }

        public async Task UpdateParameterAsync(Guid id, string nameParameter, object valueParameter)
        {
            var user = _contextDataBase.TaxObject.Find(id);
            if (user != null)
            {
                switch (nameParameter)
                {
                    case nameof(user.Status):
                        {
                            user.TypeStatus = Enum.Parse<TypeStatusTaxObject>(valueParameter.ToString());
                            break;
                        } 
                }
            }
            await _contextDataBase.SaveChangesAsync();
        }

    }
}
