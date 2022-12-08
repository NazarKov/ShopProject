using ShopProject.DataBase.Model;
using System.Data.Entity;

namespace ShopProject.DataBase.Context
{
    internal class ShopContext : DbContext
    {
        public ShopContext() : base("DBConnectionMyString") { }

        public DbSet<Product>? products { get; set; }
        public DbSet<Order>? orders { get; set; }
        public DbSet<ProductOrder>? productOrders { get; set; }
        public DbSet<Archive>? archives { get; set; }

        public DbSet<User>? user { get; set; }
        public DbSet<CashRegister>? cashRegisters { get; set; }
        public DbSet<GiftCertificates>? GiftCertificates { get; set; }



    }
}
