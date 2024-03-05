namespace Orobouros
{
    /// <summary>
    /// Holds various misc information about Orobouros.
    /// </summary>
    public static class OrobourosInformation
    {
        /// <summary>
        /// Represents web content from/for a module.
        /// </summary>
        public enum ModuleContent
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
        public enum AttachmentContent
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
            /// A link to another resource.
            /// </summary>
            Link,

            /// <summary>
            /// A compressed archive, such as a zip or rar file.
            /// </summary>
            Archive
        }

        /// <summary>
        /// Known image extensions.
        /// </summary>
        private static string[] imageExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", //etc
        };

        /// <summary>
        /// Known audio extensions.
        /// </summary>
        private static string[] audioExtensions = {
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
        };

        /// <summary>
        /// Known video extensions.
        /// </summary>
        private static string[] videoExtensions = {
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV", ".M4A", ".WEBM", //etc
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
        /// Determines whether a file is video or not.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsVideo(string path)
        {
            return videoExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }
    }
}