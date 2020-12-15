using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class Profile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        
        [JsonPropertyName("createdAt")]
        public long CreatedAt { get; set; }
        
        [JsonPropertyName("legacyProfile")]
        public bool LegacyProfile { get; set; }
        
        [JsonPropertyName("suspended")]
        public bool Suspended { get; set; }
        
        [JsonPropertyName("paid")]
        public bool Paid { get; set; }
        
        [JsonPropertyName("migrated")]
        public bool Migrated { get; set; }

        [JsonPropertyName("legacy")]
        public bool Legacy { get; set; }
    }
}
