using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.ExceptionServer
{
    public class ExceptionCheck : Exception
    {
        public const string ShiftIsAlreadyOpen = "shift is already open";
        public const string ThisKeyOpensAShiftOnAnotherDeviceFn = "this key opens a shift on another device fn";
        public const string ThereCanBeOnlyOneSignatoryWithinAShift = "there can be only one signatory within a shift";
        public const string ThereCanBeOnlyOneSignatoryWithinAShiftClosingCanBeASenior = "there can be only one signatory within a shift, shift closing can be a senior";
        public const string PermittedToUseOnlyAfter = "Permitted to use only after 2021-10-01";


        public ExceptionCheck() { }
        public ExceptionCheck(string message) : base(message) { }
    }
    
}
