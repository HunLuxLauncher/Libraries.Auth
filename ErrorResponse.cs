using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth
{
    public class ErrorResponse
    {
#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [JsonPropertyName("path")]
        public string Path { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [JsonPropertyName("errorType")]
        public string ErrorType { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [JsonPropertyName("error")]
        public string Error { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [JsonPropertyName("developerMessage")]
        public string DeveloperMessage { get; set; }

    }
}
