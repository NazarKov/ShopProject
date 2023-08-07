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
    internal class CodeUKTZEDTableAccess : IEntityAccessor<CodeUKTZED>
    {
        public void Add(CodeUKTZED item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<CodeUKTZED> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(CodeUKTZED item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<CodeUKTZED> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeUKTZED> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.codeUKTZED.Load();
                    if (context.codeUKTZED != null)
                    {
                        if (context.codeUKTZED.Any())
                        {
                            return context.codeUKTZED.ToList();
                        }
                        else
                        {
                            throw new Exception("База даних пуста");
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public CodeUKTZED GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public CodeUKTZED GetItemId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(CodeUKTZED item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<CodeUKTZED> items)
        {
            throw new NotImplementedException();
        }
    }
}
