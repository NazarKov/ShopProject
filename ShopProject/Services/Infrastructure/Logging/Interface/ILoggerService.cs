using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Logging.Interface
{
    internal interface ILoggerService  
    {
        public void WriteLog(string message);
    }
}
