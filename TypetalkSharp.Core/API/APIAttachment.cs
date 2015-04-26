using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TypetalkSharp.Core;

namespace TypetalkSharp
{
    public class APIAttachments
    {
        HttpClient _connection;
        string _url;

        /// <summary>
        /// create Typetalk attachment API Wrapper
        /// </summary>
        /// <param name="connection">connection of API</param>
        /// <param name="url">topics url</param>
        public APIAttachments(HttpClient connection, string url) {
            _connection = connection;
            _url = url;
        }
        /// <summary>
        /// returns result of upload file
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="filename">upload file name</param>
        /// <param name="bytes">upload file binary</param>
        /// <returns>the upload file key</returns>
        public async Task<AttachmentEntry> Upload(int topicId, string filename, byte[] bytes) {
            var url = string.Format("{0}/{1}/attachments", _url, topicId);
            var multiPart = new MultipartFormDataContent();
            var binaryContent = new ByteArrayContent(bytes);

            // caution: parameter must surround with ""
            multiPart.Add(binaryContent, "\"file\"", string.Format("\"{0}\"", filename));

            return await _connection.PostAsync(url, multiPart).ContinueWith(async r =>
            {
                return await JsonUtil.ParseJson<AttachmentEntry>(r);
            }).Result;

        }
    }
}
