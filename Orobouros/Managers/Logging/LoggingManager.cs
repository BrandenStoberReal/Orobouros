﻿using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace Orobouros.Managers.Logging
{
    /// <summary>
    /// Primary class for handling logging operations.
    /// </summary>
    public static class LoggingManager
    {
        /// <summary>
        /// Primary logs handler for all of Orobouros.
        /// </summary>
        private static Logger PrimaryLogger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information).WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug).CreateLogger();

        /// <summary>
        /// Logs standard information to the logging stack.
        /// </summary>
        /// <param name="message"></param>
        public static void LogInformation(string message)
        {
            PrimaryLogger.Information($"[{Assembly.GetCallingAssembly().GetName().Name}] " + message);
        }

        /// <summary>
        /// Logs standard warnings to the logging stack.
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            PrimaryLogger.Warning($"[{Assembly.GetCallingAssembly().GetName().Name}] " + message);
        }

        /// <summary>
        /// Logs standard(?) errors to the logging stack.
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            PrimaryLogger.Error($"[{Assembly.GetCallingAssembly().GetName().Name}] " + message);
        }

        /// <summary>
        /// Writes a message to the debug stack.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteToDebugLog(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
            PrimaryLogger.Debug($"[{Assembly.GetCallingAssembly().GetName().Name}] " + message);
        }
    }
}