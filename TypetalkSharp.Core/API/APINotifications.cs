using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APINotifications
    {
        HttpClient _connection;
        string _url;

        public APINotifications(HttpClient connection, string url) {
            _connection = connection;
            _url = url;
        }

        public async Task<NotificationRoot> All() {
            return await _connection.GetAsync(_url)
                             .ContinueWith(async r =>
                             {
                                 return await JsonUtil.ParseJson<NotificationRoot>(r);
                             }).Result;
        }

        public async Task<UnreadNotification> Unreads() {
            var url = string.Format("{0}/status", _url);
            return await _connection.GetAsync(_url)
                             .ContinueWith(async r =>
                             {
                                 var str = await r.Result.Content.ReadAsStringAsync();
                                 var obj = JsonUtil.ToJObject(str);
                                 return new UnreadNotification()
                                 {
                                     Mentions = Convert.ToInt32(obj["mention"]["unread"]),
                                     Accesses = Convert.ToInt32(obj["access"]["unread"]),
                                     PendingTeams = Convert.ToInt32(obj["invite"]["team"]["pending"]),
                                     PendingTopics = Convert.ToInt32(obj["invite"]["topic"]["pending"])
                                 };
                             }).Result;
        }

        public async Task<UpdateNotifications> Read() {
            return await _connection.PutAsync(_url, null)
                             .ContinueWith(async r =>
                             {
                                 var str = await r.Result.Content.ReadAsStringAsync();
                                 var obj = JsonUtil.ToJObject(str);
                                 return new UpdateNotifications()
                                 {
                                     Access = Convert.ToInt32(obj["access"]["unopend"])
                                 };
                             }).Result;
        }
    }
}
