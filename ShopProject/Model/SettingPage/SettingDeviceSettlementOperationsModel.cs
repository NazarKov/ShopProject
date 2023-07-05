using ShopProject.Helpers.DFSAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.SettingPage
{
    internal class SettingDeviceSettlementOperationsModel
    {
        DFSAPI _api;

        public SettingDeviceSettlementOperationsModel()
        {
            _api = new DFSAPI();
        }

        public void ChekConnection()
        {
            _api.ping();
        }
        
    }
}
