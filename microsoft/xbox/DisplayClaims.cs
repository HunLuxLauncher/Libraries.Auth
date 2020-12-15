using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.microsoft.xbox
{
    public class DisplayClaims
    {
        [JsonPropertyName("xui")]
        public XuiClaims[] XuiClaims { get; set; }

    }
}
