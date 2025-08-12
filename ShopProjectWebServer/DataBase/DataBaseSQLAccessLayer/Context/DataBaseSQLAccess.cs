using ShopProjectSQLDataBase.Context;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DataBaseSQLAccess : IDataAccess 
    {
        public IOrderTableAccess  OrderTable { get; set; }
        public IOperationTableAccess OperationTable { get; set; }
        public IOperationRecorderTableAccess OperationRecorderTable { get; set; }
        public IOperationRecorederUserTableAccess OperationRecorederUserTable { get; set; }
        public IProductTableAccess  ProductTable { get; set; }
        public IProductUnitTableAccess  ProductUnitTable { get; set; }
        public IProductCodeUKTZEDTableAccess  ProductCodeUKTZEDTable { get; set; }
        public IDiscountTableAccess  DiscountTable { get; set; }
        public IUserTableAccess  UserTable { get; set; }

        public IUserRoleTableAccess  UserRoleTable { get; set; }
        public IObjectOwnerTableAccess  ObjectOwnerTable { get; set; }
        public IGiftCertificatesTableAccess  GiftCertificatesTable { get; set; }
        public ITokenTableAccess  TokenTable { get; set;}


        public DataBaseSQLAccess(string ConnectionString)
        {
            UserTable = new UserTableAccess(ConnectionString);
            UserRoleTable = new UserRoleTableAccess(ConnectionString);
            TokenTable = new TokenTableAccess(ConnectionString);
            ProductTable = new ProductTableAccess(ConnectionString);
            ProductUnitTable = new ProductUnitTableAccess(ConnectionString);
            ProductCodeUKTZEDTable = new ProductCodeUKTZEDTableAccess(ConnectionString);
            ObjectOwnerTable = new ObjectOwnerTableAccess(ConnectionString);
            OperationRecorderTable = new OperationRecorderTableAccess(ConnectionString);
            OperationRecorederUserTable = new OperationRecorderUserTableAccess(ConnectionString);
            OperationTable = new OperationTableAccess(ConnectionString);
            OrderTable = new OrderTableAccess(ConnectionString);
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
