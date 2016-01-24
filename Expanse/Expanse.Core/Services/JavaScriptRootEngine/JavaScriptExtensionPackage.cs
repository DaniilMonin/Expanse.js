#region Using namespaces...

using System;
using System.Collections.Generic;

#endregion


namespace Expanse.Core.Services.JavaScriptRootEngine
{
    public abstract class JavaScriptExtensionPackage
    {
        #region Public Methods

        public abstract IDictionary<string, Delegate> GetExtensions();

        #endregion

    }
}