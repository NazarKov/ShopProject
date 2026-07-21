using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Common.Enum
{
    internal enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Unauthorized,
        Conflict,
        Server,
        DeleteBarCode,
    }
}
