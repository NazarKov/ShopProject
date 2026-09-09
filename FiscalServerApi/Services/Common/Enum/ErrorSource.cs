using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.Services.Common.Enum
{
    public enum ErrorSource
    {
        None,
        Client,
        Api,
        Service,
        Database
    }
}
