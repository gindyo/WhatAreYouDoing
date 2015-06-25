using System;
using System.Windows;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WhatAreYouDoing.History;
using WhatAreYouDoing.Interfaces;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.TaskbarIcon;
using WhatAreYouDoing.Utilities;
using ViewModel = WhatAreYouDoing.History.ViewModel;

namespace WhatAreYouDoing.Startup
{
    public class WindsorInstaller : IWindsorInstaller
    {
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

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();

            container.Register(Component.For<IHistoryViewModel>()
                .ImplementedBy<ViewModel>()
                .DependsOn(Dependency.OnComponent(typeof (IViewModelContext), BaseViewModelContext))
                .Named(HistoryViewmodel));

            container.Register(Component.For<IApplicationWrapper>()
                .ImplementedBy<ApplicationWrapper>()
                .Named(Application));

            container.Register(Component.For<Main.ViewModel>()
                .DependsOn(
                    Dependency.OnComponent(typeof (IViewModelContext), BaseViewModelContext),
                    Dependency.OnComponent(typeof (IHistoryViewModel), HistoryViewmodel)
                )
                .LifestyleTransient()
                .Named(MainWindowViewModel));

            container.Register(Component.For<IDataSourceFactory>()
                .ImplementedBy<DatasourceFactory>()
                .DependsOn(Dependency.OnValue<bool>(InMemory))
                .Named(Datasourcefactory));

            container.Register(Component.For<IViewModelContext>()
                .ImplementedBy<ViewModelContext>()
                .DependsOn(Dependency.OnComponent(typeof (IDataSourceFactory), Datasourcefactory))
                .LifestyleTransient()
                .Named(BaseViewModelContext));

            container.Register(Component.For<Scheduler>()
                .Named(Scheduler));

            container.Register(Component.For<Func<MainWindow>>()
                .AsFactory()
                .Named(Mainwindowfactory));

            container.Register(Component.For<MainWindow>()
                .DependsOn(Dependency.OnComponent(typeof (Main.ViewModel), MainWindowViewModel))
                .LifestyleTransient()
                .Named(MainWindow));

            container.Register(Component.For<NotifyIconViewModel>()
                .DependsOn(Dependency.OnComponent(typeof (Application), Application))
                .Named(Notifyiconviewmodel));
        }
    }
}