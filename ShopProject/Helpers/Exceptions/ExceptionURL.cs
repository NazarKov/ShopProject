using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.Exceptions
{
    public class ExceptionURL : Exception
    {
        public ExceptionURL( ) : base(string.Empty) { }
        public ExceptionURL(string message) : base(message) { }
    }
}
