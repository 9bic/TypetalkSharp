using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APITeams
    {
        HttpClient _connection;
        string _url;

        /// <summary>
        /// Create Typetalk teams API Wrapper.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="url"></param>
        public APITeams(HttpClient connection, string url) {
            this._connection = connection;
            _url = url;
        }

        /// <summary>
        /// returns result of all teams
        /// </summary>
        /// <returns>all teams</returns>
        public async Task<IEnumerable<Team>> All() {
            return await _connection.GetAsync(_url)
                              .ContinueWith(async r =>
                              {
                                  return await JsonUtil.ParseJson<IEnumerable<Team>>(r);
                              }).Result;
        }

        /// <summary>
        /// returns result of accept team invitation
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <param name="inviteId">invite id (in notification)</param>
        /// <returns></returns>
        public async Task<NotifTeamInvitesRoot> Accept(int teamId, int inviteId) {
            var url = string.Format("{0}/{1}/members/invite/{2}/accept", _url, teamId, inviteId);
            return await _connection.PostAsync(url,null)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<NotifTeamInvitesRoot>(r);
                                    }).Result;
        }

        /// <summary>
        /// reaturn result of decline team invitation
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <param name="inviteId">invite id (in notification)</param>
        /// <returns></returns>
        public async Task<NotifTeamInvitesRoot> Decline(int teamId, int inviteId) {
            var url = string.Format("{0}/{1}/members/invite/{2}/decline", _url, teamId, inviteId);
            return await _connection.PostAsync(url, null)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<NotifTeamInvitesRoot>(r);
                                    }).Result;
        }
    }
}
