using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TypetalkSharp.Core
{
    public class JsonUtil
    {
        static public async Task<T> ParseJson<T>(Task<HttpResponseMessage> response) {
            var res = response.Result;

            var str = await response.Result.Content.ReadAsStringAsync();
            // TODO: cancel, timeout
            if(response.Result.StatusCode == System.Net.HttpStatusCode.OK) {
                var json = JsonConvert.DeserializeObject<T>(str);
                return json;
            }
            throw new APIException(res.StatusCode, str);
        }

        static public JObject ToJObject(string json) {
            return JObject.Parse(json);
        }
    }
}
