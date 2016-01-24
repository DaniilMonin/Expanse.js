#region Using namespaces...

using System;
using System.Diagnostics;
using System.Reflection;
using Expanse.Core.Services.ScriptEngine;
using Expanse.Core.Services.VersionInfo;
using Ninject;

#endregion


namespace Expanse.Services.VersionInfo
{
    internal sealed class VersionInfoService : IVersionInfoService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IScriptEngineService _jsEngineService;

        #endregion
        
        #region Constructor

        [Inject, DebuggerStepThrough]
        public VersionInfoService(IScriptEngineService jsEngineService)
        {
            _jsEngineService = jsEngineService;
        }

        #endregion

        #region Public Properties

        public Version Version => Assembly.GetEntryAssembly().GetName().Version;
        public string Description => _jsEngineService.Description;
        public string Info => "JsExpanse";

        #endregion

        #region Public Methods

        [DebuggerStepThrough]
        public string GetVersionInformation() => $"{Info} {Version} ({Description})";

        #endregion
    }
}