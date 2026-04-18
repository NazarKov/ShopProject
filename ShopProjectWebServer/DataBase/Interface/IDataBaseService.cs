using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.Models.Domain.Setting;

namespace ShopProjectWebServer.DataBase.Interface
{
    public interface IDataBaseService
    {
        public IDataAccess DataBaseAccess {  get; set; }
        public SettingDataBaseConnection GetSetting();
    }
}
