using PartyLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Orobouros.Bases;

namespace Orobouros.Managers
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
        private static HttpAPIAsset SimpleHttpRequest(HttpMethod method, string url, string? proxy = null, string? cookies = null)
        {
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                HttpClient reqClient;
                if (proxy != null)
                {
                    // Use proxy for web request
                    HttpClientHandler httpClientHandler = new HttpClientHandler();
                    IWebProxy coolProxy = new WebProxy(proxy);
                    httpClientHandler.Proxy = coolProxy;
                    reqClient = new HttpClient(httpClientHandler);
                }
                else
                {
                    // Use default client for http if no proxies
                    reqClient = MainClient;
                }

                // Add headers here
                requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                requestMessage.Headers.Add("User-Agent", UserAgentManager.RandomDesktopUserAgent);

                if (cookies != null)
                {
                    // Add cookies for the request
                    requestMessage.Headers.Add("Cookie", cookies);
                }

                HttpAPIAsset apiAsset = new HttpAPIAsset();
                try
                {
                    // Send HTTP request
                    HttpResponseMessage reply = MainClient.SendAsync(requestMessage).Result;
                    apiAsset.Response = reply;
                    apiAsset.ResponseCode = reply.StatusCode;
                    apiAsset.ResponseHeaders = reply.Headers;
                    apiAsset.Successful = reply.IsSuccessStatusCode;
                    apiAsset.Errored = false;
                    apiAsset.Content = reply.Content;

                    if (proxy != null)
                    {
                        // Dispose of new httpclient
                        reqClient.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    if (proxy != null)
                    {
                        // Dispose of new httpclient
                        reqClient.Dispose();
                    }

                    // Fill out errored api asset
                    apiAsset.Successful = false;
                    apiAsset.Errored = true;
                    apiAsset.Exception = ex;
                }
                return apiAsset;
            }
        }

        /// <summary>
        /// Simple HTTP GET request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpAPIAsset GET(string url, string? cookies = null)
        {
            return SimpleHttpRequest(HttpMethod.Get, url, cookies);
        }

        /// <summary>
        /// Simple HTTP DELETE request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpAPIAsset DELETE(string url, string? cookies = null)
        {
            return SimpleHttpRequest(HttpMethod.Delete, url, cookies);
        }
    }
}