using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp.Bot
{
    public class BotCommunicationHandler : DelegatingHandler
    {
        string _typetalkToken;

        /// <summary>
        /// create instanse of request with typetalk token.
        /// </summary>
        /// <param name="typetalkToken"></param>
        public BotCommunicationHandler(HttpMessageHandler innerHandler, string typetalkToken) :base(innerHandler) {
            _typetalkToken = typetalkToken;
        }

        /// <summary>
        /// Send http request with typetalk Token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {
            request.Headers.Add("X-Typetalk-Token", _typetalkToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
