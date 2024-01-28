using PartyLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UniScraperDLL.Bases;

namespace UniScraperDLL.Managers
{
    public static class HttpManager
    {
        /// <summary>
        /// HTTP Client used for all HTTP requests.
        /// </summary>
        public static HttpClient MainClient = new HttpClient();

        /// <summary>
        /// Private simple HTTP request builder.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private static Tuple<HttpAPIAsset?, Exception?> SimpleHttpRequest(HttpMethod method, string url, string? cookies = null)
        {
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                // Add headers here
                requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                requestMessage.Headers.Add("User-Agent", UserAgentManager.RandomDesktopUserAgent);
                if (cookies != null)
                {
                    requestMessage.Headers.Add("Cookie", cookies);
                }

                try
                {
                    HttpResponseMessage reply = MainClient.SendAsync(requestMessage).Result;
                    HttpAPIAsset apiAsset = new HttpAPIAsset();
                    apiAsset.Response = reply;
                    apiAsset.ResponseCode = reply.StatusCode;
                    apiAsset.ResponseHeaders = reply.Headers;
                    apiAsset.Successful = reply.IsSuccessStatusCode;
                    return Tuple.Create((HttpAPIAsset?)apiAsset, (Exception?)null);
                }
                catch (Exception ex)
                {
                    return Tuple.Create((HttpAPIAsset?)null, (Exception?)ex);
                }
            }
        }

        /// <summary>
        /// Simple HTTP GET request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Tuple<HttpAPIAsset?, Exception?> GET(string url, string? cookies = null)
        {
            return SimpleHttpRequest(HttpMethod.Get, url, cookies);
        }

        /// <summary>
        /// Simple HTTP DELETE request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Tuple<HttpAPIAsset?, Exception?> DELETE(string url, string? cookies = null)
        {
            return SimpleHttpRequest(HttpMethod.Delete, url, cookies);
        }
    }
}