using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class SignoutRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
