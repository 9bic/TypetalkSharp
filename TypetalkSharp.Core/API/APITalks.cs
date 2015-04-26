using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APITalks
    {
        HttpClient _connection;
        string _url;

        /// <summary>
        /// create Typetalk talks API wrapper
        /// </summary>
        /// <param name="connection">connection of API</param>
        /// <param name="url">topics url</param>
        public APITalks(HttpClient connection, string url) {
            _connection = connection;
            _url = url;
        }
        /// <summary>
        /// returns result of all talks list as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the list of talks</returns>
        public async Task<IEnumerable<Talk>> All(int topicId) {
            var url = string.Format("{0}/{1}/talks", _url, topicId);
            return await _connection.GetAsync(url)
                             .ContinueWith(async r =>
                             {
                                 var root = await JsonUtil.ParseJson<TalksRoot>(r);
                                 return root.Talks;
                             }).Result;
        }

        /// <summary>
        /// returns result of talk messages as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="talkId">talk id</param>
        /// <returns>the list of messages specified talk id</returns>
        public async Task<TalksMessagesRoot> TalkMessages(int topicId, int talkId) {
            var defaultParam = new MessageRequest()
            {
                Direction = MessageRequest.MessageRequestDirection.Backward
            };
            return await this.TalkMessages(topicId, talkId, defaultParam);
        }

        /// <summary>
        /// returns result of talk messages with request params as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="talkId">talk id</param>
        /// <param name="param">parameter of sort direction, Message count and refernce.</param>
        /// <returns>the filtered messages</returns>
        public async Task<TalksMessagesRoot> TalkMessages(int topicId, int talkId, MessageRequest param) {
            var url = string.Format("{0}/{1}/talks/{2}/posts", _url, topicId, talkId);
            return await _connection.GetAsync(url)
                        .ContinueWith(async r =>
                        {
                            return await JsonUtil.ParseJson<TalksMessagesRoot>(r);
                        }).Result;
        }
    }
}
