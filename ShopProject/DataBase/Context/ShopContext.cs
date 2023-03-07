using ShopProject.DataBase.Model;
using System;
using System.Data.Entity;

namespace ShopProject.DataBase.Context
{
    internal class ShopContext : DbContext 
    {
        public ShopContext() : base(Properties.Settings.Default.ConnectionString) { }

        public DbSet<Product>? products { get; set; }
        public DbSet<Order>? orders { get; set; }
        public DbSet<ProductOrder>? productOrders { get; set; }
        public DbSet<ProductArchive>? productArchives { get; set; }
        public DbSet<ProductsOutOfStock>? productsOutOfs { get; set; }

        public DbSet<User>? user { get; set; }
        public DbSet<CashRegister>? cashRegisters { get; set; }
        public DbSet<GiftCertificates>? GiftCertificates { get; set; }
        
    }
}
