using Microsoft.EntityFrameworkCore;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.DataAccess.DBAccess;
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
    internal class GoodsTableAccess : IEntityAccessor<Goods>
    {
        public void Add(Goods item)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.goods.Load();
                    context.goodsUnits.Load();
                    context.codeUKTZED.Load();
                    if (context.goods != null)
                    {
                        item.unit = context.goodsUnits.Find(item.unit.id);
                        item.codeUKTZED = context.codeUKTZED.Find(item.codeUKTZED.id);

                        context.goods.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void AddRange(List<Goods> items)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.goods.Load();
                    if (context.goods != null)
                    {
                        context.goods.AddRange(items);
                    }
                    context.SaveChangesAsync();
                }
            }
        }

        public void Delete(Goods item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.goods.Load();
                    if (context.goods != null)
                    {
                        if (item != null)
                        {
                            context.goods.Remove(context.goods.Find(item.id));
                        }
                        else
                        {
                            throw new Exception("Товар не знайдено");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public void DeleteRange(List<Goods> items)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context != null)
                {
                    context.goods.Load();
                    if(context.goods != null)
                    {
                        if(items!= null)
                        {
                            context.goods.RemoveRange(items);
                        }
                        else
                        {
                            throw new Exception("Товарів не знайдено");
                        }
                    }
                    context.SaveChangesAsync();
                }
            }
        }

        public IEnumerable<Goods> GetAll(string status)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.goods.Load();
                    context.goodsUnits.Load();
                    context.codeUKTZED.Load();
                    if(context.goods != null)
                    {
                        if(context.goods.Count()!=0)
                        {
                            return context.goods.Where(item => item.status == status).ToList();
                        }
                        else
                        {
                            throw new Exception("База даних не містить товарів.\nСтворіть товар");
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                }
                else { throw new Exception(); }
            }
        }

        public IEnumerable<Goods> GetAll()
        {
            throw new NotImplementedException();
        }

        public Goods? GetItemBarCode(string barCode)
        {
            using(ContextDataBase context =new ContextDataBase())
            {
                if(context!=null)
                {
                    context.goods.Load();
                    if(context.goods!=null)
                    {
                        if (context.goods.Count() == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return context.goods.Where(item => item.code == barCode).FirstOrDefault();
                        }
                    }
                    else
                    {
                        throw new Exception("Товарів не знайдено");
                    }
                }
                else
                { throw new Exception(); }
            }
        }

        public Goods GetItemId(Guid id)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.goods.Load();
                    if(context.goods != null)
                    {
                        if(context.goods.Count()!=0)
                        {
                            throw new Exception("База даних не містить товари");
                        }
                        return context.goods.Find(id);
                    }
                    else
                    {
                        throw new Exception("Товар не знайдено");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public void Update(Goods item)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context !=null)
                {
                    context.goods.Load();
                    context.goodsUnits.Load();
                    context.codeUKTZED.Load();
                    if(context.goods != null)
                    {
                        if(item!=null)
                        {
                            UpdateFieldGood(context.goods.Find(item.id), item,context.goodsUnits.Find(item.unit.id),context.codeUKTZED.Find(item.codeUKTZED.id));
                        }
                        else
                        {
                            throw new Exception("Товар не заповнено");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
     
        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            switch(nameParameter)
            {
                case "status":
                    {
                        using (ContextDataBase context = new ContextDataBase())
                        {
                            if (context != null)
                            {
                                context.goods.Load();
                                if (context.goods.Count() != 0)
                                {
                                    var item = context.goods.Find(id);
                                    item.status = valueParameter.ToString();
                                }
                            }
                            context.SaveChanges();
                        }
                        break;
                    }
                case "arhived":
                    {
                        using (ContextDataBase context = new ContextDataBase())
                        {
                            if (context != null)
                            {
                                context.goods.Load();
                                if (context.goods.Count() != 0)
                                {
                                    var item = context.goods.Find(id);
                                    if (valueParameter == null)
                                    {
                                        item.arhivedAt = new DateTimeOffset();
                                    }
                                    else
                                    {

                                        item.arhivedAt = (DateTime)valueParameter;
                                    }
                                }
                            }
                            context.SaveChanges();
                        }
                        break;
                    }
                case "outStock":
                    {
                        using (ContextDataBase context = new ContextDataBase())
                        {
                            if (context != null)
                            {
                                context.goods.Load();
                                if (context.goods.Count() != 0)
                                {
                                    var item = context.goods.Find(id);
                                    if (valueParameter == null)
                                    {
                                        item.outStockAt = new DateTimeOffset();
                                    }
                                    else
                                    {

                                        item.outStockAt = (DateTime)valueParameter;
                                    }
                                }
                            }
                            context.SaveChanges();
                        }
                        break;
                    }
            }
        }

        public void UpdateRange(List<Goods> items)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.goods.Load();
                    context.goodsUnits.Load();
                    context.codeUKTZED.Load();
                    if (context.goods != null)
                    {
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                 UpdateFieldGood(context.goods.Find(item.id), item, context.goodsUnits.Find(item.unit.id), context.codeUKTZED.Find(item.codeUKTZED.id));
                            }
                        }
                        else
                        {
                            throw new Exception("Товар не заповнено");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        private void UpdateFieldGood(Goods goodsUpdate, Goods goods,GoodsUnit unit,CodeUKTZED codeUKTZED)
        {
            goodsUpdate.code = goods.code;
            goodsUpdate.name = goods.name;
            goodsUpdate.price = goods.price;
            goodsUpdate.sales = goods.sales;
            goodsUpdate.status = goods.status;
            goodsUpdate.articule = goods.articule;
            goodsUpdate.count = goods.count;
            goodsUpdate.codeUKTZED = codeUKTZED;
            goodsUpdate.unit = unit;
        }
    }
}
