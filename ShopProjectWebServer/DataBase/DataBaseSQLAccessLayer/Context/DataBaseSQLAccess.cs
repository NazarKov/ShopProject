using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity; 
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DataBaseSQLAccess : IDataAccess
    {
        public IOrderTableAccess OrderTable { get; set; }
        public IOperationTableAccess OperationTable { get; set; }
        public IOperationRecorderTableAccess OperationRecorderTable { get; set; }
        public IOperationRecorederUserTableAccess OperationRecorederUserTable { get; set; }
        public IProductTableAccess ProductTable { get; set; }
        public IProductUnitTableAccess ProductUnitTable { get; set; }
        public IProductCodeUKTZEDTableAccess ProductCodeUKTZEDTable { get; set; }
        public IDiscountTableAccess DiscountTable { get; set; }
        public IUserTableAccess UserTable { get; set; }

        public IUserRoleTableAccess UserRoleTable { get; set; }
        public IObjectOwnerTableAccess ObjectOwnerTable { get; set; }
        public IGiftCertificatesTableAccess GiftCertificatesTable { get; set; }
        public ITokenTableAccess TokenTable { get; set; }
        public IMediaAccessControlTableAccess MediaAccessControlTable { get; set; }
        public IWorkingShiftTableAccess WorkingShiftTable { get; set; }
        public ISignatureKeyTableAccess SignatureKeyTable { get; set; }

        private readonly DbContextOptionsBuilder<ContextDataBase> optionsBuilder;

        public DataBaseSQLAccess(string ConnectionString)
        {
            optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
            optionsBuilder.UseSqlServer(ConnectionString);

            if (IsCreate())
            {
                UserTable = new UserTableAccess(optionsBuilder.Options);
                UserRoleTable = new UserRoleTableAccess(optionsBuilder.Options);
                TokenTable = new TokenTableAccess(optionsBuilder.Options);
                ProductTable = new ProductTableAccess(optionsBuilder.Options);
                ProductUnitTable = new ProductUnitTableAccess(optionsBuilder.Options);
                ProductCodeUKTZEDTable = new ProductCodeUKTZEDTableAccess(optionsBuilder.Options);
                ObjectOwnerTable = new ObjectOwnerTableAccess(optionsBuilder.Options);
                OperationRecorderTable = new OperationRecorderTableAccess(optionsBuilder.Options);
                OperationRecorederUserTable = new OperationRecorderUserTableAccess(optionsBuilder.Options);
                OperationTable = new OperationTableAccess(optionsBuilder.Options);
                OrderTable = new OrderTableAccess(optionsBuilder.Options);
                MediaAccessControlTable = new MediaAccessControlTableAccess(optionsBuilder.Options);
                WorkingShiftTable = new WorkingShiftEntityTableAccess(optionsBuilder.Options);
                DiscountTable = new DiscountTableAccess();
                GiftCertificatesTable = new GiftCertificatesTableAccess();
                SignatureKeyTable = new SignatureKeyTableAccess(optionsBuilder.Options);
            }
            else
            {
                throw new Exception("База даних не створена");
            }
        }

        public bool IsCreate()
        {
            try
            {
                using (ContextDataBase context = new ContextDataBase(optionsBuilder.Options))
                {

                    if (!context.Database.CanConnect())
                    {
                        context.Database.Migrate();
                        context.Initial();
                    }

                    context.Database.Migrate(); 
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string Сonnection(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
