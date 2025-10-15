using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class SignatureKeyTableAccess : ISignatureKeyTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public SignatureKeyTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }
        public ElectronicSignatureKey GetKeyByUser(string UserId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.ElectronicSignatureKeys.Load(); 

                    if (context.Users.Count() != 0)
                    {
                        var user = context.Users.First(u=>u.ID ==Guid.Parse(UserId));
                        return user.SignatureKey;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }
    }
}
