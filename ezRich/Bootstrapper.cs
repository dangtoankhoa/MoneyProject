using ezRich.Views;
using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Presentation.Desktop.Seedwork.Constants;
using Presentation.Desktop.Seedwork.Error.ErrorHandlers;
using Prism.Regions;
using Infrastructure.CrossCutting.Logging;
using ezRich.Error.ErrorHandlers;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.CrossCutting.NetFramework.Adapter;
using Presentation.Desktop.Seedwork.Logging;
using Prism.Logging;
using Infrastructure.Data.Seedwork;
using ezRich.Infrastructure.Data.BoundedContext.Local.Main.UnitOfWork;
using Domain.Seedwork;
using Infrastructure.Data.Seedwork.EntityFramework;
using CrossCutting = Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.IO;
using Infrastructure.CrossCutting.NetFramework.IO;

namespace ezRich
{
    class Bootstrapper : UnityBootstrapper
    {
        #region Private fields
        private IList<IGlobalError> errorHandlers;
        #endregion

        #region Property Members
        public ReadOnlyCollection<IGlobalError> GlobalErrorHandlers { get; private set; }
        #endregion

        #region Constructors
        public Bootstrapper()
        {
            this.errorHandlers = new List<IGlobalError>();
            this.GlobalErrorHandlers = new ReadOnlyCollection<IGlobalError>(errorHandlers);
        }
        #endregion

        #region Public operation logical methods
        /// <summary>
        /// The shell object
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            Container.RegisterInstance(typeof(Window), WindowNames.MainWindowName, Container.Resolve<Shell>(), new ContainerControlledLifetimeManager());
            return Container.Resolve<Window>(WindowNames.MainWindowName);
        }

        /// <summary>
        /// Initialize shell (MainWindow)
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            // Register views
            var regionManager = this.Container.Resolve<IRegionManager>();
            if (regionManager != null)
            {
                // Register regions & thems contain views
                //regionManager.RegisterViewWithRegion(Presentation.Desktop.Seedwork.Constants.RegionNames.HeaderRegion, typeof(MainHeader));
                //regionManager.RegisterViewWithRegion(Presentation.Desktop.Seedwork.Constants.RegionNames.MainRegion, typeof(EmptyMainRegionMaster));
                //regionManager.RegisterViewWithRegion(Presentation.Desktop.Seedwork.Constants.RegionNames.FlyoutMenuRegion, typeof(Views.MainNavigationMenu));
            }

            // Register services
            this.RegisterServices();
            this.SubscribeErrorHandlers();

            System.Windows.Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configure the container
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Application commands
            //Container.RegisterType<IApplicationCommands, ApplicationCommandsProxy>();

            // Flyout service
            //Container.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());            

            // Localizer-Service
            Presentation.Desktop.Seedwork.Localization.ITranslationProvider translation = new Presentation.Desktop.Seedwork.Localization.XamlTranslationProvider(App.Current.Resources, null);
            Presentation.Desktop.Seedwork.Services.AppCultureService appCultureService = this.Container.Resolve<Presentation.Desktop.Seedwork.Services.AppCultureService>();
            Container.RegisterInstance(typeof(IAppDataStorage), new WindowAppDataStorage(), new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());
            Container.Resolve<IAppDataStorage>().Setup();
            Container.RegisterInstance(typeof(Presentation.Desktop.Seedwork.Services.IAppCultureService), appCultureService, new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());
            Container.RegisterInstance(typeof(Presentation.Desktop.Seedwork.Localization.ITranslationProvider), translation, new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());
            Container.RegisterInstance(typeof(Presentation.Desktop.Seedwork.Localization.ITranslateResourceProvider), translation, new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());
            Presentation.Desktop.Seedwork.Localization.TranslationManager.Instance.TranslationProvider = translation;
            // Load localizer for Main module
            Container.Resolve<Presentation.Desktop.Seedwork.Localization.ITranslateResourceProvider>().LoadResource(System.Reflection.Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Configure the module catalog
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            // Register Modules
        }

        /// <summary>
        /// Register services
        /// </summary>
        private void RegisterServices()
        {

            // Register application common services
            this.Container.RegisterInstance<ILogger>(CrossCutting.Logging.LoggerFactory.Get());
            var uiErrorHandler = this.Container.Resolve<UIGlobalErrorHandler>(new ParameterOverride("application", System.Windows.Application.Current).OnType<UIGlobalErrorHandler>());
            var backgroundThreadErrorHandler = this.Container.Resolve<BackgroundThreadErrorHandler>();
            this.errorHandlers.Add(uiErrorHandler);
            this.errorHandlers.Add(backgroundThreadErrorHandler);

            // Register TypAdapterFactory
            this.Container.RegisterType<ITypeAdapterFactory, AutoMapperTypeAdapterFactory>(new ContainerControlledLifetimeManager());
            var typeAdapterFactory = this.Container.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);

            // Register Domain Service
            this.Container.RegisterType<IQueryableUnitOfWork, MainBoundContext>();
            this.Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            //behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }
        #endregion

        #region Private utilities methods
        protected override ILoggerFacade CreateLogger()
        {
            ILoggerFactory logger = new CrossCutting.NetFramework.Logging.NLoggerFactory();
            CrossCutting.Logging.LoggerFactory.SetCurrent(logger);
            return new LoggerFacade(logger: logger);
        }

        private void SubscribeErrorHandlers()
        {
            foreach (IGlobalError errorHandler in this.GlobalErrorHandlers)
            {
                errorHandler.Subscribe();
            }
        }

        private void UnSubscribeErrorHandlers()
        {
            foreach (IGlobalError errorHandler in this.GlobalErrorHandlers)
            {
                errorHandler.Subscribe();
            }
        }

        public void Shutdown()
        {
            this.UnSubscribeErrorHandlers();
        }
        #endregion
    }
}
