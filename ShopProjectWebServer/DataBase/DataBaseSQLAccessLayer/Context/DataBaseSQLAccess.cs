using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DataBaseSQLAccess : IDataAccess<CodeUKTZEDEntity,DiscountEntity,GiftCertificatesEntity,ObjectOwnerEntity,OperationsRecorderEntity,OperationsRecorderUserEntity,OperationEntity,
        OrderEntity,ProductEntity,ProductUnitEntity,UserRoleEntity,UserEntity,TokenEntity>
    {
        public IOrderTableAccess<OrderEntity> OrderTable { get; set; }
        public IOperationTableAccess<OperationEntity> OperationTable { get; set; }
        public IOperationRecorderTableAccess<OperationsRecorderEntity> OperationRecorderTable { get; set; }
        public IOperationRecorederUserTableAccess<OperationsRecorderUserEntity> OperationRecorederUserTable { get; set; }
        public IProductTableAccess<ProductEntity> ProductTable { get; set; }
        public IProductUnitTableAccess<ProductUnitEntity> ProductUnitTable { get; set; }
        public ICodeUKTZEDTableAccess<CodeUKTZEDEntity> CodeUKTZEDTable { get; set; }
        public IDiscountTableAccess<DiscountEntity> DiscountTable { get; set; }
        public IUserTableAccess<UserEntity> UserTable { get; set; }

        public IUserRoleTableAccess<UserRoleEntity> UserRoleTable { get; set; }
        public IObjectOwnerTableAccess<ObjectOwnerEntity> ObjectOwnerTable { get; set; }
        public IGiftCertificatesTableAccess<GiftCertificatesEntity> GiftCertificatesTable { get; set; }
        public ITokenTableAccess<TokenEntity> TokenTable { get; set;}

        public DataBaseSQLAccess(string ConnectionString)
        {
            UserTable = new UserTableAccess(ConnectionString);
            UserRoleTable = new UserRoleTableAccess(ConnectionString);
            TokenTable = new TokenTableAccess(ConnectionString);
            ProductTable = new ProductTableAccess(ConnectionString);
            ProductUnitTable = new ProductUnitTableAccess(ConnectionString);
            CodeUKTZEDTable = new CodeUKTZEDTableAccess(ConnectionString);
            ObjectOwnerTable = new ObjectOwnerTableAccess(ConnectionString);
            OperationRecorderTable = new OperationRecorderTableAccess(ConnectionString);
            OperationRecorederUserTable = new OperationRecorderUserTableAccess(ConnectionString);
        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public string Create(string connectionString)
        {
            try
            {
                using (ContextDataBase context = new ContextDataBase(connectionString))
                {
                    if (!context.Database.Exists())
                    {
                        context.Database.Create();
                    }
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool IsCreate(string connectionString)
        {
            throw new NotImplementedException();
        }

        public string Сonnection(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
