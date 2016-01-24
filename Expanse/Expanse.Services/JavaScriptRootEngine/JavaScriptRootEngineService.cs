#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Expanse.Core.Services.JavaScriptRootEngine;
using Expanse.Core.Services.Logger;
using Jint;
using Ninject;

#endregion


namespace Expanse.Services.JavaScriptRootEngine
{
    internal sealed class JavaScriptRootEngineService : IJavaScriptRootEngineService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IEnumerable<JavaScriptExtensionPackage> _packages;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IDictionary<string, object> _cacheDictionary = new Dictionary<string, object>();

        private const string RequireScriptTemplate = "var module = {{}};\r\n{0}\r\nmodule;";

        #endregion
        
        #region Constructor

        [Inject, DebuggerStepThrough]
        public JavaScriptRootEngineService(
            LoggerService logger,
            IEnumerable<JavaScriptExtensionPackage> packages)
        {
            _logger = logger;
            _packages = packages;
        }

        #endregion

        #region Public Properties

        public string Description => $"Javascript interpreter based on Jint {typeof(Engine).Assembly.GetName().Version}";

        #endregion

        #region Public Methods

        public void RunProgram(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    CreateAndGetEngine().Execute(File.ReadAllText(fileName));
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
                    dynamic compiled = CreateAndGetEngine().Execute(string.Format(RequireScriptTemplate, File.ReadAllText(fileName))).GetCompletionValue();

                    _cacheDictionary.Add(fileName, compiled);

                    return compiled;
                }
                catch (Exception exception)
                {
                    _logger.Info(exception.Message);
                }

                return new {};
            }

            _logger.Info($"Could not find {fileName}");

            return new {};
        }

        private Engine CreateAndGetEngine()
        {
            var engine = new Engine().SetValue("require", new Func<string, dynamic>(Require));

            foreach (var extension in _packages.SelectMany(package => package.GetExtensions()))
            {
                engine.SetValue(extension.Key, extension.Value);
            }

            return engine;
        }

        #endregion
    }
}