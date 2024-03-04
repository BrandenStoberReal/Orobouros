using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    /// <summary>
    /// Primary class for handling logging operations.
    /// </summary>
    public static class LoggingManager
    {
        /// <summary>
        /// Primary logs handler for all of Orobouros.
        /// </summary>
        private static Logger PrimaryLogger = new LoggerConfiguration().WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information).WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug).CreateLogger();

        /// <summary>
        /// Ensures the logging folder exists.
        /// </summary>
        public static void VerifyLogFolderIntegrity()
        {
            // Create logs directory if non-existent
            if (!Directory.Exists("./logs"))
            {
                Directory.CreateDirectory("./logs");
            }
        }

        /// <summary>
        /// Logs standard information to the logging stack.
        /// </summary>
        /// <param name="message"></param>
        public static void LogInformation(string message)
        {
            PrimaryLogger.Information(message);
        }

        /// <summary>
        /// Logs standard warnings to the logging stack.
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            PrimaryLogger.Warning(message);
        }

        /// <summary>
        /// Logs standard(?) errors to the logging stack.
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            PrimaryLogger.Error(message);
        }

        /// <summary>
        /// Writes a message to the debug stack.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteToDebugLog(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
            PrimaryLogger.Debug(message);
        }
    }
}