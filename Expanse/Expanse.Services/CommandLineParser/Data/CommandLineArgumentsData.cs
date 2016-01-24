namespace Expanse.Services.CommandLineParser.Data
{
    internal sealed class CommandLineArgumentsData
    {
        #region Public Properties

        public string ProgramFileName { get; set; }

        public bool NoLogo { get; set; }

        public string NewProjectName { get; set; }

        public string NewProjectPath { get; set; }

        #endregion
    }
}