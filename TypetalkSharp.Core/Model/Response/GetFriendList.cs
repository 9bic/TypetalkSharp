using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class FriendsRoot
    {
        [JsonProperty(PropertyName = "accounts")]
        public IEnumerable<Account> Accounts { get; set; }
    }
}
