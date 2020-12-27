using hu.czompisoftware.libraries.crypto;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.minecraft
{
    public class OwnedItem
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

    }
}