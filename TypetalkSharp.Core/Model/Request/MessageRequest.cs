using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class MessageRequest : IGetParameter
    {
        public enum MessageRequestDirection
        {
            Forward,
            Backward
        }

        [JsonProperty(PropertyName="count")]
        public int? Count { get; set; }
        
        [JsonProperty(PropertyName="from")]
        public int? From { get; set; }

        [JsonProperty(PropertyName="converter")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public MessageRequestDirection? Direction { get; set; }

        public string ToEncodedParameter() {
            var dict = new Dictionary<string, string>();

            if(this.Count != null) {
                dict.Add("count", this.Count.ToString());
            }
            if(this.From != null) {
                dict.Add("from", this.From.ToString());
            }
            if(this.Direction != null) {
                dict.Add("direction", this.Direction.ToString());
            }
            var param = new FormUrlEncodedContent(dict);
            return param.ReadAsStringAsync().Result;
        }
    }
}
