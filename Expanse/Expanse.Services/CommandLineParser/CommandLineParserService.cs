#region Using namespaces...

using System;
using System.Diagnostics;
using System.Linq;
using Expanse.Core.Services.CommandLineParser;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.VersionInfo;
using Expanse.Shared.Data.CmdArgs;
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly FluentCommandLineParser<CommandLineArgumentsData> _commandLineParser = new FluentCommandLineParser<CommandLineArgumentsData>();

        #endregion


        #region Constructor

        [Inject, DebuggerStepThrough]
        public CommandLineParserService(LoggerService logger, IVersionInfoService versionInfo)
        {
            _commandLineParser.Setup(arg => arg.FileName).As('p', "program").Required();

            _logger = logger;
            _versionInfo = versionInfo;
        }

        #endregion

        #region Public Methods

        public void Parse(string[] args)
        {
            Console.WriteLine(_versionInfo.GetVersionInformation());
            Console.WriteLine(string.Empty);

            if (args != null && args.Any())
            {
                if (_commandLineParser.Parse(args).HasErrors)
                {
                    Console.WriteLine($"{args}");
                    Console.WriteLine("Invalid arguments - exiting...");
                }

                var fileName = _commandLineParser.Object;

                Console.WriteLine($"{fileName.FileName}");

                return;
            }

            Console.WriteLine("No any arguments - exiting...");
        }

        #endregion

    }
}