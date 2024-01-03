using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.Helpers
{
    internal enum TypeMessage
    {
        None,
        sendChk,
        sendChk2,
        lastChk,
        ping,
        delLastChk,
        delLastChkId,
        statusRro,
        infoRro,
    }
}
