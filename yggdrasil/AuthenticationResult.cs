using hu.hunluxlauncher.libraries.auth;
using System;
using System.Net;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class AuthenticationResult : ErrorResult
    {
        public string AccessToken;
        public string ProfileName;
        public Guid ProfileId;
        public bool IsLegacyProfile;
        public Profile SelectedProfile;

        public AuthenticationResult(string accessToken, string profileName, Guid profileId, bool isLegacyProfile, Profile selectedProfile) : base(HttpStatusCode.OK, null, null, null)
        {
            AccessToken = accessToken;
            ProfileName = profileName;
            ProfileId = profileId;
            IsLegacyProfile = isLegacyProfile;
            SelectedProfile = selectedProfile;
            //SelectedProfile = selectedProfile;
        }

        public AuthenticationResult(HttpStatusCode statusCode, string error, string errorMessage, string cause) : base(statusCode, error, errorMessage, cause) { }
    }
}
