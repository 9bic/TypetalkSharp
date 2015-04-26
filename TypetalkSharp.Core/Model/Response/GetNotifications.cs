using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class NotificationRoot
    {
        [JsonProperty(PropertyName = "mentions")]
        public IEnumerable<Mention> Mentions { get; set; }
        [JsonProperty(PropertyName = "invites")]
        public InviteNotification Invites { get; set; }
    }

    public class UnreadNotification
    {
        public int Mentions { get; set; }
        public int Accesses { get; set; }
        public int PendingTeams { get; set; }
        public int PendingTopics { get; set; }
    }
    public class InviteNotification
    {
        [JsonProperty(PropertyName = "teams")]
        public IEnumerable<NotifTeamInvitesRoot> Teams { get; set; }
        [JsonProperty(PropertyName = "topics")]
        public IEnumerable<NotifTopicInvitesRoot> Topics { get; set; }
    }
}
