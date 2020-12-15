using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Web;

namespace hu.hunluxlauncher.libraries.auth
{
    public static class Extensions
    {
        public static string ToFormRequest<T>(this AuthenticationElement authentication) where T : AuthenticationElement
        {
            string str = "";
            return string.Join("&", JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(authentication as T)).Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));
        }
    }
}
