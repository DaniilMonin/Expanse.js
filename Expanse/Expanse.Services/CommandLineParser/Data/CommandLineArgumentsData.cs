namespace Expanse.Services.CommandLineParser.Data
{
    internal sealed class CommandLineArgumentsData
    {
        #region Public Properties

        public string ScriptToRunFileName { get; set; }

        public bool NoLogo { get; set; }

        public string NewProjectName { get; set; }

        public string NewProjectPath { get; set; }

        public string NewProjectType { get; set; }

        public bool NoVisualStudioCodeGlobal { get; set; }

        public string LogPath { get; set; }

        #endregion

        #region Internal Methods

        internal bool CanDoExport()
        {
            if (string.IsNullOrWhiteSpace(NewProjectName))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(NewProjectPath);
        }

        internal bool CanRunScript()
        {
            return !string.IsNullOrWhiteSpace(ScriptToRunFileName);
        }

        #endregion
    }
}