using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TypetalkSharp;
namespace TypetalkSharp.Bot
{
    class BotAPIMessages
    {
        APIMessages _messages;
        /// <summary>
        /// <para>Create Typetalk messages API Wrapper for Bot</para>
        /// </summary>
        /// <param name="connection">connection of API</param>
        /// <param name="url">topics url</param>
        public BotAPIMessages(HttpClient connection, string url) {
            _messages = new APIMessages(connection, url);
        }

        /// <summary>
        /// returns messages of specified topics as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the messages</returns>
        public async Task<TopicMessagesRoot> Get(int topicId) {
            return await _messages.Get(topicId);
        }

        /// <summary>
        /// returns messages of specified topics with some parameter as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="param">parameter of sort direction, Message count and refernce.</param>
        /// <returns>the filtered messages</returns>
        private async Task<TopicMessagesRoot> Get(int topicId, MessageRequest param) {
            return await _messages.Get(topicId, param);
        }

        /// <summary>
        /// returns messages of details as async
        /// </summary>
        /// <param name="topicId">mesages topic id</param>
        /// <param name="postId">messages post id</param>
        /// <returns>the detail of Message</returns>
        public async Task<TopicMessageRoot> Details(int topicId, int postId) {
            return await _messages.Details(topicId, postId);
        }
        /// <summary>
        /// returns result of post Message as async
        /// </summary>
        /// <param name="topicId">tpoic id</param>
        /// <param name="param">new messages detail</param>
        /// <returns>posted messages detail</returns>
        public async Task<MessageRoot> Post(int topicId, NewMessge param) {
            return await _messages.Post(topicId, param);
        }
        /// <summary>
        /// returns result of delete Message as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">messages id</param>
        /// <returns>deleted messages detail</returns>
        public async Task<Post> Delete(int topicId, int postId) {
            return await _messages.Delete(topicId, postId);
        }
        /// <summary>
        /// returns result of like Message as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">messages id</param>
        /// <returns>liked messages detail</returns>
        public async Task<Like> Like(int topicId, int postId) {
            return await _messages.Like(topicId, postId);
        }
        /// <summary>
        /// returns result of unlike Message as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">post id</param>
        /// <returns>unliked messages detail</returns>
        public async Task<Like> UnLike(int topicId, int postId) {
            return await _messages.UnLike(topicId, postId);
        }
    }
}
