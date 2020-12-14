using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{

    public class Auth
    {

        public void initialize(Uri location)
        {
            /*
             * https://login.live.com/oauth20_authorize.srf
             * ?client_id=00000000402b5328
             * &response_type=code
             * &redirect_uri=https%3A%2F%2Flogin.live.com%2Foauth20_desktop.srf
             * &scope=service%3A%3Auser.auth.xboxlive.com%3A%3AMBI_SSL
            */
            var client_id = "00000000402b5328";
            var response_type = "code";
            var redirect_uri = "https%3A%2F%2Flogin.live.com%2Foauth20_desktop.srf";
            var scope = "service%3A%3Auser.auth.xboxlive.com%3A%3AMBI_SSL";
            webView.Load(AuthLinks.AuthTokenLink);
            webView.BrowserSettings.Javascript = CefSharp.CefState.Enabled;
            win.Content = webView;
            win.Title = "Login with Microsoft";
            win.Width = 400;
            win.Height = 600;
            win.Show();
            // listen to end oauth flow
            webView.AddressChanged += (s,a)=> {
                var nurl = a.NewValue.ToString();
                if (nurl != AuthLinks.AuthTokenLink && nurl.StartsWith(AuthLinks.RedirectUrlSuffix))
                {
                    String authCode = nurl.Substring(nurl.IndexOf("=") + 1, nurl.IndexOf("&"));
                    // once we got the auth code, we can turn it into a oauth token
                    Console.WriteLine("authCode: " + authCode); // TODO debug
                    AcquireAccessToken(authCode);
                }
            };

        }

        private void AcquireAccessToken(String authcode)
        {
            try
            {
                Uri uri = new(AuthLinks.AuthTokenLink);

                AuthTokenRequest data = new()
                {
                    ClientId = "00000000402b5328",
                    Code = authcode,
                    GrantType = "authorization_code",
                    RedirectUri = "https://login.live.com/oauth20_desktop.srf",
                    Scope = "service::user.auth.xboxlive.com::MBI_SSL"
                };

                HttpRequest request = HttpRequest.newBuilder(uri)
                        .header("Content-Type", "application/x-www-form-urlencoded")
                        .POST(ofFormData(data)).build();

                HttpClient.newBuilder().build().sendAsync(request, HttpResponse.BodyHandlers.ofString()).thenAccept(resp-> {
                    if (resp.statusCode() >= 200 && resp.statusCode() < 300)
                    {
                        String body = resp.body();
                        try
                        {
                            JSONObject jsonObject = (JSONObject)new JSONParser().parse(body);
                            String accessToken = (String)jsonObject.get("access_token");
                            System.out.println("accessToken: " + accessToken); // TODO debug
                            acquireXBLToken(accessToken);
                        }
                        catch (ParseException e)
                        {
                            e.printStackTrace();
                        }
                    }
                });
            }
            catch (URISyntaxException e)
            {
                e.printStackTrace();
            }
        }

        private void acquireXBLToken(String accessToken)
        {
            try
            {
                Uri uri = new Uri(AuthLinks.XblAuthLink);

                XBLToken data = new()
                {
                    Properties = new Dictionary<string, string> {
                        { "AuthMethod", "RPS"},
                        { "SiteName", "user.auth.xboxlive.com" },
                        { "RpsTicket", accessToken }
                    },
                    RelyingParty = "http://auth.xboxlive.com",
                    TokenType = "JWT"
                };

                HttpRequest request = HttpRequest.newBuilder(uri)
                        .header("Content-Type", "application/json")
                        .header("Accept", "application/json")
                        .POST(JsonSerializer.Serialize<XBLToken>(data)).build();

                HttpClient.newBuilder().build().sendAsync(request, HttpResponse.BodyHandlers.ofString()).thenAccept(resp-> {
                    if (resp.statusCode() >= 200 && resp.statusCode() < 300)
                    {
                        String body = resp.body();
                        try
                        {
                            JSONObject jsonObject = (JSONObject)new JSONParser().parse(body);
                            String xblToken = (String)jsonObject.get("Token");
                            System.out.println("xblToken: " + xblToken); // TODO debug
                            acquireXsts(xblToken);
                        }
                        catch (ParseException e)
                        {
                            e.printStackTrace();
                        }
                    }
                };
            }
            catch (WebException e)
            {
                Logger.Error(e.ToString());
            }
        }

        private void acquireXsts(String xblToken)
        {
            try
            {
                Uri uri = new Uri(AuthLinks.XstsAuthLink);

                XstsData data = new()
                {
                    Properties = new Dictionary<string, string>()
                    {
                        { "SandboxId", "RETAIL" },
                        { "UserTokens", xblToken }
                    },
                    RelyingParty = "rp://api.minecraftservices.com/",
                    TokenTyp = "JWT"
                };

                HttpRequest request = HttpRequest.newBuilder(uri)
                        .header("Content-Type", "application/json")
                        .header("Accept", "application/json")
                        .POST(ofJSONData(data)).build();

                HttpClient.newBuilder().build().sendAsync(request, HttpResponse.BodyHandlers.ofString()).thenAccept(resp-> {
                    if (resp.statusCode() >= 200 && resp.statusCode() < 300)
                    {
                        String body = resp.body();
                        try
                        {
                            JSONObject jsonObject = (JSONObject)new JSONParser().parse(body);
                            String xblXsts = (String)jsonObject.get("Token");
                            JSONObject claims = (JSONObject)jsonObject.get("DisplayClaims");
                            JSONArray xui = (JSONArray)claims.get("xui");
                            String uhs = (String)((JSONObject)xui.get(0)).get("uhs");
                            System.out.println("xblXsts: " + xblXsts + ", uhs: " + uhs); // TODO debug
                            acquireMinecraftToken(uhs, xblXsts);
                        }
                        catch (ParseException e)
                        {
                            e.printStackTrace();
                        }
                    }
                });
            }
            catch (URISyntaxException e)
            {
                e.printStackTrace();
            }
        }

        private void acquireMinecraftToken(String xblUhs, String xblXsts)
        {
            try
            {
                URI uri = new URI(mcLoginUrl);

                Map<Object, Object> data = Map.of(
                        "identityToken", "XBL3.0 x=" + xblUhs + ";" + xblXsts
                );

                HttpRequest request = HttpRequest.newBuilder(uri)
                        .header("Content-Type", "application/json")
                        .header("Accept", "application/json")
                        .POST(ofJSONData(data)).build();

                HttpClient.newBuilder().build().sendAsync(request, HttpResponse.BodyHandlers.ofString()).thenAccept(resp-> {
                    if (resp.statusCode() >= 200 && resp.statusCode() < 300)
                    {
                        String body = resp.body();
                        try
                        {
                            JSONObject jsonObject = (JSONObject)new JSONParser().parse(body);
                            String mcAccessToken = (String)jsonObject.get("access_token");
                            System.out.println("mcAccessToken: " + mcAccessToken); // TODO debug
                            checkMcStore(mcAccessToken);
                            checkMcProfile(mcAccessToken);
                        }
                        catch (ParseException e)
                        {
                            e.printStackTrace();
                        }
                    }
                });
            }
            catch (URISyntaxException e)
            {
                e.printStackTrace();
            }
        }

        private void checkMcStore(String mcAccessToken)
        {
            try
            {
                URI uri = new URI(mcStoreUrl);

                HttpRequest request = HttpRequest.newBuilder(uri)
                        .header("Authorization", "Bearer " + mcAccessToken)
                        .GET().build();

                HttpClient.newBuilder().build().sendAsync(request, HttpResponse.BodyHandlers.ofString()).thenAccept(resp-> {
                    if (resp.statusCode() >= 200 && resp.statusCode() < 300)
                    {
                        String body = resp.body();
                        System.out.println("store: " + body);
                    }
                });
            }
            catch (URISyntaxException e)
            {
                e.printStackTrace();
            }
        }

        private void checkMcProfile(String mcAccessToken)
        {
            try
            {
                URI uri = new URI(mcProfileUrl);

                HttpRequest request = HttpRequest.newBuilder(uri)
                        .header("Authorization", "Bearer " + mcAccessToken)
                        .GET().build();

                HttpClient.newBuilder().build().sendAsync(request, HttpResponse.BodyHandlers.ofString()).thenAccept(resp-> {
                    if (resp.statusCode() >= 200 && resp.statusCode() < 300)
                    {
                        String body = resp.body();
                        try
                        {
                            System.out.println("profile:" + body);
                            JSONObject jsonObject = (JSONObject)new JSONParser().parse(body);
                            String name = (String)jsonObject.get("name");
                            String uuid = (String)jsonObject.get("id");
                            String uuidDashes = uuid.replaceFirst(
                                    "(\\p{XDigit}{8})(\\p{XDigit}{4})(\\p{XDigit}{4})(\\p{XDigit}{4})(\\p{XDigit}+)", "$1-$2-$3-$4-$5"
                            );

                            // hack, not actually working, need to get the right values
                            Authenticator auth = ()-> new AuthInfo(name, mcAccessToken, UUID.fromString(uuidDashes), Map.of(), "mojang");
                            new MinecraftStartTask(()->System.out.println("corrupt!"), ()->System.out.println("started!"), auth, minecraftDirectory).start();
                        }
                        catch (ParseException e)
                        {
                            e.printStackTrace();
                        }
                    }
                    else
                    {
                        String body = resp.body();
                        System.out.println("profile: " + resp.statusCode() + ": " + body);
                    }
                });
            }
            catch (URISyntaxException e)
            {
                e.printStackTrace();
            }
        }

        public static HttpRequest.BodyPublisher ofJSONData(Map<Object, Object> data)
        {
            return HttpRequest.BodyPublishers.ofString(new JSONObject(data).toJSONString());
        }

        public static HttpRequest.BodyPublisher ofFormData(Map<Object, Object> data)
        {
            StringBuilder builder = new StringBuilder();
            for (Map.Entry<Object, Object> entry : data.entrySet())
            {
                if (builder.length() > 0)
                {
                    builder.append("&");
                }
                builder.append(URLEncoder.encode(entry.getKey().toString(), StandardCharsets.UTF_8));
                builder.append("=");
                builder.append(URLEncoder.encode(entry.getValue().toString(), StandardCharsets.UTF_8));
            }
            return HttpRequest.BodyPublishers.ofString(builder.toString());
        }
    }
}
