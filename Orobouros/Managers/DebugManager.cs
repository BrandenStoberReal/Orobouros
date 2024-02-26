using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    /// <summary>
    /// Manages various debug-related features.
    /// </summary>
    public static class DebugManager
    {
        /// <summary>
        /// Writes a message to the debug stack.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteToDebugLog(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }
    }
}