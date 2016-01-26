#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Expanse.Core.Services.Logger;
using RazorEngine;
using RazorEngine.Templating;
using Expanse.Core.Services.Templates;
using Ninject;

#endregion


namespace Expanse.Services.Templates
{
    internal sealed class TemplatesService : ITemplatesService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IList<string> _cachedFiles = new List<string>();

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public TemplatesService(LoggerService logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public string Compile(string fileName, dynamic model = null)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    if (_cachedFiles.Contains(fileName))
                    {
                        return Engine.Razor.Run(fileName, null, new {Exports = model});
                    }

                    _cachedFiles.Add(fileName);

                    return Engine.Razor.RunCompile(File.ReadAllText(fileName), fileName, null, new {Exports = model});
                }
                catch (Exception exception)
                {
                    _logger.Error(exception.Message);

                    return exception.Message;
                }
            }

            return string.Empty;
        }

        #endregion
    }
}