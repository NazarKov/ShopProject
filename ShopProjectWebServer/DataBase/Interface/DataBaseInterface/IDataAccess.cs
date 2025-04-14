using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDataAccess<TCodeUKTZEDTableAccess, TDiscountTableAccess, TGiftCertificatesTableAccess, TObjectOwnerTableAccess, TOperationRecorderTableAccess, TOperationRecorederUserTableAccess, 
        TOperationTableAccess, TOrderTableAccess, TProductTableAccess, TProductUnitTableAccess, TUserRoleTableAccess, TUserTableAccess,TTokenTableAccess>
    {
        IOrderTableAccess<TOrderTableAccess> OrderTable { get; set; }
        IOperationTableAccess<TOperationTableAccess> OperationTable { get; set; }
        IOperationRecorderTableAccess<TOperationRecorderTableAccess> OperationRecorderTable { get; set; }
        IOperationRecorederUserTableAccess<TOperationRecorederUserTableAccess> OperationRecorederUserTable { get; set; }


        IProductTableAccess<TProductTableAccess> ProductTable { get; set; }
        IProductUnitTableAccess<TProductUnitTableAccess> ProductUnitTable { get; set; }
        ICodeUKTZEDTableAccess<TCodeUKTZEDTableAccess> CodeUKTZEDTable { get; set; }
        IDiscountTableAccess<TDiscountTableAccess> DiscountTable { get; set; }

        IUserTableAccess<TUserTableAccess> UserTable  { get; set; }
        IUserRoleTableAccess<TUserRoleTableAccess> UserRoleTable { get; set; }
        IObjectOwnerTableAccess<TObjectOwnerTableAccess> ObjectOwnerTable { get; set; }
        IGiftCertificatesTableAccess<TGiftCertificatesTableAccess> GiftCertificatesTable { get; set; }
        ITokenTableAccess<TTokenTableAccess> TokenTable { get; set; }

        public string Create(string connectionString);
        public bool IsCreate(string connectionString);
        public string Сonnection(string connectionString);
        public void Clear();
        public void Delete();
    }
}
