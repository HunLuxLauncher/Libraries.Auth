using hu.hunluxlauncher.libraries.auth;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class AuthenticationResult : ErrorResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        /*[JsonPropertyName("profileName")]
        public string ProfileName { get; set; }

        [JsonPropertyName("profileId")]
        public Guid ProfileId { get; set; }

        [JsonPropertyName("isLegacyProfile")]
        public bool IsLegacyProfile { get; set; }*/

        [JsonPropertyName("selectedProfile")]
        public Profile SelectedProfile { get; set; }

    }
}
