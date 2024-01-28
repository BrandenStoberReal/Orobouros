using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UniScraperDLL.Bases
{
    public class HttpAPIAsset
    {
        /// <summary>
        /// The HTTP status code the HTTP response sent back.
        /// </summary>
        public HttpStatusCode? ResponseCode { get; set; } = null;

        /// <summary>
        /// Indicates whether the HTTP request was successful.
        /// </summary>
        public bool? Successful { get; set; } = null;

        /// <summary>
        /// A list of all response headers the HTTP request sent back.
        /// </summary>
        public HttpResponseHeaders? ResponseHeaders { get; set; } = null;

        /// <summary>
        /// Response message from an internal HTTP call.
        /// </summary>
        public HttpResponseMessage? Response { get; set; } = null;
    }
}