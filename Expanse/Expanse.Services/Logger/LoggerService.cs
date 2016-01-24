#region Using namespaces...

using System;
using System.Diagnostics;
using System.IO;
using Ninject;
using NLog;
using NLog.Config;
using NLog.Targets;

#endregion


namespace Expanse.Services.Logger
{
    internal sealed class LoggerService : Core.Services.Logger.LoggerService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly NLog.Logger _log;

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public LoggerService()
        {
            //TODO move this to configuration

            var pathToLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
                @"Expances\Log\");

            if (!Directory.Exists(pathToLog))
            {
                Directory.CreateDirectory(pathToLog);
            }

            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = $"{pathToLog}/{"${shortdate}.log"}";
            fileTarget.Layout = "${level}|${date}|${machinename}|${windows-identity}           ${message}";

            var consoleTarget = new ConsoleTarget
            {
                Name = "String",
                Layout = "${message}"
            };

            config.AddTarget(consoleTarget);

#if DEBUG
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));
#else
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, fileTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, consoleTarget));
#endif

            LogManager.Configuration = config;

            _log = LogManager.GetCurrentClassLogger();


            Info("Current directory: {0}", Environment.CurrentDirectory);
        }

        #endregion

        #region Public Methods

        [DebuggerStepThrough]
        public override void Trace(string message, params object[] args) => _log.Trace(message, args);

        [DebuggerStepThrough]
        public override void Debug(string message, params object[] args) => _log.Debug(message, args);

        [DebuggerStepThrough]
        public override void Info(string message, params object[] args) => _log.Info(message, args);

        [DebuggerStepThrough]
        public override void Warn(string message, params object[] args) => _log.Warn(message, args);

        [DebuggerStepThrough]
        public override void Error(string message, params object[] args) => _log.Error(message, args);

        [DebuggerStepThrough]
        public override void Fatal(string message, params object[] args) => _log.Fatal(message, args);

        #endregion
    }
}