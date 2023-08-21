using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class GoodsOperationTableAccess : IEntityAccessor<GoodsOperation>
    {
        public void Add(GoodsOperation item)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.operations.Load();
                    context.goods.Load();
                    context.goodsOperations.Load();
                    if (item != null)
                    {
                        context.goodsOperations.Add(new GoodsOperation()
                        {
                            count = item.count,
                            goods = context.goods.Find(item.goods.id),
                            operation = context.operations.Find(item.operation.id),
                        });
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddRange(List<GoodsOperation> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(GoodsOperation item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<GoodsOperation> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsOperation> GetAll(string statusGoods)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsOperation> GetAll()
        {
            using(ContextDataBase  context = new ContextDataBase())
            {
                if(context!= null)
                {
                    context.goods.Load();
                    context.operations.Load();
                    context.goodsOperations.Load();
                    if(context.goodsOperations.Count()!=0)
                    {
                        return context.goodsOperations.ToList();
                    }
                }
                return new List<GoodsOperation>();
            }
        }

        public GoodsOperation GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public GoodsOperation GetItemId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(GoodsOperation item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<GoodsOperation> items)
        {
            throw new NotImplementedException();
        }
    }
}
