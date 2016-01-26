#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.ScriptEngine;
using Jint;
using Ninject;

#endregion


namespace Expanse.Services.ScriptEngine
{
    internal sealed class JavaScriptEngineService : IScriptEngineService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IEnumerable<ScriptEngineExtensionPackage> _packages;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IDictionary<string, object> _cacheDictionary = new Dictionary<string, object>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private Engine _engine;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string RequireScriptTemplate = "var module = {{}};\r\n{0}\r\nmodule;";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string LogoMessageTemplate = "Javascript interpreter based on Jint {0}";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string RequireSystemFunctionName = "require";
        
        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public JavaScriptEngineService(
            LoggerService logger,
            IEnumerable<ScriptEngineExtensionPackage> packages)
        {
            _logger = logger;
            _packages = packages;
        }

        #endregion

        #region Public Properties

        public string Description => string.Format(LogoMessageTemplate, typeof (Engine).Assembly.GetName().Version);

        #endregion

        #region Public Methods

        public void RunScript(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    GetEngine().Execute(File.ReadAllText(fileName));
                }
                catch (Exception exception)
                {
                    _logger.Info(exception.Message);
                }

                return;
            }

            _logger.Error($"No such file - '{fileName}', exiting...");
        }

        #endregion

        #region Private Methods

        private dynamic Require(string fileName)
        {
            if (_cacheDictionary.ContainsKey(fileName))
            {
                return _cacheDictionary[fileName];
            }

            if (File.Exists(fileName))
            {
                try
                {
                    dynamic compiled = GetEngine().Execute(string.Format(RequireScriptTemplate, File.ReadAllText(fileName))).GetCompletionValue();

                    _cacheDictionary.Add(fileName, compiled);

                    return compiled;
                }
                catch (Exception exception)
                {
                    _logger.Error(exception.Message);
                }

                return new {};
            }

            _logger.Error($"File not found - '{fileName}'");

            return new {};
        }

        private Engine GetEngine()
        {
            if (_engine == null)
            {
                _engine = new Engine().SetValue(RequireSystemFunctionName, new Func<string, dynamic>(Require));

                foreach (var extension in _packages.SelectMany(package => package.GetExtensions()))
                {
                    _engine.SetValue(extension.Key, extension.Value);
                }
            }

            return _engine;
        }

        #endregion
    }
}