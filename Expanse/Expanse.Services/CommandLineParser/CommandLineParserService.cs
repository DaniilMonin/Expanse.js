﻿#region Using namespaces...

using System.Diagnostics;
using System.Linq;
using Expanse.Core.Services.CommandLineParser;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.ScriptEngine;
using Expanse.Core.Services.VersionInfo;
using Expanse.Services.CommandLineParser.Data;
using Fclp;
using Ninject;

#endregion


namespace Expanse.Services.CommandLineParser
{
    internal sealed class CommandLineParserService : ICommandLineParserService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IVersionInfoService _versionInfo;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IScriptEngineService _rootEngine;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly FluentCommandLineParser<CommandLineArgumentsData> _commandLineParser = new FluentCommandLineParser<CommandLineArgumentsData>();

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public CommandLineParserService(LoggerService logger, IVersionInfoService versionInfo, IScriptEngineService rootEngine)
        {
            _commandLineParser.Setup(arg => arg.ProgramFileName).As('r', "run").Required();

            _logger = logger;
            _versionInfo = versionInfo;
            _rootEngine = rootEngine;
        }

        #endregion

        #region Public Methods

        public void Parse(string[] args)
        {
            _logger.Info(_versionInfo.GetVersionInformation());

            if (args != null && args.Any())
            {
                if (_commandLineParser.Parse(args).HasErrors)
                {
                    _logger.Error("Invalid arguments - exiting...");
                }

                _rootEngine.RunScriptProgram(_commandLineParser.Object.ProgramFileName);

                return;
            }

            _logger.Error("No any arguments - exiting...");
        }

        #endregion
    }
}