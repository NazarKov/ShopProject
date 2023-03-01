using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Interfaces.InterfacesRepository
{
    internal interface ITableRepository<T,Type>
    {
        public void Add(T item) { }

        public void Update (T item) { }

        public void SetParameter(int ID, object item, Type type) { }
        
        public void Delete (T item) { }
        
        public object? GetId(int id) { return null; }
        
        public object? GetItem(string barCode) { return null; }

        public IEnumerable<object> GetAll () { return Enumerable.Empty<object> (); }
    }
}
