using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APISearch
    {
        HttpClient _connection;
        string _url;

        /// <summary>
        /// Create Typetalk search API Wrapper.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="url"></param>
        public APISearch(HttpClient connection, string url) {
            this._connection = connection;
            _url = url;
        }

        /// <summary>
        /// returns result of search accounts
        /// </summary>
        /// <param name="nameOrEmail">user name or email address</param>
        /// <returns>result of searches</returns>
        public async Task<Account> Accounts(string nameOrEmail) {
            var url = string.Format("{0}/accounts?nameOrEmailAddress={1}", _url, nameOrEmail);
            return await _connection.GetAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<Account>(r);
                                    }).Result;
        }

        /// <summary>
        /// returns result of get friends list
        /// </summary>
        /// <returns>friends list</returns>
        public async Task<FriendsRoot> Friends() {
            var url = string.Format("{0}/friends", _url);
            return await _connection.GetAsync(url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<FriendsRoot>(r);
                                    }).Result;
        }

    }
}
