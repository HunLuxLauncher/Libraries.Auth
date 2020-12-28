using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class Agent
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

    }
}
