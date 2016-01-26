#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Expanse.Core.Common;
using Expanse.Core.Services.Logger;

#endregion


namespace Expanse.Core.Services.ProjectExport.Templates
{
    public abstract class BaseProjectExportTemplate : IProjectExportTemplate
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IDictionary<SpecialFolder, string> _folders = new Dictionary<SpecialFolder, string>();

        #endregion

        #region Constructor

        protected BaseProjectExportTemplate(LoggerService logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Properties

        public abstract string ProjectType { get; }

        #endregion

        #region Public Methods

        public void DoExport(string path, string projectName)
        {
            var projectNameAsChars = projectName.ToCharArray();

            foreach (
                var invalidPathChar in
                    Path.GetInvalidPathChars()
                        .Where(invalidPathChar => projectNameAsChars.Any(o => invalidPathChar == o)))
            {
                _logger.Error($"Invalid project name, bad symbol is '{invalidPathChar}'");

                return;
            }

            var combinedPath = Path.Combine(path, projectName);

            if (Directory.Exists(combinedPath))
            {
                _logger.Error($"Error, path already exists '{combinedPath}'");

                return;
            }

            try
            {
                AddAndCreateSpecialFolder(SpecialFolder.Root, combinedPath);
                AddAndCreateSpecialFolder(SpecialFolder.Log, combinedPath);
                AddAndCreateSpecialFolder(SpecialFolder.Output, combinedPath);
                AddAndCreateSpecialFolder(SpecialFolder.Temp, combinedPath);
                AddAndCreateSpecialFolder(SpecialFolder.Modules, combinedPath);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);

                return;
            }

            RunExport(combinedPath, projectName);
        }

        #endregion

        #region Protected Properties

        protected LoggerService Logger => _logger;

        protected string ModuleDirectoryPath => _folders[SpecialFolder.Modules];

        protected string OutputDirectoryPath => _folders[SpecialFolder.Output];

        protected string TempDirectoryPath => _folders[SpecialFolder.Temp];

        protected string LogDirectoryPath => _folders[SpecialFolder.Log];

        protected string RootDirectoryPath => _folders[SpecialFolder.Root];

        #endregion

        #region Protected Methods

        protected abstract void RunExport(string fullPath, string projectName);

        protected string GetSpecialFolder(SpecialFolder folder) => _folders[folder];

        #endregion

        #region Private Methods

        private void AddAndCreateSpecialFolder(SpecialFolder folder, string path)
        {
            var combinedPath = Path.Combine(path, folder == SpecialFolder.Root ? string.Empty : folder.ToString());

            Directory.CreateDirectory(combinedPath);

            AddSpecialFolder(folder, combinedPath);
        }

        private void AddSpecialFolder(SpecialFolder folder, string path) => _folders.Add(folder, path);

        #endregion

    }
}