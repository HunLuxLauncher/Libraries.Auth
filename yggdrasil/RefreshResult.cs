using System;
using System.Net;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class RefreshResult : ErrorResult
    {
        public string NewAccessToken;

        /*public RefreshResult(string newAccessToken) : base(HttpStatusCode.OK, null, null, null)
        {
            NewAccessToken = newAccessToken;
        }

        public RefreshResult(HttpStatusCode statusCode, string error, string errorMessage, string cause) : base(statusCode, error, errorMessage, cause) { }*/
    }
}
