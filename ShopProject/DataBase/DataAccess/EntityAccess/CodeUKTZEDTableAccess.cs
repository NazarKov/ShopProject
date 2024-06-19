using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class CodeUKTZEDTableAccess : IEntityAccess<CodeUKTZEDEntiti>
    {
        public void Add(CodeUKTZEDEntiti item)
        {
            throw new NotImplementedException();
        }
        public void Update(CodeUKTZEDEntiti item)
        {
            throw new NotImplementedException();
        }

        public void Delete(CodeUKTZEDEntiti item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeUKTZEDEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.CodeUKTZED.Load();
                    if (context.CodeUKTZED != null)
                    {
                        if (context.CodeUKTZED.Any())
                        {
                            return context.CodeUKTZED.ToList();
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
    }
}
