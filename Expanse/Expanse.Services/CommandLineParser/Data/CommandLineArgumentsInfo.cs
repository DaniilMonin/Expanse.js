#region Using namespaces...

using System.Diagnostics;

#endregion


namespace Expanse.Services.CommandLineParser.Data
{
    internal static class CommandLineArgumentsInfo
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string NoLogoCommand = "nologo";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string RunCommand = "run";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string CreateProjectCommand = "createproject";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string NewProjectTypeCommand = "typeproject";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string NewProjectPathCommand = "projectpath";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string HelpCommand = "help";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string NoLogoCommandDescription = "Hide logo";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string RunCommandDescription = "Run specific script";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string CreateProjectCommandDescription = "Create new project";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string NewProjectTypeCommandDescription = "New project type";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string NewProjectPathCommandDescription = "Path to new project";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const char NoLogoShortCommand = 'n';
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const char RunShortCommand = 'r';
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const char CreateProjectShortCommand = 'c';
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const char NewProjectTypeShortCommand = 't';
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const char NewProjectPathCommandShortCommand = 'p';
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string HelpShortCommand = "?";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const string DefaultNewProjectTypeArgument = "std";

        #endregion
    }
}