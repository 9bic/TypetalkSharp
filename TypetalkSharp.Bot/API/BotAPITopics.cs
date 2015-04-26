using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp.Bot
{
    class BotAPITopics
    {
        APITopics _topics;

        public BotAPIMessages Message { get; private set; }
        public BotAPITalks Talks { get; private set; }
        public BotAPIAttachments Attachments { get; private set; }

        /// <summary>
        /// create Typetalk topics API wrapper for Bot
        /// </summary>
        /// <param name="connection">API connections</param>
        /// <param name="topicsUrl">url of topics API</param>
        public BotAPITopics(HttpClient connection, string topicsUrl) {
            _topics = new APITopics(connection, topicsUrl);

            this.Message = new BotAPIMessages(connection, topicsUrl);
            this.Talks = new BotAPITalks(connection, topicsUrl);
        }
        /// <summary>
        /// returns result of topic memberes list as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the topic memberes</returns>
        public async Task<TopicMembersRoot> Members(int topicId) {
            return await _topics.Members(topicId);
        }
        /// <summary>
        /// returns result of topic details as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the topic details</returns>
        public async Task<TopicDetails> Details(int topicId) {
            return await _topics.Detail(topicId);
        }

    }
}
