#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Expanse.Core.Services.ProjectExport.Templates;

#endregion


namespace Expanse.Core.Services.ProjectEnvironment
{
    [DebuggerDisplay("{ProjectName}")]
    public sealed class ProjectEnvironmentService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IDictionary<string, string> _customSettings =
            new Dictionary<string, string>();

        #endregion

        #region Public Properties

        public string RootProjectFolder { get; set; }

        public string ProjectName { get; set; }

        public string CurrentModulesPath
            => CombinePathWithSpecialFolder(CurrentPath, SpecialFolder.Modules);

        public string CurrentPath
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

        public string ProjectModulesPath
            => CombinePathWithSpecialFolder(RootProjectFolder, SpecialFolder.Modules);

        public string ProjectLogPath
            => CombinePathWithSpecialFolder(RootProjectFolder, SpecialFolder.Log);

        public string ProjectOutputPath
            => CombinePathWithSpecialFolder(RootProjectFolder, SpecialFolder.Output);

        public string ProjectTempPath
            => CombinePathWithSpecialFolder(RootProjectFolder, SpecialFolder.Temp);

        #endregion

        #region Public Methods

        public string CombinePathWithSpecialFolder(string path, SpecialFolder folder)
            => Path.Combine(path, folder.ToString());

        public void AddCustomSetting(string settingName, string settingValue)
            => _customSettings.Add(settingName, settingValue);

        public TValue TryGetCustomSetting<TValue>(string settingName)
        {
            if (_customSettings.ContainsKey(settingName))
            {
                return (TValue) Convert.ChangeType(_customSettings[settingName], typeof (TValue));
            }

            return default(TValue);
        }

        #endregion
    }
}