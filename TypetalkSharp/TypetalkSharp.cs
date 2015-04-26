using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

using TypetalkSharp.Core;
namespace TypetalkSharp
{
    public class TypetalkClient
    {
        Token _authResult;
        APISettings _settings;
        HttpClient _comm;
        
        /// <summary>
        /// Topics API Wrapper
        /// </summary>
        public APITopics Topics {get; private set;}

        /// <summary>
        /// Notifications API Wrapper
        /// </summary>
        public APINotifications Notifications { get; private set; }

        /// <summary>
        /// Teams API Wrapper
        /// </summary>
        public APITeams Teams { get; private set; }

        /// <summary>
        /// Profile API Wrapper
        /// </summary>
        public APIProfile Profile { get; private set; }

        /// <summary>
        /// Search API Wrapper
        /// <para>(include friends list: specifications of typetalk)</para>
        /// </summary>
        public APISearch Search { get; private set; }

        /// <summary>
        /// Create a new instance of Typetalk API v1 client
        /// </summary>
        /// <param name="clientId">your application ID</param>
        /// <param name="clientSecret">your application secrets</param>
        public TypetalkClient(string clientId, string clientSecret) {
            _settings = new APISettings();
            _comm = new HttpClient(new APICommunicationHandler(new HttpClientHandler(), _settings, clientId, clientSecret));
            this.CreateClient();
        }

        /// <summary>
        /// Create a new instance of Typetalk API v1 client
        /// with accessToken and refresh token
        /// </summary>
        /// <param name="clientId">your application ID</param>
        /// <param name="clientSecret">your application secrets</param>
        /// <param name="accessToken">authenticated access token</param>
        /// <param name="refreshToken">token for refresh</param>
        public TypetalkClient(string clientId, string clientSecret, string accessToken, string refreshToken) {
            _settings = new APISettings();
            _comm = new HttpClient(new APICommunicationHandler(new HttpClientHandler(), _settings, clientId, clientSecret, accessToken, refreshToken));
            this.CreateClient();
        }

        /// <summary>
        /// Create client instanse of each URL
        /// </summary>
        private void CreateClient() {
            this.Topics = new APITopics(_comm, _settings.TopicUrl);
            this.Notifications = new APINotifications(_comm, _settings.NotificationUrl);
            this.Teams = new APITeams(_comm, _settings.TeamsUrl);
            this.Profile = new APIProfile(_comm, _settings.ProfileUrl);
            this.Search = new APISearch(_comm, _settings.SearchUrl);
        }
    }
}
