using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.minecraft
{
    public class ProfileInfo
    {
        /*{
  "id" : "986dec87b7ec47ff89ff033fdb95c4b5", // the real uuid of the account, woo
  "name" : "HowDoesAuthWork", // the mc user name of the account
  "skins" : [ 
        {
            "id" : "6a6e65e5-76dd-4c3c-a625-162924514568",
            "state" : "ACTIVE",
            "url" : "http://textures.minecraft.net/texture/1a4af718455d4aab528e7a61f86fa25e6a369d1768dcb13f7df319a713eb810b",
            "variant" : "CLASSIC",
            "alias" : "STEVE"
        } 
    ],
    "capes" : [
    ]
 }
         */
        [JsonPropertyName("id")]
        public Guid Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Username { get; set; }

        [JsonPropertyName("skins")]
        public List<TextureInfo> Skins { get; set; }
        
        [JsonPropertyName("capes")]
        public List<TextureInfo> Capes { get; set; }

    }
}