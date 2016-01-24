#region Using namespaces...

using System.Diagnostics;
using Expanse.Core.Services.CommandLineParser;
using Expanse.Core.Services.JSonSerializer;
using Expanse.Services.JSonSerializer;
using Expanse.Services.Logger;
using Expanse.Services.Templates;
using Ninject;

#endregion


namespace Expanse.Services
{
    public sealed class Bootstrapper
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly ICommandLineParserService
            _commandLineParserService;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private static IKernel _kernel;

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        private Bootstrapper(ICommandLineParserService commandLineParserService)
        {
            _commandLineParserService = commandLineParserService;
        }

        #endregion

        #region Public Methods

        [DebuggerStepThrough]
        public static Bootstrapper Load()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<Bootstrapper>().ToSelf().InSingletonScope();

            _kernel.Bind<JSonSerializerService, IJSonSerializerService>()
                .To<JSonSerializerService>()
                .InSingletonScope();

            _kernel.Bind<LoggerService, Core.Services.Logger.LoggerService>()
                .To<LoggerService>()
                .InSingletonScope();

            _kernel.Bind<TemplatesService, Core.Services.Templates.ITemplatesService>()
                .To<TemplatesService>()
                .InSingletonScope();

            /*
            Load
            */

            return _kernel.Get<Bootstrapper>();
        }

        [DebuggerStepThrough]
        public void Start(string[] args) => _commandLineParserService.Parse(args);

        #endregion

    }
}