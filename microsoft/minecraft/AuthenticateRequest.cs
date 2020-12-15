using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.minecraft
{
    internal class AuthenticateRequest
    {
        [JsonPropertyName("identityToken")]
        public string IdentityToken { get; set; }
    }
}