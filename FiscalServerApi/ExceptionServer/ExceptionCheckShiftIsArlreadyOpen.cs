using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.ExceptionServer
{
    public class ExceptionCheckShiftIsArlreadyOpen :Exception
    {
        public const string ShiftIsAlreadyOpen = "shift is already open";
        public ExceptionCheckShiftIsArlreadyOpen() { }
        public ExceptionCheckShiftIsArlreadyOpen(string message) : base(message) { }
    }
}
