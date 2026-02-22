using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.ExceptionServer
{
    public class ExceptionCheckShiftIsNotOpen :Exception
    {
        public ExceptionCheckShiftIsNotOpen() { }
        public ExceptionCheckShiftIsNotOpen(string message) : base(message) { }
    }
}
