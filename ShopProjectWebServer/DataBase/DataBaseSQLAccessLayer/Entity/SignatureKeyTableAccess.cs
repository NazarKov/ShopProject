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
            _contextDataBase.Users.Load();
            _contextDataBase.ElectronicSignatureKeys.Load();

            if (_contextDataBase.Users.Count() != 0)
            {
                var user = _contextDataBase.Users.First(u => u.ID == Guid.Parse(UserId));
                return user.SignatureKey;
            }
            else
            {
                return null;
            }
        }
    }
}
