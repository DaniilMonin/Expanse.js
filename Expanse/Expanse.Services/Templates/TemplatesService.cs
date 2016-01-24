#region Using namespaces...

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
using Expanse.Core.Services.Templates;

#endregion


namespace Expanse.Services.Templates
{
    internal sealed class TemplatesService : ITemplatesService
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IList<string> _cachedFiles =
            new List<string>();

        #endregion

        #region Public Methods

        public string Compile(string fileName, dynamic model = null)
        {
            if (File.Exists(fileName))
            {
                if (_cachedFiles.Contains(fileName))
                {
                    return Engine.Razor.Run(fileName, null, new { Bag = model });
                }

                _cachedFiles.Add(fileName);

                var template = File.ReadAllText(fileName);

                return Engine.Razor.RunCompile(template, fileName, null, new { Bag = model });
            }

            return string.Empty;
        }

        #endregion

    }
}