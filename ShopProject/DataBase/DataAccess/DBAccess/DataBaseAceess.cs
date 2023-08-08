using Microsoft.EntityFrameworkCore.Infrastructure;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.DataAccess.DBAccess
{
    public class DataBaseAceess : IDataAccess
    {
        public DataBaseAceess() { }

        public void Create()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                }

                if (!context.goodsUnits.Any() || !context.codeUKTZED.Any())
                {
                    var initializer = new ContextDatabaseInitializer();
                    initializer.InitializeDatabase(context);
                }

            }
        }
        public void Delete()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                context.Database.Delete();
            }
        }
        public void Clear()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                context.goodsOperations.RemoveRange(context.goodsOperations);
                context.operations.RemoveRange(context.operations);
                context.goods.RemoveRange(context.goods);

                context.user.RemoveRange(context.user);
                context.cashRegisters.RemoveRange(context.cashRegisters);
                context.GiftCertificates.RemoveRange(context.GiftCertificates);

                context.SaveChanges();
            }
        }

    }
}
