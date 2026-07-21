using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Setting
{
    public class OperationRecorderSetting
    {
        public bool IsTestMode { get; set; } = true; 
        public string DeleteBarCode { get; set; } = string.Empty; 
    }
}
