using System;
using System.Net;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class InvalidationResult : ErrorResult
    {
        public bool IsSuccessful;

        /*public InvalidationResult(bool isSuccessful) : base(HttpStatusCode.NoContent, null, null, null)
        {
            IsSuccessful = isSuccessful;
        }

        public InvalidationResult(HttpStatusCode statusCode, string error, string errorMessage, string cause) : base(statusCode, error, errorMessage, cause) { }*/
    }
}
