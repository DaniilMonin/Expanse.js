#region Using namespaces...

using Expanse.Core.Services.ProjectExport;

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