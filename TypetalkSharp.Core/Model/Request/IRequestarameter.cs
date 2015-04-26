using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp
{
    public interface IGetParameter
    {
        string ToEncodedParameter();
    }

    public interface IFormParameter
    {
        FormUrlEncodedContent ToFormUrlEncodedContent();
    }
}
