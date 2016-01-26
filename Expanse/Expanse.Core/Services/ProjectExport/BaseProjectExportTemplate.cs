﻿#region Using namespaces...

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Expanse.Core.Services.Logger;

#endregion


namespace Expanse.Core.Services.ProjectExport
{
    public abstract class BaseProjectExportTemplate : IProjectExportTemplate
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

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
                _logger.Error($"Invalid project name, bad symbol is {invalidPathChar}");

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
                Directory.CreateDirectory(combinedPath);
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

        #endregion

        #region Protected Methods

        protected abstract void RunExport(string fullPath, string projectName);

        #endregion
    }
}