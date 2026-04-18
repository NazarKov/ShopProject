using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Services.Infrastructure.Logging.Interface
{
    public interface ILoggerService  
    {
        public void WriteLog(string message);
    }
}
