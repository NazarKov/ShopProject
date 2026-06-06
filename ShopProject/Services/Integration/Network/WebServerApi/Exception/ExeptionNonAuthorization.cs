using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.Exception
{
    internal class ExeptionNonAuthorization : System.Exception
    {
        public ExeptionNonAuthorization(string message) : base(message) { }
        public ExeptionNonAuthorization(string message, string error) : this(message)
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
