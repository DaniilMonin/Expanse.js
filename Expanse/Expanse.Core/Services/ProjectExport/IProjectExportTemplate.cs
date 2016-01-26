namespace Expanse.Core.Services.ProjectExport
{
    public interface IProjectExportTemplate
    {
        #region Public Properties

        string ProjectType { get; }

        #endregion

        #region Public Methods

        void DoExport(string path, string projectName);

        #endregion
    }
}