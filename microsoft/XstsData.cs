using System.Collections.Generic;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    internal class XstsData
    {
        public Dictionary<string, string> Properties { get; internal set; }
        public string RelyingParty { get; internal set; }
        public string TokenTyp { get; internal set; }
    }
}