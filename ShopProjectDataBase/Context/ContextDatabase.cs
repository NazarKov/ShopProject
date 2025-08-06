using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using System.Data.Entity;

namespace ShopProjectDataBase.DataBase.Context
{
    public class ContextDataBase : DbContext 
    {
    
        public DbSet<ProductEntity>? Products { get; set; }
        public DbSet<OperationEntity>? Operations { get; set; }
        public DbSet<OrderEntity>? Orders { get; set; }
        public DbSet<ProductUnitEntity>? ProductUnits { get; set; }
        public DbSet<ProductCodeUKTZEDEntity>? ProductCodeUKTZED { get; set; }
        public DbSet<DiscountEntity>? Discounts { get; set; }


        public DbSet<UserEntity>? Users { get; set; }
        public DbSet<UserRoleEntity>? UserRoles { get; set; }
        public DbSet<GiftCertificatesEntity>? GiftCertificates { get; set; }
        public DbSet<TokenEntity> UserTokens { get; set; }

        public DbSet<ObjectOwnerEntity>? ObjectOwners { get; set; }
        public DbSet<OperationsRecorderEntity>? OperationsRecorders { get; set; }
        public DbSet<OperationsRecorderUserEntity>? OperationsRecorderUsers { get; set; }
        public DbSet<ElectronicSignatureKey> ElectronicSignatureKeys { get; set; }

        public ContextDataBase(string connectionString)
        {
            if (connectionString != null && connectionString != string.Empty)
            {
                this.Database.Connection.ConnectionString = connectionString;
                var initializer = new ContextDatabaseInitializer();
                initializer.InitializeDatabase(this);
            }
            else
            {
                throw new Exception("Не вказані налаштування для створення бази даних");
            }

        }

    }
}
