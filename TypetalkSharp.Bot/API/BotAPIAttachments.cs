using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TypetalkSharp;
namespace TypetalkSharp.Bot
{
    class BotAPIAttachments
    {
        APIAttachments _attachments;

        /// <summary>
        /// create Typetalk attachment API Wrapper
        /// </summary>
        /// <param name="connection">connection of API</param>
        /// <param name="url">topics url</param>
        public BotAPIAttachments(HttpClient client, string url) {
            _attachments = new APIAttachments(client, url);
        }

        /// <summary>
        /// returns result of upload file
        /// </summary>
        /// <param name="topicId">topic id</param>
        /// <param name="filename">upload file name</param>
        /// <param name="bytes">upload file binary</param>
        /// <returns>the upload file key</returns>
        public async Task<AttachmentEntry> Upload(int topicId, string filename, byte[] bytes) {
            return await _attachments.Upload(topicId, filename, bytes);
        }
    }
}
