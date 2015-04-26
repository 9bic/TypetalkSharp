using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class ProfileRoot
    {
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
    }
}
