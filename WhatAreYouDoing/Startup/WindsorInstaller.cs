using System;
using System.Configuration;
using System.Windows;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WhatAreYouDoing.Display.Main;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.TaskbarIcon;
using WhatAreYouDoing.Utilities;
using ViewModel = WhatAreYouDoing.Display.History.ViewModel;

namespace WhatAreYouDoing.Startup
{
    public class WindsorInstaller : IWindsorInstaller
    {
        private IWindsorContainer container;
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
        public const string Application = "Application";
        public const string SettingsViewModel = "SettingsViewModel";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.container = container;
            container.Kernel.AddFacility<TypedFactoryFacility>();

            RegisterViewModels();

            RegisterContexts();

            container.Register(Component.For<IApplicationWrapper>()
                .ImplementedBy<ApplicationWrapper>()
                .Named(Application));

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
                .DependsOn(Dependency.OnComponent(typeof (IDataSourceFactory), Datasourcefactory))
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
        {
            container.Register(Component.For<Display.History.ViewModel>()
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
                .DependsOn(Dependency.OnComponent(typeof (Application), Application))
                .Named(Notifyiconviewmodel));

            container.Register(Component.For<Display.Settings.ViewModel>()
                .DependsOn(Dependency.OnComponent(typeof (WhatAreYouDoing.Display.Settings.Context), SettingsContext))
                .Named(SettingsViewModel));

        }
    }
}