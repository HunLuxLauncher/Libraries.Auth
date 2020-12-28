using System;
using System.Net;
using System.Text.Json.Serialization;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class ErrorResult
    {
        
        /*[JsonPropertyName("statusCode")] 
        public HttpStatusCode StatusCode { get; set; }*/
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonPropertyName("cause")]
        public string Cause { get; set; }

        /*public ErrorResult(HttpStatusCode statusCode, string error, string errorMessage, string cause)
        {
            StatusCode = statusCode;
            Error = error;
            ErrorMessage = errorMessage;
            Cause = cause;
        }*/
    }
}
