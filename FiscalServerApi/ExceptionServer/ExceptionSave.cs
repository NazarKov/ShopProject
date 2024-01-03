using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalServerApi.ExceptionServer
{
    public class ExceptionSave : Exception
    {
        public const string IncorrectHash = "incorrect hash";

        public ExceptionSave() { }
        public ExceptionSave(string message): base(message) { }
        public ExceptionSave(string message,string error) : base(message)
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
