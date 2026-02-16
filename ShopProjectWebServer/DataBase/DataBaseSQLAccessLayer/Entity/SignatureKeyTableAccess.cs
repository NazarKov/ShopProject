using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class SignatureKeyTableAccess : ISignatureKeyTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public SignatureKeyTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public ElectronicSignatureKey GetKeyByUser(string UserId)
        {
            return _contextDataBase.Users.Include(k => k.SignatureKey).FirstOrDefault(u => u.ID == Guid.Parse(UserId)).SignatureKey;
        }
    }
}
