using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Main.Interface
{
    internal interface IMainAppServise
    {
        public Task<bool> IsConnectServer();
        public Task LoadResourse();
    }
}
