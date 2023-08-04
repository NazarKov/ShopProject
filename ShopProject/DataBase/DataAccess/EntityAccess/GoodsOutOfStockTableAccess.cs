using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class GoodsOutOfStockTableAccess : IEntityAccessor<GoodsOutOfStock>
    {
        public void Add(GoodsOutOfStock item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<GoodsOutOfStock> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(GoodsOutOfStock item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<GoodsOutOfStock> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsOutOfStock> GetAll()
        {
            throw new NotImplementedException();
        }

        public GoodsOutOfStock GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public GoodsOutOfStock GetItemId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(GoodsOutOfStock item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(GoodsOutOfStock item, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<GoodsOutOfStock> items)
        {
            throw new NotImplementedException();
        }
    }
}
