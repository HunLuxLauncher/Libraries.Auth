using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using hu.czompisoftware.libraries.general;
using System.IO;

namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class Authenticator
    {
        #region Declarations
        private Guid ClientToken;
        private string UserAgent;
        readonly string AUTH_SERVER = "https://authserver.mojang.com";
        #endregion

        #region Global functions
        public Authenticator(string user_agent, Guid clientToken)
        {
            UserAgent = user_agent;
            ClientToken = clientToken;
        }

        public Authenticator(string user_agent) : this(user_agent, Guid.NewGuid()) { }

        public Authenticator(Guid clientToken)
        {
            UserAgent = "HunLuxLauncher.UNDEFINED/0.0.0";
            ClientToken = clientToken;
        }

        public Authenticator() : this("HunLuxLauncher.UNDEFINED/0.0.0", Guid.NewGuid()) { }

        public Guid GetClientToken()
        {
            return ClientToken;
        }
        #endregion

        #region Authenticate
        public AuthenticationResult Authenticate(string usernameOrEmailAddress, string password, AgentType agentType, int agentVersion)
        {
            AuthenticationRequest request = new AuthenticationRequest
            {
                Agent = new Agent
                {
                    Name = agentType.ToString(),
                    Version = agentVersion
                },
                Username = usernameOrEmailAddress,
                Password = password,
                ClientToken = ClientToken.ToString(),
                RequestUser = true
            };

            AuthenticationResult result = null;
            try
            {
                var resultStr = NetHandler.SendPostRequest(new Uri(AUTH_SERVER + "/authenticate"), UserAgent, "application/json", JsonSerializer.Serialize(request));
                result = JsonSerializer.Deserialize<AuthenticationResult>(resultStr);
            }
            catch (WebException ex)
            {
                var response = new StreamReader((ex.Response as HttpWebResponse).GetResponseStream()).ReadToEnd();
                Logger.Error($"{response}");
            }

            return result;
        }
        #endregion

        #region Refresh
        public RefreshResult Refresh(string accessToken)
        {
            RefreshRequest request = new RefreshRequest
            {
                AccessToken = accessToken,
                ClientToken = ClientToken.ToString()
            };

            RefreshResult result = JsonSerializer.Deserialize<RefreshResult>(NetHandler.SendPostRequest(new Uri(AUTH_SERVER + "/refresh"), UserAgent, "application/json", JsonSerializer.Serialize(request)));
            
            return result;
        }
        #endregion

        #region Validate
        public ValidationResult Validate(string accessToken)
        {
            ValidationRequest request = new()
            {
                AccessToken = accessToken,
                ClientToken = ClientToken.ToString()
            };

            ValidationResult result = JsonSerializer.Deserialize<ValidationResult>(NetHandler.SendPostRequest(new Uri(AUTH_SERVER + "/validate"), UserAgent,"application/json", JsonSerializer.Serialize(request)));

            return result;
        }
        #endregion

        #region Signout
        public bool Signout(string usernameOrEmailAddress, string password)
        {
            SignoutRequest request = new SignoutRequest
            {
                Username = usernameOrEmailAddress,
                Password = password
            };

            return HttpStatusCode.OK == NetHandler.GetPostRequestStatusCode(new Uri(AUTH_SERVER +"/signout"), UserAgent, "application/json", JsonSerializer.Serialize(request));
        }

        #endregion

        #region Invalidate
        public InvalidationResult Invalidate(string accessToken)
        {

            InvalidationRequest request = new()
            {
                AccessToken = accessToken,
                ClientToken = ClientToken.ToString()
            };

            InvalidationResult result = JsonSerializer.Deserialize<InvalidationResult>(NetHandler.SendPostRequest(new Uri(AUTH_SERVER + "/validate"), UserAgent, "application/json", JsonSerializer.Serialize(request)));

            return result;
        }
        #endregion

    }
}
