using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Model;
using System.Data.Entity;

namespace ShopProject.DataBase.Context
{
    public class ContextDataBase : DbContext 
    {
    
        public DbSet<ProductEntiti>? Products { get; set; }
        public DbSet<OperationEntiti>? Operations { get; set; }
        public DbSet<OrderEntiti>? Orders { get; set; }
        public DbSet<ProductUnitEntiti>? ProductUnits { get; set; }
        public DbSet<CodeUKTZEDEntiti>? CodeUKTZED { get; set; }
        
        public DbSet<UserEntiti>? Users { get; set; }
        public DbSet<UserRoleEntiti>? UserRoles { get; set; }
        public DbSet<GiftCertificates>? GiftCertificates { get; set; }

        public DbSet<ObjectOwnerEntiti>? ObjectOwners { get; set; }
        public DbSet<OperationsRecorderEntiti>? OperationsRecorders { get; set; }
        public DbSet<OperationsRecorderUserEntiti>? OperationsRecorderUsers { get; set; }

        public ContextDataBase() : base(Properties.Settings.Default.ConnectionString)
        {
            var initializer = new ContextDatabaseInitializer();
            initializer.InitializeDatabase(this);
        }
    }
}
