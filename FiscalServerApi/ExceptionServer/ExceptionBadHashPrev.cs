using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.ExceptionServer
{
    public class ExceptionBadHashPrev : Exception
    {
        public ExceptionBadHashPrev() { }
        public ExceptionBadHashPrev(string message) : base(message) { }

        private string? _mac;
        public string? Mac
        {
            get { return _mac; }
            set { _mac = value; }
        }
    }
}
