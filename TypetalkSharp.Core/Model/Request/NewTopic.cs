using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp
{
    public class NewTopic : IFormParameter
    {
        public string Name { get; set; }
        public int? TeamId { get; set; }
        /// <summary>
        /// InviteNotification member's names or mail addreses
        /// </summary>
        public IEnumerable<string> InviteMembers { get; set; }
        public string InviteMessages { get; set; }

        public NewTopic(string topicName) {
            this.Name = topicName;
            this.InviteMembers = new List<string>();
        }

        /// <summary>
        /// returns FormUrlEncodedContent
        /// </summary>
        /// <returns></returns>
        public FormUrlEncodedContent ToFormUrlEncodedContent() {
            var dict = new Dictionary<string, string>();
            if(this.Name != null) {
                dict.Add("name", this.Name);
            }
            if(this.InviteMembers.Count() > 0) {
                var array = this.InviteMembers.ToArray();
                for(var i = 0; i < array.Count(); i++) {
                    dict.Add(string.Format("inviteMembers[{0}]", i), array[i]);
                }
            }
            if(this.InviteMessages != null) {
                dict.Add("inviteMessage", this.InviteMessages);
            }
            if(this.TeamId != null) {
                dict.Add("teamId", this.TeamId.Value.ToString());
            }
            return new FormUrlEncodedContent(dict);
        }
    }
}
