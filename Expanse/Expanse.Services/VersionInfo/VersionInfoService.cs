#region Using namespaces...

using System;
using System.Diagnostics;
using System.Reflection;
using Expanse.Core.Services.VersionInfo;

#endregion


namespace Expanse.Services.VersionInfo
{
    internal sealed class VersionInfoService : IVersionInfoService
    {
        #region Public Properties

        public Version Version => Assembly.GetEntryAssembly().GetName().Version;
        public string Description => "Some description";
        public string Info => "Expanse";

        #endregion

        #region Public Methods

        [DebuggerStepThrough]
        public string GetVersionInformation() => $"{Info} {Version} ({Description})";

        #endregion

    }
}