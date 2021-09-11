using System;
using System.IO;
using System.Net;

namespace Libraries.Auth
{
    public class NetHandler
    {

        #region SendPostRequest
        public static string SendPostRequest(string uri, string user_agent, string content_type, string post, params string[] additional_headers)
        {
            return SendRequest(new Uri(uri), RequestMethod.POST, user_agent, content_type, post, additional_headers);
        }
        #endregion
        
        #region SendPostRequest
        public static string SendPostRequest(Uri uri, string user_agent, string content_type, string post, params string[] additional_headers)
        {
            return SendRequest(uri, RequestMethod.POST, user_agent, content_type, post, additional_headers);
        }
        #endregion

        #region SendRequest
        public static string SendRequest(string uri, RequestMethod method, string user_agent, string content_type, string post,  params string[] additional_headers)
        {
            return SendRequest(new Uri(uri), method, user_agent, content_type, post, additional_headers);
        }
        #endregion
        
        #region SendRequest
        public static string SendRequest(Uri uri, RequestMethod method, string user_agent, string content_type, string post,  params string[] additional_headers)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = method.ToString();
                if (!string.IsNullOrEmpty(user_agent)) webRequest.UserAgent = user_agent;
                if (content_type != null)
                {
                    webRequest.ContentType = content_type;
                    using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        if (post != "")
                        {
                            streamWriter.Write(post);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    webRequest.ContentLength = post.Length;
                }
                if (additional_headers.Length > 0 && additional_headers.Length % 2 == 0)
                {
                    for (int i = 0; i < additional_headers.Length; i += 2)
                    {
                        if (additional_headers[i].ToLower() == "content-type") continue;
                        webRequest.Headers.Add(additional_headers[i], additional_headers[i + 1]);
                    }
                }

                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var streamReader = new StreamReader(webResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                return result;
            }
            catch (WebException ex)
            {
                if ((ex.Response as HttpWebResponse).StatusCode == HttpStatusCode.NotFound) throw new EntryPointNotFoundException((ex.Response as HttpWebResponse).ResponseUri.ToString());
                else throw ex;
            }
        }
        #endregion

        #region GetPostRequestStatusCode
        public static HttpStatusCode GetPostRequestStatusCode(Uri uri, string user_agent, string content_type, string post, params string[] additional_headers)
        {
            return GetRequestStatusCode(uri, RequestMethod.POST, user_agent, content_type, post, additional_headers);
        }
        #endregion

        #region SendRequest
        public static HttpStatusCode GetRequestStatusCode(Uri uri, RequestMethod method, string user_agent, string content_type, string post,  params string[] additional_headers)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = method.ToString();
            webRequest.UserAgent = user_agent;
            if (content_type != null)
            {
                webRequest.ContentType = content_type;
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    if (post != "")
                    {
                        streamWriter.Write(post);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                webRequest.ContentLength = post.Length;
            }
            if (additional_headers.Length > 0 && additional_headers.Length % 2 == 0)
            {
                for (int i = 0; i < additional_headers.Length; i += 2)
                {
                    if (additional_headers[i].ToLower() == "content-type") continue;
                    webRequest.Headers.Add(additional_headers[i], additional_headers[i + 1]);
                }
            }

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            return webResponse.StatusCode;
        }
        #endregion
    }
}