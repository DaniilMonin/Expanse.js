#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.ProjectExport.Templates;
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly List<string> _moduleDirectories = new List<string>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private Engine _engine;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), Obsolete("Moved to ProjectEnvironmentService")] private string _currentProjectPath;


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
                    _currentProjectPath = Path.GetDirectoryName(fileName);

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

        private string ProjectModulesPath => Path.Combine(_currentProjectPath, SpecialFolder.Modules.ToString());
        private string LocalModulesPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), "Modules");

        #region Private Methods

        private string GetModuleFullPath(string moduleName)
        {
            var fullPath = Path.Combine(ProjectModulesPath, moduleName);

            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            fullPath = Path.Combine(_currentProjectPath, moduleName);

            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            fullPath = Path.Combine(LocalModulesPath, moduleName);

            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            return string.Empty;
        }

        private dynamic Require(string fileName)
        {
            var module = GetModuleFullPath(fileName);

            if (string.IsNullOrWhiteSpace(module))
            {
                _logger.Error($"File not found - '{fileName}'");

                return new { };
            }

            if (_cacheDictionary.ContainsKey(module))
            {
                return _cacheDictionary[module];
            }

            if (File.Exists(module))
            {
                try
                {
                    dynamic compiled = GetEngine().Execute(string.Format(RequireScriptTemplate, File.ReadAllText(module))).GetCompletionValue();

                    _cacheDictionary.Add(module, compiled);

                    return compiled;
                }
                catch (Exception exception)
                {
                    _logger.Error(exception.Message);
                }

                return new {};
            }

            _logger.Error($"File not found - '{fileName}'");

            return new { };
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