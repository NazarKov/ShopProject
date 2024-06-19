using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.SigningFileService.Model
{
    public enum TypeCommand
    {
        Ping = 0,
        Initialize = 1,
        IsInitialize = 2,
        SingFile = 3,
        DisconnectUser = 4,
        GetDataKey = 5,
        Finalize = 6,
        Error = 7,
    }
}
