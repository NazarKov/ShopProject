using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Service.Modules.Setting.Interface
{
    public interface ISettingService
    {
        public TSetting GetSetting<TSetting>();
        public void SetSetting<TSetting>(TSetting value);
    }
}
