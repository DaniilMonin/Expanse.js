namespace Expanse.Core.Services.ScriptEngine
{
    public interface IScriptEngineService
    {
        #region Public Properties

        string Description { get; }

        #endregion

        #region Public Methods

        void RunScriptProgram(string fileName);

        #endregion

    }
}