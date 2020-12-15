using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.xbox
{
    public class XuiClaims
    {
        [JsonPropertyName("agg")]
        public string AgeGroup { get; set; }

        [JsonPropertyName("gtg")]
        public string Gamertag { get; set; }

        [JsonPropertyName("prv")]
        public string Privileges { get; set; }

        [JsonPropertyName("xid")]
        public string Xuid { get; set; }

        [JsonPropertyName("uhs")]
        public string UserHash { get; set; }

    }
}
