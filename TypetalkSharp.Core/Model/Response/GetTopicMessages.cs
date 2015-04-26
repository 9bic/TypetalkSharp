using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{

    public class TopicMessagesRoot
    {
        [JsonProperty(PropertyName = "team")]
        public Team Team { get; set; }
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "bookmark")]
        public Bookmark Bookmark { get; set; }
        [JsonProperty(PropertyName = "posts")]
        public IEnumerable<Post> Posts { get; set; }
        [JsonProperty(PropertyName = "hasNext")]
        public bool HasNext { get; set; }
    }

    public class MessageRoot
    {
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "post")]
        public Post Post { get; set; }
    }

    public class TopicMessageRoot
    {
        [JsonProperty(PropertyName="team")]
        public Team Team { get; set; }
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "post")]
        public Post Post { get; set; }
        [JsonProperty(PropertyName = "replies")]
        public IEnumerable<Post> Replies { get; set; }
    }


}
