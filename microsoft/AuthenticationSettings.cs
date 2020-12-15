namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    public class AuthenticationSettings
    {
        /// <summary>
        /// User agent for your application. This is not required, it is just used to identify your invidual application.
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// Your application's client id. Default value is <b>00000000402b5328</b>.
        /// </summary>
        public string ClientId { get; set; } = "00000000402b5328";
        public string RedirectUri { get; set; } = "https://login.live.com/oauth20_desktop.srf";
    }
}