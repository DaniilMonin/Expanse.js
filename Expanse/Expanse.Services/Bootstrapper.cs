﻿#region Using namespaces...

using System.Diagnostics;
using Expanse.Services.CommandLineParser;
using Expanse.Services.JSonSerializer;
using Expanse.Services.Logger;
using Expanse.Services.ProjectExport;
using Expanse.Services.ProjectExport.Templates.Standard;
using Expanse.Services.ScriptEngine;
using Expanse.Services.Templates;
using Expanse.Services.VersionInfo;
using Ninject;

#endregion


namespace Expanse.Services
{
    public sealed class Bootstrapper
    {
        #region Private Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly Core.Services.CommandLineParser.ICommandLineParserService _commandLineParserService;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private static IKernel _kernel;

        #endregion

        #region Constructor

        [Inject, DebuggerStepThrough]
        public Bootstrapper(Core.Services.CommandLineParser.ICommandLineParserService commandLineParserService)
        {
            _commandLineParserService = commandLineParserService;
        }

        #endregion

        #region Public Methods

        [DebuggerStepThrough]
        public static Bootstrapper Load()
        {
            _kernel = new StandardKernel();

            //TODO move it to another class, make dynamic loading

            _kernel.Bind<Bootstrapper>().ToSelf().InSingletonScope();

            _kernel.Bind<CommandLineParserService, Core.Services.CommandLineParser.ICommandLineParserService>()
                .To<CommandLineParserService>()
                .InSingletonScope();

            _kernel.Bind<JSonSerializerService, Core.Services.JSonSerializer.IJSonSerializerService>()
                .To<JSonSerializerService>()
                .InSingletonScope();

            _kernel.Bind<LoggerService, Core.Services.Logger.LoggerService>()
                .To<LoggerService>()
                .InSingletonScope();

            _kernel.Bind<TemplatesService, Core.Services.Templates.ITemplatesService>()
                .To<TemplatesService>()
                .InSingletonScope();

            _kernel.Bind<JavaScriptEngineService, Core.Services.ScriptEngine.IScriptEngineService>()
                .To<JavaScriptEngineService>()
                .InSingletonScope();

            _kernel.Bind<VersionInfoService, Core.Services.VersionInfo.IVersionInfoService>()
                .To<VersionInfoService>()
                .InSingletonScope();

            _kernel.Bind<ProjectExportService, Core.Services.ProjectExport.IProjectExportService>()
                .To<ProjectExportService>()
                .InSingletonScope();

            _kernel.Bind<StandardJavaScriptExtensionPackage, Core.Services.ScriptEngine.ScriptEngineExtensionPackage>()
                .To<StandardJavaScriptExtensionPackage>()
                .InSingletonScope();

            _kernel.Bind<StandardProjectTemplate, Core.Services.ProjectExport.Templates.IProjectExportTemplate>()
                .To<StandardProjectTemplate>()
                .InSingletonScope();

            _kernel.Bind<CustomScriptFile, IStandardFile>().To<CustomScriptFile>();
            _kernel.Bind<MainScriptFile, IStandardFile>().To<MainScriptFile>();

            return _kernel.Get<Bootstrapper>();
        }

        [DebuggerStepThrough]
        public void Start(string[] args) => _commandLineParserService.Parse(args);

        #endregion

    }
}