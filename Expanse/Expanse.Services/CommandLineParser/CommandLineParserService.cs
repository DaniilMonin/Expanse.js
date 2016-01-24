#region Using namespaces...

using System.Diagnostics;
using Expanse.Core.Services.CommandLineParser;
using Expanse.Core.Services.Logger;
using Ninject;

#endregion


namespace Expanse.Services.CommandLineParser
{
    internal sealed class CommandLineParserService : ICommandLineParserService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

        #endregion


        #region Constructor

        [Inject, DebuggerStepThrough]
        public CommandLineParserService(LoggerService logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public void Parse(string[] args)
        {

        }

        #endregion

    }
}