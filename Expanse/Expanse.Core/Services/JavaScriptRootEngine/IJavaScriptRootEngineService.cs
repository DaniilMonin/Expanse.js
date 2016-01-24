namespace Expanse.Core.Services.JavaScriptRootEngine
{
    public interface IJavaScriptRootEngineService
    {
        #region Public Properties

        string Description { get; }

        #endregion


        #region Public Methods

        void RunProgram(string fileName);

        #endregion

    }
}