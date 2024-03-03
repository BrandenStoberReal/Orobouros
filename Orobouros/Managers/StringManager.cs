using System.Text.RegularExpressions;

namespace Orobouros.Managers
{
    /// <summary>
    /// Utility class that mainly provides string sanitization.
    /// </summary>
    public static class StringManager
    {
        /// <summary>
        /// Sanitizes text for usage in folder names. Has very specialized usage.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The sanitized string</returns>
        public static string SanitizeText(string text)
        {
            return text.Replace(".", "").Replace(":", "∶").Replace("?", "？")
                .Replace("’", "'").Replace("\n", "").Replace("\"", "“").Replace("*", "").Replace("<", "").Replace(">", "")
                .Replace("/", "⧸").Replace("|", "⏐").Replace("\\", "⧸").Replace(((char)0).ToString(), "").Replace("\t", "").Replace("\r", "").Trim();
        }

        /// <summary>
        /// Sanitizes a file name for use in downloading
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string SanitizeFile(string filename)
        {
            string nonUnicodeFilename = Regex.Replace(filename, @"[^\u0000-\u007F]+", string.Empty);
            return nonUnicodeFilename.Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("|", "").Replace("/", "").Replace("\\", "").Replace("~", "").Replace(" ", "_").Trim();
        }
    }
}