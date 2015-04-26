using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APITopics
    {
        HttpClient _connection;
        string _url;

        public APIMessages Messages { get; private set; }
        public APITalks Talks { get; private set; }
        public APIAttachments Attachments { get; private set; }

        /// <summary>
        /// create Typetalk topics API wrappper
        /// </summary>
        /// <param name="connection">API connections</param>
        /// <param name="topicsUrl">url of topics API</param>
        public APITopics(HttpClient connection, string topicsUrl) {
            _connection = connection;
            _url = topicsUrl;

            this.Messages = new APIMessages(_connection, topicsUrl);
            this.Talks = new APITalks(_connection, topicsUrl);
            this.Attachments = new APIAttachments(_connection, topicsUrl);
        }

        /// <summary>
        /// returns result of all topics async
        /// </summary>
        /// <returns>the topics</returns>
        public async Task<IEnumerable<Topics>> All() {
           return await _connection.GetAsync(_url)
                             .ContinueWith(async r => 
                             {
                                 var root = await JsonUtil.ParseJson<TopicsRoot>(r);
                                 return root.Topics;
                             }).Result;
        }

        /// <summary>
        /// returns result of specified topic as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the topic</returns>
        public async Task<TopicRoot> Single(int topicId) {
            var url = string.Format("{0}/{1}", _url, topicId);
            return await _connection.GetAsync(url)
                              .ContinueWith(async r =>
                              {
                                  return await JsonUtil.ParseJson<TopicRoot>(r);
                              }).Result;
        }

        /// <summary>
        /// returns result of topic's detail async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the topic</returns>
        public async Task<TopicDetails> Detail(int topicId) {
            var url = string.Format("{0}/{1}/details", _url, topicId);
            return await _connection.GetAsync(url)
                  .ContinueWith(async r =>
                  {
                      return await JsonUtil.ParseJson<TopicDetails>(r);
                  }).Result;
        }

        /// <summary>
        /// returns result of topic memberes list as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>topic members</returns>
        public async Task<TopicMembersRoot> Members(int topicId) {
            var url = string.Format("{0}/{1}/members/status", _url, topicId);
            return await _connection.GetAsync(url).ContinueWith(async r =>
            {
                return await JsonUtil.ParseJson<TopicMembersRoot>(r);
            }).Result;
        }

        /// <summary>
        /// returns result of favorite topic as async
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="postId">post id</param>
        /// <returns>the favorite result</returns>
        public async Task<FavoriteRoot> Favorite(int topicId, int postId) {
            var url = string.Format("{0}/{1}/favorite", _url, topicId);
            return await _connection.PostAsync(url, null)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<FavoriteRoot>(r);
                                    }).Result;
        }
        /// <summary>
        ///  returns result of unfavorite topic as async
        /// </summary>
        /// <param name="topicId">tpoic id</param>
        /// <param name="postId">post id</param>
        /// <returns>the unfavorite result</returns>
        public async Task<FavoriteRoot> Unfavorite(int topicId, int postId) {
            var url = string.Format("{0}/{1}/favorite", _url, topicId);
            return await _connection.DeleteAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<FavoriteRoot>(r);
                                    }).Result;
        }
        /// <summary>
        /// returns result of create topic
        /// </summary>
        /// <param name="name">topic name</param>
        /// <returns>the topic details</returns>
        public async Task<TopicDetails> Create(string name) {
            var param = new NewTopic(name);
            return await this.Create(param);
        }
        /// <summary>
        /// returns result of create topic
        /// </summary>
        /// <param name="name">topic name and more params</param>
        /// <returns>the created topic details</returns>
        public async Task<TopicDetails> Create(NewTopic param) {
            return await _connection.PostAsync(_url, param.ToFormUrlEncodedContent())
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<TopicDetails>(r);
                                    }).Result;
        }

        /// <summary>
        /// returns result of update name
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="param">name and more params</param>
        /// <returns>the updated topic details</returns>
        public async Task<TopicDetails> Update(int topicId, UpdateTopic param) {
            var url = string.Format("{0}/{1}", _url, topicId);
            return await _connection.PutAsync(url, param.ToFormUrlEncodedContent())
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<TopicDetails>(r);
                                    }).Result;
        }
        /// <summary>
        /// returns result of delete topic
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <returns>the deleted topic details</returns>
        public async Task<Topic> Delete(int topicId) {
            var url = string.Format("{0}/{1}", _url, topicId);
            return await _connection.DeleteAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<Topic>(r);
                                    }).Result;
        }

        /// <summary>
        /// returns result of accept topic invitation
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public async Task<NotifTopicInvitesRoot> Accept(int topicId, int inviteId) {
            var url = string.Format("{0}/{1}/members/invite/{2}/accept", _url, topicId, inviteId);
            return await _connection.PostAsync(url, null)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<NotifTopicInvitesRoot>(r);
                                    }).Result;
        }

        /// <summary>
        /// returns result of decline topic invitation
        /// </summary>
        /// <param name="topicId"></param>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public async Task<NotifTopicInvitesRoot> Decline(int topicId, int inviteId) {
            var url = string.Format("{0}/{1}/members/invite/{2}/decline", _url, topicId, inviteId);
            return await _connection.PostAsync(url, null)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<NotifTopicInvitesRoot>(r);
                                    }).Result;
        }

        public async Task<TopicDetails> CancelInvitation(int topicId, IEnumerable<int> inviteIds) {
            var url = string.Format("{0}/{1}/members/remove", _url, topicId);

            var array = inviteIds.ToArray();
            var dict = new Dictionary<string, string>();

            for(var i = 0; i < array.Count(); i++) {
                dict.Add(string.Format("removeInviteIds[{0}]", i), array[0].ToString());
            }

            return await _connection.PostAsync(url, new FormUrlEncodedContent(dict))
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<TopicDetails>(r);
                                    }).Result;
        }

        public async Task<TopicDetails> RemoveMembers(int topicId, IEnumerable<int> removeMemberIds) {
            var url = string.Format("{0}/{1}/members/remove", _url, topicId);

            var array = removeMemberIds.ToArray();
            var dict = new Dictionary<string, string>();

            for(var i = 0; i < array.Count(); i++) {
                dict.Add(string.Format("removeMemberIds[{0}]", i), array[0].ToString());
            }

            return await _connection.PostAsync(url, new FormUrlEncodedContent(dict))
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<TopicDetails>(r);
                                    }).Result;
        }
    }
}
