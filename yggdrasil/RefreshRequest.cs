using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class RefreshRequest
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("client_token")]
        public string ClientToken { get; set; }
    }
}
