using hu.czompisoftware.libraries.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{

    public class Auth
    {

        #region Properties
        private AuthenticationSettings AuthSettings { get; }
        #endregion

        #region Global methods
        public Auth(AuthenticationSettings auth_settings)
        {
            AuthSettings = auth_settings;
        }
        #endregion

        #region Authenticate with Microsoft

        #region LogoutInit
        /// <summary>
        /// Authenticate with Microsoft Part #1<br/>
        /// This function is the first one that you have to call and you need to pass the return value to the user to be able to log in to their 
        /// Microsoft account. After you done that, you need to monitor the window/webbrowser that this method's <see cref="Uri"/> is passed to,
        /// because otherwise you cannot retrieve the <b>authorization_code</b> and without it, you cannot continue the authentication process.
        /// </summary>
        /// <param name="request">Leave it <b>null</b> if you'd like to use the default request datas that are present on the official <b>Minecraft Launcher</b> as well.</param>
        /// <returns>With an <see cref="Uri"/> based on <paramref name="request"/> data. See <seealso cref="AuthorizeRequest"/> for more info.</returns>
        public Uri LogoutInit(account.AuthorizeRequest logoutRequest = null, account.AuthorizeRequest loginRequest = null)
        {
            
            if (logoutRequest == null)
            {
                logoutRequest = new account.AuthorizeRequest
                {
                    ClientId = AuthSettings.ClientId,
                    RedirectUri = AuthSettings.RedirectUri
                };
            }
            return new Uri($"{AuthLinks.LogoutUrl}?{logoutRequest.ToFormRequest<account.AuthorizeRequest>()}");
        }
        #endregion
        
        #region LoginInit
        /// <summary>
        /// Authenticate with Microsoft Part #1<br/>
        /// This function is the first one that you have to call and you need to pass the return value to the user to be able to log in to their 
        /// Microsoft account. After you done that, you need to monitor the window/webbrowser that this method's <see cref="Uri"/> is passed to,
        /// because otherwise you cannot retrieve the <b>authorization_code</b> and without it, you cannot continue the authentication process.
        /// </summary>
        /// <param name="request">Leave it <b>null</b> if you'd like to use the default request datas that are present on the official <b>Minecraft Launcher</b> as well.</param>
        /// <returns>With an <see cref="Uri"/> based on <paramref name="request"/> data. See <seealso cref="AuthorizeRequest"/> for more info.</returns>
        public Uri LoginInit(account.AuthorizeRequest request = null)
        {
            if (request == null)
            {
                request = new account.AuthorizeRequest
                {
                    ClientId = AuthSettings.ClientId,
                    ResponseType = "code",
                    RedirectUri = AuthSettings.RedirectUri,
                    Scope = "service::user.auth.xboxlive.com::MBI_SSL",
                };
            }
            return new Uri($"{AuthLinks.LoginUrl}?{request.ToFormRequest<account.AuthorizeRequest>()}");
        }
        #endregion

        #region CatchAuthorizationToken
        /// <summary>
        /// Authenticate with Microsoft Part #2<br/>
        /// Now we got the <b>authorization_code</b>, so we can get the <see cref="TokenResponse"/>.
        /// </summary>
        /// <param name="uri">After successfull authentication, we will have this <see cref="Uri"/> to get the <b>authorization_code</b> from.</param>
        /// <returns>AccessToken from <see cref="TokenResponse"/></returns>
        public account.TokenResponse CatchAuthorizationToken(Uri uri)
        {
            if (uri.ToString().StartsWith(AuthLinks.RedirectUrlSuffix))
            {
                var res = GetAuthorizationToken(uri);
                return res;
            }
            return null;
        }
        #endregion

        #region GetAuthorizationToken
        /// <summary>
        /// Authenticate with Microsoft Part #3<br/>
        /// Finally, with this method, the authentication is complete. This is private, because it is called in <seealso cref="CatchAuthorizationToken(Uri)"/>.
        /// Probably that function will pass the entire <see cref="TokenResponse"/> to return, so you can do whatever you want to do with it.
        /// </summary>
        /// <param name="uri">After we got the <b>authorization_code</b> from <see cref="Uri"/>, we navigate to <see cref="AuthLinks.AuthTokenLink"/> <see cref="Uri"/> to get the <see cref="TokenResponse"/> from it.</param>
        /// <returns></returns>
        private account.TokenResponse GetAuthorizationToken(Uri uri)
        {
            var get_data = (uri.Query.Trim('?').Contains("&") ? uri.Query.Trim('?').Split("&") : new string[] { uri.Query }).Select(x => new KeyValuePair<string, string>(x.Split("=")[0], x.Split("=")[1])).ToDictionary(x => x.Key, x => x.Value);
            account.AuthorizeRequest data = new()
            {
                ClientId = AuthSettings.ClientId,
                Code = get_data["code"],
                GrantType = "authorization_code",
                RedirectUri = AuthSettings.RedirectUri,
                Scope = "service::user.auth.xboxlive.com::MBI_SSL"
            };
            return JsonSerializer.Deserialize<account.TokenResponse>(NetHandler.SendPostRequest(AuthLinks.AuthTokenLink, AuthSettings.UserAgent, "application/x-www-form-urlencoded", data.ToFormRequest<account.AuthorizeRequest>()));
        }
        #endregion

        #region RefreshTokens
        /// <summary>
        /// Refresh <see cref="TokenResponse"/> tokens.<br/>
        /// This method will refresh <see cref="TokenResponse"/> by a help of <paramref name="refresh_token"/>.
        /// </summary>
        /// <param name="refresh_token">Refresh token from <see cref="CatchAuthorizationToken(Uri)"/>.</param>
        /// <returns>AccessToken from <see cref="TokenResponse"/></returns>
        public account.TokenResponse RefreshTokens(string refresh_token)
        {
            account.AuthorizeRequest data = new()
            {
                ClientId = AuthSettings.ClientId,
                Code = refresh_token,
                GrantType = "refresh_token",
                RedirectUri = AuthSettings.RedirectUri,
                Scope = "service::user.auth.xboxlive.com::MBI_SSL"
            };
            return JsonSerializer.Deserialize<account.TokenResponse>(NetHandler.SendPostRequest(AuthLinks.AuthTokenLink, AuthSettings.UserAgent, "application/x-www-form-urlencoded", data.ToFormRequest<account.AuthorizeRequest>()));
        }
        #endregion

        #endregion

        #region Authenticate with Xbox Live

        #region XboxLiveAuthenticate
        /// <summary>
        /// Authenticate with Xbox Live #1<br/>
        /// Now you're able to authenticate with XBL via your <paramref name="access_token"/>.
        /// </summary>
        /// <param name="access_token">The AccessToken from <see cref="TokenResponse"/>.</param>
        /// <returns></returns>
        public xbox.TokenResponse XboxLiveAuthenticate(string access_token)
        {
            xbox.AuthenticationRequest request = new()
            {
                Properties = new xbox.AuthenticationProperties
                {
                    AuthMethod = xbox.AuthMethod.RPS,
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = access_token
                },
                RelyingParty = "http://auth.xboxlive.com",
                TokenType = xbox.TokenType.JWT
            };
            var rstr = JsonSerializer.Serialize(request);
            Logger.Debug(rstr);
            xbox.TokenResponse response = JsonSerializer.Deserialize<xbox.TokenResponse>(NetHandler.SendPostRequest(AuthLinks.XblAuthLink, AuthSettings.UserAgent, "application/json", rstr, "Accept", "application/json"));

            return response;
        }
        #endregion

        #region XSTSAuthenticate
        /// <summary>
        /// Authenticate with Xbox Live #2<br/>
        /// After getting the <see cref="TokenResponse"/>, you'll be able to authenticate with XSTS.
        /// </summary>
        /// <param name="xbl_token">The Token value of <see cref="TokenResponse"/>.</param>
        public xbox.TokenResponse XSTSAuthenticate(string xbl_token)
        {
            xbox.AuthenticationRequest request = new()
            {
                Properties = new xbox.AuthenticationProperties
                {
                    SandboxId = "RETAIL",
                    UserTokens = new List<string>()
                    {
                        xbl_token
                    }
                },
                RelyingParty = "rp://api.minecraftservices.com/",
                TokenType = xbox.TokenType.JWT
            };

            xbox.TokenResponse response = JsonSerializer.Deserialize<xbox.TokenResponse>(NetHandler.SendPostRequest(AuthLinks.XstsAuthLink, AuthSettings.UserAgent, "application/json", JsonSerializer.Serialize(request), "Accept", "application/json"));
            
            return response;
        }
        #endregion

        #endregion

        #region Authenticate with Minecraft
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user_hash">Use the 1st item's <b>UserHash</b> from array <see cref="xbox.TokenResponse.DisplayClaims"/> at <see cref="XboxLiveAuthenticate(string)"/>.</param>
        /// <param name="xsts_token">Use <see cref="xbox.TokenResponse.Token"/> from <see cref="XSTSAuthenticate(string)"/>.</param>
        public minecraft.Authenticate MinecraftAuthenticate(string user_hash, string xsts_token)
        {
            minecraft.AuthenticateRequest request = new()
            {
                IdentityToken = $"XBL3.0 x={user_hash};{xsts_token}"
            };

            minecraft.Authenticate response = JsonSerializer.Deserialize<minecraft.Authenticate>(NetHandler.SendPostRequest(AuthLinks.McLoginLink, AuthSettings.UserAgent, "application/json", JsonSerializer.Serialize(request), "Accept", "application/json"));
            return response;
        }
        #endregion
    }
}
