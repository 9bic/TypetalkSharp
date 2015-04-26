using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp.Core
{
    internal class APIException : Exception
    {
        System.Net.HttpStatusCode StatusCode { get; set; }

        internal APIException(System.Net.HttpStatusCode code, string message) : base(message) { }

        internal APIException(System.Net.HttpStatusCode code, string message, Exception innerException) : base(message, innerException) { }
    }
}
