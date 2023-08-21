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
    internal class OperationTableAccess : IEntityAccessor<Operation>
    {
        public void Add(Operation item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.operations.Load();
                    if (item != null)
                    {
                        context.operations.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddRange(List<Operation> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(Operation item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<Operation> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Operation> GetAll(string statusGoods)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Operation> GetAll()
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.goods.Load();
                    context.operations.Load();
                    if (context.operations.Count() != 0)
                    {
                        return context.operations.ToList();
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                return null;
            }
        }

        public Operation GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public Operation GetItemId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Operation item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<Operation> items)
        {
            throw new NotImplementedException();
        }
    }
}
