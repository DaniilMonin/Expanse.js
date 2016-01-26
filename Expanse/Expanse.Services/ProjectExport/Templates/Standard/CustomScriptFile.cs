#region Using namespaces...

using System.Collections.Generic;
using System.Diagnostics;
using Expanse.Core.Common;
using Expanse.Core.Services.ProjectExport;
using Expanse.Core.Services.ProjectExport.Templates;
using Expanse.Core.Services.ScriptEngine;
using Ninject;

#endregion


namespace Expanse.Services.ProjectExport.Templates.Standard
{
    internal sealed class CustomScriptFile : FileTemplate<CustomScriptTemplate>, IStandardFile
    {
        #region Constructor

        [Inject, DebuggerStepThrough]
        public CustomScriptFile(IEnumerable<ScriptEngineExtensionPackage> packages) : base(packages)
        {
        }

        #endregion

        #region Public Properties

        public override string FileName => "helloWorldModule.js";

        public override SpecialFolder Folder => SpecialFolder.Modules;

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