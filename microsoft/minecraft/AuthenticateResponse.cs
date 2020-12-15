using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.minecraft
{
    public class Authenticate : AuthenticationElement
    {

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("username")]
        public string Username { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("roles")]
        public string[] Roles { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}