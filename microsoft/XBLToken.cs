using System.Collections.Generic;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    internal class XBLToken
    {
        public Dictionary<string, string> Properties { get; set; }
        public string RelyingParty { get; set; }
        public string TokenType { get; set; }
    }
}