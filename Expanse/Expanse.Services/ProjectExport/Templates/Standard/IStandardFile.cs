#region Using namespaces...

using Expanse.Core.Common;
using Expanse.Core.Services.ProjectExport;
using Expanse.Core.Services.ProjectExport.Templates;

#endregion


namespace Expanse.Services.ProjectExport.Templates.Standard
{
    public interface IStandardFile
    {
        SpecialFolder Folder { get; }

        string FileName { get; }

        string TransformText();
    }
}