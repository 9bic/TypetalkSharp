using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public enum InvitesStatus
    {
        [JsonProperty(PropertyName = "invite")]
        Invite,
        [JsonProperty(PropertyName = "decline")]
        Decline
    }

    internal class TopicsRoot
    {
        [JsonProperty(PropertyName="topics")]
        public IEnumerable<Topics> Topics { get; set; }
    }

    public class Topics
    {
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "favorite")]
        public bool Favorite { get; set; }
        [JsonProperty(PropertyName = "unread")]
        public Unread Unread { get; set; }
    }

    public class TopicRoot
    {
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "bookmark")]
        public Bookmark Bookmark { get; set; }
        [JsonProperty(PropertyName = "posts")]
        IEnumerable<Post> Posts { get; set; }
        [JsonProperty(PropertyName = "hasNext")]
        bool hasNext { get; set; }
    }

    public class TopicDetails
    {
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "teams")]
        public IEnumerable<TopicTeam> Teams { get; set; }
        [JsonProperty(PropertyName = "accounts")]
        public IEnumerable<Account> Accounts { get; set; }
        [JsonProperty(PropertyName = "invites")]
        public IEnumerable<TopicInvites> Invites { get; set; }
    }

    public class TopicTeam
    {
        [JsonProperty(PropertyName = "team")]
        public Team Team { get; set; }
        [JsonProperty(PropertyName = "members")]
        public IEnumerable<Member> Members { get; set; }
    }

    public class TopicInvites
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
        [JsonProperty(PropertyName = "mailAddress")]
        public string MailAddress { get; set; }
        [JsonProperty(PropertyName = "status")]
        public InvitesStatus Status { get; set; }
    }
}
