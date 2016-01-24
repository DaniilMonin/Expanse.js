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

        public string Compile<TModel>(string fileName, TModel model = null)
            where TModel : class
        {
            if (File.Exists(fileName))
            {
                if (_cachedFiles.Contains(fileName))
                {
                    return Engine.Razor.Run(fileName, typeof (TModel), model);
                }

                _cachedFiles.Add(fileName);

                var template = File.ReadAllText(fileName);

                return Engine.Razor.RunCompile(template, fileName, typeof (TModel), model);
            }

            return string.Empty;
        }

        #endregion

    }
}