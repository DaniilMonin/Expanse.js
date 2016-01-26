#region Using namespaces...

using System.Diagnostics;
using System.Linq;
using Expanse.Core.Services.CommandLineParser;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.ProjectExport;
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IProjectExportService _export;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly
            FluentCommandLineParser<CommandLineArgumentsData> _commandLineParser =
                new FluentCommandLineParser<CommandLineArgumentsData>();

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public CommandLineParserService(
            LoggerService logger, 
            IVersionInfoService versionInfo,
            IScriptEngineService rootEngine,
            IProjectExportService export)
        {
            _logger = logger;
            _versionInfo = versionInfo;
            _rootEngine = rootEngine;
            _export = export;

            InitializationCommandLineArguments();
        }

        #endregion

        #region Public Methods

        public void Parse(string[] args)
        {
            if (args != null && args.Any())
            {
                var parseResult = _commandLineParser.Parse(args);

                if (parseResult.HasErrors)
                {
                    _logger.Error("Invalid arguments");

                    return;
                }

                if (parseResult.HelpCalled)
                {
                    return;
                }

                Execute(_commandLineParser.Object);

                return;
            }

            _logger.Info(_versionInfo.GetVersionInformation());
        }

        #endregion

        #region Private Methods

        private void Execute(CommandLineArgumentsData cmdArgumentsData)
        {
            _logger.InfoIf(!cmdArgumentsData.NoLogo, _versionInfo.GetVersionInformation());

            if (string.IsNullOrWhiteSpace(cmdArgumentsData.ScriptToRunFileName))
            {
                return;
            }

            _rootEngine.RunScript(cmdArgumentsData.ScriptToRunFileName);
        }

        [DebuggerStepThrough]
        private void InitializationCommandLineArguments()
        {
            _commandLineParser.Setup(arg => arg.ScriptToRunFileName)
                .As(CommandLineArgumentsInfo.RunShortCommand, CommandLineArgumentsInfo.RunCommand)
                .WithDescription(CommandLineArgumentsInfo.RunCommandDescription);


            _commandLineParser.Setup(arg => arg.NoLogo)
                .As(CommandLineArgumentsInfo.NoLogoShortCommand, CommandLineArgumentsInfo.NoLogoCommand)
                .SetDefault(false)
                .WithDescription(CommandLineArgumentsInfo.NoLogoCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectName)
                .As(CommandLineArgumentsInfo.CreateProjectShortCommand, CommandLineArgumentsInfo.CreateProjectCommand)
                .WithDescription(CommandLineArgumentsInfo.CreateProjectCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectType)
                .As(CommandLineArgumentsInfo.NewProjectTypeShortCommand,
                    CommandLineArgumentsInfo.NewProjectTypeCommand)
                    .SetDefault(CommandLineArgumentsInfo.DefaultNewProjectTypeArgument)
                    .WithDescription(CommandLineArgumentsInfo.NewProjectTypeCommandDescription);

            _commandLineParser.Setup(arg => arg.NewProjectPath)
                .As(CommandLineArgumentsInfo.NewProjectPathCommandShortCommand,
                    CommandLineArgumentsInfo.NewProjectPathCommand)
                .WithDescription(CommandLineArgumentsInfo.NewProjectPathCommandDescription);

            _commandLineParser.SetupHelp(CommandLineArgumentsInfo.HelpShortCommand, CommandLineArgumentsInfo.HelpCommand)
                .Callback(text => _logger.Info(text));
        }

        #endregion

    }
}