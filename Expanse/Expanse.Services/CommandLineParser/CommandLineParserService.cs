#region Using namespaces...

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


        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string NoLogoCommand = "nologo";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string RunCommand = "run";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string CreateProjectCommand = "createproject";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string CreateMvcProjectCommand = "createmvcproject";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string NewProjectPathCommand = "projectpath";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string HelpCommand = "help";
        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public CommandLineParserService(LoggerService logger, IVersionInfoService versionInfo, IScriptEngineService rootEngine)
        {
            _logger = logger;
            _versionInfo = versionInfo;
            _rootEngine = rootEngine;

            _commandLineParser.Setup(arg => arg.ProgramFileName).As('r', RunCommand).WithDescription("Run specific script");
            _commandLineParser.Setup(arg => arg.NoLogo).As('n', NoLogoCommand).SetDefault(false).WithDescription("Hide logo");
            _commandLineParser.Setup(arg => arg.NewProjectName).As('c', CreateProjectCommand).WithDescription("Create new project");
            _commandLineParser.Setup(arg => arg.NewProjectName).As('m', CreateMvcProjectCommand).WithDescription("Create new MVC project");
            _commandLineParser.Setup(arg => arg.NewProjectPath).As('p', NewProjectPathCommand).WithDescription("Where new project should be");
            _commandLineParser.Setup(arg => arg.NewProjectPath).As('?', HelpCommand).Callback(text => _logger.Info(text));
        }

        #endregion

        #region Public Methods

        public void Parse(string[] args)
        {
            if (args != null && args.Any())
            {
                if (_commandLineParser.Parse(args).HasErrors)
                {
                    _logger.Error("Invalid arguments");

                    return;
                }

                _logger.InfoIf(!_commandLineParser.Object.NoLogo, _versionInfo.GetVersionInformation());

                if (string.IsNullOrWhiteSpace(_commandLineParser.Object.ProgramFileName))
                {
                    _logger.Error($"No file found '{_commandLineParser.Object.ProgramFileName}'");

                    return;
                }
                
                _rootEngine.RunScript(_commandLineParser.Object.ProgramFileName);

                return;
            }

            _logger.Info(_versionInfo.GetVersionInformation());
        }

        #endregion
    }
}