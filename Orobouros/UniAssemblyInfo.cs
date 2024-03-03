namespace Orobouros
{
    public static class UniAssemblyInfo
    {
        public static string Version = "1.0.0";

        public enum ModuleContent
        {
            Text,
            Files,
            Images,
            Videos,
            Comments,
            Links,
            Subposts,
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

        public static string[] imageExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", //etc
        };

        public static string[] audioExtensions = {
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
        };

        public static string[] videoExtensions = {
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV", ".M4A", ".WEBM", //etc
        };

        public static bool IsImage(string path)
        {
            return imageExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        public static bool IsAudio(string path)
        {
            return audioExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }

        public static bool IsVideo(string path)
        {
            return videoExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase);
        }
    }
}