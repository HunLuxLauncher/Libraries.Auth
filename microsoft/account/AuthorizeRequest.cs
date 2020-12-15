using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.account
{
    public partial class AuthorizeRequest : AuthenticationElement
    {
#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("response_type")]
        public string ResponseType { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("code")]
        public string Code { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("redirect_uri")]
        public string RedirectUri { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}