using ShopProject.DataBase.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ShopProject.DataBase.Context
{
    public class ContextDataBase : DbContext 
    {
    
        public DbSet<Goods>? goods { get; set; }
        internal DbSet<Operation>? operations { get; set; }
        internal DbSet<GoodsOperation>? goodsOperations { get; set; }
        public DbSet<GoodsUnit>? goodsUnits { get; set; }
        public DbSet<CodeUKTZED>? codeUKTZED { get; set; }
        
        public DbSet<User>? user { get; set; }
        internal DbSet<CashRegister>? cashRegisters { get; set; }
        internal DbSet<GiftCertificates>? GiftCertificates { get; set; }

        internal DbSet<OrderXML>? orderXML { get; set;}



        public ContextDataBase() : base(Properties.Settings.Default.ConnectionString)
        {
            var initializer = new ContextDatabaseInitializer();
            initializer.InitializeDatabase(this);
        }
    }
}
