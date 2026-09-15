using ShopProject.Services.Integration.Network.Network.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.Network
{
    internal class NetworkService : INetworkService
    {
        public async Task<bool> HasInternetAsync()
        {
            try
            {
                using var ping = new Ping();

                var reply = await ping.SendPingAsync("8.8.8.8", 2000);

                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
