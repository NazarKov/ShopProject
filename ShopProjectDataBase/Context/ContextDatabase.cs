using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Entities;
using System.Reflection.Emit;

namespace ShopProjectDataBase.Context
{
    public class ContextDataBase : DbContext
    { 
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OperationEntity> Operations { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductUnitEntity> ProductUnits { get; set; }
        public DbSet<ProductCodeUKTZEDEntity> ProductCodeUKTZED { get; set; }
        public DbSet<DiscountEntity> Discounts { get; set; }
        public DbSet<WorkingShiftEntity> WorkingShift { get; set; }


        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<GiftCertificatesEntity> GiftCertificates { get; set; }
        public DbSet<TokenEntity> UserTokens { get; set; }

        public DbSet<ObjectOwnerEntity> ObjectOwners { get; set; }
        public DbSet<OperationsRecorderEntity> OperationsRecorders { get; set; }
        public DbSet<OperationsRecorderUserEntity> OperationsRecorderUsers { get; set; }
        public DbSet<ElectronicSignatureKey> ElectronicSignatureKeys { get; set; }
        public DbSet<MediaAccessControlEntity> MediaAccessControls { get; set; }
        public ContextDataBase() { }

        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OperationEntity>()
                .HasOne(o => o.MAC)
                .WithOne(m => m.Operation)
                .HasForeignKey<OperationEntity>(o => o.MACId);

            modelBuilder.Entity<WorkingShiftEntity>()
                .HasOne(ws => ws.MACCreateAt)
                .WithMany()  
                .HasForeignKey(ws => ws.MACIdCreateAt)
                .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<WorkingShiftEntity>()
                .HasOne(ws => ws.MACEndAt)
                .WithMany()  
                .HasForeignKey(ws => ws.MACIdEndAt)
                .OnDelete(DeleteBehavior.Restrict);
        } 
    }
}
