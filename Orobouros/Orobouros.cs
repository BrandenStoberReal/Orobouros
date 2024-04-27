namespace Orobouros
{
    /// <summary>
    /// Holds various misc information about Orobouros.
    /// </summary>
    public static class Orobouros
    {
        /// <summary>
        /// Represents web content from/for a module.
        /// </summary>
        public enum ContentType
        {
            /// <summary>
            /// Generic text.
            /// </summary>
            Text,

            /// <summary>
            /// Generic files. Does not include images and videos.
            /// </summary>
            Files,

            /// <summary>
            /// Generic images.
            /// </summary>
            Images,

            /// <summary>
            /// Generic videos.
            /// </summary>
            Videos,

            /// <summary>
            /// Generic audio, such as music.
            /// </summary>
            Audio,

            /// <summary>
            /// Compiled comment objects.
            /// </summary>
            Comments,

            /// <summary>
            /// Generic links
            /// </summary>
            Links,

            /// <summary>
            /// Compiled post objects.
            /// </summary>
            Subposts,

            /// <summary>
            /// Other data not specified above.
            /// </summary>
            Other
        }

        /// <summary>
        /// Enum type representing the possible attachment content types that can be returned by modules.
        /// </summary>
        public enum AttachmentType
        {
            /// <summary>
            /// A generic file type. Example: test.json
            /// </summary>
            GenericFile,

            /// <summary>
            /// An image.
            /// </summary>
            Image,

            /// <summary>
            /// A video.
            /// </summary>
            Video,

            /// <summary>
            /// A piece of audio.
            /// </summary>
            Audio,

            /// <summary>
            /// A link to another resource.
            /// </summary>
            Link,

            /// <summary>
            /// A compressed archive, such as a zip or rar file.
            /// </summary>
            Archive,

            /// <summary>
            /// Other attachment type not listed.
            /// </summary>
            Other
        }

        /// <summary>
        /// Known image extensions. Case insensitive.
        /// </summary>
        private static readonly string[] imageExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF",
        };

        /// <summary>
        /// Known audio extensions. Case insensitive.
        /// </summary>
        private static readonly string[] audioExtensions = {
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA",
        };

        /// <summary>
        /// Known video extensions. Case insensitive.
        /// </summary>
        private static readonly string[] videoExtensions = {
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV", ".M4A", ".WEBM",
        };

        /// <summary>
        /// Known archive extensions. Case insensitive.
        /// </summary>
        private static readonly string[] archiveExtensions = {
            ".ZIP", ".RAR", ".7Z",
        };

        /// <summary>
        /// Determines whether a file is an image or not.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsImage(string path)
        {
            return imageExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether a file is audio or not.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsAudio(string path)
        {
            return audioExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether a file is a video or not.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsVideo(string path)
        {
            return videoExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether a file is an archive or not.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsArchive(string path)
        {
            return archiveExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns the attachment content type of the provided file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static AttachmentType FindAttachmentType(string path)
        {
            if (IsArchive(path))
            {
                return AttachmentType.Archive;
            }
            else if (IsVideo(path))
            {
                return AttachmentType.Video;
            }
            else if (IsImage(path))
            {
                return AttachmentType.Image;
            }
            else if (IsAudio(path))
            {
                return AttachmentType.Audio;
            }
            else
            {
                return AttachmentType.Other;
            }
        }
    }
}