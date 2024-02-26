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
    /// <summary>
    /// Manages all HTTP operations for the framework and essentially acts as the core of the entire project.
    /// </summary>
    public static class HttpManager
    {
        /// <summary>
        /// HTTP Client used for all HTTP requests. A new client is created if a proxy is used however.
        /// </summary>
        public static HttpClient MainClient = new HttpClient();

        /// <summary>
        /// </summary>
        public enum HttpVersionNumber
        {
            /// <summary>
            /// Specifies HTTP Version 1.0
            /// </summary>
            HTTP_1,

            /// <summary>
            /// Specifies HTTP Version 1.1
            /// </summary>
            HTTP_11,

            /// <summary>
            /// Specifies HTTP Version 2.0
            /// </summary>
            HTTP_2,

            /// <summary>
            /// Specifies HTTP Version 3.0 (tcp/udp dual support)
            /// </summary>
            HTTP_3
        }

        /// <summary>
        /// Private simple HTTP request builder.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="cookies"></param>
        /// <param name="useDefaultHeaders"></param>
        /// <param name="headers"></param>
        /// <param name="httpVersion"></param>
        /// <param name="httpPolicy"></param>
        /// <returns></returns>
        private static HttpAPIAsset SimpleHttpRequest(HttpMethod method, string url, string? proxy = null, List<Tuple<string, string>>? cookies = null, bool useDefaultHeaders = true, WebHeaderCollection? headers = null, HttpVersionNumber httpVersion = HttpVersionNumber.HTTP_2, HttpVersionPolicy httpPolicy = HttpVersionPolicy.RequestVersionOrHigher)
        {
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                // Initiate client
                HttpClient reqClient;

                // Handle proxy (if specified)
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

                // Add default headers here
                if (useDefaultHeaders)
                {
                    requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                    requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                    requestMessage.Headers.Add("User-Agent", UserAgentManager.RandomDesktopUserAgent);
                }

                // Add custom headers here
                if (headers != null)
                {
                    for (int i = 0; i < headers.Count; ++i)
                    {
                        string header = headers.GetKey(i);
                        foreach (string value in headers.GetValues(i))
                        {
                            requestMessage.Headers.Add(header, value);
                        }
                    }
                }

                // Add cookies (if any)
                if (cookies != null)
                {
                    // Add cookies for the request
                    string finalCookieString = String.Empty;
                    foreach (Tuple<string, string> cookie in cookies)
                    {
                        finalCookieString += $"{cookie.Item1}={cookie.Item2}; ";
                    }
                    requestMessage.Headers.Add("Cookie", finalCookieString);
                }

                // Specify HTTP protocol version
                switch (httpVersion)
                {
                    case HttpVersionNumber.HTTP_1:
                        requestMessage.Version = HttpVersion.Version10;
                        break;

                    case HttpVersionNumber.HTTP_11:
                        requestMessage.Version = HttpVersion.Version11;
                        break;

                    case HttpVersionNumber.HTTP_2:
                        requestMessage.Version = HttpVersion.Version20;
                        break;

                    case HttpVersionNumber.HTTP_3:
                        requestMessage.Version = HttpVersion.Version30;
                        break;
                }

                // Specify protocol policy
                requestMessage.VersionPolicy = httpPolicy;

                // Delcare API asset
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
        /// <param name="proxy"></param>
        /// <param name="cookies"></param>
        /// <param name="useDefaultHeaders"></param>
        /// <param name="headers"></param>
        /// <param name="httpVersion"></param>
        /// <param name="httpPolicy"></param>
        /// <returns></returns>
        public static HttpAPIAsset GET(string url, string? proxy = null, List<Tuple<string, string>>? cookies = null, bool useDefaultHeaders = true, WebHeaderCollection? headers = null, HttpVersionNumber httpVersion = HttpVersionNumber.HTTP_2, HttpVersionPolicy httpPolicy = HttpVersionPolicy.RequestVersionOrHigher)
        {
            return SimpleHttpRequest(HttpMethod.Get, url, proxy, cookies, useDefaultHeaders, headers, httpVersion, httpPolicy);
        }

        /// <summary>
        /// Simple HTTP DELETE request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proxy"></param>
        /// <param name="cookies"></param>
        /// <param name="useDefaultHeaders"></param>
        /// <param name="headers"></param>
        /// <param name="httpVersion"></param>
        /// <param name="httpPolicy"></param>
        /// <returns></returns>
        public static HttpAPIAsset DELETE(string url, string? proxy = null, List<Tuple<string, string>>? cookies = null, bool useDefaultHeaders = true, WebHeaderCollection? headers = null, HttpVersionNumber httpVersion = HttpVersionNumber.HTTP_2, HttpVersionPolicy httpPolicy = HttpVersionPolicy.RequestVersionOrHigher)
        {
            return SimpleHttpRequest(HttpMethod.Delete, url, proxy, cookies, useDefaultHeaders, headers, httpVersion, httpPolicy);
        }
    }
}