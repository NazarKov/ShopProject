using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Common
{
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Unauthorized,
        Conflict,
        Server,
        ObjectExists
    }
}
