#region Using namespaces...

using System;

#endregion


namespace Expanse.Core.Services.VersionInfo
{
    public interface IVersionInfoService
    {
        #region Public Properties

        Version Version { get; }

        string Description { get; }

        string Info { get; }

        #endregion

        #region Public Methods

        string GetVersionInformation();

        #endregion

    }
}