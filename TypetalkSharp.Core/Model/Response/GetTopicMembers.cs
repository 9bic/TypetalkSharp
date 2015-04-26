using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class TopicMembersRoot
    {
        [JsonProperty(PropertyName = "accounts")]
        public IEnumerable<TopicMembers> Accounts { get; set; }
        [JsonProperty(PropertyName="pendings")]
        public IEnumerable<TopicMembers> Pendings { get; set; }
    }

    public class TopicMembers
    {
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
        [JsonProperty(PropertyName = "online")]
        public bool Online { get; set;}
    }
}
