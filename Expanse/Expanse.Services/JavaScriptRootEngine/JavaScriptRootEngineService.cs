#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Expanse.Core.Services.JavaScriptRootEngine;
using Expanse.Core.Services.JSonSerializer;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.Templates;
using Jint;
using Jint.Native;
using Ninject;

#endregion


namespace Expanse.Services.JavaScriptRootEngine
{
    internal sealed class JavaScriptRootEngineService : IJavaScriptRootEngineService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly ITemplatesService _templates;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IJSonSerializerService _jSonSerializer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IDictionary<string, object> _cacheDictionary = new Dictionary<string, object>();

        #endregion
        
        #region Constructor

        [Inject, DebuggerStepThrough]
        public JavaScriptRootEngineService(LoggerService logger, ITemplatesService templates, IJSonSerializerService jSonSerializer)
        {
            _logger = logger;
            _templates = templates;
            _jSonSerializer = jSonSerializer;
        }

        #endregion

        #region Public Methods

        public void RunProgram(string fileName)
        {
            if (File.Exists(fileName))
            {
                CreateAndGetEngine().Execute(File.ReadAllText(fileName));

                return;
            }

            _logger.Error($"No such file - '{fileName}', exiting...");
        }

        #endregion

        #region Private Methods

        private void Info(object message) => _logger.Info(message?.ToString());

        private dynamic Require(string fileName)
        {
            if (_cacheDictionary.ContainsKey(fileName))
            {
                return _cacheDictionary[fileName];
            }

            if (File.Exists(fileName))
            {
                var result = CreateAndGetEngine().Execute(File.ReadAllText(fileName)).GetCompletionValue().ToObject();

                _cacheDictionary.Add(fileName, result);

                return result;
            }

            Info($"Could not find {fileName}");

            return new {};
        }

        private Engine CreateAndGetEngine()
        {
            return new Engine()
                .SetValue("info", new Action<object>(Info))
                .SetValue("require", new Func<string, dynamic>(Require))
                .SetValue("toJson", new Func<dynamic, string>(_jSonSerializer.Serialize))
                .SetValue("razor", new Func<string, dynamic, string>(_templates.Compile));
        }

        #endregion


    }
}