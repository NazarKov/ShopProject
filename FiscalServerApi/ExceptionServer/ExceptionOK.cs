using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.ExceptionServer
{
    public class ExceptionOK : Exception
    {
        public ExceptionOK(){ }
        public ExceptionOK(string message) : base(message) { }
        public ExceptionOK(string message, string id) : base(message)
        {
            _id = id;
        }
        private string? _id;
        public string? ID
        {
            get { return _id; }
        }
    }
}
