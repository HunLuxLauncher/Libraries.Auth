﻿using System;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    internal static class AuthLinks
    {
        /// <summary>https://login.live.com/oauth20_authorize.srf?client_id=00000000402b5328&response_type=code&scope=service%3A%3Auser.auth.xboxlive.com%3A%3AMBI_SSL&redirect_uri=https%3A%2F%2Flogin.live.com%2Foauth20_desktop.srf</summary>
        internal static String LoginUrl => "https://login.live.com/oauth20_authorize.srf?client_id=00000000402b5328&response_type=code&scope=service%3A%3Auser.auth.xboxlive.com%3A%3AMBI_SSL&redirect_uri=https%3A%2F%2Flogin.live.com%2Foauth20_desktop.srf";


        /// <summary>https://login.live.com/oauth20_desktop.srf?code=</summary>
        internal static String RedirectUrlSuffix => "https://login.live.com/oauth20_desktop.srf?code=";

        /// <summary>https://login.live.com/oauth20_token.srf</summary>
        internal static String AuthTokenLink => "https://login.live.com/oauth20_token.srf";


        /// <summary>https://user.auth.xboxlive.com/user/authenticate</summary>
        internal static String XblAuthLink => "https://user.auth.xboxlive.com/user/authenticate";


        /// <summary>https://xsts.auth.xboxlive.com/xsts/authorize</summary>
        internal static String XstsAuthLink => "https://xsts.auth.xboxlive.com/xsts/authorize";


        /// <summary>https://api.minecraftservices.com/authentication/login_with_xbox</summary>
        internal static String McLoginLink => "https://api.minecraftservices.com/authentication/login_with_xbox";


        /// <summary>https://api.minecraftservices.com/entitlements/mcstore</summary>
        internal static String McStoreLink => "https://api.minecraftservices.com/entitlements/mcstore";


        /// <summary>https://api.minecraftservices.com/minecraft/profile</summary>
        internal static String McProfileLink => "https://api.minecraftservices.com/minecraft/profile";
    }
}
