using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface ISignatureKeyTableAccess
    {
        ElectronicSignatureKey GetKeyByUser(string UserId);
    }
}
