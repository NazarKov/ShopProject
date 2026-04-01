using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Setting
{
    internal class StorageSetting
    {
        public int ProductBarCodeLength { get; set; }
        public int SertificateBarCodeLength { get; set; } 
    }
}
