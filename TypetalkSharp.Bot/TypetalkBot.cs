using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TypetalkSharp.Core;

namespace TypetalkSharp.Bot
{
    public class TypetalkBot
    {
        APISettings _settings;
        HttpClient _comm;

        /// <summary>
        /// Returns /api/topics Wrapper
        /// </summary>
        public APITopics Topics { get; private set; }

        /// <summary>
        /// Create client of Typetalk Bot
        /// </summary>
        /// <param name="token"></param>
        public TypetalkBot(string token) {
            _settings = new APISettings();
            _comm = new HttpClient(
                new BotCommunicationHandler(new HttpClientHandler(), token));
            this.CreateClient();
        }

        /// <summary>
        /// Create client instanse of each URL
        /// </summary>
        private void CreateClient() {
            this.Topics = new APITopics(_comm, _settings.TopicUrl);
        }
    }
}
