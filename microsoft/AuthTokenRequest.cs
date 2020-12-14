using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    internal class AuthTokenRequest
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
        
        [JsonPropertyName("redirect_uri")]
        public string RedirectUri { get; set; }
        
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}