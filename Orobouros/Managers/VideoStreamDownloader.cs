using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace Orobouros.Managers
{
    /// <summary>
    /// A class designed to download HLS video streams.
    /// </summary>
    public class VideoStreamDownloader
    {
        /// <summary>
        /// EVent handler called whenever a download progresses.
        /// </summary>
        public event EventHandler<DownloadProgress> DownloadProgressed;

        /// <summary>
        /// Output folder for downloaded content.
        /// </summary>
        public string OutputFolder { get; private set; } = String.Empty;

        private YoutubeDL DownloaderClass { get; set; } = new YoutubeDL();
        private Progress<DownloadProgress> ProgressHandler { get; set; } = new Progress<DownloadProgress>();
        private CancellationTokenSource CancelToken { get; set; } = new CancellationTokenSource();

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="outputFolder"></param>
        public VideoStreamDownloader(string outputFolder)
        {
            if (!Directory.Exists("./binaries"))
            {
                Directory.CreateDirectory("./binaries");
            }

            OutputFolder = outputFolder;

            // Download required binaries
            YoutubeDLSharp.Utils.DownloadYtDlp("./binaries").Wait();
            YoutubeDLSharp.Utils.DownloadFFmpeg("./binaries").Wait();

            // Assign paths
            DownloaderClass.YoutubeDLPath = Path.Combine("./binaries", "yt-dlp.exe");
            DownloaderClass.FFmpegPath = Path.Combine("./binaries", "ffmpeg.exe");
            DownloaderClass.OutputFolder = outputFolder;
            ProgressHandler.ProgressChanged += DownloadProgressed;
        }

        /// <summary>
        /// Cancels the current download, if one is in progress.
        /// </summary>
        public void CancelCurrentDownload()
        {
            CancelToken.Cancel();
        }

        /// <summary>
        /// Downloads the specified video stream.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DownloadVideo(string url)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancelToken = source;
            RunResult<string> result = DownloaderClass.RunVideoDownload(url, progress: ProgressHandler, ct: CancelToken.Token).Result;
            return result.Data;
        }

        /// <summary>
        /// Downloads the specified video stream and parses the audio out of it, only keeping the audio.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DownloadAudio(string url)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancelToken = source;
            RunResult<string> result = DownloaderClass.RunAudioDownload(url, AudioConversionFormat.Best, progress: ProgressHandler, ct: CancelToken.Token).Result;
            return result.Data;
        }
    }
}