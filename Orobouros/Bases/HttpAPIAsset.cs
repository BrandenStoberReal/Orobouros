using System.Net;
using System.Net.Http.Headers;

namespace Orobouros.Bases
{
    /// <summary>
    /// An asset fetched through an HTTP request. Contains various helpful variables and functions.
    /// </summary>
    public class HttpAPIAsset
    {
        /// <summary>
        /// The HTTP status code the HTTP response sent back.
        /// </summary>
        public HttpStatusCode? ResponseCode { get; set; } = null;

        /// <summary>
        /// Indicates whether the HTTP request was successful.
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// Whether the HTTP request errored or not. Separate from successful.
        /// </summary>
        public bool Errored { get; set; }

        /// <summary>
        /// A list of all response headers the HTTP request sent back.
        /// </summary>
        public HttpResponseHeaders? ResponseHeaders { get; set; } = null;

        /// <summary>
        /// Response message from an internal HTTP call.
        /// </summary>
        public HttpResponseMessage? Response { get; set; } = null;

        /// <summary>
        /// Provided exception if request failed.
        /// </summary>
        public Exception? Exception { get; set; } = null;

        /// <summary>
        /// HTTP response content, if applicable.
        /// </summary>

        public HttpContent? Content { get; set; } = null;
    }
}