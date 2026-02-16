using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectWebServer.DataBase.DataBaseException
{
    public class ExceptionObjectExists : Exception
    {
        public ExceptionObjectExists(string message) : base(message) { }
        public ExceptionObjectExists(string message, string error) : this(message)
        {
            _error = error;
        }

        private string? _error;
        public string? Error
        {
            get { return _error; }
        }
    }
}
