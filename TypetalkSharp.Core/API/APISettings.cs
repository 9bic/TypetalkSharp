using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp.Core
{
    /// <summary>
    /// OAuth2 scope values
    /// </summary>
    public enum TypetalkScope
    {
        /// <summary>
        /// topic.read
        /// </summary>
        Read = 0,
        /// <summary>
        /// topic.post
        /// </summary>
        Post = 1,
        /// <summary>
        /// topic.write
        /// </summary>
        Write = 2,
        /// <summary>
        /// topic.delete
        /// </summary>
        Delete = 4,
        /// <summary>
        /// my
        /// </summary>
        My = 8
    }

    public enum AttachmentThumbSize
    {
        Small,
        Medium,
        Large
    }

    public class APISettings
    {
        public string AuthUrl { get; private set; }
        public string BaseUrl { get; private set; }
        public string ApiVersion { get; private set; }
        public string Scope { get; private set; }
        public int MaxRetries { get; private set; }
        public int RetryInterval { get; private set; }
        public int Bookmarks { get; private set; }
        public int Mentions { get; private set; }

        public string TopicUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "topics").ToString();
            }
        }

        public string NotificationUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "notifications").ToString();
            }
        }

        public string BookmarkUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "bookmark").ToString();
            }
        }

        public string MentionsUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "mentions").ToString();
            }
        }

        public string TeamsUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "teams").ToString();
            }
        }
        public string ProfileUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "profile").ToString();
            }
        }
        public string SearchUrl {
            get {
                return new Uri(new Uri(this.BaseUrl), "search").ToString();
            }
        }

        public APISettings() {
            this.AuthUrl = "https://typetalk.in/oauth2/access_token";
            this.BaseUrl = "https://typetalk.in/api/v1/";

            this.ApiVersion = "1";
            this.Scope = "topic.read, topic.post, topic.write, topic.delete, my";
            this.MaxRetries = 10;
            this.RetryInterval = 100;
        }

        public APISettings(string baseUrl, string scope) : this() {
            this.BaseUrl = baseUrl;
            this.Scope = scope;
        }
    }
}
