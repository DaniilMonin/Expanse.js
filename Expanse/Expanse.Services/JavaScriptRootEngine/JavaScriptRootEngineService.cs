#region Using namespaces...

using System.Diagnostics;
using System.IO;
using Expanse.Core.Services.JavaScriptRootEngine;
using Expanse.Core.Services.Logger;
using Ninject;

#endregion


namespace Expanse.Services.JavaScriptRootEngine
{
    internal sealed class JavaScriptRootEngineService : IJavaScriptRootEngineService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

        #endregion
        
        #region Constructor

        [Inject]
        public JavaScriptRootEngineService(LoggerService logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public void RunProgram(string fileName)
        {
            if (File.Exists(fileName))
            {

            }

            _logger.Error($"No such file - '{fileName}', exiting...");
        }

        #endregion

    }
}