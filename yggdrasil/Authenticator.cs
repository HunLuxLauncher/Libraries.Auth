using System;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using hu.hunluxlauncher.libraries.launcher;
using Newtonsoft.Json;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class Authenticator
    {
        #region Declarations
        private HttpClient _Client;
        private Guid _ClientToken;
        readonly String AUTH_SERVER = "https://authserver.mojang.com";
        #endregion

        #region Global functions
        public Authenticator(Guid clientToken)
        {
            _Client = new HttpClient();
            _ClientToken = clientToken;
        }

        public Authenticator() : this(Guid.NewGuid()) { }

        public Guid GetClientToken()
        {
            return _ClientToken;
        }
        #endregion

        #region Authenticate
        public AuthenticationResult Authenticate(string usernameOrEmailAddress, string password, AgentType agentType, int agentVersion)
        {
            AuthenticationResult result = null;

            JObject payload = new JObject();
            JObject agent = new JObject();

            agent["name"] = (agentType == AgentType.Minecraft ? "Minecraft" : "Scrolls");
            agent["version"] = agentVersion;

            payload["agent"] = agent;
            payload["username"] = usernameOrEmailAddress;
            payload["password"] = password;
            payload["clientToken"] = _ClientToken.ToString();
            payload["requestUser"] = true;

            HttpContent content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _Client.PostAsync(AUTH_SERVER + "/authenticate", content).Result;
            JObject response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                Profile selectedProfile = JsonConvert.DeserializeObject<Profile>(response["selectedProfile"].ToString());

                string profileName = (string)selectedProfile.Name;
                Guid profileId = Guid.Parse((string)selectedProfile.Id);
                bool isLegacyProfile = false;

                if (selectedProfile.Legacy)
                {
                    isLegacyProfile = selectedProfile.Legacy;
                }

                string accessToken = response["accessToken"].ToString();

                result = new AuthenticationResult(accessToken, profileName, profileId, isLegacyProfile, selectedProfile);
            }
            else
            {
                string error = (string)response["error"];
                string errorMessage = (string)response["errorMessage"];
                string cause = null;

                if (response["cause"] != null)
                {
                    cause = (string)response["cause"];
                }

                result = new AuthenticationResult(httpResponse.StatusCode, error, errorMessage, cause);
            }

            return result;
        }
        #endregion

        #region Refresh
        public RefreshResult Refresh(string accessToken)
        {
            RefreshResult result = null;

            JObject payload = new JObject();

            payload["accessToken"] = accessToken;
            payload["clientToken"] = _ClientToken.ToString();

            HttpContent content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _Client.PostAsync(AUTH_SERVER + "/refresh", content).Result;
            JObject response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string newAccessToken = response["accessToken"].ToString();

                result = new RefreshResult(newAccessToken);
            }
            else
            {
                string error = (string)response["error"];
                string errorMessage = (string)response["errorMessage"];
                string cause = null;

                if (response["cause"] != null)
                {
                    cause = (string)response["cause"];
                }

                result = new RefreshResult(httpResponse.StatusCode, error, errorMessage, cause);
            }

            return result;
        }
        #endregion

        #region Validate
        public ValidationResult Validate(string accessToken)
        {
            ValidationResult result = null;

            JObject payload = new JObject();

            payload["accessToken"] = accessToken;
            payload["clientToken"] = _ClientToken.ToString();

            HttpContent content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _Client.PostAsync(AUTH_SERVER + "/validate", content).Result;

            if (httpResponse.StatusCode == HttpStatusCode.NoContent)
            {
                result = new ValidationResult(true);
            }
            else
            {
                JObject response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

                string error = (string)response["error"];
                string errorMessage = (string)response["errorMessage"];
                string cause = null;

                if (response["cause"] != null)
                {
                    cause = (string)response["cause"];
                }

                result = new ValidationResult(httpResponse.StatusCode, error, errorMessage, cause);
            }

            return result;
        }
        #endregion

        #region Signout
        public InvalidationResult Signout(string usernameOrEmailAddress, string password)
        {
            InvalidationResult result = null;

            JObject payload = new JObject();

            payload["username"] = usernameOrEmailAddress;
            payload["password"] = password;

            HttpContent content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _Client.PostAsync(AUTH_SERVER + "/signout", content).Result;

            if (httpResponse.StatusCode == HttpStatusCode.NoContent)
            {
                result = new InvalidationResult(true);
            }
            else
            {
                JObject response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

                string error = (string)response["error"];
                string errorMessage = (string)response["errorMessage"];
                string cause = null;

                if (response["cause"] != null)
                {
                    cause = (string)response["cause"];
                }

                result = new InvalidationResult(httpResponse.StatusCode, error, errorMessage, cause);
            }

            return result;
        }

        #endregion

        #region Invalidate
        public InvalidationResult Invalidate(string accessToken)
        {
            InvalidationResult result = null;

            JObject payload = new JObject();

            payload["accessToken"] = accessToken;
            payload["clientToken"] = _ClientToken.ToString();

            HttpContent content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _Client.PostAsync(AUTH_SERVER + "/invalidate", content).Result;

            if (httpResponse.StatusCode == HttpStatusCode.NoContent)
            {
                result = new InvalidationResult(true);
            }
            else
            {
                JObject response = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);

                string error = (string)response["error"];
                string errorMessage = (string)response["errorMessage"];
                string cause = null;

                if (response["cause"] != null)
                {
                    cause = (string)response["cause"];
                }

                result = new InvalidationResult(httpResponse.StatusCode, error, errorMessage, cause);
            }

            return result;
        }
        #endregion

    }
}
