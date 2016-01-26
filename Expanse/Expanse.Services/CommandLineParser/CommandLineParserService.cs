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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly
            FluentCommandLineParser<CommandLineArgumentsData> _commandLineParser =
                new FluentCommandLineParser<CommandLineArgumentsData>();

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public CommandLineParserService(
            LoggerService logger, 
            IVersionInfoService versionInfo,
            IScriptEngineService rootEngine)
        {
            _logger = logger;
            _versionInfo = versionInfo;
            _rootEngine = rootEngine;

            InitializationCommandLineArguments();
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
                    _logger.Error($"File not found '{_commandLineParser.Object.ProgramFileName}'");

                    return;
                }

                _rootEngine.RunScript(_commandLineParser.Object.ProgramFileName);

                return;
            }

            _logger.Info(_versionInfo.GetVersionInformation());
        }

        #endregion

        #region Private Methods

        [DebuggerStepThrough]
        private void InitializationCommandLineArguments()
        {
            _commandLineParser.Setup(arg => arg.ProgramFileName)
                .As(CommandLineArgumentsInfo.RunShortCommand, CommandLineArgumentsInfo.RunCommand)
                .WithDescription(CommandLineArgumentsInfo.RunCommandDescription);


            _commandLineParser.Setup(arg => arg.NoLogo)
                .As(CommandLineArgumentsInfo.NoLogoShortCommand, CommandLineArgumentsInfo.NoLogoCommand)
                .SetDefault(false)
                .WithDescription(CommandLineArgumentsInfo.HelpCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectName)
                .As(CommandLineArgumentsInfo.CreateProjectShortCommand, CommandLineArgumentsInfo.CreateProjectCommand)
                .WithDescription(CommandLineArgumentsInfo.CreateProjectCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectName)
                .As(CommandLineArgumentsInfo.CreateMvcProjectShortCommand,
                    CommandLineArgumentsInfo.CreateMvcProjectCommand)
                .WithDescription(CommandLineArgumentsInfo.CreateMvcProjectCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectPath)
                .As(CommandLineArgumentsInfo.NewProjectPathCommandShortCommand,
                    CommandLineArgumentsInfo.NewProjectPathCommand)
                .WithDescription(CommandLineArgumentsInfo.NewProjectPathCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectPath)
                .As(CommandLineArgumentsInfo.HelpShortCommand, CommandLineArgumentsInfo.HelpCommand)
                .Callback(text => _logger.Info(text))
                .WithDescription(CommandLineArgumentsInfo.HelpCommandDescription);
        }

        #endregion

    }
}