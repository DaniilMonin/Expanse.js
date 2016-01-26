﻿#region Using namespaces...

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

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public StandardProjectTemplate(LoggerService logger) : base(logger)
        {
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
                var mainScript = new MainScriptTemplate();
                var helloWorld = new CustomScriptTemplate();

                File.WriteAllText(Path.Combine(ModuleDirectoryPath, "helloWorldModule.js"), helloWorld.TransformText());
                File.WriteAllText(Path.Combine(fullPath, "main.js"), mainScript.TransformText());

                return;
            }

            Logger.Error("Path not exist");
        }

        #endregion
    }
}