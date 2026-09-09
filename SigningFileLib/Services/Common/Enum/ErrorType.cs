using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigningFileLib.Services.Common.Enum
{
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Unauthorized,
        Conflict,
        Server,
        DeleteBarCode,
        ErrorBadHashPrev,
        IncorrectHash,
    }
}
