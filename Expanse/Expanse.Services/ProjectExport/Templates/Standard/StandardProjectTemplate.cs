#region Using namespaces...

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.ProjectExport;
using Ninject;

#endregion


namespace Expanse.Services.ProjectExport.Templates.Standard
{
    internal sealed class StandardProjectTemplate : BaseProjectExportTemplate
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string TypeName = "std";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IEnumerable<IStandardFile> _templates;

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public StandardProjectTemplate(LoggerService logger, IEnumerable<IStandardFile> templates) : base(logger)
        {
            _templates = templates;
        }

        #endregion

        #region Public Properties

        public override string ProjectType => TypeName;

        #endregion

        #region Protected Methods

        protected override void RunExport(string fullPath, string projectName)
        {
            if (Directory.Exists(fullPath))
            {
                foreach (var template in _templates)
                {
                    WriteTemplateToFile(template);
                }

                Logger.Info("Project created");

                return;
            }

            Logger.Error("Path not exist");
        }

        #endregion

        #region Private Methods

        private void WriteTemplateToFile(IStandardFile file)
            => File.WriteAllText(Path.Combine(GetSpecialFolder(file.Folder), file.FileName), file.TransformText());

        #endregion

    }
}