#region Using namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Expanse.Core.Services.JavaScriptRootEngine;
using Expanse.Core.Services.JSonSerializer;
using Expanse.Core.Services.Logger;
using Expanse.Core.Services.Templates;
using Ninject;

#endregion


namespace Expanse.Services.JavaScriptRootEngine
{
    internal sealed class StandardJavaScriptExtensionPackage : JavaScriptExtensionPackage
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly LoggerService _logger;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly ITemplatesService _templates;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly IJSonSerializerService _jSonSerializer;

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public StandardJavaScriptExtensionPackage(
            LoggerService logger,
            ITemplatesService templates,
            IJSonSerializerService jSonSerializer)
        {
            _logger = logger;
            _templates = templates;
            _jSonSerializer = jSonSerializer;
        }

        #endregion

        #region Public Methods

        [DebuggerStepThrough]
        public override IDictionary<string, Delegate> GetExtensions() => new Dictionary<string, Delegate>
        {
            {"info", new Action<object>(o => _logger.Info(o?.ToString()))},
            {"toJson", new Func<dynamic, string>(_jSonSerializer.Serialize)},
            {"runRazor", new Func<string, dynamic, string>(_templates.Compile)}
        };

        #endregion
    }
}