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
        public DbSet<Operation>? operations { get; set; }
        public DbSet<GoodsOperation>? goodsOperations { get; set; }
        public DbSet<GoodsUnit>? goodsUnits { get; set; }
        public DbSet<CodeUKTZED>? codeUKTZED { get; set; }
        
        public DbSet<User>? user { get; set; }
        public DbSet<CashRegister>? cashRegisters { get; set; }
        public DbSet<GiftCertificates>? GiftCertificates { get; set; }


        public ContextDataBase() : base(Properties.Settings.Default.ConnectionString)
        {
            var initializer = new ContextDatabaseInitializer();
            initializer.InitializeDatabase(this);
        }
    }
}
