using System;
using System.Windows;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WhatAreYouDoing.Factories;
using WhatAreYouDoing.History;
using WhatAreYouDoing.Main;
using WhatAreYouDoing.Persistance;
using WhatAreYouDoing.TaskbarIcon;
using WhatAreYouDoing.Utilities;

namespace WhatAreYouDoing.Startup
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public const string Mainwindowcontext = "MainWindowContext";
        public const bool InMemory = false;
        public const string Datasourcefactory = "DatasourceFactory";
        public const string Mainwindowfactory = "MainWindowFactory";
        public const string Mainwindowviewmodel = "MainWindowViewModel";
        public const string Mainwindow = "MainWindow";
        public const string Scheduler = "Scheduler";
        public const string Notifyiconviewmodel = "NotifyIconViewModel";
        public const string Datasource = "DataSource";
        public const string HistoryViewmodel = "History.ViewModel";
        public const string Application = "Application";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();

            container.Register( Component.For<IHistoryViewModel>() .ImplementedBy<History.ViewModel>().DependsOn(Dependency.OnComponent(typeof(IViewModelContext),typeof(History.ViewModel))) .Named(HistoryViewmodel) );
           
            container.Register( Component.For<IApplicationWrapper>() .ImplementedBy<ApplicationWrapper>() .Named(Application) );

            container.Register( Component.For<Main.ViewModel>() .DependsOn(Dependency.OnComponent(typeof (IViewModelContext), Mainwindowcontext), Dependency.OnComponent(typeof(IHistoryViewModel), HistoryViewmodel)) .LifestyleTransient() .Named(Mainwindowviewmodel) );

            container.Register( Component.For<IDataSourceFactory>() .ImplementedBy<DatasourceFactory>() .DependsOn(Dependency.OnValue<bool>(InMemory)) .Named(Datasourcefactory) );

            container.Register( Component.For<IViewModelContext>() .ImplementedBy<ViewModelContext>() .LifestyleTransient() .Named(Mainwindowcontext) );

            container.Register( Component.For<Scheduler>() .Named(Scheduler) ); container.Register( Component.For<Func<MainWindow>>() .AsFactory() .Named(Mainwindowfactory) );

            container.Register( Component.For<MainWindow>() .LifestyleTransient() .Named(Mainwindow) ); 

            container.Register( Component.For<NotifyIconViewModel>() .DependsOn(Dependency.OnComponent(typeof (Application), Application)) .Named(Notifyiconviewmodel) );
        }
    }
}