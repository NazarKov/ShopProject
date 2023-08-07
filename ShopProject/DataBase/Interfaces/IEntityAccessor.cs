using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Interfaces
{
    internal interface IEntityAccessor<T>
    {
        public void Add(T item);

        public void AddRange(List<T> items);

        public void Update(T item);

        public void UpdateRange(List<T> items);

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter);

        public void Delete(T item);

        public void DeleteRange(List<T> items);

        public T GetItemId(Guid id);

        public T GetItemBarCode(string barCode);

        public IEnumerable<T> GetAll();
    }
}
