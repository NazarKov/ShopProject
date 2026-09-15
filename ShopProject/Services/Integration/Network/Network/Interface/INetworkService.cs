using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.Network.Interface
{
    public interface INetworkService
    {
        public Task<bool> HasInternetAsync();
    }
}
