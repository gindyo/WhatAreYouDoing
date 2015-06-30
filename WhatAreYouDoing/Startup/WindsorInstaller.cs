using System;
using System.Windows;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WhatAreYouDoing.Display.Main;
using WhatAreYouDoing.Display.Settings;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.ObjectFactory;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.TaskbarIcon;
using WhatAreYouDoing.ThirdPartyWrappers;
using WhatAreYouDoing.Utilities;
using ViewModel = WhatAreYouDoing.Display.Main.ViewModel;

 WhatAreYouDoing.Startup
{
    public class WindsorInstaller : IWindsorInstaller
    {
        private IWindsorContainer container;
        private string _applicationWrapper;
        public const string SettingsContext = "SettnigsContext";
        public const string BaseViewModelContext = "MainWindowContext";
        public const bool InMemory = false;
        public const string Datasourcefactory = "DatasourceFactory";
        public const string Mainwindowfactory = "MainWindowFactory";
        public const string MainWindowViewModel = "MainWindowViewModel";
        public const string MainWindow = "MainWindow";
        public const string Scheduler = "Scheduler";
        public const string Notifyiconviewmodel = "NotifyIconViewModel";
        public const string Datasource = "DataSource";
        public const string HistoryViewmodel = "History.ViewModel";
        public const string ApplicationHandler = "ApplicationHandler";
        public const string SettingsViewModel = "SettingsViewModel";
        public const string ModelFactory = "ModelFactory";
        public const string ApplicationWrapper = "ApplicationWrapper";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.container = container;
            container.Kernel.AddFacility<TypedFactoryFacility>();

            RegisterViewModels();
            RegisterContexts();

            container.Register(Component.For<IModelFactory>()
                .ImplementedBy<ModelsFactory>()
                .Named(ModelFactory));

            container.Register(Component.For<IApplicationHandler>()
                .ImplementedBy<ApplicationHandler>()
                .DependsOn(Dependency.OnComponent(typeof(IApplicationWrapper), ApplicationWrapper))
                .Named(ApplicationHandler));

            container.Register(Component.For<IApplicationWrapper>()
                .ImplementedBy<ApplicationWrapper>()
                .Named(ApplicationWrapper));

            container.Register(Component.For<IDataSourceFactory>()
                .ImplementedBy<DatasourceFactory>()
                .DependsOn(Dependency.OnValue<bool>(InMemory))
                .Named(Datasourcefactory));


            container.Register(Component.For<Scheduler>()
                .Named(Scheduler));

            container.Register(Component.For<Func<MainWindow>>()
                .AsFactory()
                .Named(Mainwindowfactory));

            container.Register(Component.For<MainWindow>()
                .DependsOn(Dependency.OnComponent(typeof (Display.Main.ViewModel), MainWindowViewModel))
                .LifestyleTransient()
                .Named(MainWindow));

        }


        private void RegisterContexts()
        {
            
            container.Register(Component.For<IViewModelContext>()
                .ImplementedBy<EntriesViewModelContext>()
                .DependsOn(
                    Dependency.OnComponent(typeof (IDataSourceFactory), Datasourcefactory),
                    Dependency.OnComponent(typeof (IModelFactory), ModelFactory)
                )
                .LifestyleTransient()
                .Named(BaseViewModelContext));

            container.Register(Component.For<WhatAreYouDoing.Display.Settings.Context>()
                .DependsOn(
                    Dependency.OnComponent(typeof (IDataSourceFactory), Datasourcefactory)
                    )
                .LifestyleTransient()
                .Named(SettingsContext));
        }

        private void RegisterViewModels()
Display.History.ViewModelntainer.Register(Component.For<Display.History.ViewModel>()
                .DependsOn(Dependency.OnComponent(typeof (IViewModelContext), BaseViewModelContext))
                .Named(HistoryViewmodel));

            container.Register(Component.For<Display.Main.ViewModel>()
                .DependsOn(
                    Dependency.OnComponent(typeof (IViewModelContext), BaseViewModelContext),
                    Dependency.OnComponent(typeof (IHistoryViewModel), HistoryViewmodel)
                )
                .LifestyleTransient()
                .Named(MainWindowViewModel));

            container.Register(Component.For<NotifyIconViewModel>()
                .DependsOn(Dependency.OnComponent(typeof (Application), ApplicationHandler))
                .Named(NoDisplay.Settings.ViewModel          container.Register(Component.For<Display.Settings.ViewModel>()
                .DependsOn(Dependency.OnComponent(typeof (WhatAreYouDoing.Display.Settings.Context), SettingsContext))
                .Named(SettingsViewModel));

        }
    }
}