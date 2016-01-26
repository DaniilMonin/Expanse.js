#region Using namespaces...

using System;
using System.Collections.Generic;

#endregion


namespace Expanse.Core.Services.ScriptEngine
{
    public abstract class ScriptEngineExtensionPackage
    {
        #region Public Methods

        public abstract IDictionary<string, Delegate> GetExtensions();

        #endregion
    }
}