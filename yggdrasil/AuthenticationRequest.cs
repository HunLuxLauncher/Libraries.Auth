using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class AuthenticationRequest
    {
        [JsonPropertyName("agent")]
        public Agent Agent { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("clientToken")]
        public string ClientToken { get; set; }

        [JsonPropertyName("requestUser")]
        public bool RequestUser { get; set; }
    }
}