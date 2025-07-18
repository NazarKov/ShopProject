﻿using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDataAccess 
    {
        IOrderTableAccess OrderTable { get; set; }
        IOperationTableAccess OperationTable { get; set; }
        IOperationRecorderTableAccess OperationRecorderTable { get; set; }
        IOperationRecorederUserTableAccess OperationRecorederUserTable { get; set; }


        IProductTableAccess ProductTable { get; set; }
        IProductUnitTableAccess ProductUnitTable { get; set; }
        IProductCodeUKTZEDTableAccess ProductCodeUKTZEDTable { get; set; }
        IDiscountTableAccess DiscountTable { get; set; }

        IUserTableAccess UserTable  { get; set; }
        IUserRoleTableAccess UserRoleTable { get; set; }
        IObjectOwnerTableAccess ObjectOwnerTable { get; set; }
        IGiftCertificatesTableAccess GiftCertificatesTable { get; set; }
        ITokenTableAccess TokenTable { get; set; }

        public string Create(string connectionString);
        public bool IsCreate(string connectionString);
        public string Сonnection(string connectionString);
        public void Clear();
        public void Delete();
    }
}
