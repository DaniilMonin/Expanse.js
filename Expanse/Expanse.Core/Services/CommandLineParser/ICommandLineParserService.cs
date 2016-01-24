namespace Expanse.Core.Services.CommandLineParser
{
    public interface ICommandLineParserService
    {
        #region Public Methods

        void Parse(string[] args);

        #endregion

    }
}