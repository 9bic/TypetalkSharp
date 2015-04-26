using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TypetalkSharp;
namespace TypetalkSharp.Bot
{
    class BotAPITalks
    {
        APITalks _talks;
        /// <summary>
        /// create Typetalk talks API Wrapper for Bot
        /// </summary>
        /// <param name="connection">connection of API</param>
        /// <param name="url">topics url</param>
        public BotAPITalks(HttpClient connection, string url) {
            _talks = new APITalks(connection, url);
        }

        /// <summary>
        /// returns result of all talks list as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the list of talks</returns>
        public async Task<IEnumerable<Talk>> All(int topicId) {
            return await _talks.All(topicId);
        }

        /// <summary>
        /// returns result of talks messages as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="talkId">talk id</param>
        /// <returns>the list of messages specified talk id</returns>
        public async Task<TalksMessagesRoot> TalkMessages(int topicId, int talkId) {
            return await _talks.TalkMessages(topicId, talkId);
        }

                /// <summary>
        /// returns result of talk messages with request params as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="talkId">talk id</param>
        /// <param name="param">parameter of sort direction, Message count and refernce.</param>
        /// <returns>the filtered messages</returns>
        public async Task<TalksMessagesRoot> TalkMessages(int topicId, int talkId, MessageRequest param) {
            return await _talks.TalkMessages(topicId, talkId, param);
        }
    }
}
