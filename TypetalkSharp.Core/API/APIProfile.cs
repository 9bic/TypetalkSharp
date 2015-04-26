using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APIProfile
    {
        HttpClient _connection;
        string _url;

        /// <summary>
        /// Create Typetalk profile API Wrapper.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="url"></param>
        public APIProfile(HttpClient connection, string url) {
            this._connection = connection;
            _url = url;
        }

        /// <summary>
        /// returns my profile
        /// </summary>
        /// <returns></returns>
        public async Task<ProfileRoot> Mine() {
            return await _connection.GetAsync(_url)
                                    .ContinueWith(async r =>
                                    {
                                        return await JsonUtil.ParseJson<ProfileRoot>(r);
                                    }).Result;
        }
    }
}
