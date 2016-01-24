#region Using namespaces...

using System.Diagnostics;
using Expanse.Services;

#endregion


namespace Expanse
{
    class Program
    {
        #region Entry Point

        [DebuggerStepThrough]
        static void Main(string[] args) => Bootstrapper.Load().Start(args);

        #endregion
    }
}
