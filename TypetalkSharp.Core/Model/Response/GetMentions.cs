using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class MentionsRoot
    {
        [JsonProperty(PropertyName = "mentions")]
        public IEnumerable<Mention> Mentions { get; set; }
    }
}
