using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using ShopProjectDataBase.Context;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity; 
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Text.RegularExpressions;

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

        private readonly ContextDataBase _contextDataBase;

        public DataBaseSQLAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
            UserTable = new UserTableAccess(_contextDataBase);
            UserRoleTable = new UserRoleTableAccess(_contextDataBase);
            TokenTable = new TokenTableAccess(_contextDataBase);
            ProductTable = new ProductTableAccess(_contextDataBase);
            ProductUnitTable = new ProductUnitTableAccess(_contextDataBase);
            ProductCodeUKTZEDTable = new ProductCodeUKTZEDTableAccess(_contextDataBase);
            ObjectOwnerTable = new ObjectOwnerTableAccess(_contextDataBase);
            OperationRecorderTable = new OperationRecorderTableAccess(_contextDataBase);
            OperationRecorederUserTable = new OperationRecorderUserTableAccess(_contextDataBase);
            OperationTable = new OperationTableAccess(_contextDataBase);
            OrderTable = new OrderTableAccess(_contextDataBase);
            MediaAccessControlTable = new MediaAccessControlTableAccess(_contextDataBase);
            WorkingShiftTable = new WorkingShiftEntityTableAccess(_contextDataBase);
            DiscountTable = new DiscountTableAccess(_contextDataBase);
            GiftCertificatesTable = new GiftCertificatesTableAccess(_contextDataBase);
            SignatureKeyTable = new SignatureKeyTableAccess(_contextDataBase);
        } 
    }
}
