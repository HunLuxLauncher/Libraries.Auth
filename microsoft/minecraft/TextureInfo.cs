using System;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.minecraft
{
    public class TextureInfo
    {
        /*
        {
            "id" : "6a6e65e5-76dd-4c3c-a625-162924514568",
            "state" : "ACTIVE",
            "url" : "http://textures.minecraft.net/texture/1a4af718455d4aab528e7a61f86fa25e6a369d1768dcb13f7df319a713eb810b",
            "variant" : "CLASSIC",
            "alias" : "STEVE"
        } 
         */

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// <b>WARNING<br/></b>This value might be enum after everyone can migrate their accounts to Microsoft.net account <i>(because this API is written using the <b>wiki.vg</b> article only)</i>.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("Url")]
        public Uri Url { get; set; }

        /// <summary>
        /// <b>WARNING<br/></b>This value might be enum after everyone can migrate their accounts to Microsoft.net account <i>(because this API is written using the <b>wiki.vg</b> article only)</i>.
        /// </summary>
        [JsonPropertyName("variant")]
        public string Variant { get; set; }
        
        /// <summary>
        /// <b>WARNING<br/></b>This value might be enum after everyone can migrate their accounts to Microsoft.net account <i>(because this API is written using the <b>wiki.vg</b> article only)</i>.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

    }
}