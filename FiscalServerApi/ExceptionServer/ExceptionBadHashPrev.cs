﻿using System;
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
        public ExceptionBadHashPrev(string message, string error) : this(message)
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
