namespace hu.hunluxlauncher.libraries.auth.yggdrasil
{
    public class AuthenticationRequest
    {
        public Agent Agent { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientToken { get; set; }
        public bool RequestUser { get; set; }
    }
}