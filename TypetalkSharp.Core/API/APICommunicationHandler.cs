using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using TypetalkSharp.Core;
namespace TypetalkSharp
{
    public class APICommunicationHandler : DelegatingHandler
    {
        APISettings _settings;
        Token _token;

        string _clientId;
        string _clientSecret;

        private APICommunicationHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }
        public APICommunicationHandler(HttpMessageHandler innerHandler, APISettings settings, string clientId, string clientSecret)
            :this(innerHandler) {

            _settings = settings;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public APICommunicationHandler(HttpMessageHandler innerHandler, APISettings settings, string clientId, string clientSecret, string accessToken, string refreshToken) 
         : this (innerHandler)
        {
            _settings = settings;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _token = new Token() { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {
            if(_token == null) {
                _token = await this.Authorize();
            }
            request.Headers.Authorization = this.GetAuthenticationHeader();
            HttpResponseMessage response = null;
            response = await base.SendAsync(request, cancellationToken);

            // expired header, then re-authentication
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                _token = await this.Authorize();
                request.Headers.Authorization = this.GetAuthenticationHeader();
            }

            if(response.IsSuccessStatusCode) {
                return response;
            }
            return response;
        }


        private async Task<Token> Authorize() {
            var hc = new HttpClient();
            FormUrlEncodedContent param;
            if(_token != null &&_token.AccessToken != string.Empty && _token.RefreshToken != string.Empty) {
                param = CreateParameter(_clientId, _clientSecret, _token.RefreshToken);
            }
            else {
                param = CreateParameter(_clientId, _clientSecret);
            }

            return await hc.PostAsync(_settings.AuthUrl, param)
                    .ContinueWith(async r =>
                    {
                        return await JsonUtil.ParseJson<Token>(r);
                    }).Result;
        }
        private FormUrlEncodedContent CreateParameter(string clientId, string secret) {
            return new FormUrlEncodedContent(new Dictionary<string, string>() { 
                {"client_id" , clientId},
                {"client_secret" , secret},
                {"grant_type", "client_credentials"},
                {"scope", _settings.Scope}
            });
        }
        private FormUrlEncodedContent CreateParameter(string clientId, string secret, string refreshToken) {
            return new FormUrlEncodedContent(new Dictionary<string, string>() { 
                {"client_id" , clientId},
                {"client_secret" , secret},
                {"refresh_token", refreshToken},
                {"grant_type", "refresh_token"},
                {"scope", _settings.Scope}
            });
        }
        private System.Net.Http.Headers.AuthenticationHeaderValue GetAuthenticationHeader() {
            return new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.AccessToken);
        }
    }
}
