using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise
{
    public class NetworkURL
    {
        /// <summary>
        /// Base IpRouter = 192.168.0.
        /// </summary>
        public string IpRouter { get; set; } = "192.168.0.";
        /// <summary>
        /// Base minIPAddress = 1
        /// </summary>
        public int MinIPAddress { get; set; } = 1;
        /// <summary>
        /// Base minIPAddress = 255
        /// </summary>
        public int MaxIPAddress { get; set; } = 255;
        /// <summary>
        /// Base Port = 5000
        /// </summary>
        public int Port { get; set; } = 5000;
        /// <summary>
        /// URL Database
        /// </summary>
        public string Url { get; set; } = string.Empty;


        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static NetworkURL Deserialize(string json)
        { 
            return JsonSerializer.Deserialize<NetworkURL>(json);
        }
    }
}
