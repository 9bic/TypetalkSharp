using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    internal class TalksRoot
    {
        [JsonProperty(PropertyName="talks")]
        internal IEnumerable<Talk> Talks { get; set; }
    }

    public class TalksMessagesRoot
    {
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "talk")]
        public Talk Talk { get; set; }
        [JsonProperty(PropertyName = "posts")]
        public IEnumerable<Post> Posts { get; set; }
        [JsonProperty(PropertyName = "hasNext")]
        public bool HasNext { get; set; }
    }
}
