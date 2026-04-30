using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.Integration.Monitoring.WebServer
{
    internal class ControlWebServer
    { 
        public bool IsEnabled { get; set; } 
        public bool IsEnableDataBase { get; set; }
    }
}
