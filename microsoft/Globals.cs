using System.Text.Json;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    internal class Globals
    {
        public static JsonSerializerOptions JsonSerializerOptions
        {
            get => new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
#if NET5_0
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
#endif
                ReadCommentHandling = JsonCommentHandling.Skip
            };
        }
    }
}