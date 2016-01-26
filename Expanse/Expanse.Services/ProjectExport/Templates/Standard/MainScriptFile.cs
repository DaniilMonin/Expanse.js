#region Using namespaces...

using System.Collections.Generic;
using System.Diagnostics;
using Expanse.Core.Services.ProjectExport;
using Expanse.Core.Services.ScriptEngine;
using Ninject;

#endregion


namespace Expanse.Services.ProjectExport.Templates.Standard
{
    internal sealed class MainScriptFile : FileTemplate<MainScriptTemplate>, IStandardFile
    {
        #region Constructor

        [Inject, DebuggerStepThrough]
        public MainScriptFile(IEnumerable<ScriptEngineExtensionPackage> packages) : base(packages)
        {
        }

        #endregion

        #region Public Properties

        public override string FileName => "main.js";

        #endregion

        #region Public Methods

        public override string TransformText()
        {
            if (Template.GlobalExtensionsMethods == null)
            {
                Template.GlobalExtensionsMethods = GetExtensionMethods();
            }

            return Template.TransformText();
        }

        #endregion
    }
}