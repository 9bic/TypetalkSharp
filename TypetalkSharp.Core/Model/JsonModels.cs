using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class Account
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "suggestion")]
        public string Suggestion { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class Member
    {
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
        [JsonProperty(PropertyName = "role")]
        String Role { get; set; }
    }
    public class AttachmentRoot
    {
        [JsonProperty(PropertyName = "attachment")]
        public AttachmentEntry Attachments { get; set; }
        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }
        [JsonProperty(PropertyName = "apiUrl")]
        public string ApiUrl { get; set; }
        [JsonProperty(PropertyName = "thumbnails")]
        public IEnumerable<Thumbnail> Thumbnails { get; set; }
    }

    public class AttachmentEntry
    {
        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }
        [JsonProperty(PropertyName = "fileKey")]
        public string FileKey { get; set; }
        [JsonProperty(PropertyName = "nameContent")]
        public string FileName { get; set; }
        [JsonProperty(PropertyName = "fileSize")]
        public int FileSize { get; set; }
    }
    public class Bookmark
    {
        [JsonProperty(PropertyName = "PostId")]
        public int PostId { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class Embed
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "version")]
        public float Version { get; set; }
        [JsonProperty(PropertyName = "provider_name")]
        public string ProviderName { get; set; }
        [JsonProperty(PropertyName = "provider_url")]
        public string ProviderUrl { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; set; }
        [JsonProperty(PropertyName = "author_url")]
        public string AuthorUrl { get; set; }
        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }
    }
    public enum Role
    {
        [JsonProperty(PropertyName = "admin")]
        Admin,
        [JsonProperty(PropertyName = "member")]
        Member
    }
    public class NotifTeamInvitesRoot
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "team")]
        public Team Team { get; set; }
        [JsonProperty(PropertyName = "sender")]
        public Account Sender { get; set; }
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
        [JsonProperty(PropertyName = "role")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Role Role { get; set; }
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class NotifTopicInvitesRoot
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "sender")]
        public Account Sender { get; set; }
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
    public class Like
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "postId")]
        public int PostId { get; set; }
        [JsonProperty(PropertyName = "topicId")]
        public int TopicId { get; set; }
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
    }
    public class Link
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "embed")]
        public Embed Embed { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class Mention
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "readAt")]
        public DateTime? readAt { get; set; }
        [JsonProperty(PropertyName = "post")]
        public Post Post { get; set; }
    }
    public class Post
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "TopicId")]
        public int TopicId { get; set; }
        [JsonProperty(PropertyName = "ReplyTo")]
        public int? ReplyTo { get; set; }
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
        [JsonProperty(PropertyName = "mention")]
        public Mention Mention { get; set; }
        [JsonProperty(PropertyName = "attachments")]
        public IEnumerable<AttachmentRoot> Attachments { get; set; }
        [JsonProperty(PropertyName = "likes")]
        public IEnumerable<Like> Likes { get; set; }
        [JsonProperty(PropertyName = "talks")]
        public IEnumerable<Talk> Talks { get; set; }
        [JsonProperty(PropertyName = "links")]
        public IEnumerable<Link> Links { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class Talk
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "TopicId")]
        public int TopicId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "suggestion")]
        public string Suggestion { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class Team
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
    public class Thumbnail
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "fileSize")]
        public int FileSize { get; set; }
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }
    }
    public class Topic
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "suggestion")]
        public string Suggestion { get; set; }
        [JsonProperty(PropertyName = "lastPostedAt")]
        public DateTime? LastPostedAt { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class Unread
    {
        [JsonProperty(PropertyName = "TopicId")]
        public int TopicId { get; set; }
        [JsonProperty(PropertyName = "PostId")]
        public int PostId { get; set; }
        [JsonProperty(PropertyName = "Count")]
        public int Count { get; set; }
    }

}
