using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Setting.Interface
{
    internal interface ISettingService
    {
        public TSetting GetSetting<TSetting>();
        public void SetSetting<TSetting>(TSetting value);
    }
}
