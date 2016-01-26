#region Using namespaces...

using System.Diagnostics;
using Expanse.Core.Services.ProjectExport;

#endregion


namespace Expanse.Services.ProjectExport.Templates.Standard
{
    internal sealed class StandardProjectTemplate : IProjectExportTemplate
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private const string TypeName = "std";

        #endregion

        #region Public Properties

        public string ProjectType => TypeName;

        #endregion

    }
}