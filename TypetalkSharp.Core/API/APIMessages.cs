using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APIMessages
    {
        HttpClient _connection;
        string _url;
        /// <summary>
        /// <para>Create Typetalk messages API Wrapper.</para>
        /// </summary>
        /// <param name="connection">connection of API</param>
        /// <param name="url">url of Message api</param>
        public APIMessages(HttpClient connection, string url) {
            _connection = connection;
            _url = url;
        }
        /// <summary>
        /// returns messages of specified topics as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the messages</returns>
        public async Task<TopicMessagesRoot> Get(int topicId) {
            var defaultParam = new MessageRequest()
            {
                Direction = MessageRequest.MessageRequestDirection.Backward
            };
            return await this.Get(topicId, defaultParam);
        }

        /// <summary>
        /// returns messages of specified topics with request params as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="param">parameter of sort direction, Message count and refernce.</param>
        /// <returns>the filtered messages</returns>
        public async Task<TopicMessagesRoot> Get(int topicId, MessageRequest param) {
            var encoded = param.ToEncodedParameter();
            var url = string.Format("{0}/{1}?{2}", _url, topicId,encoded);
            return await _connection.GetAsync(url)
                              .ContinueWith(async r =>
                              {
                                  return await JsonUtil.ParseJson<TopicMessagesRoot>(r);
                              }).Result;
        }

        public async Task<TopicMessageRoot> Details(int topicId, int postId) {
            var url = string.Format("{0}/{1}/posts/{2}", _url, topicId, postId);
            return await _connection.GetAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<TopicMessageRoot>(r);
                                    }).Result;
        }
        /// <summary>
        /// returns result of post Message as async
        /// </summary>
        /// <param name="topicId">tpoic id</param>
        /// <param name="param">new messages detail</param>
        /// <returns>posted messages detail</returns>
        public async Task<MessageRoot> Post(int topicId, NewMessge param) {
            var url = string.Format("{0}/{1}", _url, topicId);
            return await _connection.PostAsync(url, param.ToFormUrlEncodedContent())
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<MessageRoot>(r);
                                    }).Result;
        }
        /// <summary>
        /// returns result of delete Message as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">messages id</param>
        /// <returns>deleted messages detail</returns>
        public async Task<Post> Delete(int topicId, int postId) {
            var url = string.Format("{0}/{1}/posts/{2}", _url, topicId, postId);
            return await _connection.DeleteAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<Post>(r);
                                    }).Result;
        }
        /// <summary>
        /// returns result of like Message as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">messages id</param>
        /// <returns>liked messages detail</returns>
        public async Task<Like> Like(int topicId, int postId) {
            var url = string.Format("{0}/{1}/posts/{2}/like", _url, topicId, postId);
            return await _connection.PostAsync(url, null)
                                    .ContinueWith(async r =>
                                    {
                                        var elem = await JsonUtil.ParseJson<LikeRoot>(r);
                                        return elem.Like;
                                    }).Result;
        }
        /// <summary>
        /// returns result of unlike Message as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">post id</param>
        /// <returns>unliked messages detail</returns>
        public async Task<Like> UnLike(int topicId, int postId) {
            var url = string.Format("{0}/{1}/posts/{2}/like", _url, topicId, postId);
            return await _connection.DeleteAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        var elem = await JsonUtil.ParseJson<LikeRoot>(r);
                                        return elem.Like;
                                    }).Result;
        }
    }
}
