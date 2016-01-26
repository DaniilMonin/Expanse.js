namespace Expanse.Core.Services.ProjectExport
{
    public interface IProjectExportService
    {
        void Export(string path, string projectName, string projectType);
    }
}