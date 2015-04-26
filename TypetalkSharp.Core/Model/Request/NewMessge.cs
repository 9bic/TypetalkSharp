using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class NewMessge : IFormParameter
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "replyTo")]
        public int? ReplyTo { get; set; }
        [JsonProperty(PropertyName = "fileKey")]
        public string[] FileKey { get; set; }
        [JsonProperty(PropertyName = "talkId")]
        public int[] TalkId { get; set; }
        
        public NewMessge(string message) {
            this.Message = message;
            FileKey = new string[5];
            TalkId = new int[5];
        }

        public FormUrlEncodedContent ToFormUrlEncodedContent() {
            var dict = new Dictionary<string, string>();
            dict.Add("message", this.Message);

            if(this.ReplyTo != null) {
                dict.Add("replyTo", this.ReplyTo.Value.ToString());
            }
            for(var i = 0; i < FileKey.Count(); i++) {
                dict.Add(string.Format("fileKeys[{0}]", i), FileKey[i]);
            }
            for(var i = 0; i < TalkId.Count(); i++) {
                dict.Add(string.Format("talkIds[{0}]", i), TalkId[i].ToString());
            }
            return new FormUrlEncodedContent(dict);
        }
    }
}
