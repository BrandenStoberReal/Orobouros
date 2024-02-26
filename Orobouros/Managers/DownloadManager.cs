using Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Downloader;

using DownloadProgressChangedEventArgs = Downloader.DownloadProgressChangedEventArgs;

using Orobouros;
using static Orobouros.Managers.HttpManager;
using System.Net.Cache;

namespace Orobouros.Managers
{
    /// <summary>
    /// Handles all of the framework's various downloading tasks, either from the web or other resources.
    /// </summary>
    public class DownloadManager
    {
        /// <summary>
        /// Handler for completed download event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        public delegate void DownloadCompleteHandler(object sender, AsyncCompletedEventArgs eventArgs, string fileName = null);

        /// <summary>
        /// Handler for failed download tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <param name="fileName"></param>
        /// <param name="error"></param>
        public delegate void DownloadFailiureHandler(object sender, AsyncCompletedEventArgs eventArgs, string fileName = null, Exception error = null);

        /// <summary>
        /// Handler for downloader progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <param name="fileName"></param>
        public delegate void DownloadProgressHandler(object sender, DownloadProgressChangedEventArgs eventArgs, string fileName = null);

        /// <summary>
        /// Event raised whenever a download finishes at all, regardless of status
        /// </summary>
        public event DownloadCompleteHandler DownloadComplete;

        /// <summary>
        /// Event raised when a download successfully finishes
        /// </summary>
        public event DownloadCompleteHandler DownloadSuccess;

        /// <summary>
        /// Event raised whenever a download fails to finish
        /// </summary>
        public event DownloadFailiureHandler DownloadFailure;

        /// <summary>
        /// Event raised whenever the downloader recieves a new chunk and progresses forwards
        /// </summary>
        public event DownloadProgressHandler DownloadProgressed;

        /// <summary>
        /// Raw download function for interacting with the downloader library.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="folder"></param>
        /// <param name="filename"></param>
        /// <param name="cache"></param>
        /// <param name="KeepAlive"></param>
        /// <param name="chunks"></param>
        /// <param name="connections"></param>
        /// <param name="retries"></param>
        /// <param name="headers"></param>
        /// <param name="httpVersion"></param>
        /// <returns>The status of the download</returns>
        private DownloadStatus RawDownloadBuilder(string url, string folder, string filename, bool cache = true, bool KeepAlive = true, int chunks = 1, int connections = 1, int retries = 5, WebHeaderCollection? headers = null, HttpVersionNumber httpVersion = HttpVersionNumber.HTTP_2)
        {
            // Random user agent
            string userAgent = UserAgentManager.RandomUserAgent;

            // Downloader options
            var downloadOpt = new DownloadConfiguration
            {
                ChunkCount = chunks,
                ParallelDownload = true,
                ParallelCount = connections,
                MaxTryAgainOnFailover = retries,
                RequestConfiguration =
            {
                Accept = "*/*",
                KeepAlive = KeepAlive,
                UseDefaultCredentials = false,
                UserAgent = userAgent
            }
            };

            // Assign headers
            if (headers != null)
            {
                downloadOpt.RequestConfiguration.Headers = headers;
            }

            // Specify HTTP protocol version
            switch (httpVersion)
            {
                case HttpVersionNumber.HTTP_1:
                    downloadOpt.RequestConfiguration.ProtocolVersion = HttpVersion.Version10;
                    break;

                case HttpVersionNumber.HTTP_11:
                    downloadOpt.RequestConfiguration.ProtocolVersion = HttpVersion.Version11;
                    break;

                case HttpVersionNumber.HTTP_2:
                    downloadOpt.RequestConfiguration.ProtocolVersion = HttpVersion.Version20;
                    break;

                case HttpVersionNumber.HTTP_3:
                    downloadOpt.RequestConfiguration.ProtocolVersion = HttpVersion.Version30;
                    break;
            }

            // Handle caching
            if (cache)
            {
                RequestCachePolicy cachePolicy = new RequestCachePolicy(RequestCacheLevel.Default);
                downloadOpt.RequestConfiguration.CachePolicy = cachePolicy;
            }
            else
            {
                RequestCachePolicy cachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                downloadOpt.RequestConfiguration.CachePolicy = cachePolicy;
            }

            // Build downloader
            IDownload download = DownloadBuilder.New()
                .WithUrl(url)
                .WithDirectory(folder)
                .WithFileName(filename)
                .WithConfiguration(downloadOpt)
                .Build();
            download.DownloadFileCompleted += (sender, e) => Downloader_DownloadFileCompleted(sender, e, filename);
            download.DownloadProgressChanged += (sender, DownloadProgressChangedEventArgs) =>
                Downloader_ProgressChanged(sender, DownloadProgressChangedEventArgs, filename);
            download.StartAsync().Wait(); // Me when async
            return download.Status;
        }

        /// <summary>
        /// Function called whenever a download progresses forwards.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="filename"></param>
        private void Downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e, string filename)
        {
            if (DownloadProgressed != null)
            {
                DownloadProgressed(sender, e, filename);
            }
        }

        /// <summary>
        /// Function called when a downloaded file has completed downloading, either with a failiure
        /// or with a completed file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        private void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e, string fileName)
        {
            if (DownloadComplete != null)
            {
                DownloadComplete(sender, e, fileName);
            }

            if (e.Error != null)
            {
                if (DownloadFailure != null)
                {
                    DownloadFailure(sender, e, fileName, e.Error);
                }
            }
            else
            {
                if (DownloadSuccess != null)
                {
                    DownloadSuccess(sender, e, fileName);
                }
            }
        }

        /// <summary>
        /// Downloads content from a specified raw media URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parentFolder"></param>
        /// <param name="fileName"></param>
        /// <returns>A boolean on whether the download was successful</returns>
        public bool DownloadContent(string? url, string parentFolder, string? fileName)
        {
            // Sanitize file name
            string sanitizedFileName = StringManager.SanitizeFile(fileName);

            // Make parent folder if it doesn't exist
            if (!Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }

            DownloadStatus status;
            try
            {
                // If file with same name exists, append random digits
                if (File.Exists(parentFolder + "/" + sanitizedFileName))
                {
                    status = RawDownloadBuilder(url, parentFolder, new Random().Next(1, 999) + "-" + sanitizedFileName);
                }
                else
                {
                    status = RawDownloadBuilder(url, parentFolder, sanitizedFileName);
                }
            }
            catch (Exception ex)
            {
                DebugManager.WriteToDebugLog("Exception \"" + ex + "\" occurred while downloading " + sanitizedFileName + "!");
                status = DownloadStatus.Failed;
            }

            if (status == DownloadStatus.Completed)
            {
                return true;
            }
            else
            {
                DebugManager.WriteToDebugLog($"Download {fileName} failed! Skipping...");
                return false;
            }
        }
    }
}