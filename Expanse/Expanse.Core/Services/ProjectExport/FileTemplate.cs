#region Using namespaces...

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Expanse.Core.Services.ScriptEngine;

#endregion


namespace Expanse.Core.Services.ProjectExport
{
    public abstract class FileTemplate<TTemplate>
        where TTemplate : class, new()
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IEnumerable<ScriptEngineExtensionPackage>
            _packages;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly TTemplate _template = new TTemplate();

        #endregion

        #region Constructor

        protected FileTemplate(IEnumerable<ScriptEngineExtensionPackage> packages)
        {
            _packages = packages;
        }

        #endregion

        #region Public Properties

        public abstract string FileName { get; }

        public virtual SpecialFolder Folder => SpecialFolder.Root;

        #endregion

        #region Public Methods

        public abstract string TransformText();

        #endregion

        #region Protected Properties

        protected TTemplate Template => _template;

        #endregion

        #region Protected Methods

        protected IEnumerable<string> GetExtensionMethods()
            => from package in _packages from extension in package.GetExtensions() select extension.Key;

        #endregion
    }

    public enum SpecialFolder
    {
        Output,
        Log,
        Modules,
        Root,
        Temp,
    }
}