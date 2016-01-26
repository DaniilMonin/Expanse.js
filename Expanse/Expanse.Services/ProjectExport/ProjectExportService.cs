#region Using namespaces...

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.ProjectExport;
using Ninject;

#endregion


namespace Expanse.Services.ProjectExport
{
    internal sealed class ProjectExportService : IProjectExportService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IEnumerable<IProjectExportTemplate>
            _templates;

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public ProjectExportService(LoggerService logger, IEnumerable<IProjectExportTemplate> templates)
        {
            _logger = logger;
            _templates = templates;
        }

        #endregion

        #region Public Methods

        public void Export(string path, string projectName, string projectType)
        {
            var template = _templates.FirstOrDefault(o => string.CompareOrdinal(o.ProjectType, projectType) == 0);

            if (template == null)
            {
                _logger.Error($"Template not found {projectType}");

                return;
            }

            _logger.Info($"Creating new project type of {projectType} '{projectName}' at '{path}'");
        }

        #endregion
    }
}