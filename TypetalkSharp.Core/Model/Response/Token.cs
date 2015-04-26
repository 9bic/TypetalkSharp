﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypetalkSharp
{
    public class Token
    {
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName="token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName="expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty(PropertyName="refresh_token")]
        public string RefreshToken { get; set; }
    }
}
