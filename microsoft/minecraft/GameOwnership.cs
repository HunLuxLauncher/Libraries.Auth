using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.minecraft
{
    public class GameOwnership
    {
        /*
        {
            "items" : [
                {
                    "name" : "product_minecraft",
                    "signature" : "jwt sig"
                },
                {
                    "name" : "game_minecraft",
                    "signature" : "jwt sig"
                } 
            ],
            "signature" : "jwt sig",
            "keyId" : "1"
         }
         */
        [JsonPropertyName("items")]
        public List<OwnedItem> Items { get; set; }
        
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("keyId")]
        public string KeyId { get; set; }
    }
}